
using PokerAnalytics.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PokerAnalytics
{
    public partial class Main
    {
        private void AddPrize_Click(object sender, EventArgs e)
        {
            PrizeDialog prizeDialog = new PrizeDialog(TournamentAnalytics.PlacesPaid() + 1);
            if (prizeDialog.ShowDialog() == DialogResult.OK)
            {
                TournamentAnalytics.Prizes.Add(prizeDialog.Prize);
                prizesList.Items.Add(new PrizeListBoxItem(TournamentAnalytics.Prizes, TournamentAnalytics.Prizes.Count - 1));
            }
        }

        private void EditPrize_Click(object sender, EventArgs e)
        {
            int index = prizesList.SelectedIndex;
            Prize oldPrize = TournamentAnalytics.Prizes[index];
            PrizeDialog prizeDialog =
                new PrizeDialog(TournamentAnalytics.Prizes.GetRange(0, index).Sum(p => p.PlacesPaid) + 1, oldPrize);
            if (prizeDialog.ShowDialog() == DialogResult.OK)
            {
                TournamentAnalytics.Prizes.RemoveAt(index);
                TournamentAnalytics.Prizes.Insert(index, prizeDialog.Prize);
                prizesList.Items[index] = prizesList.Items[index];
            }
        }

        private void RemovePrize_Click(object sender, EventArgs e)
        {
            int index = prizesList.SelectedIndex;
            prizesList.Items.RemoveAt(prizesList.Items.Count - 1);
            TournamentAnalytics.Prizes.RemoveAt(index);
            for (int i = index; i < prizesList.Items.Count; ++i)
                prizesList.Items[i] = prizesList.Items[i];
        }

        private void playerNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            TournamentAnalytics.PlayerNum = (int)playerNumUpDown.Value;
        }

        private void StartingStackUpDown_ValueChanged(object sender, EventArgs e)
        {
            TournamentAnalytics.StartingStack = (int)StartingStackUpDown.Value;
        }

        private void CurrentStackUpDown_ValueChanged(object sender, EventArgs e)
        {
            TournamentAnalytics.CurrentStack = (int)CurrentStackUpDown.Value;
        }
    
        private void UpdateTournamentValues()
        {
            ExpectedValue.Text = string.Format("Expected tournament value: {0}", TournamentAnalytics.TournamentEV());
        }
    }

    class PrizeListBoxItem
    {
        private List<Prize> Prizes;
        private int Index;

        public PrizeListBoxItem(List<Prize> prizes, int index)
        {
            Prizes = prizes;
            Index = index;
        }

        public override string ToString()
        {
            int startingPlace = Prizes.GetRange(0, Index).Sum(p => p.PlacesPaid) + 1;
            if (Prizes[Index].PlacesPaid > 1)
                return string.Format("${0}, {1}-{2}", Prizes[Index].Value,
                                                      startingPlace,
                                                      startingPlace + Prizes[Index].PlacesPaid - 1);
            else
                return string.Format("${0}, {1}", Prizes[Index].Value, startingPlace);
        }
    }

}