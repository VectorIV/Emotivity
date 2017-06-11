namespace emotivityMain
{
    partial class emotivitySettingScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(emotivitySettingScreen));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.emotivityLogo = new System.Windows.Forms.PictureBox();
            this.settingPanel = new System.Windows.Forms.Panel();
            this.composerCheckBox = new System.Windows.Forms.CheckBox();
            this.composerIcon = new System.Windows.Forms.PictureBox();
            this.composerLabel = new System.Windows.Forms.Label();
            this.loopBox = new System.Windows.Forms.NumericUpDown();
            this.loopCheckBox = new System.Windows.Forms.CheckBox();
            this.loopIcon = new System.Windows.Forms.PictureBox();
            this.loopLabel = new System.Windows.Forms.Label();
            this.quickCheckBox = new System.Windows.Forms.CheckBox();
            this.quickIcon = new System.Windows.Forms.PictureBox();
            this.quickLabel = new System.Windows.Forms.Label();
            this.logPanelCheckBox = new System.Windows.Forms.CheckBox();
            this.logPanelIcon = new System.Windows.Forms.PictureBox();
            this.logPanelLabel = new System.Windows.Forms.Label();
            this.logExportCheckBox = new System.Windows.Forms.CheckBox();
            this.textExportCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.logExportBtn = new System.Windows.Forms.Button();
            this.logExportBox = new System.Windows.Forms.TextBox();
            this.textExportBtn = new System.Windows.Forms.Button();
            this.textExportBox = new System.Windows.Forms.TextBox();
            this.logExportIcon = new System.Windows.Forms.PictureBox();
            this.logExportLabel = new System.Windows.Forms.Label();
            this.textExportIcon = new System.Windows.Forms.PictureBox();
            this.textExportLabel = new System.Windows.Forms.Label();
            this.logFolderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.textFolderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.textSelectBtnTimer = new System.Windows.Forms.Timer(this.components);
            this.saveBtnTimer = new System.Windows.Forms.Timer(this.components);
            this.cancelBtnTimer = new System.Windows.Forms.Timer(this.components);
            this.logSelectBtnTimer = new System.Windows.Forms.Timer(this.components);
            this.defaultBtnTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotivityLogo)).BeginInit();
            this.settingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.composerIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quickIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logPanelIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logExportIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textExportIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::emotivityMain.Properties.Resources.setting_icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // emotivityLogo
            // 
            this.emotivityLogo.BackColor = System.Drawing.Color.Transparent;
            this.emotivityLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("emotivityLogo.BackgroundImage")));
            this.emotivityLogo.Location = new System.Drawing.Point(63, 20);
            this.emotivityLogo.Name = "emotivityLogo";
            this.emotivityLogo.Size = new System.Drawing.Size(146, 12);
            this.emotivityLogo.TabIndex = 28;
            this.emotivityLogo.TabStop = false;
            // 
            // settingPanel
            // 
            this.settingPanel.BackColor = System.Drawing.Color.Transparent;
            this.settingPanel.BackgroundImage = global::emotivityMain.Properties.Resources.groupBackdrop;
            this.settingPanel.Controls.Add(this.composerCheckBox);
            this.settingPanel.Controls.Add(this.composerIcon);
            this.settingPanel.Controls.Add(this.composerLabel);
            this.settingPanel.Controls.Add(this.loopBox);
            this.settingPanel.Controls.Add(this.loopCheckBox);
            this.settingPanel.Controls.Add(this.loopIcon);
            this.settingPanel.Controls.Add(this.loopLabel);
            this.settingPanel.Controls.Add(this.quickCheckBox);
            this.settingPanel.Controls.Add(this.quickIcon);
            this.settingPanel.Controls.Add(this.quickLabel);
            this.settingPanel.Controls.Add(this.logPanelCheckBox);
            this.settingPanel.Controls.Add(this.logPanelIcon);
            this.settingPanel.Controls.Add(this.logPanelLabel);
            this.settingPanel.Controls.Add(this.logExportCheckBox);
            this.settingPanel.Controls.Add(this.textExportCheckBox);
            this.settingPanel.Controls.Add(this.defaultBtn);
            this.settingPanel.Controls.Add(this.saveBtn);
            this.settingPanel.Controls.Add(this.cancelBtn);
            this.settingPanel.Controls.Add(this.logExportBtn);
            this.settingPanel.Controls.Add(this.logExportBox);
            this.settingPanel.Controls.Add(this.textExportBtn);
            this.settingPanel.Controls.Add(this.textExportBox);
            this.settingPanel.Controls.Add(this.logExportIcon);
            this.settingPanel.Controls.Add(this.logExportLabel);
            this.settingPanel.Controls.Add(this.textExportIcon);
            this.settingPanel.Controls.Add(this.textExportLabel);
            this.settingPanel.Location = new System.Drawing.Point(13, 56);
            this.settingPanel.Name = "settingPanel";
            this.settingPanel.Size = new System.Drawing.Size(511, 260);
            this.settingPanel.TabIndex = 30;
            // 
            // composerCheckBox
            // 
            this.composerCheckBox.AutoSize = true;
            this.composerCheckBox.Location = new System.Drawing.Point(372, 153);
            this.composerCheckBox.Name = "composerCheckBox";
            this.composerCheckBox.Size = new System.Drawing.Size(18, 17);
            this.composerCheckBox.TabIndex = 68;
            this.composerCheckBox.UseVisualStyleBackColor = true;
            this.composerCheckBox.CheckedChanged += new System.EventHandler(this.composerCheckBox_CheckedChanged);
            // 
            // composerIcon
            // 
            this.composerIcon.BackColor = System.Drawing.Color.Transparent;
            this.composerIcon.BackgroundImage = global::emotivityMain.Properties.Resources.compose;
            this.composerIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.composerIcon.Location = new System.Drawing.Point(396, 141);
            this.composerIcon.Name = "composerIcon";
            this.composerIcon.Size = new System.Drawing.Size(40, 40);
            this.composerIcon.TabIndex = 66;
            this.composerIcon.TabStop = false;
            // 
            // composerLabel
            // 
            this.composerLabel.AutoSize = true;
            this.composerLabel.BackColor = System.Drawing.Color.Transparent;
            this.composerLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.composerLabel.Location = new System.Drawing.Point(363, 184);
            this.composerLabel.Name = "composerLabel";
            this.composerLabel.Size = new System.Drawing.Size(101, 17);
            this.composerLabel.TabIndex = 67;
            this.composerLabel.Text = "Use Composer";
            this.composerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loopBox
            // 
            this.loopBox.BackColor = System.Drawing.Color.Silver;
            this.loopBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loopBox.Enabled = false;
            this.loopBox.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.loopBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.loopBox.Location = new System.Drawing.Point(268, 150);
            this.loopBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.loopBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.loopBox.Name = "loopBox";
            this.loopBox.Size = new System.Drawing.Size(73, 22);
            this.loopBox.TabIndex = 65;
            this.loopBox.TabStop = false;
            this.loopBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.loopBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.loopBox.ValueChanged += new System.EventHandler(this.loopBox_ValueChanged);
            this.loopBox.EnabledChanged += new System.EventHandler(this.loopBox_EnabledChanged);
            // 
            // loopCheckBox
            // 
            this.loopCheckBox.AutoSize = true;
            this.loopCheckBox.Location = new System.Drawing.Point(198, 153);
            this.loopCheckBox.Name = "loopCheckBox";
            this.loopCheckBox.Size = new System.Drawing.Size(18, 17);
            this.loopCheckBox.TabIndex = 64;
            this.loopCheckBox.UseVisualStyleBackColor = true;
            this.loopCheckBox.CheckedChanged += new System.EventHandler(this.loopCheckBox_CheckedChanged);
            // 
            // loopIcon
            // 
            this.loopIcon.BackColor = System.Drawing.Color.Transparent;
            this.loopIcon.BackgroundImage = global::emotivityMain.Properties.Resources.code_loop;
            this.loopIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loopIcon.Location = new System.Drawing.Point(222, 141);
            this.loopIcon.Name = "loopIcon";
            this.loopIcon.Size = new System.Drawing.Size(40, 40);
            this.loopIcon.TabIndex = 62;
            this.loopIcon.TabStop = false;
            // 
            // loopLabel
            // 
            this.loopLabel.AutoSize = true;
            this.loopLabel.BackColor = System.Drawing.Color.Transparent;
            this.loopLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.loopLabel.Location = new System.Drawing.Point(201, 184);
            this.loopLabel.Name = "loopLabel";
            this.loopLabel.Size = new System.Drawing.Size(89, 17);
            this.loopLabel.TabIndex = 63;
            this.loopLabel.Text = "Invoke Delay";
            this.loopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // quickCheckBox
            // 
            this.quickCheckBox.AutoSize = true;
            this.quickCheckBox.Location = new System.Drawing.Point(108, 153);
            this.quickCheckBox.Name = "quickCheckBox";
            this.quickCheckBox.Size = new System.Drawing.Size(18, 17);
            this.quickCheckBox.TabIndex = 61;
            this.quickCheckBox.UseVisualStyleBackColor = true;
            this.quickCheckBox.CheckedChanged += new System.EventHandler(this.quickCheckBox_CheckedChanged);
            // 
            // quickIcon
            // 
            this.quickIcon.BackColor = System.Drawing.Color.Transparent;
            this.quickIcon.BackgroundImage = global::emotivityMain.Properties.Resources.quick_set;
            this.quickIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.quickIcon.Location = new System.Drawing.Point(132, 141);
            this.quickIcon.Name = "quickIcon";
            this.quickIcon.Size = new System.Drawing.Size(40, 40);
            this.quickIcon.TabIndex = 59;
            this.quickIcon.TabStop = false;
            // 
            // quickLabel
            // 
            this.quickLabel.AutoSize = true;
            this.quickLabel.BackColor = System.Drawing.Color.Transparent;
            this.quickLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.quickLabel.Location = new System.Drawing.Point(111, 184);
            this.quickLabel.Name = "quickLabel";
            this.quickLabel.Size = new System.Drawing.Size(87, 17);
            this.quickLabel.TabIndex = 60;
            this.quickLabel.Text = "Quick Action";
            this.quickLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logPanelCheckBox
            // 
            this.logPanelCheckBox.AutoSize = true;
            this.logPanelCheckBox.Location = new System.Drawing.Point(16, 153);
            this.logPanelCheckBox.Name = "logPanelCheckBox";
            this.logPanelCheckBox.Size = new System.Drawing.Size(18, 17);
            this.logPanelCheckBox.TabIndex = 58;
            this.logPanelCheckBox.UseVisualStyleBackColor = true;
            this.logPanelCheckBox.CheckedChanged += new System.EventHandler(this.logPanelCheckBox_CheckedChanged);
            // 
            // logPanelIcon
            // 
            this.logPanelIcon.BackColor = System.Drawing.Color.Transparent;
            this.logPanelIcon.BackgroundImage = global::emotivityMain.Properties.Resources.log_tab;
            this.logPanelIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logPanelIcon.Location = new System.Drawing.Point(40, 141);
            this.logPanelIcon.Name = "logPanelIcon";
            this.logPanelIcon.Size = new System.Drawing.Size(40, 40);
            this.logPanelIcon.TabIndex = 56;
            this.logPanelIcon.TabStop = false;
            // 
            // logPanelLabel
            // 
            this.logPanelLabel.AutoSize = true;
            this.logPanelLabel.BackColor = System.Drawing.Color.Transparent;
            this.logPanelLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.logPanelLabel.Location = new System.Drawing.Point(25, 184);
            this.logPanelLabel.Name = "logPanelLabel";
            this.logPanelLabel.Size = new System.Drawing.Size(72, 17);
            this.logPanelLabel.TabIndex = 57;
            this.logPanelLabel.Text = "Log Panel";
            this.logPanelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logExportCheckBox
            // 
            this.logExportCheckBox.AutoSize = true;
            this.logExportCheckBox.Location = new System.Drawing.Point(16, 90);
            this.logExportCheckBox.Name = "logExportCheckBox";
            this.logExportCheckBox.Size = new System.Drawing.Size(18, 17);
            this.logExportCheckBox.TabIndex = 55;
            this.logExportCheckBox.UseVisualStyleBackColor = true;
            this.logExportCheckBox.CheckedChanged += new System.EventHandler(this.logExportCheckBox_CheckedChanged);
            // 
            // textExportCheckBox
            // 
            this.textExportCheckBox.AutoSize = true;
            this.textExportCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.textExportCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textExportCheckBox.Location = new System.Drawing.Point(16, 27);
            this.textExportCheckBox.Name = "textExportCheckBox";
            this.textExportCheckBox.Size = new System.Drawing.Size(18, 17);
            this.textExportCheckBox.TabIndex = 54;
            this.textExportCheckBox.UseVisualStyleBackColor = false;
            this.textExportCheckBox.CheckedChanged += new System.EventHandler(this.textExportCheckBox_CheckedChanged);
            // 
            // defaultBtn
            // 
            this.defaultBtn.BackColor = System.Drawing.Color.Transparent;
            this.defaultBtn.BackgroundImage = global::emotivityMain.Properties.Resources.defaultButton;
            this.defaultBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.defaultBtn.FlatAppearance.BorderSize = 0;
            this.defaultBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.defaultBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.defaultBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.defaultBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.defaultBtn.Location = new System.Drawing.Point(241, 221);
            this.defaultBtn.Name = "defaultBtn";
            this.defaultBtn.Size = new System.Drawing.Size(84, 36);
            this.defaultBtn.TabIndex = 53;
            this.defaultBtn.TabStop = false;
            this.defaultBtn.UseVisualStyleBackColor = false;
            this.defaultBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.defaultBtn_MouseDown);
            this.defaultBtn.MouseEnter += new System.EventHandler(this.defaultBtn_MouseEnter);
            this.defaultBtn.MouseLeave += new System.EventHandler(this.defaultBtn_MouseLeave);
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.Transparent;
            this.saveBtn.BackgroundImage = global::emotivityMain.Properties.Resources.saveButton_dis;
            this.saveBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveBtn.Enabled = false;
            this.saveBtn.FlatAppearance.BorderSize = 0;
            this.saveBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.saveBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.saveBtn.Location = new System.Drawing.Point(344, 221);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(74, 36);
            this.saveBtn.TabIndex = 52;
            this.saveBtn.TabStop = false;
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.saveBtn_MouseDown);
            this.saveBtn.MouseEnter += new System.EventHandler(this.saveBtn_MouseEnter);
            this.saveBtn.MouseLeave += new System.EventHandler(this.saveBtn_MouseLeave);
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.BackgroundImage = global::emotivityMain.Properties.Resources.cancelButton;
            this.cancelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelBtn.FlatAppearance.BorderSize = 0;
            this.cancelBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cancelBtn.Location = new System.Drawing.Point(423, 221);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(85, 36);
            this.cancelBtn.TabIndex = 51;
            this.cancelBtn.TabStop = false;
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cancelBtn_MouseDown);
            this.cancelBtn.MouseEnter += new System.EventHandler(this.cancelBtn_MouseEnter);
            this.cancelBtn.MouseLeave += new System.EventHandler(this.cancelBtn_MouseLeave);
            // 
            // logExportBtn
            // 
            this.logExportBtn.BackColor = System.Drawing.Color.Transparent;
            this.logExportBtn.BackgroundImage = global::emotivityMain.Properties.Resources.selectButton_dis;
            this.logExportBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logExportBtn.Enabled = false;
            this.logExportBtn.FlatAppearance.BorderSize = 0;
            this.logExportBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.logExportBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.logExportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logExportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logExportBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.logExportBtn.Location = new System.Drawing.Point(385, 78);
            this.logExportBtn.Name = "logExportBtn";
            this.logExportBtn.Size = new System.Drawing.Size(79, 36);
            this.logExportBtn.TabIndex = 38;
            this.logExportBtn.TabStop = false;
            this.logExportBtn.UseVisualStyleBackColor = false;
            this.logExportBtn.EnabledChanged += new System.EventHandler(this.logExportBtn_EnabledChanged);
            this.logExportBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logExportBtn_MouseDown);
            this.logExportBtn.MouseEnter += new System.EventHandler(this.logExportBtn_MouseEnter);
            this.logExportBtn.MouseLeave += new System.EventHandler(this.logExportBtn_MouseLeave);
            // 
            // logExportBox
            // 
            this.logExportBox.BackColor = System.Drawing.Color.Silver;
            this.logExportBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logExportBox.Enabled = false;
            this.logExportBox.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.logExportBox.Location = new System.Drawing.Point(86, 87);
            this.logExportBox.Name = "logExportBox";
            this.logExportBox.Size = new System.Drawing.Size(293, 22);
            this.logExportBox.TabIndex = 37;
            this.logExportBox.EnabledChanged += new System.EventHandler(this.logExportBox_EnabledChanged);
            this.logExportBox.TextChanged += new System.EventHandler(this.logExportBox_TextChanged);
            this.logExportBox.Leave += new System.EventHandler(this.logExportBox_Leave);
            // 
            // textExportBtn
            // 
            this.textExportBtn.BackColor = System.Drawing.Color.Transparent;
            this.textExportBtn.BackgroundImage = global::emotivityMain.Properties.Resources.selectButton_dis;
            this.textExportBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textExportBtn.Enabled = false;
            this.textExportBtn.FlatAppearance.BorderSize = 0;
            this.textExportBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.textExportBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.textExportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textExportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textExportBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textExportBtn.Location = new System.Drawing.Point(385, 16);
            this.textExportBtn.Name = "textExportBtn";
            this.textExportBtn.Size = new System.Drawing.Size(79, 36);
            this.textExportBtn.TabIndex = 36;
            this.textExportBtn.TabStop = false;
            this.textExportBtn.UseVisualStyleBackColor = false;
            this.textExportBtn.EnabledChanged += new System.EventHandler(this.textExportBtn_EnabledChanged);
            this.textExportBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textExportBtn_MouseDown);
            this.textExportBtn.MouseEnter += new System.EventHandler(this.textExportBtn_MouseEnter);
            this.textExportBtn.MouseLeave += new System.EventHandler(this.textExportBtn_MouseLeave);
            // 
            // textExportBox
            // 
            this.textExportBox.BackColor = System.Drawing.Color.Silver;
            this.textExportBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textExportBox.Enabled = false;
            this.textExportBox.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textExportBox.Location = new System.Drawing.Point(86, 24);
            this.textExportBox.Name = "textExportBox";
            this.textExportBox.Size = new System.Drawing.Size(293, 22);
            this.textExportBox.TabIndex = 35;
            this.textExportBox.EnabledChanged += new System.EventHandler(this.textExportBox_EnabledChanged);
            this.textExportBox.TextChanged += new System.EventHandler(this.textExportBox_TextChanged);
            this.textExportBox.Leave += new System.EventHandler(this.textExportBox_Leave);
            // 
            // logExportIcon
            // 
            this.logExportIcon.BackColor = System.Drawing.Color.Transparent;
            this.logExportIcon.BackgroundImage = global::emotivityMain.Properties.Resources.log;
            this.logExportIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logExportIcon.Location = new System.Drawing.Point(40, 78);
            this.logExportIcon.Name = "logExportIcon";
            this.logExportIcon.Size = new System.Drawing.Size(40, 40);
            this.logExportIcon.TabIndex = 33;
            this.logExportIcon.TabStop = false;
            // 
            // logExportLabel
            // 
            this.logExportLabel.AutoSize = true;
            this.logExportLabel.BackColor = System.Drawing.Color.Transparent;
            this.logExportLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.logExportLabel.Location = new System.Drawing.Point(19, 121);
            this.logExportLabel.Name = "logExportLabel";
            this.logExportLabel.Size = new System.Drawing.Size(79, 17);
            this.logExportLabel.TabIndex = 34;
            this.logExportLabel.Text = "Output Log";
            this.logExportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textExportIcon
            // 
            this.textExportIcon.BackColor = System.Drawing.Color.Transparent;
            this.textExportIcon.BackgroundImage = global::emotivityMain.Properties.Resources.txt;
            this.textExportIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textExportIcon.Location = new System.Drawing.Point(40, 15);
            this.textExportIcon.Name = "textExportIcon";
            this.textExportIcon.Size = new System.Drawing.Size(40, 40);
            this.textExportIcon.TabIndex = 31;
            this.textExportIcon.TabStop = false;
            // 
            // textExportLabel
            // 
            this.textExportLabel.AutoSize = true;
            this.textExportLabel.BackColor = System.Drawing.Color.Transparent;
            this.textExportLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textExportLabel.Location = new System.Drawing.Point(15, 58);
            this.textExportLabel.Name = "textExportLabel";
            this.textExportLabel.Size = new System.Drawing.Size(92, 17);
            this.textExportLabel.TabIndex = 32;
            this.textExportLabel.Text = "Output Dump";
            this.textExportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textSelectBtnTimer
            // 
            this.textSelectBtnTimer.Tick += new System.EventHandler(this.textSelectBtnTimer_Tick);
            // 
            // saveBtnTimer
            // 
            this.saveBtnTimer.Tick += new System.EventHandler(this.saveBtnTimer_Tick);
            // 
            // cancelBtnTimer
            // 
            this.cancelBtnTimer.Tick += new System.EventHandler(this.cancelBtnTimer_Tick);
            // 
            // logSelectBtnTimer
            // 
            this.logSelectBtnTimer.Tick += new System.EventHandler(this.logSelectBtnTimer_Tick);
            // 
            // defaultBtnTimer
            // 
            this.defaultBtnTimer.Tick += new System.EventHandler(this.defaultBtnTimer_Tick);
            // 
            // emotivitySettingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::emotivityMain.Properties.Resources.bg_color;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(539, 328);
            this.ControlBox = false;
            this.Controls.Add(this.settingPanel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.emotivityLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "emotivitySettingScreen";
            this.Text = "Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.emotivitySettingScreen_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotivityLogo)).EndInit();
            this.settingPanel.ResumeLayout(false);
            this.settingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.composerIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quickIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logPanelIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logExportIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textExportIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox emotivityLogo;
        private System.Windows.Forms.Panel settingPanel;
        private System.Windows.Forms.PictureBox textExportIcon;
        private System.Windows.Forms.Label textExportLabel;
        private System.Windows.Forms.PictureBox logExportIcon;
        private System.Windows.Forms.Label logExportLabel;
        private System.Windows.Forms.FolderBrowserDialog logFolderBrowse;
        private System.Windows.Forms.FolderBrowserDialog textFolderBrowse;
        private System.Windows.Forms.TextBox textExportBox;
        private System.Windows.Forms.Button logExportBtn;
        private System.Windows.Forms.TextBox logExportBox;
        private System.Windows.Forms.Button textExportBtn;
        private System.Windows.Forms.Button defaultBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Timer textSelectBtnTimer;
        private System.Windows.Forms.Timer saveBtnTimer;
        private System.Windows.Forms.Timer cancelBtnTimer;
        private System.Windows.Forms.Timer logSelectBtnTimer;
        private System.Windows.Forms.CheckBox logExportCheckBox;
        private System.Windows.Forms.CheckBox textExportCheckBox;
        private System.Windows.Forms.Timer defaultBtnTimer;
        private System.Windows.Forms.CheckBox logPanelCheckBox;
        private System.Windows.Forms.PictureBox logPanelIcon;
        private System.Windows.Forms.Label logPanelLabel;
        private System.Windows.Forms.CheckBox quickCheckBox;
        private System.Windows.Forms.PictureBox quickIcon;
        private System.Windows.Forms.Label quickLabel;
        private System.Windows.Forms.CheckBox loopCheckBox;
        private System.Windows.Forms.PictureBox loopIcon;
        private System.Windows.Forms.Label loopLabel;
        private System.Windows.Forms.NumericUpDown loopBox;
        private System.Windows.Forms.CheckBox composerCheckBox;
        private System.Windows.Forms.PictureBox composerIcon;
        private System.Windows.Forms.Label composerLabel;
    }
}