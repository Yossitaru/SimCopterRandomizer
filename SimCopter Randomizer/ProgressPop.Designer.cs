namespace SimCopter_Randomizer
{
    partial class ProgressPop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progProgBar = new System.Windows.Forms.ProgressBar();
            this.progText = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progProgBar
            // 
            this.progProgBar.Location = new System.Drawing.Point(1, 38);
            this.progProgBar.Name = "progProgBar";
            this.progProgBar.Size = new System.Drawing.Size(412, 23);
            this.progProgBar.TabIndex = 0;
            // 
            // progText
            // 
            this.progText.Location = new System.Drawing.Point(49, 12);
            this.progText.Name = "progText";
            this.progText.ReadOnly = true;
            this.progText.Size = new System.Drawing.Size(304, 20);
            this.progText.TabIndex = 1;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            // 
            // ProgressPop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 72);
            this.ControlBox = false;
            this.Controls.Add(this.progText);
            this.Controls.Add(this.progProgBar);
            this.Name = "ProgressPop";
            this.Text = "ProgressPop";
            this.Load += new System.EventHandler(this.ProgressPop_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progProgBar;
        private System.Windows.Forms.TextBox progText;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}