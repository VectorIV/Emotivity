using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using static emotivityMain.emotivityBase; 

namespace emotivityMain
{
    public partial class emotivityCustomiseScreen : Form
    {
        Properties.Settings globalVariables = Properties.Settings.Default;

        private int currentTab = 0;
        private string lastPreset;

        private emotionClass[] emotionClassArray = new emotionClass[6];
        private Single emotionThreshold;

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

        private void readPresetNamesFromDisk()
        {
            string currentDirectory = Directory.GetCurrentDirectory() + "\\UserData\\Presets\\";

            if (!Directory.Exists(currentDirectory))
                Directory.CreateDirectory(currentDirectory);

            string[] directoryArray = Directory.GetDirectories(@currentDirectory, "*", SearchOption.TopDirectoryOnly); 

            // DirectoryInfo infoDirectory = new DirectoryInfo(currentDirectory);
            // DirectoryInfo[] directoryArray = infoDirectory.GetDirectories().OrderBy(p => p.CreationTime).ToArray();
            if (directoryArray.Length > 0)
            {
                foreach (string directory in directoryArray)
                {
                    presetComboBox.Items.Add(new DirectoryInfo(@directory).Name);
                }

                try
                {
                    presetComboBox.SelectedItem = globalVariables.lastPreset;
                    lastPreset = presetComboBox.SelectedItem.ToString();
                }
                catch
                {
                    presetComboBox.SelectedItem = emptyComboxText;
                    lastPreset = presetComboBox.SelectedItem.ToString();
                }
            }
            else
            {
                presetComboBox.SelectedItem = emptyComboxText;
                lastPreset = presetComboBox.SelectedItem.ToString();
            }
        }

        private void writePresetDataToDisk(string name)
        {
            string currentDirectory = Directory.GetCurrentDirectory() + "\\UserData\\Presets\\";

            if (name != emptyComboxText) {
                string loopDirectory = currentDirectory + "\\" + name + "\\";
                if (!Directory.Exists(loopDirectory))
                    Directory.CreateDirectory(loopDirectory);

                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = "\t",
                    NewLineOnAttributes = true
                };

                using (XmlWriter xmlWriter = XmlWriter.Create(loopDirectory + "data.xml", xmlWriterSettings))
                {

                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("preset");
                    xmlWriter.WriteAttributeString("threshold", Convert.ToString(emotionThreshold));

                    foreach (emotionClass emotionClassObj in emotionClassArray)
                    {
                        xmlWriter.WriteStartElement("emtionclass");
                        xmlWriter.WriteAttributeString("name", emotionClassObj.getEmotionName());

                        Single[] emotionModifiersArray = new Single[13];
                        emotionModifiersArray = emotionClassObj.getEmotionModifiers();

                        xmlWriter.WriteStartElement("modifiers");

                        xmlWriter.WriteAttributeString("blink", Convert.ToString(emotionModifiersArray[0]));
                        xmlWriter.WriteAttributeString("leftWink", Convert.ToString(emotionModifiersArray[1]));
                        xmlWriter.WriteAttributeString("rightWink", Convert.ToString(emotionModifiersArray[2]));
                        xmlWriter.WriteAttributeString("eyesOpen", Convert.ToString(emotionModifiersArray[3]));
                        xmlWriter.WriteAttributeString("lookUp", Convert.ToString(emotionModifiersArray[4]));
                        xmlWriter.WriteAttributeString("lookDown", Convert.ToString(emotionModifiersArray[5]));
                        xmlWriter.WriteAttributeString("smile", Convert.ToString(emotionModifiersArray[6]));
                        xmlWriter.WriteAttributeString("leftSmirk", Convert.ToString(emotionModifiersArray[7]));
                        xmlWriter.WriteAttributeString("rightSmirk", Convert.ToString(emotionModifiersArray[8]));
                        xmlWriter.WriteAttributeString("laugh", Convert.ToString(emotionModifiersArray[9]));
                        xmlWriter.WriteAttributeString("teethClench", Convert.ToString(emotionModifiersArray[10]));
                        xmlWriter.WriteAttributeString("browRaise", Convert.ToString(emotionModifiersArray[11]));
                        xmlWriter.WriteAttributeString("browFurrow", Convert.ToString(emotionModifiersArray[12]));

                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }
            }
        }
        
        private void removeFromDisk(string name)
        {
            string currentDirectory = Directory.GetCurrentDirectory() + "\\UserData\\Presets\\" + name + "\\";
            Directory.Delete(currentDirectory, true);
            if (presetComboBox.Items.Count > 1)
                presetComboBox.SelectedIndex = presetComboBox.Items.Count - 1;
            else
                presetComboBox.SelectedIndex = 0;
            lastPreset = presetComboBox.SelectedItem.ToString();
            globalVariables.lastPreset = lastPreset;
        }

