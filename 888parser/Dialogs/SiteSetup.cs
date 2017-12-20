using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Factory;
using HandHistories.Parser.Parsers.Base;
using System.IO;

namespace PokerAnalytics.Dialogs
{
    public partial class SiteSetup : Form
    {
        public SiteSetup() : this(new SiteDetails(SiteName.Values.Unknown))
        {
        }

        public SiteSetup(SiteDetails details)
        {
            InitializeComponent();
            foreach (SiteName.Values s in Enum.GetValues(typeof(SiteName.Values)))
            {
                if (HandHistoryParserFactory.HasHandHistoryParserFactory(s))
                    PokerSites.Items.Add(new SiteName(s));
            }

            HandHistoryLocation.Text = details.HandsLocation;
            TournamentSummaryLocation.Text = details.TournamentsLocation;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            ShowBrowseDialog(val =>
            {
                HandHistoryLocation.Text = val;
                if (TournamentSummaryLocation.Text.Equals(""))
                    TournamentSummaryLocation.Text = HandHistoryLocation.Text;
            });
        }

        private void tournamentBrowseButton_Click(object sender, EventArgs e)
        {
            ShowBrowseDialog(val => TournamentSummaryLocation.Text = val);
        }

        private void ShowBrowseDialog(Action<string> resultHandler)
        {
            using (var historyFolderDialog = new FolderBrowserDialog())
            {
                DialogResult result = historyFolderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(historyFolderDialog.SelectedPath))
                {
                    resultHandler(historyFolderDialog.SelectedPath);
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (PokerSites.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Select a poker site first", "No site selected");
                return;
            }

            if (!IsValidDirectory(HandHistoryLocation.Text))
            {
                MessageBox.Show(this, "Hand history directory selected not valid", "Invalid directory");
                return;
            }


            if (!IsValidDirectory(TournamentSummaryLocation.Text))
            {
                MessageBox.Show(this, "Tournament summary directory selected not valid", "Invalid directory");
                return;
            }

            DialogResult = DialogResult.OK;
            Hide();
        }

        private bool IsValidDirectory(string directory)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);
                return dirInfo.Exists;
            } catch (Exception ex)
            {
                return false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void PokerSites_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
