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

        private void generateButt_Click(object sender, EventArgs e)
        {
            string exeFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            int[,] cNums = new int[30, 11];
            int[,] hNums = new int[12, 14];
         
            //Campaign File
            if (cOff.Checked == false)
            {
                if (cFair.Checked)
                {
                    cNums = fairCareerNums();
                }
                else if (cChaos.Checked)
                {
                    cNums = chaosCareerNums();
                }

                using (StreamWriter sw = File.CreateText(exeFolder + "\\career.twk"))
                {
                    for (int x = 0; x < 30; x++)
                    {
                        //Header
                        sw.WriteLine("%%%%%%%%%%%%%%% City" + x.ToString());
                        sw.WriteLine("[City" + x.ToString() + "]");
                        sw.WriteLine("NumCtrl = 11");
                        sw.WriteLine(" ");//Difficulty
                        sw.WriteLine("Ctrl0_Label=Difficulty (0-3)");
                        sw.WriteLine("Ctrl0_Type=Slider,0,3");
                        sw.WriteLine("Ctrl0_Value = " + cNums[x, 0].ToString());
                        sw.WriteLine("Ctrl0_DataType=int");
                        sw.WriteLine(" ");//Fire
                        sw.WriteLine("Ctrl1_Label = Fire(weight)");
                        sw.WriteLine("Ctrl1_Type = Slider,0,99");
                        sw.WriteLine("Ctrl1_Value = " + cNums[x, 1].ToString());
                        sw.WriteLine("Ctrl1_DataType = int");
                        sw.WriteLine(" ");//Crime
                        sw.WriteLine("Ctrl2_Label = Crime(weight)");
                        sw.WriteLine("Ctrl2_Type = Slider,0,99");
                        sw.WriteLine("Ctrl2_Value = " + cNums[x, 2].ToString());
                        sw.WriteLine("Ctrl2_DataType = int");
                        sw.WriteLine(" ");//Rescue
                        sw.WriteLine("Ctrl3_Label = Rescue(weight)");
                        sw.WriteLine("Ctrl3_Type = Slider,0,99");
                        sw.WriteLine("Ctrl3_Value = " + cNums[x, 3].ToString());
                        sw.WriteLine("Ctrl3_DataType = int");
                        sw.WriteLine(" ");//Riot
                        sw.WriteLine("Ctrl4_Label = Riot(weight)");
                        sw.WriteLine("Ctrl4_Type = Slider,0,99");
                        sw.WriteLine("Ctrl4_Value = " + cNums[x, 4].ToString());
                        sw.WriteLine("Ctrl4_DataType = int");
                        sw.WriteLine(" ");//Traffic
                        sw.WriteLine("Ctrl5_Label = Traffic(weight)");
                        sw.WriteLine("Ctrl5_Type = Slider,0,99");
                        sw.WriteLine("Ctrl5_Value = " + cNums[x, 5].ToString());
                        sw.WriteLine("Ctrl5_DataType = int");
                        sw.WriteLine(" ");//MedEvac
                        sw.WriteLine("Ctrl6_Label = MedEvac(weight)");
                        sw.WriteLine("Ctrl6_Type = Slider,0,99");
                        sw.WriteLine("Ctrl6_Value = " + cNums[x, 6].ToString());
                        sw.WriteLine("Ctrl6_DataType = int");
                        sw.WriteLine(" ");//Transport
                        sw.WriteLine("Ctrl7_Label = Transport(weight)");
                        sw.WriteLine("Ctrl7_Type = Slider,0,99");
                        sw.WriteLine("Ctrl7_Value = " + cNums[x, 7].ToString());
                        sw.WriteLine("Ctrl7_DataType = int");
                        sw.WriteLine(" ");//Day and Night
                        sw.WriteLine("Ctrl8_Label = Day or Night");
                        sw.WriteLine("Ctrl8_Type = Slider,0,1");
                        sw.WriteLine("Ctrl8_Value = " + cNums[x, 8].ToString());
                        sw.WriteLine("Ctrl8_DataType = int");
                        sw.WriteLine(" ");//Points
                        sw.WriteLine("Ctrl9_Label = Points Needed");
                        sw.WriteLine("Ctrl9_Type = Slider,1,5000");
                        sw.WriteLine("Ctrl9_Value = " + cNums[x, 9].ToString());
                        sw.WriteLine("Ctrl9_DataType = int");
                        sw.WriteLine(" ");//Cash
                        sw.WriteLine("Ctrl10_Label =$ Earned");
                        sw.WriteLine("Ctrl10_Type = Slider,1,2000");
                        sw.WriteLine("Ctrl10_Value = " + cNums[x, 10]);
                        sw.WriteLine("Ctrl10_DataType = int");
                        sw.WriteLine(" ");//Next, End
                    }
                    sw.Close();
                }

                //Heli File
                if (hOff.Checked == false)
                {
                    if (hFair.Checked)
                    {
                        hNums = fairHeliNums();
                    }
                    else if (hChaos.Checked)
                    {
                        hNums = chaosHeliNums();
                    }
                }
            }
        }

        private int[,] fairCareerNums()
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
                        nums[x, y] = randMod.Next(1,5001);
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

        private int[,] fairHeliNums()
        {
            int[,] nums = new int[12, 14];
            return nums;
        }

        private int[,] chaosHeliNums()
        {
            int[,] nums = new int[12, 14];
            return nums;
        }
    }
}
