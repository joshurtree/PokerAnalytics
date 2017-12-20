using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.Objects.GameDescription;

namespace HandHistories.Parser.Parsers.Base
{
    public class SiteDetails
    {
        public SiteName Site { get; set; }
        public string HandsLocation { get; set; }
        public string TournamentsLocation { get; set; }

        public SiteDetails(string details)
        {
            string[] values = details.Split('|');
            HandsLocation = values[0];
            TournamentsLocation = values[1];
            Site = Int32.Parse(values[2]);
        }

        public SiteDetails(SiteName site, string handsLocation = null, string tournamentsLocation = null)
        {
            Site = site;
            HandsLocation = handsLocation;
            TournamentsLocation = tournamentsLocation;
        }

        public string ToSettingString()
        {
            return HandsLocation + "|" + TournamentsLocation + "|" + (int) Site;
        }

        public override string ToString()
        {
            return Site.ToString();
        }
    }
}
