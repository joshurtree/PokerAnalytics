using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerAnalytics
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (InitDatabaseHandler(0))
                Application.Run(new Main(args));
        }

        private static bool InitDatabaseHandler(int attempts)
        {
            try
            {
                DatabaseHandler.getInstance();
            }
            catch (MongoException ex)
            {
                if (MessageBox.Show("Unable to connect to Mongo DB. Check that it is installed.",
                                    "Mongo DB connnection failed", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    return InitDatabaseHandler(++attempts);
                }
                else
                {
                    Application.Exit();
                    return false;
                }
            }

            return true;
        }

    }
}
