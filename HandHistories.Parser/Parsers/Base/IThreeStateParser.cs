﻿using HandHistories.Objects.Actions;
using HandHistories.Objects.Cards;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandHistories.Parser.Parsers.Base
{
    /// <summary>
    /// A ThreeStateParser splits the parsing of handactions into 3 phases, Blinds, Actions and Showdown
    /// The interface is used for better unit testing of a 3StateParser
    /// </summary>
    public interface IThreeStateParser
    {
        int ParseBlindActions(string[] handLines, ref List<HandAction> handActions, int firstActionIndex);

        int ParseGameActions(string[] handLines, ref List<HandAction> handActions, int firstActionIndex, out Street street);

        void ParseShowDown(string[] handLines, ref List<HandAction> handActions, int firstActionIndex, GameType gameType);
    }
}
