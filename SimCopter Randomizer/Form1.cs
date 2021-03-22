using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimCopter_Randomizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static class Globals
        {
            public static string exeFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        }

        private void carrButt_Click(object sender, EventArgs e)
        {
            //string exeFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            int[,] cNums = new int[30, 11];
            string line = null;
            int x = 0;
            int y = 0;

            //Campaign File
            if (cEasy.Checked)
            {
                cNums = easyCareerNums();
            }
            else if (cHard.Checked)
            {
                cNums = hardCareerNums();
            }
            else if (cBalance.Checked)
            {
                cNums = balancedCareerNums();
            }
            else if (cChaos.Checked)
            {
                cNums = chaosCareerNums();
            }

            if (File.Exists(Globals.exeFolder + "\\career.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\career.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                y = 0;
                            }
                            if (line.Contains("Ctrl10_DataType"))
                            {
                                x++;
                            }
                            if (line.Contains("Value"))
                            {
                                line = "Ctrl" + y.ToString("") + "_Value=" + cNums[x, y].ToString();
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\career.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\career.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                System.Windows.Forms.MessageBox.Show("Career Settings Randomized.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }
        }

        private int[,] easyCareerNums()
        {
            int[,] nums = new int[30, 11];
            Random randMod = new Random();

            for (int x = 0; x < 30; x++)
            {
                //Difficulty
                if (x < 9 || x == 10)
                {
                    nums[x, 0] = 0;
                }
                else if (x == 9 || (x > 10 && x < 18))
                {
                    nums[x, 0] = 0;
                }
                else if (x < 25)
                {
                    nums[x, 0] = 1;
                }
                else
                {
                    nums[x, 0] = 2;
                }

                //Fire
                if (x < 6)
                {
                    nums[x, 1] = 10 - (randMod.Next(11));
                }
                else if (x < 9)
                {
                    nums[x, 1] = 15 - (randMod.Next(11));
                }
                else if (x < 15 || (x > 20 && x < 23) || x > 26)
                {
                    nums[x, 1] = 20 - (randMod.Next(11));
                }
                else
                {
                    nums[x, 1] = 25 - (randMod.Next(11));
                }

                //Crime
                if (x < 3)
                {
                    nums[x, 2] = 0;
                }
                else if (x < 6)
                {
                    nums[x, 2] = 10 - (randMod.Next(11));
                }
                else if (x < 21 || x == 29)
                {
                    nums[x, 2] = 20 - (randMod.Next(11));
                }
                else
                {
                    nums[x, 2] = 25 - (randMod.Next(11));
                }

                //Rescue
                if (x < 3)
                {
                    nums[x, 3] = 0;
                }
                else if (x < 12)
                {
                    nums[x, 3] = 10 - (randMod.Next(11));
                }
                else if (x < 15 || x == 29)
                {
                    nums[x, 3] = 15 - (randMod.Next(11));
                }
                else if (x < 23 || (x > 24 && x < 29))
                {
                    nums[x, 3] = 20 - (randMod.Next(11));
                }
                else
                {
                    nums[x, 3] = 25 - (randMod.Next(11));
                }

                //Riot
                if (x < 21)
                {
                    nums[x, 4] = 0;
                }
                else if (x < 25)
                {
                    nums[x, 4] = 5 - (randMod.Next(6));
                }
                else if (x < 27)
                {
                    nums[x, 4] = 10 - (randMod.Next(11));
                }
                else
                {
                    nums[x, 4] = 20 - (randMod.Next(11));
                }

                //Traffic
                if (x < 6)
                {
                    nums[x, 5] = 30 - (randMod.Next(11));
                }
                else if (x < 9)
                {
                    nums[x, 5] = 20 - (randMod.Next(11));
                }
                else if (x < 15)
                {
                    nums[x, 5] = 15 - (randMod.Next(11));
                }
                else if (x < 23 || x == 29)
                {
                    nums[x, 5] = 10 - (randMod.Next(11));
                }
                else
                {
                    nums[x, 5] = 5 - (randMod.Next(6));
                }

                //MedEvac
                if (x < 3)
                {
                    nums[x, 6] = 30 - (randMod.Next(11));
                }
                else if (x < 12)
                {
                    nums[x, 6] = 20 - (randMod.Next(11));
                }
                else if (x < 21)
                {
                    nums[x, 6] = 15 - (randMod.Next(11));
                }
                else if (x < 27 || x == 29)
                {
                    nums[x, 6] = 10 - (randMod.Next(11));
                }
                else
                {
                    nums[x, 6] = 5 - (randMod.Next(6));
                }

                //Transport
                if (x < 3)
                {
                    nums[x, 7] = 30 - (randMod.Next(11));
                }
                else if (x < 6)
                {
                    nums[x, 7] = 25 - (randMod.Next(11));
                }
                else if (x < 15)
                {
                    nums[x, 7] = 15 - (randMod.Next(11));
                }
                else if (x < 23)
                {
                    nums[x, 7] = 10 - (randMod.Next(11));
                }
                else
                {
                    nums[x, 7] = 5 - (randMod.Next(6));
                }

                //Day and Night
                nums[x, 8] = randMod.Next(2);

                //Points
                if (x < 3)
                {
                    nums[x, 9] = 400 - (randMod.Next(201));
                }
                else if (x < 6)
                {
                    nums[x, 9] = 700 - (randMod.Next(251));
                }
                else if (x < 9)
                {
                    nums[x, 9] = 1000 - (randMod.Next(301));
                }
                else if (x < 12)
                {
                    nums[x, 9] = 1200 - (randMod.Next(351));
                }
                else if (x < 15)
                {
                    nums[x, 9] = 1500 - (randMod.Next(401));
                }
                else if (x < 18)
                {
                    nums[x, 9] = 1800 - (randMod.Next(451));
                }
                else if (x < 21)
                {
                    nums[x, 9] = 2000 - (randMod.Next(501));
                }
                else if (x < 23)
                {
                    nums[x, 9] = 2200 - (randMod.Next(551));
                }
                else if (x < 25)
                {
                    nums[x, 9] = 2400 - (randMod.Next(601));
                }
                else if (x < 27)
                {
                    nums[x, 9] = 2500 - (randMod.Next(651));
                }
                else if (x < 29)
                {
                    nums[x, 9] = 2700 - (randMod.Next(701));
                }
                else
                {
                    nums[x, 9] = 3000 - (randMod.Next(751));
                }

                //Cash Reward
                if (x < 6)
                {
                    nums[x, 10] = 500 + (randMod.Next(251));
                }
                else if (x < 9)
                {
                    nums[x, 10] = 450 + (randMod.Next(226));
                }
                else if (x < 12)
                {
                    nums[x, 10] = 400 + (randMod.Next(201));
                }
                else if (x < 15)
                {
                    nums[x, 10] = 350 + (randMod.Next(176));
                }
                else if (x < 18)
                {
                    nums[x, 10] = 300 + (randMod.Next(151));
                }
                else if (x < 22)
                {
                    nums[x, 10] = 250 + (randMod.Next(126));
                }
                else if (x < 23)
                {
                    nums[x, 10] = 200 + (randMod.Next(101));
                }
                else if (x < 25)
                {
                    nums[x, 10] = 150 + (randMod.Next(75));
                }
                else
                {
                    nums[x, 10] = 100 + (randMod.Next(51));
                }
            }

            return nums;
        }

        private int[,] hardCareerNums()
        {
            int[,] nums = new int[30, 11];
            Random randMod = new Random();

            for (int x = 0; x < 30; x++)
            {
                //Difficulty
                if (x < 9 || x == 10)
                {
                    nums[x, 0] = 1;
                }
                else if (x == 9 || (x > 10 && x < 18))
                {
                    nums[x, 0] = 2;
                }
                else if (x < 25)
                {
                    nums[x, 0] = 3;
                }
                else
                {
                    nums[x, 0] = 3;
                }

                //Fire
                if (x < 6)
                {
                    nums[x, 1] = 10 + (randMod.Next(11));
                }
                else if (x < 9)
                {
                    nums[x, 1] = 15 + (randMod.Next(11));
                }
                else if (x < 15 || (x > 20 && x < 23) || x > 26)
                {
                    nums[x, 1] = 20 + (randMod.Next(11));
                }
                else
                {
                    nums[x, 1] = 25 + (randMod.Next(11));
                }

                //Crime
                if (x < 3)
                {
                    nums[x, 2] = 0 + (randMod.Next(11));
                }
                else if (x < 6)
                {
                    nums[x, 2] = 10 + (randMod.Next(11));
                }
                else if (x < 21 || x == 29)
                {
                    nums[x, 2] = 20 + (randMod.Next(11));
                }
                else
                {
                    nums[x, 2] = 25 + (randMod.Next(11));
                }

                //Rescue
                if (x < 3)
                {
                    nums[x, 3] = 0 + (randMod.Next(11));
                }
                else if (x < 12)
                {
                    nums[x, 3] = 10 + (randMod.Next(11));
                }
                else if (x < 15 || x == 29)
                {
                    nums[x, 3] = 15 + (randMod.Next(11));
                }
                else if (x < 23 || (x > 24 && x < 29))
                {
                    nums[x, 3] = 20 + (randMod.Next(11));
                }
                else
                {
                    nums[x, 3] = 25 + (randMod.Next(11));
                }

                //Riot
                if (x < 21)
                {
                    nums[x, 4] = 0 + (randMod.Next(11));
                }
                else if (x < 25)
                {
                    nums[x, 4] = 5 + (randMod.Next(11));
                }
                else if (x < 27)
                {
                    nums[x, 4] = 10 + (randMod.Next(11));
                }
                else
                {
                    nums[x, 4] = 20 + (randMod.Next(11));
                }

                //Traffic
                if (x < 6)
                {
                    nums[x, 5] = 30 + (randMod.Next(11));
                }
                else if (x < 9)
                {
                    nums[x, 5] = 20 + (randMod.Next(11));
                }
                else if (x < 15)
                {
                    nums[x, 5] = 15 + (randMod.Next(11));
                }
                else if (x < 23 || x == 29)
                {
                    nums[x, 5] = 10 + (randMod.Next(11));
                }
                else
                {
                    nums[x, 5] = 5 + (randMod.Next(11));
                }

                //MedEvac
                if (x < 3)
                {
                    nums[x, 6] = 30 + (randMod.Next(11));
                }
                else if (x < 12)
                {
                    nums[x, 6] = 20 + (randMod.Next(11));
                }
                else if (x < 21)
                {
                    nums[x, 6] = 15 + (randMod.Next(11));
                }
                else if (x < 27 || x == 29)
                {
                    nums[x, 6] = 10 + (randMod.Next(11));
                }
                else
                {
                    nums[x, 6] = 5 + (randMod.Next(11));
                }

                //Transport
                if (x < 3)
                {
                    nums[x, 7] = 30 + (randMod.Next(11));
                }
                else if (x < 6)
                {
                    nums[x, 7] = 25 + (randMod.Next(11));
                }
                else if (x < 15)
                {
                    nums[x, 7] = 15 + (randMod.Next(11));
                }
                else if (x < 23)
                {
                    nums[x, 7] = 10 + (randMod.Next(11));
                }
                else
                {
                    nums[x, 7] = 5 + (randMod.Next(11));
                }

                //Day and Night
                nums[x, 8] = randMod.Next(2);

                //Points
                if (x < 3)
                {
                    nums[x, 9] = 400 + (randMod.Next(201));
                }
                else if (x < 6)
                {
                    nums[x, 9] = 700 + (randMod.Next(251));
                }
                else if (x < 9)
                {
                    nums[x, 9] = 1000 + (randMod.Next(301));
                }
                else if (x < 12)
                {
                    nums[x, 9] = 1200 + (randMod.Next(351));
                }
                else if (x < 15)
                {
                    nums[x, 9] = 1500 + (randMod.Next(401));
                }
                else if (x < 18)
                {
                    nums[x, 9] = 1800 + (randMod.Next(451));
                }
                else if (x < 21)
                {
                    nums[x, 9] = 2000 + (randMod.Next(501));
                }
                else if (x < 23)
                {
                    nums[x, 9] = 2200 + (randMod.Next(551));
                }
                else if (x < 25)
                {
                    nums[x, 9] = 2400 + (randMod.Next(601));
                }
                else if (x < 27)
                {
                    nums[x, 9] = 2500 + (randMod.Next(651));
                }
                else if (x < 29)
                {
                    nums[x, 9] = 2700 + (randMod.Next(701));
                }
                else
                {
                    nums[x, 9] = 3000 + (randMod.Next(751));
                }

                //Cash Reward
                if (x < 6)
                {
                    nums[x, 10] = 500 - (randMod.Next(251));
                }
                else if (x < 9)
                {
                    nums[x, 10] = 450 - (randMod.Next(226));
                }
                else if (x < 12)
                {
                    nums[x, 10] = 400 - (randMod.Next(201));
                }
                else if (x < 15)
                {
                    nums[x, 10] = 350 - (randMod.Next(176));
                }
                else if (x < 18)
                {
                    nums[x, 10] = 300 - (randMod.Next(151));
                }
                else if (x < 22)
                {
                    nums[x, 10] = 250 - (randMod.Next(126));
                }
                else if (x < 23)
                {
                    nums[x, 10] = 200 - (randMod.Next(101));
                }
                else if (x < 25)
                {
                    nums[x, 10] = 150 - (randMod.Next(75));
                }
                else
                {
                    nums[x, 10] = 100 - (randMod.Next(51));
                }
            }

            return nums;
        }

        private int[,] balancedCareerNums()
        {
            int[,] nums = new int[30, 11];
            Random randMod = new Random();
            int posNeg;

            for (int x = 0; x < 30; x++)
            {
                //Difficulty
                if (x < 9 || x == 10)
                {
                    nums[x, 0] = 0;
                }
                else if (x == 9 || (x > 10 && x < 18))
                {
                    nums[x, 0] = 1;
                }
                else if (x < 25)
                {
                    nums[x, 0] = 2;
                }
                else
                {
                    nums[x, 0] = 3;
                }

                //Fire
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 6)
                {
                    nums[x, 1] = 10 + (randMod.Next(11) * posNeg);
                }
                else if (x < 9)
                {
                    nums[x, 1] = 15 + (randMod.Next(11) * posNeg);
                }
                else if (x < 15 || (x > 20 && x < 23) || x > 26)
                {
                    nums[x, 1] = 20 + (randMod.Next(11) * posNeg);
                }
                else
                {
                    nums[x, 1] = 25 + (randMod.Next(11) * posNeg);
                }

                //Crime
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 3)
                {
                    nums[x, 2] = 0 + (randMod.Next(11) * posNeg);
                }
                else if (x < 6)
                {
                    nums[x, 2] = 10 + (randMod.Next(11) * posNeg);
                }
                else if (x < 21 || x == 29)
                {
                    nums[x, 2] = 20 + (randMod.Next(11) * posNeg);
                }
                else
                {
                    nums[x, 2] = 25 + (randMod.Next(11) * posNeg);
                }

                //Rescue
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 3)
                {
                    nums[x, 3] = 0 + (randMod.Next(11) * posNeg);
                }
                else if (x < 12)
                {
                    nums[x, 3] = 10 + (randMod.Next(11) * posNeg);
                }
                else if (x < 15 || x == 29)
                {
                    nums[x, 3] = 15 + (randMod.Next(11) * posNeg);
                }
                else if (x < 23 || (x > 24 && x < 29))
                {
                    nums[x, 3] = 20 + (randMod.Next(11) * posNeg);
                }
                else
                {
                    nums[x, 3] = 25 + (randMod.Next(11) * posNeg);
                }

                //Riot
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 21)
                {
                    nums[x, 4] = 0 + (randMod.Next(11) * posNeg);
                }
                else if (x < 25)
                {
                    nums[x, 4] = 5 + (randMod.Next(11) * posNeg);
                }
                else if (x < 27)
                {
                    nums[x, 4] = 10 + (randMod.Next(11) * posNeg);
                }
                else
                {
                    nums[x, 4] = 20 + (randMod.Next(11) * posNeg);
                }

                //Traffic
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 6)
                {
                    nums[x, 5] = 30 + (randMod.Next(11) * posNeg);
                }
                else if (x < 9)
                {
                    nums[x, 5] = 20 + (randMod.Next(11) * posNeg);
                }
                else if (x < 15)
                {
                    nums[x, 5] = 15 + (randMod.Next(11) * posNeg);
                }
                else if (x < 23 || x == 29)
                {
                    nums[x, 5] = 10 + (randMod.Next(11) * posNeg);
                }
                else
                {
                    nums[x, 5] = 5 + (randMod.Next(11) * posNeg);
                }

                //MedEvac
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 3)
                {
                    nums[x, 6] = 30 + (randMod.Next(11) * posNeg);
                }
                else if (x < 12)
                {
                    nums[x, 6] = 20 + (randMod.Next(11) * posNeg);
                }
                else if (x < 21)
                {
                    nums[x, 6] = 15 + (randMod.Next(11) * posNeg);
                }
                else if (x < 27 || x == 29)
                {
                    nums[x, 6] = 10 + (randMod.Next(11) * posNeg);
                }
                else
                {
                    nums[x, 6] = 5 + (randMod.Next(11) * posNeg);
                }

                //Transport
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 3)
                {
                    nums[x, 7] = 30 + (randMod.Next(11) * posNeg);
                }
                else if (x < 6)
                {
                    nums[x, 7] = 25 + (randMod.Next(11) * posNeg);
                }
                else if (x < 15)
                {
                    nums[x, 7] = 15 + (randMod.Next(11) * posNeg);
                }
                else if (x < 23)
                {
                    nums[x, 7] = 10 + (randMod.Next(11) * posNeg);
                }
                else
                {
                    nums[x, 7] = 5 + (randMod.Next(11) * posNeg);
                }

                //Day and Night
                nums[x, 8] = randMod.Next(2);

                //Points
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 3)
                {
                    nums[x, 9] = 400 + (randMod.Next(201) * posNeg);
                }
                else if (x < 6)
                {
                    nums[x, 9] = 700 + (randMod.Next(251) * posNeg);
                }
                else if (x < 9)
                {
                    nums[x, 9] = 1000 + (randMod.Next(301) * posNeg);
                }
                else if (x < 12)
                {
                    nums[x, 9] = 1200 + (randMod.Next(351) * posNeg);
                }
                else if (x < 15)
                {
                    nums[x, 9] = 1500 + (randMod.Next(401) * posNeg);
                }
                else if (x < 18)
                {
                    nums[x, 9] = 1800 + (randMod.Next(451) * posNeg);
                }
                else if (x < 21)
                {
                    nums[x, 9] = 2000 + (randMod.Next(501) * posNeg);
                }
                else if (x < 23)
                {
                    nums[x, 9] = 2200 + (randMod.Next(551) * posNeg);
                }
                else if (x < 25)
                {
                    nums[x, 9] = 2400 + (randMod.Next(601) * posNeg);
                }
                else if (x < 27)
                {
                    nums[x, 9] = 2500 + (randMod.Next(651) * posNeg);
                }
                else if (x < 29)
                {
                    nums[x, 9] = 2700 + (randMod.Next(701) * posNeg);
                }
                else
                {
                    nums[x, 9] = 3000 + (randMod.Next(751) * posNeg);
                }

                //Cash Reward
                if (randMod.Next(2) == 0)
                {
                    posNeg = -1;
                }
                else
                {
                    posNeg = 1;
                }

                if (x < 6)
                {
                    nums[x, 10] = 500 + (randMod.Next(251) * posNeg);
                }
                else if (x < 9)
                {
                    nums[x, 10] = 450 + (randMod.Next(226) * posNeg);
                }
                else if (x < 12)
                {
                    nums[x, 10] = 400 + (randMod.Next(201) * posNeg);
                }
                else if (x < 15)
                {
                    nums[x, 10] = 350 + (randMod.Next(176) * posNeg);
                }
                else if (x < 18)
                {
                    nums[x, 10] = 300 + (randMod.Next(151) * posNeg);
                }
                else if (x < 22)
                {
                    nums[x, 10] = 250 + (randMod.Next(126) * posNeg);
                }
                else if (x < 23)
                {
                    nums[x, 10] = 200 + (randMod.Next(101) * posNeg);
                }
                else if (x < 25)
                {
                    nums[x, 10] = 150 + (randMod.Next(75) * posNeg);
                }
                else
                {
                    nums[x, 10] = 100 + (randMod.Next(51) * posNeg);
                }
            }
            //Range Check
            for (int x = 1; x < 30; x++)
            {
                for (int y = 1; y < 8; y++)
                {
                    if (nums[x, y] < 0)
                    {
                        nums[x, y] = 0;
                    }
                    else if (nums[x, y] > 99)
                    {
                        nums[x, y] = 99;
                    }
                }
                if (nums[x, 9] < 1)
                {
                    nums[x, 9] = 1;
                }
                else if (nums[x, 9] > 5000)
                {
                    nums[x, 9] = 5000;
                }
                if (nums[x, 10] < 1)
                {
                    nums[x, 10] = 1;
                }
                else if (nums[x, 10] > 2000)
                {
                    nums[x, 10] = 2000;
                }
            }
            return nums;
        }

        private int[,] chaosCareerNums()
        {
            int[,] nums = new int[30, 11];
            Random randMod = new Random();

            for (int x = 0; x < 30; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    //Difficulty
                    if (y < 1)
                    {
                        nums[x, y] = randMod.Next(4);
                    }
                    //Missions
                    else if (y < 8)
                    {
                        nums[x, y] = randMod.Next(100);
                    }
                    //Day and Night
                    else if (y < 9)
                    {
                        nums[x, y] = randMod.Next(2);
                    }
                    //Points
                    else if (y < 10)
                    {
                        nums[x, y] = randMod.Next(1, 5001);
                    }
                    //Cash Reward
                    else
                    {
                        nums[x, y] = randMod.Next(1, 2001);
                    }
                }
            }
            return nums;

        }

        private void mssnButt_Click(object sender, EventArgs e)
        {
            double[,] mNums = new double[9, 20];
            string line = null;

            //Mission File
            if (mEasy.Checked)
            {
                mNums = easyMissionNums();
            }
            else if (mHard.Checked)
            {
                mNums = hardMissionNums();
            }
            else if (mBalance.Checked)
            {
                mNums = balancedMissionNums();
            }
            else if (mChaos.Checked)
            {
                mNums = chaosMissionNums();
            }

            if (File.Exists(Globals.exeFolder + "\\sim3d.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\sim3d.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        int x = -1;
                        int y = 0;

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                x++;
                                y = 0;
                            }
                            if (line.Contains("Value"))
                            {
                                if ((x == 0 && (y == 1 || y == 2)) || (x == 1 && y == 4) || (x == 8 && y == 2))
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + mNums[x, y].ToString("0.0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + mNums[x, y].ToString("0");
                                }
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\sim3d.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\sim3d.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                System.Windows.Forms.MessageBox.Show("Mission Settings Randomized.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }

        }

        private double[,] easyMissionNums()
        {
            double[,] nums = new double[9, 20];
            Random randMod = new Random();
            return nums;
        }

        private double[,] hardMissionNums()
        {
            double[,] nums = new double[9, 20];
            Random randMod = new Random();
            return nums;
        }

        private double[,] balancedMissionNums()
        {
            double[,] nums = new double[9, 20];
            Random randMod = new Random();
            return nums;
        }

        private double[,] chaosMissionNums()
        {
            double[,] nums = new double[9, 20];
            Random randMod = new Random();

            //General Mission
            nums[0, 0] = randMod.Next(11);
            nums[0, 1] = randMod.Next(100, 1000);
            nums[0, 1] = nums[0, 1] + randMod.NextDouble();
            nums[0, 2] = randMod.Next(2);
            nums[0, 2] = nums[0, 2] + randMod.NextDouble();
            nums[0, 3] = randMod.Next(2001);
            nums[0, 4] = randMod.Next(2001);

            //Riot
            nums[1, 0] = randMod.Next(2001);
            nums[1, 1] = randMod.Next(2001);
            nums[1, 2] = randMod.Next(1001);
            nums[1, 3] = randMod.Next(1001);
            nums[1, 4] = randMod.Next(1000);
            nums[1, 4] = nums[1, 4] + randMod.NextDouble();

            //Rescue
            for (int x = 0; x < 3; x++)
            {
                nums[2, x] = randMod.Next(2001);
            }

            //Transport
            nums[3, 0] = randMod.Next(201);
            nums[3, 1] = randMod.Next(201);
            nums[3, 2] = randMod.Next(101);
            nums[3, 3] = randMod.Next(101);

            //MedEvac
            nums[4, 0] = randMod.Next(2001);
            nums[4, 1] = randMod.Next(2001);
            nums[4, 2] = randMod.Next(101);

            //Fire
            for (int x = 0; x < 20; x++)
            {
                if (x < 13)
                {
                    nums[5, x] = randMod.Next(2001);
                }
                else if (x < 16)
                {
                    nums[5, x] = randMod.Next(201);
                }
                else
                {
                    nums[5, x] = randMod.Next(101);
                }
            }

            //Criminal
            nums[6, 0] = randMod.Next(2001);
            nums[6, 1] = randMod.Next(2001);

            //Speeder
            nums[7, 0] = randMod.Next(2001);
            nums[7, 1] = randMod.Next(2001);
            nums[7, 2] = randMod.Next(21);

            //Traffic
            nums[8, 0] = randMod.Next(1001);
            nums[8, 1] = randMod.Next(501);
            nums[8, 2] = randMod.Next(500);
            nums[8, 2] = nums[8, 2] + randMod.NextDouble();

            return nums;
        }

        private void heliButt_Click(object sender, EventArgs e)
        {
            double[,] hNums = new double[12, 16];
            string line = null;
            int x = -1;
            int y = 0;

            //Heli File
            if (hChaos.Checked)
            {
                hNums = chaosHeliNums();
            }

            if (File.Exists(Globals.exeFolder + "\\heli.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\heli.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                x++;
                                y = 0;
                            }
                            if (line.Contains("Value"))
                            {
                                if ((x < 9 && (y == 8 || y == 11 || y == 12)) || (x == 10 && (y == 0 || y == 1 || y == 2 || y == 4) || (x == 11 && y == 3)))
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + hNums[x, y].ToString("0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + hNums[x, y].ToString("0.0");
                                }
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\heli.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\heli.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                System.Windows.Forms.MessageBox.Show("Helicopter Settings Randomized.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }
        }

        private double[,] fairHeliNums()
        {
            double[,] nums = new double[12, 16];

            return nums;
        }

        private double[,] chaosHeliNums()
        {
            double[,] nums = new double[12, 16];
            Random randMod = new Random();

            //Helicopters
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (y < 7)
                    {
                        nums[x, y] = randMod.Next(10, 800);
                        nums[x, y] = nums[x, y] + randMod.NextDouble();
                    }
                    else if (y == 7)
                    {
                        nums[x, y] = randMod.Next(50);
                        nums[x, y] = nums[x, y] + randMod.NextDouble();
                    }
                    else if (y == 8)
                    {
                        nums[x, y] = randMod.Next(10, 20001);
                    }
                    else if (y == 9)
                    {
                        nums[x, y] = randMod.Next(10, 900);
                        nums[x, y] = nums[x, y] + randMod.NextDouble();
                    }
                    else if (y == 10)
                    {
                        nums[x, y] = randMod.Next(3000);
                        nums[x, y] = nums[x, y] + randMod.NextDouble();
                    }
                    else if (y == 11)
                    {
                        nums[x, y] = randMod.Next(100001);
                    }
                    else if (y == 12)
                    {
                        nums[x, y] = randMod.Next(1, 5001);
                    }
                    else if (y == 13)
                    {
                        nums[x, y] = randMod.Next(100);
                        nums[x, y] = nums[x, y] + randMod.NextDouble();
                    }
                    else
                    {
                        nums[x, y] = randMod.Next(10);
                        nums[x, y] = nums[x, y] + randMod.NextDouble();
                    }
                }
            }
            for (int y = 0; y < 5; y++)
            {
                if (y < 2)
                {
                    nums[9, y] = randMod.Next(10, 800);
                    nums[9, y] = nums[9, y] + randMod.NextDouble();
                }
                else if (y < 4)
                {
                    nums[9, y] = randMod.Next(200);
                    nums[9, y] = nums[9, y] + randMod.NextDouble();
                }
                else
                {
                    nums[9, y] = randMod.Next(40);
                    nums[9, y] = nums[9, y] + randMod.NextDouble();
                }
            }
            for (int x = 10; x < 12; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    if (x == 10)
                    {
                        if (y < 2)
                        {
                            nums[x, y] = randMod.Next(2001);
                        }
                        else if (y == 2)
                        {
                            nums[x, y] = randMod.Next(8, 1001);
                        }
                        else if (y == 3)
                        {
                            nums[x, y] = randMod.NextDouble();
                        }
                        else if (y == 4)
                        {
                            nums[x, y] = randMod.Next(1, 65);
                        }
                        else
                        {
                            nums[x, y] = randMod.Next(10, 500);
                            nums[x, y] = nums[x, y] + randMod.NextDouble();
                        }
                    }
                    else
                    {
                        if (y == 0)
                        {
                            nums[x, y] = randMod.Next(-100, 100);
                            nums[x, y] = nums[x, y] + randMod.NextDouble();
                        }
                        else if (y == 1)
                        {
                            nums[x, y] = randMod.Next(200);
                            nums[x, y] = nums[x, y] + randMod.NextDouble();
                        }
                        else if (y == 2)
                        {
                            nums[x, y] = randMod.Next(5);
                            nums[x, y] = nums[x, y] + randMod.NextDouble();
                        }
                        else if (y == 3)
                        {
                            nums[x, y] = randMod.Next(501);
                        }
                        else
                        {
                            nums[x, y] = randMod.Next(100);
                            nums[x, y] = nums[x, y] + randMod.NextDouble();
                        }
                    }
                }
            }
            return nums;
        }

        private void fireButt_Click(object sender, EventArgs e)
        {
            double[] fNums = new double[6];
            string line = null;
            int x = -1;
            int y = 0;

            if (fChaos.Checked)
            {
                fNums = fChaosNums();
            }

            if (File.Exists(Globals.exeFolder + "\\fire.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\fire.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                y = 0;
                            }
                            if (line.Contains("Value"))
                            {
                                if (y == 1 || y == 4)
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + fNums[y].ToString("0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + fNums[y].ToString("0.0");
                                }
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\fire.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\fire.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                System.Windows.Forms.MessageBox.Show("Fire Settings Randomized.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }
        }

        private double[] fChaosNums()
        {
            double[] nums = new double[6];
            Random randMod = new Random();
            
            //Douse Points
            nums[0] = randMod.Next(10, 128);
            nums[0] = nums[0] + randMod.NextDouble();
            //Douse Mult
            nums[1] = randMod.Next(1, 101);
            //TimeToLive
            nums[2] = randMod.Next(80, 1000);
            nums[2] = nums[2] + randMod.NextDouble();
            //SpreadInterval
            nums[3] = randMod.Next(18, 100);
            nums[3] = nums[3] + randMod.NextDouble();
            //SpreadProb
            nums[4] = randMod.Next(40, 1025);
            //Fire Radius
            nums[5] = randMod.Next(30, 256);
            nums[5] = nums[5] + randMod.NextDouble();

            return nums;
        }

        private void amssnButt_Click(object sender, EventArgs e)
        {
            int[] aNums = new int[2];
            string line = null;
            int y = 0;

            if (amssnChaos.Checked)
            {
                aNums = chaosAMssnNums();
            }

            if (File.Exists(Globals.exeFolder + "\\automssn.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\automssn.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Value"))
                            {
                                line = "Ctrl" + y.ToString() + "_Value=" + aNums[y].ToString("0");
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\automssn.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\automssn.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                System.Windows.Forms.MessageBox.Show("Auto Mission Settings Randomized.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }
        }

        private int[] chaosAMssnNums()
        {
            int[] nums = new int[2];
            Random randMod = new Random();

            //Fire Scan
            nums[0] = randMod.Next(3, 11);
            //Road Scan
            nums[1] = randMod.Next(3, 11);

            return nums;
        }

        private void resetButt_Click(object sender, EventArgs e)
        {
            double[,] rNums = new double[,] { { 0, 10, 0, 0, 0, 30, 30, 30, 1, 400, 500 }, { 0, 10, 0, 0, 0, 30, 30, 30, 0, 400, 500 }, { 0, 10, 0, 0, 0, 30, 30, 30, 1, 400, 500 }, { 0, 10, 10, 5, 0, 30, 20, 25, 0, 700, 500 }, { 0, 10, 10, 5, 0, 30, 20, 25, 1, 700, 500 }, { 0, 10, 10, 5, 0, 30, 20, 25, 0, 700, 500 }, { 0, 15, 20, 10, 0, 20, 20, 15, 0, 1000, 450 }, { 0, 15, 20, 10, 0, 20, 20, 15, 0, 1000, 450 }, { 0, 15, 20, 10, 0, 20, 20, 15, 1, 1000, 450 }, { 1, 20, 20, 10, 0, 15, 20, 15, 1, 1200, 400 }, { 0, 20, 20, 10, 0, 15, 20, 15, 0, 1200, 400 }, { 1, 20, 20, 10, 0, 15, 20, 15, 1, 1200, 400 }, { 1, 20, 20, 15, 0, 15, 15, 15, 0, 1500, 350 }, { 1, 20, 20, 15, 0, 15, 15, 15, 1, 1500, 350 }, { 1, 20, 20, 15, 0, 15, 15, 15, 0, 1500, 350 }, { 1, 25, 20, 20, 0, 10, 15, 10, 0, 1800, 300 }, { 1, 25, 20, 20, 0, 10, 15, 10, 1, 1800, 300 }, { 1, 25, 20, 20, 0, 10, 15, 10, 1, 1800, 300 }, { 2, 25, 20, 20, 0, 10, 15, 10, 1, 2000, 250 }, { 2, 25, 20, 20, 0, 10, 15, 10, 0, 2000, 250 }, { 2, 25, 20, 20, 0, 10, 15, 10, 0, 2000, 250 }, { 2, 20, 25, 20, 5, 10, 10, 10, 1, 2200, 200 }, { 2, 20, 25, 20, 5, 10, 10, 10, 0, 2200, 200 }, { 2, 25, 25, 25, 5, 5, 10, 5, 0, 2400, 150 }, { 2, 25, 25, 25, 5, 5, 10, 5, 1, 2400, 150 }, { 3, 25, 25, 20, 10, 5, 10, 5, 1, 2500, 100 }, { 3, 25, 25, 20, 10, 5, 10, 5, 0, 2500, 100 }, { 3, 20, 25, 20, 20, 5, 5, 5, 0, 2700, 100 }, { 3, 20, 25, 20, 20, 5, 5, 5, 1, 2700, 100 }, { 3, 20, 20, 15, 20, 10, 10, 5, 1, 3000, 100 } };
            int x = 0;
            int y = 0;
            int z = 0;
            string line = null;

            //career.twk
            if (File.Exists(Globals.exeFolder + "\\career.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\career.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                y = 0;
                            }
                            if (line.Contains("Ctrl10_DataType"))
                            {
                                x++;
                            }
                            if (line.Contains("Value"))
                            {
                                line = "Ctrl" + y.ToString("") + "_Value=" + rNums[x, y].ToString();
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\career.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\career.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                z++;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }

            //sim3d.twk
            rNums = new double[,] { { 2, 380.0, 0.2, 2000, 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 725, 505, 0, 250, 197.8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 200, 100, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 100, 50, 20, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 200, 100, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 150, 100, 0, 50, 200, 100, 100, 100, 100, 50, 50, 50, 20, 50, 200, 150, 50, 20, 30, 20 }, { 500, 300, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 200, 100, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 100, 50, 60.0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            x = -1;
            y = 0;

            if (File.Exists(Globals.exeFolder + "\\sim3d.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\sim3d.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                x++;
                                y = 0;
                            }
                            if (line.Contains("Value"))
                            {
                                if ((x == 0 && (y == 1 || y == 2)) || (x == 1 && y == 4) || (x == 8 && y == 2))
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + rNums[x, y].ToString("0.0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + rNums[x, y].ToString("0");
                                }
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\sim3d.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\sim3d.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                z++;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }

            //heli.twk
            rNums = new double[,] { { 426.7, 140.2, 192.3, 452.7, 105.5, 209.7, 209.7, 7.1, 1548, 58.9, 230.8, 7800, 604, 91.2, 1.0, 3.0 }, { 452.7, 140.2, 201.0, 496.2, 114.2, 227.0, 227.0, 8.2, 1987, 68.7, 197.8, 8800, 659, 65.9, 1.0, 3.0 }, { 279.1, 96.8, 183.6, 183.6, 79.5, 79.5, 96.8, 6.6, 5062, 39.3, 813.2, 13500, 1000, 214.3, 1.0, 3.0 }, { 418.0, 122.9, 131.5, 470.1, 122.9, 235.7, 235.7, 4.4, 1062, 58.9, 87.9, 3400, 495, 38.5, 1.0, 3.0 }, { 556.9, 235.7, 296.5, 357.3, 122.9, 183.6, 183.6, 15.3, 1904, 98.0, 571.4, 50000, 1000, 148.4, 1.0, 3.0 }, { 522.2, 140.2, 253.1, 322.5, 122.9, 157.6, 174.9, 10.4, 2207, 58.9, 571.4, 16500, 714, 148.4, 1.0, 3.0 }, { 348.6, 122.9, 253.1, 270.4, 96.8, 114.2, 131.5, 8.2, 3525, 49.1, 835.2, 18500, 769, 302.2, 1.0, 3.0 }, { 339.9, 140.2, 244.4, 339.9, 96.8, 148.9, 183.6, 13.7, 3085, 68.7, 439.6, 20500, 769, 159.3, 1.0, 3.0 }, { 452.7, 192.3, 218.4, 435.4, 96.8, 305.2, 227.0, 9.3, 2426, 78.5, 285.7, 11000, 659, 65.9, 1.0, 3.0 }, { 51.6, 43.3, 42.1, 20.1, 19.4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 30, 21, 102, 0.5, 49, 128.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { -48.0, 61.1, 0.5, 27, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            x = -1;
            y = 0;

            if (File.Exists(Globals.exeFolder + "\\heli.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\heli.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                x++;
                                y = 0;
                            }
                            if (line.Contains("Value"))
                            {
                                if ((x < 9 && (y == 8 || y == 11 || y == 12)) || (x == 10 && (y == 0 || y == 1 || y == 2 || y == 4) || (x == 11 && y == 3)))
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + rNums[x, y].ToString("0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + rNums[x, y].ToString("0.0");
                                }
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\heli.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\heli.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                z++;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }

            //Fire.twk
            rNums = new double[,] { { 37.3, 21, 190.3, 34.7, 224, 43.9 } };
            y = 0;

            if (File.Exists(Globals.exeFolder + "\\fire.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\fire.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                x++;
                                y = 0;
                            }
                            if (line.Contains("Value"))
                            {
                                if (y == 1 || y == 4)
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + rNums[0, y].ToString("0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + rNums[0, y].ToString("0.0");
                                }
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\fire.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\fire.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                z++;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }

            //automssn.twk
            rNums = new double[,] { { 5, 5 } };

            if (File.Exists(Globals.exeFolder + "\\automssn.twk"))
            {
                using (StreamReader sr = new StreamReader(Globals.exeFolder + "\\automssn.twk"))
                {
                    using (StreamWriter sw = new StreamWriter(Globals.exeFolder + "\\tempFile.twk"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("Ctrl0_Label"))
                            {
                                x++;
                                y = 0;
                            }
                            if (line.Contains("Value"))
                            {
                                line = "Ctrl" + y.ToString() + "_Value=" + rNums[0, y].ToString("0");
                                y++;
                            }
                            sw.WriteLine(line);
                        }
                        sw.Close();
                    }
                    sr.Close();
                }
                File.Delete(Globals.exeFolder + "\\automssn.twk");
                File.Copy(Globals.exeFolder + "\\tempFile.twk", Globals.exeFolder + "\\automssn.twk");
                File.Delete(Globals.exeFolder + "\\tempFile.twk");
                z++;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            }
            if (z == 5)
            {
                System.Windows.Forms.MessageBox.Show("Files reset.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("One or more files missing.");
            }
        }
    }
}
