using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokertrackerFix
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo directory = new DirectoryInfo("C:\\Users\\Josh\\Documents\\888poker\\HandHistory\\888 Poker");

            IEnumerable<FileInfo> fileInfo = directory.GetFiles();
            Func<string, string> afterColon = s => s.Substring(s.IndexOf(':') + 1).Trim();
            Regex tournIdMatch = new Regex(@"(?<=Game )\d+", RegexOptions.Compiled);
            Dictionary<string, string> filenames = new Dictionary<string, string>();

            foreach (FileInfo fi in fileInfo)
            {
                if (fi.Name.EndsWith("Summary.txt"))
                {
                    string[] text = File.ReadAllLines(fi.FullName);
                    List<string> output = new List<string>();
                    output.Add("***** Cassava Tournament Summary *****");
                    string id = afterColon(text[3]);
                    output.Add("Tournament Id: " + id);
                    string buyin = afterColon(text[6]);
                    string rake = afterColon(text[7]);
                    output.Add("Buyin: " + buyin + " + " + rake);
                    int lineOffset = 0;
                    for (; lineOffset < 3; ++lineOffset)
                        if (text[9 + lineOffset].StartsWith("Table"))
                            break;
                    string tabletype = afterColon(text[9 + lineOffset]);
                    string[] playerline = new string[1];
                    string players = players = afterColon(text[11 + lineOffset]);

                    int test = Int32.Parse(players); // Check is integer
                    for (int i = text.Length - 1; i > 7; --i)
                        if (text[i].Contains("joshurtree"))
                        {
                            playerline = text[i].Split(',');
                            break;
                        }



                    string filename = string.Format("888poker {0} ({1})", parseTableFormat(tabletype, buyin, rake), id);

                    if (playerline[3].Length != 0)
                        output.Add("Rebuys: " + playerline[3].Last());

                    output.Add("joshurtree finished " + afterColon(playerline[0]) + "/" + players + " and won " + afterColon(playerline[2]));


                    File.WriteAllLines("C:\\Users\\Josh\\Documents\\888poker\\HandHistory\\joshurtree\\" + filename + ".txt - Summary.txt", output);
                    filenames.Add(fi.Name.Replace(" - Summary", ""), filename);
                    //File.Move("C:\\Users\\Josh\\Documents\\888poker\\HandHistory\\joshurtree\\"  + fi.Name.Replace(" - Summary", ""), "C:\\Users\\Josh\\Documents\\888poker\\HandHistory\\joshurtree\\" + filename + ".txt");
                }
                else
                {
                    List<string> lines = File.ReadAllLines(fi.FullName).ToList();
                    for (int i = 0; i < lines.Count; ++i)
                    {
                        if (lines[i].StartsWith("****"))
                        {
                            string id = tournIdMatch.Match(lines[i]).Groups[0].Value;
                            lines.Insert(i, "#Game No : " + id);
                            ++i;
                        }
                    }

                    string filename = lines[3].StartsWith("Tournament") ? filenames[fi.Name] + ".txt" : fi.Name;
                    File.WriteAllLines("C:\\Users\\Josh\\Documents\\888poker\\HandHistory\\joshurtree\\" + filename , lines);
                }
            }
        }

        private static string parseTableFormat(string tabletype, string buyin, string rake)
        {
            string tableformat = "";

            if (tabletype.Contains("STEP"))
            {
                int step = 3;
                if (buyin.Equals("$0.01"))
                    step = 1;
                else if (buyin.Equals("$0.09"))
                    step = 2;

                tableformat = string.Format("Step {0} to Super XL & Major Tournaments", step);
            }
            else if (tabletype.Contains("SNG"))
                tableformat = "Sit & Go";
            else if (tabletype.Equals("N/A") && (buyin.Equals("$0.01") || buyin.Equals("$0.34")))
                tableformat = "Sit & Go 6-Max";
            else if (tabletype.Equals("N/A") && buyin.Equals("$0.90"))
                tableformat = "Step 3 to Super XL & Major Tournaments";
            else if (buyin.Equals("$0.00"))
                tableformat = "Freeroll Tournament";
            else
                tableformat = buyin + " + " + rake + " Tournament";

            return tableformat;

        }
    }
}
