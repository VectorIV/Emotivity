using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using static emotivityMain.emotivityBase;

namespace emotivityMain
{
    public partial class emotivitySettingScreen : Form
    {
        Properties.Settings globalVariables = Properties.Settings.Default;


        private Single emotionThreshold = getDefaultEmotionThreshold();

        private static object[] defaultObject = getDefaultOptionData();
        private static Boolean[] defaultBooleanArray = (Boolean[])defaultObject[0];
        private static string[] defaultStringArray = (string[])defaultObject[1];
        private static int[] defaultIntegerArray = (int[])defaultObject[2];

        private Boolean isTextOutput = defaultBooleanArray[0];
        private Boolean isLogOutput = defaultBooleanArray[1];
        private Boolean isLogPanel = defaultBooleanArray[2];
        private Boolean isQuickAction = defaultBooleanArray[3];
        private Boolean isInvokeDelay = defaultBooleanArray[4];
        private Boolean isUseEmotivComposer = defaultBooleanArray[5];

        private int invokeDelay = defaultIntegerArray[0];

        private string textFolderDirectory = defaultStringArray[0];
        private string logFolderDirectory = defaultStringArray[1];

        private Image bmpBackground;

        public override Image BackgroundImage
        {
            get
            {
                return bmpBackground;
            }
            set
            {
                if (value != null)
                {
                    //Create new BitMap Object of the size 
                    bmpBackground = new Bitmap(value.Width, value.Height);

                    //Create graphics from image
                    System.Drawing.Graphics g =
                       System.Drawing.Graphics.FromImage(bmpBackground);

                    //set the graphics interpolation mode to heigh
                    g.InterpolationMode =
                       System.Drawing.Drawing2D.InterpolationMode.High;

                    //draw the image to the graphics to create the new image 
                    //which will be used in the onpaint background
                    g.DrawImage(value, new Rectangle(0, 0, bmpBackground.Width,
                                bmpBackground.Height));
                }
                else
                    bmpBackground = null;
            }
        }
        
