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
            this.cEasy = new System.Windows.Forms.RadioButton();
            this.cChaos = new System.Windows.Forms.RadioButton();
            this.cBalance = new System.Windows.Forms.RadioButton();
            this.cHard = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.hPanel = new System.Windows.Forms.Panel();
            this.mEasy = new System.Windows.Forms.RadioButton();
            this.mChaos = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.mBalance = new System.Windows.Forms.RadioButton();
            this.mHard = new System.Windows.Forms.RadioButton();
            this.carrButt = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.Career = new System.Windows.Forms.TabPage();
            this.Missions = new System.Windows.Forms.TabPage();
            this.mssnButt = new System.Windows.Forms.Button();
            this.Helicopters = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hEasy = new System.Windows.Forms.RadioButton();
            this.hChaos = new System.Windows.Forms.RadioButton();
            this.hBalance = new System.Windows.Forms.RadioButton();
            this.hHard = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.heliButt = new System.Windows.Forms.Button();
            this.Fire = new System.Windows.Forms.TabPage();
            this.fireButt = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.fChaos = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.AutoMissions = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.amssnChaos = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.amssnButt = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.resetButt = new System.Windows.Forms.Button();
            this.cPanel.SuspendLayout();
            this.hPanel.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.Career.SuspendLayout();
            this.Missions.SuspendLayout();
            this.Helicopters.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Fire.SuspendLayout();
            this.panel2.SuspendLayout();
            this.AutoMissions.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cPanel
            // 
            this.cPanel.Controls.Add(this.cEasy);
            this.cPanel.Controls.Add(this.cChaos);
            this.cPanel.Controls.Add(this.cBalance);
            this.cPanel.Controls.Add(this.cHard);
            this.cPanel.Controls.Add(this.label1);
            this.cPanel.Location = new System.Drawing.Point(8, 6);
            this.cPanel.Name = "cPanel";
            this.cPanel.Size = new System.Drawing.Size(340, 196);
            this.cPanel.TabIndex = 0;
            // 
            // cEasy
            // 
            this.cEasy.AutoSize = true;
            this.cEasy.Enabled = false;
            this.cEasy.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cEasy.Location = new System.Drawing.Point(58, 65);
            this.cEasy.Name = "cEasy";
            this.cEasy.Size = new System.Drawing.Size(106, 28);
            this.cEasy.TabIndex = 4;
            this.cEasy.Text = "Easier";
            this.cEasy.UseVisualStyleBackColor = true;
            this.cEasy.Visible = false;
            // 
            // cChaos
            // 
            this.cChaos.AutoSize = true;
            this.cChaos.Checked = true;
            this.cChaos.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cChaos.Location = new System.Drawing.Point(58, 154);
            this.cChaos.Name = "cChaos";
            this.cChaos.Size = new System.Drawing.Size(93, 28);
            this.cChaos.TabIndex = 3;
            this.cChaos.TabStop = true;
            this.cChaos.Text = "Chaos";
            this.cChaos.UseVisualStyleBackColor = true;
            // 
            // cBalance
            // 
            this.cBalance.AutoSize = true;
            this.cBalance.Enabled = false;
            this.cBalance.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBalance.Location = new System.Drawing.Point(58, 125);
            this.cBalance.Name = "cBalance";
            this.cBalance.Size = new System.Drawing.Size(132, 28);
            this.cBalance.TabIndex = 2;
            this.cBalance.Text = "Balanced";
            this.cBalance.UseVisualStyleBackColor = true;
            this.cBalance.Visible = false;
            // 
            // cHard
            // 
            this.cHard.AutoSize = true;
            this.cHard.Enabled = false;
            this.cHard.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cHard.Location = new System.Drawing.Point(58, 95);
            this.cHard.Name = "cHard";
            this.cHard.Size = new System.Drawing.Size(106, 28);
            this.cHard.TabIndex = 1;
            this.cHard.Text = "Harder";
            this.cHard.UseVisualStyleBackColor = true;
            this.cHard.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Career Randomizer";
            // 
            // hPanel
            // 
            this.hPanel.Controls.Add(this.mEasy);
            this.hPanel.Controls.Add(this.mChaos);
            this.hPanel.Controls.Add(this.label2);
            this.hPanel.Controls.Add(this.mBalance);
            this.hPanel.Controls.Add(this.mHard);
            this.hPanel.Location = new System.Drawing.Point(8, 6);
            this.hPanel.Name = "hPanel";
            this.hPanel.Size = new System.Drawing.Size(340, 196);
            this.hPanel.TabIndex = 1;
            // 
            // mEasy
            // 
            this.mEasy.AutoSize = true;
            this.mEasy.Enabled = false;
            this.mEasy.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mEasy.Location = new System.Drawing.Point(72, 53);
            this.mEasy.Name = "mEasy";
            this.mEasy.Size = new System.Drawing.Size(210, 28);
            this.mEasy.TabIndex = 7;
            this.mEasy.Text = "More Rewarding";
            this.mEasy.UseVisualStyleBackColor = true;
            this.mEasy.Visible = false;
            // 
            // mChaos
            // 
            this.mChaos.AutoSize = true;
            this.mChaos.Checked = true;
            this.mChaos.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mChaos.Location = new System.Drawing.Point(72, 154);
            this.mChaos.Name = "mChaos";
            this.mChaos.Size = new System.Drawing.Size(93, 28);
            this.mChaos.TabIndex = 6;
            this.mChaos.TabStop = true;
            this.mChaos.Text = "Chaos";
            this.mChaos.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(282, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mission Randomizer";
            // 
            // mBalance
            // 
            this.mBalance.AutoSize = true;
            this.mBalance.Enabled = false;
            this.mBalance.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mBalance.Location = new System.Drawing.Point(72, 122);
            this.mBalance.Name = "mBalance";
            this.mBalance.Size = new System.Drawing.Size(132, 28);
            this.mBalance.TabIndex = 5;
            this.mBalance.Text = "Balanced";
            this.mBalance.UseVisualStyleBackColor = true;
            this.mBalance.Visible = false;
            // 
            // mHard
            // 
            this.mHard.AutoSize = true;
            this.mHard.Enabled = false;
            this.mHard.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mHard.Location = new System.Drawing.Point(72, 87);
            this.mHard.Name = "mHard";
            this.mHard.Size = new System.Drawing.Size(210, 28);
            this.mHard.TabIndex = 4;
            this.mHard.Text = "Less Rewarding";
            this.mHard.UseVisualStyleBackColor = true;
            this.mHard.Visible = false;
            // 
            // carrButt
            // 
            this.carrButt.Font = new System.Drawing.Font("NSimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carrButt.Location = new System.Drawing.Point(8, 208);
            this.carrButt.Name = "carrButt";
            this.carrButt.Size = new System.Drawing.Size(340, 69);
            this.carrButt.TabIndex = 2;
            this.carrButt.Text = "Randomize!";
            this.carrButt.UseVisualStyleBackColor = true;
            this.carrButt.Click += new System.EventHandler(this.carrButt_Click);
            // 
            // Tabs
            // 
            this.Tabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.Tabs.Controls.Add(this.Career);
            this.Tabs.Controls.Add(this.Missions);
            this.Tabs.Controls.Add(this.Helicopters);
            this.Tabs.Controls.Add(this.Fire);
            this.Tabs.Controls.Add(this.AutoMissions);
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.HotTrack = true;
            this.Tabs.Location = new System.Drawing.Point(12, 12);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(365, 310);
            this.Tabs.TabIndex = 3;
            // 
            // Career
            // 
            this.Career.Controls.Add(this.cPanel);
            this.Career.Controls.Add(this.carrButt);
            this.Career.Location = new System.Drawing.Point(4, 25);
            this.Career.Name = "Career";
            this.Career.Padding = new System.Windows.Forms.Padding(3);
            this.Career.Size = new System.Drawing.Size(357, 281);
            this.Career.TabIndex = 0;
            this.Career.Text = "Career";
            this.Career.UseVisualStyleBackColor = true;
            // 
            // Missions
            // 
            this.Missions.Controls.Add(this.mssnButt);
            this.Missions.Controls.Add(this.hPanel);
            this.Missions.Location = new System.Drawing.Point(4, 25);
            this.Missions.Name = "Missions";
            this.Missions.Padding = new System.Windows.Forms.Padding(3);
            this.Missions.Size = new System.Drawing.Size(357, 281);
            this.Missions.TabIndex = 1;
            this.Missions.Text = "Missions";
            this.Missions.UseVisualStyleBackColor = true;
            // 
            // mssnButt
            // 
            this.mssnButt.Font = new System.Drawing.Font("NSimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mssnButt.Location = new System.Drawing.Point(8, 208);
            this.mssnButt.Name = "mssnButt";
            this.mssnButt.Size = new System.Drawing.Size(340, 69);
            this.mssnButt.TabIndex = 3;
            this.mssnButt.Text = "Randomize!";
            this.mssnButt.UseVisualStyleBackColor = true;
            this.mssnButt.Click += new System.EventHandler(this.mssnButt_Click);
            // 
            // Helicopters
            // 
            this.Helicopters.Controls.Add(this.panel1);
            this.Helicopters.Controls.Add(this.heliButt);
            this.Helicopters.Location = new System.Drawing.Point(4, 25);
            this.Helicopters.Name = "Helicopters";
            this.Helicopters.Size = new System.Drawing.Size(357, 281);
            this.Helicopters.TabIndex = 2;
            this.Helicopters.Text = "Helicopters";
            this.Helicopters.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hEasy);
            this.panel1.Controls.Add(this.hChaos);
            this.panel1.Controls.Add(this.hBalance);
            this.panel1.Controls.Add(this.hHard);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 196);
            this.panel1.TabIndex = 4;
            // 
            // hEasy
            // 
            this.hEasy.AutoSize = true;
            this.hEasy.Enabled = false;
            this.hEasy.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hEasy.Location = new System.Drawing.Point(58, 52);
            this.hEasy.Name = "hEasy";
            this.hEasy.Size = new System.Drawing.Size(106, 28);
            this.hEasy.TabIndex = 4;
            this.hEasy.Text = "Better";
            this.hEasy.UseVisualStyleBackColor = true;
            this.hEasy.Visible = false;
            // 
            // hChaos
            // 
            this.hChaos.AutoSize = true;
            this.hChaos.Checked = true;
            this.hChaos.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hChaos.Location = new System.Drawing.Point(58, 154);
            this.hChaos.Name = "hChaos";
            this.hChaos.Size = new System.Drawing.Size(93, 28);
            this.hChaos.TabIndex = 3;
            this.hChaos.TabStop = true;
            this.hChaos.Text = "Chaos";
            this.hChaos.UseVisualStyleBackColor = true;
            // 
            // hBalance
            // 
            this.hBalance.AutoSize = true;
            this.hBalance.Enabled = false;
            this.hBalance.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hBalance.Location = new System.Drawing.Point(58, 120);
            this.hBalance.Name = "hBalance";
            this.hBalance.Size = new System.Drawing.Size(132, 28);
            this.hBalance.TabIndex = 2;
            this.hBalance.Text = "Balanced";
            this.hBalance.UseVisualStyleBackColor = true;
            this.hBalance.Visible = false;
            // 
            // hHard
            // 
            this.hHard.AutoSize = true;
            this.hHard.Enabled = false;
            this.hHard.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hHard.Location = new System.Drawing.Point(58, 86);
            this.hHard.Name = "hHard";
            this.hHard.Size = new System.Drawing.Size(93, 28);
            this.hHard.TabIndex = 1;
            this.hHard.Text = "Worse";
            this.hHard.UseVisualStyleBackColor = true;
            this.hHard.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(327, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "Helicopter Randomizer";
            // 
            // heliButt
            // 
            this.heliButt.Font = new System.Drawing.Font("NSimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heliButt.Location = new System.Drawing.Point(8, 208);
            this.heliButt.Name = "heliButt";
            this.heliButt.Size = new System.Drawing.Size(340, 69);
            this.heliButt.TabIndex = 3;
            this.heliButt.Text = "Randomize!";
            this.heliButt.UseVisualStyleBackColor = true;
            this.heliButt.Click += new System.EventHandler(this.heliButt_Click);
            // 
            // Fire
            // 
            this.Fire.Controls.Add(this.fireButt);
            this.Fire.Controls.Add(this.panel2);
            this.Fire.Location = new System.Drawing.Point(4, 25);
            this.Fire.Name = "Fire";
            this.Fire.Size = new System.Drawing.Size(357, 281);
            this.Fire.TabIndex = 3;
            this.Fire.Text = "Fire";
            this.Fire.UseVisualStyleBackColor = true;
            // 
            // fireButt
            // 
            this.fireButt.Font = new System.Drawing.Font("NSimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fireButt.Location = new System.Drawing.Point(9, 211);
            this.fireButt.Name = "fireButt";
            this.fireButt.Size = new System.Drawing.Size(340, 69);
            this.fireButt.TabIndex = 4;
            this.fireButt.Text = "Randomize!";
            this.fireButt.UseVisualStyleBackColor = true;
            this.fireButt.Click += new System.EventHandler(this.fireButt_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton10);
            this.panel2.Controls.Add(this.fChaos);
            this.panel2.Controls.Add(this.radioButton5);
            this.panel2.Controls.Add(this.radioButton6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(9, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 196);
            this.panel2.TabIndex = 1;
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Enabled = false;
            this.radioButton10.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton10.Location = new System.Drawing.Point(58, 64);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(106, 28);
            this.radioButton10.TabIndex = 4;
            this.radioButton10.Text = "Easier";
            this.radioButton10.UseVisualStyleBackColor = true;
            this.radioButton10.Visible = false;
            // 
            // fChaos
            // 
            this.fChaos.AutoSize = true;
            this.fChaos.Checked = true;
            this.fChaos.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fChaos.Location = new System.Drawing.Point(58, 154);
            this.fChaos.Name = "fChaos";
            this.fChaos.Size = new System.Drawing.Size(93, 28);
            this.fChaos.TabIndex = 3;
            this.fChaos.TabStop = true;
            this.fChaos.Text = "Chaos";
            this.fChaos.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Enabled = false;
            this.radioButton5.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton5.Location = new System.Drawing.Point(58, 126);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(132, 28);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.Text = "Balanced";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Visible = false;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Enabled = false;
            this.radioButton6.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton6.Location = new System.Drawing.Point(58, 98);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(106, 28);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.Text = "Harder";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(237, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "Fire Randomizer";
            // 
            // AutoMissions
            // 
            this.AutoMissions.Controls.Add(this.panel3);
            this.AutoMissions.Controls.Add(this.amssnButt);
            this.AutoMissions.Location = new System.Drawing.Point(4, 25);
            this.AutoMissions.Name = "AutoMissions";
            this.AutoMissions.Size = new System.Drawing.Size(357, 281);
            this.AutoMissions.TabIndex = 4;
            this.AutoMissions.Text = "AutoMissions";
            this.AutoMissions.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.amssnChaos);
            this.panel3.Controls.Add(this.radioButton8);
            this.panel3.Controls.Add(this.radioButton9);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(9, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(340, 196);
            this.panel3.TabIndex = 5;
            // 
            // amssnChaos
            // 
            this.amssnChaos.AutoSize = true;
            this.amssnChaos.Checked = true;
            this.amssnChaos.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amssnChaos.Location = new System.Drawing.Point(58, 154);
            this.amssnChaos.Name = "amssnChaos";
            this.amssnChaos.Size = new System.Drawing.Size(93, 28);
            this.amssnChaos.TabIndex = 3;
            this.amssnChaos.TabStop = true;
            this.amssnChaos.Text = "Chaos";
            this.amssnChaos.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Enabled = false;
            this.radioButton8.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton8.Location = new System.Drawing.Point(58, 123);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(80, 28);
            this.radioButton8.TabIndex = 2;
            this.radioButton8.Text = "Fair";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.radioButton8.Visible = false;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Enabled = false;
            this.radioButton9.Font = new System.Drawing.Font("NSimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton9.Location = new System.Drawing.Point(58, 90);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(223, 28);
            this.radioButton9.TabIndex = 1;
            this.radioButton9.Text = "Do Not Generate";
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(297, 27);
            this.label5.TabIndex = 0;
            this.label5.Text = "AMission Randomizer";
            // 
            // amssnButt
            // 
            this.amssnButt.Font = new System.Drawing.Font("NSimSun", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amssnButt.Location = new System.Drawing.Point(9, 210);
            this.amssnButt.Name = "amssnButt";
            this.amssnButt.Size = new System.Drawing.Size(340, 69);
            this.amssnButt.TabIndex = 4;
            this.amssnButt.Text = "Randomize!";
            this.amssnButt.UseVisualStyleBackColor = true;
            this.amssnButt.Click += new System.EventHandler(this.amssnButt_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.resetButt);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(357, 281);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Reset";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // resetButt
            // 
            this.resetButt.Font = new System.Drawing.Font("NSimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButt.Location = new System.Drawing.Point(10, 209);
            this.resetButt.Name = "resetButt";
            this.resetButt.Size = new System.Drawing.Size(340, 69);
            this.resetButt.TabIndex = 3;
            this.resetButt.Text = "Reset Files";
            this.resetButt.UseVisualStyleBackColor = true;
            this.resetButt.Click += new System.EventHandler(this.resetButt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 332);
            this.Controls.Add(this.Tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SimCopter Tweak File Randomizer";
            this.cPanel.ResumeLayout(false);
            this.cPanel.PerformLayout();
            this.hPanel.ResumeLayout(false);
            this.hPanel.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.Career.ResumeLayout(false);
            this.Missions.ResumeLayout(false);
            this.Helicopters.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Fire.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.AutoMissions.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel cPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel hPanel;
        private System.Windows.Forms.RadioButton cChaos;
        private System.Windows.Forms.RadioButton cBalance;
        private System.Windows.Forms.RadioButton cHard;
        private System.Windows.Forms.RadioButton mChaos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton mBalance;
        private System.Windows.Forms.RadioButton mHard;
        private System.Windows.Forms.Button carrButt;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage Career;
        private System.Windows.Forms.TabPage Missions;
        private System.Windows.Forms.Button mssnButt;
        private System.Windows.Forms.TabPage Helicopters;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton hChaos;
        private System.Windows.Forms.RadioButton hBalance;
        private System.Windows.Forms.RadioButton hHard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button heliButt;
        private System.Windows.Forms.TabPage Fire;
        private System.Windows.Forms.Button fireButt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton fChaos;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage AutoMissions;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton amssnChaos;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button amssnButt;
        private System.Windows.Forms.RadioButton cEasy;
        private System.Windows.Forms.RadioButton mEasy;
        private System.Windows.Forms.RadioButton hEasy;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button resetButt;
    }
}

