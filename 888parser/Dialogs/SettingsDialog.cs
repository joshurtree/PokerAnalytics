using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using HandHistories.Objects.GameDescription;
using System.Collections.Specialized;
using HandHistories.Parser.Parsers.Base;
using PokerAnalytics.Properties;

namespace PokerAnalytics.Dialogs
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            PokerSites.SelectedIndexChanged += (s, e) =>
                {
                    bool itemSelected = PokerSites.SelectedIndex != -1;
                    ModifyButton.Enabled = itemSelected;
                    RemoveButton.Enabled = itemSelected;
                };
            LoadValues();
        }
        
        private void LoadValues()
        {
            PokerSites.Items.Clear();
            if (Settings.Default.Sites != null)
            {
                foreach (string site in Settings.Default.Sites)
                {
                    SiteDetails details = new SiteDetails(site);
                    PokerSites.Items.Add(details);
                }
            }

            autoUpdate.Checked = Settings.Default.autoUpdate;
        }

        #region event handlers
        private void ok_Click(object sender, EventArgs e)
        {
            //Properties.Settings settings = Properties.Settings.Default;

            Settings.Default.Sites.Clear();
            foreach (object item in PokerSites.Items)
                Settings.Default.Sites.Add(((SiteDetails) item).ToSettingString());
             
            Settings.Default.autoUpdate = autoUpdate.Checked;
            Settings.Default.Save();
            Hide();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            LoadValues();   // Revert to saved values
            Hide();
        }

        private void clearDatabase_Click(object sender, EventArgs e)
        {
            DatabaseHandler.getInstance().ClearDatabase();
        }

        private void Settings_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                LoadValues();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            SiteSetup siteSetup = new SiteSetup();
            if (siteSetup.ShowDialog(this) == DialogResult.OK)
            {
                SiteDetails details = new SiteDetails((SiteName)siteSetup.PokerSites.SelectedItems[0],
                                                      siteSetup.HandHistoryLocation.Text, 
                                                      siteSetup.TournamentSummaryLocation.Text);
                PokerSites.Items.Add(details);
            }
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            SiteDetails item = (SiteDetails) PokerSites.SelectedItem;
            SiteSetup siteSetup = new SiteSetup(item);
            if (siteSetup.ShowDialog(this) == DialogResult.OK)
            {
                SiteDetails details = new SiteDetails(((SiteName)siteSetup.PokerSites.SelectedItems[0]),
                                                      siteSetup.HandHistoryLocation.Text, 
                                                      siteSetup.TournamentSummaryLocation.Text);
                (PokerSites.Items[PokerSites.SelectedIndex]) = details;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            PokerSites.Items.RemoveAt(PokerSites.SelectedIndex);
        }
        #endregion
    }
}