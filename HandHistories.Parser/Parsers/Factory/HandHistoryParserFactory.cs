using System;
using System.Collections.Generic;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Hand;
using HandHistories.Parser.Compression;
using HandHistories.Parser.Parsers.Base;
using System.IO;
using System.Collections;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    public abstract class HandHistoryParserFactory 
    {
        public SiteDetails SiteDetails { get; }
        //public HandHistoryParserFast HandHistoryParser { get; }
        //public TournamentParserFast TournamentParser { get; }

        public virtual HandHistoryParserFast HandHistoryParser
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual TournamentParserFast TournamentParser
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected HandHistoryParserFactory(SiteName site)
        {
            SiteDetails = new SiteDetails(site);
        }

        protected HandHistoryParserFactory(SiteDetails siteDetails)
        {
            SiteDetails = siteDetails;
        }

        public static bool HasHandHistoryParserFactory(SiteName siteName)
        {
            try
            {
                GetHandHistoryParserFactory(siteName);
                return true;
            }
            catch (NotImplementedException e)
            {
                return false;
            }
        }

        public static HandHistoryParserFactory GetHandHistoryParserFactory(SiteName siteName)
        {
            return GetHandHistoryParserFactory(new SiteDetails(siteName));
        }

        public static HandHistoryParserFactory GetHandHistoryParserFactory(SiteDetails siteDetails)
        {
            switch ((SiteName.Values) siteDetails.Site)
            {
                
                case SiteName.Values.PartyPokerEs:
                case SiteName.Values.PartyPokerFr:
                case SiteName.Values.PartyPokerNJ:
                case SiteName.Values.PartyPokerIt:
                case SiteName.Values.PartyPoker:
                    return new PartyPokerParserFactory(siteDetails);
                case SiteName.Values.PokerStars:
                case SiteName.Values.PokerStarsFr:
                case SiteName.Values.PokerStarsIt:
                case SiteName.Values.PokerStarsEs:
                    return new PokerStarsParserFactory(siteDetails);
                case SiteName.Values.Merge:
                    return new MergeParserFactory(siteDetails);
                case SiteName.Values.IPoker:
                    return new IPokerParserFactory(siteDetails);
                case SiteName.Values.IPoker2:
                    return new IPokerParserFactory(siteDetails);
                case SiteName.Values.OnGame:
                    return new OnGameParserFactory(siteDetails);
                case SiteName.Values.OnGameFr:
                    return new OnGameParserFactory(siteDetails);
                case SiteName.Values.OnGameIt:
                    return new OnGameParserFactory(siteDetails);                    
                case SiteName.Values.Pacific:
                    return new Poker888ParserFactory(siteDetails);
                case SiteName.Values.Entraction:
                    return new EntractionParserFactory(siteDetails);
                case SiteName.Values.FullTilt:
                    return new FullTiltPokerParserFactory(siteDetails);
                case SiteName.Values.MicroGaming:
                    return new MicroGamingParserFactory(siteDetails);
                case SiteName.Values.Winamax:
                    return new WinamaxParserFactory(siteDetails);
                case SiteName.Values.WinningPoker:
                    return new WinningPokerNetworkParserFactory(siteDetails);
                case SiteName.Values.BossMedia:
                    return new BossMediaParserFactory(siteDetails);
                default:
                    throw new NotImplementedException("GetHandHistorySummaryParser: No full regex parser for " + siteDetails.Site);
            }
        }

        public virtual bool IsHandHistoryFile(FileInfo filename)
        {
            return true;
        }

        public virtual TournamentSummary ParseTournamentSummary(string handHistoryFile)
        {
            string path = Path.Combine(SiteDetails.TournamentsLocation, GetTournamentFile(Path.GetFileName(handHistoryFile)));
            ((TournamentParserFast)TournamentParser).FileName = path;
            return File.Exists(path) ? TournamentParser.ParseTournamentSummary(File.ReadAllText(path)) : null;
        }

        public virtual string GetTournamentFile(string handHistoryFile) { return handHistoryFile; }
        public virtual bool HasTournamentSummaryParser() { return false; }

        public IEnumerable<HandHistory> ParseHandHistories(string file, bool rethrowExceptions = false)
        {
            string path = Path.IsPathRooted(file) ? file : Path.Combine(SiteDetails.HandsLocation, file);
            HandHistoryParser.FileName = file;
            string text = File.ReadAllText(path);
            IEnumerable<string> hands = HandHistoryParser.SplitUpMultipleHands(text);
            HandHistoryParser.Reset();

            foreach (string hand in hands)
            {
                HandHistory parsedHand = HandHistoryParser.ParseFullHandHistory(hand, rethrowExceptions);
                if (parsedHand != null)
                    yield return parsedHand;
            }
        }
    }
}
