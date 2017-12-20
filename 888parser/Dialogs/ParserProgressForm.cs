using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HandHistories.Objects.Hand;
using HandHistories.Objects.Actions;
using HandHistories.Objects.Cards;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Players;
using HandHistories.Parser.Parsers.Factory;
using HandHistories.Parser.Parsers.FastParser.Base;
using HandHistories.Parser.Parsers.Exceptions;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using HandHistories.Parser.Parsers.Base;
using PokerAnalytics.Properties;
using System.Collections.Specialized;

namespace PokerAnalytics.Dialogs
{
    public partial class ParserProgressForm : Form
    {
        private string handHistoryPath;
        private int handsFound;
        private int tournamentsFound;
        private DateTime lastUpdate;

        private DatabaseHandler DatabaseHandler;

        public ParserProgressForm()
        {
            InitializeComponent();

            DatabaseHandler = DatabaseHandler.getInstance();

            Properties.Settings settings = Properties.Settings.Default;
            //handHistoryPath = settings.handHistoryPath;
            lastUpdate = settings.lastUpdate;
            parsingWorker.RunWorkerAsync();
        }

        private void parsingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<HandHistoryParserFactory, IEnumerable<FileInfo>> factories =
                new Dictionary<HandHistoryParserFactory, IEnumerable<FileInfo>>();
            int totalFiles = 0;
            foreach (string siteDetailsString in Settings.Default.Sites)
            {
                SiteDetails details = new SiteDetails(siteDetailsString);
                HandHistoryParserFactory parserFactory = HandHistoryParserFactory.GetHandHistoryParserFactory(details);
                DirectoryInfo info = new DirectoryInfo(details.HandsLocation);
                IEnumerable<FileInfo> files = info.GetFiles().Where(fi => parserFactory.IsHandHistoryFile(fi)
                                                                        && fi.LastWriteTime > lastUpdate);
                totalFiles += files.Count();
                factories.Add(parserFactory, files);
            }

            handsFound = 0;
            tournamentsFound = 0;

            float fileProgressInc = 100.0f / totalFiles;
            int filesProcessed = 0;
            foreach (HandHistoryParserFactory factory in factories.Keys)
            {
                factory.HandHistoryParser.AmountsAsBigBlindMultiples = true;
                foreach (FileInfo file in factories[factory])
                {
                    int byteCount = 0;
                    HandHistory parsedHand = null;

                    TournamentSummary tournamentSummary = null;

                    if (factory.HasTournamentSummaryParser() && File.Exists(factory.GetTournamentFile(file.FullName)))
                    {
                        tournamentSummary = factory.ParseTournamentSummary(file.FullName);
                        DatabaseHandler.Add(tournamentSummary);
                        ++tournamentsFound;
                    }
                    IEnumerable<HandHistory> hands = factory.ParseHandHistories(file.FullName);
                    float totalHands = (float) hands.Count();
                    int fileHands = 0;

                    foreach (HandHistory hand in hands)
                    {
                        if (parsingWorker.CancellationPending)
                            break;

                        try
                        {
                            parsedHand = hand;
                            string tournamentId = parsedHand.GameDescription.TournamentId;

                            if (tournamentSummary != null)
                                parsedHand.GameDescription.TournamentSummary = tournamentSummary;
                            DatabaseHandler.Add(parsedHand);
                            byteCount += parsedHand.FullHandHistoryText.Length;
                            ++handsFound;
                            ++fileHands;
                            float progress = (int)((filesProcessed + fileHands / totalHands) * fileProgressInc);
                            parsingWorker.ReportProgress((int)progress);
                        }
                        catch (MongoWriteException ex)
                        {
                            Console.Write("Failed");
                        }
                        catch (HandParseException ex)
                        {
                            Console.Write("Parsing failed for: " + string.Concat(ex.HandText) + "\n");
                        }
                    }
                    if (parsingWorker.CancellationPending)
                        break;

                    ++filesProcessed;
                    parsingWorker.ReportProgress((int)(filesProcessed * fileProgressInc));
                }
            }

            parsingWorker.ReportProgress(100);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (parsingWorker.IsBusy)
                parsingWorker.CancelAsync();

            Dispose();
        }

        private void parsingWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            progressLabel.Text = string.Format("{0} hands and {1} tournaments parsed so far...", handsFound, tournamentsFound);
        }

        private void parsingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                Properties.Settings.Default.lastUpdate = DateTime.Now;
                Properties.Settings.Default.Save();
            }
            Dispose();
        }
    }
}
