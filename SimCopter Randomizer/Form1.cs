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
            public static string mapFolder = System.IO.Path.Combine(exeFolder, "..", "cities", "career");
            public static string smkFolder = System.IO.Path.Combine(exeFolder, "..", "smk");
            public static bool dlComplete = false;
            public static Random randMod = new Random();
        }

        private void randButt_Click(object sender, EventArgs e)
        {

            if (seedBox.Text.Contains(""))
            {
                Globals.randMod = new Random();
            }
            else
            {
                Globals.randMod = new Random(seedBox.Text.GetHashCode());
            }

            carrRand();
            mssnRand();
            heliRand();
            fireRand();
            amssnRand();
        }

        private void carrRand()
        {
            //string exeFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            int?[,] cNums = new int?[30, 11];
            string line = null;
            int x = 0;
            int y = 0;

            //Campaign File
            if (cChaos.Checked)
            {
                cNums = chaosCareerNums();
            }

            if (cCustom.Checked)
            {
                cNums = customizedCareerNums();
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
                            if (line.Contains("Value") && cNums[x, y] != null)
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

        private int?[,] chaosCareerNums()
        {
            int?[,] nums = new int?[30, 11];

            for (int x = 0; x < 30; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    //Difficulty
                    if (y < 1)
                    {
                        nums[x, y] = Globals.randMod.Next(4);
                    }
                    //Missions
                    else if (y < 8)
                    {
                        nums[x, y] = Globals.randMod.Next(100);
                    }
                    //Day and Night
                    else if (y < 9)
                    {
                        nums[x, y] = Globals.randMod.Next(2);
                    }
                    //Points
                    else if (y < 10)
                    {
                        nums[x, y] = Globals.randMod.Next(1, 5001);
                    }
                    //Cash Reward
                    else
                    {
                        nums[x, y] = Globals.randMod.Next(1, 2001);
                    }
                }
                //If none of the missions will spawn.
                while (nums[x, 1] == 0 && nums[x, 2] == 0 && nums[x, 3] == 0 && nums[x, 4] == 0 && nums[x, 5] == 0 && nums[x, 6] == 0 && nums[x, 7] == 0)
                {
                    for (int z = 1; z < 8; z++)
                    {
                        nums[x, z] = Globals.randMod.Next(100);
                    }
                }
            }
            return nums;

        }

        private int?[,] customizedCareerNums()
        {
            int?[,] nums = new int?[30, 11];

            //City 0
            if (c0DiffCheck.Checked)
            {
                nums[0, 0] = Globals.randMod.Next(Convert.ToInt32(c0DiffMin.Value), Convert.ToInt32(c0DiffMax.Value) + 1);
            }
            else
            {
                nums[0, 0] = null;
            }

            if (c0FireCheck.Checked)
            {
                nums[0, 1] = Globals.randMod.Next(Convert.ToInt32(c0FireMin.Value), Convert.ToInt32(c0FireMax.Value) + 1);
            }
            else
            {
                nums[0, 1] = null;
            }

            if (c0CrimeCheck.Checked)
            {
                nums[0, 2] = Globals.randMod.Next(Convert.ToInt32(c0CrimeMin.Value), Convert.ToInt32(c0CrimeMax.Value) + 1);
            }
            else
            {
                nums[0, 2] = null;
            }

            if (c0RescueCheck.Checked)
            {
                nums[0, 3] = Globals.randMod.Next(Convert.ToInt32(c0RescueMin.Value), Convert.ToInt32(c0RescueMax.Value) + 1);
            }
            else
            {
                nums[0, 3] = null;
            }

            if (c0RiotCheck.Checked)
            {
                nums[0, 4] = Globals.randMod.Next(Convert.ToInt32(c0RiotMin.Value), Convert.ToInt32(c0RiotMax.Value) + 1);
            }
            else
            {
                nums[0, 4] = null;
            }

            if (c0TrafficCheck.Checked)
            {
                nums[0, 5] = Globals.randMod.Next(Convert.ToInt32(c0TrafficMin.Value), Convert.ToInt32(c0TrafficMax.Value) + 1);
            }
            else
            {
                nums[0, 5] = null;
            }

            if (c0MedevacCheck.Checked)
            {
                nums[0, 6] = Globals.randMod.Next(Convert.ToInt32(c0MedevacMin.Value), Convert.ToInt32(c0MedevacMax.Value) + 1);
            }
            else
            {
                nums[0, 6] = null;
            }

            if (c0TransportCheck.Checked)
            {
                nums[0, 7] = Globals.randMod.Next(Convert.ToInt32(c0TransportMin.Value), Convert.ToInt32(c0TransportMax.Value) + 1);
            }
            else
            {
                nums[0, 7] = null;
            }

            if (c0DayCheck.Checked)
            {
                nums[0, 8] = Globals.randMod.Next(Convert.ToInt32(c0DayMin.Value), Convert.ToInt32(c0DayMax.Value) + 1);
            }
            else
            {
                nums[0, 8] = null;
            }

            if (c0PointCheck.Checked)
            {
                nums[0, 9] = Globals.randMod.Next(Convert.ToInt32(c0PointsMin.Value), Convert.ToInt32(c0PointsMax.Value) + 1);
            }
            else
            {
                nums[0, 9] = null;
            }

            if (c0MoneyCheck.Checked)
            {
                nums[0, 10] = Globals.randMod.Next(Convert.ToInt32(c0MoneyMin.Value), Convert.ToInt32(c0MoneyMax.Value) + 1);
            }
            else
            {
                nums[0, 10] = null;
            }

            //City 1
            if (c1DiffCheck.Checked)
            {
                nums[1, 0] = Globals.randMod.Next(Convert.ToInt32(c1DiffMin.Value), Convert.ToInt32(c1DiffMax.Value) + 1);
            }
            else
            {
                nums[1, 0] = null;
            }

            if (c1FireCheck.Checked)
            {
                nums[1, 1] = Globals.randMod.Next(Convert.ToInt32(c1FireMin.Value), Convert.ToInt32(c1FireMax.Value) + 1);
            }
            else
            {
                nums[1, 1] = null;
            }

            if (c1CrimeCheck.Checked)
            {
                nums[1, 2] = Globals.randMod.Next(Convert.ToInt32(c1CrimeMin.Value), Convert.ToInt32(c1CrimeMax.Value) + 1);
            }
            else
            {
                nums[1, 2] = null;
            }

            if (c1RescueCheck.Checked)
            {
                nums[1, 3] = Globals.randMod.Next(Convert.ToInt32(c1RescueMin.Value), Convert.ToInt32(c1RescueMax.Value) + 1);
            }
            else
            {
                nums[1, 3] = null;
            }

            if (c1RiotCheck.Checked)
            {
                nums[1, 4] = Globals.randMod.Next(Convert.ToInt32(c1RiotMin.Value), Convert.ToInt32(c1RiotMax.Value) + 1);
            }
            else
            {
                nums[1, 4] = null;
            }

            if (c1TrafficCheck.Checked)
            {
                nums[1, 5] = Globals.randMod.Next(Convert.ToInt32(c1TrafficMin.Value), Convert.ToInt32(c1TrafficMax.Value) + 1);
            }
            else
            {
                nums[1, 5] = null;
            }

            if (c1MedevacCheck.Checked)
            {
                nums[1, 6] = Globals.randMod.Next(Convert.ToInt32(c1MedevacMin.Value), Convert.ToInt32(c1MedevacMax.Value) + 1);
            }
            else
            {
                nums[1, 6] = null;
            }

            if (c1TransportCheck.Checked)
            {
                nums[1, 7] = Globals.randMod.Next(Convert.ToInt32(c1TransportMin.Value), Convert.ToInt32(c1TransportMax.Value) + 1);
            }
            else
            {
                nums[1, 7] = null;
            }

            if (c1DayCheck.Checked)
            {
                nums[1, 8] = Globals.randMod.Next(Convert.ToInt32(c1DayMin.Value), Convert.ToInt32(c1DayMax.Value) + 1);
            }
            else
            {
                nums[1, 8] = null;
            }

            if (c1PointCheck.Checked)
            {
                nums[1, 9] = Globals.randMod.Next(Convert.ToInt32(c1PointsMin.Value), Convert.ToInt32(c1PointsMax.Value) + 1);
            }
            else
            {
                nums[1, 9] = null;
            }

            if (c1MoneyCheck.Checked)
            {
                nums[1, 10] = Globals.randMod.Next(Convert.ToInt32(c1MoneyMin.Value), Convert.ToInt32(c1MoneyMax.Value) + 1);
            }
            else
            {
                nums[1, 10] = null;
            }

            //City 2
            if (c2DiffCheck.Checked)
            {
                nums[2, 0] = Globals.randMod.Next(Convert.ToInt32(c2DiffMin.Value), Convert.ToInt32(c2DiffMax.Value) + 1);
            }
            else
            {
                nums[2, 0] = null;
            }

            if (c2FireCheck.Checked)
            {
                nums[2, 1] = Globals.randMod.Next(Convert.ToInt32(c2FireMin.Value), Convert.ToInt32(c2FireMax.Value) + 1);
            }
            else
            {
                nums[2, 1] = null;
            }

            if (c2CrimeCheck.Checked)
            {
                nums[2, 2] = Globals.randMod.Next(Convert.ToInt32(c2CrimeMin.Value), Convert.ToInt32(c2CrimeMax.Value) + 1);
            }
            else
            {
                nums[2, 2] = null;
            }

            if (c2RescueCheck.Checked)
            {
                nums[2, 3] = Globals.randMod.Next(Convert.ToInt32(c2RescueMin.Value), Convert.ToInt32(c2RescueMax.Value) + 1);
            }
            else
            {
                nums[2, 3] = null;
            }

            if (c2RiotCheck.Checked)
            {
                nums[2, 4] = Globals.randMod.Next(Convert.ToInt32(c2RiotMin.Value), Convert.ToInt32(c2RiotMax.Value) + 1);
            }
            else
            {
                nums[2, 4] = null;
            }

            if (c2TrafficCheck.Checked)
            {
                nums[2, 5] = Globals.randMod.Next(Convert.ToInt32(c2TrafficMin.Value), Convert.ToInt32(c2TrafficMax.Value) + 1);
            }
            else
            {
                nums[2, 5] = null;
            }

            if (c2MedevacCheck.Checked)
            {
                nums[2, 6] = Globals.randMod.Next(Convert.ToInt32(c2MedevacMin.Value), Convert.ToInt32(c2MedevacMax.Value) + 1);
            }
            else
            {
                nums[2, 6] = null;
            }

            if (c2TransportCheck.Checked)
            {
                nums[2, 7] = Globals.randMod.Next(Convert.ToInt32(c2TransportMin.Value), Convert.ToInt32(c2TransportMax.Value) + 1);
            }
            else
            {
                nums[2, 7] = null;
            }

            if (c2DayCheck.Checked)
            {
                nums[2, 8] = Globals.randMod.Next(Convert.ToInt32(c2DayMin.Value), Convert.ToInt32(c2DayMax.Value) + 1);
            }
            else
            {
                nums[2, 8] = null;
            }

            if (c2PointCheck.Checked)
            {
                nums[2, 9] = Globals.randMod.Next(Convert.ToInt32(c2PointsMin.Value), Convert.ToInt32(c2PointsMax.Value) + 1);
            }
            else
            {
                nums[2, 9] = null;
            }

            if (c2MoneyCheck.Checked)
            {
                nums[2, 10] = Globals.randMod.Next(Convert.ToInt32(c2MoneyMin.Value), Convert.ToInt32(c2MoneyMax.Value) + 1);
            }
            else
            {
                nums[2, 10] = null;
            }

            //City 3
            if (c3DiffCheck.Checked)
            {
                nums[3, 0] = Globals.randMod.Next(Convert.ToInt32(c3DiffMin.Value), Convert.ToInt32(c3DiffMax.Value) + 1);
            }
            else
            {
                nums[3, 0] = null;
            }

            if (c3FireCheck.Checked)
            {
                nums[3, 1] = Globals.randMod.Next(Convert.ToInt32(c3FireMin.Value), Convert.ToInt32(c3FireMax.Value) + 1);
            }
            else
            {
                nums[3, 1] = null;
            }

            if (c3CrimeCheck.Checked)
            {
                nums[3, 2] = Globals.randMod.Next(Convert.ToInt32(c3CrimeMin.Value), Convert.ToInt32(c3CrimeMax.Value) + 1);
            }
            else
            {
                nums[3, 2] = null;
            }

            if (c3RescueCheck.Checked)
            {
                nums[3, 3] = Globals.randMod.Next(Convert.ToInt32(c3RescueMin.Value), Convert.ToInt32(c3RescueMax.Value) + 1);
            }
            else
            {
                nums[3, 3] = null;
            }

            if (c3RiotCheck.Checked)
            {
                nums[3, 4] = Globals.randMod.Next(Convert.ToInt32(c3RiotMin.Value), Convert.ToInt32(c3RiotMax.Value) + 1);
            }
            else
            {
                nums[3, 4] = null;
            }

            if (c3TrafficCheck.Checked)
            {
                nums[3, 5] = Globals.randMod.Next(Convert.ToInt32(c3TrafficMin.Value), Convert.ToInt32(c3TrafficMax.Value) + 1);
            }
            else
            {
                nums[3, 5] = null;
            }

            if (c3MedevacCheck.Checked)
            {
                nums[3, 6] = Globals.randMod.Next(Convert.ToInt32(c3MedevacMin.Value), Convert.ToInt32(c3MedevacMax.Value) + 1);
            }
            else
            {
                nums[3, 6] = null;
            }

            if (c3TransportCheck.Checked)
            {
                nums[3, 7] = Globals.randMod.Next(Convert.ToInt32(c3TransportMin.Value), Convert.ToInt32(c3TransportMax.Value) + 1);
            }
            else
            {
                nums[3, 7] = null;
            }

            if (c3DayCheck.Checked)
            {
                nums[3, 8] = Globals.randMod.Next(Convert.ToInt32(c3DayMin.Value), Convert.ToInt32(c3DayMax.Value) + 1);
            }
            else
            {
                nums[3, 8] = null;
            }

            if (c3PointCheck.Checked)
            {
                nums[3, 9] = Globals.randMod.Next(Convert.ToInt32(c3PointsMin.Value), Convert.ToInt32(c3PointsMax.Value) + 1);
            }
            else
            {
                nums[3, 9] = null;
            }

            if (c3MoneyCheck.Checked)
            {
                nums[3, 10] = Globals.randMod.Next(Convert.ToInt32(c3MoneyMin.Value), Convert.ToInt32(c3MoneyMax.Value) + 1);
            }
            else
            {
                nums[3, 10] = null;
            }

            //City 4
            if (c4DiffCheck.Checked)
            {
                nums[4, 0] = Globals.randMod.Next(Convert.ToInt32(c4DiffMin.Value), Convert.ToInt32(c4DiffMax.Value) + 1);
            }
            else
            {
                nums[4, 0] = null;
            }

            if (c4FireCheck.Checked)
            {
                nums[4, 1] = Globals.randMod.Next(Convert.ToInt32(c4FireMin.Value), Convert.ToInt32(c4FireMax.Value) + 1);
            }
            else
            {
                nums[4, 1] = null;
            }

            if (c4CrimeCheck.Checked)
            {
                nums[4, 2] = Globals.randMod.Next(Convert.ToInt32(c4CrimeMin.Value), Convert.ToInt32(c4CrimeMax.Value) + 1);
            }
            else
            {
                nums[4, 2] = null;
            }

            if (c4RescueCheck.Checked)
            {
                nums[4, 3] = Globals.randMod.Next(Convert.ToInt32(c4RescueMin.Value), Convert.ToInt32(c4RescueMax.Value) + 1);
            }
            else
            {
                nums[4, 3] = null;
            }

            if (c4RiotCheck.Checked)
            {
                nums[4, 4] = Globals.randMod.Next(Convert.ToInt32(c4RiotMin.Value), Convert.ToInt32(c4RiotMax.Value) + 1);
            }
            else
            {
                nums[4, 4] = null;
            }

            if (c4TrafficCheck.Checked)
            {
                nums[4, 5] = Globals.randMod.Next(Convert.ToInt32(c4TrafficMin.Value), Convert.ToInt32(c4TrafficMax.Value) + 1);
            }
            else
            {
                nums[4, 5] = null;
            }

            if (c4MedevacCheck.Checked)
            {
                nums[4, 6] = Globals.randMod.Next(Convert.ToInt32(c4MedevacMin.Value), Convert.ToInt32(c4MedevacMax.Value) + 1);
            }
            else
            {
                nums[4, 6] = null;
            }

            if (c4TransportCheck.Checked)
            {
                nums[4, 7] = Globals.randMod.Next(Convert.ToInt32(c4TransportMin.Value), Convert.ToInt32(c4TransportMax.Value) + 1);
            }
            else
            {
                nums[4, 7] = null;
            }

            if (c4DayCheck.Checked)
            {
                nums[4, 8] = Globals.randMod.Next(Convert.ToInt32(c4DayMin.Value), Convert.ToInt32(c4DayMax.Value) + 1);
            }
            else
            {
                nums[4, 8] = null;
            }

            if (c4PointCheck.Checked)
            {
                nums[4, 9] = Globals.randMod.Next(Convert.ToInt32(c4PointsMin.Value), Convert.ToInt32(c4PointsMax.Value) + 1);
            }
            else
            {
                nums[4, 9] = null;
            }

            if (c4MoneyCheck.Checked)
            {
                nums[4, 10] = Globals.randMod.Next(Convert.ToInt32(c4MoneyMin.Value), Convert.ToInt32(c4MoneyMax.Value) + 1);
            }
            else
            {
                nums[4, 10] = null;
            }

            //City 5
            if (c5DiffCheck.Checked)
            {
                nums[5, 0] = Globals.randMod.Next(Convert.ToInt32(c5DiffMin.Value), Convert.ToInt32(c5DiffMax.Value) + 1);
            }
            else
            {
                nums[5, 0] = null;
            }

            if (c5FireCheck.Checked)
            {
                nums[5, 1] = Globals.randMod.Next(Convert.ToInt32(c5FireMin.Value), Convert.ToInt32(c5FireMax.Value) + 1);
            }
            else
            {
                nums[5, 1] = null;
            }

            if (c5CrimeCheck.Checked)
            {
                nums[5, 2] = Globals.randMod.Next(Convert.ToInt32(c5CrimeMin.Value), Convert.ToInt32(c5CrimeMax.Value) + 1);
            }
            else
            {
                nums[5, 2] = null;
            }

            if (c5RescueCheck.Checked)
            {
                nums[5, 3] = Globals.randMod.Next(Convert.ToInt32(c5RescueMin.Value), Convert.ToInt32(c5RescueMax.Value) + 1);
            }
            else
            {
                nums[5, 3] = null;
            }

            if (c5RiotCheck.Checked)
            {
                nums[5, 4] = Globals.randMod.Next(Convert.ToInt32(c5RiotMin.Value), Convert.ToInt32(c5RiotMax.Value) + 1);
            }
            else
            {
                nums[5, 4] = null;
            }

            if (c5TrafficCheck.Checked)
            {
                nums[5, 5] = Globals.randMod.Next(Convert.ToInt32(c5TrafficMin.Value), Convert.ToInt32(c5TrafficMax.Value) + 1);
            }
            else
            {
                nums[5, 5] = null;
            }

            if (c5MedevacCheck.Checked)
            {
                nums[5, 6] = Globals.randMod.Next(Convert.ToInt32(c5MedevacMin.Value), Convert.ToInt32(c5MedevacMax.Value) + 1);
            }
            else
            {
                nums[5, 6] = null;
            }

            if (c5TransportCheck.Checked)
            {
                nums[5, 7] = Globals.randMod.Next(Convert.ToInt32(c5TransportMin.Value), Convert.ToInt32(c5TransportMax.Value) + 1);
            }
            else
            {
                nums[5, 7] = null;
            }

            if (c5DayCheck.Checked)
            {
                nums[5, 8] = Globals.randMod.Next(Convert.ToInt32(c5DayMin.Value), Convert.ToInt32(c5DayMax.Value) + 1);
            }
            else
            {
                nums[5, 8] = null;
            }

            if (c5PointCheck.Checked)
            {
                nums[5, 9] = Globals.randMod.Next(Convert.ToInt32(c5PointsMin.Value), Convert.ToInt32(c5PointsMax.Value) + 1);
            }
            else
            {
                nums[5, 9] = null;
            }

            if (c5MoneyCheck.Checked)
            {
                nums[5, 10] = Globals.randMod.Next(Convert.ToInt32(c5MoneyMin.Value), Convert.ToInt32(c5MoneyMax.Value) + 1);
            }
            else
            {
                nums[5, 10] = null;
            }

            //City 6
            if (c6DiffCheck.Checked)
            {
                nums[6, 0] = Globals.randMod.Next(Convert.ToInt32(c6DiffMin.Value), Convert.ToInt32(c6DiffMax.Value) + 1);
            }
            else
            {
                nums[6, 0] = null;
            }

            if (c6FireCheck.Checked)
            {
                nums[6, 1] = Globals.randMod.Next(Convert.ToInt32(c6FireMin.Value), Convert.ToInt32(c6FireMax.Value) + 1);
            }
            else
            {
                nums[6, 1] = null;
            }

            if (c6CrimeCheck.Checked)
            {
                nums[6, 2] = Globals.randMod.Next(Convert.ToInt32(c6CrimeMin.Value), Convert.ToInt32(c6CrimeMax.Value) + 1);
            }
            else
            {
                nums[6, 2] = null;
            }

            if (c6RescueCheck.Checked)
            {
                nums[6, 3] = Globals.randMod.Next(Convert.ToInt32(c6RescueMin.Value), Convert.ToInt32(c6RescueMax.Value) + 1);
            }
            else
            {
                nums[6, 3] = null;
            }

            if (c6RiotCheck.Checked)
            {
                nums[6, 4] = Globals.randMod.Next(Convert.ToInt32(c6RiotMin.Value), Convert.ToInt32(c6RiotMax.Value) + 1);
            }
            else
            {
                nums[6, 4] = null;
            }

            if (c6TrafficCheck.Checked)
            {
                nums[6, 5] = Globals.randMod.Next(Convert.ToInt32(c6TrafficMin.Value), Convert.ToInt32(c6TrafficMax.Value) + 1);
            }
            else
            {
                nums[6, 5] = null;
            }

            if (c6MedevacCheck.Checked)
            {
                nums[6, 6] = Globals.randMod.Next(Convert.ToInt32(c6MedevacMin.Value), Convert.ToInt32(c6MedevacMax.Value) + 1);
            }
            else
            {
                nums[6, 6] = null;
            }

            if (c6TransportCheck.Checked)
            {
                nums[6, 7] = Globals.randMod.Next(Convert.ToInt32(c6TransportMin.Value), Convert.ToInt32(c6TransportMax.Value) + 1);
            }
            else
            {
                nums[6, 7] = null;
            }

            if (c6DayCheck.Checked)
            {
                nums[6, 8] = Globals.randMod.Next(Convert.ToInt32(c6DayMin.Value), Convert.ToInt32(c6DayMax.Value) + 1);
            }
            else
            {
                nums[6, 8] = null;
            }

            if (c6PointCheck.Checked)
            {
                nums[6, 9] = Globals.randMod.Next(Convert.ToInt32(c6PointsMin.Value), Convert.ToInt32(c6PointsMax.Value) + 1);
            }
            else
            {
                nums[6, 9] = null;
            }

            if (c6MoneyCheck.Checked)
            {
                nums[6, 10] = Globals.randMod.Next(Convert.ToInt32(c6MoneyMin.Value), Convert.ToInt32(c6MoneyMax.Value) + 1);
            }
            else
            {
                nums[6, 10] = null;
            }

            //City 7
            if (c7DiffCheck.Checked)
            {
                nums[7, 0] = Globals.randMod.Next(Convert.ToInt32(c7DiffMin.Value), Convert.ToInt32(c7DiffMax.Value) + 1);
            }
            else
            {
                nums[7, 0] = null;
            }

            if (c7FireCheck.Checked)
            {
                nums[7, 1] = Globals.randMod.Next(Convert.ToInt32(c7FireMin.Value), Convert.ToInt32(c7FireMax.Value) + 1);
            }
            else
            {
                nums[7, 1] = null;
            }

            if (c7CrimeCheck.Checked)
            {
                nums[7, 2] = Globals.randMod.Next(Convert.ToInt32(c7CrimeMin.Value), Convert.ToInt32(c7CrimeMax.Value) + 1);
            }
            else
            {
                nums[7, 2] = null;
            }

            if (c7RescueCheck.Checked)
            {
                nums[7, 3] = Globals.randMod.Next(Convert.ToInt32(c7RescueMin.Value), Convert.ToInt32(c7RescueMax.Value) + 1);
            }
            else
            {
                nums[7, 3] = null;
            }

            if (c7RiotCheck.Checked)
            {
                nums[7, 4] = Globals.randMod.Next(Convert.ToInt32(c7RiotMin.Value), Convert.ToInt32(c7RiotMax.Value) + 1);
            }
            else
            {
                nums[7, 4] = null;
            }

            if (c7TrafficCheck.Checked)
            {
                nums[7, 5] = Globals.randMod.Next(Convert.ToInt32(c7TrafficMin.Value), Convert.ToInt32(c7TrafficMax.Value) + 1);
            }
            else
            {
                nums[7, 5] = null;
            }

            if (c7MedevacCheck.Checked)
            {
                nums[7, 6] = Globals.randMod.Next(Convert.ToInt32(c7MedevacMin.Value), Convert.ToInt32(c7MedevacMax.Value) + 1);
            }
            else
            {
                nums[7, 6] = null;
            }

            if (c7TransportCheck.Checked)
            {
                nums[7, 7] = Globals.randMod.Next(Convert.ToInt32(c7TransportMin.Value), Convert.ToInt32(c7TransportMax.Value) + 1);
            }
            else
            {
                nums[7, 7] = null;
            }

            if (c7DayCheck.Checked)
            {
                nums[7, 8] = Globals.randMod.Next(Convert.ToInt32(c7DayMin.Value), Convert.ToInt32(c7DayMax.Value) + 1);
            }
            else
            {
                nums[7, 8] = null;
            }

            if (c7PointCheck.Checked)
            {
                nums[7, 9] = Globals.randMod.Next(Convert.ToInt32(c7PointsMin.Value), Convert.ToInt32(c7PointsMax.Value) + 1);
            }
            else
            {
                nums[7, 9] = null;
            }

            if (c7MoneyCheck.Checked)
            {
                nums[7, 10] = Globals.randMod.Next(Convert.ToInt32(c7MoneyMin.Value), Convert.ToInt32(c7MoneyMax.Value) + 1);
            }
            else
            {
                nums[7, 10] = null;
            }

            //City 8
            if (c8DiffCheck.Checked)
            {
                nums[8, 0] = Globals.randMod.Next(Convert.ToInt32(c8DiffMin.Value), Convert.ToInt32(c8DiffMax.Value) + 1);
            }
            else
            {
                nums[8, 0] = null;
            }

            if (c8FireCheck.Checked)
            {
                nums[8, 1] = Globals.randMod.Next(Convert.ToInt32(c8FireMin.Value), Convert.ToInt32(c8FireMax.Value) + 1);
            }
            else
            {
                nums[8, 1] = null;
            }

            if (c8CrimeCheck.Checked)
            {
                nums[8, 2] = Globals.randMod.Next(Convert.ToInt32(c8CrimeMin.Value), Convert.ToInt32(c8CrimeMax.Value) + 1);
            }
            else
            {
                nums[8, 2] = null;
            }

            if (c8RescueCheck.Checked)
            {
                nums[8, 3] = Globals.randMod.Next(Convert.ToInt32(c8RescueMin.Value), Convert.ToInt32(c8RescueMax.Value) + 1);
            }
            else
            {
                nums[8, 3] = null;
            }

            if (c8RiotCheck.Checked)
            {
                nums[8, 4] = Globals.randMod.Next(Convert.ToInt32(c8RiotMin.Value), Convert.ToInt32(c8RiotMax.Value) + 1);
            }
            else
            {
                nums[8, 4] = null;
            }

            if (c8TrafficCheck.Checked)
            {
                nums[8, 5] = Globals.randMod.Next(Convert.ToInt32(c8TrafficMin.Value), Convert.ToInt32(c8TrafficMax.Value) + 1);
            }
            else
            {
                nums[8, 5] = null;
            }

            if (c8MedevacCheck.Checked)
            {
                nums[8, 6] = Globals.randMod.Next(Convert.ToInt32(c8MedevacMin.Value), Convert.ToInt32(c8MedevacMax.Value) + 1);
            }
            else
            {
                nums[8, 6] = null;
            }

            if (c8TransportCheck.Checked)
            {
                nums[8, 7] = Globals.randMod.Next(Convert.ToInt32(c8TransportMin.Value), Convert.ToInt32(c8TransportMax.Value) + 1);
            }
            else
            {
                nums[8, 7] = null;
            }

            if (c8DayCheck.Checked)
            {
                nums[8, 8] = Globals.randMod.Next(Convert.ToInt32(c8DayMin.Value), Convert.ToInt32(c8DayMax.Value) + 1);
            }
            else
            {
                nums[8, 8] = null;
            }

            if (c8PointCheck.Checked)
            {
                nums[8, 9] = Globals.randMod.Next(Convert.ToInt32(c8PointsMin.Value), Convert.ToInt32(c8PointsMax.Value) + 1);
            }
            else
            {
                nums[8, 9] = null;
            }

            if (c8MoneyCheck.Checked)
            {
                nums[8, 10] = Globals.randMod.Next(Convert.ToInt32(c8MoneyMin.Value), Convert.ToInt32(c8MoneyMax.Value) + 1);
            }
            else
            {
                nums[8, 10] = null;
            }

            //City 9
            if (c9DiffCheck.Checked)
            {
                nums[9, 0] = Globals.randMod.Next(Convert.ToInt32(c9DiffMin.Value), Convert.ToInt32(c9DiffMax.Value) + 1);
            }
            else
            {
                nums[9, 0] = null;
            }

            if (c9FireCheck.Checked)
            {
                nums[9, 1] = Globals.randMod.Next(Convert.ToInt32(c9FireMin.Value), Convert.ToInt32(c9FireMax.Value) + 1);
            }
            else
            {
                nums[9, 1] = null;
            }

            if (c9CrimeCheck.Checked)
            {
                nums[9, 2] = Globals.randMod.Next(Convert.ToInt32(c9CrimeMin.Value), Convert.ToInt32(c9CrimeMax.Value) + 1);
            }
            else
            {
                nums[9, 2] = null;
            }

            if (c9RescueCheck.Checked)
            {
                nums[9, 3] = Globals.randMod.Next(Convert.ToInt32(c9RescueMin.Value), Convert.ToInt32(c9RescueMax.Value) + 1);
            }
            else
            {
                nums[9, 3] = null;
            }

            if (c9RiotCheck.Checked)
            {
                nums[9, 4] = Globals.randMod.Next(Convert.ToInt32(c9RiotMin.Value), Convert.ToInt32(c9RiotMax.Value) + 1);
            }
            else
            {
                nums[9, 4] = null;
            }

            if (c9TrafficCheck.Checked)
            {
                nums[9, 5] = Globals.randMod.Next(Convert.ToInt32(c9TrafficMin.Value), Convert.ToInt32(c9TrafficMax.Value) + 1);
            }
            else
            {
                nums[9, 5] = null;
            }

            if (c9MedevacCheck.Checked)
            {
                nums[9, 6] = Globals.randMod.Next(Convert.ToInt32(c9MedevacMin.Value), Convert.ToInt32(c9MedevacMax.Value) + 1);
            }
            else
            {
                nums[9, 6] = null;
            }

            if (c9TransportCheck.Checked)
            {
                nums[9, 7] = Globals.randMod.Next(Convert.ToInt32(c9TransportMin.Value), Convert.ToInt32(c9TransportMax.Value) + 1);
            }
            else
            {
                nums[9, 7] = null;
            }

            if (c9DayCheck.Checked)
            {
                nums[9, 8] = Globals.randMod.Next(Convert.ToInt32(c9DayMin.Value), Convert.ToInt32(c9DayMax.Value) + 1);
            }
            else
            {
                nums[9, 8] = null;
            }

            if (c9PointCheck.Checked)
            {
                nums[9, 9] = Globals.randMod.Next(Convert.ToInt32(c9PointsMin.Value), Convert.ToInt32(c9PointsMax.Value) + 1);
            }
            else
            {
                nums[9, 9] = null;
            }

            if (c9MoneyCheck.Checked)
            {
                nums[9, 10] = Globals.randMod.Next(Convert.ToInt32(c9MoneyMin.Value), Convert.ToInt32(c9MoneyMax.Value) + 1);
            }
            else
            {
                nums[9, 10] = null;
            }

            //City 10
            if (c10DiffCheck.Checked)
            {
                nums[10, 0] = Globals.randMod.Next(Convert.ToInt32(c10DiffMin.Value), Convert.ToInt32(c10DiffMax.Value) + 1);
            }
            else
            {
                nums[10, 0] = null;
            }

            if (c10FireCheck.Checked)
            {
                nums[10, 1] = Globals.randMod.Next(Convert.ToInt32(c10FireMin.Value), Convert.ToInt32(c10FireMax.Value) + 1);
            }
            else
            {
                nums[10, 1] = null;
            }

            if (c10CrimeCheck.Checked)
            {
                nums[10, 2] = Globals.randMod.Next(Convert.ToInt32(c10CrimeMin.Value), Convert.ToInt32(c10CrimeMax.Value) + 1);
            }
            else
            {
                nums[10, 2] = null;
            }

            if (c10RescueCheck.Checked)
            {
                nums[10, 3] = Globals.randMod.Next(Convert.ToInt32(c10RescueMin.Value), Convert.ToInt32(c10RescueMax.Value) + 1);
            }
            else
            {
                nums[10, 3] = null;
            }

            if (c10RiotCheck.Checked)
            {
                nums[10, 4] = Globals.randMod.Next(Convert.ToInt32(c10RiotMin.Value), Convert.ToInt32(c10RiotMax.Value) + 1);
            }
            else
            {
                nums[10, 4] = null;
            }

            if (c10TrafficCheck.Checked)
            {
                nums[10, 5] = Globals.randMod.Next(Convert.ToInt32(c10TrafficMin.Value), Convert.ToInt32(c10TrafficMax.Value) + 1);
            }
            else
            {
                nums[10, 5] = null;
            }

            if (c10MedevacCheck.Checked)
            {
                nums[10, 6] = Globals.randMod.Next(Convert.ToInt32(c10MedevacMin.Value), Convert.ToInt32(c10MedevacMax.Value) + 1);
            }
            else
            {
                nums[10, 6] = null;
            }

            if (c10TransportCheck.Checked)
            {
                nums[10, 7] = Globals.randMod.Next(Convert.ToInt32(c10TransportMin.Value), Convert.ToInt32(c10TransportMax.Value) + 1);
            }
            else
            {
                nums[10, 7] = null;
            }

            if (c10DayCheck.Checked)
            {
                nums[10, 8] = Globals.randMod.Next(Convert.ToInt32(c10DayMin.Value), Convert.ToInt32(c10DayMax.Value) + 1);
            }
            else
            {
                nums[10, 8] = null;
            }

            if (c10PointCheck.Checked)
            {
                nums[10, 9] = Globals.randMod.Next(Convert.ToInt32(c10PointsMin.Value), Convert.ToInt32(c10PointsMax.Value) + 1);
            }
            else
            {
                nums[10, 9] = null;
            }

            if (c10MoneyCheck.Checked)
            {
                nums[10, 10] = Globals.randMod.Next(Convert.ToInt32(c10MoneyMin.Value), Convert.ToInt32(c10MoneyMax.Value) + 1);
            }
            else
            {
                nums[10, 10] = null;
            }

            //City 11
            if (c11DiffCheck.Checked)
            {
                nums[11, 0] = Globals.randMod.Next(Convert.ToInt32(c11DiffMin.Value), Convert.ToInt32(c11DiffMax.Value) + 1);
            }
            else
            {
                nums[11, 0] = null;
            }

            if (c11FireCheck.Checked)
            {
                nums[11, 1] = Globals.randMod.Next(Convert.ToInt32(c11FireMin.Value), Convert.ToInt32(c11FireMax.Value) + 1);
            }
            else
            {
                nums[11, 1] = null;
            }

            if (c11CrimeCheck.Checked)
            {
                nums[11, 2] = Globals.randMod.Next(Convert.ToInt32(c11CrimeMin.Value), Convert.ToInt32(c11CrimeMax.Value) + 1);
            }
            else
            {
                nums[11, 2] = null;
            }

            if (c11RescueCheck.Checked)
            {
                nums[11, 3] = Globals.randMod.Next(Convert.ToInt32(c11RescueMin.Value), Convert.ToInt32(c11RescueMax.Value) + 1);
            }
            else
            {
                nums[11, 3] = null;
            }

            if (c11RiotCheck.Checked)
            {
                nums[11, 4] = Globals.randMod.Next(Convert.ToInt32(c11RiotMin.Value), Convert.ToInt32(c11RiotMax.Value) + 1);
            }
            else
            {
                nums[11, 4] = null;
            }

            if (c11TrafficCheck.Checked)
            {
                nums[11, 5] = Globals.randMod.Next(Convert.ToInt32(c11TrafficMin.Value), Convert.ToInt32(c11TrafficMax.Value) + 1);
            }
            else
            {
                nums[11, 5] = null;
            }

            if (c11MedevacCheck.Checked)
            {
                nums[11, 6] = Globals.randMod.Next(Convert.ToInt32(c11MedevacMin.Value), Convert.ToInt32(c11MedevacMax.Value) + 1);
            }
            else
            {
                nums[11, 6] = null;
            }

            if (c11TransportCheck.Checked)
            {
                nums[11, 7] = Globals.randMod.Next(Convert.ToInt32(c11TransportMin.Value), Convert.ToInt32(c11TransportMax.Value) + 1);
            }
            else
            {
                nums[11, 7] = null;
            }

            if (c11DayCheck.Checked)
            {
                nums[11, 8] = Globals.randMod.Next(Convert.ToInt32(c11DayMin.Value), Convert.ToInt32(c11DayMax.Value) + 1);
            }
            else
            {
                nums[11, 8] = null;
            }

            if (c11PointCheck.Checked)
            {
                nums[11, 9] = Globals.randMod.Next(Convert.ToInt32(c11PointsMin.Value), Convert.ToInt32(c11PointsMax.Value) + 1);
            }
            else
            {
                nums[11, 9] = null;
            }

            if (c11MoneyCheck.Checked)
            {
                nums[11, 10] = Globals.randMod.Next(Convert.ToInt32(c11MoneyMin.Value), Convert.ToInt32(c11MoneyMax.Value) + 1);
            }
            else
            {
                nums[11, 10] = null;
            }

            //City 12
            if (c12DiffCheck.Checked)
            {
                nums[12, 0] = Globals.randMod.Next(Convert.ToInt32(c12DiffMin.Value), Convert.ToInt32(c12DiffMax.Value) + 1);
            }
            else
            {
                nums[12, 0] = null;
            }

            if (c12FireCheck.Checked)
            {
                nums[12, 1] = Globals.randMod.Next(Convert.ToInt32(c12FireMin.Value), Convert.ToInt32(c12FireMax.Value) + 1);
            }
            else
            {
                nums[12, 1] = null;
            }

            if (c12CrimeCheck.Checked)
            {
                nums[12, 2] = Globals.randMod.Next(Convert.ToInt32(c12CrimeMin.Value), Convert.ToInt32(c12CrimeMax.Value) + 1);
            }
            else
            {
                nums[12, 2] = null;
            }

            if (c12RescueCheck.Checked)
            {
                nums[12, 3] = Globals.randMod.Next(Convert.ToInt32(c12RescueMin.Value), Convert.ToInt32(c12RescueMax.Value) + 1);
            }
            else
            {
                nums[12, 3] = null;
            }

            if (c12RiotCheck.Checked)
            {
                nums[12, 4] = Globals.randMod.Next(Convert.ToInt32(c12RiotMin.Value), Convert.ToInt32(c12RiotMax.Value) + 1);
            }
            else
            {
                nums[12, 4] = null;
            }

            if (c12TrafficCheck.Checked)
            {
                nums[12, 5] = Globals.randMod.Next(Convert.ToInt32(c12TrafficMin.Value), Convert.ToInt32(c12TrafficMax.Value) + 1);
            }
            else
            {
                nums[12, 5] = null;
            }

            if (c12MedevacCheck.Checked)
            {
                nums[12, 6] = Globals.randMod.Next(Convert.ToInt32(c12MedevacMin.Value), Convert.ToInt32(c12MedevacMax.Value) + 1);
            }
            else
            {
                nums[12, 6] = null;
            }

            if (c12TransportCheck.Checked)
            {
                nums[12, 7] = Globals.randMod.Next(Convert.ToInt32(c12TransportMin.Value), Convert.ToInt32(c12TransportMax.Value) + 1);
            }
            else
            {
                nums[12, 7] = null;
            }

            if (c12DayCheck.Checked)
            {
                nums[12, 8] = Globals.randMod.Next(Convert.ToInt32(c12DayMin.Value), Convert.ToInt32(c12DayMax.Value) + 1);
            }
            else
            {
                nums[12, 8] = null;
            }

            if (c12PointCheck.Checked)
            {
                nums[12, 9] = Globals.randMod.Next(Convert.ToInt32(c12PointsMin.Value), Convert.ToInt32(c12PointsMax.Value) + 1);
            }
            else
            {
                nums[12, 9] = null;
            }

            if (c12MoneyCheck.Checked)
            {
                nums[12, 10] = Globals.randMod.Next(Convert.ToInt32(c12MoneyMin.Value), Convert.ToInt32(c12MoneyMax.Value) + 1);
            }
            else
            {
                nums[12, 10] = null;
            }

            //City 13
            if (c13DiffCheck.Checked)
            {
                nums[13, 0] = Globals.randMod.Next(Convert.ToInt32(c13DiffMin.Value), Convert.ToInt32(c13DiffMax.Value) + 1);
            }
            else
            {
                nums[13, 0] = null;
            }

            if (c13FireCheck.Checked)
            {
                nums[13, 1] = Globals.randMod.Next(Convert.ToInt32(c13FireMin.Value), Convert.ToInt32(c13FireMax.Value) + 1);
            }
            else
            {
                nums[13, 1] = null;
            }

            if (c13CrimeCheck.Checked)
            {
                nums[13, 2] = Globals.randMod.Next(Convert.ToInt32(c13CrimeMin.Value), Convert.ToInt32(c13CrimeMax.Value) + 1);
            }
            else
            {
                nums[13, 2] = null;
            }

            if (c13RescueCheck.Checked)
            {
                nums[13, 3] = Globals.randMod.Next(Convert.ToInt32(c13RescueMin.Value), Convert.ToInt32(c13RescueMax.Value) + 1);
            }
            else
            {
                nums[13, 3] = null;
            }

            if (c13RiotCheck.Checked)
            {
                nums[13, 4] = Globals.randMod.Next(Convert.ToInt32(c13RiotMin.Value), Convert.ToInt32(c13RiotMax.Value) + 1);
            }
            else
            {
                nums[13, 4] = null;
            }

            if (c13TrafficCheck.Checked)
            {
                nums[13, 5] = Globals.randMod.Next(Convert.ToInt32(c13TrafficMin.Value), Convert.ToInt32(c13TrafficMax.Value) + 1);
            }
            else
            {
                nums[13, 5] = null;
            }

            if (c13MedevacCheck.Checked)
            {
                nums[13, 6] = Globals.randMod.Next(Convert.ToInt32(c13MedevacMin.Value), Convert.ToInt32(c13MedevacMax.Value) + 1);
            }
            else
            {
                nums[13, 6] = null;
            }

            if (c13TransportCheck.Checked)
            {
                nums[13, 7] = Globals.randMod.Next(Convert.ToInt32(c13TransportMin.Value), Convert.ToInt32(c13TransportMax.Value) + 1);
            }
            else
            {
                nums[13, 7] = null;
            }

            if (c13DayCheck.Checked)
            {
                nums[13, 8] = Globals.randMod.Next(Convert.ToInt32(c13DayMin.Value), Convert.ToInt32(c13DayMax.Value) + 1);
            }
            else
            {
                nums[13, 8] = null;
            }

            if (c13PointCheck.Checked)
            {
                nums[13, 9] = Globals.randMod.Next(Convert.ToInt32(c13PointsMin.Value), Convert.ToInt32(c13PointsMax.Value) + 1);
            }
            else
            {
                nums[13, 9] = null;
            }

            if (c13MoneyCheck.Checked)
            {
                nums[13, 10] = Globals.randMod.Next(Convert.ToInt32(c13MoneyMin.Value), Convert.ToInt32(c13MoneyMax.Value) + 1);
            }
            else
            {
                nums[13, 10] = null;
            }

            //City 14
            if (c14DiffCheck.Checked)
            {
                nums[14, 0] = Globals.randMod.Next(Convert.ToInt32(c14DiffMin.Value), Convert.ToInt32(c14DiffMax.Value) + 1);
            }
            else
            {
                nums[14, 0] = null;
            }

            if (c14FireCheck.Checked)
            {
                nums[14, 1] = Globals.randMod.Next(Convert.ToInt32(c14FireMin.Value), Convert.ToInt32(c14FireMax.Value) + 1);
            }
            else
            {
                nums[14, 1] = null;
            }

            if (c14CrimeCheck.Checked)
            {
                nums[14, 2] = Globals.randMod.Next(Convert.ToInt32(c14CrimeMin.Value), Convert.ToInt32(c14CrimeMax.Value) + 1);
            }
            else
            {
                nums[14, 2] = null;
            }

            if (c14RescueCheck.Checked)
            {
                nums[14, 3] = Globals.randMod.Next(Convert.ToInt32(c14RescueMin.Value), Convert.ToInt32(c14RescueMax.Value) + 1);
            }
            else
            {
                nums[14, 3] = null;
            }

            if (c14RiotCheck.Checked)
            {
                nums[14, 4] = Globals.randMod.Next(Convert.ToInt32(c14RiotMin.Value), Convert.ToInt32(c14RiotMax.Value) + 1);
            }
            else
            {
                nums[14, 4] = null;
            }

            if (c14TrafficCheck.Checked)
            {
                nums[14, 5] = Globals.randMod.Next(Convert.ToInt32(c14TrafficMin.Value), Convert.ToInt32(c14TrafficMax.Value) + 1);
            }
            else
            {
                nums[14, 5] = null;
            }

            if (c14MedevacCheck.Checked)
            {
                nums[14, 6] = Globals.randMod.Next(Convert.ToInt32(c14MedevacMin.Value), Convert.ToInt32(c14MedevacMax.Value) + 1);
            }
            else
            {
                nums[14, 6] = null;
            }

            if (c14TransportCheck.Checked)
            {
                nums[14, 7] = Globals.randMod.Next(Convert.ToInt32(c14TransportMin.Value), Convert.ToInt32(c14TransportMax.Value) + 1);
            }
            else
            {
                nums[14, 7] = null;
            }

            if (c14DayCheck.Checked)
            {
                nums[14, 8] = Globals.randMod.Next(Convert.ToInt32(c14DayMin.Value), Convert.ToInt32(c14DayMax.Value) + 1);
            }
            else
            {
                nums[14, 8] = null;
            }

            if (c14PointCheck.Checked)
            {
                nums[14, 9] = Globals.randMod.Next(Convert.ToInt32(c14PointsMin.Value), Convert.ToInt32(c14PointsMax.Value) + 1);
            }
            else
            {
                nums[14, 9] = null;
            }

            if (c14MoneyCheck.Checked)
            {
                nums[14, 10] = Globals.randMod.Next(Convert.ToInt32(c14MoneyMin.Value), Convert.ToInt32(c14MoneyMax.Value) + 1);
            }
            else
            {
                nums[14, 10] = null;
            }

            //City 15
            if (c15DiffCheck.Checked)
            {
                nums[15, 0] = Globals.randMod.Next(Convert.ToInt32(c15DiffMin.Value), Convert.ToInt32(c15DiffMax.Value) + 1);
            }
            else
            {
                nums[15, 0] = null;
            }

            if (c15FireCheck.Checked)
            {
                nums[15, 1] = Globals.randMod.Next(Convert.ToInt32(c15FireMin.Value), Convert.ToInt32(c15FireMax.Value) + 1);
            }
            else
            {
                nums[15, 1] = null;
            }

            if (c15CrimeCheck.Checked)
            {
                nums[15, 2] = Globals.randMod.Next(Convert.ToInt32(c15CrimeMin.Value), Convert.ToInt32(c15CrimeMax.Value) + 1);
            }
            else
            {
                nums[15, 2] = null;
            }

            if (c15RescueCheck.Checked)
            {
                nums[15, 3] = Globals.randMod.Next(Convert.ToInt32(c15RescueMin.Value), Convert.ToInt32(c15RescueMax.Value) + 1);
            }
            else
            {
                nums[15, 3] = null;
            }

            if (c15RiotCheck.Checked)
            {
                nums[15, 4] = Globals.randMod.Next(Convert.ToInt32(c15RiotMin.Value), Convert.ToInt32(c15RiotMax.Value) + 1);
            }
            else
            {
                nums[15, 4] = null;
            }

            if (c15TrafficCheck.Checked)
            {
                nums[15, 5] = Globals.randMod.Next(Convert.ToInt32(c15TrafficMin.Value), Convert.ToInt32(c15TrafficMax.Value) + 1);
            }
            else
            {
                nums[15, 5] = null;
            }

            if (c15MedevacCheck.Checked)
            {
                nums[15, 6] = Globals.randMod.Next(Convert.ToInt32(c15MedevacMin.Value), Convert.ToInt32(c15MedevacMax.Value) + 1);
            }
            else
            {
                nums[15, 6] = null;
            }

            if (c15TransportCheck.Checked)
            {
                nums[15, 7] = Globals.randMod.Next(Convert.ToInt32(c15TransportMin.Value), Convert.ToInt32(c15TransportMax.Value) + 1);
            }
            else
            {
                nums[15, 7] = null;
            }

            if (c15DayCheck.Checked)
            {
                nums[15, 8] = Globals.randMod.Next(Convert.ToInt32(c15DayMin.Value), Convert.ToInt32(c15DayMax.Value) + 1);
            }
            else
            {
                nums[15, 8] = null;
            }

            if (c15PointCheck.Checked)
            {
                nums[15, 9] = Globals.randMod.Next(Convert.ToInt32(c15PointsMin.Value), Convert.ToInt32(c15PointsMax.Value) + 1);
            }
            else
            {
                nums[15, 9] = null;
            }

            if (c15MoneyCheck.Checked)
            {
                nums[15, 10] = Globals.randMod.Next(Convert.ToInt32(c15MoneyMin.Value), Convert.ToInt32(c15MoneyMax.Value) + 1);
            }
            else
            {
                nums[15, 10] = null;
            }

            //City 16
            if (c16DiffCheck.Checked)
            {
                nums[16, 0] = Globals.randMod.Next(Convert.ToInt32(c16DiffMin.Value), Convert.ToInt32(c16DiffMax.Value) + 1);
            }
            else
            {
                nums[16, 0] = null;
            }

            if (c16FireCheck.Checked)
            {
                nums[16, 1] = Globals.randMod.Next(Convert.ToInt32(c16FireMin.Value), Convert.ToInt32(c16FireMax.Value) + 1);
            }
            else
            {
                nums[16, 1] = null;
            }

            if (c16CrimeCheck.Checked)
            {
                nums[16, 2] = Globals.randMod.Next(Convert.ToInt32(c16CrimeMin.Value), Convert.ToInt32(c16CrimeMax.Value) + 1);
            }
            else
            {
                nums[16, 2] = null;
            }

            if (c16RescueCheck.Checked)
            {
                nums[16, 3] = Globals.randMod.Next(Convert.ToInt32(c16RescueMin.Value), Convert.ToInt32(c16RescueMax.Value) + 1);
            }
            else
            {
                nums[16, 3] = null;
            }

            if (c16RiotCheck.Checked)
            {
                nums[16, 4] = Globals.randMod.Next(Convert.ToInt32(c16RiotMin.Value), Convert.ToInt32(c16RiotMax.Value) + 1);
            }
            else
            {
                nums[16, 4] = null;
            }

            if (c16TrafficCheck.Checked)
            {
                nums[16, 5] = Globals.randMod.Next(Convert.ToInt32(c16TrafficMin.Value), Convert.ToInt32(c16TrafficMax.Value) + 1);
            }
            else
            {
                nums[16, 5] = null;
            }

            if (c16MedevacCheck.Checked)
            {
                nums[16, 6] = Globals.randMod.Next(Convert.ToInt32(c16MedevacMin.Value), Convert.ToInt32(c16MedevacMax.Value) + 1);
            }
            else
            {
                nums[16, 6] = null;
            }

            if (c16TransportCheck.Checked)
            {
                nums[16, 7] = Globals.randMod.Next(Convert.ToInt32(c16TransportMin.Value), Convert.ToInt32(c16TransportMax.Value) + 1);
            }
            else
            {
                nums[16, 7] = null;
            }

            if (c16DayCheck.Checked)
            {
                nums[16, 8] = Globals.randMod.Next(Convert.ToInt32(c16DayMin.Value), Convert.ToInt32(c16DayMax.Value) + 1);
            }
            else
            {
                nums[16, 8] = null;
            }

            if (c16PointCheck.Checked)
            {
                nums[16, 9] = Globals.randMod.Next(Convert.ToInt32(c16PointsMin.Value), Convert.ToInt32(c16PointsMax.Value) + 1);
            }
            else
            {
                nums[16, 9] = null;
            }

            if (c16MoneyCheck.Checked)
            {
                nums[16, 10] = Globals.randMod.Next(Convert.ToInt32(c16MoneyMin.Value), Convert.ToInt32(c16MoneyMax.Value) + 1);
            }
            else
            {
                nums[16, 10] = null;
            }

            //City 17
            if (c17DiffCheck.Checked)
            {
                nums[17, 0] = Globals.randMod.Next(Convert.ToInt32(c17DiffMin.Value), Convert.ToInt32(c17DiffMax.Value) + 1);
            }
            else
            {
                nums[17, 0] = null;
            }

            if (c17FireCheck.Checked)
            {
                nums[17, 1] = Globals.randMod.Next(Convert.ToInt32(c17FireMin.Value), Convert.ToInt32(c17FireMax.Value) + 1);
            }
            else
            {
                nums[17, 1] = null;
            }

            if (c17CrimeCheck.Checked)
            {
                nums[17, 2] = Globals.randMod.Next(Convert.ToInt32(c17CrimeMin.Value), Convert.ToInt32(c17CrimeMax.Value) + 1);
            }
            else
            {
                nums[17, 2] = null;
            }

            if (c17RescueCheck.Checked)
            {
                nums[17, 3] = Globals.randMod.Next(Convert.ToInt32(c17RescueMin.Value), Convert.ToInt32(c17RescueMax.Value) + 1);
            }
            else
            {
                nums[17, 3] = null;
            }

            if (c17RiotCheck.Checked)
            {
                nums[17, 4] = Globals.randMod.Next(Convert.ToInt32(c17RiotMin.Value), Convert.ToInt32(c17RiotMax.Value) + 1);
            }
            else
            {
                nums[17, 4] = null;
            }

            if (c17TrafficCheck.Checked)
            {
                nums[17, 5] = Globals.randMod.Next(Convert.ToInt32(c17TrafficMin.Value), Convert.ToInt32(c17TrafficMax.Value) + 1);
            }
            else
            {
                nums[17, 5] = null;
            }

            if (c17MedevacCheck.Checked)
            {
                nums[17, 6] = Globals.randMod.Next(Convert.ToInt32(c17MedevacMin.Value), Convert.ToInt32(c17MedevacMax.Value) + 1);
            }
            else
            {
                nums[17, 6] = null;
            }

            if (c17TransportCheck.Checked)
            {
                nums[17, 7] = Globals.randMod.Next(Convert.ToInt32(c17TransportMin.Value), Convert.ToInt32(c17TransportMax.Value) + 1);
            }
            else
            {
                nums[17, 7] = null;
            }

            if (c17DayCheck.Checked)
            {
                nums[17, 8] = Globals.randMod.Next(Convert.ToInt32(c17DayMin.Value), Convert.ToInt32(c17DayMax.Value) + 1);
            }
            else
            {
                nums[17, 8] = null;
            }

            if (c17PointCheck.Checked)
            {
                nums[17, 9] = Globals.randMod.Next(Convert.ToInt32(c17PointsMin.Value), Convert.ToInt32(c17PointsMax.Value) + 1);
            }
            else
            {
                nums[17, 9] = null;
            }

            if (c17MoneyCheck.Checked)
            {
                nums[17, 10] = Globals.randMod.Next(Convert.ToInt32(c17MoneyMin.Value), Convert.ToInt32(c17MoneyMax.Value) + 1);
            }
            else
            {
                nums[17, 10] = null;
            }

            //City 18
            if (c18DiffCheck.Checked)
            {
                nums[18, 0] = Globals.randMod.Next(Convert.ToInt32(c18DiffMin.Value), Convert.ToInt32(c18DiffMax.Value) + 1);
            }
            else
            {
                nums[18, 0] = null;
            }

            if (c18FireCheck.Checked)
            {
                nums[18, 1] = Globals.randMod.Next(Convert.ToInt32(c18FireMin.Value), Convert.ToInt32(c18FireMax.Value) + 1);
            }
            else
            {
                nums[18, 1] = null;
            }

            if (c18CrimeCheck.Checked)
            {
                nums[18, 2] = Globals.randMod.Next(Convert.ToInt32(c18CrimeMin.Value), Convert.ToInt32(c18CrimeMax.Value) + 1);
            }
            else
            {
                nums[18, 2] = null;
            }

            if (c18RescueCheck.Checked)
            {
                nums[18, 3] = Globals.randMod.Next(Convert.ToInt32(c18RescueMin.Value), Convert.ToInt32(c18RescueMax.Value) + 1);
            }
            else
            {
                nums[18, 3] = null;
            }

            if (c18RiotCheck.Checked)
            {
                nums[18, 4] = Globals.randMod.Next(Convert.ToInt32(c18RiotMin.Value), Convert.ToInt32(c18RiotMax.Value) + 1);
            }
            else
            {
                nums[18, 4] = null;
            }

            if (c18TrafficCheck.Checked)
            {
                nums[18, 5] = Globals.randMod.Next(Convert.ToInt32(c18TrafficMin.Value), Convert.ToInt32(c18TrafficMax.Value) + 1);
            }
            else
            {
                nums[18, 5] = null;
            }

            if (c18MedevacCheck.Checked)
            {
                nums[18, 6] = Globals.randMod.Next(Convert.ToInt32(c18MedevacMin.Value), Convert.ToInt32(c18MedevacMax.Value) + 1);
            }
            else
            {
                nums[18, 6] = null;
            }

            if (c18TransportCheck.Checked)
            {
                nums[18, 7] = Globals.randMod.Next(Convert.ToInt32(c18TransportMin.Value), Convert.ToInt32(c18TransportMax.Value) + 1);
            }
            else
            {
                nums[18, 7] = null;
            }

            if (c18DayCheck.Checked)
            {
                nums[18, 8] = Globals.randMod.Next(Convert.ToInt32(c18DayMin.Value), Convert.ToInt32(c18DayMax.Value) + 1);
            }
            else
            {
                nums[18, 8] = null;
            }

            if (c18PointCheck.Checked)
            {
                nums[18, 9] = Globals.randMod.Next(Convert.ToInt32(c18PointsMin.Value), Convert.ToInt32(c18PointsMax.Value) + 1);
            }
            else
            {
                nums[18, 9] = null;
            }

            if (c18MoneyCheck.Checked)
            {
                nums[18, 10] = Globals.randMod.Next(Convert.ToInt32(c18MoneyMin.Value), Convert.ToInt32(c18MoneyMax.Value) + 1);
            }
            else
            {
                nums[18, 10] = null;
            }

            //City 19
            if (c19DiffCheck.Checked)
            {
                nums[19, 0] = Globals.randMod.Next(Convert.ToInt32(c19DiffMin.Value), Convert.ToInt32(c19DiffMax.Value) + 1);
            }
            else
            {
                nums[19, 0] = null;
            }

            if (c19FireCheck.Checked)
            {
                nums[19, 1] = Globals.randMod.Next(Convert.ToInt32(c19FireMin.Value), Convert.ToInt32(c19FireMax.Value) + 1);
            }
            else
            {
                nums[19, 1] = null;
            }

            if (c19CrimeCheck.Checked)
            {
                nums[19, 2] = Globals.randMod.Next(Convert.ToInt32(c19CrimeMin.Value), Convert.ToInt32(c19CrimeMax.Value) + 1);
            }
            else
            {
                nums[19, 2] = null;
            }

            if (c19RescueCheck.Checked)
            {
                nums[19, 3] = Globals.randMod.Next(Convert.ToInt32(c19RescueMin.Value), Convert.ToInt32(c19RescueMax.Value) + 1);
            }
            else
            {
                nums[19, 3] = null;
            }

            if (c19RiotCheck.Checked)
            {
                nums[19, 4] = Globals.randMod.Next(Convert.ToInt32(c19RiotMin.Value), Convert.ToInt32(c19RiotMax.Value) + 1);
            }
            else
            {
                nums[19, 4] = null;
            }

            if (c19TrafficCheck.Checked)
            {
                nums[19, 5] = Globals.randMod.Next(Convert.ToInt32(c19TrafficMin.Value), Convert.ToInt32(c19TrafficMax.Value) + 1);
            }
            else
            {
                nums[19, 5] = null;
            }

            if (c19MedevacCheck.Checked)
            {
                nums[19, 6] = Globals.randMod.Next(Convert.ToInt32(c19MedevacMin.Value), Convert.ToInt32(c19MedevacMax.Value) + 1);
            }
            else
            {
                nums[19, 6] = null;
            }

            if (c19TransportCheck.Checked)
            {
                nums[19, 7] = Globals.randMod.Next(Convert.ToInt32(c19TransportMin.Value), Convert.ToInt32(c19TransportMax.Value) + 1);
            }
            else
            {
                nums[19, 7] = null;
            }

            if (c19DayCheck.Checked)
            {
                nums[19, 8] = Globals.randMod.Next(Convert.ToInt32(c19DayMin.Value), Convert.ToInt32(c19DayMax.Value) + 1);
            }
            else
            {
                nums[19, 8] = null;
            }

            if (c19PointCheck.Checked)
            {
                nums[19, 9] = Globals.randMod.Next(Convert.ToInt32(c19PointsMin.Value), Convert.ToInt32(c19PointsMax.Value) + 1);
            }
            else
            {
                nums[19, 9] = null;
            }

            if (c19MoneyCheck.Checked)
            {
                nums[19, 10] = Globals.randMod.Next(Convert.ToInt32(c19MoneyMin.Value), Convert.ToInt32(c19MoneyMax.Value) + 1);
            }
            else
            {
                nums[19, 10] = null;
            }

            //City 20
            if (c20DiffCheck.Checked)
            {
                nums[20, 0] = Globals.randMod.Next(Convert.ToInt32(c20DiffMin.Value), Convert.ToInt32(c20DiffMax.Value) + 1);
            }
            else
            {
                nums[20, 0] = null;
            }

            if (c20FireCheck.Checked)
            {
                nums[20, 1] = Globals.randMod.Next(Convert.ToInt32(c20FireMin.Value), Convert.ToInt32(c20FireMax.Value) + 1);
            }
            else
            {
                nums[20, 1] = null;
            }

            if (c20CrimeCheck.Checked)
            {
                nums[20, 2] = Globals.randMod.Next(Convert.ToInt32(c20CrimeMin.Value), Convert.ToInt32(c20CrimeMax.Value) + 1);
            }
            else
            {
                nums[20, 2] = null;
            }

            if (c20RescueCheck.Checked)
            {
                nums[20, 3] = Globals.randMod.Next(Convert.ToInt32(c20RescueMin.Value), Convert.ToInt32(c20RescueMax.Value) + 1);
            }
            else
            {
                nums[20, 3] = null;
            }

            if (c20RiotCheck.Checked)
            {
                nums[20, 4] = Globals.randMod.Next(Convert.ToInt32(c20RiotMin.Value), Convert.ToInt32(c20RiotMax.Value) + 1);
            }
            else
            {
                nums[20, 4] = null;
            }

            if (c20TrafficCheck.Checked)
            {
                nums[20, 5] = Globals.randMod.Next(Convert.ToInt32(c20TrafficMin.Value), Convert.ToInt32(c20TrafficMax.Value) + 1);
            }
            else
            {
                nums[20, 5] = null;
            }

            if (c20MedevacCheck.Checked)
            {
                nums[20, 6] = Globals.randMod.Next(Convert.ToInt32(c20MedevacMin.Value), Convert.ToInt32(c20MedevacMax.Value) + 1);
            }
            else
            {
                nums[20, 6] = null;
            }

            if (c20TransportCheck.Checked)
            {
                nums[20, 7] = Globals.randMod.Next(Convert.ToInt32(c20TransportMin.Value), Convert.ToInt32(c20TransportMax.Value) + 1);
            }
            else
            {
                nums[20, 7] = null;
            }

            if (c20DayCheck.Checked)
            {
                nums[20, 8] = Globals.randMod.Next(Convert.ToInt32(c20DayMin.Value), Convert.ToInt32(c20DayMax.Value) + 1);
            }
            else
            {
                nums[20, 8] = null;
            }

            if (c20PointCheck.Checked)
            {
                nums[20, 9] = Globals.randMod.Next(Convert.ToInt32(c20PointsMin.Value), Convert.ToInt32(c20PointsMax.Value) + 1);
            }
            else
            {
                nums[20, 9] = null;
            }

            if (c20MoneyCheck.Checked)
            {
                nums[20, 10] = Globals.randMod.Next(Convert.ToInt32(c20MoneyMin.Value), Convert.ToInt32(c20MoneyMax.Value) + 1);
            }
            else
            {
                nums[20, 10] = null;
            }

            //City 21
            if (c21DiffCheck.Checked)
            {
                nums[21, 0] = Globals.randMod.Next(Convert.ToInt32(c21DiffMin.Value), Convert.ToInt32(c21DiffMax.Value) + 1);
            }
            else
            {
                nums[21, 0] = null;
            }

            if (c21FireCheck.Checked)
            {
                nums[21, 1] = Globals.randMod.Next(Convert.ToInt32(c21FireMin.Value), Convert.ToInt32(c21FireMax.Value) + 1);
            }
            else
            {
                nums[21, 1] = null;
            }

            if (c21CrimeCheck.Checked)
            {
                nums[21, 2] = Globals.randMod.Next(Convert.ToInt32(c21CrimeMin.Value), Convert.ToInt32(c21CrimeMax.Value) + 1);
            }
            else
            {
                nums[21, 2] = null;
            }

            if (c21RescueCheck.Checked)
            {
                nums[21, 3] = Globals.randMod.Next(Convert.ToInt32(c21RescueMin.Value), Convert.ToInt32(c21RescueMax.Value) + 1);
            }
            else
            {
                nums[21, 3] = null;
            }

            if (c21RiotCheck.Checked)
            {
                nums[21, 4] = Globals.randMod.Next(Convert.ToInt32(c21RiotMin.Value), Convert.ToInt32(c21RiotMax.Value) + 1);
            }
            else
            {
                nums[21, 4] = null;
            }

            if (c21TrafficCheck.Checked)
            {
                nums[21, 5] = Globals.randMod.Next(Convert.ToInt32(c21TrafficMin.Value), Convert.ToInt32(c21TrafficMax.Value) + 1);
            }
            else
            {
                nums[21, 5] = null;
            }

            if (c21MedevacCheck.Checked)
            {
                nums[21, 6] = Globals.randMod.Next(Convert.ToInt32(c21MedevacMin.Value), Convert.ToInt32(c21MedevacMax.Value) + 1);
            }
            else
            {
                nums[21, 6] = null;
            }

            if (c21TransportCheck.Checked)
            {
                nums[21, 7] = Globals.randMod.Next(Convert.ToInt32(c21TransportMin.Value), Convert.ToInt32(c21TransportMax.Value) + 1);
            }
            else
            {
                nums[21, 7] = null;
            }

            if (c21DayCheck.Checked)
            {
                nums[21, 8] = Globals.randMod.Next(Convert.ToInt32(c21DayMin.Value), Convert.ToInt32(c21DayMax.Value) + 1);
            }
            else
            {
                nums[21, 8] = null;
            }

            if (c21PointCheck.Checked)
            {
                nums[21, 9] = Globals.randMod.Next(Convert.ToInt32(c21PointsMin.Value), Convert.ToInt32(c21PointsMax.Value) + 1);
            }
            else
            {
                nums[21, 9] = null;
            }

            if (c21MoneyCheck.Checked)
            {
                nums[21, 10] = Globals.randMod.Next(Convert.ToInt32(c21MoneyMin.Value), Convert.ToInt32(c21MoneyMax.Value) + 1);
            }
            else
            {
                nums[21, 10] = null;
            }

            //City 22
            if (c22DiffCheck.Checked)
            {
                nums[22, 0] = Globals.randMod.Next(Convert.ToInt32(c22DiffMin.Value), Convert.ToInt32(c22DiffMax.Value) + 1);
            }
            else
            {
                nums[22, 0] = null;
            }

            if (c22FireCheck.Checked)
            {
                nums[22, 1] = Globals.randMod.Next(Convert.ToInt32(c22FireMin.Value), Convert.ToInt32(c22FireMax.Value) + 1);
            }
            else
            {
                nums[22, 1] = null;
            }

            if (c22CrimeCheck.Checked)
            {
                nums[22, 2] = Globals.randMod.Next(Convert.ToInt32(c22CrimeMin.Value), Convert.ToInt32(c22CrimeMax.Value) + 1);
            }
            else
            {
                nums[22, 2] = null;
            }

            if (c22RescueCheck.Checked)
            {
                nums[22, 3] = Globals.randMod.Next(Convert.ToInt32(c22RescueMin.Value), Convert.ToInt32(c22RescueMax.Value) + 1);
            }
            else
            {
                nums[22, 3] = null;
            }

            if (c22RiotCheck.Checked)
            {
                nums[22, 4] = Globals.randMod.Next(Convert.ToInt32(c22RiotMin.Value), Convert.ToInt32(c22RiotMax.Value) + 1);
            }
            else
            {
                nums[22, 4] = null;
            }

            if (c22TrafficCheck.Checked)
            {
                nums[22, 5] = Globals.randMod.Next(Convert.ToInt32(c22TrafficMin.Value), Convert.ToInt32(c22TrafficMax.Value) + 1);
            }
            else
            {
                nums[22, 5] = null;
            }

            if (c22MedevacCheck.Checked)
            {
                nums[22, 6] = Globals.randMod.Next(Convert.ToInt32(c22MedevacMin.Value), Convert.ToInt32(c22MedevacMax.Value) + 1);
            }
            else
            {
                nums[22, 6] = null;
            }

            if (c22TransportCheck.Checked)
            {
                nums[22, 7] = Globals.randMod.Next(Convert.ToInt32(c22TransportMin.Value), Convert.ToInt32(c22TransportMax.Value) + 1);
            }
            else
            {
                nums[22, 7] = null;
            }

            if (c22DayCheck.Checked)
            {
                nums[22, 8] = Globals.randMod.Next(Convert.ToInt32(c22DayMin.Value), Convert.ToInt32(c22DayMax.Value) + 1);
            }
            else
            {
                nums[22, 8] = null;
            }

            if (c22PointCheck.Checked)
            {
                nums[22, 9] = Globals.randMod.Next(Convert.ToInt32(c22PointsMin.Value), Convert.ToInt32(c22PointsMax.Value) + 1);
            }
            else
            {
                nums[22, 9] = null;
            }

            if (c22MoneyCheck.Checked)
            {
                nums[22, 10] = Globals.randMod.Next(Convert.ToInt32(c22MoneyMin.Value), Convert.ToInt32(c22MoneyMax.Value) + 1);
            }
            else
            {
                nums[22, 10] = null;
            }

            //City 23
            if (c23DiffCheck.Checked)
            {
                nums[23, 0] = Globals.randMod.Next(Convert.ToInt32(c23DiffMin.Value), Convert.ToInt32(c23DiffMax.Value) + 1);
            }
            else
            {
                nums[23, 0] = null;
            }

            if (c23FireCheck.Checked)
            {
                nums[23, 1] = Globals.randMod.Next(Convert.ToInt32(c23FireMin.Value), Convert.ToInt32(c23FireMax.Value) + 1);
            }
            else
            {
                nums[23, 1] = null;
            }

            if (c23CrimeCheck.Checked)
            {
                nums[23, 2] = Globals.randMod.Next(Convert.ToInt32(c23CrimeMin.Value), Convert.ToInt32(c23CrimeMax.Value) + 1);
            }
            else
            {
                nums[23, 2] = null;
            }

            if (c23RescueCheck.Checked)
            {
                nums[23, 3] = Globals.randMod.Next(Convert.ToInt32(c23RescueMin.Value), Convert.ToInt32(c23RescueMax.Value) + 1);
            }
            else
            {
                nums[23, 3] = null;
            }

            if (c23RiotCheck.Checked)
            {
                nums[23, 4] = Globals.randMod.Next(Convert.ToInt32(c23RiotMin.Value), Convert.ToInt32(c23RiotMax.Value) + 1);
            }
            else
            {
                nums[23, 4] = null;
            }

            if (c23TrafficCheck.Checked)
            {
                nums[23, 5] = Globals.randMod.Next(Convert.ToInt32(c23TrafficMin.Value), Convert.ToInt32(c23TrafficMax.Value) + 1);
            }
            else
            {
                nums[23, 5] = null;
            }

            if (c23MedevacCheck.Checked)
            {
                nums[23, 6] = Globals.randMod.Next(Convert.ToInt32(c23MedevacMin.Value), Convert.ToInt32(c23MedevacMax.Value) + 1);
            }
            else
            {
                nums[23, 6] = null;
            }

            if (c23TransportCheck.Checked)
            {
                nums[23, 7] = Globals.randMod.Next(Convert.ToInt32(c23TransportMin.Value), Convert.ToInt32(c23TransportMax.Value) + 1);
            }
            else
            {
                nums[23, 7] = null;
            }

            if (c23DayCheck.Checked)
            {
                nums[23, 8] = Globals.randMod.Next(Convert.ToInt32(c23DayMin.Value), Convert.ToInt32(c23DayMax.Value) + 1);
            }
            else
            {
                nums[23, 8] = null;
            }

            if (c23PointCheck.Checked)
            {
                nums[23, 9] = Globals.randMod.Next(Convert.ToInt32(c23PointsMin.Value), Convert.ToInt32(c23PointsMax.Value) + 1);
            }
            else
            {
                nums[23, 9] = null;
            }

            if (c23MoneyCheck.Checked)
            {
                nums[23, 10] = Globals.randMod.Next(Convert.ToInt32(c23MoneyMin.Value), Convert.ToInt32(c23MoneyMax.Value) + 1);
            }
            else
            {
                nums[23, 10] = null;
            }

            //City 24
            if (c24DiffCheck.Checked)
            {
                nums[24, 0] = Globals.randMod.Next(Convert.ToInt32(c24DiffMin.Value), Convert.ToInt32(c24DiffMax.Value) + 1);
            }
            else
            {
                nums[24, 0] = null;
            }

            if (c24FireCheck.Checked)
            {
                nums[24, 1] = Globals.randMod.Next(Convert.ToInt32(c24FireMin.Value), Convert.ToInt32(c24FireMax.Value) + 1);
            }
            else
            {
                nums[24, 1] = null;
            }

            if (c24CrimeCheck.Checked)
            {
                nums[24, 2] = Globals.randMod.Next(Convert.ToInt32(c24CrimeMin.Value), Convert.ToInt32(c24CrimeMax.Value) + 1);
            }
            else
            {
                nums[24, 2] = null;
            }

            if (c24RescueCheck.Checked)
            {
                nums[24, 3] = Globals.randMod.Next(Convert.ToInt32(c24RescueMin.Value), Convert.ToInt32(c24RescueMax.Value) + 1);
            }
            else
            {
                nums[24, 3] = null;
            }

            if (c24RiotCheck.Checked)
            {
                nums[24, 4] = Globals.randMod.Next(Convert.ToInt32(c24RiotMin.Value), Convert.ToInt32(c24RiotMax.Value) + 1);
            }
            else
            {
                nums[24, 4] = null;
            }

            if (c24TrafficCheck.Checked)
            {
                nums[24, 5] = Globals.randMod.Next(Convert.ToInt32(c24TrafficMin.Value), Convert.ToInt32(c24TrafficMax.Value) + 1);
            }
            else
            {
                nums[24, 5] = null;
            }

            if (c24MedevacCheck.Checked)
            {
                nums[24, 6] = Globals.randMod.Next(Convert.ToInt32(c24MedevacMin.Value), Convert.ToInt32(c24MedevacMax.Value) + 1);
            }
            else
            {
                nums[24, 6] = null;
            }

            if (c24TransportCheck.Checked)
            {
                nums[24, 7] = Globals.randMod.Next(Convert.ToInt32(c24TransportMin.Value), Convert.ToInt32(c24TransportMax.Value) + 1);
            }
            else
            {
                nums[24, 7] = null;
            }

            if (c24DayCheck.Checked)
            {
                nums[24, 8] = Globals.randMod.Next(Convert.ToInt32(c24DayMin.Value), Convert.ToInt32(c24DayMax.Value) + 1);
            }
            else
            {
                nums[24, 8] = null;
            }

            if (c24PointCheck.Checked)
            {
                nums[24, 9] = Globals.randMod.Next(Convert.ToInt32(c24PointsMin.Value), Convert.ToInt32(c24PointsMax.Value) + 1);
            }
            else
            {
                nums[24, 9] = null;
            }

            if (c24MoneyCheck.Checked)
            {
                nums[24, 10] = Globals.randMod.Next(Convert.ToInt32(c24MoneyMin.Value), Convert.ToInt32(c24MoneyMax.Value) + 1);
            }
            else
            {
                nums[24, 10] = null;
            }

            //City 25
            if (c25DiffCheck.Checked)
            {
                nums[25, 0] = Globals.randMod.Next(Convert.ToInt32(c25DiffMin.Value), Convert.ToInt32(c25DiffMax.Value) + 1);
            }
            else
            {
                nums[25, 0] = null;
            }

            if (c25FireCheck.Checked)
            {
                nums[25, 1] = Globals.randMod.Next(Convert.ToInt32(c25FireMin.Value), Convert.ToInt32(c25FireMax.Value) + 1);
            }
            else
            {
                nums[25, 1] = null;
            }

            if (c25CrimeCheck.Checked)
            {
                nums[25, 2] = Globals.randMod.Next(Convert.ToInt32(c25CrimeMin.Value), Convert.ToInt32(c25CrimeMax.Value) + 1);
            }
            else
            {
                nums[25, 2] = null;
            }

            if (c25RescueCheck.Checked)
            {
                nums[25, 3] = Globals.randMod.Next(Convert.ToInt32(c25RescueMin.Value), Convert.ToInt32(c25RescueMax.Value) + 1);
            }
            else
            {
                nums[25, 3] = null;
            }

            if (c25RiotCheck.Checked)
            {
                nums[25, 4] = Globals.randMod.Next(Convert.ToInt32(c25RiotMin.Value), Convert.ToInt32(c25RiotMax.Value) + 1);
            }
            else
            {
                nums[25, 4] = null;
            }

            if (c25TrafficCheck.Checked)
            {
                nums[25, 5] = Globals.randMod.Next(Convert.ToInt32(c25TrafficMin.Value), Convert.ToInt32(c25TrafficMax.Value) + 1);
            }
            else
            {
                nums[25, 5] = null;
            }

            if (c25MedevacCheck.Checked)
            {
                nums[25, 6] = Globals.randMod.Next(Convert.ToInt32(c25MedevacMin.Value), Convert.ToInt32(c25MedevacMax.Value) + 1);
            }
            else
            {
                nums[25, 6] = null;
            }

            if (c25TransportCheck.Checked)
            {
                nums[25, 7] = Globals.randMod.Next(Convert.ToInt32(c25TransportMin.Value), Convert.ToInt32(c25TransportMax.Value) + 1);
            }
            else
            {
                nums[25, 7] = null;
            }

            if (c25DayCheck.Checked)
            {
                nums[25, 8] = Globals.randMod.Next(Convert.ToInt32(c25DayMin.Value), Convert.ToInt32(c25DayMax.Value) + 1);
            }
            else
            {
                nums[25, 8] = null;
            }

            if (c25PointCheck.Checked)
            {
                nums[25, 9] = Globals.randMod.Next(Convert.ToInt32(c25PointsMin.Value), Convert.ToInt32(c25PointsMax.Value) + 1);
            }
            else
            {
                nums[25, 9] = null;
            }

            if (c25MoneyCheck.Checked)
            {
                nums[25, 10] = Globals.randMod.Next(Convert.ToInt32(c25MoneyMin.Value), Convert.ToInt32(c25MoneyMax.Value) + 1);
            }
            else
            {
                nums[25, 10] = null;
            }

            //City 26
            if (c26DiffCheck.Checked)
            {
                nums[26, 0] = Globals.randMod.Next(Convert.ToInt32(c26DiffMin.Value), Convert.ToInt32(c26DiffMax.Value) + 1);
            }
            else
            {
                nums[26, 0] = null;
            }

            if (c26FireCheck.Checked)
            {
                nums[26, 1] = Globals.randMod.Next(Convert.ToInt32(c26FireMin.Value), Convert.ToInt32(c26FireMax.Value) + 1);
            }
            else
            {
                nums[26, 1] = null;
            }

            if (c26CrimeCheck.Checked)
            {
                nums[26, 2] = Globals.randMod.Next(Convert.ToInt32(c26CrimeMin.Value), Convert.ToInt32(c26CrimeMax.Value) + 1);
            }
            else
            {
                nums[26, 2] = null;
            }

            if (c26RescueCheck.Checked)
            {
                nums[26, 3] = Globals.randMod.Next(Convert.ToInt32(c26RescueMin.Value), Convert.ToInt32(c26RescueMax.Value) + 1);
            }
            else
            {
                nums[26, 3] = null;
            }

            if (c26RiotCheck.Checked)
            {
                nums[26, 4] = Globals.randMod.Next(Convert.ToInt32(c26RiotMin.Value), Convert.ToInt32(c26RiotMax.Value) + 1);
            }
            else
            {
                nums[26, 4] = null;
            }

            if (c26TrafficCheck.Checked)
            {
                nums[26, 5] = Globals.randMod.Next(Convert.ToInt32(c26TrafficMin.Value), Convert.ToInt32(c26TrafficMax.Value) + 1);
            }
            else
            {
                nums[26, 5] = null;
            }

            if (c26MedevacCheck.Checked)
            {
                nums[26, 6] = Globals.randMod.Next(Convert.ToInt32(c26MedevacMin.Value), Convert.ToInt32(c26MedevacMax.Value) + 1);
            }
            else
            {
                nums[26, 6] = null;
            }

            if (c26TransportCheck.Checked)
            {
                nums[26, 7] = Globals.randMod.Next(Convert.ToInt32(c26TransportMin.Value), Convert.ToInt32(c26TransportMax.Value) + 1);
            }
            else
            {
                nums[26, 7] = null;
            }

            if (c26DayCheck.Checked)
            {
                nums[26, 8] = Globals.randMod.Next(Convert.ToInt32(c26DayMin.Value), Convert.ToInt32(c26DayMax.Value) + 1);
            }
            else
            {
                nums[26, 8] = null;
            }

            if (c26PointCheck.Checked)
            {
                nums[26, 9] = Globals.randMod.Next(Convert.ToInt32(c26PointsMin.Value), Convert.ToInt32(c26PointsMax.Value) + 1);
            }
            else
            {
                nums[26, 9] = null;
            }

            if (c26MoneyCheck.Checked)
            {
                nums[26, 10] = Globals.randMod.Next(Convert.ToInt32(c26MoneyMin.Value), Convert.ToInt32(c26MoneyMax.Value) + 1);
            }
            else
            {
                nums[26, 10] = null;
            }

            //City 27
            if (c27DiffCheck.Checked)
            {
                nums[27, 0] = Globals.randMod.Next(Convert.ToInt32(c27DiffMin.Value), Convert.ToInt32(c27DiffMax.Value) + 1);
            }
            else
            {
                nums[27, 0] = null;
            }

            if (c27FireCheck.Checked)
            {
                nums[27, 1] = Globals.randMod.Next(Convert.ToInt32(c27FireMin.Value), Convert.ToInt32(c27FireMax.Value) + 1);
            }
            else
            {
                nums[27, 1] = null;
            }

            if (c27CrimeCheck.Checked)
            {
                nums[27, 2] = Globals.randMod.Next(Convert.ToInt32(c27CrimeMin.Value), Convert.ToInt32(c27CrimeMax.Value) + 1);
            }
            else
            {
                nums[27, 2] = null;
            }

            if (c27RescueCheck.Checked)
            {
                nums[27, 3] = Globals.randMod.Next(Convert.ToInt32(c27RescueMin.Value), Convert.ToInt32(c27RescueMax.Value) + 1);
            }
            else
            {
                nums[27, 3] = null;
            }

            if (c27RiotCheck.Checked)
            {
                nums[27, 4] = Globals.randMod.Next(Convert.ToInt32(c27RiotMin.Value), Convert.ToInt32(c27RiotMax.Value) + 1);
            }
            else
            {
                nums[27, 4] = null;
            }

            if (c27TrafficCheck.Checked)
            {
                nums[27, 5] = Globals.randMod.Next(Convert.ToInt32(c27TrafficMin.Value), Convert.ToInt32(c27TrafficMax.Value) + 1);
            }
            else
            {
                nums[27, 5] = null;
            }

            if (c27MedevacCheck.Checked)
            {
                nums[27, 6] = Globals.randMod.Next(Convert.ToInt32(c27MedevacMin.Value), Convert.ToInt32(c27MedevacMax.Value) + 1);
            }
            else
            {
                nums[27, 6] = null;
            }

            if (c27TransportCheck.Checked)
            {
                nums[27, 7] = Globals.randMod.Next(Convert.ToInt32(c27TransportMin.Value), Convert.ToInt32(c27TransportMax.Value) + 1);
            }
            else
            {
                nums[27, 7] = null;
            }

            if (c27DayCheck.Checked)
            {
                nums[27, 8] = Globals.randMod.Next(Convert.ToInt32(c27DayMin.Value), Convert.ToInt32(c27DayMax.Value) + 1);
            }
            else
            {
                nums[27, 8] = null;
            }

            if (c27PointCheck.Checked)
            {
                nums[27, 9] = Globals.randMod.Next(Convert.ToInt32(c27PointsMin.Value), Convert.ToInt32(c27PointsMax.Value) + 1);
            }
            else
            {
                nums[27, 9] = null;
            }

            if (c27MoneyCheck.Checked)
            {
                nums[27, 10] = Globals.randMod.Next(Convert.ToInt32(c27MoneyMin.Value), Convert.ToInt32(c27MoneyMax.Value) + 1);
            }
            else
            {
                nums[27, 10] = null;
            }

            //City 28
            if (c28DiffCheck.Checked)
            {
                nums[28, 0] = Globals.randMod.Next(Convert.ToInt32(c28DiffMin.Value), Convert.ToInt32(c28DiffMax.Value) + 1);
            }
            else
            {
                nums[28, 0] = null;
            }

            if (c28FireCheck.Checked)
            {
                nums[28, 1] = Globals.randMod.Next(Convert.ToInt32(c28FireMin.Value), Convert.ToInt32(c28FireMax.Value) + 1);
            }
            else
            {
                nums[28, 1] = null;
            }

            if (c28CrimeCheck.Checked)
            {
                nums[28, 2] = Globals.randMod.Next(Convert.ToInt32(c28CrimeMin.Value), Convert.ToInt32(c28CrimeMax.Value) + 1);
            }
            else
            {
                nums[28, 2] = null;
            }

            if (c28RescueCheck.Checked)
            {
                nums[28, 3] = Globals.randMod.Next(Convert.ToInt32(c28RescueMin.Value), Convert.ToInt32(c28RescueMax.Value) + 1);
            }
            else
            {
                nums[28, 3] = null;
            }

            if (c28RiotCheck.Checked)
            {
                nums[28, 4] = Globals.randMod.Next(Convert.ToInt32(c28RiotMin.Value), Convert.ToInt32(c28RiotMax.Value) + 1);
            }
            else
            {
                nums[28, 4] = null;
            }

            if (c28TrafficCheck.Checked)
            {
                nums[28, 5] = Globals.randMod.Next(Convert.ToInt32(c28TrafficMin.Value), Convert.ToInt32(c28TrafficMax.Value) + 1);
            }
            else
            {
                nums[28, 5] = null;
            }

            if (c28MedevacCheck.Checked)
            {
                nums[28, 6] = Globals.randMod.Next(Convert.ToInt32(c28MedevacMin.Value), Convert.ToInt32(c28MedevacMax.Value) + 1);
            }
            else
            {
                nums[28, 6] = null;
            }

            if (c28TransportCheck.Checked)
            {
                nums[28, 7] = Globals.randMod.Next(Convert.ToInt32(c28TransportMin.Value), Convert.ToInt32(c28TransportMax.Value) + 1);
            }
            else
            {
                nums[28, 7] = null;
            }

            if (c28DayCheck.Checked)
            {
                nums[28, 8] = Globals.randMod.Next(Convert.ToInt32(c28DayMin.Value), Convert.ToInt32(c28DayMax.Value) + 1);
            }
            else
            {
                nums[28, 8] = null;
            }

            if (c28PointCheck.Checked)
            {
                nums[28, 9] = Globals.randMod.Next(Convert.ToInt32(c28PointsMin.Value), Convert.ToInt32(c28PointsMax.Value) + 1);
            }
            else
            {
                nums[28, 9] = null;
            }

            if (c28MoneyCheck.Checked)
            {
                nums[28, 10] = Globals.randMod.Next(Convert.ToInt32(c28MoneyMin.Value), Convert.ToInt32(c28MoneyMax.Value) + 1);
            }
            else
            {
                nums[28, 10] = null;
            }

            //City 29
            if (c29DiffCheck.Checked)
            {
                nums[29, 0] = Globals.randMod.Next(Convert.ToInt32(c29DiffMin.Value), Convert.ToInt32(c29DiffMax.Value) + 1);
            }
            else
            {
                nums[29, 0] = null;
            }

            if (c29FireCheck.Checked)
            {
                nums[29, 1] = Globals.randMod.Next(Convert.ToInt32(c29FireMin.Value), Convert.ToInt32(c29FireMax.Value) + 1);
            }
            else
            {
                nums[29, 1] = null;
            }

            if (c29CrimeCheck.Checked)
            {
                nums[29, 2] = Globals.randMod.Next(Convert.ToInt32(c29CrimeMin.Value), Convert.ToInt32(c29CrimeMax.Value) + 1);
            }
            else
            {
                nums[29, 2] = null;
            }

            if (c29RescueCheck.Checked)
            {
                nums[29, 3] = Globals.randMod.Next(Convert.ToInt32(c29RescueMin.Value), Convert.ToInt32(c29RescueMax.Value) + 1);
            }
            else
            {
                nums[29, 3] = null;
            }

            if (c29RiotCheck.Checked)
            {
                nums[29, 4] = Globals.randMod.Next(Convert.ToInt32(c29RiotMin.Value), Convert.ToInt32(c29RiotMax.Value) + 1);
            }
            else
            {
                nums[29, 4] = null;
            }

            if (c29TrafficCheck.Checked)
            {
                nums[29, 5] = Globals.randMod.Next(Convert.ToInt32(c29TrafficMin.Value), Convert.ToInt32(c29TrafficMax.Value) + 1);
            }
            else
            {
                nums[29, 5] = null;
            }

            if (c29MedevacCheck.Checked)
            {
                nums[29, 6] = Globals.randMod.Next(Convert.ToInt32(c29MedevacMin.Value), Convert.ToInt32(c29MedevacMax.Value) + 1);
            }
            else
            {
                nums[29, 6] = null;
            }

            if (c29TransportCheck.Checked)
            {
                nums[29, 7] = Globals.randMod.Next(Convert.ToInt32(c29TransportMin.Value), Convert.ToInt32(c29TransportMax.Value) + 1);
            }
            else
            {
                nums[29, 7] = null;
            }

            if (c29DayCheck.Checked)
            {
                nums[29, 8] = Globals.randMod.Next(Convert.ToInt32(c29DayMin.Value), Convert.ToInt32(c29DayMax.Value) + 1);
            }
            else
            {
                nums[29, 8] = null;
            }

            if (c29PointCheck.Checked)
            {
                nums[29, 9] = Globals.randMod.Next(Convert.ToInt32(c29PointsMin.Value), Convert.ToInt32(c29PointsMax.Value) + 1);
            }
            else
            {
                nums[29, 9] = null;
            }

            if (c29MoneyCheck.Checked)
            {
                nums[29, 10] = Globals.randMod.Next(Convert.ToInt32(c29MoneyMin.Value), Convert.ToInt32(c29MoneyMax.Value) + 1);
            }
            else
            {
                nums[29, 10] = null;
            }

            return nums;
        }

        private void mssnRand()
        {
            double?[,] mNums = new double?[9, 20];
            string line = null;

            //Mission File
            if (mChaos.Checked)
            {
                mNums = chaosMissionNums();
            }

            if (mCustom.Checked)
            {
                mNums = customizedMissionNums();
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
                            if (line.Contains("Value") && mNums[x, y] != null)
                            {
                                if ((x == 0 && (y == 1 || y == 2)) || (x == 1 && y == 4) || (x == 8 && y == 2))
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + Convert.ToDouble(mNums[x, y]).ToString("0.0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + Convert.ToDouble(mNums[x, y]).ToString("0");
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

        private double?[,] chaosMissionNums()
        {
            double?[,] nums = new double?[9, 20];

            //General Mission
            nums[0, 0] = Globals.randMod.Next(0, 11);
            nums[0, 1] = Globals.randMod.Next(100, 1000);
            nums[0, 1] = nums[0, 1] + Globals.randMod.NextDouble();
            nums[0, 2] = Globals.randMod.Next(2);
            nums[0, 2] = nums[0, 2] + Globals.randMod.NextDouble();
            nums[0, 3] = Globals.randMod.Next(2001);
            nums[0, 4] = Globals.randMod.Next(2001);

            //Riot
            nums[1, 0] = Globals.randMod.Next(2001);
            nums[1, 1] = Globals.randMod.Next(2001);
            nums[1, 2] = Globals.randMod.Next(1001);
            nums[1, 3] = Globals.randMod.Next(1001);
            nums[1, 4] = Globals.randMod.Next(1000);
            nums[1, 4] = nums[1, 4] + Globals.randMod.NextDouble();

            //Rescue
            for (int x = 0; x < 3; x++)
            {
                nums[2, x] = Globals.randMod.Next(2001);
            }

            //Transport
            nums[3, 0] = Globals.randMod.Next(201);
            nums[3, 1] = Globals.randMod.Next(201);
            nums[3, 2] = Globals.randMod.Next(101);
            nums[3, 3] = Globals.randMod.Next(101);

            //MedEvac
            nums[4, 0] = Globals.randMod.Next(2001);
            nums[4, 1] = Globals.randMod.Next(2001);
            nums[4, 2] = Globals.randMod.Next(101);

            //Fire
            for (int x = 0; x < 20; x++)
            {
                if (x < 13)
                {
                    nums[5, x] = Globals.randMod.Next(2001);
                }
                else if (x < 16)
                {
                    nums[5, x] = Globals.randMod.Next(201);
                }
                else
                {
                    nums[5, x] = Globals.randMod.Next(101);
                }
            }

            //Criminal
            nums[6, 0] = Globals.randMod.Next(2001);
            nums[6, 1] = Globals.randMod.Next(2001);

            //Speeder
            nums[7, 0] = Globals.randMod.Next(2001);
            nums[7, 1] = Globals.randMod.Next(2001);
            nums[7, 2] = Globals.randMod.Next(21);

            //Traffic
            nums[8, 0] = Globals.randMod.Next(1001);
            nums[8, 1] = Globals.randMod.Next(501);
            nums[8, 2] = Globals.randMod.Next(500);
            nums[8, 2] = nums[8, 2] + Globals.randMod.NextDouble();

            return nums;
        }

        private double?[,] customizedMissionNums()
        {
            double?[,] nums = new double?[9, 20];

            //General Missions
            if (genCheck1.Checked)
            {
                nums[0, 0] = Globals.randMod.Next(Convert.ToInt32(genMin1.Value), Convert.ToInt32(genMax1.Value) + 1);
            }
            else
            {
                nums[0, 0] = null;
            }

            if (genCheck2.Checked)
            {
                nums[0, 1] = Globals.randMod.Next(Convert.ToInt32(genMin2.Value), Convert.ToInt32(genMax2.Value));
                nums[0, 1] = nums[0, 1] + randDecimal(nums[0, 1], genMin2.Value, genMax2.Value);
            }
            else
            {
                nums[0, 1] = null;
            }

            if (genCheck3.Checked)
            {
                nums[0, 2] = Globals.randMod.Next(Convert.ToInt32(genMin3.Value), Convert.ToInt32(genMax3.Value));
                nums[0, 2] = nums[0, 2] + randDecimal(nums[0, 2], genMin3.Value, genMax3.Value);
            }
            else
            {
                nums[0, 2] = null;
            }

            if (genCheck4.Checked)
            {
                nums[0, 3] = Globals.randMod.Next(Convert.ToInt32(genMin4.Value), Convert.ToInt32(genMax4.Value) + 1);
            }
            else
            {
                nums[0, 3] = null;
            }

            if (genCheck5.Checked)
            {
                nums[0, 4] = Globals.randMod.Next(Convert.ToInt32(genMin5.Value), Convert.ToInt32(genMax5.Value) + 1);
            }
            else
            {
                nums[0, 4] = null;
            }

            //Riot Missions
            if (riotCheck1.Checked)
            {
                nums[1, 0] = Globals.randMod.Next(Convert.ToInt32(riotMin1.Value), Convert.ToInt32(riotMax1.Value) + 1);
            }
            else
            {
                nums[1, 0] = null;
            }

            if (riotCheck2.Checked)
            {
                nums[1, 1] = Globals.randMod.Next(Convert.ToInt32(riotMin2.Value), Convert.ToInt32(riotMax2.Value) + 1);
            }
            else
            {
                nums[1, 1] = null;
            }

            if (riotCheck3.Checked)
            {
                nums[1, 2] = Globals.randMod.Next(Convert.ToInt32(riotMin3.Value), Convert.ToInt32(riotMax3.Value) + 1);
            }
            else
            {
                nums[1, 2] = null;
            }

            if (riotCheck4.Checked)
            {
                nums[1, 3] = Globals.randMod.Next(Convert.ToInt32(riotMin4.Value), Convert.ToInt32(riotMax4.Value) + 1);
            }
            else
            {
                nums[1, 3] = null;
            }

            if (riotCheck5.Checked)
            {
                nums[1, 4] = Globals.randMod.Next(Convert.ToInt32(riotMin5.Value), Convert.ToInt32(riotMax5.Value));
                nums[1, 4] = nums[1, 4] + randDecimal(nums[1, 4], riotMin5.Value, riotMax5.Value);
            }
            else
            {
                nums[1, 4] = null;
            }

            //Rescue Missions
            if (rescueCheck1.Checked)
            {
                nums[2, 0] = Globals.randMod.Next(Convert.ToInt32(rescueMin1.Value), Convert.ToInt32(rescueMax1.Value) + 1);
            }
            else
            {
                nums[2, 0] = null;
            }

            if (rescueCheck2.Checked)
            {
                nums[2, 1] = Globals.randMod.Next(Convert.ToInt32(rescueMin2.Value), Convert.ToInt32(rescueMax2.Value) + 1);
            }
            else
            {
                nums[2, 1] = null;
            }

            if (rescueCheck3.Checked)
            {
                nums[2, 2] = Globals.randMod.Next(Convert.ToInt32(rescueMin3.Value), Convert.ToInt32(rescueMax3.Value) + 1);
            }
            else
            {
                nums[2, 2] = null;
            }

            //Transport Missions
            if (transportCheck1.Checked)
            {
                nums[3, 0] = Globals.randMod.Next(Convert.ToInt32(transportMin1.Value), Convert.ToInt32(transportMax1.Value) + 1);
            }
            else
            {
                nums[3, 0] = null;
            }

            if (transportCheck2.Checked)
            {
                nums[3, 1] = Globals.randMod.Next(Convert.ToInt32(transportMin2.Value), Convert.ToInt32(transportMax2.Value) + 1);
            }
            else
            {
                nums[3, 1] = null;
            }

            if (transportCheck3.Checked)
            {
                nums[3, 2] = Globals.randMod.Next(Convert.ToInt32(transportMin3.Value), Convert.ToInt32(transportMax3.Value) + 1);
            }
            else
            {
                nums[3, 2] = null;
            }

            if (transportCheck4.Checked)
            {
                nums[3, 3] = Globals.randMod.Next(Convert.ToInt32(transportMin4.Value), Convert.ToInt32(transportMax4.Value) + 1);
            }
            else
            {
                nums[3, 3] = null;
            }

            //MedEvac Missions
            if (medevacCheck1.Checked)
            {
                nums[4, 0] = Globals.randMod.Next(Convert.ToInt32(medevacMin1.Value), Convert.ToInt32(medevacMax1.Value) + 1);
            }
            else
            {
                nums[4, 0] = null;
            }

            if (medevacCheck2.Checked)
            {
                nums[4, 1] = Globals.randMod.Next(Convert.ToInt32(medevacMin2.Value), Convert.ToInt32(medevacMax2.Value) + 1);
            }
            else
            {
                nums[4, 1] = null;
            }

            if (medevacCheck3.Checked)
            {
                nums[4, 2] = Globals.randMod.Next(Convert.ToInt32(medevacMin3.Value), Convert.ToInt32(medevacMax3.Value) + 1);
            }
            else
            {
                nums[4, 2] = null;
            }

            //Fire Missions
            if (mFireCheck1.Checked)
            {
                nums[5, 0] = Globals.randMod.Next(Convert.ToInt32(mFireMin1.Value), Convert.ToInt32(mFireMax1.Value) + 1);
            }
            else
            {
                nums[5, 0] = null;
            }

            if (mFireCheck2.Checked)
            {
                nums[5, 1] = Globals.randMod.Next(Convert.ToInt32(mFireMin2.Value), Convert.ToInt32(mFireMax2.Value) + 1);
            }
            else
            {
                nums[5, 1] = null;
            }

            if (mFireCheck3.Checked)
            {
                nums[5, 2] = Globals.randMod.Next(Convert.ToInt32(mFireMin3.Value), Convert.ToInt32(mFireMax3.Value) + 1);
            }
            else
            {
                nums[5, 2] = null;
            }

            if (mFireCheck4.Checked)
            {
                nums[5, 3] = Globals.randMod.Next(Convert.ToInt32(mFireMin4.Value), Convert.ToInt32(mFireMax4.Value) + 1);
            }
            else
            {
                nums[5, 3] = null;
            }

            if (mFireCheck5.Checked)
            {
                nums[5, 4] = Globals.randMod.Next(Convert.ToInt32(mFireMin5.Value), Convert.ToInt32(mFireMax5.Value) + 1);
            }
            else
            {
                nums[5, 4] = null;
            }

            if (mFireCheck6.Checked)
            {
                nums[5, 5] = Globals.randMod.Next(Convert.ToInt32(mFireMin6.Value), Convert.ToInt32(mFireMax6.Value) + 1);
            }
            else
            {
                nums[5, 5] = null;
            }

            if (mFireCheck7.Checked)
            {
                nums[5, 6] = Globals.randMod.Next(Convert.ToInt32(mFireMin7.Value), Convert.ToInt32(mFireMax7.Value) + 1);
            }
            else
            {
                nums[5, 6] = null;
            }

            if (mFireCheck8.Checked)
            {
                nums[5, 7] = Globals.randMod.Next(Convert.ToInt32(mFireMin8.Value), Convert.ToInt32(mFireMax8.Value) + 1);
            }
            else
            {
                nums[5, 7] = null;
            }

            if (mFireCheck9.Checked)
            {
                nums[5, 8] = Globals.randMod.Next(Convert.ToInt32(mFireMin9.Value), Convert.ToInt32(mFireMax9.Value) + 1);
            }
            else
            {
                nums[5, 8] = null;
            }

            if (mFireCheck10.Checked)
            {
                nums[5, 9] = Globals.randMod.Next(Convert.ToInt32(mFireMin10.Value), Convert.ToInt32(mFireMax10.Value) + 1);
            }
            else
            {
                nums[5, 9] = null;
            }

            if (mFireCheck11.Checked)
            {
                nums[5, 10] = Globals.randMod.Next(Convert.ToInt32(mFireMin11.Value), Convert.ToInt32(mFireMax11.Value) + 1);
            }
            else
            {
                nums[5, 10] = null;
            }

            if (mFireCheck12.Checked)
            {
                nums[5, 11] = Globals.randMod.Next(Convert.ToInt32(mFireMin12.Value), Convert.ToInt32(mFireMax12.Value) + 1);
            }
            else
            {
                nums[5, 11] = null;
            }

            if (mFireCheck13.Checked)
            {
                nums[5, 12] = Globals.randMod.Next(Convert.ToInt32(mFireMin13.Value), Convert.ToInt32(mFireMax13.Value) + 1);
            }
            else
            {
                nums[5, 12] = null;
            }

            if (mFireCheck14.Checked)
            {
                nums[5, 13] = Globals.randMod.Next(Convert.ToInt32(mFireMin14.Value), Convert.ToInt32(mFireMax14.Value) + 1);
            }
            else
            {
                nums[5, 13] = null;
            }

            if (mFireCheck15.Checked)
            {
                nums[5, 14] = Globals.randMod.Next(Convert.ToInt32(mFireMin15.Value), Convert.ToInt32(mFireMax15.Value) + 1);
            }
            else
            {
                nums[5, 14] = null;
            }

            if (mFireCheck16.Checked)
            {
                nums[5, 15] = Globals.randMod.Next(Convert.ToInt32(mFireMin16.Value), Convert.ToInt32(mFireMax16.Value) + 1);
            }
            else
            {
                nums[5, 15] = null;
            }

            if (mFireCheck17.Checked)
            {
                nums[5, 16] = Globals.randMod.Next(Convert.ToInt32(mFireMin17.Value), Convert.ToInt32(mFireMax17.Value) + 1);
            }
            else
            {
                nums[5, 16] = null;
            }

            if (mFireCheck18.Checked)
            {
                nums[5, 17] = Globals.randMod.Next(Convert.ToInt32(mFireMin18.Value), Convert.ToInt32(mFireMax18.Value) + 1);
            }
            else
            {
                nums[5, 17] = null;
            }

            if (mFireCheck19.Checked)
            {
                nums[5, 18] = Globals.randMod.Next(Convert.ToInt32(mFireMin19.Value), Convert.ToInt32(mFireMax19.Value) + 1);
            }
            else
            {
                nums[5, 18] = null;
            }

            if (mFireCheck20.Checked)
            {
                nums[5, 19] = Globals.randMod.Next(Convert.ToInt32(mFireMin20.Value), Convert.ToInt32(mFireMax20.Value) + 1);
            }
            else
            {
                nums[5, 19] = null;
            }

            //Criminal Missios
            if (crimeCheck1.Checked)
            {
                nums[6, 0] = Globals.randMod.Next(Convert.ToInt32(crimeMin1.Value), Convert.ToInt32(crimeMax1.Value) + 1);
            }
            else
            {
                nums[6, 0] = null;
            }

            if (crimeCheck2.Checked)
            {
                nums[6, 1] = Globals.randMod.Next(Convert.ToInt32(crimeMin2.Value), Convert.ToInt32(crimeMax2.Value) + 1);
            }
            else
            {
                nums[6, 1] = null;
            }

            //Speeder Missions
            if (speedCheck1.Checked)
            {
                nums[7, 0] = Globals.randMod.Next(Convert.ToInt32(speedMin1.Value), Convert.ToInt32(speedMax1.Value) + 1);
            }
            else
            {
                nums[7, 0] = null;
            }

            if (speedCheck2.Checked)
            {
                nums[7, 1] = Globals.randMod.Next(Convert.ToInt32(speedMin2.Value), Convert.ToInt32(speedMax2.Value) + 1);
            }
            else
            {
                nums[7, 1] = null;
            }

            if (speedCheck3.Checked)
            {
                nums[7, 2] = Globals.randMod.Next(Convert.ToInt32(speedMin3.Value), Convert.ToInt32(speedMax3.Value) + 1);
            }
            else
            {
                nums[7, 2] = null;
            }

            //Traffic Missions
            if (trafficCheck1.Checked)
            {
                nums[8, 0] = Globals.randMod.Next(Convert.ToInt32(trafficMin1.Value), Convert.ToInt32(trafficMax1.Value) + 1);
            }
            else
            {
                nums[8, 0] = null;
            }

            if (trafficCheck2.Checked)
            {
                nums[8, 1] = Globals.randMod.Next(Convert.ToInt32(trafficMin2.Value), Convert.ToInt32(trafficMax2.Value) + 1);
            }
            else
            {
                nums[8, 1] = null;
            }

            if (trafficCheck3.Checked)
            {
                nums[8, 2] = Globals.randMod.Next(Convert.ToInt32(trafficMin3.Value), Convert.ToInt32(trafficMax3.Value) + 1);
            }
            else
            {
                nums[8, 2] = null;
            }

            return nums;
        }

        private void heliRand()
        {
            double?[,] hNums = new double?[12, 16];
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
                            if (line.Contains("Value") && hNums[x, y] != null)
                            {
                                if ((x < 9 && (y == 8 || y == 11 || y == 12)) || (x == 10 && (y == 0 || y == 1 || y == 2 || y == 4) || (x == 11 && y == 3)))
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + Convert.ToDouble(hNums[x, y]).ToString("0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + Convert.ToDouble(hNums[x, y]).ToString("0.0");
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

        private double?[,] chaosHeliNums()
        {
            double?[,] nums = new double?[12, 16];

            //Helicopters
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (y < 7)
                    {
                        nums[x, y] = Globals.randMod.Next(10, 800);
                        nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                    }
                    else if (y == 7)
                    {
                        nums[x, y] = Globals.randMod.Next(50);
                        nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                    }
                    else if (y == 8)
                    {
                        nums[x, y] = Globals.randMod.Next(10, 20001);
                    }
                    else if (y == 9)
                    {
                        nums[x, y] = Globals.randMod.Next(10, 900);
                        nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                    }
                    else if (y == 10)
                    {
                        nums[x, y] = Globals.randMod.Next(3000);
                        nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                    }
                    else if (y == 11)
                    {
                        nums[x, y] = Globals.randMod.Next(100001);
                    }
                    else if (y == 12)
                    {
                        nums[x, y] = Globals.randMod.Next(1, 5001);
                    }
                    else if (y == 13)
                    {
                        nums[x, y] = Globals.randMod.Next(100);
                        nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                    }
                    else
                    {
                        nums[x, y] = Globals.randMod.Next(10);
                        nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                    }
                }
            }
            for (int y = 0; y < 5; y++)
            {
                if (y < 2)
                {
                    nums[9, y] = Globals.randMod.Next(10, 800);
                    nums[9, y] = nums[9, y] + Globals.randMod.NextDouble();
                }
                else if (y < 4)
                {
                    nums[9, y] = Globals.randMod.Next(200);
                    nums[9, y] = nums[9, y] + Globals.randMod.NextDouble();
                }
                else
                {
                    nums[9, y] = Globals.randMod.Next(40);
                    nums[9, y] = nums[9, y] + Globals.randMod.NextDouble();
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
                            nums[x, y] = Globals.randMod.Next(2001);
                        }
                        else if (y == 2)
                        {
                            nums[x, y] = Globals.randMod.Next(8, 1001);
                        }
                        else if (y == 3)
                        {
                            nums[x, y] = Globals.randMod.NextDouble();
                        }
                        else if (y == 4)
                        {
                            nums[x, y] = Globals.randMod.Next(1, 65);
                        }
                        else
                        {
                            nums[x, y] = Globals.randMod.Next(10, 500);
                            nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                        }
                    }
                    else
                    {
                        if (y == 0)
                        {
                            nums[x, y] = Globals.randMod.Next(-100, 100);
                            nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                        }
                        else if (y == 1)
                        {
                            nums[x, y] = Globals.randMod.Next(200);
                            nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                        }
                        else if (y == 2)
                        {
                            nums[x, y] = Globals.randMod.Next(5);
                            nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                        }
                        else if (y == 3)
                        {
                            nums[x, y] = Globals.randMod.Next(501);
                        }
                        else
                        {
                            nums[x, y] = Globals.randMod.Next(100);
                            nums[x, y] = nums[x, y] + Globals.randMod.NextDouble();
                        }
                    }
                }
            }
            return nums;
        }

        private double?[,] customizedHeliNums()
        {
            double?[,] nums = new double?[12, 16];

            //Heli 0
            if (mBankCheck0.Checked)
            {
                nums[0, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin0.Value), Convert.ToInt32(mBankMax0.Value));
                nums[0, 0] = nums[0, 0] + randDecimal(nums[0, 0], mBankMin0.Value, mBankMax0.Value);
            }
            else
            {
                nums[0, 0] = null;
            }

            if (mSlideCheck0.Checked)
            {
                nums[0, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin0.Value), Convert.ToInt32(mSlideMax0.Value));
                nums[0, 1] = nums[0, 1] + randDecimal(nums[0, 1], mSlideMin0.Value, mSlideMax0.Value);
            }
            else
            {
                nums[0, 1] = null;
            }

            if (mPitchCheck0.Checked)
            {
                nums[0, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin0.Value), Convert.ToInt32(mPitchMax0.Value));
                nums[0, 2] = nums[0, 2] + randDecimal(nums[0, 2], mPitchMin0.Value, mPitchMax0.Value);
            }
            else
            {
                nums[0, 2] = null;
            }

            if (pitchRateCheck0.Checked)
            {
                nums[0, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin0.Value), Convert.ToInt32(pitchRateMax0.Value));
                nums[0, 3] = nums[0, 3] + randDecimal(nums[0, 3], pitchRateMin0.Value, pitchRateMax0.Value);
            }
            else
            {
                nums[0, 3] = null;
            }

            if (yawRateCheck0.Checked)
            {
                nums[0, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin0.Value), Convert.ToInt32(yawRateMax0.Value));
                nums[0, 4] = nums[0, 4] + randDecimal(nums[0, 4], yawRateMin0.Value, yawRateMax0.Value);
            }
            else
            {
                nums[0, 4] = null;
            }

            if (rollRateCheck0.Checked)
            {
                nums[0, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin0.Value), Convert.ToInt32(rollRateMax0.Value));
                nums[0, 5] = nums[0, 5] + randDecimal(nums[0, 5], rollRateMin0.Value, rollRateMax0.Value);
            }
            else
            {
                nums[0, 5] = null;
            }

            if (slideRateCheck0.Checked)
            {
                nums[0, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin0.Value), Convert.ToInt32(slideRateMax0.Value));
                nums[0, 6] = nums[0, 6] + randDecimal(nums[0, 6], slideRateMin0.Value, slideRateMax0.Value);
            }
            else
            {
                nums[0, 6] = null;
            }

            if (climbCheck0.Checked)
            {
                nums[0, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin0.Value), Convert.ToInt32(climbMax0.Value));
                nums[0, 7] = nums[0, 7] + randDecimal(nums[0, 7], climbMin0.Value, climbMax0.Value);
            }
            else
            {
                nums[0, 7] = null;
            }

            if (mLoadCheck0.Checked)
            {
                nums[0, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin0.Value), Convert.ToInt32(mLoadMax0.Value));
            }
            else
            {
                nums[0, 8] = null;
            }

            if (mYawRateCheck0.Checked)
            {
                nums[0, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin0.Value), Convert.ToInt32(mYawRateMax0.Value));
                nums[0, 9] = nums[0, 9] + randDecimal(nums[0, 9], mYawRateMin0.Value, mYawRateMax0.Value);
            }
            else
            {
                nums[0, 9] = null;
            }

            if (fuelRateCheck0.Checked)
            {
                nums[0, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin0.Value), Convert.ToInt32(fuelRateMax0.Value));
                nums[0, 10] = nums[0, 10] + randDecimal(nums[0, 10], fuelRateMin0.Value, fuelRateMax0.Value);
            }
            else
            {
                nums[0, 10] = null;
            }

            if (costCheck0.Checked)
            {
                nums[0, 11] = Globals.randMod.Next(Convert.ToInt32(costMin0.Value), Convert.ToInt32(costMax0.Value));
            }
            else
            {
                nums[0, 11] = null;
            }

            if (damageCheck0.Checked)
            {
                nums[0, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin0.Value), Convert.ToInt32(damageMax0.Value));
            }
            else
            {
                nums[0, 12] = null;
            }

            if (fuelCheck0.Checked)
            {
                nums[0, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin0.Value), Convert.ToInt32(fuelMax0.Value));
                nums[0, 13] = nums[0, 13] + randDecimal(nums[0, 13], fuelMin0.Value, fuelMax0.Value);
            }
            else
            {
                nums[0, 13] = null;
            }

            if (repairCheck0.Checked)
            {
                nums[0, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin0.Value), Convert.ToInt32(repairMax0.Value));
                nums[0, 14] = nums[0, 14] + randDecimal(nums[0, 14], repairMin0.Value, repairMax0.Value);
            }
            else
            {
                nums[0, 14] = null;
            }

            if (fuelCostCheck0.Checked)
            {
                nums[0, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin0.Value), Convert.ToInt32(fuelCostMax0.Value));
                nums[0, 15] = nums[0, 15] + randDecimal(nums[0, 15], fuelCostMin0.Value, fuelCostMax0.Value);
            }
            else
            {
                nums[0, 15] = null;
            }

            //Heli 1
            if (mBankCheck1.Checked)
            {
                nums[1, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin1.Value), Convert.ToInt32(mBankMax1.Value));
                nums[1, 0] = nums[1, 0] + randDecimal(nums[1, 0], mBankMin1.Value, mBankMax1.Value);
            }
            else
            {
                nums[1, 0] = null;
            }

            if (mSlideCheck1.Checked)
            {
                nums[1, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin1.Value), Convert.ToInt32(mSlideMax1.Value));
                nums[1, 1] = nums[1, 1] + randDecimal(nums[1, 1], mSlideMin1.Value, mSlideMax1.Value);
            }
            else
            {
                nums[1, 1] = null;
            }

            if (mPitchCheck1.Checked)
            {
                nums[1, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin1.Value), Convert.ToInt32(mPitchMax1.Value));
                nums[1, 2] = nums[1, 2] + randDecimal(nums[1, 2], mPitchMin1.Value, mPitchMax1.Value);
            }
            else
            {
                nums[1, 2] = null;
            }

            if (pitchRateCheck1.Checked)
            {
                nums[1, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin1.Value), Convert.ToInt32(pitchRateMax1.Value));
                nums[1, 3] = nums[1, 3] + randDecimal(nums[1, 3], pitchRateMin1.Value, pitchRateMax1.Value);
            }
            else
            {
                nums[1, 3] = null;
            }

            if (yawRateCheck1.Checked)
            {
                nums[1, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin1.Value), Convert.ToInt32(yawRateMax1.Value));
                nums[1, 4] = nums[1, 4] + randDecimal(nums[1, 4], yawRateMin1.Value, yawRateMax1.Value);
            }
            else
            {
                nums[1, 4] = null;
            }

            if (rollRateCheck1.Checked)
            {
                nums[1, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin1.Value), Convert.ToInt32(rollRateMax1.Value));
                nums[1, 5] = nums[1, 5] + randDecimal(nums[1, 5], rollRateMin1.Value, rollRateMax1.Value);
            }
            else
            {
                nums[1, 5] = null;
            }

            if (slideRateCheck1.Checked)
            {
                nums[1, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin1.Value), Convert.ToInt32(slideRateMax1.Value));
                nums[1, 6] = nums[1, 6] + randDecimal(nums[1, 6], slideRateMin1.Value, slideRateMax1.Value);
            }
            else
            {
                nums[1, 6] = null;
            }

            if (climbCheck1.Checked)
            {
                nums[1, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin1.Value), Convert.ToInt32(climbMax1.Value));
                nums[1, 7] = nums[1, 7] + randDecimal(nums[1, 7], climbMin1.Value, climbMax1.Value);
            }
            else
            {
                nums[1, 7] = null;
            }

            if (mLoadCheck1.Checked)
            {
                nums[1, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin1.Value), Convert.ToInt32(mLoadMax1.Value));
            }
            else
            {
                nums[1, 8] = null;
            }

            if (mYawRateCheck1.Checked)
            {
                nums[1, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin1.Value), Convert.ToInt32(mYawRateMax1.Value));
                nums[1, 9] = nums[1, 9] + randDecimal(nums[1, 9], mYawRateMin1.Value, mYawRateMax1.Value);
            }
            else
            {
                nums[1, 9] = null;
            }

            if (fuelRateCheck1.Checked)
            {
                nums[1, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin1.Value), Convert.ToInt32(fuelRateMax1.Value));
                nums[1, 10] = nums[1, 10] + randDecimal(nums[1, 10], fuelRateMin1.Value, fuelRateMax1.Value);
            }
            else
            {
                nums[1, 10] = null;
            }

            if (costCheck1.Checked)
            {
                nums[1, 11] = Globals.randMod.Next(Convert.ToInt32(costMin1.Value), Convert.ToInt32(costMax1.Value));
            }
            else
            {
                nums[1, 11] = null;
            }

            if (damageCheck1.Checked)
            {
                nums[1, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin1.Value), Convert.ToInt32(damageMax1.Value));
            }
            else
            {
                nums[1, 12] = null;
            }

            if (fuelCheck1.Checked)
            {
                nums[1, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin1.Value), Convert.ToInt32(fuelMax1.Value));
                nums[1, 13] = nums[1, 13] + randDecimal(nums[1, 13], fuelMin1.Value, fuelMax1.Value);
            }
            else
            {
                nums[1, 13] = null;
            }

            if (repairCheck1.Checked)
            {
                nums[1, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin1.Value), Convert.ToInt32(repairMax1.Value));
                nums[1, 14] = nums[1, 14] + randDecimal(nums[1, 14], repairMin1.Value, repairMax1.Value);
            }
            else
            {
                nums[1, 14] = null;
            }

            if (fuelCostCheck1.Checked)
            {
                nums[1, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin1.Value), Convert.ToInt32(fuelCostMax1.Value));
                nums[1, 15] = nums[1, 15] + randDecimal(nums[1, 15], fuelCostMin1.Value, fuelCostMax1.Value);
            }
            else
            {
                nums[1, 15] = null;
            }

            //Heli 2
            if (mBankCheck2.Checked)
            {
                nums[2, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin2.Value), Convert.ToInt32(mBankMax2.Value));
                nums[2, 0] = nums[2, 0] + randDecimal(nums[2, 0], mBankMin2.Value, mBankMax2.Value);
            }
            else
            {
                nums[2, 0] = null;
            }

            if (mSlideCheck2.Checked)
            {
                nums[2, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin2.Value), Convert.ToInt32(mSlideMax2.Value));
                nums[2, 1] = nums[2, 1] + randDecimal(nums[2, 1], mSlideMin2.Value, mSlideMax2.Value);
            }
            else
            {
                nums[2, 1] = null;
            }

            if (mPitchCheck2.Checked)
            {
                nums[2, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin2.Value), Convert.ToInt32(mPitchMax2.Value));
                nums[2, 2] = nums[2, 2] + randDecimal(nums[2, 2], mPitchMin2.Value, mPitchMax2.Value);
            }
            else
            {
                nums[2, 2] = null;
            }

            if (pitchRateCheck2.Checked)
            {
                nums[2, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin2.Value), Convert.ToInt32(pitchRateMax2.Value));
                nums[2, 3] = nums[2, 3] + randDecimal(nums[2, 3], pitchRateMin2.Value, pitchRateMax2.Value);
            }
            else
            {
                nums[2, 3] = null;
            }

            if (yawRateCheck2.Checked)
            {
                nums[2, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin2.Value), Convert.ToInt32(yawRateMax2.Value));
                nums[2, 4] = nums[2, 4] + randDecimal(nums[2, 4], yawRateMin2.Value, yawRateMax2.Value);
            }
            else
            {
                nums[2, 4] = null;
            }

            if (rollRateCheck2.Checked)
            {
                nums[2, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin2.Value), Convert.ToInt32(rollRateMax2.Value));
                nums[2, 5] = nums[2, 5] + randDecimal(nums[2, 5], rollRateMin2.Value, rollRateMax2.Value);
            }
            else
            {
                nums[2, 5] = null;
            }

            if (slideRateCheck2.Checked)
            {
                nums[2, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin2.Value), Convert.ToInt32(slideRateMax2.Value));
                nums[2, 6] = nums[2, 6] + randDecimal(nums[2, 6], slideRateMin2.Value, slideRateMax2.Value);
            }
            else
            {
                nums[2, 6] = null;
            }

            if (climbCheck2.Checked)
            {
                nums[2, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin2.Value), Convert.ToInt32(climbMax2.Value));
                nums[2, 7] = nums[2, 7] + randDecimal(nums[2, 7], climbMin2.Value, climbMax2.Value);
            }
            else
            {
                nums[2, 7] = null;
            }

            if (mLoadCheck2.Checked)
            {
                nums[2, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin2.Value), Convert.ToInt32(mLoadMax2.Value));
            }
            else
            {
                nums[2, 8] = null;
            }

            if (mYawRateCheck2.Checked)
            {
                nums[2, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin2.Value), Convert.ToInt32(mYawRateMax2.Value));
                nums[2, 9] = nums[2, 9] + randDecimal(nums[2, 9], mYawRateMin2.Value, mYawRateMax2.Value);
            }
            else
            {
                nums[2, 9] = null;
            }

            if (fuelRateCheck2.Checked)
            {
                nums[2, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin2.Value), Convert.ToInt32(fuelRateMax2.Value));
                nums[2, 10] = nums[2, 10] + randDecimal(nums[2, 10], fuelRateMin2.Value, fuelRateMax2.Value);
            }
            else
            {
                nums[2, 10] = null;
            }

            if (costCheck2.Checked)
            {
                nums[2, 11] = Globals.randMod.Next(Convert.ToInt32(costMin2.Value), Convert.ToInt32(costMax2.Value));
            }
            else
            {
                nums[2, 11] = null;
            }

            if (damageCheck2.Checked)
            {
                nums[2, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin2.Value), Convert.ToInt32(damageMax2.Value));
            }
            else
            {
                nums[2, 12] = null;
            }

            if (fuelCheck2.Checked)
            {
                nums[2, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin2.Value), Convert.ToInt32(fuelMax2.Value));
                nums[2, 13] = nums[2, 13] + randDecimal(nums[2, 13], fuelMin2.Value, fuelMax2.Value);
            }
            else
            {
                nums[2, 13] = null;
            }

            if (repairCheck2.Checked)
            {
                nums[2, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin2.Value), Convert.ToInt32(repairMax2.Value));
                nums[2, 14] = nums[2, 14] + randDecimal(nums[2, 14], repairMin2.Value, repairMax2.Value);
            }
            else
            {
                nums[2, 14] = null;
            }

            if (fuelCostCheck2.Checked)
            {
                nums[2, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin2.Value), Convert.ToInt32(fuelCostMax2.Value));
                nums[2, 15] = nums[2, 15] + randDecimal(nums[2, 15], fuelCostMin2.Value, fuelCostMax2.Value);
            }
            else
            {
                nums[2, 15] = null;
            }

            //Heli 3
            if (mBankCheck3.Checked)
            {
                nums[3, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin3.Value), Convert.ToInt32(mBankMax3.Value));
                nums[3, 0] = nums[3, 0] + randDecimal(nums[3, 0], mBankMin3.Value, mBankMax3.Value);
            }
            else
            {
                nums[3, 0] = null;
            }

            if (mSlideCheck3.Checked)
            {
                nums[3, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin3.Value), Convert.ToInt32(mSlideMax3.Value));
                nums[3, 1] = nums[3, 1] + randDecimal(nums[3, 1], mSlideMin3.Value, mSlideMax3.Value);
            }
            else
            {
                nums[3, 1] = null;
            }

            if (mPitchCheck3.Checked)
            {
                nums[3, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin3.Value), Convert.ToInt32(mPitchMax3.Value));
                nums[3, 2] = nums[3, 2] + randDecimal(nums[3, 2], mPitchMin3.Value, mPitchMax3.Value);
            }
            else
            {
                nums[3, 2] = null;
            }

            if (pitchRateCheck3.Checked)
            {
                nums[3, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin3.Value), Convert.ToInt32(pitchRateMax3.Value));
                nums[3, 3] = nums[3, 3] + randDecimal(nums[3, 3], pitchRateMin3.Value, pitchRateMax3.Value);
            }
            else
            {
                nums[3, 3] = null;
            }

            if (yawRateCheck3.Checked)
            {
                nums[3, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin3.Value), Convert.ToInt32(yawRateMax3.Value));
                nums[3, 4] = nums[3, 4] + randDecimal(nums[3, 4], yawRateMin3.Value, yawRateMax3.Value);
            }
            else
            {
                nums[3, 4] = null;
            }

            if (rollRateCheck3.Checked)
            {
                nums[3, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin3.Value), Convert.ToInt32(rollRateMax3.Value));
                nums[3, 5] = nums[3, 5] + randDecimal(nums[3, 5], rollRateMin3.Value, rollRateMax3.Value);
            }
            else
            {
                nums[3, 5] = null;
            }

            if (slideRateCheck3.Checked)
            {
                nums[3, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin3.Value), Convert.ToInt32(slideRateMax3.Value));
                nums[3, 6] = nums[3, 6] + randDecimal(nums[3, 6], slideRateMin3.Value, slideRateMax3.Value);
            }
            else
            {
                nums[3, 6] = null;
            }

            if (climbCheck3.Checked)
            {
                nums[3, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin3.Value), Convert.ToInt32(climbMax3.Value));
                nums[3, 7] = nums[3, 7] + randDecimal(nums[3, 7], climbMin3.Value, climbMax3.Value);
            }
            else
            {
                nums[3, 7] = null;
            }

            if (mLoadCheck3.Checked)
            {
                nums[3, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin3.Value), Convert.ToInt32(mLoadMax3.Value));
            }
            else
            {
                nums[3, 8] = null;
            }

            if (mYawRateCheck3.Checked)
            {
                nums[3, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin3.Value), Convert.ToInt32(mYawRateMax3.Value));
                nums[3, 9] = nums[3, 9] + randDecimal(nums[3, 9], mYawRateMin3.Value, mYawRateMax3.Value);
            }
            else
            {
                nums[3, 9] = null;
            }

            if (fuelRateCheck3.Checked)
            {
                nums[3, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin3.Value), Convert.ToInt32(fuelRateMax3.Value));
                nums[3, 10] = nums[3, 10] + randDecimal(nums[3, 10], fuelRateMin3.Value, fuelRateMax3.Value);
            }
            else
            {
                nums[3, 10] = null;
            }

            if (costCheck3.Checked)
            {
                nums[3, 11] = Globals.randMod.Next(Convert.ToInt32(costMin3.Value), Convert.ToInt32(costMax3.Value));
            }
            else
            {
                nums[3, 11] = null;
            }

            if (damageCheck3.Checked)
            {
                nums[3, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin3.Value), Convert.ToInt32(damageMax3.Value));
            }
            else
            {
                nums[3, 12] = null;
            }

            if (fuelCheck3.Checked)
            {
                nums[3, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin3.Value), Convert.ToInt32(fuelMax3.Value));
                nums[3, 13] = nums[3, 13] + randDecimal(nums[3, 13], fuelMin3.Value, fuelMax3.Value);
            }
            else
            {
                nums[3, 13] = null;
            }

            if (repairCheck3.Checked)
            {
                nums[3, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin3.Value), Convert.ToInt32(repairMax3.Value));
                nums[3, 14] = nums[3, 14] + randDecimal(nums[3, 14], repairMin3.Value, repairMax3.Value);
            }
            else
            {
                nums[3, 14] = null;
            }

            if (fuelCostCheck3.Checked)
            {
                nums[3, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin3.Value), Convert.ToInt32(fuelCostMax3.Value));
                nums[3, 15] = nums[3, 15] + randDecimal(nums[3, 15], fuelCostMin3.Value, fuelCostMax3.Value);
            }
            else
            {
                nums[3, 15] = null;
            }

            //Heli 4
            if (mBankCheck4.Checked)
            {
                nums[4, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin4.Value), Convert.ToInt32(mBankMax4.Value));
                nums[4, 0] = nums[4, 0] + randDecimal(nums[4, 0], mBankMin4.Value, mBankMax4.Value);
            }
            else
            {
                nums[4, 0] = null;
            }

            if (mSlideCheck4.Checked)
            {
                nums[4, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin4.Value), Convert.ToInt32(mSlideMax4.Value));
                nums[4, 1] = nums[4, 1] + randDecimal(nums[4, 1], mSlideMin4.Value, mSlideMax4.Value);
            }
            else
            {
                nums[4, 1] = null;
            }

            if (mPitchCheck4.Checked)
            {
                nums[4, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin4.Value), Convert.ToInt32(mPitchMax4.Value));
                nums[4, 2] = nums[4, 2] + randDecimal(nums[4, 2], mPitchMin4.Value, mPitchMax4.Value);
            }
            else
            {
                nums[4, 2] = null;
            }

            if (pitchRateCheck4.Checked)
            {
                nums[4, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin4.Value), Convert.ToInt32(pitchRateMax4.Value));
                nums[4, 3] = nums[4, 3] + randDecimal(nums[4, 3], pitchRateMin4.Value, pitchRateMax4.Value);
            }
            else
            {
                nums[4, 3] = null;
            }

            if (yawRateCheck4.Checked)
            {
                nums[4, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin4.Value), Convert.ToInt32(yawRateMax4.Value));
                nums[4, 4] = nums[4, 4] + randDecimal(nums[4, 4], yawRateMin4.Value, yawRateMax4.Value);
            }
            else
            {
                nums[4, 4] = null;
            }

            if (rollRateCheck4.Checked)
            {
                nums[4, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin4.Value), Convert.ToInt32(rollRateMax4.Value));
                nums[4, 5] = nums[4, 5] + randDecimal(nums[4, 5], rollRateMin4.Value, rollRateMax4.Value);
            }
            else
            {
                nums[4, 5] = null;
            }

            if (slideRateCheck4.Checked)
            {
                nums[4, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin4.Value), Convert.ToInt32(slideRateMax4.Value));
                nums[4, 6] = nums[4, 6] + randDecimal(nums[4, 6], slideRateMin4.Value, slideRateMax4.Value);
            }
            else
            {
                nums[4, 6] = null;
            }

            if (climbCheck4.Checked)
            {
                nums[4, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin4.Value), Convert.ToInt32(climbMax4.Value));
                nums[4, 7] = nums[4, 7] + randDecimal(nums[4, 7], climbMin4.Value, climbMax4.Value);
            }
            else
            {
                nums[4, 7] = null;
            }

            if (mLoadCheck4.Checked)
            {
                nums[4, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin4.Value), Convert.ToInt32(mLoadMax4.Value));
            }
            else
            {
                nums[4, 8] = null;
            }

            if (mYawRateCheck4.Checked)
            {
                nums[4, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin4.Value), Convert.ToInt32(mYawRateMax4.Value));
                nums[4, 9] = nums[4, 9] + randDecimal(nums[4, 9], mYawRateMin4.Value, mYawRateMax4.Value);
            }
            else
            {
                nums[4, 9] = null;
            }

            if (fuelRateCheck4.Checked)
            {
                nums[4, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin4.Value), Convert.ToInt32(fuelRateMax4.Value));
                nums[4, 10] = nums[4, 10] + randDecimal(nums[4, 10], fuelRateMin4.Value, fuelRateMax4.Value);
            }
            else
            {
                nums[4, 10] = null;
            }

            if (costCheck4.Checked)
            {
                nums[4, 11] = Globals.randMod.Next(Convert.ToInt32(costMin4.Value), Convert.ToInt32(costMax4.Value));
            }
            else
            {
                nums[4, 11] = null;
            }

            if (damageCheck4.Checked)
            {
                nums[4, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin4.Value), Convert.ToInt32(damageMax4.Value));
            }
            else
            {
                nums[4, 12] = null;
            }

            if (fuelCheck4.Checked)
            {
                nums[4, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin4.Value), Convert.ToInt32(fuelMax4.Value));
                nums[4, 13] = nums[4, 13] + randDecimal(nums[4, 13], fuelMin4.Value, fuelMax4.Value);
            }
            else
            {
                nums[4, 13] = null;
            }

            if (repairCheck4.Checked)
            {
                nums[4, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin4.Value), Convert.ToInt32(repairMax4.Value));
                nums[4, 14] = nums[4, 14] + randDecimal(nums[4, 14], repairMin4.Value, repairMax4.Value);
            }
            else
            {
                nums[4, 14] = null;
            }

            if (fuelCostCheck4.Checked)
            {
                nums[4, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin4.Value), Convert.ToInt32(fuelCostMax4.Value));
                nums[4, 15] = nums[4, 15] + randDecimal(nums[4, 15], fuelCostMin4.Value, fuelCostMax4.Value);
            }
            else
            {
                nums[4, 15] = null;
            }

            //Heli 5
            if (mBankCheck5.Checked)
            {
                nums[5, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin5.Value), Convert.ToInt32(mBankMax5.Value));
                nums[5, 0] = nums[5, 0] + randDecimal(nums[5, 0], mBankMin5.Value, mBankMax5.Value);
            }
            else
            {
                nums[5, 0] = null;
            }

            if (mSlideCheck5.Checked)
            {
                nums[5, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin5.Value), Convert.ToInt32(mSlideMax5.Value));
                nums[5, 1] = nums[5, 1] + randDecimal(nums[5, 1], mSlideMin5.Value, mSlideMax5.Value);
            }
            else
            {
                nums[5, 1] = null;
            }

            if (mPitchCheck5.Checked)
            {
                nums[5, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin5.Value), Convert.ToInt32(mPitchMax5.Value));
                nums[5, 2] = nums[5, 2] + randDecimal(nums[5, 2], mPitchMin5.Value, mPitchMax5.Value);
            }
            else
            {
                nums[5, 2] = null;
            }

            if (pitchRateCheck5.Checked)
            {
                nums[5, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin5.Value), Convert.ToInt32(pitchRateMax5.Value));
                nums[5, 3] = nums[5, 3] + randDecimal(nums[5, 3], pitchRateMin5.Value, pitchRateMax5.Value);
            }
            else
            {
                nums[5, 3] = null;
            }

            if (yawRateCheck5.Checked)
            {
                nums[5, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin5.Value), Convert.ToInt32(yawRateMax5.Value));
                nums[5, 4] = nums[5, 4] + randDecimal(nums[5, 4], yawRateMin5.Value, yawRateMax5.Value);
            }
            else
            {
                nums[5, 4] = null;
            }

            if (rollRateCheck5.Checked)
            {
                nums[5, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin5.Value), Convert.ToInt32(rollRateMax5.Value));
                nums[5, 5] = nums[5, 5] + randDecimal(nums[5, 5], rollRateMin5.Value, rollRateMax5.Value);
            }
            else
            {
                nums[5, 5] = null;
            }

            if (slideRateCheck5.Checked)
            {
                nums[5, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin5.Value), Convert.ToInt32(slideRateMax5.Value));
                nums[5, 6] = nums[5, 6] + randDecimal(nums[5, 6], slideRateMin5.Value, slideRateMax5.Value);
            }
            else
            {
                nums[5, 6] = null;
            }

            if (climbCheck5.Checked)
            {
                nums[5, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin5.Value), Convert.ToInt32(climbMax5.Value));
                nums[5, 7] = nums[5, 7] + randDecimal(nums[5, 7], climbMin5.Value, climbMax5.Value);
            }
            else
            {
                nums[5, 7] = null;
            }

            if (mLoadCheck5.Checked)
            {
                nums[5, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin5.Value), Convert.ToInt32(mLoadMax5.Value));
            }
            else
            {
                nums[5, 8] = null;
            }

            if (mYawRateCheck5.Checked)
            {
                nums[5, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin5.Value), Convert.ToInt32(mYawRateMax5.Value));
                nums[5, 9] = nums[5, 9] + randDecimal(nums[5, 9], mYawRateMin5.Value, mYawRateMax5.Value);
            }
            else
            {
                nums[5, 9] = null;
            }

            if (fuelRateCheck5.Checked)
            {
                nums[5, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin5.Value), Convert.ToInt32(fuelRateMax5.Value));
                nums[5, 10] = nums[5, 10] + randDecimal(nums[5, 10], fuelRateMin5.Value, fuelRateMax5.Value);
            }
            else
            {
                nums[5, 10] = null;
            }

            if (costCheck5.Checked)
            {
                nums[5, 11] = Globals.randMod.Next(Convert.ToInt32(costMin5.Value), Convert.ToInt32(costMax5.Value));
            }
            else
            {
                nums[5, 11] = null;
            }

            if (damageCheck5.Checked)
            {
                nums[5, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin5.Value), Convert.ToInt32(damageMax5.Value));
            }
            else
            {
                nums[5, 12] = null;
            }

            if (fuelCheck5.Checked)
            {
                nums[5, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin5.Value), Convert.ToInt32(fuelMax5.Value));
                nums[5, 13] = nums[5, 13] + randDecimal(nums[5, 13], fuelMin5.Value, fuelMax5.Value);
            }
            else
            {
                nums[5, 13] = null;
            }

            if (repairCheck5.Checked)
            {
                nums[5, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin5.Value), Convert.ToInt32(repairMax5.Value));
                nums[5, 14] = nums[5, 14] + randDecimal(nums[5, 14], repairMin5.Value, repairMax5.Value);
            }
            else
            {
                nums[5, 14] = null;
            }

            if (fuelCostCheck5.Checked)
            {
                nums[5, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin5.Value), Convert.ToInt32(fuelCostMax5.Value));
                nums[5, 15] = nums[5, 15] + randDecimal(nums[5, 15], fuelCostMin5.Value, fuelCostMax5.Value);
            }
            else
            {
                nums[5, 15] = null;
            }

            //Heli 6
            if (mBankCheck6.Checked)
            {
                nums[6, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin6.Value), Convert.ToInt32(mBankMax6.Value));
                nums[6, 0] = nums[6, 0] + randDecimal(nums[6, 0], mBankMin6.Value, mBankMax6.Value);
            }
            else
            {
                nums[6, 0] = null;
            }

            if (mSlideCheck6.Checked)
            {
                nums[6, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin6.Value), Convert.ToInt32(mSlideMax6.Value));
                nums[6, 1] = nums[6, 1] + randDecimal(nums[6, 1], mSlideMin6.Value, mSlideMax6.Value);
            }
            else
            {
                nums[6, 1] = null;
            }

            if (mPitchCheck6.Checked)
            {
                nums[6, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin6.Value), Convert.ToInt32(mPitchMax6.Value));
                nums[6, 2] = nums[6, 2] + randDecimal(nums[6, 2], mPitchMin6.Value, mPitchMax6.Value);
            }
            else
            {
                nums[6, 2] = null;
            }

            if (pitchRateCheck6.Checked)
            {
                nums[6, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin6.Value), Convert.ToInt32(pitchRateMax6.Value));
                nums[6, 3] = nums[6, 3] + randDecimal(nums[6, 3], pitchRateMin6.Value, pitchRateMax6.Value);
            }
            else
            {
                nums[6, 3] = null;
            }

            if (yawRateCheck6.Checked)
            {
                nums[6, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin6.Value), Convert.ToInt32(yawRateMax6.Value));
                nums[6, 4] = nums[6, 4] + randDecimal(nums[6, 4], yawRateMin6.Value, yawRateMax6.Value);
            }
            else
            {
                nums[6, 4] = null;
            }

            if (rollRateCheck6.Checked)
            {
                nums[6, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin6.Value), Convert.ToInt32(rollRateMax6.Value));
                nums[6, 5] = nums[6, 5] + randDecimal(nums[6, 5], rollRateMin6.Value, rollRateMax6.Value);
            }
            else
            {
                nums[6, 5] = null;
            }

            if (slideRateCheck6.Checked)
            {
                nums[6, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin6.Value), Convert.ToInt32(slideRateMax6.Value));
                nums[6, 6] = nums[6, 6] + randDecimal(nums[6, 6], slideRateMin6.Value, slideRateMax6.Value);
            }
            else
            {
                nums[6, 6] = null;
            }

            if (climbCheck6.Checked)
            {
                nums[6, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin6.Value), Convert.ToInt32(climbMax6.Value));
                nums[6, 7] = nums[6, 7] + randDecimal(nums[6, 7], climbMin6.Value, climbMax6.Value);
            }
            else
            {
                nums[6, 7] = null;
            }

            if (mLoadCheck6.Checked)
            {
                nums[6, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin6.Value), Convert.ToInt32(mLoadMax6.Value));
            }
            else
            {
                nums[6, 8] = null;
            }

            if (mYawRateCheck6.Checked)
            {
                nums[6, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin6.Value), Convert.ToInt32(mYawRateMax6.Value));
                nums[6, 9] = nums[6, 9] + randDecimal(nums[6, 9], mYawRateMin6.Value, mYawRateMax6.Value);
            }
            else
            {
                nums[6, 9] = null;
            }

            if (fuelRateCheck6.Checked)
            {
                nums[6, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin6.Value), Convert.ToInt32(fuelRateMax6.Value));
                nums[6, 10] = nums[6, 10] + randDecimal(nums[6, 10], fuelRateMin6.Value, fuelRateMax6.Value);
            }
            else
            {
                nums[6, 10] = null;
            }

            if (costCheck6.Checked)
            {
                nums[6, 11] = Globals.randMod.Next(Convert.ToInt32(costMin6.Value), Convert.ToInt32(costMax6.Value));
            }
            else
            {
                nums[6, 11] = null;
            }

            if (damageCheck6.Checked)
            {
                nums[6, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin6.Value), Convert.ToInt32(damageMax6.Value));
            }
            else
            {
                nums[6, 12] = null;
            }

            if (fuelCheck6.Checked)
            {
                nums[6, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin6.Value), Convert.ToInt32(fuelMax6.Value));
                nums[6, 13] = nums[6, 13] + randDecimal(nums[6, 13], fuelMin6.Value, fuelMax6.Value);
            }
            else
            {
                nums[6, 13] = null;
            }

            if (repairCheck6.Checked)
            {
                nums[6, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin6.Value), Convert.ToInt32(repairMax6.Value));
                nums[6, 14] = nums[6, 14] + randDecimal(nums[6, 14], repairMin6.Value, repairMax6.Value);
            }
            else
            {
                nums[6, 14] = null;
            }

            if (fuelCostCheck6.Checked)
            {
                nums[6, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin6.Value), Convert.ToInt32(fuelCostMax6.Value));
                nums[6, 15] = nums[6, 15] + randDecimal(nums[6, 15], fuelCostMin6.Value, fuelCostMax6.Value);
            }
            else
            {
                nums[6, 15] = null;
            }

            //Heli 7
            if (mBankCheck7.Checked)
            {
                nums[7, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin7.Value), Convert.ToInt32(mBankMax7.Value));
                nums[7, 0] = nums[7, 0] + randDecimal(nums[7, 0], mBankMin7.Value, mBankMax7.Value);
            }
            else
            {
                nums[7, 0] = null;
            }

            if (mSlideCheck7.Checked)
            {
                nums[7, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin7.Value), Convert.ToInt32(mSlideMax7.Value));
                nums[7, 1] = nums[7, 1] + randDecimal(nums[7, 1], mSlideMin7.Value, mSlideMax7.Value);
            }
            else
            {
                nums[7, 1] = null;
            }

            if (mPitchCheck7.Checked)
            {
                nums[7, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin7.Value), Convert.ToInt32(mPitchMax7.Value));
                nums[7, 2] = nums[7, 2] + randDecimal(nums[7, 2], mPitchMin7.Value, mPitchMax7.Value);
            }
            else
            {
                nums[7, 2] = null;
            }

            if (pitchRateCheck7.Checked)
            {
                nums[7, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin7.Value), Convert.ToInt32(pitchRateMax7.Value));
                nums[7, 3] = nums[7, 3] + randDecimal(nums[7, 3], pitchRateMin7.Value, pitchRateMax7.Value);
            }
            else
            {
                nums[7, 3] = null;
            }

            if (yawRateCheck7.Checked)
            {
                nums[7, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin7.Value), Convert.ToInt32(yawRateMax7.Value));
                nums[7, 4] = nums[7, 4] + randDecimal(nums[7, 4], yawRateMin7.Value, yawRateMax7.Value);
            }
            else
            {
                nums[7, 4] = null;
            }

            if (rollRateCheck7.Checked)
            {
                nums[7, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin7.Value), Convert.ToInt32(rollRateMax7.Value));
                nums[7, 5] = nums[7, 5] + randDecimal(nums[7, 5], rollRateMin7.Value, rollRateMax7.Value);
            }
            else
            {
                nums[7, 5] = null;
            }

            if (slideRateCheck7.Checked)
            {
                nums[7, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin7.Value), Convert.ToInt32(slideRateMax7.Value));
                nums[7, 6] = nums[7, 6] + randDecimal(nums[7, 6], slideRateMin7.Value, slideRateMax7.Value);
            }
            else
            {
                nums[7, 6] = null;
            }

            if (climbCheck7.Checked)
            {
                nums[7, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin7.Value), Convert.ToInt32(climbMax7.Value));
                nums[7, 7] = nums[7, 7] + randDecimal(nums[7, 7], climbMin7.Value, climbMax7.Value);
            }
            else
            {
                nums[7, 7] = null;
            }

            if (mLoadCheck7.Checked)
            {
                nums[7, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin7.Value), Convert.ToInt32(mLoadMax7.Value));
            }
            else
            {
                nums[7, 8] = null;
            }

            if (mYawRateCheck7.Checked)
            {
                nums[7, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin7.Value), Convert.ToInt32(mYawRateMax7.Value));
                nums[7, 9] = nums[7, 9] + randDecimal(nums[7, 9], mYawRateMin7.Value, mYawRateMax7.Value);
            }
            else
            {
                nums[7, 9] = null;
            }

            if (fuelRateCheck7.Checked)
            {
                nums[7, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin7.Value), Convert.ToInt32(fuelRateMax7.Value));
                nums[7, 10] = nums[7, 10] + randDecimal(nums[7, 10], fuelRateMin7.Value, fuelRateMax7.Value);
            }
            else
            {
                nums[7, 10] = null;
            }

            if (costCheck7.Checked)
            {
                nums[7, 11] = Globals.randMod.Next(Convert.ToInt32(costMin7.Value), Convert.ToInt32(costMax7.Value));
            }
            else
            {
                nums[7, 11] = null;
            }

            if (damageCheck7.Checked)
            {
                nums[7, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin7.Value), Convert.ToInt32(damageMax7.Value));
            }
            else
            {
                nums[7, 12] = null;
            }

            if (fuelCheck7.Checked)
            {
                nums[7, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin7.Value), Convert.ToInt32(fuelMax7.Value));
                nums[7, 13] = nums[7, 13] + randDecimal(nums[7, 13], fuelMin7.Value, fuelMax7.Value);
            }
            else
            {
                nums[7, 13] = null;
            }

            if (repairCheck7.Checked)
            {
                nums[7, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin7.Value), Convert.ToInt32(repairMax7.Value));
                nums[7, 14] = nums[7, 14] + randDecimal(nums[7, 14], repairMin7.Value, repairMax7.Value);
            }
            else
            {
                nums[7, 14] = null;
            }

            if (fuelCostCheck7.Checked)
            {
                nums[7, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin7.Value), Convert.ToInt32(fuelCostMax7.Value));
                nums[7, 15] = nums[7, 15] + randDecimal(nums[7, 15], fuelCostMin7.Value, fuelCostMax7.Value);
            }
            else
            {
                nums[7, 15] = null;
            }

            //Heli 8
            if (mBankCheck8.Checked)
            {
                nums[8, 0] = Globals.randMod.Next(Convert.ToInt32(mBankMin8.Value), Convert.ToInt32(mBankMax8.Value));
                nums[8, 0] = nums[8, 0] + randDecimal(nums[8, 0], mBankMin8.Value, mBankMax8.Value);
            }
            else
            {
                nums[8, 0] = null;
            }

            if (mSlideCheck8.Checked)
            {
                nums[8, 1] = Globals.randMod.Next(Convert.ToInt32(mSlideMin8.Value), Convert.ToInt32(mSlideMax8.Value));
                nums[8, 1] = nums[8, 1] + randDecimal(nums[8, 1], mSlideMin8.Value, mSlideMax8.Value);
            }
            else
            {
                nums[8, 1] = null;
            }

            if (mPitchCheck8.Checked)
            {
                nums[8, 2] = Globals.randMod.Next(Convert.ToInt32(mPitchMin8.Value), Convert.ToInt32(mPitchMax8.Value));
                nums[8, 2] = nums[8, 2] + randDecimal(nums[8, 2], mPitchMin8.Value, mPitchMax8.Value);
            }
            else
            {
                nums[8, 2] = null;
            }

            if (pitchRateCheck8.Checked)
            {
                nums[8, 3] = Globals.randMod.Next(Convert.ToInt32(pitchRateMin8.Value), Convert.ToInt32(pitchRateMax8.Value));
                nums[8, 3] = nums[8, 3] + randDecimal(nums[8, 3], pitchRateMin8.Value, pitchRateMax8.Value);
            }
            else
            {
                nums[8, 3] = null;
            }

            if (yawRateCheck8.Checked)
            {
                nums[8, 4] = Globals.randMod.Next(Convert.ToInt32(yawRateMin8.Value), Convert.ToInt32(yawRateMax8.Value));
                nums[8, 4] = nums[8, 4] + randDecimal(nums[8, 4], yawRateMin8.Value, yawRateMax8.Value);
            }
            else
            {
                nums[8, 4] = null;
            }

            if (rollRateCheck8.Checked)
            {
                nums[8, 5] = Globals.randMod.Next(Convert.ToInt32(rollRateMin8.Value), Convert.ToInt32(rollRateMax8.Value));
                nums[8, 5] = nums[8, 5] + randDecimal(nums[8, 5], rollRateMin8.Value, rollRateMax8.Value);
            }
            else
            {
                nums[8, 5] = null;
            }

            if (slideRateCheck8.Checked)
            {
                nums[8, 6] = Globals.randMod.Next(Convert.ToInt32(slideRateMin8.Value), Convert.ToInt32(slideRateMax8.Value));
                nums[8, 6] = nums[8, 6] + randDecimal(nums[8, 6], slideRateMin8.Value, slideRateMax8.Value);
            }
            else
            {
                nums[8, 6] = null;
            }

            if (climbCheck8.Checked)
            {
                nums[8, 7] = Globals.randMod.Next(Convert.ToInt32(climbMin8.Value), Convert.ToInt32(climbMax8.Value));
                nums[8, 7] = nums[8, 7] + randDecimal(nums[8, 7], climbMin8.Value, climbMax8.Value);
            }
            else
            {
                nums[8, 7] = null;
            }

            if (mLoadCheck8.Checked)
            {
                nums[8, 8] = Globals.randMod.Next(Convert.ToInt32(mLoadMin8.Value), Convert.ToInt32(mLoadMax8.Value));
            }
            else
            {
                nums[8, 8] = null;
            }

            if (mYawRateCheck8.Checked)
            {
                nums[8, 9] = Globals.randMod.Next(Convert.ToInt32(mYawRateMin8.Value), Convert.ToInt32(mYawRateMax8.Value));
                nums[8, 9] = nums[8, 9] + randDecimal(nums[8, 9], mYawRateMin8.Value, mYawRateMax8.Value);
            }
            else
            {
                nums[8, 9] = null;
            }

            if (fuelRateCheck8.Checked)
            {
                nums[8, 10] = Globals.randMod.Next(Convert.ToInt32(fuelRateMin8.Value), Convert.ToInt32(fuelRateMax8.Value));
                nums[8, 10] = nums[8, 10] + randDecimal(nums[8, 10], fuelRateMin8.Value, fuelRateMax8.Value);
            }
            else
            {
                nums[8, 10] = null;
            }

            if (costCheck8.Checked)
            {
                nums[8, 11] = Globals.randMod.Next(Convert.ToInt32(costMin8.Value), Convert.ToInt32(costMax8.Value));
            }
            else
            {
                nums[8, 11] = null;
            }

            if (damageCheck8.Checked)
            {
                nums[8, 12] = Globals.randMod.Next(Convert.ToInt32(damageMin8.Value), Convert.ToInt32(damageMax8.Value));
            }
            else
            {
                nums[8, 12] = null;
            }

            if (fuelCheck8.Checked)
            {
                nums[8, 13] = Globals.randMod.Next(Convert.ToInt32(fuelMin8.Value), Convert.ToInt32(fuelMax8.Value));
                nums[8, 13] = nums[8, 13] + randDecimal(nums[8, 13], fuelMin8.Value, fuelMax8.Value);
            }
            else
            {
                nums[8, 13] = null;
            }

            if (repairCheck8.Checked)
            {
                nums[8, 14] = Globals.randMod.Next(Convert.ToInt32(repairMin8.Value), Convert.ToInt32(repairMax8.Value));
                nums[8, 14] = nums[8, 14] + randDecimal(nums[8, 14], repairMin8.Value, repairMax8.Value);
            }
            else
            {
                nums[8, 14] = null;
            }

            if (fuelCostCheck8.Checked)
            {
                nums[8, 15] = Globals.randMod.Next(Convert.ToInt32(fuelCostMin8.Value), Convert.ToInt32(fuelCostMax8.Value));
                nums[8, 15] = nums[8, 15] + randDecimal(nums[8, 15], fuelCostMin8.Value, fuelCostMax8.Value);
            }
            else
            {
                nums[8, 15] = null;
            }

            //Landing
            if (landCheck0.Checked)
            {
                nums[9, 0] = Globals.randMod.Next(Convert.ToInt32(landMin0.Value), Convert.ToInt32(landMax0.Value));
                nums[9, 0] = nums[9, 0] + randDecimal(nums[9, 0], landMin0.Value, landMax0.Value);
            }
            else
            {
                nums[9, 0] = null;
            }

            if (landCheck1.Checked)
            {
                nums[9, 1] = Globals.randMod.Next(Convert.ToInt32(landMin1.Value), Convert.ToInt32(landMax1.Value));
                nums[9, 1] = nums[9, 1] + randDecimal(nums[9, 1], landMin1.Value, landMax1.Value);
            }
            else
            {
                nums[9, 1] = null;
            }

            if (landCheck2.Checked)
            {
                nums[9, 2] = Globals.randMod.Next(Convert.ToInt32(landMin2.Value), Convert.ToInt32(landMax2.Value));
                nums[9, 2] = nums[9, 2] + randDecimal(nums[9, 2], landMin2.Value, landMax2.Value);
            }
            else
            {
                nums[9, 2] = null;
            }

            if (landCheck3.Checked)
            {
                nums[9, 3] = Globals.randMod.Next(Convert.ToInt32(landMin3.Value), Convert.ToInt32(landMax3.Value));
                nums[9, 3] = nums[9, 3] + randDecimal(nums[9, 3], landMin3.Value, landMax3.Value);
            }
            else
            {
                nums[9, 3] = null;
            }

            if (landCheck4.Checked)
            {
                nums[9, 4] = Globals.randMod.Next(Convert.ToInt32(landMin4.Value), Convert.ToInt32(landMax4.Value));
                nums[9, 4] = nums[9, 4] + randDecimal(nums[9, 4], landMin4.Value, landMax4.Value);
            }
            else
            {
                nums[9, 4] = null;
            }

            //Ropestuff
            if (ropeCheck0.Checked)
            {
                nums[10, 0] = Globals.randMod.Next(Convert.ToInt32(ropeMin0.Value), Convert.ToInt32(ropeMax0.Value));
            }
            else
            {
                nums[10, 0] = null;
            }

            if (ropeCheck1.Checked)
            {
                nums[10, 1] = Globals.randMod.Next(Convert.ToInt32(ropeMin1.Value), Convert.ToInt32(ropeMax1.Value));
            }
            else
            {
                nums[10, 1] = null;
            }

            if (ropeCheck2.Checked)
            {
                nums[10, 2] = Globals.randMod.Next(Convert.ToInt32(ropeMin2.Value), Convert.ToInt32(ropeMax2.Value));
            }
            else
            {
                nums[10, 2] = null;
            }

            if (ropeCheck3.Checked)
            {
                nums[10, 3] = Globals.randMod.Next(Convert.ToInt32(ropeMin3.Value), Convert.ToInt32(ropeMax3.Value));
                nums[10, 3] = nums[10, 3] + randDecimal(nums[10, 3], ropeMin3.Value, ropeMax3.Value);
            }
            else
            {
                nums[10, 3] = null;
            }

            if (ropeCheck4.Checked)
            {
                nums[10, 4] = Globals.randMod.Next(Convert.ToInt32(ropeMin4.Value), Convert.ToInt32(ropeMax4.Value));
            }
            else
            {
                nums[10, 4] = null;
            }

            if (ropeCheck5.Checked)
            {
                nums[10, 5] = Globals.randMod.Next(Convert.ToInt32(ropeMin5.Value), Convert.ToInt32(ropeMax5.Value));
                nums[10, 5] = nums[10, 5] + randDecimal(nums[10, 5], ropeMin5.Value, ropeMax5.Value);
            }
            else
            {
                nums[10, 5] = null;
            }

            //Heli Damage
            if (dCheck0.Checked)
            {
                nums[11, 0] = Globals.randMod.Next(Convert.ToInt32(dMin0.Value), Convert.ToInt32(dMax0.Value));
                nums[11, 0] = nums[11, 0] + randDecimal(nums[11, 0], dMin0.Value, dMax0.Value);
            }
            else
            {
                nums[11, 0] = null;
            }

            if (dCheck1.Checked)
            {
                nums[11, 1] = Globals.randMod.Next(Convert.ToInt32(dMin1.Value), Convert.ToInt32(dMax1.Value));
                nums[11, 1] = nums[11, 1] + randDecimal(nums[11, 1], dMin1.Value, dMax1.Value);
            }
            else
            {
                nums[11, 1] = null;
            }

            if (dCheck2.Checked)
            {
                nums[11, 2] = Globals.randMod.Next(Convert.ToInt32(dMin2.Value), Convert.ToInt32(dMax2.Value));
                nums[11, 2] = nums[11, 2] + randDecimal(nums[11, 2], dMin2.Value, dMax2.Value);
            }
            else
            {
                nums[11, 2] = null;
            }

            if (dCheck3.Checked)
            {
                nums[11, 3] = Globals.randMod.Next(Convert.ToInt32(dMin3.Value), Convert.ToInt32(dMax3.Value));
                nums[11, 3] = nums[11, 3] + randDecimal(nums[11, 3], dMin3.Value, dMax3.Value);
            }
            else
            {
                nums[11, 3] = null;
            }

            if (dCheck4.Checked)
            {
                nums[11, 4] = Globals.randMod.Next(Convert.ToInt32(dMin4.Value), Convert.ToInt32(dMax4.Value));
                nums[11, 4] = nums[11, 4] + randDecimal(nums[11, 4], dMin4.Value, dMax4.Value);
            }
            else
            {
                nums[11, 4] = null;
            }

            if (dCheck5.Checked)
            {
                nums[11, 5] = Globals.randMod.Next(Convert.ToInt32(dMin5.Value), Convert.ToInt32(dMax5.Value));
                nums[11, 5] = nums[11, 5] + randDecimal(nums[11, 5], dMin5.Value, dMax5.Value);
            }
            else
            {
                nums[11, 5] = null;
            }

            return nums;
        }

        private void fireRand()
        {
            double?[] fNums = new double?[6];
            string line = null;
            int y = 0;

            if (fChaos.Checked)
            {
                fNums = fChaosNums();
            }
            if (fCustom.Checked)
            {
                fNums = fCustomizedums();
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
                                    line = "Ctrl" + y.ToString() + "_Value=" + Convert.ToDouble(fNums[y]).ToString("0");
                                }
                                else
                                {
                                    line = "Ctrl" + y.ToString() + "_Value=" + Convert.ToDouble(fNums[y]).ToString("0.0");
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

        private double?[] fChaosNums()
        {
            double?[] nums = new double?[6];

            //Douse Points
            nums[0] = Globals.randMod.Next(10, 128);
            nums[0] = nums[0] + Globals.randMod.NextDouble();
            //Douse Mult
            nums[1] = Globals.randMod.Next(1, 101);
            //TimeToLive
            nums[2] = Globals.randMod.Next(80, 1000);
            nums[2] = nums[2] + Globals.randMod.NextDouble();
            //SpreadInterval
            nums[3] = Globals.randMod.Next(18, 100);
            nums[3] = nums[3] + Globals.randMod.NextDouble();
            //SpreadProb
            nums[4] = Globals.randMod.Next(40, 1025);
            //Fire Radius
            nums[5] = Globals.randMod.Next(30, 256);
            nums[5] = nums[5] + Globals.randMod.NextDouble();

            return nums;
        }

        private double?[] fCustomizedums()
        {
            double?[] nums = new double?[6];

            //Douse Points
            if (fCheck0.Checked)
            {
                nums[0] = Globals.randMod.Next(Convert.ToInt32(fMin0.Value), Convert.ToInt32(fMax0.Value));
                nums[0] = nums[0] + randDecimal(nums[0], fMin0.Value, fMax0.Value);
            }
            else
            {
                nums[0] = null;
            }

            //Douse Mult
            if (fCheck1.Checked)
            {
                nums[1] = Globals.randMod.Next(Convert.ToInt32(fMin1.Value), Convert.ToInt32(fMax1.Value));
            }
            else
            {
                nums[1] = null;
            }

            //Time To Live
            if (fCheck2.Checked)
            {
                nums[2] = Globals.randMod.Next(Convert.ToInt32(fMin2.Value), Convert.ToInt32(fMax2.Value));
                nums[2] = nums[2] + randDecimal(nums[2], fMin2.Value, fMax2.Value);
            }
            else
            {
                nums[2] = null;
            }

            //Spread Interval
            if (fCheck3.Checked)
            {
                nums[3] = Globals.randMod.Next(Convert.ToInt32(fMin3.Value), Convert.ToInt32(fMax3.Value));
                nums[3] = nums[3] + randDecimal(nums[3], fMin3.Value, fMax3.Value);
            }
            else
            {
                nums[3] = null;
            }

            //Spread Probability
            if (fCheck4.Checked)
            {
                nums[4] = Globals.randMod.Next(Convert.ToInt32(fMin4.Value), Convert.ToInt32(fMax4.Value));
            }
            else
            {
                nums[4] = null;
            }

            //Fire Radius
            if (fCheck5.Checked)
            {
                nums[5] = Globals.randMod.Next(Convert.ToInt32(fMin5.Value), Convert.ToInt32(fMax5.Value));
                nums[5] = nums[5] + randDecimal(nums[5], fMin5.Value, fMax5.Value);
            }
            else
            {
                nums[5] = null;
            }

            return nums;
        }

        private void amssnRand()
        {
            int?[] aNums = new int?[2];
            string line = null;
            int y = 0;

            if (amssnChaos.Checked)
            {
                aNums = chaosAMssnNums();
            }
            if (amssnCustom.Checked)
            {
                aNums = customizedAMssnNums();
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
                                line = "Ctrl" + y.ToString() + "_Value=" + Convert.ToInt32(aNums[y]).ToString("0");
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

        private int?[] chaosAMssnNums()
        {
            int?[] nums = new int?[2];

            //Fire Scan
            nums[0] = Globals.randMod.Next(3, 11);
            //Road Scan
            nums[1] = Globals.randMod.Next(3, 11);

            return nums;
        }

        private int?[] customizedAMssnNums()
        {
            int?[] nums = new int?[2];

            //Fire Scan
            if (amssnCheck0.Checked)
            {
                nums[0] = Globals.randMod.Next(Convert.ToInt32(amssnMin0.Value), Convert.ToInt32(amssnMax0.Value));
            }
            else
            {
                nums[0] = null;
            }

            //Road Scan
            if (amssnCheck1.Checked)
            {
                nums[1] = Globals.randMod.Next(Convert.ToInt32(amssnMin1.Value), Convert.ToInt32(amssnMax1.Value));
            }
            else
            {
                nums[1] = null;
            }

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
                //Check File Hash
                using (FileStream st = File.OpenRead(Globals.exeFolder + "\\career.twk"))
                {
                    System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create();
                    if (bTS(hash.ComputeHash(st)).Contains("72f8052778037e2a7943258e29a812c29e02f81138dfb39e3a9d43f29b782988"))
                    {
                        z++;
                    }
                    hash.Dispose();
                    st.Close();
                }
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
                            if (line.Contains("ReadFile"))
                            {
                                line = "ReadFile=ALL";
                            }
                            if (line.Contains("ReadSection"))
                            {
                                line = "ReadSection=";
                            }
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
                //Check file Hash.
                using (FileStream st = File.OpenRead(Globals.exeFolder + "\\sim3d.twk"))
                {
                    System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create();
                    if (bTS(hash.ComputeHash(st)).Contains("fd576bddb084964d7ca2279c3b406c15f8ee82c59c4554efb64590013d86f719"))
                    {
                        z++;
                    }
                    hash.Dispose();
                    st.Close();
                }
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
                //Check File Hash.
                using (FileStream st = File.OpenRead(Globals.exeFolder + "\\heli.twk"))
                {
                    System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create();
                    if (bTS(hash.ComputeHash(st)).Contains("f0ddb81b86ba3ec55218be9ab8b213b6b279b679ade2330e6fc28a2c41c36d0b"))
                    {
                        z++;
                    }
                    hash.Dispose();
                    st.Close();
                }
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
                //Check File Hash
                using (FileStream st = File.OpenRead(Globals.exeFolder + "\\fire.twk"))
                {
                    System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create();
                    if (bTS(hash.ComputeHash(st)).Contains("2e0e722f36d389049110a22c0540a8a904c70aca24d986a424258fb78a29cf83"))
                    {
                        z++;
                    }
                    hash.Dispose();
                    st.Close();
                }
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
                //Check File Hash
                using (FileStream st = File.OpenRead(Globals.exeFolder + "\\automssn.twk"))
                {
                    System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create();
                    if (bTS(hash.ComputeHash(st)).Contains("ae1efb64438087b83fdd44f87ec8b7fea90164cefeaa946c61c2b3c93ec81838"))
                    {
                        z++;
                    }
                    hash.Dispose();
                    st.Close();
                }
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
                System.Windows.Forms.MessageBox.Show("One or more files missing or improperly written.");
            }
        }


        private string bTS(byte[] hash)
        {
            string hashst = "";
            foreach (byte b in hash) hashst += b.ToString("x2");
            return hashst;
        }

        private double? randDecimal(double? wholePart, decimal min, decimal max)
        {
            double? deci = new double?();

            if (Math.Truncate(min) == Math.Truncate(max))
            {
                deci = ((Globals.randMod.Next((10 * Convert.ToInt32((min - Math.Truncate(min)))), (10 * Convert.ToInt32((max - Math.Truncate(max)))) + 1)) / 10);
            }
            else
            {
                if (Convert.ToInt32(wholePart) == Math.Truncate(min))
                {
                    deci = ((Globals.randMod.Next(10 * Convert.ToInt32((min - Math.Truncate(min))), 10) / 10));
                }
                else if (Convert.ToInt32(wholePart) == Math.Truncate(max))
                {
                    deci = ((Globals.randMod.Next((10 * Convert.ToInt32((max - Math.Truncate(max)))) + 1)) / 10);
                }
                else
                {
                    deci = (Globals.randMod.Next(10)) / 10;
                }
            }
            return deci;
        }

        //UI Handlers
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Yossitaru/SimCopterRandomizer/wiki");
        }

        private void cCustom_CheckedChanged(object sender, EventArgs e)
        {
            cityTabs.Enabled = true;
            cCheckOffButt.Enabled = true;
            cCheckOnButt.Enabled = true;
        }

        private void cChaos_CheckedChanged(object sender, EventArgs e)
        {
            cityTabs.Enabled = false;
            cCheckOffButt.Enabled = false;
            cCheckOnButt.Enabled = false;
        }

        #region Career

        #region check

        #region difficulty check
        private void c0DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0DiffMin.Enabled = c0DiffCheck.Checked;
            c0DiffMax.Enabled = c0DiffCheck.Checked;
        }

        private void c1DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1DiffMin.Enabled = c1DiffCheck.Checked;
            c1DiffMax.Enabled = c1DiffCheck.Checked;
        }

        private void c2DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2DiffMin.Enabled = c2DiffCheck.Checked;
            c2DiffMax.Enabled = c2DiffCheck.Checked;
        }

        private void c3DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3DiffMin.Enabled = c3DiffCheck.Checked;
            c3DiffMax.Enabled = c3DiffCheck.Checked;
        }

        private void c4DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4DiffMin.Enabled = c4DiffCheck.Checked;
            c4DiffMax.Enabled = c4DiffCheck.Checked;
        }

        private void c5DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5DiffMin.Enabled = c5DiffCheck.Checked;
            c5DiffMax.Enabled = c5DiffCheck.Checked;
        }

        private void c6DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6DiffMin.Enabled = c6DiffCheck.Checked;
            c6DiffMax.Enabled = c6DiffCheck.Checked;
        }

        private void c7DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7DiffMin.Enabled = c7DiffCheck.Checked;
            c7DiffMax.Enabled = c7DiffCheck.Checked;
        }

        private void c8DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8DiffMin.Enabled = c8DiffCheck.Checked;
            c8DiffMax.Enabled = c8DiffCheck.Checked;
        }

        private void c9DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9DiffMin.Enabled = c9DiffCheck.Checked;
            c9DiffMax.Enabled = c9DiffCheck.Checked;
        }

        private void c10DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10DiffMin.Enabled = c10DiffCheck.Checked;
            c10DiffMax.Enabled = c10DiffCheck.Checked;
        }

        private void c11DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11DiffMin.Enabled = c11DiffCheck.Checked;
            c11DiffMax.Enabled = c11DiffCheck.Checked;
        }

        private void c12DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12DiffMin.Enabled = c12DiffCheck.Checked;
            c12DiffMax.Enabled = c12DiffCheck.Checked;
        }

        private void c13DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13DiffMin.Enabled = c13DiffCheck.Checked;
            c13DiffMax.Enabled = c13DiffCheck.Checked;
        }

        private void c14DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14DiffMin.Enabled = c14DiffCheck.Checked;
            c14DiffMax.Enabled = c14DiffCheck.Checked;
        }

        private void c15DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15DiffMin.Enabled = c15DiffCheck.Checked;
            c15DiffMax.Enabled = c15DiffCheck.Checked;
        }

        private void c16DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16DiffMin.Enabled = c16DiffCheck.Checked;
            c16DiffMax.Enabled = c16DiffCheck.Checked;
        }

        private void c17DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17DiffMin.Enabled = c17DiffCheck.Checked;
            c17DiffMax.Enabled = c17DiffCheck.Checked;
        }

        private void c18DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18DiffMin.Enabled = c18DiffCheck.Checked;
            c18DiffMax.Enabled = c18DiffCheck.Checked;
        }

        private void c19DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19DiffMin.Enabled = c19DiffCheck.Checked;
            c19DiffMax.Enabled = c19DiffCheck.Checked;
        }

        private void c20DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20DiffMin.Enabled = c20DiffCheck.Checked;
            c20DiffMax.Enabled = c20DiffCheck.Checked;
        }

        private void c21DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21DiffMin.Enabled = c21DiffCheck.Checked;
            c21DiffMax.Enabled = c21DiffCheck.Checked;
        }

        private void c22DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22DiffMin.Enabled = c22DiffCheck.Checked;
            c22DiffMax.Enabled = c22DiffCheck.Checked;
        }

        private void c23DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23DiffMin.Enabled = c23DiffCheck.Checked;
            c23DiffMax.Enabled = c23DiffCheck.Checked;
        }

        private void c24DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24DiffMin.Enabled = c24DiffCheck.Checked;
            c24DiffMax.Enabled = c24DiffCheck.Checked;
        }

        private void c25DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25DiffMin.Enabled = c25DiffCheck.Checked;
            c25DiffMax.Enabled = c25DiffCheck.Checked;
        }

        private void c26DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26DiffMin.Enabled = c26DiffCheck.Checked;
            c26DiffMax.Enabled = c26DiffCheck.Checked;
        }

        private void c27DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27DiffMin.Enabled = c27DiffCheck.Checked;
            c27DiffMax.Enabled = c27DiffCheck.Checked;
        }

        private void c28DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28DiffMin.Enabled = c28DiffCheck.Checked;
            c28DiffMax.Enabled = c28DiffCheck.Checked;
        }

        private void c29DiffCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29DiffMin.Enabled = c29DiffCheck.Checked;
            c29DiffMax.Enabled = c29DiffCheck.Checked;
        }
        #endregion

        #region fire check
        private void c0FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0FireMin.Enabled = c0FireCheck.Checked;
            c0FireMax.Enabled = c0FireCheck.Checked;
        }

        private void c1FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1FireMin.Enabled = c1FireCheck.Checked;
            c1FireMax.Enabled = c1FireCheck.Checked;
        }

        private void c2FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2FireMin.Enabled = c2FireCheck.Checked;
            c2FireMax.Enabled = c2FireCheck.Checked;
        }

        private void c3FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3FireMin.Enabled = c3FireCheck.Checked;
            c3FireMax.Enabled = c3FireCheck.Checked;
        }

        private void c4FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4FireMin.Enabled = c4FireCheck.Checked;
            c4FireMax.Enabled = c4FireCheck.Checked;
        }

        private void c5FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5FireMin.Enabled = c5FireCheck.Checked;
            c5FireMax.Enabled = c5FireCheck.Checked;
        }

        private void c6FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6FireMin.Enabled = c6FireCheck.Checked;
            c6FireMax.Enabled = c6FireCheck.Checked;
        }

        private void c7FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7FireMin.Enabled = c7FireCheck.Checked;
            c7FireMax.Enabled = c7FireCheck.Checked;
        }

        private void c8FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8FireMin.Enabled = c8FireCheck.Checked;
            c8FireMax.Enabled = c8FireCheck.Checked;
        }

        private void c9FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9FireMin.Enabled = c9FireCheck.Checked;
            c9FireMax.Enabled = c9FireCheck.Checked;
        }

        private void c10FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10FireMin.Enabled = c10FireCheck.Checked;
            c10FireMax.Enabled = c10FireCheck.Checked;
        }

        private void c11FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11FireMin.Enabled = c11FireCheck.Checked;
            c11FireMax.Enabled = c11FireCheck.Checked;
        }

        private void c12FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12FireMin.Enabled = c12FireCheck.Checked;
            c12FireMax.Enabled = c12FireCheck.Checked;
        }

        private void c13FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13FireMin.Enabled = c13FireCheck.Checked;
            c13FireMax.Enabled = c13FireCheck.Checked;
        }

        private void c14FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14FireMin.Enabled = c14FireCheck.Checked;
            c14FireMax.Enabled = c14FireCheck.Checked;
        }

        private void c15FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15FireMin.Enabled = c15FireCheck.Checked;
            c15FireMax.Enabled = c15FireCheck.Checked;
        }

        private void c16FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16FireMin.Enabled = c16FireCheck.Checked;
            c16FireMax.Enabled = c16FireCheck.Checked;
        }

        private void c17FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17FireMin.Enabled = c17FireCheck.Checked;
            c17FireMax.Enabled = c17FireCheck.Checked;
        }

        private void c18FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18FireMin.Enabled = c18FireCheck.Checked;
            c18FireMax.Enabled = c18FireCheck.Checked;
        }

        private void c19FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19FireMin.Enabled = c19FireCheck.Checked;
            c19FireMax.Enabled = c19FireCheck.Checked;
        }

        private void c20FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20FireMin.Enabled = c20FireCheck.Checked;
            c20FireMax.Enabled = c20FireCheck.Checked;
        }

        private void c21FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21FireMin.Enabled = c21FireCheck.Checked;
            c21FireMax.Enabled = c21FireCheck.Checked;
        }

        private void c22FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22FireMin.Enabled = c22FireCheck.Checked;
            c22FireMax.Enabled = c22FireCheck.Checked;
        }

        private void c23FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23FireMin.Enabled = c23FireCheck.Checked;
            c23FireMax.Enabled = c23FireCheck.Checked;
        }

        private void c24FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24FireMin.Enabled = c24FireCheck.Checked;
            c24FireMax.Enabled = c24FireCheck.Checked;
        }

        private void c25FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25FireMin.Enabled = c25FireCheck.Checked;
            c25FireMax.Enabled = c25FireCheck.Checked;
        }

        private void c26FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26FireMin.Enabled = c26FireCheck.Checked;
            c26FireMax.Enabled = c26FireCheck.Checked;
        }

        private void c27FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27FireMin.Enabled = c27FireCheck.Checked;
            c27FireMax.Enabled = c27FireCheck.Checked;
        }

        private void c28FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28FireMin.Enabled = c28FireCheck.Checked;
            c28FireMax.Enabled = c28FireCheck.Checked;
        }

        private void c29FireCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29FireMin.Enabled = c29FireCheck.Checked;
            c29FireMax.Enabled = c29FireCheck.Checked;
        }

        #endregion

        #region crime check
        private void c0CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0CrimeMin.Enabled = c0CrimeCheck.Checked;
            c0CrimeMax.Enabled = c0CrimeCheck.Checked;
        }

        private void c1CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1CrimeMin.Enabled = c1CrimeCheck.Checked;
            c1CrimeMax.Enabled = c1CrimeCheck.Checked;
        }

        private void c2CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2CrimeMin.Enabled = c2CrimeCheck.Checked;
            c2CrimeMax.Enabled = c2CrimeCheck.Checked;
        }

        private void c3CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3CrimeMin.Enabled = c3CrimeCheck.Checked;
            c3CrimeMax.Enabled = c3CrimeCheck.Checked;
        }

        private void c4CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4CrimeMin.Enabled = c4CrimeCheck.Checked;
            c4CrimeMax.Enabled = c4CrimeCheck.Checked;
        }

        private void c5CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5CrimeMin.Enabled = c5CrimeCheck.Checked;
            c5CrimeMax.Enabled = c5CrimeCheck.Checked;
        }

        private void c6CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6CrimeMin.Enabled = c6CrimeCheck.Checked;
            c6CrimeMax.Enabled = c6CrimeCheck.Checked;
        }

        private void c7CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7CrimeMin.Enabled = c7CrimeCheck.Checked;
            c7CrimeMax.Enabled = c7CrimeCheck.Checked;
        }

        private void c8CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8CrimeMin.Enabled = c8CrimeCheck.Checked;
            c8CrimeMax.Enabled = c8CrimeCheck.Checked;
        }

        private void c9CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9CrimeMin.Enabled = c9CrimeCheck.Checked;
            c9CrimeMax.Enabled = c9CrimeCheck.Checked;
        }

        private void c10CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10CrimeMin.Enabled = c10CrimeCheck.Checked;
            c10CrimeMax.Enabled = c10CrimeCheck.Checked;
        }

        private void c11CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11CrimeMin.Enabled = c11CrimeCheck.Checked;
            c11CrimeMax.Enabled = c11CrimeCheck.Checked;
        }

        private void c12CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12CrimeMin.Enabled = c12CrimeCheck.Checked;
            c12CrimeMax.Enabled = c12CrimeCheck.Checked;
        }

        private void c13CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13CrimeMin.Enabled = c13CrimeCheck.Checked;
            c13CrimeMax.Enabled = c13CrimeCheck.Checked;
        }

        private void c14CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14CrimeMin.Enabled = c14CrimeCheck.Checked;
            c14CrimeMax.Enabled = c14CrimeCheck.Checked;
        }

        private void c15CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15CrimeMin.Enabled = c15CrimeCheck.Checked;
            c15CrimeMax.Enabled = c15CrimeCheck.Checked;
        }

        private void c16CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16CrimeMin.Enabled = c16CrimeCheck.Checked;
            c16CrimeMax.Enabled = c16CrimeCheck.Checked;
        }

        private void c17CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17CrimeMin.Enabled = c17CrimeCheck.Checked;
            c17CrimeMax.Enabled = c17CrimeCheck.Checked;
        }

        private void c18CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18CrimeMin.Enabled = c18CrimeCheck.Checked;
            c18CrimeMax.Enabled = c18CrimeCheck.Checked;
        }

        private void c19CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19CrimeMin.Enabled = c19CrimeCheck.Checked;
            c19CrimeMax.Enabled = c19CrimeCheck.Checked;
        }

        private void c20CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20CrimeMin.Enabled = c20CrimeCheck.Checked;
            c20CrimeMax.Enabled = c20CrimeCheck.Checked;
        }

        private void c21CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21CrimeMin.Enabled = c21CrimeCheck.Checked;
            c21CrimeMax.Enabled = c21CrimeCheck.Checked;
        }

        private void c22CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22CrimeMin.Enabled = c22CrimeCheck.Checked;
            c22CrimeMax.Enabled = c22CrimeCheck.Checked;
        }

        private void c23CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23CrimeMin.Enabled = c23CrimeCheck.Checked;
            c23CrimeMax.Enabled = c23CrimeCheck.Checked;
        }

        private void c24CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24CrimeMin.Enabled = c24CrimeCheck.Checked;
            c24CrimeMax.Enabled = c24CrimeCheck.Checked;
        }

        private void c25CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25CrimeMin.Enabled = c25CrimeCheck.Checked;
            c25CrimeMax.Enabled = c25CrimeCheck.Checked;
        }

        private void c26CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26CrimeMin.Enabled = c26CrimeCheck.Checked;
            c26CrimeMax.Enabled = c26CrimeCheck.Checked;
        }

        private void c27CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27CrimeMin.Enabled = c27CrimeCheck.Checked;
            c27CrimeMax.Enabled = c27CrimeCheck.Checked;
        }

        private void c28CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28CrimeMin.Enabled = c28CrimeCheck.Checked;
            c28CrimeMax.Enabled = c28CrimeCheck.Checked;
        }

        private void c29CrimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29CrimeMin.Enabled = c29CrimeCheck.Checked;
            c29CrimeMax.Enabled = c29CrimeCheck.Checked;
        }
        #endregion

        #region rescue check
        private void c0RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0RescueMin.Enabled = c0RescueCheck.Checked;
            c0RescueMax.Enabled = c0RescueCheck.Checked;
        }

        private void c1RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1RescueMin.Enabled = c1RescueCheck.Checked;
            c1RescueMax.Enabled = c1RescueCheck.Checked;
        }

        private void c2RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2RescueMin.Enabled = c2RescueCheck.Checked;
            c2RescueMax.Enabled = c2RescueCheck.Checked;
        }

        private void c3RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3RescueMin.Enabled = c3RescueCheck.Checked;
            c3RescueMax.Enabled = c3RescueCheck.Checked;
        }

        private void c4RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4RescueMin.Enabled = c4RescueCheck.Checked;
            c4RescueMax.Enabled = c4RescueCheck.Checked;
        }

        private void c5RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5RescueMin.Enabled = c5RescueCheck.Checked;
            c5RescueMax.Enabled = c5RescueCheck.Checked;
        }

        private void c6RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6RescueMin.Enabled = c6RescueCheck.Checked;
            c6RescueMax.Enabled = c6RescueCheck.Checked;
        }

        private void c7RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7RescueMin.Enabled = c7RescueCheck.Checked;
            c7RescueMax.Enabled = c7RescueCheck.Checked;
        }

        private void c8RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8RescueMin.Enabled = c8RescueCheck.Checked;
            c8RescueMax.Enabled = c8RescueCheck.Checked;
        }

        private void c9RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9RescueMin.Enabled = c9RescueCheck.Checked;
            c9RescueMax.Enabled = c9RescueCheck.Checked;
        }

        private void c10RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10RescueMin.Enabled = c10RescueCheck.Checked;
            c10RescueMax.Enabled = c10RescueCheck.Checked;
        }

        private void c11RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11RescueMin.Enabled = c11RescueCheck.Checked;
            c11RescueMax.Enabled = c11RescueCheck.Checked;
        }

        private void c12RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12RescueMin.Enabled = c12RescueCheck.Checked;
            c12RescueMax.Enabled = c12RescueCheck.Checked;
        }

        private void c13RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13RescueMin.Enabled = c13RescueCheck.Checked;
            c13RescueMax.Enabled = c13RescueCheck.Checked;
        }

        private void c14RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14RescueMin.Enabled = c14RescueCheck.Checked;
            c14RescueMax.Enabled = c14RescueCheck.Checked;
        }

        private void c15RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15RescueMin.Enabled = c15RescueCheck.Checked;
            c15RescueMax.Enabled = c15RescueCheck.Checked;
        }

        private void c16RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16RescueMin.Enabled = c16RescueCheck.Checked;
            c16RescueMax.Enabled = c16RescueCheck.Checked;
        }

        private void c17RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17RescueMin.Enabled = c17RescueCheck.Checked;
            c17RescueMax.Enabled = c17RescueCheck.Checked;
        }

        private void c18RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18RescueMin.Enabled = c18RescueCheck.Checked;
            c18RescueMax.Enabled = c18RescueCheck.Checked;
        }

        private void c19RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19RescueMin.Enabled = c19RescueCheck.Checked;
            c19RescueMax.Enabled = c19RescueCheck.Checked;
        }

        private void c20RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20RescueMin.Enabled = c20RescueCheck.Checked;
            c20RescueMax.Enabled = c20RescueCheck.Checked;
        }

        private void c21RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21RescueMin.Enabled = c21RescueCheck.Checked;
            c21RescueMax.Enabled = c21RescueCheck.Checked;
        }

        private void c22RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22RescueMin.Enabled = c22RescueCheck.Checked;
            c22RescueMax.Enabled = c22RescueCheck.Checked;
        }

        private void c23RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23RescueMin.Enabled = c23RescueCheck.Checked;
            c23RescueMax.Enabled = c23RescueCheck.Checked;
        }

        private void c24RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24RescueMin.Enabled = c24RescueCheck.Checked;
            c24RescueMax.Enabled = c24RescueCheck.Checked;
        }

        private void c25RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25RescueMin.Enabled = c25RescueCheck.Checked;
            c25RescueMax.Enabled = c25RescueCheck.Checked;
        }

        private void c26RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26RescueMin.Enabled = c26RescueCheck.Checked;
            c26RescueMax.Enabled = c26RescueCheck.Checked;
        }

        private void c27RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27RescueMin.Enabled = c27RescueCheck.Checked;
            c27RescueMax.Enabled = c27RescueCheck.Checked;
        }

        private void c28RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28RescueMin.Enabled = c28RescueCheck.Checked;
            c28RescueMax.Enabled = c28RescueCheck.Checked;
        }

        private void c29RescueCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29RescueMin.Enabled = c29RescueCheck.Checked;
            c29RescueMax.Enabled = c29RescueCheck.Checked;
        }
        #endregion

        #region riot check
        private void c0RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0RiotMin.Enabled = c0RiotCheck.Checked;
            c0RiotMax.Enabled = c0RiotCheck.Checked;
        }

        private void c1RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1RiotMin.Enabled = c1RiotCheck.Checked;
            c1RiotMax.Enabled = c1RiotCheck.Checked;
        }

        private void c2RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2RiotMin.Enabled = c2RiotCheck.Checked;
            c2RiotMax.Enabled = c2RiotCheck.Checked;
        }

        private void c3RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3RiotMin.Enabled = c3RiotCheck.Checked;
            c3RiotMax.Enabled = c3RiotCheck.Checked;
        }

        private void c4RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4RiotMin.Enabled = c4RiotCheck.Checked;
            c4RiotMax.Enabled = c4RiotCheck.Checked;
        }

        private void c5RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5RiotMin.Enabled = c5RiotCheck.Checked;
            c5RiotMax.Enabled = c5RiotCheck.Checked;
        }

        private void c6RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6RiotMin.Enabled = c6RiotCheck.Checked;
            c6RiotMax.Enabled = c6RiotCheck.Checked;
        }

        private void c7RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7RiotMin.Enabled = c7RiotCheck.Checked;
            c7RiotMax.Enabled = c7RiotCheck.Checked;
        }

        private void c8RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8RiotMin.Enabled = c8RiotCheck.Checked;
            c8RiotMax.Enabled = c8RiotCheck.Checked;
        }

        private void c9RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9RiotMin.Enabled = c9RiotCheck.Checked;
            c9RiotMax.Enabled = c9RiotCheck.Checked;
        }

        private void c10RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10RiotMin.Enabled = c10RiotCheck.Checked;
            c10RiotMax.Enabled = c10RiotCheck.Checked;
        }

        private void c11RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11RiotMin.Enabled = c11RiotCheck.Checked;
            c11RiotMax.Enabled = c11RiotCheck.Checked;
        }

        private void c12RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12RiotMin.Enabled = c12RiotCheck.Checked;
            c12RiotMax.Enabled = c12RiotCheck.Checked;
        }

        private void c13RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13RiotMin.Enabled = c13RiotCheck.Checked;
            c13RiotMax.Enabled = c13RiotCheck.Checked;
        }

        private void c14RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14RiotMin.Enabled = c14RiotCheck.Checked;
            c14RiotMax.Enabled = c14RiotCheck.Checked;
        }

        private void c15RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15RiotMin.Enabled = c15RiotCheck.Checked;
            c15RiotMax.Enabled = c15RiotCheck.Checked;
        }

        private void c16RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16RiotMin.Enabled = c16RiotCheck.Checked;
            c16RiotMax.Enabled = c16RiotCheck.Checked;
        }

        private void c17RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17RiotMin.Enabled = c17RiotCheck.Checked;
            c17RiotMax.Enabled = c17RiotCheck.Checked;
        }

        private void c18RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18RiotMin.Enabled = c18RiotCheck.Checked;
            c18RiotMax.Enabled = c18RiotCheck.Checked;
        }

        private void c19RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19RiotMin.Enabled = c19RiotCheck.Checked;
            c19RiotMax.Enabled = c19RiotCheck.Checked;
        }

        private void c20RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20RiotMin.Enabled = c20RiotCheck.Checked;
            c20RiotMax.Enabled = c20RiotCheck.Checked;
        }

        private void c21RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21RiotMin.Enabled = c21RiotCheck.Checked;
            c21RiotMax.Enabled = c21RiotCheck.Checked;
        }

        private void c22RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22RiotMin.Enabled = c22RiotCheck.Checked;
            c22RiotMax.Enabled = c22RiotCheck.Checked;
        }

        private void c23RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23RiotMin.Enabled = c23RiotCheck.Checked;
            c23RiotMax.Enabled = c23RiotCheck.Checked;
        }

        private void c24RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24RiotMin.Enabled = c24RiotCheck.Checked;
            c24RiotMax.Enabled = c24RiotCheck.Checked;
        }

        private void c25RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25RiotMin.Enabled = c25RiotCheck.Checked;
            c25RiotMax.Enabled = c25RiotCheck.Checked;
        }

        private void c26RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26RiotMin.Enabled = c26RiotCheck.Checked;
            c26RiotMax.Enabled = c26RiotCheck.Checked;
        }

        private void c27RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27RiotMin.Enabled = c27RiotCheck.Checked;
            c27RiotMax.Enabled = c27RiotCheck.Checked;
        }

        private void c28RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28RiotMin.Enabled = c28RiotCheck.Checked;
            c28RiotMax.Enabled = c28RiotCheck.Checked;
        }

        private void c29RiotCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29RiotMin.Enabled = c29RiotCheck.Checked;
            c29RiotMax.Enabled = c29RiotCheck.Checked;
        }
        #endregion

        #region traffic check
        private void c0TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0TrafficMin.Enabled = c0TrafficCheck.Checked;
            c0TrafficMax.Enabled = c0TrafficCheck.Checked;
        }

        private void c1TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1TrafficMin.Enabled = c1TrafficCheck.Checked;
            c1TrafficMax.Enabled = c1TrafficCheck.Checked;
        }

        private void c2TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2TrafficMin.Enabled = c2TrafficCheck.Checked;
            c2TrafficMax.Enabled = c2TrafficCheck.Checked;
        }

        private void c3TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3TrafficMin.Enabled = c3TrafficCheck.Checked;
            c3TrafficMax.Enabled = c3TrafficCheck.Checked;
        }

        private void c4TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4TrafficMin.Enabled = c4TrafficCheck.Checked;
            c4TrafficMax.Enabled = c4TrafficCheck.Checked;
        }

        private void c5TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5TrafficMin.Enabled = c5TrafficCheck.Checked;
            c5TrafficMax.Enabled = c5TrafficCheck.Checked;
        }

        private void c6TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6TrafficMin.Enabled = c6TrafficCheck.Checked;
            c6TrafficMax.Enabled = c6TrafficCheck.Checked;
        }

        private void c7TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7TrafficMin.Enabled = c7TrafficCheck.Checked;
            c7TrafficMax.Enabled = c7TrafficCheck.Checked;
        }

        private void c8TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8TrafficMin.Enabled = c8TrafficCheck.Checked;
            c8TrafficMax.Enabled = c8TrafficCheck.Checked;
        }

        private void c9TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9TrafficMin.Enabled = c9TrafficCheck.Checked;
            c9TrafficMax.Enabled = c9TrafficCheck.Checked;
        }

        private void c10TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10TrafficMin.Enabled = c10TrafficCheck.Checked;
            c10TrafficMax.Enabled = c10TrafficCheck.Checked;
        }

        private void c11TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11TrafficMin.Enabled = c11TrafficCheck.Checked;
            c11TrafficMax.Enabled = c11TrafficCheck.Checked;
        }

        private void c12TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12TrafficMin.Enabled = c12TrafficCheck.Checked;
            c12TrafficMax.Enabled = c12TrafficCheck.Checked;
        }

        private void c13TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13TrafficMin.Enabled = c13TrafficCheck.Checked;
            c13TrafficMax.Enabled = c13TrafficCheck.Checked;
        }

        private void c14TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14TrafficMin.Enabled = c14TrafficCheck.Checked;
            c14TrafficMax.Enabled = c14TrafficCheck.Checked;
        }

        private void c15TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15TrafficMin.Enabled = c15TrafficCheck.Checked;
            c15TrafficMax.Enabled = c15TrafficCheck.Checked;
        }

        private void c16TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16TrafficMin.Enabled = c16TrafficCheck.Checked;
            c16TrafficMax.Enabled = c16TrafficCheck.Checked;
        }

        private void c17TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17TrafficMin.Enabled = c17TrafficCheck.Checked;
            c17TrafficMax.Enabled = c17TrafficCheck.Checked;
        }

        private void c18TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18TrafficMin.Enabled = c18TrafficCheck.Checked;
            c18TrafficMax.Enabled = c18TrafficCheck.Checked;
        }

        private void c19TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19TrafficMin.Enabled = c19TrafficCheck.Checked;
            c19TrafficMax.Enabled = c19TrafficCheck.Checked;
        }

        private void c20TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20TrafficMin.Enabled = c20TrafficCheck.Checked;
            c20TrafficMax.Enabled = c20TrafficCheck.Checked;
        }

        private void c21TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21TrafficMin.Enabled = c21TrafficCheck.Checked;
            c21TrafficMax.Enabled = c21TrafficCheck.Checked;
        }

        private void c22TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22TrafficMin.Enabled = c22TrafficCheck.Checked;
            c22TrafficMax.Enabled = c22TrafficCheck.Checked;
        }

        private void c23TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23TrafficMin.Enabled = c23TrafficCheck.Checked;
            c23TrafficMax.Enabled = c23TrafficCheck.Checked;
        }

        private void c24TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24TrafficMin.Enabled = c24TrafficCheck.Checked;
            c24TrafficMax.Enabled = c24TrafficCheck.Checked;
        }

        private void c25TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25TrafficMin.Enabled = c25TrafficCheck.Checked;
            c25TrafficMax.Enabled = c25TrafficCheck.Checked;
        }

        private void c26TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26TrafficMin.Enabled = c26TrafficCheck.Checked;
            c26TrafficMax.Enabled = c26TrafficCheck.Checked;
        }

        private void c27TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27TrafficMin.Enabled = c27TrafficCheck.Checked;
            c27TrafficMax.Enabled = c27TrafficCheck.Checked;
        }

        private void c28TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28TrafficMin.Enabled = c28TrafficCheck.Checked;
            c28TrafficMax.Enabled = c28TrafficCheck.Checked;
        }

        private void c29TrafficCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29TrafficMin.Enabled = c29TrafficCheck.Checked;
            c29TrafficMax.Enabled = c29TrafficCheck.Checked;
        }
        #endregion

        #region medevac check
        private void c0MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0MedevacMin.Enabled = c0MedevacCheck.Checked;
            c0MedevacMax.Enabled = c0MedevacCheck.Checked;
        }

        private void c1MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1MedevacMin.Enabled = c1MedevacCheck.Checked;
            c1MedevacMax.Enabled = c1MedevacCheck.Checked;
        }

        private void c2MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2MedevacMin.Enabled = c2MedevacCheck.Checked;
            c2MedevacMax.Enabled = c2MedevacCheck.Checked;
        }

        private void c3MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3MedevacMin.Enabled = c3MedevacCheck.Checked;
            c3MedevacMax.Enabled = c3MedevacCheck.Checked;
        }

        private void c4MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4MedevacMin.Enabled = c4MedevacCheck.Checked;
            c4MedevacMax.Enabled = c4MedevacCheck.Checked;
        }

        private void c5MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5MedevacMin.Enabled = c5MedevacCheck.Checked;
            c5MedevacMax.Enabled = c5MedevacCheck.Checked;
        }

        private void c6MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6MedevacMin.Enabled = c6MedevacCheck.Checked;
            c6MedevacMax.Enabled = c6MedevacCheck.Checked;
        }

        private void c7MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7MedevacMin.Enabled = c7MedevacCheck.Checked;
            c7MedevacMax.Enabled = c7MedevacCheck.Checked;
        }

        private void c8MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8MedevacMin.Enabled = c8MedevacCheck.Checked;
            c8MedevacMax.Enabled = c8MedevacCheck.Checked;
        }

        private void c9MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9MedevacMin.Enabled = c9MedevacCheck.Checked;
            c9MedevacMax.Enabled = c9MedevacCheck.Checked;
        }

        private void c10MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10MedevacMin.Enabled = c10MedevacCheck.Checked;
            c10MedevacMax.Enabled = c10MedevacCheck.Checked;
        }

        private void c11MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11MedevacMin.Enabled = c11MedevacCheck.Checked;
            c11MedevacMax.Enabled = c11MedevacCheck.Checked;
        }

        private void c12MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12MedevacMin.Enabled = c12MedevacCheck.Checked;
            c12MedevacMax.Enabled = c12MedevacCheck.Checked;
        }

        private void c13MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13MedevacMin.Enabled = c13MedevacCheck.Checked;
            c13MedevacMax.Enabled = c13MedevacCheck.Checked;
        }

        private void c14MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14MedevacMin.Enabled = c14MedevacCheck.Checked;
            c14MedevacMax.Enabled = c14MedevacCheck.Checked;
        }

        private void c15MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15MedevacMin.Enabled = c15MedevacCheck.Checked;
            c15MedevacMax.Enabled = c15MedevacCheck.Checked;
        }

        private void c16MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16MedevacMin.Enabled = c16MedevacCheck.Checked;
            c16MedevacMax.Enabled = c16MedevacCheck.Checked;
        }

        private void c17MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17MedevacMin.Enabled = c17MedevacCheck.Checked;
            c17MedevacMax.Enabled = c17MedevacCheck.Checked;
        }

        private void c18MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18MedevacMin.Enabled = c18MedevacCheck.Checked;
            c18MedevacMax.Enabled = c18MedevacCheck.Checked;
        }

        private void c19MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19MedevacMin.Enabled = c19MedevacCheck.Checked;
            c19MedevacMax.Enabled = c19MedevacCheck.Checked;
        }

        private void c20MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20MedevacMin.Enabled = c20MedevacCheck.Checked;
            c20MedevacMax.Enabled = c20MedevacCheck.Checked;
        }

        private void c21MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21MedevacMin.Enabled = c21MedevacCheck.Checked;
            c21MedevacMax.Enabled = c21MedevacCheck.Checked;
        }

        private void c22MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22MedevacMin.Enabled = c22MedevacCheck.Checked;
            c22MedevacMax.Enabled = c22MedevacCheck.Checked;
        }

        private void c23MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23MedevacMin.Enabled = c23MedevacCheck.Checked;
            c23MedevacMax.Enabled = c23MedevacCheck.Checked;
        }

        private void c24MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24MedevacMin.Enabled = c24MedevacCheck.Checked;
            c24MedevacMax.Enabled = c24MedevacCheck.Checked;
        }

        private void c25MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25MedevacMin.Enabled = c25MedevacCheck.Checked;
            c25MedevacMax.Enabled = c25MedevacCheck.Checked;
        }

        private void c26MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26MedevacMin.Enabled = c26MedevacCheck.Checked;
            c26MedevacMax.Enabled = c26MedevacCheck.Checked;
        }

        private void c27MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27MedevacMin.Enabled = c27MedevacCheck.Checked;
            c27MedevacMax.Enabled = c27MedevacCheck.Checked;
        }

        private void c28MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28MedevacMin.Enabled = c28MedevacCheck.Checked;
            c28MedevacMax.Enabled = c28MedevacCheck.Checked;
        }

        private void c29MedevacCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29MedevacMin.Enabled = c29MedevacCheck.Checked;
            c29MedevacMax.Enabled = c29MedevacCheck.Checked;
        }
        #endregion

        #region transport check
        private void c0TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0TransportMin.Enabled = c0TransportCheck.Checked;
            c0TransportMax.Enabled = c0TransportCheck.Checked;
        }

        private void c1TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1TransportMin.Enabled = c1TransportCheck.Checked;
            c1TransportMax.Enabled = c1TransportCheck.Checked;
        }

        private void c2TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2TransportMin.Enabled = c2TransportCheck.Checked;
            c2TransportMax.Enabled = c2TransportCheck.Checked;
        }

        private void c3TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3TransportMin.Enabled = c3TransportCheck.Checked;
            c3TransportMax.Enabled = c3TransportCheck.Checked;
        }

        private void c4TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4TransportMin.Enabled = c4TransportCheck.Checked;
            c4TransportMax.Enabled = c4TransportCheck.Checked;
        }

        private void c5TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5TransportMin.Enabled = c5TransportCheck.Checked;
            c5TransportMax.Enabled = c5TransportCheck.Checked;
        }

        private void c6TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6TransportMin.Enabled = c6TransportCheck.Checked;
            c6TransportMax.Enabled = c6TransportCheck.Checked;
        }

        private void c7TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7TransportMin.Enabled = c7TransportCheck.Checked;
            c7TransportMax.Enabled = c7TransportCheck.Checked;
        }

        private void c8TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8TransportMin.Enabled = c8TransportCheck.Checked;
            c8TransportMax.Enabled = c8TransportCheck.Checked;
        }

        private void c9TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9TransportMin.Enabled = c9TransportCheck.Checked;
            c9TransportMax.Enabled = c9TransportCheck.Checked;
        }

        private void c10TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10TransportMin.Enabled = c10TransportCheck.Checked;
            c10TransportMax.Enabled = c10TransportCheck.Checked;
        }

        private void c11TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11TransportMin.Enabled = c11TransportCheck.Checked;
            c11TransportMax.Enabled = c11TransportCheck.Checked;
        }

        private void c12TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12TransportMin.Enabled = c12TransportCheck.Checked;
            c12TransportMax.Enabled = c12TransportCheck.Checked;
        }

        private void c13TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13TransportMin.Enabled = c13TransportCheck.Checked;
            c13TransportMax.Enabled = c13TransportCheck.Checked;
        }

        private void c14TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14TransportMin.Enabled = c14TransportCheck.Checked;
            c14TransportMax.Enabled = c14TransportCheck.Checked;
        }

        private void c15TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15TransportMin.Enabled = c15TransportCheck.Checked;
            c15TransportMax.Enabled = c15TransportCheck.Checked;
        }

        private void c16TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16TransportMin.Enabled = c16TransportCheck.Checked;
            c16TransportMax.Enabled = c16TransportCheck.Checked;
        }

        private void c17TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17TransportMin.Enabled = c17TransportCheck.Checked;
            c17TransportMax.Enabled = c17TransportCheck.Checked;
        }

        private void c18TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18TransportMin.Enabled = c18TransportCheck.Checked;
            c18TransportMax.Enabled = c18TransportCheck.Checked;
        }

        private void c19TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19TransportMin.Enabled = c19TransportCheck.Checked;
            c19TransportMax.Enabled = c19TransportCheck.Checked;
        }

        private void c20TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20TransportMin.Enabled = c20TransportCheck.Checked;
            c20TransportMax.Enabled = c20TransportCheck.Checked;
        }

        private void c21TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21TransportMin.Enabled = c21TransportCheck.Checked;
            c21TransportMax.Enabled = c21TransportCheck.Checked;
        }

        private void c22TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22TransportMin.Enabled = c22TransportCheck.Checked;
            c22TransportMax.Enabled = c22TransportCheck.Checked;
        }

        private void c23TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23TransportMin.Enabled = c23TransportCheck.Checked;
            c23TransportMax.Enabled = c23TransportCheck.Checked;
        }

        private void c24TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24TransportMin.Enabled = c24TransportCheck.Checked;
            c24TransportMax.Enabled = c24TransportCheck.Checked;
        }

        private void c25TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25TransportMin.Enabled = c25TransportCheck.Checked;
            c25TransportMax.Enabled = c25TransportCheck.Checked;
        }

        private void c26TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26TransportMin.Enabled = c26TransportCheck.Checked;
            c26TransportMax.Enabled = c26TransportCheck.Checked;
        }

        private void c27TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27TransportMin.Enabled = c27TransportCheck.Checked;
            c27TransportMax.Enabled = c27TransportCheck.Checked;
        }

        private void c28TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28TransportMin.Enabled = c28TransportCheck.Checked;
            c28TransportMax.Enabled = c28TransportCheck.Checked;
        }

        private void c29TransportCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29TransportMin.Enabled = c29TransportCheck.Checked;
            c29TransportMax.Enabled = c29TransportCheck.Checked;
        }
        #endregion

        #region day check
        private void c0DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0DayMin.Enabled = c0DayCheck.Checked;
            c0DayMax.Enabled = c0DayCheck.Checked;
        }

        private void c1DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1DayMin.Enabled = c1DayCheck.Checked;
            c1DayMax.Enabled = c1DayCheck.Checked;
        }

        private void c2DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2DayMin.Enabled = c2DayCheck.Checked;
            c2DayMax.Enabled = c2DayCheck.Checked;
        }

        private void c3DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3DayMin.Enabled = c3DayCheck.Checked;
            c3DayMax.Enabled = c3DayCheck.Checked;
        }

        private void c4DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4DayMin.Enabled = c4DayCheck.Checked;
            c4DayMax.Enabled = c4DayCheck.Checked;
        }

        private void c5DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5DayMin.Enabled = c5DayCheck.Checked;
            c5DayMax.Enabled = c5DayCheck.Checked;
        }

        private void c6DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6DayMin.Enabled = c6DayCheck.Checked;
            c6DayMax.Enabled = c6DayCheck.Checked;
        }

        private void c7DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7DayMin.Enabled = c7DayCheck.Checked;
            c7DayMax.Enabled = c7DayCheck.Checked;
        }

        private void c8DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8DayMin.Enabled = c8DayCheck.Checked;
            c8DayMax.Enabled = c8DayCheck.Checked;
        }

        private void c9DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9DayMin.Enabled = c9DayCheck.Checked;
            c9DayMax.Enabled = c9DayCheck.Checked;
        }

        private void c10DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10DayMin.Enabled = c10DayCheck.Checked;
            c10DayMax.Enabled = c10DayCheck.Checked;
        }

        private void c11DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11DayMin.Enabled = c11DayCheck.Checked;
            c11DayMax.Enabled = c11DayCheck.Checked;
        }

        private void c12DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12DayMin.Enabled = c12DayCheck.Checked;
            c12DayMax.Enabled = c12DayCheck.Checked;
        }

        private void c13DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13DayMin.Enabled = c13DayCheck.Checked;
            c13DayMax.Enabled = c13DayCheck.Checked;
        }

        private void c14DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14DayMin.Enabled = c14DayCheck.Checked;
            c14DayMax.Enabled = c14DayCheck.Checked;
        }

        private void c15DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15DayMin.Enabled = c15DayCheck.Checked;
            c15DayMax.Enabled = c15DayCheck.Checked;
        }

        private void c16DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16DayMin.Enabled = c16DayCheck.Checked;
            c16DayMax.Enabled = c16DayCheck.Checked;
        }

        private void c17DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17DayMin.Enabled = c17DayCheck.Checked;
            c17DayMax.Enabled = c17DayCheck.Checked;
        }

        private void c18DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18DayMin.Enabled = c18DayCheck.Checked;
            c18DayMax.Enabled = c18DayCheck.Checked;
        }

        private void c19DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19DayMin.Enabled = c19DayCheck.Checked;
            c19DayMax.Enabled = c19DayCheck.Checked;
        }

        private void c20DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20DayMin.Enabled = c20DayCheck.Checked;
            c20DayMax.Enabled = c20DayCheck.Checked;
        }

        private void c21DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21DayMin.Enabled = c21DayCheck.Checked;
            c21DayMax.Enabled = c21DayCheck.Checked;
        }

        private void c22DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22DayMin.Enabled = c22DayCheck.Checked;
            c22DayMax.Enabled = c22DayCheck.Checked;
        }

        private void c23DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23DayMin.Enabled = c23DayCheck.Checked;
            c23DayMax.Enabled = c23DayCheck.Checked;
        }

        private void c24DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24DayMin.Enabled = c24DayCheck.Checked;
            c24DayMax.Enabled = c24DayCheck.Checked;
        }

        private void c25DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25DayMin.Enabled = c25DayCheck.Checked;
            c25DayMax.Enabled = c25DayCheck.Checked;
        }

        private void c26DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26DayMin.Enabled = c26DayCheck.Checked;
            c26DayMax.Enabled = c26DayCheck.Checked;
        }

        private void c27DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27DayMin.Enabled = c27DayCheck.Checked;
            c27DayMax.Enabled = c27DayCheck.Checked;
        }

        private void c28DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28DayMin.Enabled = c28DayCheck.Checked;
            c28DayMax.Enabled = c28DayCheck.Checked;
        }

        private void c29DayCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29DayMin.Enabled = c29DayCheck.Checked;
            c29DayMax.Enabled = c29DayCheck.Checked;
        }
        #endregion

        #region points check
        private void c0PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0PointsMin.Enabled = c0PointCheck.Checked;
            c0PointsMax.Enabled = c0PointCheck.Checked;
        }

        private void c1PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1PointsMin.Enabled = c1PointCheck.Checked;
            c1PointsMax.Enabled = c1PointCheck.Checked;
        }

        private void c2PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2PointsMin.Enabled = c2PointCheck.Checked;
            c2PointsMax.Enabled = c2PointCheck.Checked;
        }

        private void c3PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3PointsMin.Enabled = c3PointCheck.Checked;
            c3PointsMax.Enabled = c3PointCheck.Checked;
        }

        private void c4PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4PointsMin.Enabled = c4PointCheck.Checked;
            c4PointsMax.Enabled = c4PointCheck.Checked;
        }

        private void c5PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5PointsMin.Enabled = c5PointCheck.Checked;
            c5PointsMax.Enabled = c5PointCheck.Checked;
        }

        private void c6PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6PointsMin.Enabled = c6PointCheck.Checked;
            c6PointsMax.Enabled = c6PointCheck.Checked;
        }

        private void c7PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7PointsMin.Enabled = c7PointCheck.Checked;
            c7PointsMax.Enabled = c7PointCheck.Checked;
        }

        private void c8PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8PointsMin.Enabled = c8PointCheck.Checked;
            c8PointsMax.Enabled = c8PointCheck.Checked;
        }

        private void c9PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9PointsMin.Enabled = c9PointCheck.Checked;
            c9PointsMax.Enabled = c9PointCheck.Checked;
        }

        private void c10PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10PointsMin.Enabled = c10PointCheck.Checked;
            c10PointsMax.Enabled = c10PointCheck.Checked;
        }

        private void c11PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11PointsMin.Enabled = c11PointCheck.Checked;
            c11PointsMax.Enabled = c11PointCheck.Checked;
        }

        private void c12PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12PointsMin.Enabled = c12PointCheck.Checked;
            c12PointsMax.Enabled = c12PointCheck.Checked;
        }

        private void c13PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13PointsMin.Enabled = c13PointCheck.Checked;
            c13PointsMax.Enabled = c13PointCheck.Checked;
        }

        private void c14PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14PointsMin.Enabled = c14PointCheck.Checked;
            c14PointsMax.Enabled = c14PointCheck.Checked;
        }

        private void c15PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15PointsMin.Enabled = c15PointCheck.Checked;
            c15PointsMax.Enabled = c15PointCheck.Checked;
        }

        private void c16PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16PointsMin.Enabled = c16PointCheck.Checked;
            c16PointsMax.Enabled = c16PointCheck.Checked;
        }

        private void c17PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17PointsMin.Enabled = c17PointCheck.Checked;
            c17PointsMax.Enabled = c17PointCheck.Checked;
        }

        private void c18PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18PointsMin.Enabled = c18PointCheck.Checked;
            c18PointsMax.Enabled = c18PointCheck.Checked;
        }

        private void c19PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19PointsMin.Enabled = c19PointCheck.Checked;
            c19PointsMax.Enabled = c19PointCheck.Checked;
        }

        private void c20PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20PointsMin.Enabled = c20PointCheck.Checked;
            c20PointsMax.Enabled = c20PointCheck.Checked;
        }

        private void c21PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21PointsMin.Enabled = c21PointCheck.Checked;
            c21PointsMax.Enabled = c21PointCheck.Checked;
        }

        private void c22PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22PointsMin.Enabled = c22PointCheck.Checked;
            c22PointsMax.Enabled = c22PointCheck.Checked;
        }

        private void c23PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23PointsMin.Enabled = c23PointCheck.Checked;
            c23PointsMax.Enabled = c23PointCheck.Checked;
        }

        private void c24PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24PointsMin.Enabled = c24PointCheck.Checked;
            c24PointsMax.Enabled = c24PointCheck.Checked;
        }

        private void c25PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25PointsMin.Enabled = c25PointCheck.Checked;
            c25PointsMax.Enabled = c25PointCheck.Checked;
        }

        private void c26PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26PointsMin.Enabled = c26PointCheck.Checked;
            c26PointsMax.Enabled = c26PointCheck.Checked;
        }

        private void c27PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27PointsMin.Enabled = c27PointCheck.Checked;
            c27PointsMax.Enabled = c27PointCheck.Checked;
        }

        private void c28PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28PointsMin.Enabled = c28PointCheck.Checked;
            c28PointsMax.Enabled = c28PointCheck.Checked;
        }

        private void c29PointCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29PointsMin.Enabled = c29PointCheck.Checked;
            c29PointsMax.Enabled = c29PointCheck.Checked;
        }
        #endregion

        #region money check
        private void c0MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c0MoneyMin.Enabled = c0MoneyCheck.Checked;
            c0MoneyMax.Enabled = c0MoneyCheck.Checked;
        }

        private void c1MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c1MoneyMin.Enabled = c1MoneyCheck.Checked;
            c1MoneyMax.Enabled = c1MoneyCheck.Checked;
        }

        private void c2MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c2MoneyMin.Enabled = c2MoneyCheck.Checked;
            c2MoneyMax.Enabled = c2MoneyCheck.Checked;
        }

        private void c3MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c3MoneyMin.Enabled = c3MoneyCheck.Checked;
            c3MoneyMax.Enabled = c3MoneyCheck.Checked;
        }

        private void c4MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c4MoneyMin.Enabled = c4MoneyCheck.Checked;
            c4MoneyMax.Enabled = c4MoneyCheck.Checked;
        }

        private void c5MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c5MoneyMin.Enabled = c5MoneyCheck.Checked;
            c5MoneyMax.Enabled = c5MoneyCheck.Checked;
        }

        private void c6MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c6MoneyMin.Enabled = c6MoneyCheck.Checked;
            c6MoneyMax.Enabled = c6MoneyCheck.Checked;
        }

        private void c7MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c7MoneyMin.Enabled = c7MoneyCheck.Checked;
            c7MoneyMax.Enabled = c7MoneyCheck.Checked;
        }

        private void c8MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c8MoneyMin.Enabled = c8MoneyCheck.Checked;
            c8MoneyMax.Enabled = c8MoneyCheck.Checked;
        }

        private void c9MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c9MoneyMin.Enabled = c9MoneyCheck.Checked;
            c9MoneyMax.Enabled = c9MoneyCheck.Checked;
        }

        private void c10MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c10MoneyMin.Enabled = c10MoneyCheck.Checked;
            c10MoneyMax.Enabled = c10MoneyCheck.Checked;
        }

        private void c11MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c11MoneyMin.Enabled = c11MoneyCheck.Checked;
            c11MoneyMax.Enabled = c11MoneyCheck.Checked;
        }

        private void c12MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c12MoneyMin.Enabled = c12MoneyCheck.Checked;
            c12MoneyMax.Enabled = c12MoneyCheck.Checked;
        }

        private void c13MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c13MoneyMin.Enabled = c13MoneyCheck.Checked;
            c13MoneyMax.Enabled = c13MoneyCheck.Checked;
        }

        private void c14MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c14MoneyMin.Enabled = c14MoneyCheck.Checked;
            c14MoneyMax.Enabled = c14MoneyCheck.Checked;
        }

        private void c15MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c15MoneyMin.Enabled = c15MoneyCheck.Checked;
            c15MoneyMax.Enabled = c15MoneyCheck.Checked;
        }

        private void c16MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c16MoneyMin.Enabled = c16MoneyCheck.Checked;
            c16MoneyMax.Enabled = c16MoneyCheck.Checked;
        }

        private void c17MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c17MoneyMin.Enabled = c17MoneyCheck.Checked;
            c17MoneyMax.Enabled = c17MoneyCheck.Checked;
        }

        private void c18MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c18MoneyMin.Enabled = c18MoneyCheck.Checked;
            c18MoneyMax.Enabled = c18MoneyCheck.Checked;
        }

        private void c19MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c19MoneyMin.Enabled = c19MoneyCheck.Checked;
            c19MoneyMax.Enabled = c19MoneyCheck.Checked;
        }

        private void c20MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c20MoneyMin.Enabled = c20MoneyCheck.Checked;
            c20MoneyMax.Enabled = c20MoneyCheck.Checked;
        }

        private void c21MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c21MoneyMin.Enabled = c21MoneyCheck.Checked;
            c21MoneyMax.Enabled = c21MoneyCheck.Checked;
        }

        private void c22MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c22MoneyMin.Enabled = c22MoneyCheck.Checked;
            c22MoneyMax.Enabled = c22MoneyCheck.Checked;
        }

        private void c23MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c23MoneyMin.Enabled = c23MoneyCheck.Checked;
            c23MoneyMax.Enabled = c23MoneyCheck.Checked;
        }

        private void c24MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c24MoneyMin.Enabled = c24MoneyCheck.Checked;
            c24MoneyMax.Enabled = c24MoneyCheck.Checked;
        }

        private void c25MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c25MoneyMin.Enabled = c25MoneyCheck.Checked;
            c25MoneyMax.Enabled = c25MoneyCheck.Checked;
        }

        private void c26MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c26MoneyMin.Enabled = c26MoneyCheck.Checked;
            c26MoneyMax.Enabled = c26MoneyCheck.Checked;
        }

        private void c27MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c27MoneyMin.Enabled = c27MoneyCheck.Checked;
            c27MoneyMax.Enabled = c27MoneyCheck.Checked;
        }

        private void c28MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c28MoneyMin.Enabled = c28MoneyCheck.Checked;
            c28MoneyMax.Enabled = c28MoneyCheck.Checked;
        }

        private void c29MoneyCheck_CheckedChanged(object sender, EventArgs e)
        {
            c29MoneyMin.Enabled = c29MoneyCheck.Checked;
            c29MoneyMax.Enabled = c29MoneyCheck.Checked;
        }
        #endregion

        #endregion

        #region min

        #region difficulty min
        private void c0DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0DiffMin.Value < 0 || c0DiffMin.Value > c0DiffMax.Value)
            {
                c0DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1DiffMin.Value < 0 || c1DiffMin.Value > c1DiffMax.Value)
            {
                c1DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2DiffMin.Value < 0 || c2DiffMin.Value > c2DiffMax.Value)
            {
                c2DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3DiffMin.Value < 0 || c3DiffMin.Value > c3DiffMax.Value)
            {
                c3DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4DiffMin.Value < 0 || c4DiffMin.Value > c4DiffMax.Value)
            {
                c4DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5DiffMin.Value < 0 || c5DiffMin.Value > c5DiffMax.Value)
            {
                c5DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6DiffMin.Value < 0 || c6DiffMin.Value > c6DiffMax.Value)
            {
                c6DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7DiffMin.Value < 0 || c7DiffMin.Value > c7DiffMax.Value)
            {
                c7DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8DiffMin.Value < 0 || c8DiffMin.Value > c8DiffMax.Value)
            {
                c8DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9DiffMin.Value < 0 || c9DiffMin.Value > c9DiffMax.Value)
            {
                c9DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10DiffMin.Value < 0 || c10DiffMin.Value > c10DiffMax.Value)
            {
                c10DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11DiffMin.Value < 0 || c11DiffMin.Value > c11DiffMax.Value)
            {
                c11DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12DiffMin.Value < 0 || c12DiffMin.Value > c12DiffMax.Value)
            {
                c12DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13DiffMin.Value < 0 || c13DiffMin.Value > c13DiffMax.Value)
            {
                c13DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14DiffMin.Value < 0 || c14DiffMin.Value > c14DiffMax.Value)
            {
                c14DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15DiffMin.Value < 0 || c15DiffMin.Value > c15DiffMax.Value)
            {
                c15DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16DiffMin.Value < 0 || c16DiffMin.Value > c16DiffMax.Value)
            {
                c16DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17DiffMin.Value < 0 || c17DiffMin.Value > c17DiffMax.Value)
            {
                c17DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18DiffMin.Value < 0 || c18DiffMin.Value > c18DiffMax.Value)
            {
                c18DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19DiffMin.Value < 0 || c19DiffMin.Value > c19DiffMax.Value)
            {
                c19DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20DiffMin.Value < 0 || c20DiffMin.Value > c20DiffMax.Value)
            {
                c20DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21DiffMin.Value < 0 || c21DiffMin.Value > c21DiffMax.Value)
            {
                c21DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22DiffMin.Value < 0 || c22DiffMin.Value > c22DiffMax.Value)
            {
                c22DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23DiffMin.Value < 0 || c23DiffMin.Value > c23DiffMax.Value)
            {
                c23DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24DiffMin.Value < 0 || c24DiffMin.Value > c24DiffMax.Value)
            {
                c24DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25DiffMin.Value < 0 || c25DiffMin.Value > c25DiffMax.Value)
            {
                c25DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26DiffMin.Value < 0 || c26DiffMin.Value > c26DiffMax.Value)
            {
                c26DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27DiffMin.Value < 0 || c27DiffMin.Value > c27DiffMax.Value)
            {
                c27DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28DiffMin.Value < 0 || c28DiffMin.Value > c28DiffMax.Value)
            {
                c28DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29DiffMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29DiffMin.Value < 0 || c29DiffMin.Value > c29DiffMax.Value)
            {
                c29DiffMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region fire min
        private void c0FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0FireMin.Value < 0 || c0FireMin.Value > c0FireMax.Value)
            {
                c0FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1FireMin.Value < 0 || c1FireMin.Value > c1FireMax.Value)
            {
                c1FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2FireMin.Value < 0 || c2FireMin.Value > c2FireMax.Value)
            {
                c2FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3FireMin.Value < 0 || c3FireMin.Value > c3FireMax.Value)
            {
                c3FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4FireMin.Value < 0 || c4FireMin.Value > c4FireMax.Value)
            {
                c4FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5FireMin.Value < 0 || c5FireMin.Value > c5FireMax.Value)
            {
                c5FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6FireMin.Value < 0 || c6FireMin.Value > c6FireMax.Value)
            {
                c6FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7FireMin.Value < 0 || c7FireMin.Value > c7FireMax.Value)
            {
                c7FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8FireMin.Value < 0 || c8FireMin.Value > c8FireMax.Value)
            {
                c8FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9FireMin.Value < 0 || c9FireMin.Value > c9FireMax.Value)
            {
                c9FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10FireMin.Value < 0 || c10FireMin.Value > c10FireMax.Value)
            {
                c10FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11FireMin.Value < 0 || c11FireMin.Value > c11FireMax.Value)
            {
                c11FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12FireMin.Value < 0 || c12FireMin.Value > c12FireMax.Value)
            {
                c12FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13FireMin.Value < 0 || c13FireMin.Value > c13FireMax.Value)
            {
                c13FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14FireMin.Value < 0 || c14FireMin.Value > c14FireMax.Value)
            {
                c14FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15FireMin.Value < 0 || c15FireMin.Value > c15FireMax.Value)
            {
                c15FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16FireMin.Value < 0 || c16FireMin.Value > c16FireMax.Value)
            {
                c16FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17FireMin.Value < 0 || c17FireMin.Value > c17FireMax.Value)
            {
                c17FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18FireMin.Value < 0 || c18FireMin.Value > c18FireMax.Value)
            {
                c18FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19FireMin.Value < 0 || c19FireMin.Value > c19FireMax.Value)
            {
                c19FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20FireMin.Value < 0 || c20FireMin.Value > c20FireMax.Value)
            {
                c20FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21FireMin.Value < 0 || c21FireMin.Value > c21FireMax.Value)
            {
                c21FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22FireMin.Value < 0 || c22FireMin.Value > c22FireMax.Value)
            {
                c22FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23FireMin.Value < 0 || c23FireMin.Value > c23FireMax.Value)
            {
                c23FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24FireMin.Value < 0 || c24FireMin.Value > c24FireMax.Value)
            {
                c24FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25FireMin.Value < 0 || c25FireMin.Value > c25FireMax.Value)
            {
                c25FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26FireMin.Value < 0 || c26FireMin.Value > c26FireMax.Value)
            {
                c26FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27FireMin.Value < 0 || c27FireMin.Value > c27FireMax.Value)
            {
                c27FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28FireMin.Value < 0 || c28FireMin.Value > c28FireMax.Value)
            {
                c28FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29FireMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29FireMin.Value < 0 || c29FireMin.Value > c29FireMax.Value)
            {
                c29FireMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region crime min
        private void c0CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0CrimeMin.Value < 0 || c0CrimeMin.Value > c0CrimeMax.Value)
            {
                c0CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1CrimeMin.Value < 0 || c1CrimeMin.Value > c1CrimeMax.Value)
            {
                c1CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2CrimeMin.Value < 0 || c2CrimeMin.Value > c2CrimeMax.Value)
            {
                c2CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3CrimeMin.Value < 0 || c3CrimeMin.Value > c3CrimeMax.Value)
            {
                c3CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4CrimeMin.Value < 0 || c4CrimeMin.Value > c4CrimeMax.Value)
            {
                c4CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5CrimeMin.Value < 0 || c5CrimeMin.Value > c5CrimeMax.Value)
            {
                c5CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6CrimeMin.Value < 0 || c6CrimeMin.Value > c6CrimeMax.Value)
            {
                c6CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7CrimeMin.Value < 0 || c7CrimeMin.Value > c7CrimeMax.Value)
            {
                c7CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8CrimeMin.Value < 0 || c8CrimeMin.Value > c8CrimeMax.Value)
            {
                c8CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9CrimeMin.Value < 0 || c9CrimeMin.Value > c9CrimeMax.Value)
            {
                c9CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10CrimeMin.Value < 0 || c10CrimeMin.Value > c10CrimeMax.Value)
            {
                c10CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11CrimeMin.Value < 0 || c11CrimeMin.Value > c11CrimeMax.Value)
            {
                c11CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12CrimeMin.Value < 0 || c12CrimeMin.Value > c12CrimeMax.Value)
            {
                c12CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13CrimeMin.Value < 0 || c13CrimeMin.Value > c13CrimeMax.Value)
            {
                c13CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14CrimeMin.Value < 0 || c14CrimeMin.Value > c14CrimeMax.Value)
            {
                c14CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15CrimeMin.Value < 0 || c15CrimeMin.Value > c15CrimeMax.Value)
            {
                c15CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16CrimeMin.Value < 0 || c16CrimeMin.Value > c16CrimeMax.Value)
            {
                c16CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17CrimeMin.Value < 0 || c17CrimeMin.Value > c17CrimeMax.Value)
            {
                c17CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18CrimeMin.Value < 0 || c18CrimeMin.Value > c18CrimeMax.Value)
            {
                c18CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19CrimeMin.Value < 0 || c19CrimeMin.Value > c19CrimeMax.Value)
            {
                c19CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20CrimeMin.Value < 0 || c20CrimeMin.Value > c20CrimeMax.Value)
            {
                c20CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21CrimeMin.Value < 0 || c21CrimeMin.Value > c21CrimeMax.Value)
            {
                c21CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22CrimeMin.Value < 0 || c22CrimeMin.Value > c22CrimeMax.Value)
            {
                c22CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23CrimeMin.Value < 0 || c23CrimeMin.Value > c23CrimeMax.Value)
            {
                c23CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24CrimeMin.Value < 0 || c24CrimeMin.Value > c24CrimeMax.Value)
            {
                c24CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25CrimeMin.Value < 0 || c25CrimeMin.Value > c25CrimeMax.Value)
            {
                c25CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26CrimeMin.Value < 0 || c26CrimeMin.Value > c26CrimeMax.Value)
            {
                c26CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27CrimeMin.Value < 0 || c27CrimeMin.Value > c27CrimeMax.Value)
            {
                c27CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28CrimeMin.Value < 0 || c28CrimeMin.Value > c28CrimeMax.Value)
            {
                c28CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29CrimeMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29CrimeMin.Value < 0 || c29CrimeMin.Value > c29CrimeMax.Value)
            {
                c29CrimeMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region rescue min
        private void c0RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0RescueMin.Value < 0 || c0RescueMin.Value > c0RescueMax.Value)
            {
                c0RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1RescueMin.Value < 0 || c1RescueMin.Value > c1RescueMax.Value)
            {
                c1RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2RescueMin.Value < 0 || c2RescueMin.Value > c2RescueMax.Value)
            {
                c2RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3RescueMin.Value < 0 || c3RescueMin.Value > c3RescueMax.Value)
            {
                c3RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4RescueMin.Value < 0 || c4RescueMin.Value > c4RescueMax.Value)
            {
                c4RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5RescueMin.Value < 0 || c5RescueMin.Value > c5RescueMax.Value)
            {
                c5RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6RescueMin.Value < 0 || c6RescueMin.Value > c6RescueMax.Value)
            {
                c6RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7RescueMin.Value < 0 || c7RescueMin.Value > c7RescueMax.Value)
            {
                c7RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8RescueMin.Value < 0 || c8RescueMin.Value > c8RescueMax.Value)
            {
                c8RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9RescueMin.Value < 0 || c9RescueMin.Value > c9RescueMax.Value)
            {
                c9RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10RescueMin.Value < 0 || c10RescueMin.Value > c10RescueMax.Value)
            {
                c10RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11RescueMin.Value < 0 || c11RescueMin.Value > c11RescueMax.Value)
            {
                c11RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12RescueMin.Value < 0 || c12RescueMin.Value > c12RescueMax.Value)
            {
                c12RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13RescueMin.Value < 0 || c13RescueMin.Value > c13RescueMax.Value)
            {
                c13RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14RescueMin.Value < 0 || c14RescueMin.Value > c14RescueMax.Value)
            {
                c14RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15RescueMin.Value < 0 || c15RescueMin.Value > c15RescueMax.Value)
            {
                c15RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16RescueMin.Value < 0 || c16RescueMin.Value > c16RescueMax.Value)
            {
                c16RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17RescueMin.Value < 0 || c17RescueMin.Value > c17RescueMax.Value)
            {
                c17RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18RescueMin.Value < 0 || c18RescueMin.Value > c18RescueMax.Value)
            {
                c18RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19RescueMin.Value < 0 || c19RescueMin.Value > c19RescueMax.Value)
            {
                c19RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20RescueMin.Value < 0 || c20RescueMin.Value > c20RescueMax.Value)
            {
                c20RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21RescueMin.Value < 0 || c21RescueMin.Value > c21RescueMax.Value)
            {
                c21RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22RescueMin.Value < 0 || c22RescueMin.Value > c22RescueMax.Value)
            {
                c22RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23RescueMin.Value < 0 || c23RescueMin.Value > c23RescueMax.Value)
            {
                c23RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24RescueMin.Value < 0 || c24RescueMin.Value > c24RescueMax.Value)
            {
                c24RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25RescueMin.Value < 0 || c25RescueMin.Value > c25RescueMax.Value)
            {
                c25RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26RescueMin.Value < 0 || c26RescueMin.Value > c26RescueMax.Value)
            {
                c26RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27RescueMin.Value < 0 || c27RescueMin.Value > c27RescueMax.Value)
            {
                c27RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28RescueMin.Value < 0 || c28RescueMin.Value > c28RescueMax.Value)
            {
                c28RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29RescueMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29RescueMin.Value < 0 || c29RescueMin.Value > c29RescueMax.Value)
            {
                c29RescueMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region riot min
        private void c0RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0RiotMin.Value < 0 || c0RiotMin.Value > c0RiotMax.Value)
            {
                c0RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1RiotMin.Value < 0 || c1RiotMin.Value > c1RiotMax.Value)
            {
                c1RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2RiotMin.Value < 0 || c2RiotMin.Value > c2RiotMax.Value)
            {
                c2RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3RiotMin.Value < 0 || c3RiotMin.Value > c3RiotMax.Value)
            {
                c3RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4RiotMin.Value < 0 || c4RiotMin.Value > c4RiotMax.Value)
            {
                c4RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5RiotMin.Value < 0 || c5RiotMin.Value > c5RiotMax.Value)
            {
                c5RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6RiotMin.Value < 0 || c6RiotMin.Value > c6RiotMax.Value)
            {
                c6RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7RiotMin.Value < 0 || c7RiotMin.Value > c7RiotMax.Value)
            {
                c7RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8RiotMin.Value < 0 || c8RiotMin.Value > c8RiotMax.Value)
            {
                c8RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9RiotMin.Value < 0 || c9RiotMin.Value > c9RiotMax.Value)
            {
                c9RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10RiotMin.Value < 0 || c10RiotMin.Value > c10RiotMax.Value)
            {
                c10RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11RiotMin.Value < 0 || c11RiotMin.Value > c11RiotMax.Value)
            {
                c11RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12RiotMin.Value < 0 || c12RiotMin.Value > c12RiotMax.Value)
            {
                c12RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13RiotMin.Value < 0 || c13RiotMin.Value > c13RiotMax.Value)
            {
                c13RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14RiotMin.Value < 0 || c14RiotMin.Value > c14RiotMax.Value)
            {
                c14RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15RiotMin.Value < 0 || c15RiotMin.Value > c15RiotMax.Value)
            {
                c15RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16RiotMin.Value < 0 || c16RiotMin.Value > c16RiotMax.Value)
            {
                c16RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17RiotMin.Value < 0 || c17RiotMin.Value > c17RiotMax.Value)
            {
                c17RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18RiotMin.Value < 0 || c18RiotMin.Value > c18RiotMax.Value)
            {
                c18RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19RiotMin.Value < 0 || c19RiotMin.Value > c19RiotMax.Value)
            {
                c19RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20RiotMin.Value < 0 || c20RiotMin.Value > c20RiotMax.Value)
            {
                c20RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21RiotMin.Value < 0 || c21RiotMin.Value > c21RiotMax.Value)
            {
                c21RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22RiotMin.Value < 0 || c22RiotMin.Value > c22RiotMax.Value)
            {
                c22RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23RiotMin.Value < 0 || c23RiotMin.Value > c23RiotMax.Value)
            {
                c23RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24RiotMin.Value < 0 || c24RiotMin.Value > c24RiotMax.Value)
            {
                c24RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25RiotMin.Value < 0 || c25RiotMin.Value > c25RiotMax.Value)
            {
                c25RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26RiotMin.Value < 0 || c26RiotMin.Value > c26RiotMax.Value)
            {
                c26RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27RiotMin.Value < 0 || c27RiotMin.Value > c27RiotMax.Value)
            {
                c27RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28RiotMin.Value < 0 || c28RiotMin.Value > c28RiotMax.Value)
            {
                c28RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29RiotMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29RiotMin.Value < 0 || c29RiotMin.Value > c29RiotMax.Value)
            {
                c29RiotMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region traffic min
        private void c0TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0TrafficMin.Value < 0 || c0TrafficMin.Value > c0TrafficMax.Value)
            {
                c0TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1TrafficMin.Value < 0 || c1TrafficMin.Value > c1TrafficMax.Value)
            {
                c1TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2TrafficMin.Value < 0 || c2TrafficMin.Value > c2TrafficMax.Value)
            {
                c2TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3TrafficMin.Value < 0 || c3TrafficMin.Value > c3TrafficMax.Value)
            {
                c3TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4TrafficMin.Value < 0 || c4TrafficMin.Value > c4TrafficMax.Value)
            {
                c4TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5TrafficMin.Value < 0 || c5TrafficMin.Value > c5TrafficMax.Value)
            {
                c5TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6TrafficMin.Value < 0 || c6TrafficMin.Value > c6TrafficMax.Value)
            {
                c6TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7TrafficMin.Value < 0 || c7TrafficMin.Value > c7TrafficMax.Value)
            {
                c7TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8TrafficMin.Value < 0 || c8TrafficMin.Value > c8TrafficMax.Value)
            {
                c8TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9TrafficMin.Value < 0 || c9TrafficMin.Value > c9TrafficMax.Value)
            {
                c9TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10TrafficMin.Value < 0 || c10TrafficMin.Value > c10TrafficMax.Value)
            {
                c10TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11TrafficMin.Value < 0 || c11TrafficMin.Value > c11TrafficMax.Value)
            {
                c11TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12TrafficMin.Value < 0 || c12TrafficMin.Value > c12TrafficMax.Value)
            {
                c12TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13TrafficMin.Value < 0 || c13TrafficMin.Value > c13TrafficMax.Value)
            {
                c13TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14TrafficMin.Value < 0 || c14TrafficMin.Value > c14TrafficMax.Value)
            {
                c14TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15TrafficMin.Value < 0 || c15TrafficMin.Value > c15TrafficMax.Value)
            {
                c15TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16TrafficMin.Value < 0 || c16TrafficMin.Value > c16TrafficMax.Value)
            {
                c16TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17TrafficMin.Value < 0 || c17TrafficMin.Value > c17TrafficMax.Value)
            {
                c17TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18TrafficMin.Value < 0 || c18TrafficMin.Value > c18TrafficMax.Value)
            {
                c18TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19TrafficMin.Value < 0 || c19TrafficMin.Value > c19TrafficMax.Value)
            {
                c19TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20TrafficMin.Value < 0 || c20TrafficMin.Value > c20TrafficMax.Value)
            {
                c20TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21TrafficMin.Value < 0 || c21TrafficMin.Value > c21TrafficMax.Value)
            {
                c21TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22TrafficMin.Value < 0 || c22TrafficMin.Value > c22TrafficMax.Value)
            {
                c22TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23TrafficMin.Value < 0 || c23TrafficMin.Value > c23TrafficMax.Value)
            {
                c23TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24TrafficMin.Value < 0 || c24TrafficMin.Value > c24TrafficMax.Value)
            {
                c24TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25TrafficMin.Value < 0 || c25TrafficMin.Value > c25TrafficMax.Value)
            {
                c25TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26TrafficMin.Value < 0 || c26TrafficMin.Value > c26TrafficMax.Value)
            {
                c26TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27TrafficMin.Value < 0 || c27TrafficMin.Value > c27TrafficMax.Value)
            {
                c27TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28TrafficMin.Value < 0 || c28TrafficMin.Value > c28TrafficMax.Value)
            {
                c28TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29TrafficMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29TrafficMin.Value < 0 || c29TrafficMin.Value > c29TrafficMax.Value)
            {
                c29TrafficMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region medevac min
        private void c0MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0MedevacMin.Value < 0 || c0MedevacMin.Value > c0MedevacMax.Value)
            {
                c0MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1MedevacMin.Value < 0 || c1MedevacMin.Value > c1MedevacMax.Value)
            {
                c1MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2MedevacMin.Value < 0 || c2MedevacMin.Value > c2MedevacMax.Value)
            {
                c2MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3MedevacMin.Value < 0 || c3MedevacMin.Value > c3MedevacMax.Value)
            {
                c3MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4MedevacMin.Value < 0 || c4MedevacMin.Value > c4MedevacMax.Value)
            {
                c4MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5MedevacMin.Value < 0 || c5MedevacMin.Value > c5MedevacMax.Value)
            {
                c5MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6MedevacMin.Value < 0 || c6MedevacMin.Value > c6MedevacMax.Value)
            {
                c6MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7MedevacMin.Value < 0 || c7MedevacMin.Value > c7MedevacMax.Value)
            {
                c7MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8MedevacMin.Value < 0 || c8MedevacMin.Value > c8MedevacMax.Value)
            {
                c8MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9MedevacMin.Value < 0 || c9MedevacMin.Value > c9MedevacMax.Value)
            {
                c9MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10MedevacMin.Value < 0 || c10MedevacMin.Value > c10MedevacMax.Value)
            {
                c10MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11MedevacMin.Value < 0 || c11MedevacMin.Value > c11MedevacMax.Value)
            {
                c11MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12MedevacMin.Value < 0 || c12MedevacMin.Value > c12MedevacMax.Value)
            {
                c12MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13MedevacMin.Value < 0 || c13MedevacMin.Value > c13MedevacMax.Value)
            {
                c13MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14MedevacMin.Value < 0 || c14MedevacMin.Value > c14MedevacMax.Value)
            {
                c14MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15MedevacMin.Value < 0 || c15MedevacMin.Value > c15MedevacMax.Value)
            {
                c15MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16MedevacMin.Value < 0 || c16MedevacMin.Value > c16MedevacMax.Value)
            {
                c16MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17MedevacMin.Value < 0 || c17MedevacMin.Value > c17MedevacMax.Value)
            {
                c17MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18MedevacMin.Value < 0 || c18MedevacMin.Value > c18MedevacMax.Value)
            {
                c18MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19MedevacMin.Value < 0 || c19MedevacMin.Value > c19MedevacMax.Value)
            {
                c19MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20MedevacMin.Value < 0 || c20MedevacMin.Value > c20MedevacMax.Value)
            {
                c20MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21MedevacMin.Value < 0 || c21MedevacMin.Value > c21MedevacMax.Value)
            {
                c21MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22MedevacMin.Value < 0 || c22MedevacMin.Value > c22MedevacMax.Value)
            {
                c22MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23MedevacMin.Value < 0 || c23MedevacMin.Value > c23MedevacMax.Value)
            {
                c23MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24MedevacMin.Value < 0 || c24MedevacMin.Value > c24MedevacMax.Value)
            {
                c24MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25MedevacMin.Value < 0 || c25MedevacMin.Value > c25MedevacMax.Value)
            {
                c25MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26MedevacMin.Value < 0 || c26MedevacMin.Value > c26MedevacMax.Value)
            {
                c26MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27MedevacMin.Value < 0 || c27MedevacMin.Value > c27MedevacMax.Value)
            {
                c27MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28MedevacMin.Value < 0 || c28MedevacMin.Value > c28MedevacMax.Value)
            {
                c28MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29MedevacMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29MedevacMin.Value < 0 || c29MedevacMin.Value > c29MedevacMax.Value)
            {
                c29MedevacMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region transport min
        private void c0TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0TransportMin.Value < 0 || c0TransportMin.Value > c0TransportMax.Value)
            {
                c0TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1TransportMin.Value < 0 || c1TransportMin.Value > c1TransportMax.Value)
            {
                c1TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2TransportMin.Value < 0 || c2TransportMin.Value > c2TransportMax.Value)
            {
                c2TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3TransportMin.Value < 0 || c3TransportMin.Value > c3TransportMax.Value)
            {
                c3TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4TransportMin.Value < 0 || c4TransportMin.Value > c4TransportMax.Value)
            {
                c4TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5TransportMin.Value < 0 || c5TransportMin.Value > c5TransportMax.Value)
            {
                c5TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6TransportMin.Value < 0 || c6TransportMin.Value > c6TransportMax.Value)
            {
                c6TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7TransportMin.Value < 0 || c7TransportMin.Value > c7TransportMax.Value)
            {
                c7TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8TransportMin.Value < 0 || c8TransportMin.Value > c8TransportMax.Value)
            {
                c8TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9TransportMin.Value < 0 || c9TransportMin.Value > c9TransportMax.Value)
            {
                c9TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10TransportMin.Value < 0 || c10TransportMin.Value > c10TransportMax.Value)
            {
                c10TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11TransportMin.Value < 0 || c11TransportMin.Value > c11TransportMax.Value)
            {
                c11TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12TransportMin.Value < 0 || c12TransportMin.Value > c12TransportMax.Value)
            {
                c12TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13TransportMin.Value < 0 || c13TransportMin.Value > c13TransportMax.Value)
            {
                c13TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14TransportMin.Value < 0 || c14TransportMin.Value > c14TransportMax.Value)
            {
                c14TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15TransportMin.Value < 0 || c15TransportMin.Value > c15TransportMax.Value)
            {
                c15TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16TransportMin.Value < 0 || c16TransportMin.Value > c16TransportMax.Value)
            {
                c16TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17TransportMin.Value < 0 || c17TransportMin.Value > c17TransportMax.Value)
            {
                c17TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18TransportMin.Value < 0 || c18TransportMin.Value > c18TransportMax.Value)
            {
                c18TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19TransportMin.Value < 0 || c19TransportMin.Value > c19TransportMax.Value)
            {
                c19TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20TransportMin.Value < 0 || c20TransportMin.Value > c20TransportMax.Value)
            {
                c20TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21TransportMin.Value < 0 || c21TransportMin.Value > c21TransportMax.Value)
            {
                c21TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22TransportMin.Value < 0 || c22TransportMin.Value > c22TransportMax.Value)
            {
                c22TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23TransportMin.Value < 0 || c23TransportMin.Value > c23TransportMax.Value)
            {
                c23TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24TransportMin.Value < 0 || c24TransportMin.Value > c24TransportMax.Value)
            {
                c24TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25TransportMin.Value < 0 || c25TransportMin.Value > c25TransportMax.Value)
            {
                c25TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26TransportMin.Value < 0 || c26TransportMin.Value > c26TransportMax.Value)
            {
                c26TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27TransportMin.Value < 0 || c27TransportMin.Value > c27TransportMax.Value)
            {
                c27TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28TransportMin.Value < 0 || c28TransportMin.Value > c28TransportMax.Value)
            {
                c28TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29TransportMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29TransportMin.Value < 0 || c29TransportMin.Value > c29TransportMax.Value)
            {
                c29TransportMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region day min
        private void c0DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0DayMin.Value < 0 || c0DayMin.Value > c0DayMax.Value)
            {
                c0DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c1DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1DayMin.Value < 0 || c1DayMin.Value > c1DayMax.Value)
            {
                c1DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c2DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2DayMin.Value < 0 || c2DayMin.Value > c2DayMax.Value)
            {
                c2DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c3DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3DayMin.Value < 0 || c3DayMin.Value > c3DayMax.Value)
            {
                c3DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c4DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4DayMin.Value < 0 || c4DayMin.Value > c4DayMax.Value)
            {
                c4DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c5DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5DayMin.Value < 0 || c5DayMin.Value > c5DayMax.Value)
            {
                c5DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c6DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6DayMin.Value < 0 || c6DayMin.Value > c6DayMax.Value)
            {
                c6DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c7DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7DayMin.Value < 0 || c7DayMin.Value > c7DayMax.Value)
            {
                c7DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c8DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8DayMin.Value < 0 || c8DayMin.Value > c8DayMax.Value)
            {
                c8DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c9DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9DayMin.Value < 0 || c9DayMin.Value > c9DayMax.Value)
            {
                c9DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c10DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10DayMin.Value < 0 || c10DayMin.Value > c10DayMax.Value)
            {
                c10DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c11DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11DayMin.Value < 0 || c11DayMin.Value > c11DayMax.Value)
            {
                c11DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c12DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12DayMin.Value < 0 || c12DayMin.Value > c12DayMax.Value)
            {
                c12DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c13DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13DayMin.Value < 0 || c13DayMin.Value > c13DayMax.Value)
            {
                c13DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c14DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14DayMin.Value < 0 || c14DayMin.Value > c14DayMax.Value)
            {
                c14DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c15DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15DayMin.Value < 0 || c15DayMin.Value > c15DayMax.Value)
            {
                c15DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c16DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16DayMin.Value < 0 || c16DayMin.Value > c16DayMax.Value)
            {
                c16DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c17DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17DayMin.Value < 0 || c17DayMin.Value > c17DayMax.Value)
            {
                c17DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c18DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18DayMin.Value < 0 || c18DayMin.Value > c18DayMax.Value)
            {
                c18DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c19DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19DayMin.Value < 0 || c19DayMin.Value > c19DayMax.Value)
            {
                c19DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c20DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20DayMin.Value < 0 || c20DayMin.Value > c20DayMax.Value)
            {
                c20DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c21DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21DayMin.Value < 0 || c21DayMin.Value > c21DayMax.Value)
            {
                c21DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c22DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22DayMin.Value < 0 || c22DayMin.Value > c22DayMax.Value)
            {
                c22DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c23DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23DayMin.Value < 0 || c23DayMin.Value > c23DayMax.Value)
            {
                c23DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c24DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24DayMin.Value < 0 || c24DayMin.Value > c24DayMax.Value)
            {
                c24DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c25DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25DayMin.Value < 0 || c25DayMin.Value > c25DayMax.Value)
            {
                c25DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c26DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26DayMin.Value < 0 || c26DayMin.Value > c26DayMax.Value)
            {
                c26DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c27DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27DayMin.Value < 0 || c27DayMin.Value > c27DayMax.Value)
            {
                c27DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c28DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28DayMin.Value < 0 || c28DayMin.Value > c28DayMax.Value)
            {
                c28DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void c29DayMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29DayMin.Value < 0 || c29DayMin.Value > c29DayMax.Value)
            {
                c29DayMin.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }
        #endregion

        #region points min
        private void c0PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0PointsMin.Value < 1 || c0PointsMin.Value > c0PointsMax.Value)
            {
                c0PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c1PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1PointsMin.Value < 1 || c1PointsMin.Value > c1PointsMax.Value)
            {
                c1PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c2PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2PointsMin.Value < 1 || c2PointsMin.Value > c2PointsMax.Value)
            {
                c2PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c3PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3PointsMin.Value < 1 || c3PointsMin.Value > c3PointsMax.Value)
            {
                c3PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c4PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4PointsMin.Value < 1 || c4PointsMin.Value > c4PointsMax.Value)
            {
                c4PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c5PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5PointsMin.Value < 1 || c5PointsMin.Value > c5PointsMax.Value)
            {
                c5PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c6PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6PointsMin.Value < 1 || c6PointsMin.Value > c6PointsMax.Value)
            {
                c6PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c7PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7PointsMin.Value < 1 || c7PointsMin.Value > c7PointsMax.Value)
            {
                c7PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c8PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8PointsMin.Value < 1 || c8PointsMin.Value > c8PointsMax.Value)
            {
                c8PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c9PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9PointsMin.Value < 1 || c9PointsMin.Value > c9PointsMax.Value)
            {
                c9PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c10PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10PointsMin.Value < 1 || c10PointsMin.Value > c10PointsMax.Value)
            {
                c10PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c11PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11PointsMin.Value < 1 || c11PointsMin.Value > c11PointsMax.Value)
            {
                c11PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c12PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12PointsMin.Value < 1 || c12PointsMin.Value > c12PointsMax.Value)
            {
                c12PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c13PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13PointsMin.Value < 1 || c13PointsMin.Value > c13PointsMax.Value)
            {
                c13PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c14PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14PointsMin.Value < 1 || c14PointsMin.Value > c14PointsMax.Value)
            {
                c14PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c15PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15PointsMin.Value < 1 || c15PointsMin.Value > c15PointsMax.Value)
            {
                c15PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c16PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16PointsMin.Value < 1 || c16PointsMin.Value > c16PointsMax.Value)
            {
                c16PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c17PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17PointsMin.Value < 1 || c17PointsMin.Value > c17PointsMax.Value)
            {
                c17PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c18PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18PointsMin.Value < 1 || c18PointsMin.Value > c18PointsMax.Value)
            {
                c18PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c19PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19PointsMin.Value < 1 || c19PointsMin.Value > c19PointsMax.Value)
            {
                c19PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c20PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20PointsMin.Value < 1 || c20PointsMin.Value > c20PointsMax.Value)
            {
                c20PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c21PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21PointsMin.Value < 1 || c21PointsMin.Value > c21PointsMax.Value)
            {
                c21PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c22PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22PointsMin.Value < 1 || c22PointsMin.Value > c22PointsMax.Value)
            {
                c22PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c23PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23PointsMin.Value < 1 || c23PointsMin.Value > c23PointsMax.Value)
            {
                c23PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c24PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24PointsMin.Value < 1 || c24PointsMin.Value > c24PointsMax.Value)
            {
                c24PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c25PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25PointsMin.Value < 1 || c25PointsMin.Value > c25PointsMax.Value)
            {
                c25PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c26PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26PointsMin.Value < 1 || c26PointsMin.Value > c26PointsMax.Value)
            {
                c26PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c27PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27PointsMin.Value < 1 || c27PointsMin.Value > c27PointsMax.Value)
            {
                c27PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c28PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28PointsMin.Value < 1 || c28PointsMin.Value > c28PointsMax.Value)
            {
                c28PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c29PointsMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29PointsMin.Value < 1 || c29PointsMin.Value > c29PointsMax.Value)
            {
                c29PointsMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }
        #endregion

        #region money min
        private void c0MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c0MoneyMin.Value < 1 || c0MoneyMin.Value > c0MoneyMax.Value)
            {
                c0MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c1MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c1MoneyMin.Value < 1 || c1MoneyMin.Value > c1MoneyMax.Value)
            {
                c1MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c2MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c2MoneyMin.Value < 1 || c2MoneyMin.Value > c2MoneyMax.Value)
            {
                c2MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c3MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c3MoneyMin.Value < 1 || c3MoneyMin.Value > c3MoneyMax.Value)
            {
                c3MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c4MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c4MoneyMin.Value < 1 || c4MoneyMin.Value > c4MoneyMax.Value)
            {
                c4MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c5MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c5MoneyMin.Value < 1 || c5MoneyMin.Value > c5MoneyMax.Value)
            {
                c5MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c6MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c6MoneyMin.Value < 1 || c6MoneyMin.Value > c6MoneyMax.Value)
            {
                c6MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c7MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c7MoneyMin.Value < 1 || c7MoneyMin.Value > c7MoneyMax.Value)
            {
                c7MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c8MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c8MoneyMin.Value < 1 || c8MoneyMin.Value > c8MoneyMax.Value)
            {
                c8MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c9MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c9MoneyMin.Value < 1 || c9MoneyMin.Value > c9MoneyMax.Value)
            {
                c9MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c10MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c10MoneyMin.Value < 1 || c10MoneyMin.Value > c10MoneyMax.Value)
            {
                c10MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c11MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c11MoneyMin.Value < 1 || c11MoneyMin.Value > c11MoneyMax.Value)
            {
                c11MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c12MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c12MoneyMin.Value < 1 || c12MoneyMin.Value > c12MoneyMax.Value)
            {
                c12MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c13MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c13MoneyMin.Value < 1 || c13MoneyMin.Value > c13MoneyMax.Value)
            {
                c13MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c14MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c14MoneyMin.Value < 1 || c14MoneyMin.Value > c14MoneyMax.Value)
            {
                c14MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c15MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c15MoneyMin.Value < 1 || c15MoneyMin.Value > c15MoneyMax.Value)
            {
                c15MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c16MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c16MoneyMin.Value < 1 || c16MoneyMin.Value > c16MoneyMax.Value)
            {
                c16MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c17MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c17MoneyMin.Value < 1 || c17MoneyMin.Value > c17MoneyMax.Value)
            {
                c17MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c18MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c18MoneyMin.Value < 1 || c18MoneyMin.Value > c18MoneyMax.Value)
            {
                c18MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c19MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c19MoneyMin.Value < 1 || c19MoneyMin.Value > c19MoneyMax.Value)
            {
                c19MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c20MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c20MoneyMin.Value < 1 || c20MoneyMin.Value > c20MoneyMax.Value)
            {
                c20MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c21MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c21MoneyMin.Value < 1 || c21MoneyMin.Value > c21MoneyMax.Value)
            {
                c21MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c22MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c22MoneyMin.Value < 1 || c22MoneyMin.Value > c22MoneyMax.Value)
            {
                c22MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c23MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c23MoneyMin.Value < 1 || c23MoneyMin.Value > c23MoneyMax.Value)
            {
                c23MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c24MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c24MoneyMin.Value < 1 || c24MoneyMin.Value > c24MoneyMax.Value)
            {
                c24MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c25MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c25MoneyMin.Value < 1 || c25MoneyMin.Value > c25MoneyMax.Value)
            {
                c25MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c26MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c26MoneyMin.Value < 1 || c26MoneyMin.Value > c26MoneyMax.Value)
            {
                c26MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c27MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c27MoneyMin.Value < 1 || c27MoneyMin.Value > c27MoneyMax.Value)
            {
                c27MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c28MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c28MoneyMin.Value < 1 || c28MoneyMin.Value > c28MoneyMax.Value)
            {
                c28MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void c29MoneyMin_ValueChanged(object sender, EventArgs e)
        {
            if (c29MoneyMin.Value < 1 || c29MoneyMin.Value > c29MoneyMax.Value)
            {
                c29MoneyMin.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }


        #endregion

        #endregion

        #region max

        #region difficulty max
        private void c0DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0DiffMax.Value > 3 || c0DiffMax.Value < c0DiffMin.Value)
            {
                c0DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c1DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1DiffMax.Value > 3 || c1DiffMax.Value < c1DiffMin.Value)
            {
                c1DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c2DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2DiffMax.Value > 3 || c2DiffMax.Value < c2DiffMin.Value)
            {
                c2DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c3DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3DiffMax.Value > 3 || c3DiffMax.Value < c3DiffMin.Value)
            {
                c3DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c4DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4DiffMax.Value > 3 || c4DiffMax.Value < c4DiffMin.Value)
            {
                c4DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c5DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5DiffMax.Value > 3 || c5DiffMax.Value < c5DiffMin.Value)
            {
                c5DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c6DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6DiffMax.Value > 3 || c6DiffMax.Value < c6DiffMin.Value)
            {
                c6DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c7DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7DiffMax.Value > 3 || c7DiffMax.Value < c7DiffMin.Value)
            {
                c7DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c8DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8DiffMax.Value > 3 || c8DiffMax.Value < c8DiffMin.Value)
            {
                c8DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c9DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9DiffMax.Value > 3 || c9DiffMax.Value < c9DiffMin.Value)
            {
                c9DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c10DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10DiffMax.Value > 3 || c10DiffMax.Value < c10DiffMin.Value)
            {
                c10DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c11DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11DiffMax.Value > 3 || c11DiffMax.Value < c11DiffMin.Value)
            {
                c11DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c12DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12DiffMax.Value > 3 || c12DiffMax.Value < c12DiffMin.Value)
            {
                c12DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c13DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13DiffMax.Value > 3 || c13DiffMax.Value < c13DiffMin.Value)
            {
                c13DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c14DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14DiffMax.Value > 3 || c14DiffMax.Value < c14DiffMin.Value)
            {
                c14DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c15DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15DiffMax.Value > 3 || c15DiffMax.Value < c15DiffMin.Value)
            {
                c15DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c16DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16DiffMax.Value > 3 || c16DiffMax.Value < c16DiffMin.Value)
            {
                c16DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c17DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17DiffMax.Value > 3 || c17DiffMax.Value < c17DiffMin.Value)
            {
                c17DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c18DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18DiffMax.Value > 3 || c18DiffMax.Value < c18DiffMin.Value)
            {
                c18DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c19DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19DiffMax.Value > 3 || c19DiffMax.Value < c19DiffMin.Value)
            {
                c19DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c20DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20DiffMax.Value > 3 || c20DiffMax.Value < c20DiffMin.Value)
            {
                c20DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c21DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21DiffMax.Value > 3 || c21DiffMax.Value < c21DiffMin.Value)
            {
                c21DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c22DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22DiffMax.Value > 3 || c22DiffMax.Value < c22DiffMin.Value)
            {
                c22DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c23DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23DiffMax.Value > 3 || c23DiffMax.Value < c23DiffMin.Value)
            {
                c23DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c24DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24DiffMax.Value > 3 || c24DiffMax.Value < c24DiffMin.Value)
            {
                c24DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c25DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25DiffMax.Value > 3 || c25DiffMax.Value < c25DiffMin.Value)
            {
                c25DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c26DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26DiffMax.Value > 3 || c26DiffMax.Value < c26DiffMin.Value)
            {
                c26DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c27DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27DiffMax.Value > 3 || c27DiffMax.Value < c27DiffMin.Value)
            {
                c27DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c28DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28DiffMax.Value > 3 || c28DiffMax.Value < c28DiffMin.Value)
            {
                c28DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }

        private void c29DiffMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29DiffMax.Value > 3 || c29DiffMax.Value < c29DiffMin.Value)
            {
                c29DiffMax.Value = 3;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 3.");
            }
        }
        #endregion

        #region fire max
        private void c0FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0FireMax.Value > 99 || c0FireMax.Value < c0FireMin.Value)
            {
                c0FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c1FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1FireMax.Value > 99 || c1FireMax.Value < c1FireMin.Value)
            {
                c1FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c2FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2FireMax.Value > 99 || c2FireMax.Value < c2FireMin.Value)
            {
                c2FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c3FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3FireMax.Value > 99 || c3FireMax.Value < c3FireMin.Value)
            {
                c3FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c4FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4FireMax.Value > 99 || c4FireMax.Value < c4FireMin.Value)
            {
                c4FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c5FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5FireMax.Value > 99 || c5FireMax.Value < c5FireMin.Value)
            {
                c5FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c6FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6FireMax.Value > 99 || c6FireMax.Value < c6FireMin.Value)
            {
                c6FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c7FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7FireMax.Value > 99 || c7FireMax.Value < c7FireMin.Value)
            {
                c7FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c8FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8FireMax.Value > 99 || c8FireMax.Value < c8FireMin.Value)
            {
                c8FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c9FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9FireMax.Value > 99 || c9FireMax.Value < c9FireMin.Value)
            {
                c9FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c10FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10FireMax.Value > 99 || c10FireMax.Value < c10FireMin.Value)
            {
                c10FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c11FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11FireMax.Value > 99 || c11FireMax.Value < c11FireMin.Value)
            {
                c11FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c12FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12FireMax.Value > 99 || c12FireMax.Value < c12FireMin.Value)
            {
                c12FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c13FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13FireMax.Value > 99 || c13FireMax.Value < c13FireMin.Value)
            {
                c13FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c14FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14FireMax.Value > 99 || c14FireMax.Value < c14FireMin.Value)
            {
                c14FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c15FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15FireMax.Value > 99 || c15FireMax.Value < c15FireMin.Value)
            {
                c15FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c16FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16FireMax.Value > 99 || c16FireMax.Value < c16FireMin.Value)
            {
                c16FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c17FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17FireMax.Value > 99 || c17FireMax.Value < c17FireMin.Value)
            {
                c17FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c18FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18FireMax.Value > 99 || c18FireMax.Value < c18FireMin.Value)
            {
                c18FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c19FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19FireMax.Value > 99 || c19FireMax.Value < c19FireMin.Value)
            {
                c19FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c20FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20FireMax.Value > 99 || c20FireMax.Value < c20FireMin.Value)
            {
                c20FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c21FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21FireMax.Value > 99 || c21FireMax.Value < c21FireMin.Value)
            {
                c21FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c22FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22FireMax.Value > 99 || c22FireMax.Value < c22FireMin.Value)
            {
                c22FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c23FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23FireMax.Value > 99 || c23FireMax.Value < c23FireMin.Value)
            {
                c23FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c24FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24FireMax.Value > 99 || c24FireMax.Value < c24FireMin.Value)
            {
                c24FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c25FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25FireMax.Value > 99 || c25FireMax.Value < c25FireMin.Value)
            {
                c25FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c26FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26FireMax.Value > 99 || c26FireMax.Value < c26FireMin.Value)
            {
                c26FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c27FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27FireMax.Value > 99 || c27FireMax.Value < c27FireMin.Value)
            {
                c27FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c28FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28FireMax.Value > 99 || c28FireMax.Value < c28FireMin.Value)
            {
                c28FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c29FireMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29FireMax.Value > 99 || c29FireMax.Value < c29FireMin.Value)
            {
                c29FireMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }
        #endregion

        #region crime max
        private void c0CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0CrimeMax.Value > 99 || c0CrimeMax.Value < c0CrimeMin.Value)
            {
                c0CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c1CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1CrimeMax.Value > 99 || c1CrimeMax.Value < c1CrimeMin.Value)
            {
                c1CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c2CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2CrimeMax.Value > 99 || c2CrimeMax.Value < c2CrimeMin.Value)
            {
                c2CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c3CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3CrimeMax.Value > 99 || c3CrimeMax.Value < c3CrimeMin.Value)
            {
                c3CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c4CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4CrimeMax.Value > 99 || c4CrimeMax.Value < c4CrimeMin.Value)
            {
                c4CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c5CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5CrimeMax.Value > 99 || c5CrimeMax.Value < c5CrimeMin.Value)
            {
                c5CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c6CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6CrimeMax.Value > 99 || c6CrimeMax.Value < c6CrimeMin.Value)
            {
                c6CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c7CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7CrimeMax.Value > 99 || c7CrimeMax.Value < c7CrimeMin.Value)
            {
                c7CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c8CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8CrimeMax.Value > 99 || c8CrimeMax.Value < c8CrimeMin.Value)
            {
                c8CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c9CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9CrimeMax.Value > 99 || c9CrimeMax.Value < c9CrimeMin.Value)
            {
                c9CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c10CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10CrimeMax.Value > 99 || c10CrimeMax.Value < c10CrimeMin.Value)
            {
                c10CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c11CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11CrimeMax.Value > 99 || c11CrimeMax.Value < c11CrimeMin.Value)
            {
                c11CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c12CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12CrimeMax.Value > 99 || c12CrimeMax.Value < c12CrimeMin.Value)
            {
                c12CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c13CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13CrimeMax.Value > 99 || c13CrimeMax.Value < c13CrimeMin.Value)
            {
                c13CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c14CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14CrimeMax.Value > 99 || c14CrimeMax.Value < c14CrimeMin.Value)
            {
                c14CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c15CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15CrimeMax.Value > 99 || c15CrimeMax.Value < c15CrimeMin.Value)
            {
                c15CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c16CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16CrimeMax.Value > 99 || c16CrimeMax.Value < c16CrimeMin.Value)
            {
                c16CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c17CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17CrimeMax.Value > 99 || c17CrimeMax.Value < c17CrimeMin.Value)
            {
                c17CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c18CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18CrimeMax.Value > 99 || c18CrimeMax.Value < c18CrimeMin.Value)
            {
                c18CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c19CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19CrimeMax.Value > 99 || c19CrimeMax.Value < c19CrimeMin.Value)
            {
                c19CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c20CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20CrimeMax.Value > 99 || c20CrimeMax.Value < c20CrimeMin.Value)
            {
                c20CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c21CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21CrimeMax.Value > 99 || c21CrimeMax.Value < c21CrimeMin.Value)
            {
                c21CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c22CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22CrimeMax.Value > 99 || c22CrimeMax.Value < c22CrimeMin.Value)
            {
                c22CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c23CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23CrimeMax.Value > 99 || c23CrimeMax.Value < c23CrimeMin.Value)
            {
                c23CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c24CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24CrimeMax.Value > 99 || c24CrimeMax.Value < c24CrimeMin.Value)
            {
                c24CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c25CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25CrimeMax.Value > 99 || c25CrimeMax.Value < c25CrimeMin.Value)
            {
                c25CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c26CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26CrimeMax.Value > 99 || c26CrimeMax.Value < c26CrimeMin.Value)
            {
                c26CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c27CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27CrimeMax.Value > 99 || c27CrimeMax.Value < c27CrimeMin.Value)
            {
                c27CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c28CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28CrimeMax.Value > 99 || c28CrimeMax.Value < c28CrimeMin.Value)
            {
                c28CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c29CrimeMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29CrimeMax.Value > 99 || c29CrimeMax.Value < c29CrimeMin.Value)
            {
                c29CrimeMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }
        #endregion

        #region rescue max
        private void c0RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0RescueMax.Value > 99 || c0RescueMax.Value < c0RescueMin.Value)
            {
                c0RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c1RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1RescueMax.Value > 99 || c1RescueMax.Value < c1RescueMin.Value)
            {
                c1RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c2RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2RescueMax.Value > 99 || c2RescueMax.Value < c2RescueMin.Value)
            {
                c2RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c3RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3RescueMax.Value > 99 || c3RescueMax.Value < c3RescueMin.Value)
            {
                c3RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c4RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4RescueMax.Value > 99 || c4RescueMax.Value < c4RescueMin.Value)
            {
                c4RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c5RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5RescueMax.Value > 99 || c5RescueMax.Value < c5RescueMin.Value)
            {
                c5RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c6RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6RescueMax.Value > 99 || c6RescueMax.Value < c6RescueMin.Value)
            {
                c6RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c7RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7RescueMax.Value > 99 || c7RescueMax.Value < c7RescueMin.Value)
            {
                c7RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c8RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8RescueMax.Value > 99 || c8RescueMax.Value < c8RescueMin.Value)
            {
                c8RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c9RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9RescueMax.Value > 99 || c9RescueMax.Value < c9RescueMin.Value)
            {
                c9RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c10RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10RescueMax.Value > 99 || c10RescueMax.Value < c10RescueMin.Value)
            {
                c10RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c11RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11RescueMax.Value > 99 || c11RescueMax.Value < c11RescueMin.Value)
            {
                c11RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c12RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12RescueMax.Value > 99 || c12RescueMax.Value < c12RescueMin.Value)
            {
                c12RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c13RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13RescueMax.Value > 99 || c13RescueMax.Value < c13RescueMin.Value)
            {
                c13RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c14RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14RescueMax.Value > 99 || c14RescueMax.Value < c14RescueMin.Value)
            {
                c14RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c15RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15RescueMax.Value > 99 || c15RescueMax.Value < c15RescueMin.Value)
            {
                c15RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c16RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16RescueMax.Value > 99 || c16RescueMax.Value < c16RescueMin.Value)
            {
                c16RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c17RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17RescueMax.Value > 99 || c17RescueMax.Value < c17RescueMin.Value)
            {
                c17RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c18RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18RescueMax.Value > 99 || c18RescueMax.Value < c18RescueMin.Value)
            {
                c18RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c19RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19RescueMax.Value > 99 || c19RescueMax.Value < c19RescueMin.Value)
            {
                c19RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c20RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20RescueMax.Value > 99 || c20RescueMax.Value < c20RescueMin.Value)
            {
                c20RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c21RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21RescueMax.Value > 99 || c21RescueMax.Value < c21RescueMin.Value)
            {
                c21RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c22RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22RescueMax.Value > 99 || c22RescueMax.Value < c22RescueMin.Value)
            {
                c22RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c23RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23RescueMax.Value > 99 || c23RescueMax.Value < c23RescueMin.Value)
            {
                c23RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c24RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24RescueMax.Value > 99 || c24RescueMax.Value < c24RescueMin.Value)
            {
                c24RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c25RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25RescueMax.Value > 99 || c25RescueMax.Value < c25RescueMin.Value)
            {
                c25RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c26RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26RescueMax.Value > 99 || c26RescueMax.Value < c26RescueMin.Value)
            {
                c26RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c27RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27RescueMax.Value > 99 || c27RescueMax.Value < c27RescueMin.Value)
            {
                c27RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c28RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28RescueMax.Value > 99 || c28RescueMax.Value < c28RescueMin.Value)
            {
                c28RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c29RescueMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29RescueMax.Value > 99 || c29RescueMax.Value < c29RescueMin.Value)
            {
                c29RescueMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }
        #endregion

        #region riot max
        private void c0RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0RiotMax.Value > 99 || c0RiotMax.Value < c0RiotMin.Value)
            {
                c0RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c1RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1RiotMax.Value > 99 || c1RiotMax.Value < c1RiotMin.Value)
            {
                c1RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c2RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2RiotMax.Value > 99 || c2RiotMax.Value < c2RiotMin.Value)
            {
                c2RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c3RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3RiotMax.Value > 99 || c3RiotMax.Value < c3RiotMin.Value)
            {
                c3RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c4RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4RiotMax.Value > 99 || c4RiotMax.Value < c4RiotMin.Value)
            {
                c4RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c5RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5RiotMax.Value > 99 || c5RiotMax.Value < c5RiotMin.Value)
            {
                c5RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c6RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6RiotMax.Value > 99 || c6RiotMax.Value < c6RiotMin.Value)
            {
                c6RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c7RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7RiotMax.Value > 99 || c7RiotMax.Value < c7RiotMin.Value)
            {
                c7RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c8RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8RiotMax.Value > 99 || c8RiotMax.Value < c8RiotMin.Value)
            {
                c8RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c9RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9RiotMax.Value > 99 || c9RiotMax.Value < c9RiotMin.Value)
            {
                c9RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c10RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10RiotMax.Value > 99 || c10RiotMax.Value < c10RiotMin.Value)
            {
                c10RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c11RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11RiotMax.Value > 99 || c11RiotMax.Value < c11RiotMin.Value)
            {
                c11RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c12RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12RiotMax.Value > 99 || c12RiotMax.Value < c12RiotMin.Value)
            {
                c12RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c13RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13RiotMax.Value > 99 || c13RiotMax.Value < c13RiotMin.Value)
            {
                c13RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c14RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14RiotMax.Value > 99 || c14RiotMax.Value < c14RiotMin.Value)
            {
                c14RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c15RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15RiotMax.Value > 99 || c15RiotMax.Value < c15RiotMin.Value)
            {
                c15RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c16RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16RiotMax.Value > 99 || c16RiotMax.Value < c16RiotMin.Value)
            {
                c16RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c17RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17RiotMax.Value > 99 || c17RiotMax.Value < c17RiotMin.Value)
            {
                c17RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c18RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18RiotMax.Value > 99 || c18RiotMax.Value < c18RiotMin.Value)
            {
                c18RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c19RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19RiotMax.Value > 99 || c19RiotMax.Value < c19RiotMin.Value)
            {
                c19RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c20RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20RiotMax.Value > 99 || c20RiotMax.Value < c20RiotMin.Value)
            {
                c20RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c21RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21RiotMax.Value > 99 || c21RiotMax.Value < c21RiotMin.Value)
            {
                c21RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c22RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22RiotMax.Value > 99 || c22RiotMax.Value < c22RiotMin.Value)
            {
                c22RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c23RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23RiotMax.Value > 99 || c23RiotMax.Value < c23RiotMin.Value)
            {
                c23RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c24RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24RiotMax.Value > 99 || c24RiotMax.Value < c24RiotMin.Value)
            {
                c24RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c25RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25RiotMax.Value > 99 || c25RiotMax.Value < c25RiotMin.Value)
            {
                c25RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c26RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26RiotMax.Value > 99 || c26RiotMax.Value < c26RiotMin.Value)
            {
                c26RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c27RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27RiotMax.Value > 99 || c27RiotMax.Value < c27RiotMin.Value)
            {
                c27RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c28RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28RiotMax.Value > 99 || c28RiotMax.Value < c28RiotMin.Value)
            {
                c28RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c29RiotMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29RiotMax.Value > 99 || c29RiotMax.Value < c29RiotMin.Value)
            {
                c29RiotMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }
        #endregion

        #region traffic max
        private void c0TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0TrafficMax.Value > 99 || c0TrafficMax.Value < c0TrafficMin.Value)
            {
                c0TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c1TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1TrafficMax.Value > 99 || c1TrafficMax.Value < c1TrafficMin.Value)
            {
                c1TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c2TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2TrafficMax.Value > 99 || c2TrafficMax.Value < c2TrafficMin.Value)
            {
                c2TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c3TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3TrafficMax.Value > 99 || c3TrafficMax.Value < c3TrafficMin.Value)
            {
                c3TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c4TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4TrafficMax.Value > 99 || c4TrafficMax.Value < c4TrafficMin.Value)
            {
                c4TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c5TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5TrafficMax.Value > 99 || c5TrafficMax.Value < c5TrafficMin.Value)
            {
                c5TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c6TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6TrafficMax.Value > 99 || c6TrafficMax.Value < c6TrafficMin.Value)
            {
                c6TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c7TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7TrafficMax.Value > 99 || c7TrafficMax.Value < c7TrafficMin.Value)
            {
                c7TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c8TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8TrafficMax.Value > 99 || c8TrafficMax.Value < c8TrafficMin.Value)
            {
                c8TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c9TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9TrafficMax.Value > 99 || c9TrafficMax.Value < c9TrafficMin.Value)
            {
                c9TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c10TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10TrafficMax.Value > 99 || c10TrafficMax.Value < c10TrafficMin.Value)
            {
                c10TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c11TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11TrafficMax.Value > 99 || c11TrafficMax.Value < c11TrafficMin.Value)
            {
                c11TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c12TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12TrafficMax.Value > 99 || c12TrafficMax.Value < c12TrafficMin.Value)
            {
                c12TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c13TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13TrafficMax.Value > 99 || c13TrafficMax.Value < c13TrafficMin.Value)
            {
                c13TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c14TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14TrafficMax.Value > 99 || c14TrafficMax.Value < c14TrafficMin.Value)
            {
                c14TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c15TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15TrafficMax.Value > 99 || c15TrafficMax.Value < c15TrafficMin.Value)
            {
                c15TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c16TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16TrafficMax.Value > 99 || c16TrafficMax.Value < c16TrafficMin.Value)
            {
                c16TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c17TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17TrafficMax.Value > 99 || c17TrafficMax.Value < c17TrafficMin.Value)
            {
                c17TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c18TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18TrafficMax.Value > 99 || c18TrafficMax.Value < c18TrafficMin.Value)
            {
                c18TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c19TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19TrafficMax.Value > 99 || c19TrafficMax.Value < c19TrafficMin.Value)
            {
                c19TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c20TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20TrafficMax.Value > 99 || c20TrafficMax.Value < c20TrafficMin.Value)
            {
                c20TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c21TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21TrafficMax.Value > 99 || c21TrafficMax.Value < c21TrafficMin.Value)
            {
                c21TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c22TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22TrafficMax.Value > 99 || c22TrafficMax.Value < c22TrafficMin.Value)
            {
                c22TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c23TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23TrafficMax.Value > 99 || c23TrafficMax.Value < c23TrafficMin.Value)
            {
                c23TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c24TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24TrafficMax.Value > 99 || c24TrafficMax.Value < c24TrafficMin.Value)
            {
                c24TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c25TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25TrafficMax.Value > 99 || c25TrafficMax.Value < c25TrafficMin.Value)
            {
                c25TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c26TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26TrafficMax.Value > 99 || c26TrafficMax.Value < c26TrafficMin.Value)
            {
                c26TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c27TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27TrafficMax.Value > 99 || c27TrafficMax.Value < c27TrafficMin.Value)
            {
                c27TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c28TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28TrafficMax.Value > 99 || c28TrafficMax.Value < c28TrafficMin.Value)
            {
                c28TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c29TrafficMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29TrafficMax.Value > 99 || c29TrafficMax.Value < c29TrafficMin.Value)
            {
                c29TrafficMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }
        #endregion

        #region medevac max
        private void c0MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0MedevacMax.Value > 99 || c0MedevacMax.Value < c0MedevacMin.Value)
            {
                c0MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c1MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1MedevacMax.Value > 99 || c1MedevacMax.Value < c1MedevacMin.Value)
            {
                c1MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c2MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2MedevacMax.Value > 99 || c2MedevacMax.Value < c2MedevacMin.Value)
            {
                c2MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c3MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3MedevacMax.Value > 99 || c3MedevacMax.Value < c3MedevacMin.Value)
            {
                c3MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c4MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4MedevacMax.Value > 99 || c4MedevacMax.Value < c4MedevacMin.Value)
            {
                c4MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c5MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5MedevacMax.Value > 99 || c5MedevacMax.Value < c5MedevacMin.Value)
            {
                c5MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c6MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6MedevacMax.Value > 99 || c6MedevacMax.Value < c6MedevacMin.Value)
            {
                c6MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c7MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7MedevacMax.Value > 99 || c7MedevacMax.Value < c7MedevacMin.Value)
            {
                c7MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c8MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8MedevacMax.Value > 99 || c8MedevacMax.Value < c8MedevacMin.Value)
            {
                c8MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c9MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9MedevacMax.Value > 99 || c9MedevacMax.Value < c9MedevacMin.Value)
            {
                c9MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c10MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10MedevacMax.Value > 99 || c10MedevacMax.Value < c10MedevacMin.Value)
            {
                c10MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c11MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11MedevacMax.Value > 99 || c11MedevacMax.Value < c11MedevacMin.Value)
            {
                c11MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c12MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12MedevacMax.Value > 99 || c12MedevacMax.Value < c12MedevacMin.Value)
            {
                c12MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c13MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13MedevacMax.Value > 99 || c13MedevacMax.Value < c13MedevacMin.Value)
            {
                c13MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c14MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14MedevacMax.Value > 99 || c14MedevacMax.Value < c14MedevacMin.Value)
            {
                c14MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c15MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15MedevacMax.Value > 99 || c15MedevacMax.Value < c15MedevacMin.Value)
            {
                c15MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c16MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16MedevacMax.Value > 99 || c16MedevacMax.Value < c16MedevacMin.Value)
            {
                c16MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c17MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17MedevacMax.Value > 99 || c17MedevacMax.Value < c17MedevacMin.Value)
            {
                c17MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c18MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18MedevacMax.Value > 99 || c18MedevacMax.Value < c18MedevacMin.Value)
            {
                c18MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c19MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19MedevacMax.Value > 99 || c19MedevacMax.Value < c19MedevacMin.Value)
            {
                c19MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c20MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20MedevacMax.Value > 99 || c20MedevacMax.Value < c20MedevacMin.Value)
            {
                c20MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c21MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21MedevacMax.Value > 99 || c21MedevacMax.Value < c21MedevacMin.Value)
            {
                c21MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c22MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22MedevacMax.Value > 99 || c22MedevacMax.Value < c22MedevacMin.Value)
            {
                c22MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c23MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23MedevacMax.Value > 99 || c23MedevacMax.Value < c23MedevacMin.Value)
            {
                c23MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c24MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24MedevacMax.Value > 99 || c24MedevacMax.Value < c24MedevacMin.Value)
            {
                c24MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c25MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25MedevacMax.Value > 99 || c25MedevacMax.Value < c25MedevacMin.Value)
            {
                c25MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c26MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26MedevacMax.Value > 99 || c26MedevacMax.Value < c26MedevacMin.Value)
            {
                c26MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c27MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27MedevacMax.Value > 99 || c27MedevacMax.Value < c27MedevacMin.Value)
            {
                c27MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c28MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28MedevacMax.Value > 99 || c28MedevacMax.Value < c28MedevacMin.Value)
            {
                c28MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c29MedevacMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29MedevacMax.Value > 99 || c29MedevacMax.Value < c29MedevacMin.Value)
            {
                c29MedevacMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }
        #endregion

        #region transport max
        private void c0TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0TransportMax.Value > 99 || c0TransportMax.Value < c0TransportMin.Value)
            {
                c0TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c1TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1TransportMax.Value > 99 || c1TransportMax.Value < c1TransportMin.Value)
            {
                c1TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c2TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2TransportMax.Value > 99 || c2TransportMax.Value < c2TransportMin.Value)
            {
                c2TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c3TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3TransportMax.Value > 99 || c3TransportMax.Value < c3TransportMin.Value)
            {
                c3TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c4TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4TransportMax.Value > 99 || c4TransportMax.Value < c4TransportMin.Value)
            {
                c4TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c5TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5TransportMax.Value > 99 || c5TransportMax.Value < c5TransportMin.Value)
            {
                c5TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c6TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6TransportMax.Value > 99 || c6TransportMax.Value < c6TransportMin.Value)
            {
                c6TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c7TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7TransportMax.Value > 99 || c7TransportMax.Value < c7TransportMin.Value)
            {
                c7TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c8TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8TransportMax.Value > 99 || c8TransportMax.Value < c8TransportMin.Value)
            {
                c8TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c9TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9TransportMax.Value > 99 || c9TransportMax.Value < c9TransportMin.Value)
            {
                c9TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c10TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10TransportMax.Value > 99 || c10TransportMax.Value < c10TransportMin.Value)
            {
                c10TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c11TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11TransportMax.Value > 99 || c11TransportMax.Value < c11TransportMin.Value)
            {
                c11TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c12TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12TransportMax.Value > 99 || c12TransportMax.Value < c12TransportMin.Value)
            {
                c12TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c13TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13TransportMax.Value > 99 || c13TransportMax.Value < c13TransportMin.Value)
            {
                c13TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c14TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14TransportMax.Value > 99 || c14TransportMax.Value < c14TransportMin.Value)
            {
                c14TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c15TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15TransportMax.Value > 99 || c15TransportMax.Value < c15TransportMin.Value)
            {
                c15TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c16TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16TransportMax.Value > 99 || c16TransportMax.Value < c16TransportMin.Value)
            {
                c16TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c17TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17TransportMax.Value > 99 || c17TransportMax.Value < c17TransportMin.Value)
            {
                c17TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c18TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18TransportMax.Value > 99 || c18TransportMax.Value < c18TransportMin.Value)
            {
                c18TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c19TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19TransportMax.Value > 99 || c19TransportMax.Value < c19TransportMin.Value)
            {
                c19TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c20TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20TransportMax.Value > 99 || c20TransportMax.Value < c20TransportMin.Value)
            {
                c20TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c21TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21TransportMax.Value > 99 || c21TransportMax.Value < c21TransportMin.Value)
            {
                c21TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c22TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22TransportMax.Value > 99 || c22TransportMax.Value < c22TransportMin.Value)
            {
                c22TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c23TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23TransportMax.Value > 99 || c23TransportMax.Value < c23TransportMin.Value)
            {
                c23TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c24TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24TransportMax.Value > 99 || c24TransportMax.Value < c24TransportMin.Value)
            {
                c24TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c25TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25TransportMax.Value > 99 || c25TransportMax.Value < c25TransportMin.Value)
            {
                c25TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c26TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26TransportMax.Value > 99 || c26TransportMax.Value < c26TransportMin.Value)
            {
                c26TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c27TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27TransportMax.Value > 99 || c27TransportMax.Value < c27TransportMin.Value)
            {
                c27TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c28TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28TransportMax.Value > 99 || c28TransportMax.Value < c28TransportMin.Value)
            {
                c28TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }

        private void c29TransportMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29TransportMax.Value > 99 || c29TransportMax.Value < c29TransportMin.Value)
            {
                c29TransportMax.Value = 99;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 99.");
            }
        }
        #endregion

        #region day max
        private void c0DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0DayMax.Value > 1 || c0DayMax.Value < c0DayMin.Value)
            {
                c0DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c1DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1DayMax.Value > 1 || c1DayMax.Value < c1DayMin.Value)
            {
                c1DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c2DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2DayMax.Value > 1 || c2DayMax.Value < c2DayMin.Value)
            {
                c2DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c3DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3DayMax.Value > 1 || c3DayMax.Value < c3DayMin.Value)
            {
                c3DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c4DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4DayMax.Value > 1 || c4DayMax.Value < c4DayMin.Value)
            {
                c4DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c5DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5DayMax.Value > 1 || c5DayMax.Value < c5DayMin.Value)
            {
                c5DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c6DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6DayMax.Value > 1 || c6DayMax.Value < c6DayMin.Value)
            {
                c6DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c7DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7DayMax.Value > 1 || c7DayMax.Value < c7DayMin.Value)
            {
                c7DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c8DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8DayMax.Value > 1 || c8DayMax.Value < c8DayMin.Value)
            {
                c8DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c9DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9DayMax.Value > 1 || c9DayMax.Value < c9DayMin.Value)
            {
                c9DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c10DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10DayMax.Value > 1 || c10DayMax.Value < c10DayMin.Value)
            {
                c10DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c11DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11DayMax.Value > 1 || c11DayMax.Value < c11DayMin.Value)
            {
                c11DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c12DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12DayMax.Value > 1 || c12DayMax.Value < c12DayMin.Value)
            {
                c12DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c13DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13DayMax.Value > 1 || c13DayMax.Value < c13DayMin.Value)
            {
                c13DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c14DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14DayMax.Value > 1 || c14DayMax.Value < c14DayMin.Value)
            {
                c14DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c15DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15DayMax.Value > 1 || c15DayMax.Value < c15DayMin.Value)
            {
                c15DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c16DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16DayMax.Value > 1 || c16DayMax.Value < c16DayMin.Value)
            {
                c16DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c17DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17DayMax.Value > 1 || c17DayMax.Value < c17DayMin.Value)
            {
                c17DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c18DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18DayMax.Value > 1 || c18DayMax.Value < c18DayMin.Value)
            {
                c18DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c19DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19DayMax.Value > 1 || c19DayMax.Value < c19DayMin.Value)
            {
                c19DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c20DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20DayMax.Value > 1 || c20DayMax.Value < c20DayMin.Value)
            {
                c20DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c21DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21DayMax.Value > 1 || c21DayMax.Value < c21DayMin.Value)
            {
                c21DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c22DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22DayMax.Value > 1 || c22DayMax.Value < c22DayMin.Value)
            {
                c22DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c23DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23DayMax.Value > 1 || c23DayMax.Value < c23DayMin.Value)
            {
                c23DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c24DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24DayMax.Value > 1 || c24DayMax.Value < c24DayMin.Value)
            {
                c24DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c25DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25DayMax.Value > 1 || c25DayMax.Value < c25DayMin.Value)
            {
                c25DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c26DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26DayMax.Value > 1 || c26DayMax.Value < c26DayMin.Value)
            {
                c26DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c27DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27DayMax.Value > 1 || c27DayMax.Value < c27DayMin.Value)
            {
                c27DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c28DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28DayMax.Value > 1 || c28DayMax.Value < c28DayMin.Value)
            {
                c28DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void c29DayMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29DayMax.Value > 1 || c29DayMax.Value < c29DayMin.Value)
            {
                c29DayMax.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }
        #endregion

        #region points max
        private void c0PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0PointsMax.Value > 5000 || c0PointsMax.Value < c0PointsMin.Value)
            {
                c0PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c1PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1PointsMax.Value > 5000 || c1PointsMax.Value < c1PointsMin.Value)
            {
                c1PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c2PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2PointsMax.Value > 5000 || c2PointsMax.Value < c2PointsMin.Value)
            {
                c2PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c3PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3PointsMax.Value > 5000 || c3PointsMax.Value < c3PointsMin.Value)
            {
                c3PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c4PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4PointsMax.Value > 5000 || c4PointsMax.Value < c4PointsMin.Value)
            {
                c4PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c5PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5PointsMax.Value > 5000 || c5PointsMax.Value < c5PointsMin.Value)
            {
                c5PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c6PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6PointsMax.Value > 5000 || c6PointsMax.Value < c6PointsMin.Value)
            {
                c6PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c7PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7PointsMax.Value > 5000 || c7PointsMax.Value < c7PointsMin.Value)
            {
                c7PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c8PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8PointsMax.Value > 5000 || c8PointsMax.Value < c8PointsMin.Value)
            {
                c8PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c9PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9PointsMax.Value > 5000 || c9PointsMax.Value < c9PointsMin.Value)
            {
                c9PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c10PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10PointsMax.Value > 5000 || c10PointsMax.Value < c10PointsMin.Value)
            {
                c10PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c11PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11PointsMax.Value > 5000 || c11PointsMax.Value < c11PointsMin.Value)
            {
                c11PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c12PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12PointsMax.Value > 5000 || c12PointsMax.Value < c12PointsMin.Value)
            {
                c12PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c13PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13PointsMax.Value > 5000 || c13PointsMax.Value < c13PointsMin.Value)
            {
                c13PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c14PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14PointsMax.Value > 5000 || c14PointsMax.Value < c14PointsMin.Value)
            {
                c14PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c15PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15PointsMax.Value > 5000 || c15PointsMax.Value < c15PointsMin.Value)
            {
                c15PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c16PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16PointsMax.Value > 5000 || c16PointsMax.Value < c16PointsMin.Value)
            {
                c16PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c17PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17PointsMax.Value > 5000 || c17PointsMax.Value < c17PointsMin.Value)
            {
                c17PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c18PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18PointsMax.Value > 5000 || c18PointsMax.Value < c18PointsMin.Value)
            {
                c18PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c19PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19PointsMax.Value > 5000 || c19PointsMax.Value < c19PointsMin.Value)
            {
                c19PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c20PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20PointsMax.Value > 5000 || c20PointsMax.Value < c20PointsMin.Value)
            {
                c20PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c21PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21PointsMax.Value > 5000 || c21PointsMax.Value < c21PointsMin.Value)
            {
                c21PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c22PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22PointsMax.Value > 5000 || c22PointsMax.Value < c22PointsMin.Value)
            {
                c22PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c23PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23PointsMax.Value > 5000 || c23PointsMax.Value < c23PointsMin.Value)
            {
                c23PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c24PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24PointsMax.Value > 5000 || c24PointsMax.Value < c24PointsMin.Value)
            {
                c24PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c25PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25PointsMax.Value > 5000 || c25PointsMax.Value < c25PointsMin.Value)
            {
                c25PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c26PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26PointsMax.Value > 5000 || c26PointsMax.Value < c26PointsMin.Value)
            {
                c26PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c27PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27PointsMax.Value > 5000 || c27PointsMax.Value < c27PointsMin.Value)
            {
                c27PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c28PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28PointsMax.Value > 5000 || c28PointsMax.Value < c28PointsMin.Value)
            {
                c28PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void c29PointsMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29PointsMax.Value > 5000 || c29PointsMax.Value < c29PointsMin.Value)
            {
                c29PointsMax.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }
        #endregion

        #region money max
        private void c0MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c0MoneyMax.Value > 2000 || c0MoneyMax.Value < c0MoneyMin.Value)
            {
                c0MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c1MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c1MoneyMax.Value > 2000 || c1MoneyMax.Value < c1MoneyMin.Value)
            {
                c1MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c2MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c2MoneyMax.Value > 2000 || c2MoneyMax.Value < c2MoneyMin.Value)
            {
                c2MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c3MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c3MoneyMax.Value > 2000 || c3MoneyMax.Value < c3MoneyMin.Value)
            {
                c3MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c4MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c4MoneyMax.Value > 2000 || c4MoneyMax.Value < c4MoneyMin.Value)
            {
                c4MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c5MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c5MoneyMax.Value > 2000 || c5MoneyMax.Value < c5MoneyMin.Value)
            {
                c5MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c6MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c6MoneyMax.Value > 2000 || c6MoneyMax.Value < c6MoneyMin.Value)
            {
                c6MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c7MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c7MoneyMax.Value > 2000 || c7MoneyMax.Value < c7MoneyMin.Value)
            {
                c7MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c8MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c8MoneyMax.Value > 2000 || c8MoneyMax.Value < c8MoneyMin.Value)
            {
                c8MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c9MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c9MoneyMax.Value > 2000 || c9MoneyMax.Value < c9MoneyMin.Value)
            {
                c9MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c10MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c10MoneyMax.Value > 2000 || c10MoneyMax.Value < c10MoneyMin.Value)
            {
                c10MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c11MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c11MoneyMax.Value > 2000 || c11MoneyMax.Value < c11MoneyMin.Value)
            {
                c11MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c12MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c12MoneyMax.Value > 2000 || c12MoneyMax.Value < c12MoneyMin.Value)
            {
                c12MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c13MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c13MoneyMax.Value > 2000 || c13MoneyMax.Value < c13MoneyMin.Value)
            {
                c13MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c14MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c14MoneyMax.Value > 2000 || c14MoneyMax.Value < c14MoneyMin.Value)
            {
                c14MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c15MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c15MoneyMax.Value > 2000 || c15MoneyMax.Value < c15MoneyMin.Value)
            {
                c15MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c16MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c16MoneyMax.Value > 2000 || c16MoneyMax.Value < c16MoneyMin.Value)
            {
                c16MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c17MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c17MoneyMax.Value > 2000 || c17MoneyMax.Value < c17MoneyMin.Value)
            {
                c17MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c18MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c18MoneyMax.Value > 2000 || c18MoneyMax.Value < c18MoneyMin.Value)
            {
                c18MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c19MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c19MoneyMax.Value > 2000 || c19MoneyMax.Value < c19MoneyMin.Value)
            {
                c19MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c20MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c20MoneyMax.Value > 2000 || c20MoneyMax.Value < c20MoneyMin.Value)
            {
                c20MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c21MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c21MoneyMax.Value > 2000 || c21MoneyMax.Value < c21MoneyMin.Value)
            {
                c21MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c22MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c22MoneyMax.Value > 2000 || c22MoneyMax.Value < c22MoneyMin.Value)
            {
                c22MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c23MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c23MoneyMax.Value > 2000 || c23MoneyMax.Value < c23MoneyMin.Value)
            {
                c23MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c24MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c24MoneyMax.Value > 2000 || c24MoneyMax.Value < c24MoneyMin.Value)
            {
                c24MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c25MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c25MoneyMax.Value > 2000 || c25MoneyMax.Value < c25MoneyMin.Value)
            {
                c25MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c26MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c26MoneyMax.Value > 2000 || c26MoneyMax.Value < c26MoneyMin.Value)
            {
                c26MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c27MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c27MoneyMax.Value > 2000 || c27MoneyMax.Value < c27MoneyMin.Value)
            {
                c27MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c28MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c28MoneyMax.Value > 2000 || c28MoneyMax.Value < c28MoneyMin.Value)
            {
                c28MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void c29MoneyMax_ValueChanged(object sender, EventArgs e)
        {
            if (c29MoneyMax.Value > 2000 || c29MoneyMax.Value < c29MoneyMin.Value)
            {
                c29MoneyMax.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }
        #endregion

        #endregion

        #endregion

        private void mCustom_CheckedChanged(object sender, EventArgs e)
        {
            missionTabs.Enabled = true;
            mCheckOffButt.Enabled = true;
            mCheckOnButt.Enabled = true;
        }

        private void mChaos_CheckedChanged(object sender, EventArgs e)
        {
            missionTabs.Enabled = false;
            mCheckOffButt.Enabled = false;
            mCheckOnButt.Enabled = false;
        }

        #region missions

        #region general

        #region check
        private void genCheck1_CheckedChanged(object sender, EventArgs e)
        {
            genMin1.Enabled = genCheck1.Checked;
            genMax1.Enabled = genCheck1.Checked;
        }

        private void genCheck2_CheckedChanged(object sender, EventArgs e)
        {
            genMin2.Enabled = genCheck2.Checked;
            genMax2.Enabled = genCheck2.Checked;
        }

        private void genCheck3_CheckedChanged(object sender, EventArgs e)
        {
            genMin3.Enabled = genCheck3.Checked;
            genMax3.Enabled = genCheck3.Checked;
        }

        private void genCheck4_CheckedChanged(object sender, EventArgs e)
        {
            genMin4.Enabled = genCheck4.Checked;
            genMax4.Enabled = genCheck4.Checked;
        }

        private void genCheck5_CheckedChanged(object sender, EventArgs e)
        {
            genMin5.Enabled = genCheck5.Checked;
            genMax5.Enabled = genCheck5.Checked;
        }



        #endregion

        #region min
        private void genMin1_ValueChanged(object sender, EventArgs e)
        {
            if (genMin1.Value > genMax1.Value)
            {
                genMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void genMin2_ValueChanged(object sender, EventArgs e)
        {
            if (genMin2.Value > genMax2.Value)
            {
                genMin2.Value = 100;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 100.");
            }
        }

        private void genMin3_ValueChanged(object sender, EventArgs e)
        {
            if (genMin3.Value > genMax3.Value)
            {
                genMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void genMin4_ValueChanged(object sender, EventArgs e)
        {
            if (genMin4.Value > genMax4.Value)
            {
                genMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void genMin5_ValueChanged(object sender, EventArgs e)
        {
            if (genMin5.Value > genMax5.Value)
            {
                genMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }




        #endregion

        #region max
        private void genMax1_ValueChanged(object sender, EventArgs e)
        {
            if (genMax1.Value < genMin1.Value)
            {
                genMax1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void genMax2_ValueChanged(object sender, EventArgs e)
        {
            if (genMax2.Value < genMin2.Value)
            {
                genMax2.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        private void genMax3_ValueChanged(object sender, EventArgs e)
        {
            if (genMax3.Value < genMin3.Value)
            {
                genMax3.Value = 2;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2.");
            }
        }

        private void genMax4_ValueChanged(object sender, EventArgs e)
        {
            if (genMax4.Value < genMin4.Value)
            {
                genMax4.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void genMax5_ValueChanged(object sender, EventArgs e)
        {
            if (genMax5.Value < genMin5.Value)
            {
                genMax5.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }




        #endregion

        #endregion

        #region fire

        #region check

        private void mFireCheck1_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin1.Enabled = mFireCheck1.Checked;
            mFireMax1.Enabled = mFireCheck1.Checked;
        }

        private void mFireCheck2_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin2.Enabled = mFireCheck2.Checked;
            mFireMax2.Enabled = mFireCheck2.Checked;
        }

        private void mFireCheck3_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin3.Enabled = mFireCheck3.Checked;
            mFireMax3.Enabled = mFireCheck3.Checked;
        }

        private void mFireCheck4_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin4.Enabled = mFireCheck4.Checked;
            mFireMax4.Enabled = mFireCheck4.Checked;
        }

        private void mFireCheck5_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin5.Enabled = mFireCheck5.Checked;
            mFireMax5.Enabled = mFireCheck5.Checked;
        }

        private void mFireCheck6_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin6.Enabled = mFireCheck6.Checked;
            mFireMax6.Enabled = mFireCheck6.Checked;
        }

        private void mFireCheck7_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin7.Enabled = mFireCheck7.Checked;
            mFireMax7.Enabled = mFireCheck7.Checked;
        }

        private void mFireCheck8_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin8.Enabled = mFireCheck8.Checked;
            mFireMax8.Enabled = mFireCheck8.Checked;
        }

        private void mFireCheck9_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin9.Enabled = mFireCheck9.Checked;
            mFireMax9.Enabled = mFireCheck9.Checked;
        }

        private void mFireCheck10_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin10.Enabled = mFireCheck10.Checked;
            mFireMax10.Enabled = mFireCheck10.Checked;
        }

        private void mFireCheck11_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin11.Enabled = mFireCheck11.Checked;
            mFireMax11.Enabled = mFireCheck11.Checked;
        }

        private void mFireCheck12_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin12.Enabled = mFireCheck12.Checked;
            mFireMax12.Enabled = mFireCheck12.Checked;
        }

        private void mFireCheck13_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin11.Enabled = mFireCheck11.Checked;
            mFireMax11.Enabled = mFireCheck11.Checked;
        }

        private void mFireCheck14_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin12.Enabled = mFireCheck12.Checked;
            mFireMax12.Enabled = mFireCheck12.Checked;
        }

        private void mFireCheck15_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin13.Enabled = mFireCheck13.Checked;
            mFireMax13.Enabled = mFireCheck13.Checked;
        }

        private void mFireCheck16_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin14.Enabled = mFireCheck14.Checked;
            mFireMax14.Enabled = mFireCheck14.Checked;
        }

        private void mFireCheck17_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin15.Enabled = mFireCheck15.Checked;
            mFireMax15.Enabled = mFireCheck15.Checked;
        }

        private void mFireCheck18_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin16.Enabled = mFireCheck16.Checked;
            mFireMax16.Enabled = mFireCheck16.Checked;
        }

        private void mFireCheck19_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin17.Enabled = mFireCheck17.Checked;
            mFireMax17.Enabled = mFireCheck17.Checked;
        }

        private void mFireCheck20_CheckedChanged(object sender, EventArgs e)
        {
            mFireMin18.Enabled = mFireCheck18.Checked;
            mFireMax18.Enabled = mFireCheck18.Checked;
        }



        #endregion

        #region min

        private void mFireMin1_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin1.Value > mFireMax1.Value)
            {
                mFireMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin2_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin2.Value > mFireMax2.Value)
            {
                mFireMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin3_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin3.Value > mFireMax3.Value)
            {
                mFireMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin4_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin4.Value > mFireMax4.Value)
            {
                mFireMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin5_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin5.Value > mFireMax5.Value)
            {
                mFireMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin6_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin6.Value > mFireMax6.Value)
            {
                mFireMin6.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin7_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin7.Value > mFireMax7.Value)
            {
                mFireMin7.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin8_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin8.Value > mFireMax8.Value)
            {
                mFireMin8.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin9_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin9.Value > mFireMax9.Value)
            {
                mFireMin9.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin10_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin10.Value > mFireMax10.Value)
            {
                mFireMin10.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin11_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin11.Value > mFireMax11.Value)
            {
                mFireMin11.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin12_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin12.Value > mFireMax12.Value)
            {
                mFireMin12.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin13_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin11.Value > mFireMax11.Value)
            {
                mFireMin11.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin14_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin12.Value > mFireMax12.Value)
            {
                mFireMin12.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin15_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin13.Value > mFireMax13.Value)
            {
                mFireMin13.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin16_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin14.Value > mFireMax14.Value)
            {
                mFireMin14.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin17_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin15.Value > mFireMax15.Value)
            {
                mFireMin15.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin18_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin16.Value > mFireMax16.Value)
            {
                mFireMin16.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin19_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin17.Value > mFireMax17.Value)
            {
                mFireMin17.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mFireMin20_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMin18.Value > mFireMax18.Value)
            {
                mFireMin18.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }


        #endregion

        #region max

        private void mFireMax1_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax1.Value < mFireMin1.Value)
            {
                mFireMax1.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax2_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax2.Value < mFireMin2.Value)
            {
                mFireMax2.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax3_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax3.Value < mFireMin3.Value)
            {
                mFireMax3.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        private void mFireMax4_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax4.Value < mFireMin4.Value)
            {
                mFireMax4.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        private void mFireMax5_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax5.Value < mFireMin5.Value)
            {
                mFireMax5.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax6_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax6.Value < mFireMin6.Value)
            {
                mFireMax6.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax7_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax7.Value < mFireMin7.Value)
            {
                mFireMax7.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax8_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax8.Value < mFireMin8.Value)
            {
                mFireMax8.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax9_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax9.Value < mFireMin9.Value)
            {
                mFireMax9.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax10_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax10.Value < mFireMin10.Value)
            {
                mFireMax10.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax11_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax11.Value < mFireMin11.Value)
            {
                mFireMax11.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax12_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax12.Value < mFireMin12.Value)
            {
                mFireMax12.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax13_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax11.Value < mFireMin11.Value)
            {
                mFireMax11.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void mFireMax14_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax12.Value < mFireMin12.Value)
            {
                mFireMax12.Value = 200;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 200.");
            }
        }

        private void mFireMax15_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax13.Value < mFireMin13.Value)
            {
                mFireMax13.Value = 200;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 200.");
            }
        }

        private void mFireMax16_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax14.Value < mFireMin14.Value)
            {
                mFireMax14.Value = 200;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 200.");
            }
        }

        private void mFireMax17_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax15.Value < mFireMin15.Value)
            {
                mFireMax15.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        private void mFireMax18_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax16.Value < mFireMin16.Value)
            {
                mFireMax16.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        private void mFireMax19_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax17.Value < mFireMin17.Value)
            {
                mFireMax17.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        private void mFireMax20_ValueChanged(object sender, EventArgs e)
        {
            if (mFireMax18.Value < mFireMin18.Value)
            {
                mFireMax18.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        #endregion

        #endregion

        #region crime

        #region check
        private void crimeCheck1_CheckedChanged(object sender, EventArgs e)
        {
            crimeMin1.Enabled = crimeCheck1.Checked;
            crimeMax1.Enabled = crimeCheck1.Checked;
        }

        private void crimeCheck2_CheckedChanged(object sender, EventArgs e)
        {
            crimeMin2.Enabled = crimeCheck2.Checked;
            crimeMax2.Enabled = crimeCheck2.Checked;
        }


        #endregion

        #region min

        private void crimeMin1_ValueChanged(object sender, EventArgs e)
        {
            if (crimeMin1.Value > crimeMax1.Value)
            {
                crimeMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void crimeMin2_ValueChanged(object sender, EventArgs e)
        {
            if (crimeMin2.Value > crimeMax2.Value)
            {
                crimeMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }


        #endregion

        #region max

        private void crimeMax1_ValueChanged(object sender, EventArgs e)
        {
            if (crimeMax1.Value < crimeMin1.Value)
            {
                crimeMax1.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void crimeMax2_ValueChanged(object sender, EventArgs e)
        {
            if (crimeMax2.Value < crimeMin2.Value)
            {
                crimeMax2.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        #endregion

        #endregion

        #region rescue

        #region check

        private void rescueCheck1_CheckedChanged(object sender, EventArgs e)
        {
            rescueMin1.Enabled = rescueCheck1.Checked;
            rescueMax1.Enabled = rescueCheck1.Checked;
        }

        private void rescueCheck2_CheckedChanged(object sender, EventArgs e)
        {
            rescueMin2.Enabled = rescueCheck2.Checked;
            rescueMax2.Enabled = rescueCheck2.Checked;
        }

        private void rescueCheck3_CheckedChanged(object sender, EventArgs e)
        {
            rescueMin3.Enabled = rescueCheck3.Checked;
            rescueMax3.Enabled = rescueCheck3.Checked;
        }

        #endregion

        #region min

        private void rescueMin1_ValueChanged(object sender, EventArgs e)
        {
            if (rescueMin1.Value > rescueMax1.Value)
            {
                rescueMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void rescueMin2_ValueChanged(object sender, EventArgs e)
        {
            if (rescueMin2.Value > rescueMax2.Value)
            {
                rescueMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void rescueMin3_ValueChanged(object sender, EventArgs e)
        {
            if (rescueMin3.Value > rescueMax3.Value)
            {
                rescueMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region max

        private void rescueMax1_ValueChanged(object sender, EventArgs e)
        {
            if (rescueMax1.Value < rescueMin1.Value)
            {
                rescueMax1.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void rescueMax2_ValueChanged(object sender, EventArgs e)
        {
            if (rescueMax2.Value < rescueMin2.Value)
            {
                rescueMax2.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void rescueMax3_ValueChanged(object sender, EventArgs e)
        {
            if (rescueMax3.Value < rescueMin3.Value)
            {
                rescueMax3.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        #endregion

        #endregion

        #region riot

        #region check

        private void riotCheck1_CheckedChanged(object sender, EventArgs e)
        {
            riotMin1.Enabled = riotCheck1.Checked;
            riotMax1.Enabled = riotCheck1.Checked;
        }

        private void riotCheck2_CheckedChanged(object sender, EventArgs e)
        {
            riotMin2.Enabled = riotCheck2.Checked;
            riotMax2.Enabled = riotCheck2.Checked;
        }

        private void riotCheck3_CheckedChanged(object sender, EventArgs e)
        {
            riotMin3.Enabled = riotCheck3.Checked;
            riotMax3.Enabled = riotCheck3.Checked;
        }

        private void riotCheck4_CheckedChanged(object sender, EventArgs e)
        {
            riotMin4.Enabled = riotCheck4.Checked;
            riotMax4.Enabled = riotCheck4.Checked;
        }

        private void riotCheck5_CheckedChanged(object sender, EventArgs e)
        {
            riotMin5.Enabled = riotCheck5.Checked;
            riotMax5.Enabled = riotCheck5.Checked;
        }

        #endregion

        #region min

        private void riotMin1_ValueChanged(object sender, EventArgs e)
        {
            if (riotMin1.Value > riotMax1.Value)
            {
                riotMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void riotMin2_ValueChanged(object sender, EventArgs e)
        {
            if (riotMin2.Value > riotMax2.Value)
            {
                riotMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void riotMin3_ValueChanged(object sender, EventArgs e)
        {
            if (riotMin3.Value > riotMax3.Value)
            {
                riotMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void riotMin4_ValueChanged(object sender, EventArgs e)
        {
            if (riotMin4.Value > riotMax4.Value)
            {
                riotMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void riotMin5_ValueChanged(object sender, EventArgs e)
        {
            if (riotMin5.Value > riotMax5.Value)
            {
                riotMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region max

        private void riotMax1_ValueChanged(object sender, EventArgs e)
        {
            if (riotMax1.Value < riotMin1.Value)
            {
                riotMax1.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void riotMax2_ValueChanged(object sender, EventArgs e)
        {
            if (riotMax2.Value < riotMin2.Value)
            {
                riotMax2.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void riotMax3_ValueChanged(object sender, EventArgs e)
        {
            if (riotMax3.Value < riotMin3.Value)
            {
                riotMax3.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        private void riotMax4_ValueChanged(object sender, EventArgs e)
        {
            if (riotMax4.Value < riotMin4.Value)
            {
                riotMax4.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        private void riotMax5_ValueChanged(object sender, EventArgs e)
        {
            if (riotMax5.Value < riotMin5.Value)
            {
                riotMax5.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        #endregion

        #endregion

        #region traffic

        #region check

        private void trafficCheck1_CheckedChanged(object sender, EventArgs e)
        {
            trafficMin1.Enabled = trafficCheck1.Checked;
            trafficMax1.Enabled = trafficCheck1.Checked;
        }

        private void trafficCheck2_CheckedChanged(object sender, EventArgs e)
        {
            trafficMin2.Enabled = trafficCheck2.Checked;
            trafficMax2.Enabled = trafficCheck2.Checked;
        }

        private void trafficCheck3_CheckedChanged(object sender, EventArgs e)
        {
            trafficMin3.Enabled = trafficCheck3.Checked;
            trafficMax3.Enabled = trafficCheck3.Checked;
        }

        #endregion

        #region min

        private void trafficMin1_ValueChanged(object sender, EventArgs e)
        {
            if (trafficMin1.Value > trafficMax1.Value)
            {
                trafficMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void trafficMin2_ValueChanged(object sender, EventArgs e)
        {
            if (trafficMin2.Value > trafficMax2.Value)
            {
                trafficMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void trafficMin3_ValueChanged(object sender, EventArgs e)
        {
            if (trafficMin3.Value > trafficMax3.Value)
            {
                trafficMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region max

        private void trafficMax1_ValueChanged(object sender, EventArgs e)
        {
            if (trafficMax1.Value < trafficMin1.Value)
            {
                trafficMax1.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        private void trafficMax2_ValueChanged(object sender, EventArgs e)
        {
            if (trafficMax2.Value < trafficMin2.Value)
            {
                trafficMax2.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void trafficMax3_ValueChanged(object sender, EventArgs e)
        {
            if (trafficMax3.Value < trafficMin3.Value)
            {
                trafficMax3.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        #endregion

        #endregion

        #region medevac

        #region check

        private void medevacCheck1_CheckedChanged(object sender, EventArgs e)
        {
            medevacMin1.Enabled = medevacCheck1.Checked;
            medevacMax1.Enabled = medevacCheck1.Checked;
        }

        private void medevacCheck2_CheckedChanged(object sender, EventArgs e)
        {
            medevacMin2.Enabled = medevacCheck2.Checked;
            medevacMax2.Enabled = medevacCheck2.Checked;
        }

        private void medevacCheck3_CheckedChanged(object sender, EventArgs e)
        {
            medevacMin3.Enabled = medevacCheck3.Checked;
            medevacMax3.Enabled = medevacCheck3.Checked;
        }

        #endregion

        #region min

        private void medevacMin1_ValueChanged(object sender, EventArgs e)
        {
            if (medevacMin1.Value > medevacMax1.Value)
            {
                medevacMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void medevacMin2_ValueChanged(object sender, EventArgs e)
        {
            if (medevacMin2.Value > medevacMax2.Value)
            {
                medevacMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void medevacMin3_ValueChanged(object sender, EventArgs e)
        {
            if (medevacMin3.Value > medevacMax3.Value)
            {
                medevacMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region max

        private void medevacMax1_ValueChanged(object sender, EventArgs e)
        {
            if (medevacMax1.Value < medevacMin1.Value)
            {
                medevacMax1.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void medevacMax2_ValueChanged(object sender, EventArgs e)
        {
            if (medevacMax2.Value < medevacMin2.Value)
            {
                medevacMax2.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void medevacMax3_ValueChanged(object sender, EventArgs e)
        {
            if (medevacMax3.Value < medevacMin3.Value)
            {
                medevacMax3.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        #endregion

        #endregion

        #region transport

        #region check

        private void transportCheck1_CheckedChanged(object sender, EventArgs e)
        {
            transportMin1.Enabled = transportCheck1.Checked;
            transportMax1.Enabled = transportCheck1.Checked;
        }

        private void transportCheck2_CheckedChanged(object sender, EventArgs e)
        {
            transportMin2.Enabled = transportCheck2.Checked;
            transportMax2.Enabled = transportCheck2.Checked;
        }

        private void transportCheck3_CheckedChanged(object sender, EventArgs e)
        {
            transportMin3.Enabled = transportCheck3.Checked;
            transportMax3.Enabled = transportCheck3.Checked;
        }

        private void transportCheck4_CheckedChanged(object sender, EventArgs e)
        {
            transportMin4.Enabled = transportCheck4.Checked;
            transportMax4.Enabled = transportCheck4.Checked;
        }

        #endregion

        #region min

        private void transportMin1_ValueChanged(object sender, EventArgs e)
        {
            if (transportMin1.Value > transportMax1.Value)
            {
                transportMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void transportMin2_ValueChanged(object sender, EventArgs e)
        {
            if (transportMin2.Value > transportMax2.Value)
            {
                transportMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void transportMin3_ValueChanged(object sender, EventArgs e)
        {
            if (transportMin3.Value > transportMax3.Value)
            {
                transportMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void transportMin4_ValueChanged(object sender, EventArgs e)
        {
            if (transportMin4.Value > transportMax4.Value)
            {
                transportMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region max

        private void transportMax1_ValueChanged(object sender, EventArgs e)
        {
            if (transportMax1.Value < transportMin1.Value)
            {
                transportMax1.Value = 200;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 200.");
            }
        }

        private void transportMax2_ValueChanged(object sender, EventArgs e)
        {
            if (transportMax2.Value < transportMin2.Value)
            {
                transportMax2.Value = 200;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 200.");
            }
        }

        private void transportMax3_ValueChanged(object sender, EventArgs e)
        {
            if (transportMax3.Value < transportMin3.Value)
            {
                transportMax3.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        private void transportMax4_ValueChanged(object sender, EventArgs e)
        {
            if (transportMax4.Value < transportMin4.Value)
            {
                transportMax4.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        #endregion

        #endregion

        #region speeder

        #region check

        private void speedCheck1_CheckedChanged(object sender, EventArgs e)
        {
            speedMin1.Enabled = speedCheck1.Checked;
            speedMax1.Enabled = speedCheck1.Checked;
        }

        private void speedCheck2_CheckedChanged(object sender, EventArgs e)
        {
            speedMin2.Enabled = speedCheck2.Checked;
            speedMax2.Enabled = speedCheck2.Checked;
        }

        private void speedCheck3_CheckedChanged(object sender, EventArgs e)
        {
            speedMin3.Enabled = speedCheck3.Checked;
            speedMax3.Enabled = speedCheck3.Checked;
        }

        #endregion

        #region min

        private void speedMin1_ValueChanged(object sender, EventArgs e)
        {
            if (speedMin1.Value > speedMax1.Value)
            {
                speedMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void speedMin2_ValueChanged(object sender, EventArgs e)
        {
            if (speedMin2.Value > speedMax2.Value)
            {
                speedMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void speedMin3_ValueChanged(object sender, EventArgs e)
        {
            if (speedMin3.Value > speedMax3.Value)
            {
                speedMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region max

        private void speedMax1_ValueChanged(object sender, EventArgs e)
        {
            if (speedMax1.Value < speedMin1.Value)
            {
                speedMax1.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void speedMax2_ValueChanged(object sender, EventArgs e)
        {
            if (speedMax2.Value < speedMin2.Value)
            {
                speedMax2.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void speedMax3_ValueChanged(object sender, EventArgs e)
        {
            if (speedMax3.Value < speedMin3.Value)
            {
                speedMax3.Value = 20;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20.");
            }
        }

        #endregion

        #endregion

        #endregion

        private void hCustom_CheckedChanged(object sender, EventArgs e)
        {
            heliTabs.Enabled = true;
            hCheckOffButt.Enabled = true;
            hCheckOnButt.Enabled = true;
        }

        private void hChaos_CheckedChanged(object sender, EventArgs e)
        {
            heliTabs.Enabled = false;
            hCheckOffButt.Enabled = false;
            hCheckOnButt.Enabled = false;
        }


        #region helis

        #region copters

        #region check

        #region mbank

        private void mBankCheck0_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin0.Enabled = mBankCheck0.Checked;
            mBankMin0.Enabled = mBankCheck0.Checked;
        }

        private void mBankCheck1_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin1.Enabled = mBankCheck1.Checked;
            mBankMin1.Enabled = mBankCheck1.Checked;
        }

        private void mBankCheck2_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin2.Enabled = mBankCheck2.Checked;
            mBankMin2.Enabled = mBankCheck2.Checked;
        }

        private void mBankCheck3_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin3.Enabled = mBankCheck3.Checked;
            mBankMin3.Enabled = mBankCheck3.Checked;
        }

        private void mBankCheck4_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin4.Enabled = mBankCheck4.Checked;
            mBankMin4.Enabled = mBankCheck4.Checked;
        }

        private void mBankCheck5_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin5.Enabled = mBankCheck5.Checked;
            mBankMin5.Enabled = mBankCheck5.Checked;
        }

        private void mBankCheck6_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin6.Enabled = mBankCheck6.Checked;
            mBankMin6.Enabled = mBankCheck6.Checked;
        }

        private void mBankCheck7_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin7.Enabled = mBankCheck7.Checked;
            mBankMin7.Enabled = mBankCheck7.Checked;
        }

        private void mBankCheck8_CheckedChanged(object sender, EventArgs e)
        {
            mBankMin8.Enabled = mBankCheck8.Checked;
            mBankMin8.Enabled = mBankCheck8.Checked;
        }

        #endregion

        #region mslide

        private void mSlideCheck0_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin0.Enabled = mSlideCheck0.Checked;
            mSlideMax0.Enabled = mSlideCheck0.Checked;
        }

        private void mSlideCheck1_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin1.Enabled = mSlideCheck1.Checked;
            mSlideMax1.Enabled = mSlideCheck1.Checked;
        }

        private void mSlideCheck2_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin2.Enabled = mSlideCheck2.Checked;
            mSlideMax2.Enabled = mSlideCheck2.Checked;
        }

        private void mSlideCheck3_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin3.Enabled = mSlideCheck3.Checked;
            mSlideMax3.Enabled = mSlideCheck3.Checked;
        }

        private void mSlideCheck4_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin4.Enabled = mSlideCheck4.Checked;
            mSlideMax4.Enabled = mSlideCheck4.Checked;
        }

        private void mSlideCheck5_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin5.Enabled = mSlideCheck5.Checked;
            mSlideMax5.Enabled = mSlideCheck5.Checked;
        }

        private void mSlideCheck6_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin6.Enabled = mSlideCheck6.Checked;
            mSlideMax6.Enabled = mSlideCheck6.Checked;
        }

        private void mSlideCheck7_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin7.Enabled = mSlideCheck7.Checked;
            mSlideMax7.Enabled = mSlideCheck7.Checked;
        }

        private void mSlideCheck8_CheckedChanged(object sender, EventArgs e)
        {
            mSlideMin8.Enabled = mSlideCheck8.Checked;
            mSlideMax8.Enabled = mSlideCheck8.Checked;
        }


        #endregion

        #region mpitch

        private void mPitchCheck0_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin0.Enabled = mPitchCheck0.Checked;
            mPitchMax0.Enabled = mPitchCheck0.Checked;
        }

        private void mPitchCheck1_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin1.Enabled = mPitchCheck1.Checked;
            mPitchMax1.Enabled = mPitchCheck1.Checked;
        }

        private void mPitchCheck2_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin2.Enabled = mPitchCheck2.Checked;
            mPitchMax2.Enabled = mPitchCheck2.Checked;
        }

        private void mPitchCheck3_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin3.Enabled = mPitchCheck3.Checked;
            mPitchMax3.Enabled = mPitchCheck3.Checked;
        }

        private void mPitchCheck4_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin4.Enabled = mPitchCheck4.Checked;
            mPitchMax4.Enabled = mPitchCheck4.Checked;
        }

        private void mPitchCheck5_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin5.Enabled = mPitchCheck5.Checked;
            mPitchMax5.Enabled = mPitchCheck5.Checked;
        }

        private void mPitchCheck6_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin6.Enabled = mPitchCheck6.Checked;
            mPitchMax6.Enabled = mPitchCheck6.Checked;
        }

        private void mPitchCheck7_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin7.Enabled = mPitchCheck7.Checked;
            mPitchMax7.Enabled = mPitchCheck7.Checked;
        }

        private void mPitchCheck8_CheckedChanged(object sender, EventArgs e)
        {
            mPitchMin8.Enabled = mPitchCheck8.Checked;
            mPitchMax8.Enabled = mPitchCheck8.Checked;
        }

        #endregion

        #region pitchrate

        private void pitchRateCheck0_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin0.Enabled = pitchRateCheck0.Checked;
            pitchRateMax0.Enabled = pitchRateCheck0.Checked;
        }

        private void pitchRateCheck1_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin1.Enabled = pitchRateCheck1.Checked;
            pitchRateMax1.Enabled = pitchRateCheck1.Checked;
        }

        private void pitchRateCheck2_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin2.Enabled = pitchRateCheck2.Checked;
            pitchRateMax2.Enabled = pitchRateCheck2.Checked;
        }

        private void pitchRateCheck3_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin3.Enabled = pitchRateCheck3.Checked;
            pitchRateMax3.Enabled = pitchRateCheck3.Checked;
        }

        private void pitchRateCheck4_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin4.Enabled = pitchRateCheck4.Checked;
            pitchRateMax4.Enabled = pitchRateCheck4.Checked;
        }

        private void pitchRateCheck5_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin5.Enabled = pitchRateCheck5.Checked;
            pitchRateMax5.Enabled = pitchRateCheck5.Checked;
        }

        private void pitchRateCheck6_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin6.Enabled = pitchRateCheck6.Checked;
            pitchRateMax6.Enabled = pitchRateCheck6.Checked;
        }

        private void pitchRateCheck7_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin7.Enabled = pitchRateCheck7.Checked;
            pitchRateMax7.Enabled = pitchRateCheck7.Checked;
        }

        private void pitchRateCheck8_CheckedChanged(object sender, EventArgs e)
        {
            pitchRateMin8.Enabled = pitchRateCheck8.Checked;
            pitchRateMax8.Enabled = pitchRateCheck8.Checked;
        }

        #endregion

        #region yawrate

        private void yawRateCheck0_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin0.Enabled = yawRateCheck0.Checked;
            yawRateMax0.Enabled = yawRateCheck0.Checked;
        }

        private void yawRateCheck1_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin1.Enabled = yawRateCheck1.Checked;
            yawRateMax1.Enabled = yawRateCheck1.Checked;
        }

        private void yawRateCheck2_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin2.Enabled = yawRateCheck2.Checked;
            yawRateMax2.Enabled = yawRateCheck2.Checked;
        }

        private void yawRateCheck3_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin3.Enabled = yawRateCheck3.Checked;
            yawRateMax3.Enabled = yawRateCheck3.Checked;
        }

        private void yawRateCheck4_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin4.Enabled = yawRateCheck4.Checked;
            yawRateMax4.Enabled = yawRateCheck4.Checked;
        }

        private void yawRateCheck5_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin5.Enabled = yawRateCheck5.Checked;
            yawRateMax5.Enabled = yawRateCheck5.Checked;
        }

        private void yawRateCheck6_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin6.Enabled = yawRateCheck6.Checked;
            yawRateMax6.Enabled = yawRateCheck6.Checked;
        }

        private void yawRateCheck7_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin7.Enabled = yawRateCheck7.Checked;
            yawRateMax7.Enabled = yawRateCheck7.Checked;
        }

        private void yawRateCheck8_CheckedChanged(object sender, EventArgs e)
        {
            yawRateMin8.Enabled = yawRateCheck8.Checked;
            yawRateMax8.Enabled = yawRateCheck8.Checked;
        }



        #endregion

        #region rollrate

        private void rollRateCheck0_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin0.Enabled = rollRateCheck0.Checked;
            rollRateMax0.Enabled = rollRateCheck0.Checked;
        }

        private void rollRateCheck1_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin1.Enabled = rollRateCheck1.Checked;
            rollRateMax1.Enabled = rollRateCheck1.Checked;
        }

        private void rollRateCheck2_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin2.Enabled = rollRateCheck2.Checked;
            rollRateMax2.Enabled = rollRateCheck2.Checked;
        }

        private void rollRateCheck3_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin3.Enabled = rollRateCheck3.Checked;
            rollRateMax3.Enabled = rollRateCheck3.Checked;
        }

        private void rollRateCheck4_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin4.Enabled = rollRateCheck4.Checked;
            rollRateMax4.Enabled = rollRateCheck4.Checked;
        }

        private void rollRateCheck5_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin5.Enabled = rollRateCheck5.Checked;
            rollRateMax5.Enabled = rollRateCheck5.Checked;
        }

        private void rollRateCheck6_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin6.Enabled = rollRateCheck6.Checked;
            rollRateMax6.Enabled = rollRateCheck6.Checked;
        }

        private void rollRateCheck7_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin7.Enabled = rollRateCheck7.Checked;
            rollRateMax7.Enabled = rollRateCheck7.Checked;
        }

        private void rollRateCheck8_CheckedChanged(object sender, EventArgs e)
        {
            rollRateMin8.Enabled = rollRateCheck8.Checked;
            rollRateMax8.Enabled = rollRateCheck8.Checked;
        }



        #endregion

        #region sliderate

        private void slideRateCheck0_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin0.Enabled = slideRateCheck0.Checked;
            slideRateMax0.Enabled = slideRateCheck0.Checked;
        }

        private void slideRateCheck1_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin1.Enabled = slideRateCheck1.Checked;
            slideRateMax1.Enabled = slideRateCheck1.Checked;
        }

        private void slideRateCheck2_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin2.Enabled = slideRateCheck2.Checked;
            slideRateMax2.Enabled = slideRateCheck2.Checked;
        }

        private void slideRateCheck3_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin3.Enabled = slideRateCheck3.Checked;
            slideRateMax3.Enabled = slideRateCheck3.Checked;
        }

        private void slideRateCheck4_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin4.Enabled = slideRateCheck4.Checked;
            slideRateMax4.Enabled = slideRateCheck4.Checked;
        }

        private void slideRateCheck5_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin5.Enabled = slideRateCheck5.Checked;
            slideRateMax5.Enabled = slideRateCheck5.Checked;
        }

        private void slideRateCheck6_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin6.Enabled = slideRateCheck6.Checked;
            slideRateMax6.Enabled = slideRateCheck6.Checked;
        }

        private void slideRateCheck7_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin7.Enabled = slideRateCheck7.Checked;
            slideRateMax7.Enabled = slideRateCheck7.Checked;
        }

        private void slideRateCheck8_CheckedChanged(object sender, EventArgs e)
        {
            slideRateMin8.Enabled = slideRateCheck8.Checked;
            slideRateMax8.Enabled = slideRateCheck8.Checked;
        }

        #endregion

        #region climbrate

        private void climbCheck0_CheckedChanged(object sender, EventArgs e)
        {
            climbMin0.Enabled = climbCheck0.Checked;
            climbMax0.Enabled = climbCheck0.Checked;
        }

        private void climbCheck1_CheckedChanged(object sender, EventArgs e)
        {
            climbMin1.Enabled = climbCheck1.Checked;
            climbMax1.Enabled = climbCheck1.Checked;
        }

        private void climbCheck2_CheckedChanged(object sender, EventArgs e)
        {
            climbMin2.Enabled = climbCheck2.Checked;
            climbMax2.Enabled = climbCheck2.Checked;
        }

        private void climbCheck3_CheckedChanged(object sender, EventArgs e)
        {
            climbMin3.Enabled = climbCheck3.Checked;
            climbMax3.Enabled = climbCheck3.Checked;
        }

        private void climbCheck4_CheckedChanged(object sender, EventArgs e)
        {
            climbMin4.Enabled = climbCheck4.Checked;
            climbMax4.Enabled = climbCheck4.Checked;
        }

        private void climbCheck5_CheckedChanged(object sender, EventArgs e)
        {
            climbMin5.Enabled = climbCheck5.Checked;
            climbMax5.Enabled = climbCheck5.Checked;
        }

        private void climbCheck6_CheckedChanged(object sender, EventArgs e)
        {
            climbMin6.Enabled = climbCheck6.Checked;
            climbMax6.Enabled = climbCheck6.Checked;
        }

        private void climbCheck7_CheckedChanged(object sender, EventArgs e)
        {
            climbMin7.Enabled = climbCheck7.Checked;
            climbMax7.Enabled = climbCheck7.Checked;
        }

        private void climbCheck8_CheckedChanged(object sender, EventArgs e)
        {
            climbMin8.Enabled = climbCheck8.Checked;
            climbMax8.Enabled = climbCheck8.Checked;
        }



        #endregion

        #region mload

        private void mLoadCheck0_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin0.Enabled = mLoadCheck0.Checked;
            mLoadMax0.Enabled = mLoadCheck0.Checked;
        }

        private void mLoadCheck1_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin1.Enabled = mLoadCheck1.Checked;
            mLoadMax1.Enabled = mLoadCheck1.Checked;
        }

        private void mLoadCheck2_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin2.Enabled = mLoadCheck2.Checked;
            mLoadMax2.Enabled = mLoadCheck2.Checked;
        }

        private void mLoadCheck3_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin3.Enabled = mLoadCheck3.Checked;
            mLoadMax3.Enabled = mLoadCheck3.Checked;
        }

        private void mLoadCheck4_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin4.Enabled = mLoadCheck4.Checked;
            mLoadMax4.Enabled = mLoadCheck4.Checked;
        }

        private void mLoadCheck5_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin5.Enabled = mLoadCheck5.Checked;
            mLoadMax5.Enabled = mLoadCheck5.Checked;
        }

        private void mLoadCheck6_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin6.Enabled = mLoadCheck6.Checked;
            mLoadMax6.Enabled = mLoadCheck6.Checked;
        }

        private void mLoadCheck7_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin7.Enabled = mLoadCheck7.Checked;
            mLoadMax7.Enabled = mLoadCheck7.Checked;
        }

        private void mLoadCheck8_CheckedChanged(object sender, EventArgs e)
        {
            mLoadMin8.Enabled = mLoadCheck8.Checked;
            mLoadMax8.Enabled = mLoadCheck8.Checked;
        }



        #endregion

        #region myawrate

        private void mYawRateCheck0_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin0.Enabled = mYawRateCheck0.Checked;
            mYawRateMax0.Enabled = mYawRateCheck0.Checked;
        }

        private void mYawRateCheck1_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin1.Enabled = mYawRateCheck1.Checked;
            mYawRateMax1.Enabled = mYawRateCheck1.Checked;
        }

        private void mYawRateCheck2_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin2.Enabled = mYawRateCheck2.Checked;
            mYawRateMax2.Enabled = mYawRateCheck2.Checked;
        }

        private void mYawRateCheck3_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin3.Enabled = mYawRateCheck3.Checked;
            mYawRateMax3.Enabled = mYawRateCheck3.Checked;
        }

        private void mYawRateCheck4_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin4.Enabled = mYawRateCheck4.Checked;
            mYawRateMax4.Enabled = mYawRateCheck4.Checked;
        }

        private void mYawRateCheck5_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin5.Enabled = mYawRateCheck5.Checked;
            mYawRateMax5.Enabled = mYawRateCheck5.Checked;
        }

        private void mYawRateCheck6_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin6.Enabled = mYawRateCheck6.Checked;
            mYawRateMax6.Enabled = mYawRateCheck6.Checked;
        }

        private void mYawRateCheck7_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin7.Enabled = mYawRateCheck7.Checked;
            mYawRateMax7.Enabled = mYawRateCheck7.Checked;
        }

        private void mYawRateCheck8_CheckedChanged(object sender, EventArgs e)
        {
            mYawRateMin8.Enabled = mYawRateCheck8.Checked;
            mYawRateMax8.Enabled = mYawRateCheck8.Checked;
        }

        #endregion

        #region fuelrate

        private void fuelRateCheck0_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin0.Enabled = fuelRateCheck0.Checked;
            fuelRateMax0.Enabled = fuelRateCheck0.Checked;
        }

        private void fuelRateCheck1_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin1.Enabled = fuelRateCheck1.Checked;
            fuelRateMax1.Enabled = fuelRateCheck1.Checked;
        }

        private void fuelRateCheck2_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin2.Enabled = fuelRateCheck2.Checked;
            fuelRateMax2.Enabled = fuelRateCheck2.Checked;
        }

        private void fuelRateCheck3_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin3.Enabled = fuelRateCheck3.Checked;
            fuelRateMax3.Enabled = fuelRateCheck3.Checked;
        }

        private void fuelRateCheck4_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin4.Enabled = fuelRateCheck4.Checked;
            fuelRateMax4.Enabled = fuelRateCheck4.Checked;
        }

        private void fuelRateCheck5_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin5.Enabled = fuelRateCheck5.Checked;
            fuelRateMax5.Enabled = fuelRateCheck5.Checked;
        }

        private void fuelRateCheck6_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin6.Enabled = fuelRateCheck6.Checked;
            fuelRateMax6.Enabled = fuelRateCheck6.Checked;
        }

        private void fuelRateCheck7_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin7.Enabled = fuelRateCheck7.Checked;
            fuelRateMax7.Enabled = fuelRateCheck7.Checked;
        }

        private void fuelRateCheck8_CheckedChanged(object sender, EventArgs e)
        {
            fuelRateMin8.Enabled = fuelRateCheck8.Checked;
            fuelRateMax8.Enabled = fuelRateCheck8.Checked;
        }

        #endregion

        #region cost

        private void costCheck0_CheckedChanged(object sender, EventArgs e)
        {
            costMin0.Enabled = costCheck0.Checked;
            costMax0.Enabled = costCheck0.Checked;
        }

        private void costCheck1_CheckedChanged(object sender, EventArgs e)
        {
            costMin1.Enabled = costCheck1.Checked;
            costMax1.Enabled = costCheck1.Checked;
        }

        private void costCheck2_CheckedChanged(object sender, EventArgs e)
        {
            costMin2.Enabled = costCheck2.Checked;
            costMax2.Enabled = costCheck2.Checked;
        }

        private void costCheck3_CheckedChanged(object sender, EventArgs e)
        {
            costMin3.Enabled = costCheck3.Checked;
            costMax3.Enabled = costCheck3.Checked;
        }

        private void costCheck4_CheckedChanged(object sender, EventArgs e)
        {
            costMin4.Enabled = costCheck4.Checked;
            costMax4.Enabled = costCheck4.Checked;
        }

        private void costCheck5_CheckedChanged(object sender, EventArgs e)
        {
            costMin5.Enabled = costCheck5.Checked;
            costMax5.Enabled = costCheck5.Checked;
        }

        private void costCheck6_CheckedChanged(object sender, EventArgs e)
        {
            costMin6.Enabled = costCheck6.Checked;
            costMax6.Enabled = costCheck6.Checked;
        }

        private void costCheck7_CheckedChanged(object sender, EventArgs e)
        {
            costMin7.Enabled = costCheck7.Checked;
            costMax7.Enabled = costCheck7.Checked;
        }

        private void costCheck8_CheckedChanged(object sender, EventArgs e)
        {
            costMin8.Enabled = costCheck8.Checked;
            costMax8.Enabled = costCheck8.Checked;
        }

        #endregion

        #region damage

        private void damageCheck0_CheckedChanged(object sender, EventArgs e)
        {
            damageMin0.Enabled = damageCheck0.Checked;
            damageMax0.Enabled = damageCheck0.Checked;
        }

        private void damageCheck1_CheckedChanged(object sender, EventArgs e)
        {
            damageMin1.Enabled = damageCheck1.Checked;
            damageMax1.Enabled = damageCheck1.Checked;
        }

        private void damageCheck2_CheckedChanged(object sender, EventArgs e)
        {
            damageMin2.Enabled = damageCheck2.Checked;
            damageMax2.Enabled = damageCheck2.Checked;
        }

        private void damageCheck3_CheckedChanged(object sender, EventArgs e)
        {
            damageMin3.Enabled = damageCheck3.Checked;
            damageMax3.Enabled = damageCheck3.Checked;
        }

        private void damageCheck4_CheckedChanged(object sender, EventArgs e)
        {
            damageMin4.Enabled = damageCheck4.Checked;
            damageMax4.Enabled = damageCheck4.Checked;
        }

        private void damageCheck5_CheckedChanged(object sender, EventArgs e)
        {
            damageMin5.Enabled = damageCheck5.Checked;
            damageMax5.Enabled = damageCheck5.Checked;
        }

        private void damageCheck6_CheckedChanged(object sender, EventArgs e)
        {
            damageMin6.Enabled = damageCheck6.Checked;
            damageMax6.Enabled = damageCheck6.Checked;
        }

        private void damageCheck7_CheckedChanged(object sender, EventArgs e)
        {
            damageMin7.Enabled = damageCheck7.Checked;
            damageMax7.Enabled = damageCheck7.Checked;
        }

        private void damageCheck8_CheckedChanged(object sender, EventArgs e)
        {
            damageMin8.Enabled = damageCheck8.Checked;
            damageMax8.Enabled = damageCheck8.Checked;
        }

        #endregion

        #region fuel

        private void fuelCheck0_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin0.Enabled = fuelCheck0.Checked;
            fuelMax0.Enabled = fuelCheck0.Checked;
        }

        private void fuelCheck1_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin1.Enabled = fuelCheck1.Checked;
            fuelMax1.Enabled = fuelCheck1.Checked;
        }

        private void fuelCheck2_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin2.Enabled = fuelCheck2.Checked;
            fuelMax2.Enabled = fuelCheck2.Checked;
        }

        private void fuelCheck3_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin3.Enabled = fuelCheck3.Checked;
            fuelMax3.Enabled = fuelCheck3.Checked;
        }

        private void fuelCheck4_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin4.Enabled = fuelCheck4.Checked;
            fuelMax4.Enabled = fuelCheck4.Checked;
        }

        private void fuelCheck5_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin5.Enabled = fuelCheck5.Checked;
            fuelMax5.Enabled = fuelCheck5.Checked;
        }

        private void fuelCheck6_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin6.Enabled = fuelCheck6.Checked;
            fuelMax6.Enabled = fuelCheck6.Checked;
        }

        private void fuelCheck7_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin7.Enabled = fuelCheck7.Checked;
            fuelMax7.Enabled = fuelCheck7.Checked;
        }

        private void fuelCheck8_CheckedChanged(object sender, EventArgs e)
        {
            fuelMin8.Enabled = fuelCheck8.Checked;
            fuelMax8.Enabled = fuelCheck8.Checked;
        }

        #endregion

        #region repair

        private void repairCheck0_CheckedChanged(object sender, EventArgs e)
        {
            repairMin0.Enabled = repairCheck0.Checked;
            repairMax0.Enabled = repairCheck0.Checked;
        }

        private void repairCheck1_CheckedChanged(object sender, EventArgs e)
        {
            repairMin1.Enabled = repairCheck1.Checked;
            repairMax1.Enabled = repairCheck1.Checked;
        }

        private void repairCheck2_CheckedChanged(object sender, EventArgs e)
        {
            repairMin2.Enabled = repairCheck2.Checked;
            repairMax2.Enabled = repairCheck2.Checked;
        }

        private void repairCheck3_CheckedChanged(object sender, EventArgs e)
        {
            repairMin3.Enabled = repairCheck3.Checked;
            repairMax3.Enabled = repairCheck3.Checked;
        }

        private void repairCheck4_CheckedChanged(object sender, EventArgs e)
        {
            repairMin4.Enabled = repairCheck4.Checked;
            repairMax4.Enabled = repairCheck4.Checked;
        }

        private void repairCheck5_CheckedChanged(object sender, EventArgs e)
        {
            repairMin5.Enabled = repairCheck5.Checked;
            repairMax5.Enabled = repairCheck5.Checked;
        }

        private void repairCheck6_CheckedChanged(object sender, EventArgs e)
        {
            repairMin6.Enabled = repairCheck6.Checked;
            repairMax6.Enabled = repairCheck6.Checked;
        }

        private void repairCheck7_CheckedChanged(object sender, EventArgs e)
        {
            repairMin7.Enabled = repairCheck7.Checked;
            repairMax7.Enabled = repairCheck7.Checked;
        }

        private void repairCheck8_CheckedChanged(object sender, EventArgs e)
        {
            repairMin8.Enabled = repairCheck8.Checked;
            repairMax8.Enabled = repairCheck8.Checked;
        }

        #endregion

        #region fuelcost

        private void fuelCostCheck0_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin0.Enabled = fuelCostCheck0.Checked;
            fuelCostMax0.Enabled = fuelCostCheck0.Checked;
        }

        private void fuelCostCheck1_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin1.Enabled = fuelCostCheck1.Checked;
            fuelCostMax1.Enabled = fuelCostCheck1.Checked;
        }

        private void fuelCostCheck2_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin2.Enabled = fuelCostCheck2.Checked;
            fuelCostMax2.Enabled = fuelCostCheck2.Checked;
        }

        private void fuelCostCheck3_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin3.Enabled = fuelCostCheck3.Checked;
            fuelCostMax3.Enabled = fuelCostCheck3.Checked;
        }

        private void fuelCostCheck4_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin4.Enabled = fuelCostCheck4.Checked;
            fuelCostMax4.Enabled = fuelCostCheck4.Checked;
        }

        private void fuelCostCheck5_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin5.Enabled = fuelCostCheck5.Checked;
            fuelCostMax5.Enabled = fuelCostCheck5.Checked;
        }

        private void fuelCostCheck6_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin6.Enabled = fuelCostCheck6.Checked;
            fuelCostMax6.Enabled = fuelCostCheck6.Checked;
        }

        private void fuelCostCheck7_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin7.Enabled = fuelCostCheck7.Checked;
            fuelCostMax7.Enabled = fuelCostCheck7.Checked;
        }

        private void fuelCostCheck8_CheckedChanged(object sender, EventArgs e)
        {
            fuelCostMin8.Enabled = fuelCostCheck8.Checked;
            fuelCostMax8.Enabled = fuelCostCheck8.Checked;
        }

        #endregion

        #endregion

        #region min

        #region mbank

        private void mBankMin0_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin0.Value > mBankMax0.Value)
            {
                mBankMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mBankMin1_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin1.Value > mBankMax1.Value)
            {
                mBankMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }
        private void mBankMin2_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin2.Value > mBankMax2.Value)
            {
                mBankMin2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }
        private void mBankMin3_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin3.Value > mBankMax3.Value)
            {
                mBankMin3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }
        private void mBankMin4_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin4.Value > mBankMax4.Value)
            {
                mBankMin4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }
        private void mBankMin5_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin5.Value > mBankMax5.Value)
            {
                mBankMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }
        private void mBankMin6_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin6.Value > mBankMax6.Value)
            {
                mBankMin6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }
        private void mBankMin7_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin7.Value > mBankMax7.Value)
            {
                mBankMin7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }
        private void mBankMin8_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMin8.Value > mBankMax8.Value)
            {
                mBankMin8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        #endregion

        #region mslide

        private void mSlideMin0_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin0.Value > mSlideMax0.Value)
            {
                mSlideMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mSlideMin1_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin1.Value > mSlideMax1.Value)
            {
                mSlideMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mSlideMin2_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin2.Value > mSlideMax2.Value)
            {
                mSlideMin2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mSlideMin3_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin3.Value > mSlideMax3.Value)
            {
                mSlideMin3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mSlideMin4_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin4.Value > mSlideMax4.Value)
            {
                mSlideMin4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mSlideMin5_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin5.Value > mSlideMax5.Value)
            {
                mSlideMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mSlideMin6_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin6.Value > mSlideMax6.Value)
            {
                mSlideMin6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mSlideMin7_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin7.Value > mSlideMax7.Value)
            {
                mSlideMin7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mSlideMin8_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMin8.Value > mSlideMax8.Value)
            {
                mSlideMin8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        #endregion

        #region mpitch

        private void mPitchMin0_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin0.Value > mPitchMax0.Value)
            {
                mPitchMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mPitchMin1_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin1.Value > mPitchMax1.Value)
            {
                mPitchMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mPitchMin2_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin2.Value > mPitchMax2.Value)
            {
                mPitchMin2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mPitchMin3_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin3.Value > mPitchMax3.Value)
            {
                mPitchMin3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mPitchMin4_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin4.Value > mPitchMax4.Value)
            {
                mPitchMin4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mPitchMin5_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin5.Value > mPitchMax5.Value)
            {
                mPitchMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mPitchMin6_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin6.Value > mPitchMax6.Value)
            {
                mPitchMin6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mPitchMin7_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin7.Value > mPitchMax7.Value)
            {
                mPitchMin7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mPitchMin8_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMin8.Value > mPitchMax8.Value)
            {
                mPitchMin8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        #endregion

        #region pitchrate

        private void pitchRateMin0_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin0.Value > mPitchMax0.Value)
            {
                pitchRateMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void pitchRateMin1_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin1.Value > mPitchMax1.Value)
            {
                pitchRateMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void pitchRateMin2_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin2.Value > mPitchMax2.Value)
            {
                pitchRateMin2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void pitchRateMin3_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin3.Value > mPitchMax3.Value)
            {
                pitchRateMin3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void pitchRateMin4_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin4.Value > mPitchMax4.Value)
            {
                pitchRateMin4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void pitchRateMin5_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin5.Value > mPitchMax5.Value)
            {
                pitchRateMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void pitchRateMin6_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin6.Value > mPitchMax6.Value)
            {
                pitchRateMin6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void pitchRateMin7_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin7.Value > mPitchMax7.Value)
            {
                pitchRateMin7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void pitchRateMin8_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMin8.Value > mPitchMax8.Value)
            {
                pitchRateMin8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        #endregion

        #region yawrate

        private void yawRateMin0_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin0.Value > yawRateMax0.Value)
            {
                yawRateMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void yawRateMin1_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin1.Value > yawRateMax1.Value)
            {
                yawRateMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void yawRateMin2_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin2.Value > yawRateMax2.Value)
            {
                yawRateMin2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void yawRateMin3_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin3.Value > yawRateMax3.Value)
            {
                yawRateMin3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void yawRateMin4_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin4.Value > yawRateMax4.Value)
            {
                yawRateMin4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void yawRateMin5_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin5.Value > yawRateMax5.Value)
            {
                yawRateMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void yawRateMin6_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin6.Value > yawRateMax6.Value)
            {
                yawRateMin6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void yawRateMin7_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin7.Value > yawRateMax7.Value)
            {
                yawRateMin7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void yawRateMin8_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMin8.Value > yawRateMax8.Value)
            {
                yawRateMin8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        #endregion

        #region rollrate

        private void rollRateMin0_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin0.Value > rollRateMax0.Value)
            {
                rollRateMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void rollRateMin1_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin1.Value > rollRateMax1.Value)
            {
                rollRateMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void rollRateMin2_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin2.Value > rollRateMax2.Value)
            {
                rollRateMin2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void rollRateMin3_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin3.Value > rollRateMax3.Value)
            {
                rollRateMin3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void rollRateMin4_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin4.Value > rollRateMax4.Value)
            {
                rollRateMin4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void rollRateMin5_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin5.Value > rollRateMax5.Value)
            {
                rollRateMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void rollRateMin6_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin6.Value > rollRateMax6.Value)
            {
                rollRateMin6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void rollRateMin7_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin7.Value > rollRateMax7.Value)
            {
                rollRateMin7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void rollRateMin8_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMin8.Value > rollRateMax8.Value)
            {
                rollRateMin8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        #endregion

        #region sliderate

        private void slideRateMin0_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin0.Value > mSlideMax0.Value)
            {
                slideRateMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void slideRateMin1_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin1.Value > mSlideMax1.Value)
            {
                slideRateMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void slideRateMin2_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin2.Value > mSlideMax2.Value)
            {
                slideRateMin2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void slideRateMin3_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin3.Value > mSlideMax3.Value)
            {
                slideRateMin3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void slideRateMin4_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin4.Value > mSlideMax4.Value)
            {
                slideRateMin4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void slideRateMin5_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin5.Value > mSlideMax5.Value)
            {
                slideRateMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void slideRateMin6_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin6.Value > mSlideMax6.Value)
            {
                slideRateMin6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void slideRateMin7_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin7.Value > mSlideMax7.Value)
            {
                slideRateMin7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void slideRateMin8_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMin8.Value > mSlideMax8.Value)
            {
                slideRateMin8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        #endregion

        #region climbrate

        private void climbMin0_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin0.Value > climbMax0.Value)
            {
                climbMin0.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void climbMin1_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin1.Value > climbMax1.Value)
            {
                climbMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void climbMin2_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin2.Value > climbMax2.Value)
            {
                climbMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void climbMin3_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin3.Value > climbMax3.Value)
            {
                climbMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void climbMin4_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin4.Value > climbMax4.Value)
            {
                climbMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void climbMin5_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin5.Value > climbMax5.Value)
            {
                climbMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void climbMin6_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin6.Value > climbMax6.Value)
            {
                climbMin6.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void climbMin7_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin7.Value > climbMax7.Value)
            {
                climbMin7.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void climbMin8_ValueChanged(object sender, EventArgs e)
        {
            if (climbMin8.Value > climbMax8.Value)
            {
                climbMin8.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region mload

        private void mLoadMin0_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin0.Value > mLoadMax0.Value)
            {
                mLoadMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mLoadMin1_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin1.Value > mLoadMax1.Value)
            {
                mLoadMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mLoadMin2_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin2.Value > mLoadMax2.Value)
            {
                mLoadMin2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mLoadMin3_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin3.Value > mLoadMax3.Value)
            {
                mLoadMin3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mLoadMin4_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin4.Value > mLoadMax4.Value)
            {
                mLoadMin4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mLoadMin5_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin5.Value > mLoadMax5.Value)
            {
                mLoadMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mLoadMin6_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin6.Value > mLoadMax6.Value)
            {
                mLoadMin6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mLoadMin7_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin7.Value > mLoadMax7.Value)
            {
                mLoadMin7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void mLoadMin8_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMin8.Value > mLoadMax8.Value)
            {
                mLoadMin8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        #endregion

        #region myawrate

        private void mYawRateMin0_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin0.Value > mYawRateMax0.Value)
            {
                mYawRateMin0.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mYawRateMin1_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin1.Value > mYawRateMax1.Value)
            {
                mYawRateMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mYawRateMin2_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin2.Value > mYawRateMax2.Value)
            {
                mYawRateMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mYawRateMin3_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin3.Value > mYawRateMax3.Value)
            {
                mYawRateMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mYawRateMin4_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin4.Value > mYawRateMax4.Value)
            {
                mYawRateMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mYawRateMin5_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin5.Value > mYawRateMax5.Value)
            {
                mYawRateMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mYawRateMin6_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin6.Value > mYawRateMax6.Value)
            {
                mYawRateMin6.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mYawRateMin7_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin7.Value > mYawRateMax7.Value)
            {
                mYawRateMin7.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void mYawRateMin8_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMin8.Value > mYawRateMax8.Value)
            {
                mYawRateMin8.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region fuelrate

        private void fuelRateMin0_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin0.Value > fuelRateMax0.Value)
            {
                fuelRateMin0.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelRateMin1_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin1.Value > fuelRateMax1.Value)
            {
                fuelRateMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 2.");
            }
        }

        private void fuelRateMin2_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin2.Value > fuelRateMax2.Value)
            {
                fuelRateMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelRateMin3_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin3.Value > fuelRateMax3.Value)
            {
                fuelRateMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelRateMin4_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin4.Value > fuelRateMax4.Value)
            {
                fuelRateMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelRateMin5_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin5.Value > fuelRateMax5.Value)
            {
                fuelRateMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelRateMin6_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin6.Value > fuelRateMax6.Value)
            {
                fuelRateMin6.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelRateMin7_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin7.Value > fuelRateMax7.Value)
            {
                fuelRateMin7.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelRateMin8_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMin8.Value > fuelRateMax8.Value)
            {
                fuelRateMin8.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region cost

        private void costMin0_ValueChanged(object sender, EventArgs e)
        {
            if (costMin0.Value > costMax0.Value)
            {
                costMin0.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void costMin1_ValueChanged(object sender, EventArgs e)
        {
            if (costMin1.Value > costMax1.Value)
            {
                costMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void costMin2_ValueChanged(object sender, EventArgs e)
        {
            if (costMin2.Value > costMax2.Value)
            {
                costMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void costMin3_ValueChanged(object sender, EventArgs e)
        {
            if (costMin3.Value > costMax3.Value)
            {
                costMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void costMin4_ValueChanged(object sender, EventArgs e)
        {
            if (costMin4.Value > costMax4.Value)
            {
                costMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void costMin5_ValueChanged(object sender, EventArgs e)
        {
            if (costMin5.Value > costMax5.Value)
            {
                costMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void costMin6_ValueChanged(object sender, EventArgs e)
        {
            if (costMin6.Value > costMax6.Value)
            {
                costMin6.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void costMin7_ValueChanged(object sender, EventArgs e)
        {
            if (costMin7.Value > costMax7.Value)
            {
                costMin7.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void costMin8_ValueChanged(object sender, EventArgs e)
        {
            if (costMin8.Value > costMax8.Value)
            {
                costMin8.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region damage

        private void damageMin0_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin0.Value > damageMax0.Value)
            {
                damageMin0.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void damageMin1_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin1.Value > damageMax1.Value)
            {
                damageMin1.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void damageMin2_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin2.Value > damageMax2.Value)
            {
                damageMin2.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void damageMin3_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin3.Value > damageMax3.Value)
            {
                damageMin3.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void damageMin4_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin4.Value > damageMax4.Value)
            {
                damageMin4.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void damageMin5_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin5.Value > damageMax5.Value)
            {
                damageMin5.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void damageMin6_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin6.Value > damageMax6.Value)
            {
                damageMin6.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void damageMin7_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin7.Value > damageMax7.Value)
            {
                damageMin7.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void damageMin8_ValueChanged(object sender, EventArgs e)
        {
            if (damageMin8.Value > damageMax8.Value)
            {
                damageMin8.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        #endregion

        #region fuel

        private void fuelMin0_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin0.Value > fuelMax0.Value)
            {
                fuelMin0.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelMin1_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin1.Value > fuelMax1.Value)
            {
                fuelMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelMin2_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin2.Value > fuelMax2.Value)
            {
                fuelMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelMin3_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin3.Value > fuelMax3.Value)
            {
                fuelMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelMin4_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin4.Value > fuelMax4.Value)
            {
                fuelMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelMin5_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin5.Value > fuelMax5.Value)
            {
                fuelMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelMin6_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin6.Value > fuelMax6.Value)
            {
                fuelMin6.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelMin7_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin7.Value > fuelMax7.Value)
            {
                fuelMin7.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelMin8_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMin8.Value > fuelMax8.Value)
            {
                fuelMin8.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region repair

        private void repairMin0_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin0.Value > repairMax0.Value)
            {
                repairMin0.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void repairMin1_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin1.Value > repairMax1.Value)
            {
                repairMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void repairMin2_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin2.Value > repairMax2.Value)
            {
                repairMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void repairMin3_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin3.Value > repairMax3.Value)
            {
                repairMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void repairMin4_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin4.Value > repairMax4.Value)
            {
                repairMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void repairMin5_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin5.Value > repairMax5.Value)
            {
                repairMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void repairMin6_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin6.Value > repairMax6.Value)
            {
                repairMin6.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void repairMin7_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin7.Value > repairMax7.Value)
            {
                repairMin7.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void repairMin8_ValueChanged(object sender, EventArgs e)
        {
            if (repairMin8.Value > repairMax8.Value)
            {
                repairMin8.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region fuelcost

        private void fuelCostMin0_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin0.Value > fuelCostMax0.Value)
            {
                fuelCostMin0.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelCostMin1_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin1.Value > fuelCostMax1.Value)
            {
                fuelCostMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelCostMin2_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin2.Value > fuelCostMax2.Value)
            {
                fuelCostMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelCostMin3_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin3.Value > fuelCostMax3.Value)
            {
                fuelCostMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelCostMin4_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin4.Value > fuelCostMax4.Value)
            {
                fuelCostMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelCostMin5_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin5.Value > fuelCostMax5.Value)
            {
                fuelCostMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelCostMin6_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin6.Value > fuelCostMax6.Value)
            {
                fuelCostMin6.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelCostMin7_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin7.Value > fuelCostMax7.Value)
            {
                fuelCostMin7.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void fuelCostMin8_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMin8.Value > fuelCostMax8.Value)
            {
                fuelCostMin8.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #endregion

        #region max

        #region mbank

        private void mBankMax0_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax0.Value < mBankMin0.Value)
            {
                mBankMax0.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mBankMax1_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax1.Value < mBankMin1.Value)
            {
                mBankMax1.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mBankMax2_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax2.Value < mBankMin2.Value)
            {
                mBankMax2.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mBankMax3_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax3.Value < mBankMin3.Value)
            {
                mBankMax3.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mBankMax4_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax4.Value < mBankMin4.Value)
            {
                mBankMax4.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mBankMax5_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax5.Value < mBankMin5.Value)
            {
                mBankMax5.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mBankMax6_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax6.Value < mBankMin6.Value)
            {
                mBankMax6.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mBankMax7_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax7.Value < mBankMin7.Value)
            {
                mBankMax7.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mBankMax8_ValueChanged(object sender, EventArgs e)
        {
            if (mBankMax8.Value < mBankMin8.Value)
            {
                mBankMax8.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        #endregion

        #region mslide

        private void mSlideMax0_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax0.Value < mSlideMin0.Value)
            {
                mSlideMax0.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mSlideMax1_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax1.Value < mSlideMin1.Value)
            {
                mSlideMax1.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mSlideMax2_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax2.Value < mSlideMin2.Value)
            {
                mSlideMax2.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mSlideMax3_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax3.Value < mSlideMin3.Value)
            {
                mSlideMax3.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mSlideMax4_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax4.Value < mSlideMin4.Value)
            {
                mSlideMax4.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mSlideMax5_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax5.Value < mSlideMin5.Value)
            {
                mSlideMax5.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mSlideMax6_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax6.Value < mSlideMin6.Value)
            {
                mSlideMax6.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mSlideMax7_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax7.Value < mSlideMin7.Value)
            {
                mSlideMax7.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mSlideMax8_ValueChanged(object sender, EventArgs e)
        {
            if (mSlideMax8.Value < mSlideMin8.Value)
            {
                mSlideMax8.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        #endregion

        #region mpitch

        private void mPitchMax0_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax0.Value < mPitchMin0.Value)
            {
                mPitchMax0.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mPitchMax1_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax1.Value < mPitchMin1.Value)
            {
                mPitchMax1.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mPitchMax2_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax2.Value < mPitchMin2.Value)
            {
                mPitchMax2.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mPitchMax3_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax3.Value < mPitchMin3.Value)
            {
                mPitchMax3.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mPitchMax4_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax4.Value < mPitchMin4.Value)
            {
                mPitchMax4.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mPitchMax5_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax5.Value < mPitchMin5.Value)
            {
                mPitchMax5.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mPitchMax6_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax6.Value < mPitchMin6.Value)
            {
                mPitchMax6.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mPitchMax7_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax7.Value < mPitchMin7.Value)
            {
                mPitchMax7.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void mPitchMax8_ValueChanged(object sender, EventArgs e)
        {
            if (mPitchMax8.Value < mPitchMin8.Value)
            {
                mPitchMax8.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        #endregion

        #region pitchrate

        private void pitchRateMax0_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax0.Value < pitchRateMin0.Value)
            {
                mPitchMax0.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void pitchRateMax1_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax1.Value < pitchRateMin1.Value)
            {
                mPitchMax1.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void pitchRateMax2_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax2.Value < pitchRateMin2.Value)
            {
                mPitchMax2.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void pitchRateMax3_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax3.Value < pitchRateMin3.Value)
            {
                mPitchMax3.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void pitchRateMax4_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax4.Value < pitchRateMin4.Value)
            {
                mPitchMax4.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void pitchRateMax5_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax5.Value < pitchRateMin5.Value)
            {
                mPitchMax5.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void pitchRateMax6_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax6.Value < pitchRateMin6.Value)
            {
                mPitchMax6.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void pitchRateMax7_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax7.Value < pitchRateMin7.Value)
            {
                mPitchMax7.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void pitchRateMax8_ValueChanged(object sender, EventArgs e)
        {
            if (pitchRateMax8.Value < pitchRateMin8.Value)
            {
                mPitchMax8.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        #endregion

        #region yawrate

        private void yawRateMax0_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax0.Value < yawRateMin0.Value)
            {
                yawRateMax0.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void yawRateMax1_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax1.Value < yawRateMin1.Value)
            {
                yawRateMax1.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void yawRateMax2_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax2.Value < yawRateMin2.Value)
            {
                yawRateMax2.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void yawRateMax3_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax3.Value < yawRateMin3.Value)
            {
                yawRateMax3.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void yawRateMax4_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax4.Value < yawRateMin4.Value)
            {
                yawRateMax4.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void yawRateMax5_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax5.Value < yawRateMin5.Value)
            {
                yawRateMax5.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void yawRateMax6_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax6.Value < yawRateMin6.Value)
            {
                yawRateMax6.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void yawRateMax7_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax7.Value < yawRateMin7.Value)
            {
                yawRateMax7.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void yawRateMax8_ValueChanged(object sender, EventArgs e)
        {
            if (yawRateMax8.Value < yawRateMin8.Value)
            {
                yawRateMax8.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        #endregion

        #region rollrate

        private void rollRateMax0_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax0.Value < rollRateMin0.Value)
            {
                rollRateMax0.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void rollRateMax1_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax1.Value < rollRateMin1.Value)
            {
                rollRateMax1.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void rollRateMax2_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax2.Value < rollRateMin2.Value)
            {
                rollRateMax2.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void rollRateMax3_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax3.Value < rollRateMin3.Value)
            {
                rollRateMax3.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void rollRateMax4_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax4.Value < rollRateMin4.Value)
            {
                rollRateMax4.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void rollRateMax5_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax5.Value < rollRateMin5.Value)
            {
                rollRateMax5.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void rollRateMax6_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax6.Value < rollRateMin6.Value)
            {
                rollRateMax6.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void rollRateMax7_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax7.Value < rollRateMin7.Value)
            {
                rollRateMax7.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void rollRateMax8_ValueChanged(object sender, EventArgs e)
        {
            if (rollRateMax8.Value < rollRateMin8.Value)
            {
                rollRateMax8.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        #endregion

        #region sliderate

        private void slideRateMax0_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax0.Value < slideRateMin0.Value)
            {
                slideRateMax0.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void slideRateMax1_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax1.Value < slideRateMin1.Value)
            {
                slideRateMax1.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void slideRateMax2_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax2.Value < slideRateMin2.Value)
            {
                slideRateMax2.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void slideRateMax3_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax3.Value < slideRateMin3.Value)
            {
                slideRateMax3.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void slideRateMax4_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax4.Value < slideRateMin4.Value)
            {
                slideRateMax4.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void slideRateMax5_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax5.Value < slideRateMin5.Value)
            {
                slideRateMax5.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void slideRateMax6_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax6.Value < slideRateMin6.Value)
            {
                slideRateMax6.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void slideRateMax7_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax7.Value < slideRateMin7.Value)
            {
                slideRateMax7.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void slideRateMax8_ValueChanged(object sender, EventArgs e)
        {
            if (slideRateMax8.Value < slideRateMin8.Value)
            {
                slideRateMax8.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        #endregion

        #region climbrate

        private void climbMax0_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax0.Value < climbMin0.Value)
            {
                climbMax0.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        private void climbMax1_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax1.Value < climbMin1.Value)
            {
                climbMax1.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        private void climbMax2_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax2.Value < climbMin2.Value)
            {
                climbMax2.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        private void climbMax3_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax3.Value < climbMin3.Value)
            {
                climbMax3.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        private void climbMax4_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax4.Value < climbMin4.Value)
            {
                climbMax4.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        private void climbMax5_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax5.Value < climbMin5.Value)
            {
                climbMax5.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        private void climbMax6_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax6.Value < climbMin6.Value)
            {
                climbMax6.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        private void climbMax7_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax7.Value < climbMin7.Value)
            {
                climbMax7.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        private void climbMax8_ValueChanged(object sender, EventArgs e)
        {
            if (climbMax8.Value < climbMin8.Value)
            {
                climbMax8.Value = 50;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 50.");
            }
        }

        #endregion

        #region mload

        private void mLoadMax0_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax0.Value < mLoadMin0.Value)
            {
                mLoadMax0.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        private void mLoadMax1_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax1.Value < mLoadMin1.Value)
            {
                mLoadMax1.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        private void mLoadMax2_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax2.Value < mLoadMin2.Value)
            {
                mLoadMax2.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        private void mLoadMax3_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax3.Value < mLoadMin3.Value)
            {
                mLoadMax3.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        private void mLoadMax4_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax4.Value < mLoadMin4.Value)
            {
                mLoadMax4.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        private void mLoadMax5_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax5.Value < mLoadMin5.Value)
            {
                mLoadMax5.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        private void mLoadMax6_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax6.Value < mLoadMin6.Value)
            {
                mLoadMax6.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        private void mLoadMax7_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax7.Value < mLoadMin7.Value)
            {
                mLoadMax7.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        private void mLoadMax8_ValueChanged(object sender, EventArgs e)
        {
            if (mLoadMax8.Value < mLoadMin8.Value)
            {
                mLoadMax8.Value = 20000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 20000.");
            }
        }

        #endregion

        #region myawrate

        private void mYawRateMax0_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax0.Value < mYawRateMin0.Value)
            {
                mYawRateMax0.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        private void mYawRateMax1_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax1.Value < mYawRateMin1.Value)
            {
                mYawRateMax1.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        private void mYawRateMax2_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax2.Value < mYawRateMin2.Value)
            {
                mYawRateMax2.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        private void mYawRateMax3_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax3.Value < mYawRateMin3.Value)
            {
                mYawRateMax3.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        private void mYawRateMax4_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax4.Value < mYawRateMin4.Value)
            {
                mYawRateMax4.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        private void mYawRateMax5_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax5.Value < mYawRateMin5.Value)
            {
                mYawRateMax5.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        private void mYawRateMax6_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax6.Value < mYawRateMin6.Value)
            {
                mYawRateMax6.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        private void mYawRateMax7_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax7.Value < mYawRateMin7.Value)
            {
                mYawRateMax7.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        private void mYawRateMax8_ValueChanged(object sender, EventArgs e)
        {
            if (mYawRateMax8.Value < mYawRateMin8.Value)
            {
                mYawRateMax8.Value = 900;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 900.");
            }
        }

        #endregion

        #region fuelrate

        private void fuelRateMax0_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax0.Value < fuelRateMin0.Value)
            {
                fuelRateMax0.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void fuelRateMax1_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax1.Value < fuelRateMin1.Value)
            {
                fuelRateMax1.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void fuelRateMax2_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax2.Value < fuelRateMin2.Value)
            {
                fuelRateMax2.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void fuelRateMax3_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax3.Value < fuelRateMin3.Value)
            {
                fuelRateMax3.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void fuelRateMax4_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax4.Value < fuelRateMin4.Value)
            {
                fuelRateMax4.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void fuelRateMax5_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax5.Value < fuelRateMin5.Value)
            {
                fuelRateMax5.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void fuelRateMax6_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax6.Value < fuelRateMin6.Value)
            {
                fuelRateMax6.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void fuelRateMax7_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax7.Value < fuelRateMin7.Value)
            {
                fuelRateMax7.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void fuelRateMax8_ValueChanged(object sender, EventArgs e)
        {
            if (fuelRateMax8.Value < fuelRateMin8.Value)
            {
                fuelRateMax8.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        #endregion

        #region cost

        private void costMax0_ValueChanged(object sender, EventArgs e)
        {
            if (costMax0.Value < costMin0.Value)
            {
                costMax0.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        private void costMax1_ValueChanged(object sender, EventArgs e)
        {
            if (costMax1.Value < costMin1.Value)
            {
                costMax1.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        private void costMax2_ValueChanged(object sender, EventArgs e)
        {
            if (costMax2.Value < costMin2.Value)
            {
                costMax2.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        private void costMax3_ValueChanged(object sender, EventArgs e)
        {
            if (costMax3.Value < costMin3.Value)
            {
                costMax3.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        private void costMax4_ValueChanged(object sender, EventArgs e)
        {
            if (costMax4.Value < costMin4.Value)
            {
                costMax4.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        private void costMax5_ValueChanged(object sender, EventArgs e)
        {
            if (costMax5.Value < costMin5.Value)
            {
                costMax5.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        private void costMax6_ValueChanged(object sender, EventArgs e)
        {
            if (costMax6.Value < costMin6.Value)
            {
                costMax6.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        private void costMax7_ValueChanged(object sender, EventArgs e)
        {
            if (costMax7.Value < costMin7.Value)
            {
                costMax7.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        private void costMax8_ValueChanged(object sender, EventArgs e)
        {
            if (costMax8.Value < costMin8.Value)
            {
                costMax8.Value = 100000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100000.");
            }
        }

        #endregion

        #region damage

        private void damageMax0_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax0.Value < damageMin0.Value)
            {
                damageMax0.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void damageMax1_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax1.Value < damageMin1.Value)
            {
                damageMax1.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void damageMax2_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax2.Value < damageMin2.Value)
            {
                damageMax2.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void damageMax3_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax3.Value < damageMin3.Value)
            {
                damageMax3.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void damageMax4_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax4.Value < damageMin4.Value)
            {
                damageMax4.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void damageMax5_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax5.Value < damageMin5.Value)
            {
                damageMax5.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void damageMax6_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax6.Value < damageMin6.Value)
            {
                damageMax6.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void damageMax7_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax7.Value < damageMin7.Value)
            {
                damageMax7.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        private void damageMax8_ValueChanged(object sender, EventArgs e)
        {
            if (damageMax8.Value < damageMin8.Value)
            {
                damageMax8.Value = 5000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5000.");
            }
        }

        #endregion

        #region fuel

        private void fuelMax0_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax0.Value < fuelMin0.Value)
            {
                fuelMax0.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void fuelMax1_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax1.Value < fuelMin1.Value)
            {
                fuelMax1.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void fuelMax2_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax2.Value < fuelMin2.Value)
            {
                fuelMax2.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void fuelMax3_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax3.Value < fuelMin3.Value)
            {
                fuelMax3.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void fuelMax4_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax4.Value < fuelMin4.Value)
            {
                fuelMax4.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void fuelMax5_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax5.Value < fuelMin5.Value)
            {
                fuelMax5.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void fuelMax6_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax6.Value < fuelMin6.Value)
            {
                fuelMax6.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void fuelMax7_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax7.Value < fuelMin7.Value)
            {
                fuelMax7.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void fuelMax8_ValueChanged(object sender, EventArgs e)
        {
            if (fuelMax8.Value < fuelMin8.Value)
            {
                fuelMax8.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        #endregion

        #region repair

        private void repairMax0_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax0.Value < repairMin0.Value)
            {
                repairMax0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void repairMax1_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax1.Value < repairMin1.Value)
            {
                repairMax1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void repairMax2_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax2.Value < repairMin2.Value)
            {
                repairMax2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void repairMax3_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax3.Value < repairMin3.Value)
            {
                repairMax3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void repairMax4_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax4.Value < repairMin4.Value)
            {
                repairMax4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void repairMax5_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax5.Value < repairMin5.Value)
            {
                repairMax5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void repairMax6_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax6.Value < repairMin6.Value)
            {
                repairMax6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void repairMax7_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax7.Value < repairMin7.Value)
            {
                repairMax7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void repairMax8_ValueChanged(object sender, EventArgs e)
        {
            if (repairMax8.Value < repairMin8.Value)
            {
                repairMax8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        #endregion

        #region fuelcost

        private void fuelCostMax0_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax0.Value < fuelCostMin0.Value)
            {
                fuelCostMax0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void fuelCostMax1_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax1.Value < fuelCostMin1.Value)
            {
                fuelCostMax1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void fuelCostMax2_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax2.Value < fuelCostMin2.Value)
            {
                fuelCostMax2.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void fuelCostMax3_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax3.Value < fuelCostMin3.Value)
            {
                fuelCostMax3.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void fuelCostMax4_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax4.Value < fuelCostMin4.Value)
            {
                fuelCostMax4.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void fuelCostMax5_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax5.Value < fuelCostMin5.Value)
            {
                fuelCostMax5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void fuelCostMax6_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax6.Value < fuelCostMin6.Value)
            {
                fuelCostMax6.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void fuelCostMax7_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax7.Value < fuelCostMin7.Value)
            {
                fuelCostMax7.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void fuelCostMax8_ValueChanged(object sender, EventArgs e)
        {
            if (fuelCostMax8.Value < fuelCostMin8.Value)
            {
                fuelCostMax8.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        #endregion

        #endregion

        #endregion

        #region landing

        #region check

        private void landCheck0_CheckedChanged(object sender, EventArgs e)
        {
            landMin0.Enabled = landCheck0.Checked;
            landMax0.Enabled = landCheck0.Checked;
        }

        private void landCheck1_CheckedChanged(object sender, EventArgs e)
        {
            landMin1.Enabled = landCheck1.Checked;
            landMax1.Enabled = landCheck1.Checked;
        }

        private void landCheck2_CheckedChanged(object sender, EventArgs e)
        {
            landMin2.Enabled = landCheck2.Checked;
            landMax2.Enabled = landCheck2.Checked;
        }

        private void landCheck3_CheckedChanged(object sender, EventArgs e)
        {
            landMin3.Enabled = landCheck3.Checked;
            landMax3.Enabled = landCheck3.Checked;
        }

        private void landCheck4_CheckedChanged(object sender, EventArgs e)
        {
            landMin4.Enabled = landCheck4.Checked;
            landMax4.Enabled = landCheck4.Checked;
        }


        #endregion

        #region min

        private void landMin0_ValueChanged(object sender, EventArgs e)
        {
            if (landMin0.Value > landMax0.Value)
            {
                landMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void landMin1_ValueChanged(object sender, EventArgs e)
        {
            if (landMin1.Value > landMax1.Value)
            {
                landMin1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void landMin2_ValueChanged(object sender, EventArgs e)
        {
            if (landMin2.Value > landMax2.Value)
            {
                landMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void landMin3_ValueChanged(object sender, EventArgs e)
        {
            if (landMin3.Value > landMax3.Value)
            {
                landMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void landMin4_ValueChanged(object sender, EventArgs e)
        {
            if (landMin4.Value > landMax4.Value)
            {
                landMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }


        #endregion

        #region max

        private void landMax0_ValueChanged(object sender, EventArgs e)
        {
            if (landMax0.Value < landMin0.Value)
            {
                landMax0.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void landMax1_ValueChanged(object sender, EventArgs e)
        {
            if (landMax1.Value < landMin1.Value)
            {
                landMax1.Value = 800;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 800.");
            }
        }

        private void landMax2_ValueChanged(object sender, EventArgs e)
        {
            if (landMax2.Value < landMin2.Value)
            {
                landMax2.Value = 200;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 200.");
            }
        }

        private void landMax3_ValueChanged(object sender, EventArgs e)
        {
            if (landMax3.Value < landMin3.Value)
            {
                landMax3.Value = 200;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 200.");
            }
        }

        private void landMax4_ValueChanged(object sender, EventArgs e)
        {
            if (landMax4.Value < landMin4.Value)
            {
                landMax4.Value = 40;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 40.");
            }
        }


        #endregion

        #endregion

        #region rope

        #region check

        private void ropeCheck0_CheckedChanged(object sender, EventArgs e)
        {
            ropeMin0.Enabled = ropeCheck0.Checked;
            ropeMax0.Enabled = ropeCheck0.Checked;
        }

        private void ropeCheck1_CheckedChanged(object sender, EventArgs e)
        {
            ropeMin1.Enabled = ropeCheck1.Checked;
            ropeMax1.Enabled = ropeCheck1.Checked;
        }

        private void ropeCheck2_CheckedChanged(object sender, EventArgs e)
        {
            ropeMin2.Enabled = ropeCheck2.Checked;
            ropeMax2.Enabled = ropeCheck2.Checked;
        }

        private void ropeCheck3_CheckedChanged(object sender, EventArgs e)
        {
            ropeMin3.Enabled = ropeCheck3.Checked;
            ropeMax3.Enabled = ropeCheck3.Checked;
        }

        private void ropeCheck4_CheckedChanged(object sender, EventArgs e)
        {
            ropeMin4.Enabled = ropeCheck4.Checked;
            ropeMax4.Enabled = ropeCheck4.Checked;
        }

        private void ropeCheck5_CheckedChanged(object sender, EventArgs e)
        {
            ropeMin5.Enabled = ropeCheck5.Checked;
            ropeMax5.Enabled = ropeCheck5.Checked;
        }

        #endregion

        #region min

        private void ropeMin0_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMin0.Value > ropeMax0.Value)
            {
                ropeMin0.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void ropeMin1_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMin1.Value > ropeMax1.Value)
            {
                ropeMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void ropeMin2_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMin2.Value > ropeMax2.Value)
            {
                ropeMin2.Value = 8;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 8.");
            }
        }

        private void ropeMin3_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMin3.Value > ropeMax3.Value)
            {
                ropeMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void ropeMin4_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMin4.Value > ropeMax4.Value)
            {
                ropeMin4.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void ropeMin5_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMin5.Value > ropeMax5.Value)
            {
                ropeMin5.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }


        #endregion

        #region max

        private void ropeMax0_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMax0.Value < ropeMin0.Value)
            {
                ropeMax0.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void ropeMax1_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMax1.Value < ropeMin1.Value)
            {
                ropeMax1.Value = 2000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 2000.");
            }
        }

        private void ropeMax2_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMax2.Value < ropeMin2.Value)
            {
                ropeMax2.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        private void ropeMax3_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMax3.Value < ropeMin3.Value)
            {
                ropeMax3.Value = 1;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1.");
            }
        }

        private void ropeMax4_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMax4.Value < ropeMin4.Value)
            {
                ropeMax4.Value = 64;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 64.");
            }
        }

        private void ropeMax5_ValueChanged(object sender, EventArgs e)
        {
            if (ropeMax5.Value < ropeMin5.Value)
            {
                ropeMax5.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }


        #endregion

        #endregion

        #region damage

        #region check

        private void dCheck0_CheckedChanged(object sender, EventArgs e)
        {
            dMin0.Enabled = dCheck0.Checked;
            dMax0.Enabled = dCheck0.Checked;
        }

        private void dCheck1_CheckedChanged(object sender, EventArgs e)
        {
            dMin1.Enabled = dCheck1.Checked;
            dMax1.Enabled = dCheck1.Checked;
        }

        private void dCheck2_CheckedChanged(object sender, EventArgs e)
        {
            dMin2.Enabled = dCheck2.Checked;
            dMax2.Enabled = dCheck2.Checked;
        }

        private void dCheck3_CheckedChanged(object sender, EventArgs e)
        {
            dMin3.Enabled = dCheck3.Checked;
            dMax3.Enabled = dCheck3.Checked;
        }

        private void dCheck4_CheckedChanged(object sender, EventArgs e)
        {
            dMin4.Enabled = dCheck4.Checked;
            dMax4.Enabled = dCheck4.Checked;
        }

        private void dCheck5_CheckedChanged(object sender, EventArgs e)
        {
            dMin5.Enabled = dCheck5.Checked;
            dMax5.Enabled = dCheck5.Checked;
        }

        #endregion

        #region min

        private void dMin0_ValueChanged(object sender, EventArgs e)
        {
            if (dMin0.Value > dMax0.Value)
            {
                dMin0.Value = -100;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to -100.");
            }
        }

        private void dMin1_ValueChanged(object sender, EventArgs e)
        {
            if (dMin1.Value > dMax1.Value)
            {
                dMin1.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void dMin2_ValueChanged(object sender, EventArgs e)
        {
            if (dMin2.Value > dMax2.Value)
            {
                dMin2.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void dMin3_ValueChanged(object sender, EventArgs e)
        {
            if (dMin3.Value > dMax3.Value)
            {
                dMin3.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void dMin4_ValueChanged(object sender, EventArgs e)
        {
            if (dMin4.Value > dMax4.Value)
            {
                dMin4.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        private void dMin5_ValueChanged(object sender, EventArgs e)
        {
            if (dMin5.Value > dMax5.Value)
            {
                dMin5.Value = 0;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 0.");
            }
        }

        #endregion

        #region max

        private void dMax0_ValueChanged(object sender, EventArgs e)
        {
            if (dMax0.Value < dMin0.Value)
            {
                dMax0.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        private void dMax1_ValueChanged(object sender, EventArgs e)
        {
            if (dMax1.Value < dMin1.Value)
            {
                dMax1.Value = 200;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 200.");
            }
        }

        private void dMax2_ValueChanged(object sender, EventArgs e)
        {
            if (dMax2.Value < dMin2.Value)
            {
                dMax2.Value = 5;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 5.");
            }
        }

        private void dMax3_ValueChanged(object sender, EventArgs e)
        {
            if (dMax3.Value < dMin3.Value)
            {
                dMax3.Value = 500;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 500.");
            }
        }

        private void dMax4_ValueChanged(object sender, EventArgs e)
        {
            if (dMax4.Value < dMin4.Value)
            {
                dMax4.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        private void dMax5_ValueChanged(object sender, EventArgs e)
        {
            if (dMax5.Value < dMin5.Value)
            {
                dMax5.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }


        #endregion

        #endregion

        #endregion

        private void fCustom_CheckedChanged(object sender, EventArgs e)
        {
            fGroup.Enabled = true;
            fCheckOffButt.Enabled = true;
            fCheckOnButt.Enabled = true;
        }

        private void fChaos_CheckedChanged(object sender, EventArgs e)
        {
            fGroup.Enabled = false;
            fCheckOffButt.Enabled = false;
            fCheckOnButt.Enabled = false;
        }

        #region fire

        #region check

        private void fCheck0_CheckedChanged(object sender, EventArgs e)
        {
            fMin0.Enabled = fCheck0.Checked;
            fMax0.Enabled = fCheck0.Checked;
        }

        private void fCheck1_CheckedChanged(object sender, EventArgs e)
        {
            fMin1.Enabled = fCheck1.Checked;
            fMax1.Enabled = fCheck1.Checked;
        }

        private void fCheck2_CheckedChanged(object sender, EventArgs e)
        {
            fMin2.Enabled = fCheck2.Checked;
            fMax2.Enabled = fCheck2.Checked;
        }

        private void fCheck3_CheckedChanged(object sender, EventArgs e)
        {
            fMin3.Enabled = fCheck3.Checked;
            fMax3.Enabled = fCheck3.Checked;
        }

        private void fCheck4_CheckedChanged(object sender, EventArgs e)
        {
            fMin4.Enabled = fCheck4.Checked;
            fMax4.Enabled = fCheck4.Checked;
        }

        private void fCheck5_CheckedChanged(object sender, EventArgs e)
        {
            fMin5.Enabled = fCheck5.Checked;
            fMax5.Enabled = fCheck5.Checked;
        }


        #endregion

        #region min

        private void fMin0_ValueChanged(object sender, EventArgs e)
        {
            if (fMin0.Value > fMax0.Value)
            {
                fMin0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 10.");
            }
        }

        private void fMin1_ValueChanged(object sender, EventArgs e)
        {
            if (fMin1.Value > fMax1.Value)
            {
                fMin1.Value = 1;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 1.");
            }
        }

        private void fMin2_ValueChanged(object sender, EventArgs e)
        {
            if (fMin2.Value > fMax2.Value)
            {
                fMin2.Value = 80;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 80.");
            }
        }

        private void fMin3_ValueChanged(object sender, EventArgs e)
        {
            if (fMin3.Value > fMax3.Value)
            {
                fMin3.Value = 18;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 18.");
            }
        }

        private void fMin4_ValueChanged(object sender, EventArgs e)
        {
            if (fMin4.Value > fMax4.Value)
            {
                fMin4.Value = 40;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 40.");
            }
        }

        private void fMin5_ValueChanged(object sender, EventArgs e)
        {
            if (fMin5.Value > fMax5.Value)
            {
                fMin5.Value = 30;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 30.");
            }
        }


        #endregion

        #region max

        private void fMax0_ValueChanged(object sender, EventArgs e)
        {
            if (fMax0.Value < fMin0.Value)
            {
                fMax0.Value = 128;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 128.");
            }
        }

        private void fMax1_ValueChanged(object sender, EventArgs e)
        {
            if (fMax1.Value < fMin1.Value)
            {
                fMax1.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        private void fMax2_ValueChanged(object sender, EventArgs e)
        {
            if (fMax2.Value < fMin2.Value)
            {
                fMax2.Value = 1000;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1000.");
            }
        }

        private void fMax3_ValueChanged(object sender, EventArgs e)
        {
            if (fMax3.Value < fMin3.Value)
            {
                fMax3.Value = 100;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 100.");
            }
        }

        private void fMax4_ValueChanged(object sender, EventArgs e)
        {
            if (fMax4.Value < fMin4.Value)
            {
                fMax4.Value = 1024;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 1024.");
            }
        }

        private void fMax5_ValueChanged(object sender, EventArgs e)
        {
            if (fMax5.Value < fMin5.Value)
            {
                fMax5.Value = 256;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 256.");
            }
        }


        #endregion

        #endregion

        private void amssnCustom_CheckedChanged(object sender, EventArgs e)
        {
            amssnGroup.Enabled = true;
            amssnCheckOffButt.Enabled = true;
            amssnCheckOnButt.Enabled = true;
        }

        private void amssnChaos_CheckedChanged(object sender, EventArgs e)
        {
            amssnGroup.Enabled = false;
            amssnCheckOffButt.Enabled = false;
            amssnCheckOnButt.Enabled = false;
        }

        #region auto mission

        #region check

        private void amssnCheck0_CheckedChanged(object sender, EventArgs e)
        {
            amssnMin0.Enabled = amssnCheck0.Checked;
            amssnMax0.Enabled = amssnCheck0.Checked;
        }

        private void amssnCheck1_CheckedChanged(object sender, EventArgs e)
        {
            amssnMin1.Enabled = amssnCheck1.Checked;
            amssnMax1.Enabled = amssnCheck1.Checked;
        }

        #endregion

        #region min

        private void amssnMin0_ValueChanged(object sender, EventArgs e)
        {
            if (amssnMin0.Value > amssnMax0.Value)
            {
                amssnMin0.Value = 3;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 3.");
            }
        }

        private void amssnMin1_ValueChanged(object sender, EventArgs e)
        {
            if (amssnMin1.Value > amssnMax1.Value)
            {
                amssnMin1.Value = 3;
                System.Windows.Forms.MessageBox.Show("Min value can't be larger than max value. Value reset to 3.");
            }
        }

        #endregion

        #region max

        private void amssnMax0_ValueChanged(object sender, EventArgs e)
        {
            if (amssnMax0.Value < amssnMin0.Value)
            {
                amssnMax0.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        private void amssnMax1_ValueChanged(object sender, EventArgs e)
        {
            if (amssnMax1.Value < amssnMin1.Value)
            {
                amssnMax1.Value = 10;
                System.Windows.Forms.MessageBox.Show("Max value can't be smaller than min value. Value reset to 10.");
            }
        }

        #endregion

        #endregion

        private void cCheckOffButt_Click(object sender, EventArgs e)
        {
            //City 0
            c0DiffCheck.Checked = false;
            c0FireCheck.Checked = false;
            c0CrimeCheck.Checked = false;
            c0RescueCheck.Checked = false;
            c0RiotCheck.Checked = false;
            c0TrafficCheck.Checked = false;
            c0MedevacCheck.Checked = false;
            c0TransportCheck.Checked = false;
            c0DayCheck.Checked = false;
            c0PointCheck.Checked = false;
            c0MoneyCheck.Checked = false;
            //City 1
            c1DiffCheck.Checked = false;
            c1FireCheck.Checked = false;
            c1CrimeCheck.Checked = false;
            c1RescueCheck.Checked = false;
            c1RiotCheck.Checked = false;
            c1TrafficCheck.Checked = false;
            c1MedevacCheck.Checked = false;
            c1TransportCheck.Checked = false;
            c1DayCheck.Checked = false;
            c1PointCheck.Checked = false;
            c1MoneyCheck.Checked = false;
            //City 2
            c2DiffCheck.Checked = false;
            c2FireCheck.Checked = false;
            c2CrimeCheck.Checked = false;
            c2RescueCheck.Checked = false;
            c2RiotCheck.Checked = false;
            c2TrafficCheck.Checked = false;
            c2MedevacCheck.Checked = false;
            c2TransportCheck.Checked = false;
            c2DayCheck.Checked = false;
            c2PointCheck.Checked = false;
            c2MoneyCheck.Checked = false;
            //City 3
            c3DiffCheck.Checked = false;
            c3FireCheck.Checked = false;
            c3CrimeCheck.Checked = false;
            c3RescueCheck.Checked = false;
            c3RiotCheck.Checked = false;
            c3TrafficCheck.Checked = false;
            c3MedevacCheck.Checked = false;
            c3TransportCheck.Checked = false;
            c3DayCheck.Checked = false;
            c3PointCheck.Checked = false;
            c3MoneyCheck.Checked = false;
            //City 4
            c4DiffCheck.Checked = false;
            c4FireCheck.Checked = false;
            c4CrimeCheck.Checked = false;
            c4RescueCheck.Checked = false;
            c4RiotCheck.Checked = false;
            c4TrafficCheck.Checked = false;
            c4MedevacCheck.Checked = false;
            c4TransportCheck.Checked = false;
            c4DayCheck.Checked = false;
            c4PointCheck.Checked = false;
            c4MoneyCheck.Checked = false;
            //City 5
            c5DiffCheck.Checked = false;
            c5FireCheck.Checked = false;
            c5CrimeCheck.Checked = false;
            c5RescueCheck.Checked = false;
            c5RiotCheck.Checked = false;
            c5TrafficCheck.Checked = false;
            c5MedevacCheck.Checked = false;
            c5TransportCheck.Checked = false;
            c5DayCheck.Checked = false;
            c5PointCheck.Checked = false;
            c5MoneyCheck.Checked = false;
            //City 6
            c6DiffCheck.Checked = false;
            c6FireCheck.Checked = false;
            c6CrimeCheck.Checked = false;
            c6RescueCheck.Checked = false;
            c6RiotCheck.Checked = false;
            c6TrafficCheck.Checked = false;
            c6MedevacCheck.Checked = false;
            c6TransportCheck.Checked = false;
            c6DayCheck.Checked = false;
            c6PointCheck.Checked = false;
            c6MoneyCheck.Checked = false;
            //City 7
            c7DiffCheck.Checked = false;
            c7FireCheck.Checked = false;
            c7CrimeCheck.Checked = false;
            c7RescueCheck.Checked = false;
            c7RiotCheck.Checked = false;
            c7TrafficCheck.Checked = false;
            c7MedevacCheck.Checked = false;
            c7TransportCheck.Checked = false;
            c7DayCheck.Checked = false;
            c7PointCheck.Checked = false;
            c7MoneyCheck.Checked = false;
            //City 8
            c8DiffCheck.Checked = false;
            c8FireCheck.Checked = false;
            c8CrimeCheck.Checked = false;
            c8RescueCheck.Checked = false;
            c8RiotCheck.Checked = false;
            c8TrafficCheck.Checked = false;
            c8MedevacCheck.Checked = false;
            c8TransportCheck.Checked = false;
            c8DayCheck.Checked = false;
            c8PointCheck.Checked = false;
            c8MoneyCheck.Checked = false;
            //City 9
            c9DiffCheck.Checked = false;
            c9FireCheck.Checked = false;
            c9CrimeCheck.Checked = false;
            c9RescueCheck.Checked = false;
            c9RiotCheck.Checked = false;
            c9TrafficCheck.Checked = false;
            c9MedevacCheck.Checked = false;
            c9TransportCheck.Checked = false;
            c9DayCheck.Checked = false;
            c9PointCheck.Checked = false;
            c9MoneyCheck.Checked = false;
            //City 10
            c10DiffCheck.Checked = false;
            c10FireCheck.Checked = false;
            c10CrimeCheck.Checked = false;
            c10RescueCheck.Checked = false;
            c10RiotCheck.Checked = false;
            c10TrafficCheck.Checked = false;
            c10MedevacCheck.Checked = false;
            c10TransportCheck.Checked = false;
            c10DayCheck.Checked = false;
            c10PointCheck.Checked = false;
            c10MoneyCheck.Checked = false;
            //City 11
            c11DiffCheck.Checked = false;
            c11FireCheck.Checked = false;
            c11CrimeCheck.Checked = false;
            c11RescueCheck.Checked = false;
            c11RiotCheck.Checked = false;
            c11TrafficCheck.Checked = false;
            c11MedevacCheck.Checked = false;
            c11TransportCheck.Checked = false;
            c11DayCheck.Checked = false;
            c11PointCheck.Checked = false;
            c11MoneyCheck.Checked = false;
            //City 12
            c12DiffCheck.Checked = false;
            c12FireCheck.Checked = false;
            c12CrimeCheck.Checked = false;
            c12RescueCheck.Checked = false;
            c12RiotCheck.Checked = false;
            c12TrafficCheck.Checked = false;
            c12MedevacCheck.Checked = false;
            c12TransportCheck.Checked = false;
            c12DayCheck.Checked = false;
            c12PointCheck.Checked = false;
            c12MoneyCheck.Checked = false;
            //City 13
            c13DiffCheck.Checked = false;
            c13FireCheck.Checked = false;
            c13CrimeCheck.Checked = false;
            c13RescueCheck.Checked = false;
            c13RiotCheck.Checked = false;
            c13TrafficCheck.Checked = false;
            c13MedevacCheck.Checked = false;
            c13TransportCheck.Checked = false;
            c13DayCheck.Checked = false;
            c13PointCheck.Checked = false;
            c13MoneyCheck.Checked = false;
            //City 14
            c14DiffCheck.Checked = false;
            c14FireCheck.Checked = false;
            c14CrimeCheck.Checked = false;
            c14RescueCheck.Checked = false;
            c14RiotCheck.Checked = false;
            c14TrafficCheck.Checked = false;
            c14MedevacCheck.Checked = false;
            c14TransportCheck.Checked = false;
            c14DayCheck.Checked = false;
            c14PointCheck.Checked = false;
            c14MoneyCheck.Checked = false;
            //City 15
            c15DiffCheck.Checked = false;
            c15FireCheck.Checked = false;
            c15CrimeCheck.Checked = false;
            c15RescueCheck.Checked = false;
            c15RiotCheck.Checked = false;
            c15TrafficCheck.Checked = false;
            c15MedevacCheck.Checked = false;
            c15TransportCheck.Checked = false;
            c15DayCheck.Checked = false;
            c15PointCheck.Checked = false;
            c15MoneyCheck.Checked = false;
            //City 16
            c16DiffCheck.Checked = false;
            c16FireCheck.Checked = false;
            c16CrimeCheck.Checked = false;
            c16RescueCheck.Checked = false;
            c16RiotCheck.Checked = false;
            c16TrafficCheck.Checked = false;
            c16MedevacCheck.Checked = false;
            c16TransportCheck.Checked = false;
            c16DayCheck.Checked = false;
            c16PointCheck.Checked = false;
            c16MoneyCheck.Checked = false;
            //City 17
            c17DiffCheck.Checked = false;
            c17FireCheck.Checked = false;
            c17CrimeCheck.Checked = false;
            c17RescueCheck.Checked = false;
            c17RiotCheck.Checked = false;
            c17TrafficCheck.Checked = false;
            c17MedevacCheck.Checked = false;
            c17TransportCheck.Checked = false;
            c17DayCheck.Checked = false;
            c17PointCheck.Checked = false;
            c17MoneyCheck.Checked = false;
            //City 18
            c18DiffCheck.Checked = false;
            c18FireCheck.Checked = false;
            c18CrimeCheck.Checked = false;
            c18RescueCheck.Checked = false;
            c18RiotCheck.Checked = false;
            c18TrafficCheck.Checked = false;
            c18MedevacCheck.Checked = false;
            c18TransportCheck.Checked = false;
            c18DayCheck.Checked = false;
            c18PointCheck.Checked = false;
            c18MoneyCheck.Checked = false;
            //City 19
            c19DiffCheck.Checked = false;
            c19FireCheck.Checked = false;
            c19CrimeCheck.Checked = false;
            c19RescueCheck.Checked = false;
            c19RiotCheck.Checked = false;
            c19TrafficCheck.Checked = false;
            c19MedevacCheck.Checked = false;
            c19TransportCheck.Checked = false;
            c19DayCheck.Checked = false;
            c19PointCheck.Checked = false;
            c19MoneyCheck.Checked = false;
            //City 20
            c20DiffCheck.Checked = false;
            c20FireCheck.Checked = false;
            c20CrimeCheck.Checked = false;
            c20RescueCheck.Checked = false;
            c20RiotCheck.Checked = false;
            c20TrafficCheck.Checked = false;
            c20MedevacCheck.Checked = false;
            c20TransportCheck.Checked = false;
            c20DayCheck.Checked = false;
            c20PointCheck.Checked = false;
            c20MoneyCheck.Checked = false;
            //City 21
            c21DiffCheck.Checked = false;
            c21FireCheck.Checked = false;
            c21CrimeCheck.Checked = false;
            c21RescueCheck.Checked = false;
            c21RiotCheck.Checked = false;
            c21TrafficCheck.Checked = false;
            c21MedevacCheck.Checked = false;
            c21TransportCheck.Checked = false;
            c21DayCheck.Checked = false;
            c21PointCheck.Checked = false;
            c21MoneyCheck.Checked = false;
            //City 22
            c22DiffCheck.Checked = false;
            c22FireCheck.Checked = false;
            c22CrimeCheck.Checked = false;
            c22RescueCheck.Checked = false;
            c22RiotCheck.Checked = false;
            c22TrafficCheck.Checked = false;
            c22MedevacCheck.Checked = false;
            c22TransportCheck.Checked = false;
            c22DayCheck.Checked = false;
            c22PointCheck.Checked = false;
            c22MoneyCheck.Checked = false;
            //City 23
            c23DiffCheck.Checked = false;
            c23FireCheck.Checked = false;
            c23CrimeCheck.Checked = false;
            c23RescueCheck.Checked = false;
            c23RiotCheck.Checked = false;
            c23TrafficCheck.Checked = false;
            c23MedevacCheck.Checked = false;
            c23TransportCheck.Checked = false;
            c23DayCheck.Checked = false;
            c23PointCheck.Checked = false;
            c23MoneyCheck.Checked = false;
            //City 24
            c24DiffCheck.Checked = false;
            c24FireCheck.Checked = false;
            c24CrimeCheck.Checked = false;
            c24RescueCheck.Checked = false;
            c24RiotCheck.Checked = false;
            c24TrafficCheck.Checked = false;
            c24MedevacCheck.Checked = false;
            c24TransportCheck.Checked = false;
            c24DayCheck.Checked = false;
            c24PointCheck.Checked = false;
            c24MoneyCheck.Checked = false;
            //City 25
            c25DiffCheck.Checked = false;
            c25FireCheck.Checked = false;
            c25CrimeCheck.Checked = false;
            c25RescueCheck.Checked = false;
            c25RiotCheck.Checked = false;
            c25TrafficCheck.Checked = false;
            c25MedevacCheck.Checked = false;
            c25TransportCheck.Checked = false;
            c25DayCheck.Checked = false;
            c25PointCheck.Checked = false;
            c25MoneyCheck.Checked = false;
            //City 26
            c26DiffCheck.Checked = false;
            c26FireCheck.Checked = false;
            c26CrimeCheck.Checked = false;
            c26RescueCheck.Checked = false;
            c26RiotCheck.Checked = false;
            c26TrafficCheck.Checked = false;
            c26MedevacCheck.Checked = false;
            c26TransportCheck.Checked = false;
            c26DayCheck.Checked = false;
            c26PointCheck.Checked = false;
            c26MoneyCheck.Checked = false;
            //City 27
            c27DiffCheck.Checked = false;
            c27FireCheck.Checked = false;
            c27CrimeCheck.Checked = false;
            c27RescueCheck.Checked = false;
            c27RiotCheck.Checked = false;
            c27TrafficCheck.Checked = false;
            c27MedevacCheck.Checked = false;
            c27TransportCheck.Checked = false;
            c27DayCheck.Checked = false;
            c27PointCheck.Checked = false;
            c27MoneyCheck.Checked = false;
            //City 28
            c28DiffCheck.Checked = false;
            c28FireCheck.Checked = false;
            c28CrimeCheck.Checked = false;
            c28RescueCheck.Checked = false;
            c28RiotCheck.Checked = false;
            c28TrafficCheck.Checked = false;
            c28MedevacCheck.Checked = false;
            c28TransportCheck.Checked = false;
            c28DayCheck.Checked = false;
            c28PointCheck.Checked = false;
            c28MoneyCheck.Checked = false;
            //City 29
            c29DiffCheck.Checked = false;
            c29FireCheck.Checked = false;
            c29CrimeCheck.Checked = false;
            c29RescueCheck.Checked = false;
            c29RiotCheck.Checked = false;
            c29TrafficCheck.Checked = false;
            c29MedevacCheck.Checked = false;
            c29TransportCheck.Checked = false;
            c29DayCheck.Checked = false;
            c29PointCheck.Checked = false;
            c29MoneyCheck.Checked = false;
        }

        private void cCheckOnButt_Click(object sender, EventArgs e)
        {
            //City 0
            c0DiffCheck.Checked = true;
            c0FireCheck.Checked = true;
            c0CrimeCheck.Checked = true;
            c0RescueCheck.Checked = true;
            c0RiotCheck.Checked = true;
            c0TrafficCheck.Checked = true;
            c0MedevacCheck.Checked = true;
            c0TransportCheck.Checked = true;
            c0DayCheck.Checked = true;
            c0PointCheck.Checked = true;
            c0MoneyCheck.Checked = true;
            //City 1
            c1DiffCheck.Checked = true;
            c1FireCheck.Checked = true;
            c1CrimeCheck.Checked = true;
            c1RescueCheck.Checked = true;
            c1RiotCheck.Checked = true;
            c1TrafficCheck.Checked = true;
            c1MedevacCheck.Checked = true;
            c1TransportCheck.Checked = true;
            c1DayCheck.Checked = true;
            c1PointCheck.Checked = true;
            c1MoneyCheck.Checked = true;
            //City 2
            c2DiffCheck.Checked = true;
            c2FireCheck.Checked = true;
            c2CrimeCheck.Checked = true;
            c2RescueCheck.Checked = true;
            c2RiotCheck.Checked = true;
            c2TrafficCheck.Checked = true;
            c2MedevacCheck.Checked = true;
            c2TransportCheck.Checked = true;
            c2DayCheck.Checked = true;
            c2PointCheck.Checked = true;
            c2MoneyCheck.Checked = true;
            //City 3
            c3DiffCheck.Checked = true;
            c3FireCheck.Checked = true;
            c3CrimeCheck.Checked = true;
            c3RescueCheck.Checked = true;
            c3RiotCheck.Checked = true;
            c3TrafficCheck.Checked = true;
            c3MedevacCheck.Checked = true;
            c3TransportCheck.Checked = true;
            c3DayCheck.Checked = true;
            c3PointCheck.Checked = true;
            c3MoneyCheck.Checked = true;
            //City 4
            c4DiffCheck.Checked = true;
            c4FireCheck.Checked = true;
            c4CrimeCheck.Checked = true;
            c4RescueCheck.Checked = true;
            c4RiotCheck.Checked = true;
            c4TrafficCheck.Checked = true;
            c4MedevacCheck.Checked = true;
            c4TransportCheck.Checked = true;
            c4DayCheck.Checked = true;
            c4PointCheck.Checked = true;
            c4MoneyCheck.Checked = true;
            //City 5
            c5DiffCheck.Checked = true;
            c5FireCheck.Checked = true;
            c5CrimeCheck.Checked = true;
            c5RescueCheck.Checked = true;
            c5RiotCheck.Checked = true;
            c5TrafficCheck.Checked = true;
            c5MedevacCheck.Checked = true;
            c5TransportCheck.Checked = true;
            c5DayCheck.Checked = true;
            c5PointCheck.Checked = true;
            c5MoneyCheck.Checked = true;
            //City 6
            c6DiffCheck.Checked = true;
            c6FireCheck.Checked = true;
            c6CrimeCheck.Checked = true;
            c6RescueCheck.Checked = true;
            c6RiotCheck.Checked = true;
            c6TrafficCheck.Checked = true;
            c6MedevacCheck.Checked = true;
            c6TransportCheck.Checked = true;
            c6DayCheck.Checked = true;
            c6PointCheck.Checked = true;
            c6MoneyCheck.Checked = true;
            //City 7
            c7DiffCheck.Checked = true;
            c7FireCheck.Checked = true;
            c7CrimeCheck.Checked = true;
            c7RescueCheck.Checked = true;
            c7RiotCheck.Checked = true;
            c7TrafficCheck.Checked = true;
            c7MedevacCheck.Checked = true;
            c7TransportCheck.Checked = true;
            c7DayCheck.Checked = true;
            c7PointCheck.Checked = true;
            c7MoneyCheck.Checked = true;
            //City 8
            c8DiffCheck.Checked = true;
            c8FireCheck.Checked = true;
            c8CrimeCheck.Checked = true;
            c8RescueCheck.Checked = true;
            c8RiotCheck.Checked = true;
            c8TrafficCheck.Checked = true;
            c8MedevacCheck.Checked = true;
            c8TransportCheck.Checked = true;
            c8DayCheck.Checked = true;
            c8PointCheck.Checked = true;
            c8MoneyCheck.Checked = true;
            //City 9
            c9DiffCheck.Checked = true;
            c9FireCheck.Checked = true;
            c9CrimeCheck.Checked = true;
            c9RescueCheck.Checked = true;
            c9RiotCheck.Checked = true;
            c9TrafficCheck.Checked = true;
            c9MedevacCheck.Checked = true;
            c9TransportCheck.Checked = true;
            c9DayCheck.Checked = true;
            c9PointCheck.Checked = true;
            c9MoneyCheck.Checked = true;
            //City 10
            c10DiffCheck.Checked = true;
            c10FireCheck.Checked = true;
            c10CrimeCheck.Checked = true;
            c10RescueCheck.Checked = true;
            c10RiotCheck.Checked = true;
            c10TrafficCheck.Checked = true;
            c10MedevacCheck.Checked = true;
            c10TransportCheck.Checked = true;
            c10DayCheck.Checked = true;
            c10PointCheck.Checked = true;
            c10MoneyCheck.Checked = true;
            //City 11
            c11DiffCheck.Checked = true;
            c11FireCheck.Checked = true;
            c11CrimeCheck.Checked = true;
            c11RescueCheck.Checked = true;
            c11RiotCheck.Checked = true;
            c11TrafficCheck.Checked = true;
            c11MedevacCheck.Checked = true;
            c11TransportCheck.Checked = true;
            c11DayCheck.Checked = true;
            c11PointCheck.Checked = true;
            c11MoneyCheck.Checked = true;
            //City 12
            c12DiffCheck.Checked = true;
            c12FireCheck.Checked = true;
            c12CrimeCheck.Checked = true;
            c12RescueCheck.Checked = true;
            c12RiotCheck.Checked = true;
            c12TrafficCheck.Checked = true;
            c12MedevacCheck.Checked = true;
            c12TransportCheck.Checked = true;
            c12DayCheck.Checked = true;
            c12PointCheck.Checked = true;
            c12MoneyCheck.Checked = true;
            //City 13
            c13DiffCheck.Checked = true;
            c13FireCheck.Checked = true;
            c13CrimeCheck.Checked = true;
            c13RescueCheck.Checked = true;
            c13RiotCheck.Checked = true;
            c13TrafficCheck.Checked = true;
            c13MedevacCheck.Checked = true;
            c13TransportCheck.Checked = true;
            c13DayCheck.Checked = true;
            c13PointCheck.Checked = true;
            c13MoneyCheck.Checked = true;
            //City 14
            c14DiffCheck.Checked = true;
            c14FireCheck.Checked = true;
            c14CrimeCheck.Checked = true;
            c14RescueCheck.Checked = true;
            c14RiotCheck.Checked = true;
            c14TrafficCheck.Checked = true;
            c14MedevacCheck.Checked = true;
            c14TransportCheck.Checked = true;
            c14DayCheck.Checked = true;
            c14PointCheck.Checked = true;
            c14MoneyCheck.Checked = true;
            //City 15
            c15DiffCheck.Checked = true;
            c15FireCheck.Checked = true;
            c15CrimeCheck.Checked = true;
            c15RescueCheck.Checked = true;
            c15RiotCheck.Checked = true;
            c15TrafficCheck.Checked = true;
            c15MedevacCheck.Checked = true;
            c15TransportCheck.Checked = true;
            c15DayCheck.Checked = true;
            c15PointCheck.Checked = true;
            c15MoneyCheck.Checked = true;
            //City 16
            c16DiffCheck.Checked = true;
            c16FireCheck.Checked = true;
            c16CrimeCheck.Checked = true;
            c16RescueCheck.Checked = true;
            c16RiotCheck.Checked = true;
            c16TrafficCheck.Checked = true;
            c16MedevacCheck.Checked = true;
            c16TransportCheck.Checked = true;
            c16DayCheck.Checked = true;
            c16PointCheck.Checked = true;
            c16MoneyCheck.Checked = true;
            //City 17
            c17DiffCheck.Checked = true;
            c17FireCheck.Checked = true;
            c17CrimeCheck.Checked = true;
            c17RescueCheck.Checked = true;
            c17RiotCheck.Checked = true;
            c17TrafficCheck.Checked = true;
            c17MedevacCheck.Checked = true;
            c17TransportCheck.Checked = true;
            c17DayCheck.Checked = true;
            c17PointCheck.Checked = true;
            c17MoneyCheck.Checked = true;
            //City 18
            c18DiffCheck.Checked = true;
            c18FireCheck.Checked = true;
            c18CrimeCheck.Checked = true;
            c18RescueCheck.Checked = true;
            c18RiotCheck.Checked = true;
            c18TrafficCheck.Checked = true;
            c18MedevacCheck.Checked = true;
            c18TransportCheck.Checked = true;
            c18DayCheck.Checked = true;
            c18PointCheck.Checked = true;
            c18MoneyCheck.Checked = true;
            //City 19
            c19DiffCheck.Checked = true;
            c19FireCheck.Checked = true;
            c19CrimeCheck.Checked = true;
            c19RescueCheck.Checked = true;
            c19RiotCheck.Checked = true;
            c19TrafficCheck.Checked = true;
            c19MedevacCheck.Checked = true;
            c19TransportCheck.Checked = true;
            c19DayCheck.Checked = true;
            c19PointCheck.Checked = true;
            c19MoneyCheck.Checked = true;
            //City 20
            c20DiffCheck.Checked = true;
            c20FireCheck.Checked = true;
            c20CrimeCheck.Checked = true;
            c20RescueCheck.Checked = true;
            c20RiotCheck.Checked = true;
            c20TrafficCheck.Checked = true;
            c20MedevacCheck.Checked = true;
            c20TransportCheck.Checked = true;
            c20DayCheck.Checked = true;
            c20PointCheck.Checked = true;
            c20MoneyCheck.Checked = true;
            //City 21
            c21DiffCheck.Checked = true;
            c21FireCheck.Checked = true;
            c21CrimeCheck.Checked = true;
            c21RescueCheck.Checked = true;
            c21RiotCheck.Checked = true;
            c21TrafficCheck.Checked = true;
            c21MedevacCheck.Checked = true;
            c21TransportCheck.Checked = true;
            c21DayCheck.Checked = true;
            c21PointCheck.Checked = true;
            c21MoneyCheck.Checked = true;
            //City 22
            c22DiffCheck.Checked = true;
            c22FireCheck.Checked = true;
            c22CrimeCheck.Checked = true;
            c22RescueCheck.Checked = true;
            c22RiotCheck.Checked = true;
            c22TrafficCheck.Checked = true;
            c22MedevacCheck.Checked = true;
            c22TransportCheck.Checked = true;
            c22DayCheck.Checked = true;
            c22PointCheck.Checked = true;
            c22MoneyCheck.Checked = true;
            //City 23
            c23DiffCheck.Checked = true;
            c23FireCheck.Checked = true;
            c23CrimeCheck.Checked = true;
            c23RescueCheck.Checked = true;
            c23RiotCheck.Checked = true;
            c23TrafficCheck.Checked = true;
            c23MedevacCheck.Checked = true;
            c23TransportCheck.Checked = true;
            c23DayCheck.Checked = true;
            c23PointCheck.Checked = true;
            c23MoneyCheck.Checked = true;
            //City 24
            c24DiffCheck.Checked = true;
            c24FireCheck.Checked = true;
            c24CrimeCheck.Checked = true;
            c24RescueCheck.Checked = true;
            c24RiotCheck.Checked = true;
            c24TrafficCheck.Checked = true;
            c24MedevacCheck.Checked = true;
            c24TransportCheck.Checked = true;
            c24DayCheck.Checked = true;
            c24PointCheck.Checked = true;
            c24MoneyCheck.Checked = true;
            //City 25
            c25DiffCheck.Checked = true;
            c25FireCheck.Checked = true;
            c25CrimeCheck.Checked = true;
            c25RescueCheck.Checked = true;
            c25RiotCheck.Checked = true;
            c25TrafficCheck.Checked = true;
            c25MedevacCheck.Checked = true;
            c25TransportCheck.Checked = true;
            c25DayCheck.Checked = true;
            c25PointCheck.Checked = true;
            c25MoneyCheck.Checked = true;
            //City 26
            c26DiffCheck.Checked = true;
            c26FireCheck.Checked = true;
            c26CrimeCheck.Checked = true;
            c26RescueCheck.Checked = true;
            c26RiotCheck.Checked = true;
            c26TrafficCheck.Checked = true;
            c26MedevacCheck.Checked = true;
            c26TransportCheck.Checked = true;
            c26DayCheck.Checked = true;
            c26PointCheck.Checked = true;
            c26MoneyCheck.Checked = true;
            //City 27
            c27DiffCheck.Checked = true;
            c27FireCheck.Checked = true;
            c27CrimeCheck.Checked = true;
            c27RescueCheck.Checked = true;
            c27RiotCheck.Checked = true;
            c27TrafficCheck.Checked = true;
            c27MedevacCheck.Checked = true;
            c27TransportCheck.Checked = true;
            c27DayCheck.Checked = true;
            c27PointCheck.Checked = true;
            c27MoneyCheck.Checked = true;
            //City 28
            c28DiffCheck.Checked = true;
            c28FireCheck.Checked = true;
            c28CrimeCheck.Checked = true;
            c28RescueCheck.Checked = true;
            c28RiotCheck.Checked = true;
            c28TrafficCheck.Checked = true;
            c28MedevacCheck.Checked = true;
            c28TransportCheck.Checked = true;
            c28DayCheck.Checked = true;
            c28PointCheck.Checked = true;
            c28MoneyCheck.Checked = true;
            //City 29
            c29DiffCheck.Checked = true;
            c29FireCheck.Checked = true;
            c29CrimeCheck.Checked = true;
            c29RescueCheck.Checked = true;
            c29RiotCheck.Checked = true;
            c29TrafficCheck.Checked = true;
            c29MedevacCheck.Checked = true;
            c29TransportCheck.Checked = true;
            c29DayCheck.Checked = true;
            c29PointCheck.Checked = true;
            c29MoneyCheck.Checked = true;
        }

        private void mCheckOffButt_Click(object sender, EventArgs e)
        {
            //General
            genCheck1.Checked = false;
            genCheck2.Checked = false;
            genCheck3.Checked = false;
            genCheck4.Checked = false;
            genCheck5.Checked = false;
            //Riot
            riotCheck1.Checked = false;
            riotCheck2.Checked = false;
            riotCheck3.Checked = false;
            riotCheck4.Checked = false;
            riotCheck5.Checked = false;
            //Rescue
            rescueCheck1.Checked = false;
            rescueCheck2.Checked = false;
            rescueCheck3.Checked = false;
            //Transport
            transportCheck1.Checked = false;
            transportCheck2.Checked = false;
            transportCheck3.Checked = false;
            transportCheck4.Checked = false;
            //Medevac
            medevacCheck1.Checked = false;
            medevacCheck2.Checked = false;
            medevacCheck3.Checked = false;
            //Fire
            mFireCheck1.Checked = false;
            mFireCheck2.Checked = false;
            mFireCheck3.Checked = false;
            mFireCheck4.Checked = false;
            mFireCheck5.Checked = false;
            mFireCheck6.Checked = false;
            mFireCheck7.Checked = false;
            mFireCheck8.Checked = false;
            mFireCheck9.Checked = false;
            mFireCheck10.Checked = false;
            mFireCheck11.Checked = false;
            mFireCheck12.Checked = false;
            mFireCheck13.Checked = false;
            mFireCheck14.Checked = false;
            mFireCheck15.Checked = false;
            mFireCheck16.Checked = false;
            mFireCheck17.Checked = false;
            mFireCheck18.Checked = false;
            mFireCheck19.Checked = false;
            mFireCheck20.Checked = false;
            //Criminal
            crimeCheck1.Checked = false;
            crimeCheck2.Checked = false;
            //Speeder
            speedCheck1.Checked = false;
            speedCheck2.Checked = false;
            speedCheck3.Checked = false;
            //Traffic
            trafficCheck1.Checked = false;
            trafficCheck2.Checked = false;
            trafficCheck3.Checked = false;
        }

        private void mCheckOnButt_Click(object sender, EventArgs e)
        {
            //General
            genCheck1.Checked = true;
            genCheck2.Checked = true;
            genCheck3.Checked = true;
            genCheck4.Checked = true;
            genCheck5.Checked = true;
            //Riot
            riotCheck1.Checked = true;
            riotCheck2.Checked = true;
            riotCheck3.Checked = true;
            riotCheck4.Checked = true;
            riotCheck5.Checked = true;
            //Rescue
            rescueCheck1.Checked = true;
            rescueCheck2.Checked = true;
            rescueCheck3.Checked = true;
            //Transport
            transportCheck1.Checked = true;
            transportCheck2.Checked = true;
            transportCheck3.Checked = true;
            transportCheck4.Checked = true;
            //Medevac
            medevacCheck1.Checked = true;
            medevacCheck2.Checked = true;
            medevacCheck3.Checked = true;
            //Fire
            mFireCheck1.Checked = true;
            mFireCheck2.Checked = true;
            mFireCheck3.Checked = true;
            mFireCheck4.Checked = true;
            mFireCheck5.Checked = true;
            mFireCheck6.Checked = true;
            mFireCheck7.Checked = true;
            mFireCheck8.Checked = true;
            mFireCheck9.Checked = true;
            mFireCheck10.Checked = true;
            mFireCheck11.Checked = true;
            mFireCheck12.Checked = true;
            mFireCheck13.Checked = true;
            mFireCheck14.Checked = true;
            mFireCheck15.Checked = true;
            mFireCheck16.Checked = true;
            mFireCheck17.Checked = true;
            mFireCheck18.Checked = true;
            mFireCheck19.Checked = true;
            mFireCheck20.Checked = true;
            //Criminal
            crimeCheck1.Checked = true;
            crimeCheck2.Checked = true;
            //Speeder
            speedCheck1.Checked = true;
            speedCheck2.Checked = true;
            speedCheck3.Checked = true;
            //Traffic
            trafficCheck1.Checked = true;
            trafficCheck2.Checked = true;
            trafficCheck3.Checked = true;
        }

        private void hCheckOffButt_Click(object sender, EventArgs e)
        {
            //heli 0
            mBankCheck0.Checked = false;
            mSlideCheck0.Checked = false;
            mPitchCheck0.Checked = false;
            pitchRateCheck0.Checked = false;
            yawRateCheck0.Checked = false;
            rollRateCheck0.Checked = false;
            slideRateCheck0.Checked = false;
            climbCheck0.Checked = false;
            mLoadCheck0.Checked = false;
            mYawRateCheck0.Checked = false;
            fuelRateCheck0.Checked = false;
            costCheck0.Checked = false;
            damageCheck0.Checked = false;
            fuelCheck0.Checked = false;
            repairCheck0.Checked = false;
            fuelCostCheck0.Checked = false;
            //heli 1
            mBankCheck1.Checked = false;
            mSlideCheck1.Checked = false;
            mPitchCheck1.Checked = false;
            pitchRateCheck1.Checked = false;
            yawRateCheck1.Checked = false;
            rollRateCheck1.Checked = false;
            slideRateCheck1.Checked = false;
            climbCheck1.Checked = false;
            mLoadCheck1.Checked = false;
            mYawRateCheck1.Checked = false;
            fuelRateCheck1.Checked = false;
            costCheck1.Checked = false;
            damageCheck1.Checked = false;
            fuelCheck1.Checked = false;
            repairCheck1.Checked = false;
            fuelCostCheck1.Checked = false;
            //heli 2
            mBankCheck2.Checked = false;
            mSlideCheck2.Checked = false;
            mPitchCheck2.Checked = false;
            pitchRateCheck2.Checked = false;
            yawRateCheck2.Checked = false;
            rollRateCheck2.Checked = false;
            slideRateCheck2.Checked = false;
            climbCheck2.Checked = false;
            mLoadCheck2.Checked = false;
            mYawRateCheck2.Checked = false;
            fuelRateCheck2.Checked = false;
            costCheck2.Checked = false;
            damageCheck2.Checked = false;
            fuelCheck2.Checked = false;
            repairCheck2.Checked = false;
            fuelCostCheck2.Checked = false;
            //heli 3
            mBankCheck3.Checked = false;
            mSlideCheck3.Checked = false;
            mPitchCheck3.Checked = false;
            pitchRateCheck3.Checked = false;
            yawRateCheck3.Checked = false;
            rollRateCheck3.Checked = false;
            slideRateCheck3.Checked = false;
            climbCheck3.Checked = false;
            mLoadCheck3.Checked = false;
            mYawRateCheck3.Checked = false;
            fuelRateCheck3.Checked = false;
            costCheck3.Checked = false;
            damageCheck3.Checked = false;
            fuelCheck3.Checked = false;
            repairCheck3.Checked = false;
            fuelCostCheck3.Checked = false;
            //heli 4
            mBankCheck4.Checked = false;
            mSlideCheck4.Checked = false;
            mPitchCheck4.Checked = false;
            pitchRateCheck4.Checked = false;
            yawRateCheck4.Checked = false;
            rollRateCheck4.Checked = false;
            slideRateCheck4.Checked = false;
            climbCheck4.Checked = false;
            mLoadCheck4.Checked = false;
            mYawRateCheck4.Checked = false;
            fuelRateCheck4.Checked = false;
            costCheck4.Checked = false;
            damageCheck4.Checked = false;
            fuelCheck4.Checked = false;
            repairCheck4.Checked = false;
            fuelCostCheck4.Checked = false;
            //heli 5
            mBankCheck5.Checked = false;
            mSlideCheck5.Checked = false;
            mPitchCheck5.Checked = false;
            pitchRateCheck5.Checked = false;
            yawRateCheck5.Checked = false;
            rollRateCheck5.Checked = false;
            slideRateCheck5.Checked = false;
            climbCheck5.Checked = false;
            mLoadCheck5.Checked = false;
            mYawRateCheck5.Checked = false;
            fuelRateCheck5.Checked = false;
            costCheck5.Checked = false;
            damageCheck5.Checked = false;
            fuelCheck5.Checked = false;
            repairCheck5.Checked = false;
            fuelCostCheck5.Checked = false;
            //heli 6
            mBankCheck6.Checked = false;
            mSlideCheck6.Checked = false;
            mPitchCheck6.Checked = false;
            pitchRateCheck6.Checked = false;
            yawRateCheck6.Checked = false;
            rollRateCheck6.Checked = false;
            slideRateCheck6.Checked = false;
            climbCheck6.Checked = false;
            mLoadCheck6.Checked = false;
            mYawRateCheck6.Checked = false;
            fuelRateCheck6.Checked = false;
            costCheck6.Checked = false;
            damageCheck6.Checked = false;
            fuelCheck6.Checked = false;
            repairCheck6.Checked = false;
            fuelCostCheck6.Checked = false;
            //heli 7
            mBankCheck7.Checked = false;
            mSlideCheck7.Checked = false;
            mPitchCheck7.Checked = false;
            pitchRateCheck7.Checked = false;
            yawRateCheck7.Checked = false;
            rollRateCheck7.Checked = false;
            slideRateCheck7.Checked = false;
            climbCheck7.Checked = false;
            mLoadCheck7.Checked = false;
            mYawRateCheck7.Checked = false;
            fuelRateCheck7.Checked = false;
            costCheck7.Checked = false;
            damageCheck7.Checked = false;
            fuelCheck7.Checked = false;
            repairCheck7.Checked = false;
            fuelCostCheck7.Checked = false;
            //heli 8
            mBankCheck8.Checked = false;
            mSlideCheck8.Checked = false;
            mPitchCheck8.Checked = false;
            pitchRateCheck8.Checked = false;
            yawRateCheck8.Checked = false;
            rollRateCheck8.Checked = false;
            slideRateCheck8.Checked = false;
            climbCheck8.Checked = false;
            mLoadCheck8.Checked = false;
            mYawRateCheck8.Checked = false;
            fuelRateCheck8.Checked = false;
            costCheck8.Checked = false;
            damageCheck8.Checked = false;
            fuelCheck8.Checked = false;
            repairCheck8.Checked = false;
            fuelCostCheck8.Checked = false;
            //Landing
            landCheck0.Checked = false;
            landCheck1.Checked = false;
            landCheck2.Checked = false;
            landCheck3.Checked = false;
            landCheck4.Checked = false;
            //Rope
            ropeCheck0.Checked = false;
            ropeCheck1.Checked = false;
            ropeCheck2.Checked = false;
            ropeCheck3.Checked = false;
            ropeCheck4.Checked = false;
            ropeCheck5.Checked = false;
            //Damage
            dCheck0.Checked = false;
            dCheck1.Checked = false;
            dCheck2.Checked = false;
            dCheck3.Checked = false;
            dCheck4.Checked = false;
            dCheck5.Checked = false;
        }

        private void hCheckOnButt_Click(object sender, EventArgs e)
        {
            //heli 0
            mBankCheck0.Checked = true;
            mSlideCheck0.Checked = true;
            mPitchCheck0.Checked = true;
            pitchRateCheck0.Checked = true;
            yawRateCheck0.Checked = true;
            rollRateCheck0.Checked = true;
            slideRateCheck0.Checked = true;
            climbCheck0.Checked = true;
            mLoadCheck0.Checked = true;
            mYawRateCheck0.Checked = true;
            fuelRateCheck0.Checked = true;
            costCheck0.Checked = true;
            damageCheck0.Checked = true;
            fuelCheck0.Checked = true;
            repairCheck0.Checked = true;
            fuelCostCheck0.Checked = true;
            //heli 1
            mBankCheck1.Checked = true;
            mSlideCheck1.Checked = true;
            mPitchCheck1.Checked = true;
            pitchRateCheck1.Checked = true;
            yawRateCheck1.Checked = true;
            rollRateCheck1.Checked = true;
            slideRateCheck1.Checked = true;
            climbCheck1.Checked = true;
            mLoadCheck1.Checked = true;
            mYawRateCheck1.Checked = true;
            fuelRateCheck1.Checked = true;
            costCheck1.Checked = true;
            damageCheck1.Checked = true;
            fuelCheck1.Checked = true;
            repairCheck1.Checked = true;
            fuelCostCheck1.Checked = true;
            //heli 2
            mBankCheck2.Checked = true;
            mSlideCheck2.Checked = true;
            mPitchCheck2.Checked = true;
            pitchRateCheck2.Checked = true;
            yawRateCheck2.Checked = true;
            rollRateCheck2.Checked = true;
            slideRateCheck2.Checked = true;
            climbCheck2.Checked = true;
            mLoadCheck2.Checked = true;
            mYawRateCheck2.Checked = true;
            fuelRateCheck2.Checked = true;
            costCheck2.Checked = true;
            damageCheck2.Checked = true;
            fuelCheck2.Checked = true;
            repairCheck2.Checked = true;
            fuelCostCheck2.Checked = true;
            //heli 3
            mBankCheck3.Checked = true;
            mSlideCheck3.Checked = true;
            mPitchCheck3.Checked = true;
            pitchRateCheck3.Checked = true;
            yawRateCheck3.Checked = true;
            rollRateCheck3.Checked = true;
            slideRateCheck3.Checked = true;
            climbCheck3.Checked = true;
            mLoadCheck3.Checked = true;
            mYawRateCheck3.Checked = true;
            fuelRateCheck3.Checked = true;
            costCheck3.Checked = true;
            damageCheck3.Checked = true;
            fuelCheck3.Checked = true;
            repairCheck3.Checked = true;
            fuelCostCheck3.Checked = true;
            //heli 4
            mBankCheck4.Checked = true;
            mSlideCheck4.Checked = true;
            mPitchCheck4.Checked = true;
            pitchRateCheck4.Checked = true;
            yawRateCheck4.Checked = true;
            rollRateCheck4.Checked = true;
            slideRateCheck4.Checked = true;
            climbCheck4.Checked = true;
            mLoadCheck4.Checked = true;
            mYawRateCheck4.Checked = true;
            fuelRateCheck4.Checked = true;
            costCheck4.Checked = true;
            damageCheck4.Checked = true;
            fuelCheck4.Checked = true;
            repairCheck4.Checked = true;
            fuelCostCheck4.Checked = true;
            //heli 5
            mBankCheck5.Checked = true;
            mSlideCheck5.Checked = true;
            mPitchCheck5.Checked = true;
            pitchRateCheck5.Checked = true;
            yawRateCheck5.Checked = true;
            rollRateCheck5.Checked = true;
            slideRateCheck5.Checked = true;
            climbCheck5.Checked = true;
            mLoadCheck5.Checked = true;
            mYawRateCheck5.Checked = true;
            fuelRateCheck5.Checked = true;
            costCheck5.Checked = true;
            damageCheck5.Checked = true;
            fuelCheck5.Checked = true;
            repairCheck5.Checked = true;
            fuelCostCheck5.Checked = true;
            //heli 6
            mBankCheck6.Checked = true;
            mSlideCheck6.Checked = true;
            mPitchCheck6.Checked = true;
            pitchRateCheck6.Checked = true;
            yawRateCheck6.Checked = true;
            rollRateCheck6.Checked = true;
            slideRateCheck6.Checked = true;
            climbCheck6.Checked = true;
            mLoadCheck6.Checked = true;
            mYawRateCheck6.Checked = true;
            fuelRateCheck6.Checked = true;
            costCheck6.Checked = true;
            damageCheck6.Checked = true;
            fuelCheck6.Checked = true;
            repairCheck6.Checked = true;
            fuelCostCheck6.Checked = true;
            //heli 7
            mBankCheck7.Checked = true;
            mSlideCheck7.Checked = true;
            mPitchCheck7.Checked = true;
            pitchRateCheck7.Checked = true;
            yawRateCheck7.Checked = true;
            rollRateCheck7.Checked = true;
            slideRateCheck7.Checked = true;
            climbCheck7.Checked = true;
            mLoadCheck7.Checked = true;
            mYawRateCheck7.Checked = true;
            fuelRateCheck7.Checked = true;
            costCheck7.Checked = true;
            damageCheck7.Checked = true;
            fuelCheck7.Checked = true;
            repairCheck7.Checked = true;
            fuelCostCheck7.Checked = true;
            //heli 8
            mBankCheck8.Checked = true;
            mSlideCheck8.Checked = true;
            mPitchCheck8.Checked = true;
            pitchRateCheck8.Checked = true;
            yawRateCheck8.Checked = true;
            rollRateCheck8.Checked = true;
            slideRateCheck8.Checked = true;
            climbCheck8.Checked = true;
            mLoadCheck8.Checked = true;
            mYawRateCheck8.Checked = true;
            fuelRateCheck8.Checked = true;
            costCheck8.Checked = true;
            damageCheck8.Checked = true;
            fuelCheck8.Checked = true;
            repairCheck8.Checked = true;
            fuelCostCheck8.Checked = true;
            //Landing
            landCheck0.Checked = true;
            landCheck1.Checked = true;
            landCheck2.Checked = true;
            landCheck3.Checked = true;
            landCheck4.Checked = true;
            //Rope
            ropeCheck0.Checked = true;
            ropeCheck1.Checked = true;
            ropeCheck2.Checked = true;
            ropeCheck3.Checked = true;
            ropeCheck4.Checked = true;
            ropeCheck5.Checked = true;
            //Damage
            dCheck0.Checked = true;
            dCheck1.Checked = true;
            dCheck2.Checked = true;
            dCheck3.Checked = true;
            dCheck4.Checked = true;
            dCheck5.Checked = true;
        }

        private void fCheckOffButt_Click(object sender, EventArgs e)
        {
            fCheck0.Checked = false;
            fCheck1.Checked = false;
            fCheck2.Checked = false;
            fCheck3.Checked = false;
            fCheck4.Checked = false;
            fCheck5.Checked = false;
        }

        private void fCheckOnButt_Click(object sender, EventArgs e)
        {
            fCheck0.Checked = true;
            fCheck1.Checked = true;
            fCheck2.Checked = true;
            fCheck3.Checked = true;
            fCheck4.Checked = true;
            fCheck5.Checked = true;
        }

        private void amssnCheckOffButt_Click(object sender, EventArgs e)
        {
            amssnCheck0.Checked = false;
            amssnCheck1.Checked = false;
        }

        private void amssnCheckOnButt_Click(object sender, EventArgs e)
        {
            amssnCheck0.Checked = true;
            amssnCheck1.Checked = true;
        }

        //Map Buttons
        private void resMaps_Click(object sender, EventArgs e)
        {
            Tabs.Enabled = false;
            rMaps.Enabled = false;
            resMaps.Enabled = false;
            if (File.Exists(Globals.mapFolder + "\\city0.sc2") && File.Exists(Globals.smkFolder + "\\city0_b.smk"))
            {
                bool cont = true;
                ProgressPop pop = new ProgressPop(0);
                pop.Location = this.Location;
                pop.Text = "Resetting Career Cities";
                pop.path1 = Globals.exeFolder;
                pop.path2 = Globals.mapFolder;
                pop.path3 = Globals.smkFolder;
                Shell32.Shell objShell = new Shell32.Shell();
                pop.ShowDialog();
                using (FileStream st = File.OpenRead(Globals.exeFolder + "\\CF.zip"))
                {
                    System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create();
                    if (!bTS(hash.ComputeHash(st)).Contains("2d48db91726eee77205f5e59de1d17f4751e4b2a8d053cc67aece6655ef8d83f"))
                        cont = false;
                    hash.Dispose();
                    st.Close();
                }
                if (cont)
                {
                    pop = new ProgressPop(1);
                    pop.Location = this.Location;
                    pop.Text = "Resetting Career Cities";
                    pop.path1 = Globals.exeFolder;
                    pop.path2 = Globals.mapFolder;
                    pop.path3 = Globals.smkFolder;
                    pop.destinationFolder = objShell.NameSpace(Globals.exeFolder);
                    pop.sourceFile = objShell.NameSpace(Globals.exeFolder + "\\CF.zip");
                    pop.ShowDialog();
                }
                else
                {
                    MessageBox.Show("City file corrupted or failed to download.");
                    pop.Dispose();
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("This must run in SimCopter's tweak folder.");
            Tabs.Enabled = true;
            rMaps.Enabled = true;
            resMaps.Enabled = true;
        }

        private void rMaps_Click(object sender, EventArgs e)
        {
            int[] maps = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            int x = 0;
            //Choose which maps go where.
            while (x < 30)
            {
                int temp = Globals.randMod.Next(0, 30);
                Console.WriteLine(temp.ToString());
                if (!(Array.Exists(maps, element => element == temp)))
                {
                    maps[x] = temp;
                    x++;
                }
            }
            //Assign the maps to their new spots +30, to avoid overwriting.
            for (int y = 0; y < 30; y++)
            {
                File.Move(Globals.mapFolder + "\\city" + maps[y].ToString() + ".sc2", Globals.mapFolder + "\\city" + (y + 30).ToString() + ".sc2");
                File.Move(Globals.smkFolder + "\\city" + maps[y].ToString() + "_b.smk", Globals.smkFolder + "\\city" + (y + 30).ToString() + "_b.smk");
                File.Move(Globals.smkFolder + "\\city" + maps[y].ToString() + "_s.smk", Globals.smkFolder + "\\city" + (y + 30).ToString() + "_s.smk");
            }
            //Move the maps to their real spots.
            for (int y = 30; y < 60; y++)
            {
                File.Move(Globals.mapFolder + "\\city" + y.ToString() + ".sc2", Globals.mapFolder + "\\city" + (y - 30).ToString() + ".sc2");
                File.Move(Globals.smkFolder + "\\city" + y.ToString() + "_b.smk", Globals.smkFolder + "\\city" + (y - 30).ToString() + "_b.smk");
                File.Move(Globals.smkFolder + "\\city" + y.ToString() + "_s.smk", Globals.smkFolder + "\\city" + (y - 30).ToString() + "_s.smk");
            }
            MessageBox.Show("Maps Shuffled.");
        }
    }
}

