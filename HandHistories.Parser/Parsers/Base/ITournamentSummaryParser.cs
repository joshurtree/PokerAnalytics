using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandHistories.Objects.GameDescription;
namespace HandHistories.Parser.Parsers.Base
{
    public interface ITournamentSummaryParser
    {
        TournamentSummary ParseTournamentSummary(string text);
    }
}
