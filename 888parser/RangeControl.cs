using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HandHistories.Objects.Cards;
using System.Collections.Concurrent;

namespace PokerAnalytics
{
    public partial class RangeControl : UserControl
    {
        private RangeWorker RangeThread = null;
        private List<Label> rangeLabels = new List<Label>();
        public string ViewName { get; set; }

        public RangeControl()
        {
            InitializeComponent();
            SetupRanges();
        }

        public void SetupRanges()
        {
            foreach (StartingHand hand in StartingHand.AllStartingHands.Values)
            {
                Label label = new Label() { Text = hand.ToString() };
                rangeLabels.Add(label);
                RangesTable.Controls.Add(label);
            }
        }


        public void LoadRange(string viewName, bool implied = true)
        {
            foreach (Label label in rangeLabels)
                label.BackColor = Color.Transparent;

            if (RangeThread != null && RangeThread.IsBusy)
                RangeThread.CancelAsync();

            RangeThread = new RangeWorker(viewName, -PreFlopBetSize.Value, implied);
            RangeThread.ProgressChanged += rangeListener;
            RangeThread.RunWorkerAsync();
        }

        private void rangeListener(Object sender, EventArgs e)
        {
            RangeWorker worker = (RangeWorker)sender;
            Dictionary<StartingHand, double> values = new Dictionary<StartingHand, double>(worker.Counts);
            long count = worker.TotalCount;

            double average = values.Values.Average(); 
            double stdDev = values.StdDev();

            foreach (Label label in rangeLabels)
            {
                StartingHand hand = StartingHand.AllStartingHands[label.Text];
                if (values.ContainsKey(hand))
                {
                    double deviations = (values[hand] - average) / stdDev;
                    Color colour = Color.Transparent;
                    if (deviations > 2)
                        colour = Color.Yellow;
                    else if (deviations > 1)
                        colour = Color.LightGreen;
                    else if (deviations > 0)
                        colour = Color.YellowGreen;

                    label.Invoke((MethodInvoker)(() =>
                    {
                        label.BackColor = colour;
                    }));
                }
            }

            RangeHandCountLabel.Invoke((MethodInvoker)(() => RangeHandCountLabel.Text = string.Format("Based on {0} hands", count)));
        }

        private void rangeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            switch (box.SelectedIndex)
            {
                case 0:
                    LoadRange(ViewName, false);
                    break;

                case 1:
                    LoadRange(ViewName, true);
                    break;
            }
        }

    }
}