        private void updateNumericTextBox(int tab)
        {
            Single[] emotionModifiersArray = emotionClassArray[tab].getEmotionModifiers();

            blinkModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[0]);
            lwinkModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[1]);
            rwinkModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[2]);
            eyesModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[3]);
            ulookModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[4]);
            dlookModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[5]);
            smileModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[6]);
            lsmirkModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[7]);
            rsmirkModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[8]);
            laughModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[9]);
            clenchModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[10]);
            browModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[11]);
            frownModifierBox.Value = Convert.ToDecimal(emotionModifiersArray[12]);

            thresholdBox.Value = Convert.ToDecimal(emotionThreshold);
        }

        private void printEmotionClassArray()
        {
            foreach (emotionClass emotionClassObj in emotionClassArray)
            {
                Single[] emotionModifiersArray = new Single[13];
                emotionModifiersArray = emotionClassObj.getEmotionModifiers();
                Console.Write("[");
                foreach (Single modifier in emotionModifiersArray)
                {
                    Console.Write(modifier + " ");
                }
                Console.Write("]\r\n");
            }
        }
        
        public emotivityCustomiseScreen()
        {
            InitializeComponent();
            
            presetComboBox.Items.Add(emptyComboxText);
            readPresetNamesFromDisk();
            
            this.Select();
        }

        private void emotivityCustomiseScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            globalVariables.lastPreset = lastPreset;
            globalVariables.Save();
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

        private void saveBtn_MouseDown(object sender, MouseEventArgs e)
        {
            saveBtn.BackgroundImage = Properties.Resources.saveButton_click;
            saveBtnTimer.Start();

            writePresetDataToDisk(presetComboBox.SelectedItem.ToString());
            lastPreset = presetComboBox.SelectedItem.ToString();
            globalVariables.lastPreset = lastPreset;
            globalVariables.variablesChanged = true;
        }

        private void saveBtnTimer_Tick(object sender, EventArgs e)
        {
            saveBtnTimer.Stop();

            saveBtn.Enabled = false;
            saveBtn.BackgroundImage = Properties.Resources.saveButton_dis;
        }

        private void cancelBtn_MouseEnter(object sender, EventArgs e)
        {
            cancelBtn.BackgroundImage = Properties.Resources.cancelButton_inv;
        }

        private void cancelBtn_MouseLeave(object sender, EventArgs e)
        {
            if (saveBtn.Enabled == true)
                cancelBtn.BackgroundImage = Properties.Resources.cancelButton;
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

        private void newBtn_MouseEnter(object sender, EventArgs e)
        {
            newBtn.BackgroundImage = Properties.Resources.newButton_inv;
        }

        private void newBtn_MouseLeave(object sender, EventArgs e)
        {
            newBtn.BackgroundImage = Properties.Resources.newButton;
        }

        private void newBtn_MouseDown(object sender, MouseEventArgs e)
        {
            newBtn.BackgroundImage = Properties.Resources.newButton_click;
            newBtnTimer.Start();
            if (!string.IsNullOrEmpty(presetComboBox.Text) && !presetComboBox.Items.Contains(presetComboBox.Text))
            {
                presetComboBox.Items.Add(presetComboBox.Text);
                presetComboBox.SelectedIndex = presetComboBox.Items.Count - 1;
                
                writePresetDataToDisk(presetComboBox.SelectedItem.ToString());
            }
        }

        private void newBtnTimer_Tick(object sender, EventArgs e)
        {
            newBtnTimer.Stop();
            newBtn.BackgroundImage = Properties.Resources.newButton_dis;

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void presetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (presetComboBox.SelectedIndex < 1)
            {
                object[] readData = readPresetDataFromDisk("Default");

                emotionClassArray = (emotionClass[])readData[0];
                emotionThreshold = (Single)readData[1];

                updateNumericTextBox(currentTab);

                newBtn.Enabled = true;
                newBtn.BackgroundImage = Properties.Resources.newButton;
                removeBtn.Enabled = false;
                removeBtn.BackgroundImage = Properties.Resources.removeButton_dis;
                saveBtn.Enabled = false;
                saveBtn.BackgroundImage = Properties.Resources.saveButton_dis;

                blinkModifierBox.Enabled = false;
                blinkModifierBox.BackColor = Color.Silver;
                lwinkModifierBox.Enabled = false;
                lwinkModifierBox.BackColor = Color.Silver;
                rwinkModifierBox.Enabled = false;
                rwinkModifierBox.BackColor = Color.Silver;
                eyesModifierBox.Enabled = false;
                eyesModifierBox.BackColor = Color.Silver;
                ulookModifierBox.Enabled = false;
                ulookModifierBox.BackColor = Color.Silver;
                dlookModifierBox.Enabled = false;
                dlookModifierBox.BackColor = Color.Silver;
                smileModifierBox.Enabled = false;
                smileModifierBox.BackColor = Color.Silver;
                lsmirkModifierBox.Enabled = false;
                lsmirkModifierBox.BackColor = Color.Silver;
                rsmirkModifierBox.Enabled = false;
                rsmirkModifierBox.BackColor = Color.Silver;
                laughModifierBox.Enabled = false;
                laughModifierBox.BackColor = Color.Silver;
                clenchModifierBox.Enabled = false;
                clenchModifierBox.BackColor = Color.Silver;
                browModifierBox.Enabled = false;
                browModifierBox.BackColor = Color.Silver;
                frownModifierBox.Enabled = false;
                frownModifierBox.BackColor = Color.Silver;
                thresholdBox.Enabled = false;
                thresholdBox.BackColor = Color.Silver;

                lastPreset = emptyComboxText;
            }
            else
            {
                object[] readData = readPresetDataFromDisk(presetComboBox.SelectedItem.ToString());
                
                emotionClassArray = (emotionClass[])readData[0];
                emotionThreshold = (Single)readData[1];

                updateNumericTextBox(currentTab);

                newBtn.Enabled = false;
                newBtn.BackgroundImage = Properties.Resources.newButton_dis;
                removeBtn.Enabled = true;
                removeBtn.BackgroundImage = Properties.Resources.removeButton;
                saveBtn.Enabled = false;
                saveBtn.BackgroundImage = Properties.Resources.saveButton_dis;

                blinkModifierBox.Enabled = true;
                blinkModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                lwinkModifierBox.Enabled = true;
                lwinkModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                rwinkModifierBox.Enabled = true;
                rwinkModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                eyesModifierBox.Enabled = true;
                eyesModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                ulookModifierBox.Enabled = true;
                ulookModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                dlookModifierBox.Enabled = true;
                dlookModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                smileModifierBox.Enabled = true;
                smileModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                lsmirkModifierBox.Enabled = true;
                lsmirkModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                rsmirkModifierBox.Enabled = true;
                rsmirkModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                laughModifierBox.Enabled = true;
                laughModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                clenchModifierBox.Enabled = true;
                clenchModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                browModifierBox.Enabled = true;
                browModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                frownModifierBox.Enabled = true;
                frownModifierBox.BackColor = Color.FromArgb(74, 78, 84);
                thresholdBox.Enabled = true;
                thresholdBox.BackColor = Color.FromArgb(74, 78, 84);

                lastPreset = presetComboBox.SelectedItem.ToString();
            }
            globalVariables.variablesChanged = true;
        }

        private void presetComboBox_Enter(object sender, EventArgs e)
        {
            if (presetComboBox.SelectedIndex < 1 && presetComboBox.Text == emptyComboxText)
            {
                presetComboBox.Text = "";
            }
        }

        private void presetComboBox_Leave(object sender, EventArgs e)
        {
            if (presetComboBox.SelectedIndex < 1 && string.IsNullOrEmpty(presetComboBox.Text))
            {
                presetComboBox.Text = emptyComboxText;
            }
        }

        private void removeBtn_MouseEnter(object sender, EventArgs e)
        {
            removeBtn.BackgroundImage = Properties.Resources.removeButton_inv;
        }

        private void removeBtn_MouseLeave(object sender, EventArgs e)
        {
            removeBtn.BackgroundImage = Properties.Resources.removeButton;
        }

        private void removeBtn_MouseDown(object sender, MouseEventArgs e)
        {
            removeBtn.BackgroundImage = Properties.Resources.removeButton_click;
            removeBtnTimer.Start();
            presetComboBox.Items.RemoveAt(presetComboBox.SelectedIndex);
            removeFromDisk(lastPreset);
        }

        private void removeBtnTimer_Tick(object sender, EventArgs e)
        {
            removeBtnTimer.Stop();
            if (presetComboBox.SelectedIndex < 1)
                removeBtn.BackgroundImage = Properties.Resources.removeButton_dis;
            else
                removeBtn.BackgroundImage = Properties.Resources.removeButton;
        }

        private void blinkModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(blinkModifierBox.Value), 0);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void lwinkModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(lwinkModifierBox.Value), 1);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void rwinkModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(rwinkModifierBox.Value), 2);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void eyesModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(eyesModifierBox.Value), 3);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void ulookModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(ulookModifierBox.Value), 4);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void dlookModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(dlookModifierBox.Value), 5);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void smileModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(smileModifierBox.Value), 6);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void lsmirkModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(lsmirkModifierBox.Value), 7);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void rsmirkModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(rsmirkModifierBox.Value), 8);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void laughModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(laughModifierBox.Value), 9);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void clenchModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(clenchModifierBox.Value), 10);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void browModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(browModifierBox.Value), 11);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void frownModifierBox_ValueChanged(object sender, EventArgs e)
        {
            emotionClassArray[currentTab].setEmotionModifier(Convert.ToSingle(frownModifierBox.Value), 12);

            saveBtn.Enabled = true;
            saveBtn.BackgroundImage = Properties.Resources.saveButton;
        }

        private void joyTabIcon_MouseDown(object sender, MouseEventArgs e)
        {
            joyTabIcon.BackgroundImage = Properties.Resources.i_joy_click;
            loveTabIcon.BackgroundImage = Properties.Resources.i_love;
            wowTabIcon.BackgroundImage = Properties.Resources.i_wow;
            sadTabIcon.BackgroundImage = Properties.Resources.i_sad;
            fearTabIcon.BackgroundImage = Properties.Resources.i_fear;
            angerTabIcon.BackgroundImage = Properties.Resources.i_anger;

            currentTab = 0;
            updateNumericTextBox(currentTab);
        }

        private void joyTabIcon_MouseEnter(object sender, EventArgs e)
        {
            if (currentTab != 0)
                joyTabIcon.BackgroundImage = Properties.Resources.i_joy_inv;
        }

        private void joyTabIcon_MouseLeave(object sender, EventArgs e)
        {
            if (currentTab != 0)
                joyTabIcon.BackgroundImage = Properties.Resources.i_joy;
        }

        private void loveTabIcon_MouseDown(object sender, MouseEventArgs e)
        {
            joyTabIcon.BackgroundImage = Properties.Resources.i_joy;
            loveTabIcon.BackgroundImage = Properties.Resources.i_love_click;
            wowTabIcon.BackgroundImage = Properties.Resources.i_wow;
            sadTabIcon.BackgroundImage = Properties.Resources.i_sad;
            fearTabIcon.BackgroundImage = Properties.Resources.i_fear;
            angerTabIcon.BackgroundImage = Properties.Resources.i_anger;

            currentTab = 1;
            updateNumericTextBox(currentTab);
        }

        private void loveTabIcon_MouseEnter(object sender, EventArgs e)
        {
            if (currentTab != 1)
                loveTabIcon.BackgroundImage = Properties.Resources.i_love_inv;
        }

        private void loveTabIcon_MouseLeave(object sender, EventArgs e)
        {
            if (currentTab != 1)
                loveTabIcon.BackgroundImage = Properties.Resources.i_love;
        }

        private void wowTabIcon_MouseDown(object sender, MouseEventArgs e)
        {
            joyTabIcon.BackgroundImage = Properties.Resources.i_joy;
            loveTabIcon.BackgroundImage = Properties.Resources.i_love;
            wowTabIcon.BackgroundImage = Properties.Resources.i_wow_click;
            sadTabIcon.BackgroundImage = Properties.Resources.i_sad;
            fearTabIcon.BackgroundImage = Properties.Resources.i_fear;
            angerTabIcon.BackgroundImage = Properties.Resources.i_anger;

            currentTab = 2;
            updateNumericTextBox(currentTab);
        }

        private void wowTabIcon_MouseEnter(object sender, EventArgs e)
        {
            if (currentTab != 2)
                wowTabIcon.BackgroundImage = Properties.Resources.i_wow_inv;
        }

        private void wowTabIcon_MouseLeave(object sender, EventArgs e)
        {
            if (currentTab != 2)
                wowTabIcon.BackgroundImage = Properties.Resources.i_wow;
        }

        private void sadTabIcon_MouseDown(object sender, MouseEventArgs e)
        {
            joyTabIcon.BackgroundImage = Properties.Resources.i_joy;
            loveTabIcon.BackgroundImage = Properties.Resources.i_love;
            wowTabIcon.BackgroundImage = Properties.Resources.i_wow;
            sadTabIcon.BackgroundImage = Properties.Resources.i_sad_click;
            fearTabIcon.BackgroundImage = Properties.Resources.i_fear;
            angerTabIcon.BackgroundImage = Properties.Resources.i_anger;

            currentTab = 3;
            updateNumericTextBox(currentTab);
        }

        private void sadTabIcon_MouseEnter(object sender, EventArgs e)
        {
            if (currentTab != 3)
                sadTabIcon.BackgroundImage = Properties.Resources.i_sad_inv;
        }

        private void sadTabIcon_MouseLeave(object sender, EventArgs e)
        {
            if (currentTab != 3)
                sadTabIcon.BackgroundImage = Properties.Resources.i_sad;
        }

        private void fearTabIcon_MouseDown(object sender, MouseEventArgs e)
        {
            joyTabIcon.BackgroundImage = Properties.Resources.i_joy;
            loveTabIcon.BackgroundImage = Properties.Resources.i_love;
            wowTabIcon.BackgroundImage = Properties.Resources.i_wow;
            sadTabIcon.BackgroundImage = Properties.Resources.i_sad;
            fearTabIcon.BackgroundImage = Properties.Resources.i_fear_click;
            angerTabIcon.BackgroundImage = Properties.Resources.i_anger;

            currentTab = 4;
            updateNumericTextBox(currentTab);
        }

        private void fearTabIcon_MouseEnter(object sender, EventArgs e)
        {
            if (currentTab != 4)
                fearTabIcon.BackgroundImage = Properties.Resources.i_fear_inv;
        }

        private void fearTabIcon_MouseLeave(object sender, EventArgs e)
        {
            if (currentTab != 4)
                fearTabIcon.BackgroundImage = Properties.Resources.i_fear;
        }

        private void angerTabIcon_MouseDown(object sender, MouseEventArgs e)
        {
            joyTabIcon.BackgroundImage = Properties.Resources.i_joy;
            loveTabIcon.BackgroundImage = Properties.Resources.i_love;
            wowTabIcon.BackgroundImage = Properties.Resources.i_wow;
            sadTabIcon.BackgroundImage = Properties.Resources.i_sad;
            fearTabIcon.BackgroundImage = Properties.Resources.i_fear;
            angerTabIcon.BackgroundImage = Properties.Resources.i_anger_click;

            currentTab = 5;
            updateNumericTextBox(currentTab);
        }

        private void angerTabIcon_MouseEnter(object sender, EventArgs e)
        {
            if (currentTab != 5)
                angerTabIcon.BackgroundImage = Properties.Resources.i_anger_inv;
        }

        private void angerTabIcon_MouseLeave(object sender, EventArgs e)
        {
            if (currentTab != 5)
                angerTabIcon.BackgroundImage = Properties.Resources.i_anger;
        }

        private void thresholdBox_ValueChanged(object sender, EventArgs e)
        {
            emotionThreshold = Convert.ToSingle(thresholdBox.Value);
            saveBtn.Enabled = true;
        }

        private void defaultBtn_MouseDown(object sender, MouseEventArgs e)
        {
            defaultBtn.BackgroundImage = Properties.Resources.defaultButton_click;
            defaultBtnTimer.Start();

            emotionClass[] _emotionClassArray = getDefaultEmotionClassArray();
            emotionClassArray[currentTab] = _emotionClassArray[currentTab];
            Single _emotionThreshold = getDefaultEmotionThreshold();
            emotionThreshold = _emotionThreshold;

            updateNumericTextBox(currentTab);
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

        private void allDefaultBtn_MouseDown(object sender, MouseEventArgs e)
        {
            allDefaultBtn.BackgroundImage = Properties.Resources.allDefaultButton_click;
            allDefaultBtnTimer.Start();

            emotionClass[] _emotionClassArray = getDefaultEmotionClassArray();
            emotionClassArray = _emotionClassArray;
            Single _emotionThreshold = getDefaultEmotionThreshold();
            emotionThreshold = _emotionThreshold;

            updateNumericTextBox(currentTab);
        }

        private void allDefaultBtn_MouseEnter(object sender, EventArgs e)
        {
            allDefaultBtn.BackgroundImage = Properties.Resources.allDefaultButton_inv;
        }

        private void allDefaultBtn_MouseLeave(object sender, EventArgs e)
        {
            allDefaultBtn.BackgroundImage = Properties.Resources.allDefaultButton;
        }

        private void allDefaultBtnTimer_Tick(object sender, EventArgs e)
        {
            allDefaultBtnTimer.Stop();
            allDefaultBtn.BackgroundImage = Properties.Resources.allDefaultButton;
        }
    }
}
