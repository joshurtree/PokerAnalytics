using HandHistories.Objects.Cards;
using System;
using System.Windows.Forms;

namespace PokerAnalytics
{
    public partial class Main
    {
        public const int MAX_SEAT_NUM = 10;
        private TextBox[] RangeTextBoxes = new TextBox[MAX_SEAT_NUM];
        private Button[] RandomButtons = new Button[MAX_SEAT_NUM];
        private Button[] TwoSigmaRangeButtons = new Button[MAX_SEAT_NUM];
        private Button[] OneSigmaRangeButtons = new Button[MAX_SEAT_NUM];
        private Button[] SelectRangeButtons = new Button[MAX_SEAT_NUM];


        public void SetupHandEvaluator()
        {
            for (int i = 0; i < MAX_SEAT_NUM; ++i)
            {
                HandEvalLayout.Controls.Add(new Label { Text = string.Format("Player {0}", i) });
                RangeTextBoxes[i] = new TextBox();
                RangeTextBoxes[i].ReadOnly = true;
                HandEvalLayout.Controls.Add(RangeTextBoxes[i]);

                RandomButtons[i] = new Button { Text = "Random"  };
                RandomButtons[i].Click += (s, e) => SetupRange(i, "Random", HandRange.AllHands());
                HandEvalLayout.Controls.Add(RandomButtons[i]);

                TwoSigmaRangeButtons[i] = new Button { Text = "95%" };
                //TwoSigmaRangeButtons[i].Click += (s, e) => SetupRange(i, ;
            }
        }

        private void SetupRange(int index, string text, HandRange tag)
        {
            RangeTextBoxes[index].Text = text;
            RangeTextBoxes[index].Tag = tag;
        }
    }
}