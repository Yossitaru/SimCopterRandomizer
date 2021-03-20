namespace SimCopter_Randomizer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cPanel = new System.Windows.Forms.Panel();
            this.cChaos = new System.Windows.Forms.RadioButton();
            this.cFair = new System.Windows.Forms.RadioButton();
            this.cOff = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.hPanel = new System.Windows.Forms.Panel();
            this.hChaos = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.hFair = new System.Windows.Forms.RadioButton();
            this.hOff = new System.Windows.Forms.RadioButton();
            this.generateButt = new System.Windows.Forms.Button();
            this.cPanel.SuspendLayout();
            this.hPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cPanel
            // 
            this.cPanel.Controls.Add(this.cChaos);
            this.cPanel.Controls.Add(this.cFair);
            this.cPanel.Controls.Add(this.cOff);
            this.cPanel.Controls.Add(this.label1);
            this.cPanel.Location = new System.Drawing.Point(12, 12);
            this.cPanel.Name = "cPanel";
            this.cPanel.Size = new System.Drawing.Size(340, 196);
            this.cPanel.TabIndex = 0;
            // 
            // cChaos
            // 
            this.cChaos.AutoSize = true;
            this.cChaos.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cChaos.Location = new System.Drawing.Point(58, 154);
            this.cChaos.Name = "cChaos";
            this.cChaos.Size = new System.Drawing.Size(93, 28);
            this.cChaos.TabIndex = 3;
            this.cChaos.Text = "Chaos";
            this.cChaos.UseVisualStyleBackColor = true;
            // 
            // cFair
            // 
            this.cFair.AutoSize = true;
            this.cFair.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cFair.Location = new System.Drawing.Point(58, 107);
            this.cFair.Name = "cFair";
            this.cFair.Size = new System.Drawing.Size(80, 28);
            this.cFair.TabIndex = 2;
            this.cFair.Text = "Fair";
            this.cFair.UseVisualStyleBackColor = true;
            // 
            // cOff
            // 
            this.cOff.AutoSize = true;
            this.cOff.Checked = true;
            this.cOff.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cOff.Location = new System.Drawing.Point(58, 60);
            this.cOff.Name = "cOff";
            this.cOff.Size = new System.Drawing.Size(223, 28);
            this.cOff.TabIndex = 1;
            this.cOff.TabStop = true;
            this.cOff.Text = "Do Not Generate";
            this.cOff.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Campaign Randomizer";
            // 
            // hPanel
            // 
            this.hPanel.Controls.Add(this.hChaos);
            this.hPanel.Controls.Add(this.label2);
            this.hPanel.Controls.Add(this.hFair);
            this.hPanel.Controls.Add(this.hOff);
            this.hPanel.Location = new System.Drawing.Point(371, 12);
            this.hPanel.Name = "hPanel";
            this.hPanel.Size = new System.Drawing.Size(340, 196);
            this.hPanel.TabIndex = 1;
            // 
            // hChaos
            // 
            this.hChaos.AutoSize = true;
            this.hChaos.Enabled = false;
            this.hChaos.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hChaos.Location = new System.Drawing.Point(72, 154);
            this.hChaos.Name = "hChaos";
            this.hChaos.Size = new System.Drawing.Size(93, 28);
            this.hChaos.TabIndex = 6;
            this.hChaos.Text = "Chaos";
            this.hChaos.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(327, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Helicopter Randomizer";
            // 
            // hFair
            // 
            this.hFair.AutoSize = true;
            this.hFair.Enabled = false;
            this.hFair.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hFair.Location = new System.Drawing.Point(72, 107);
            this.hFair.Name = "hFair";
            this.hFair.Size = new System.Drawing.Size(80, 28);
            this.hFair.TabIndex = 5;
            this.hFair.Text = "Fair";
            this.hFair.UseVisualStyleBackColor = true;
            // 
            // hOff
            // 
            this.hOff.AutoSize = true;
            this.hOff.Checked = true;
            this.hOff.Enabled = false;
            this.hOff.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hOff.Location = new System.Drawing.Point(72, 60);
            this.hOff.Name = "hOff";
            this.hOff.Size = new System.Drawing.Size(223, 28);
            this.hOff.TabIndex = 4;
            this.hOff.TabStop = true;
            this.hOff.Text = "Do Not Generate";
            this.hOff.UseVisualStyleBackColor = true;
            // 
            // generateButt
            // 
            this.generateButt.Font = new System.Drawing.Font("NSimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generateButt.Location = new System.Drawing.Point(12, 225);
            this.generateButt.Name = "generateButt";
            this.generateButt.Size = new System.Drawing.Size(699, 69);
            this.generateButt.TabIndex = 2;
            this.generateButt.Text = "Make Files";
            this.generateButt.UseVisualStyleBackColor = true;
            this.generateButt.Click += new System.EventHandler(this.generateButt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 306);
            this.Controls.Add(this.generateButt);
            this.Controls.Add(this.hPanel);
            this.Controls.Add(this.cPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SimCopter Tweak File Randomizer";
            this.cPanel.ResumeLayout(false);
            this.cPanel.PerformLayout();
            this.hPanel.ResumeLayout(false);
            this.hPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel cPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel hPanel;
        private System.Windows.Forms.RadioButton cChaos;
        private System.Windows.Forms.RadioButton cFair;
        private System.Windows.Forms.RadioButton cOff;
        private System.Windows.Forms.RadioButton hChaos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton hFair;
        private System.Windows.Forms.RadioButton hOff;
        private System.Windows.Forms.Button generateButt;
    }
}

