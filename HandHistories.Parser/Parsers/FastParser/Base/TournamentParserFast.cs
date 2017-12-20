using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;

namespace HandHistories.Parser.Parsers.FastParser.Base
{
    public abstract class TournamentParserFast : ITournamentSummaryParser
    {
        public string FileName { get; set; }
        protected virtual string[] SplitHandsLines(string handText)
        {
            string[] text = handText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].Trim();
            }
            return text;
        }

        public TournamentSummary ParseTournamentSummary(string handText)
        {
            return ParseTournamentSummary(SplitHandsLines(handText));

        }

        public TournamentSummary ParseTournamentSummary(string[] handLines)
        {
            return new TournamentSummary(ParseId(handLines),
                                         ParseIsSitAndGo(handLines),
                                         ParseBuyin(handLines),
                                         ParsePlayers(handLines),
                                         ParseWinnings(handLines),
                                         ParsePosition(handLines));
        }

        protected abstract string ParseId(string[] handLines);
        protected abstract bool ParseIsSitAndGo(string[] handLines);
        protected abstract Buyin ParseBuyin(string[] handLines);
        protected abstract Buyin ParseRebuy(string[] handLines, out int count);
        protected abstract Buyin ParseAddon(string[] handLines);
        protected abstract decimal ParseWinnings(string[] handLines);
        protected abstract int ParsePosition(string[] handLines);
        protected abstract int ParsePlayers(string[] handLines);
    }
}
