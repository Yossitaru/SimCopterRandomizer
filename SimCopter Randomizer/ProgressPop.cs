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
    public partial class ProgressPop : Form
    {
        public string path1 { get; set; }
        public string path2 { get; set; }
        public string path3 { get; set; }
        public Shell32.Shell objShell;
        public Shell32.Folder destinationFolder;
        public Shell32.Folder sourceFile;

        public ProgressPop(int picker)
        {
            InitializeComponent();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = false;
            if (picker == 0)
                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_Download);
            else if (picker == 1)
                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_Move);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_Complete);
        }

        void ProgressPop_Load(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        void bgWorker_Download(object sender, DoWorkEventArgs e)
        {
            //Download default files.
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                BeginInvoke((MethodInvoker)delegate { progText.Text = "Downloading city files."; });
                try
                {
                    wc.DownloadFile(new Uri("https://www.googleapis.com/drive/v3/files/1sSJccqbgkz5blTaodZcsxTQDJxKctq3P?alt=media&key=" + Environment.GetEnvironmentVariable("Drive API").ToString()), "CF.zip");
                }
                catch (Exception e2)
                {
                    if (e2 != null)
                    {
                        System.Windows.Forms.MessageBox.Show("Could not download default city files.");
                    }
                }
            }
        }

        void bgWorker_Move(object sender, DoWorkEventArgs e)
        {
            //Get rid of old files.
            BeginInvoke((MethodInvoker)delegate { progText.Text = "Deleting Old City Files"; });
            BeginInvoke((MethodInvoker)delegate { progProgBar.Value = 10; });
                            for (int x = 0; x < 30; x++)
                            {
                                System.IO.File.Delete(path2 + "\\city" + x.ToString() + ".sc2");
                                System.IO.File.Delete(path3 + "\\city" + x.ToString() + "_b.smk");
                                System.IO.File.Delete(path3 + "\\city" + x.ToString() + "_s.smk");
                                BeginInvoke((MethodInvoker)delegate { progProgBar.Value += 1; });
                            }
            BeginInvoke((MethodInvoker)delegate { progText.Text = "Unzipping cities"; });
            System.Threading.Thread.Sleep(1000);
            //Unzip default files.
            foreach (var file in sourceFile.Items())
            {
                destinationFolder.CopyHere(file);
            }
            BeginInvoke((MethodInvoker)delegate { progProgBar.Value += 30; });
            //Delete zip.
            System.IO.File.Delete("CF.zip");
            //Move default files to proper places.
            BeginInvoke((MethodInvoker)delegate { progText.Text = "Moving cities"; });
            for (int x = 0; x < 30; x++)
            {
                System.IO.File.Move("city" + x.ToString() + ".sc2", path2 + "\\city" + x.ToString() + ".sc2");
                System.IO.File.Move("city" + x.ToString() + "_b.smk", path3 + "\\city" + x.ToString() + "_b.smk");
                System.IO.File.Move("city" + x.ToString() + "_s.smk", path3 + "\\city" + x.ToString() + "_s.smk");
                BeginInvoke((MethodInvoker)delegate { progProgBar.Value += 1; });
            }
            //Message of Confirmation
            System.Windows.Forms.MessageBox.Show("Career Maps reset.");
        }
      
        void bgWorker_ProgChanged(object sender, ProgressChangedEventArgs e)
        {
            //make sure the new value is valid for the progress bar and update it
            if (e.ProgressPercentage >= progProgBar.Minimum &&
                e.ProgressPercentage <= progProgBar.Maximum)
            {
                progProgBar.Value = e.ProgressPercentage;
            }
        }

        void bgWorker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
