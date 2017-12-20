using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace HandHistories.Objects.GameDescription
{
    [Serializable]
    [DataContract]
    public class Buyin
    {
        [DataMember]
        public Currency Currency { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public decimal Rake { get; set; }

        public decimal PrizePool { get { return Total - Rake; } }
        public Buyin() : this(0, 0, Currency.USD)
        {  
        }

        public Buyin(decimal prizePool, decimal rake, Currency currency) 
        {
            Total = prizePool + rake;
            Rake = rake;
            Currency = currency;
        }

        public string GetCurrencySymbol()
        {
            switch (Currency)
            {
                case Currency.USD:
                    return @"$";
                case Currency.EURO:
                    return @"€";
                case Currency.GBP:
                    return @"£";
                case Currency.PlayMoney:
                case Currency.All:
                    return @"";
                default:
                    throw new Exception("Unrecognized currency " + Currency);
            }
        }

        // TODO: adjust these values
        public LimitGrouping GetLimitGrouping()
        {
            var combinedValue = Total;

            if (combinedValue < 5m)
            {
                return LimitGrouping.Micro;
            }
            if (combinedValue < 20m)
            {
                return LimitGrouping.Small;
            }
            if (combinedValue < 100m)
            {
                return LimitGrouping.Mid;
            }
            
            return LimitGrouping.High;
        }

        public string ToDbSafeString(bool skipCurrency = false)
        {
            // result should be like B100c-100c-10c ( knockout version )
            //                       B100c-10c ( regular version )

            if(PrizePool == 0)
            {
                return "Freeroll";
            }

            string buyin = string.Format("B{0}c-{1}c", (int)(PrizePool * 100), (int)(Rake * 100));

            if (skipCurrency == false)
            {
                buyin = buyin + "-" + Currency;
            }

            return buyin;
        }

        public static Buyin ParseDbSafeString(string buyinString)
        {
            if (buyinString == "Freeroll")
            {
                return AllBuyin();
            }

            if (buyinString[0] == 'B' || buyinString[0] == 'B')
                buyinString = buyinString.Substring(1);
            string[] split = buyinString.Replace("c", "").Split('-');
            
            decimal prizePoolValue = Int32.Parse(split[0]) / 100.0m;
            decimal rake = 0m;
            string currencyString = "All";

            // Format: PrizePool-Rake-Currency
            if (split.Length == 3)
            {
                currencyString = split[2];
                rake = Int32.Parse(split[1]) / 100.0m;
            }

            // Format: PrizePool-Rake
            else if (split.Length == 2)
            {
                rake = Int32.Parse(split[1]) / 100.0m;
            }

            var currency = (Currency)Enum.Parse(typeof(Currency), currencyString, true);

            return new Buyin(prizePoolValue, rake, currency);
        }

        public static Buyin AllBuyin()
        {
            return new Buyin(0, 0, Currency.All);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }


        public string ToString(IFormatProvider format, bool ignoreCurrencyStrings = false, string seperatorCharacter = "-")
        {
            string currencySymbol = (ignoreCurrencyStrings ? string.Empty : GetCurrencySymbol());

            return GetBuyinString(currencySymbol, seperatorCharacter, format);
        }

        private string GetBuyinString(string currencySymbol, string seperatorCharacter, IFormatProvider format)
        {
            string prizePoolString = (PrizePool != Math.Round(PrizePool)) ? PrizePool.ToString("N2", format) : PrizePool.ToString("N0", format);
            string rakeString = (Rake != Math.Round(Rake)) ? Rake.ToString("N2", format) : Rake.ToString("N0", format);

            return string.Format("{0}{1}{3}{0}{2}", currencySymbol, prizePoolString, rakeString, seperatorCharacter);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.ToString().Equals(ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
