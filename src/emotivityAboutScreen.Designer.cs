namespace emotivityMain
{
    partial class emotivityAboutScreen
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.programLabelPanel = new System.Windows.Forms.Panel();
            this.creditBox = new System.Windows.Forms.RichTextBox();
            this.rightsLabel = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.programLabel = new System.Windows.Forms.Label();
            this.programLogo = new System.Windows.Forms.PictureBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.creditBoxScrollTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.programLabelPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.programLabelPanel);
            this.panel1.Controls.Add(this.programLogo);
            this.panel1.Location = new System.Drawing.Point(-4, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 220);
            this.panel1.TabIndex = 1;
            // 
            // programLabelPanel
            // 
            this.programLabelPanel.BackColor = System.Drawing.SystemColors.Window;
            this.programLabelPanel.Controls.Add(this.creditBox);
            this.programLabelPanel.Controls.Add(this.rightsLabel);
            this.programLabelPanel.Controls.Add(this.copyrightLabel);
            this.programLabelPanel.Controls.Add(this.versionLabel);
            this.programLabelPanel.Controls.Add(this.programLabel);
            this.programLabelPanel.Location = new System.Drawing.Point(119, 13);
            this.programLabelPanel.Name = "programLabelPanel";
            this.programLabelPanel.Size = new System.Drawing.Size(230, 194);
            this.programLabelPanel.TabIndex = 1;
            // 
            // creditBox
            // 
            this.creditBox.BackColor = System.Drawing.SystemColors.Window;
            this.creditBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.creditBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditBox.Location = new System.Drawing.Point(4, 121);
            this.creditBox.Name = "creditBox";
            this.creditBox.ReadOnly = true;
            this.creditBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.creditBox.Size = new System.Drawing.Size(223, 65);
            this.creditBox.TabIndex = 4;
            this.creditBox.Text = "";
            // 
            // rightsLabel
            // 
            this.rightsLabel.AutoSize = true;
            this.rightsLabel.Location = new System.Drawing.Point(4, 91);
            this.rightsLabel.Name = "rightsLabel";
            this.rightsLabel.Size = new System.Drawing.Size(126, 17);
            this.rightsLabel.TabIndex = 3;
            this.rightsLabel.Text = "All rights reserved.";
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.AutoSize = true;
            this.copyrightLabel.Location = new System.Drawing.Point(4, 69);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(219, 17);
            this.copyrightLabel.TabIndex = 2;
            this.copyrightLabel.Text = "Copyright © 2017 Acoustic Alpha.";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft PhagsPa", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.Black;
            this.versionLabel.Location = new System.Drawing.Point(5, 38);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(216, 20);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "Version 1.0.0 Build:234e2017";
            // 
            // programLabel
            // 
            this.programLabel.AutoSize = true;
            this.programLabel.Font = new System.Drawing.Font("Microsoft PhagsPa", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programLabel.ForeColor = System.Drawing.Color.Orange;
            this.programLabel.Location = new System.Drawing.Point(4, 4);
            this.programLabel.Name = "programLabel";
            this.programLabel.Size = new System.Drawing.Size(213, 22);
            this.programLabel.TabIndex = 0;
            this.programLabel.Text = "Emotivity Control Panel™\r\n";
            // 
            // programLogo
            // 
            this.programLogo.BackgroundImage = global::emotivityMain.Properties.Resources.icon;
            this.programLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.programLogo.Location = new System.Drawing.Point(16, 13);
            this.programLogo.Name = "programLogo";
            this.programLogo.Size = new System.Drawing.Size(86, 86);
            this.programLogo.TabIndex = 0;
            this.programLogo.TabStop = false;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(255, 232);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(90, 27);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // creditBoxScrollTimer
            // 
            this.creditBoxScrollTimer.Interval = 5000;
            this.creditBoxScrollTimer.Tick += new System.EventHandler(this.creditBoxScrollTimer_Tick);
            // 
            // emotivityAboutScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 271);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "emotivityAboutScreen";
            this.ShowIcon = false;
            this.Text = "About";
            this.panel1.ResumeLayout(false);
            this.programLabelPanel.ResumeLayout(false);
            this.programLabelPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox programLogo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel programLabelPanel;
        private System.Windows.Forms.Label programLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.Label rightsLabel;
        private System.Windows.Forms.RichTextBox creditBox;
        private System.Windows.Forms.Timer creditBoxScrollTimer;
    }
}