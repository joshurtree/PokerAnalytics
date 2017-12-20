using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.FastParser.Base;
using System.Text.RegularExpressions;

namespace HandHistories.Parser.Parsers.FastParser._888
{
    public class Poker888TournamentParserImpl : TournamentParserFast
    {
        protected override Buyin ParseBuyin(string[] handLines)
        {
            if (handLines[2].Contains("Free"))
                return new Buyin(0, 0, Currency.USD);
            else
                return ParseBuyinValues(handLines[2]);
        }

        protected override Buyin ParseRebuy(string[] handLines, out int count)
        {
            if (handLines[3].StartsWith("Rebuy"))
            {
                count = Int32.Parse(new Regex(@"\d+").Match(handLines[4]).Value);
                return ParseBuyinValues(handLines[3]);
            } else
            {
                count = 0;
                return new Buyin();
            }

        }

        protected override Buyin ParseAddon(string[] handLines)
        {
            throw new NotImplementedException();
        }

        private static Regex BuyinPattern = new Regex(@"[\d,]*\.?\d{2}", RegexOptions.Compiled);
        private Buyin ParseBuyinValues(string line)
        {
            MatchCollection values = BuyinPattern.Matches(line);

            decimal prizePool = Decimal.Parse(values[0].Value);
            decimal rake = 0;

            if (values.Count > 1)
                rake = Decimal.Parse(values[1].Value);

            return new Buyin(prizePool, rake, line.Contains("$") ? Currency.USD : Currency.PlayMoney);
        }

        protected override bool ParseIsSitAndGo(string[] handLines)
        {
            return FileName != null && FileName.Contains("Sit & Go");
        }

        protected override string ParseId(string[] handLines)
        {
            return new Regex(@"\d+").Match(handLines[1]).Value;
        }

        protected override int ParsePlayers(string[] handLines)
        {
            Match match = new Regex(@"(?<=/)\d+").Match(handLines[handLines.Count()-1]);
            return Int32.Parse(match.Value);
        }

        protected override int ParsePosition(string[] handLines)
        {
            Match match = new Regex(@"\d+(?=/)").Match(handLines[handLines.Count()-1]);
            return Int32.Parse(match.Value);
        }

        private static Regex WinningsPattern = new Regex(@"(?<=won\s\$?)[\d,]+\.?\d{2}");
        protected override decimal ParseWinnings(string[] handLines)
        {
            Match match = WinningsPattern.Match(handLines[handLines.Count()-1]);
            return match.Success ? Decimal.Parse(match.Value) : 0;
        }
    }
}
