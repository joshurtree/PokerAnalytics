using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerAnalytics.Dialogs
{
    public partial class PrizeDialog : Form
    {
        public Prize Prize { get; private set; } = new Prize();
        public int StartPlace;

        public PrizeDialog(int startPlace)
        {
            InitializeComponent();
            StartPlace = startPlace;
        }

        public PrizeDialog(int startPlace, Prize prize)
        {
            InitializeComponent();
            Prize = prize;
            StartPlace = startPlace;

            placesUpDown.Value = prize.PlacesPaid;
            prizeUpDown.Value = prize.Value;
        }

        private void PrizeDialog_Load(object sender, EventArgs e)
        {
            placesLabel.Text = string.Format("{0}-{1}", StartPlace, StartPlace + placesUpDown.Value - 1);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            Prize.Value = prizeUpDown.Value;
            Prize.PlacesPaid = (int) placesUpDown.Value;
            Dispose();
            DialogResult = DialogResult.OK;
        }
    }

    public class CurrencyUpDown : NumericUpDown
    {
        public string MajorCurrencySymbol { get; set; } = "$";
        public string MinorCurrencySymbol { get; set; } = "c";

        protected override void UpdateEditText()
        {
            ChangingText = true;
            if (Value > 1)
                Text = (Value * 100).ToString("#0" + MinorCurrencySymbol);
            else
                Text = Value.ToString(MajorCurrencySymbol + "########0.##");
        }
    }
}
