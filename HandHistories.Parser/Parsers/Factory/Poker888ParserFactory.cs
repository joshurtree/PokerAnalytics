using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using System.IO;
using HandHistories.Parser.Parsers.FastParser._888;
using HandHistories.Parser.Parsers.FastParser.Base;
using System.Text.RegularExpressions;
using HandHistories.Objects.Hand;

namespace HandHistories.Parser.Parsers.Factory
{
    class Poker888ParserFactory : HandHistoryParserFactory
    {

        private Poker888FastParserImpl _HandHistoryParser = new Poker888FastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }

        private Poker888TournamentParserImpl _TournamentParser = new Poker888TournamentParserImpl();
        public override TournamentParserFast TournamentParser
        {
            get
            {
                return _TournamentParser;
            }
        }

        public Poker888ParserFactory(SiteDetails details) : base(details)
        { }

        public override bool HasTournamentSummaryParser()
        {
            return true;
        }

        public override bool IsHandHistoryFile(FileInfo file)
        {
            return !file.Name.EndsWith("- Summary.txt");
        }

        public override string GetTournamentFile(string handHistoryFile)
        {
            return handHistoryFile + " - Summary.txt";
        }

        private static Regex StepTournaments = new Regex(@"(?<=Step )\d", RegexOptions.Compiled);
        private static decimal[] StepPrizes = new decimal[] { 0.1m, 1, 5, 30};
        public override TournamentSummary ParseTournamentSummary(string handHistoryFile)
        {
            TournamentSummary summary = base.ParseTournamentSummary(handHistoryFile);
            Match stepMatch = StepTournaments.Match(GetTournamentFile(handHistoryFile));
            
            if (stepMatch.Success)
            {
                HandHistory lastHand = 
                    HandHistoryParser.ParseFullHandHistory(
                        ((HandHistoryParserFast)HandHistoryParser).SplitUpMultipleHands(File.ReadAllText(handHistoryFile)).Last());

                if (lastHand.Hero.StartingStack > -lastHand.HandActions.Where(a => a.PlayerName.Equals(lastHand.Hero.PlayerName))
                                                                      .Sum(a => a.Amount))
                    summary.Winnings = StepPrizes[int.Parse(stepMatch.Groups[0].Value) - 1];
            }

            return summary;
        }
    }
}