        private Boolean checkDirectory(string directory)
        {
            try
            {
                Boolean directoryExistence = Directory.Exists(directory);
                if (!directoryExistence)
                {
                    Directory.CreateDirectory(directory);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void initializeFormLoad()
        {
            object[] readData = readOptionDataFromDisk();

            Boolean[] checkBoxArray = (Boolean[])readData[0];
            isTextOutput = checkBoxArray[0];
            isLogOutput = checkBoxArray[1];
            isLogPanel = checkBoxArray[2];
            isQuickAction = checkBoxArray[3];
            isInvokeDelay = checkBoxArray[4];
            isUseEmotivComposer = checkBoxArray[5];

            string[] directoryArray = (string[])readData[1];
            textFolderDirectory = directoryArray[0];
            logFolderDirectory = directoryArray[1];

            int[] integerValueArray = (int[])readData[2];
            invokeDelay = integerValueArray[0];

            int[] invokeRange = getInvokeDelayRange();

            textExportBox.Text = textFolderDirectory;
            logExportBox.Text = logFolderDirectory;
            textFolderBrowse.SelectedPath = textFolderDirectory;
            logFolderBrowse.SelectedPath = logFolderDirectory;

            textExportCheckBox.Checked = isTextOutput;
            logExportCheckBox.Checked = isLogOutput;
            logPanelCheckBox.Checked = isLogPanel;
            quickCheckBox.Checked = isQuickAction;
            loopCheckBox.Checked = isInvokeDelay;
            composerCheckBox.Checked = isUseEmotivComposer;

            loopBox.Value = invokeDelay;
            loopBox.Minimum = invokeRange[0];
            loopBox.Maximum = invokeRange[1];

            saveBtn.Enabled = false;
            saveBtn.BackgroundImage = Properties.Resources.saveButton_dis;
        }

        public emotivitySettingScreen()
        {
            InitializeComponent();

            initializeFormLoad();
        }

        private void emotivitySettingScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void textExportBtn_MouseDown(object sender, MouseEventArgs e)
        {
            textExportBtn.BackgroundImage = Properties.Resources.selectButton_click;
            textSelectBtnTimer.Start();
        }

        private void textExportBtn_MouseEnter(object sender, EventArgs e)
        {
            textExportBtn.BackgroundImage = Properties.Resources.selectButton_inv;
        }

        private void textExportBtn_MouseLeave(object sender, EventArgs e)
        {
            textExportBtn.BackgroundImage = Properties.Resources.selectButton;
        }

        private void textSelectBtnTimer_Tick(object sender, EventArgs e)
        {
            textSelectBtnTimer.Stop();
            textExportBtn.BackgroundImage = Properties.Resources.selectButton;

            DialogResult dialogResult = textFolderBrowse.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                textFolderDirectory = @textFolderBrowse.SelectedPath + "\\";
                // Some bizzare bug where the variable is empty, but the text box is not?
                textExportBox.Text = textFolderDirectory;
                textFolderDirectory = textExportBox.Text;
                saveBtn.Enabled = true;
                saveBtn.BackgroundImage = Properties.Resources.saveButton;
            }
        }

        private void textExportBtn_EnabledChanged(object sender, EventArgs e)
        {
            if (!isTextOutput)
                    textExportBtn.BackgroundImage = Properties.Resources.selectButton_dis;
            else
                    textExportBtn.BackgroundImage = Properties.Resources.selectButton;
        }

        private void textExportBox_TextChanged(object sender, EventArgs e)
        {
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void textExportBox_Leave(object sender, EventArgs e)
        {
            textFolderDirectory = textExportBox.Text;

            if (!checkDirectory(textFolderDirectory))
            {
                MessageBox.Show("Please specify the directory in a corrent syntax.", "Invalid Directory Syntax",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                textExportBox.Focus();
            }
            else
            {
                textFolderBrowse.SelectedPath = textFolderDirectory;
            }
        }

        private void textExportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isTextOutput = textExportCheckBox.Checked;
            if (isTextOutput)
            {
                textExportBox.Enabled = true;
                textExportBtn.Enabled = true;
            }
            else
            {
                textExportBox.Enabled = false;
                textExportBtn.Enabled = false;
            }
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void textExportBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!isTextOutput)
                 textExportBox.BackColor = Color.Silver;
            else
                 textExportBox.BackColor = Color.FromArgb(74, 78, 84);
        }

        private void logExportBtn_MouseDown(object sender, MouseEventArgs e)
        {
            logExportBtn.BackgroundImage = Properties.Resources.selectButton_click;
            logSelectBtnTimer.Start();
        }

        private void logExportBtn_MouseEnter(object sender, EventArgs e)
        {
            logExportBtn.BackgroundImage = Properties.Resources.selectButton_inv;
        }

        private void logExportBtn_MouseLeave(object sender, EventArgs e)
        {
            logExportBtn.BackgroundImage = Properties.Resources.selectButton;
        }

        private void logSelectBtnTimer_Tick(object sender, EventArgs e)
        {
            logSelectBtnTimer.Stop();
            logExportBtn.BackgroundImage = Properties.Resources.selectButton;

            DialogResult dialogResult = logFolderBrowse.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                logFolderDirectory = @logFolderBrowse.SelectedPath + "\\";
                // Some bizzare bug where the variable is empty, but the text box is not?
                logExportBox.Text = logFolderDirectory;
                logFolderDirectory = logExportBox.Text;
                saveBtn.Enabled = true;
                saveBtn.BackgroundImage = Properties.Resources.saveButton;
            }
        }

        private void logExportBtn_EnabledChanged(object sender, EventArgs e)
        {
            if (!isLogOutput)
                    logExportBtn.BackgroundImage = Properties.Resources.selectButton_dis;
            else
                    logExportBtn.BackgroundImage = Properties.Resources.selectButton;
        }

        private void logExportBox_TextChanged(object sender, EventArgs e)
        {
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void logExportBox_Leave(object sender, EventArgs e)
        {
            logFolderDirectory = logExportBox.Text;

            if (!checkDirectory(logFolderDirectory))
            {
                MessageBox.Show("Please specify the directory in a corrent syntax.", "Invalid Directory Syntax",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                logExportBox.Focus();
            }
            else
            {
                logFolderBrowse.SelectedPath = logFolderDirectory;
            }
        }

        private void logExportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isLogOutput = logExportCheckBox.Checked;
            if (isLogOutput)
            {
                logExportBox.Enabled = true;
                logExportBtn.Enabled = true;
            }
            else
            {
                logExportBox.Enabled = false;
                logExportBtn.Enabled = false;
            }
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void logExportBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!isLogOutput)
                logExportBox.BackColor = Color.Silver;
            else
                logExportBox.BackColor = Color.FromArgb(74, 78, 84);
        }

        private void saveBtn_MouseDown(object sender, MouseEventArgs e)
        {
            saveBtn.BackgroundImage = Properties.Resources.saveButton_click;
            saveBtnTimer.Start();

            writeOptionsDataToDisk(isTextOutput, isLogOutput, textFolderDirectory, logFolderDirectory,
                isLogPanel, isQuickAction, isInvokeDelay, invokeDelay, isUseEmotivComposer);

            globalVariables.settingsChanged = true;
            globalVariables.Save();
        }
        
        private void saveBtnTimer_Tick(object sender, EventArgs e)
        {
            saveBtnTimer.Stop();

            saveBtn.Enabled = false;
            saveBtn.BackgroundImage = Properties.Resources.saveButton_dis;
        }

        private void saveBtn_MouseEnter(object sender, EventArgs e)
        {
            saveBtn.BackgroundImage = Properties.Resources.saveButton_inv;
        }

        private void saveBtn_MouseLeave(object sender, EventArgs e)
        {
            if (saveBtn.Enabled == true)
                saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void cancelBtn_MouseDown(object sender, MouseEventArgs e)
        {
            cancelBtn.BackgroundImage = Properties.Resources.cancelButton_click;
            cancelBtnTimer.Start();
        }

        private void cancelBtnTimer_Tick(object sender, EventArgs e)
        {
            cancelBtnTimer.Stop();
            cancelBtn.BackgroundImage = Properties.Resources.cancelButton;
            this.Close();
        }

        private void cancelBtn_MouseEnter(object sender, EventArgs e)
        {
            cancelBtn.BackgroundImage = Properties.Resources.cancelButton_inv;
        }

        private void cancelBtn_MouseLeave(object sender, EventArgs e)
        {
            cancelBtn.BackgroundImage = Properties.Resources.cancelButton;
        }

        private void defaultBtn_MouseDown(object sender, MouseEventArgs e)
        {
            defaultBtn.BackgroundImage = Properties.Resources.defaultButton_click;
            defaultBtnTimer.Start();

            object[] readData = getDefaultOptionData();

            Boolean[] checkBoxArray = (Boolean[])readData[0];
            isTextOutput = checkBoxArray[0];
            isLogOutput = checkBoxArray[1];
            isLogPanel = checkBoxArray[2];
            isQuickAction = checkBoxArray[3];
            isInvokeDelay = checkBoxArray[4];

            string[] directoryArray = (string[])readData[1];
            textFolderDirectory = directoryArray[0];
            logFolderDirectory = directoryArray[1];

            int[] integerValueArray = (int[])readData[2];
            invokeDelay = integerValueArray[0];

            textExportBox.Text = textFolderDirectory;
            logExportBox.Text = logFolderDirectory;
            textFolderBrowse.SelectedPath = textFolderDirectory;
            logFolderBrowse.SelectedPath = logFolderDirectory;

            textExportCheckBox.Checked = isTextOutput;
            logExportCheckBox.Checked = isLogOutput;
            logPanelCheckBox.Checked = isLogPanel;
            quickCheckBox.Checked = isQuickAction;
            loopCheckBox.Checked = isInvokeDelay;

            loopBox.Value = invokeDelay;
        }

        private void defaultBtn_MouseEnter(object sender, EventArgs e)
        {
            defaultBtn.BackgroundImage = Properties.Resources.defaultButton_inv;
        }

        private void defaultBtn_MouseLeave(object sender, EventArgs e)
        {
            defaultBtn.BackgroundImage = Properties.Resources.defaultButton;
        }

        private void defaultBtnTimer_Tick(object sender, EventArgs e)
        {
            defaultBtnTimer.Stop();
            defaultBtn.BackgroundImage = Properties.Resources.defaultButton;
        }

        private void logPanelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isLogPanel = logPanelCheckBox.Checked;
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void quickCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isQuickAction = quickCheckBox.Checked;
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void loopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isInvokeDelay = loopCheckBox.Checked;
            if (isInvokeDelay)
                loopBox.Enabled = true;
            else
                loopBox.Enabled = false;
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void loopBox_ValueChanged(object sender, EventArgs e)
        {
            invokeDelay = (int)loopBox.Value;
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void loopBox_EnabledChanged(object sender, EventArgs e)
        {
            if (isInvokeDelay)
                loopBox.BackColor = Color.FromArgb(74, 78, 84);
            else
                loopBox.BackColor = Color.Silver;
        }

        private void composerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isUseEmotivComposer = composerCheckBox.Checked;
            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }
    }
}
