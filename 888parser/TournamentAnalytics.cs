using System.Collections.Generic;
using System;
namespace PokerAnalytics
{
    public class TournamentAnalytics
    {
        public List<Prize> Prizes = new List<Prize>();
        public decimal StartingStack { get; set; }
        public decimal CurrentStack { get; set; }
        public int PlayerNum { get; set; }
        public int RebuyNum { get; set; }
        public decimal AddOnStack { get; set; }
        public int AddOnNum { get; set; }
        public float PlayerSkill { get; set; }

        public TournamentAnalytics()
        {
        }
    
        public decimal TotalChips()
        {
             return (PlayerNum + RebuyNum) * StartingStack + AddOnNum * AddOnStack;
        }

        public int PlacesPaid()
        {
            int placesPaid = 0;
            Prizes.ForEach((o) => placesPaid += o.PlacesPaid);
            return placesPaid;
        }

        public decimal TournamentEV()
        {
            decimal retVal = 0;
            int place = 1;

            foreach (Prize prize in Prizes)
            {
                for (int i = 0; i < prize.PlacesPaid; ++i)
                    retVal += placingProbability(place + i) * prize.Value;
                place += prize.PlacesPaid;
            }
            return retVal;
        }

        private decimal placingProbability(int place)
        {
            return (decimal) (Math.Pow(PlayerSkill, MathExtensions.Log2(PlayerNum / place)) - place > 2 ? Math.Pow(PlayerNum, MathExtensions.Log2(PlayerNum/(place-1))) : 0);
        }
    }

    public class Prize
    {
        public decimal Value { get; set; } = 0;
        public int PlacesPaid { get; set; } = 0;
    }
}