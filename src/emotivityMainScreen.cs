using System;
using System.Drawing;
using System.Windows.Forms;

using Emotiv;
using System.Threading;
using System.Linq;
using static emotivityMain.emotivityBase;
using System.IO;

namespace emotivityMain
{
    
    partial class emotivityMainForm : Form
    {
        EmoEngine engine = EmoEngine.Instance;
        Properties.Settings globalVariables = Properties.Settings.Default;

        private static int userID = -1;
        private static int engineConnected = 0;

        private Single[] emotionArray = new Single[13];
        private emotionClass[] emotionClassArray = new emotionClass[6];
        
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
        
        private string currentDate = "";

        private int currentTab = 0;
        private int menuBtnState = 0;
        private int menuBtnCounter = 0;

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
                    Graphics g = Graphics.FromImage(bmpBackground);

                    //set the graphics interpolation mode to heigh
                    g.InterpolationMode =
                       System.Drawing.Drawing2D.InterpolationMode.High;

                    //draw the image to the graphics to create the new image 
                    //which will be used in the onpaint background
                    g.DrawImage(value, new Rectangle(0, 0, bmpBackground.Width, bmpBackground.Height));
                }
                else
                    bmpBackground = null;
            }
        }

        private void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
        {
            engineConnected = 1;
            Console.WriteLine("// ---------- Emoengine connected ---------- //");
        }

        private void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            engineConnected = 0;
            Console.WriteLine("// ---------- Emoengine disconnected ---------- //");
        }

        private void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    engine_EmoStateUpdated(sender, e);
                });
            }
            else
            {
                EmoState es = e.emoState;

                if (e.userId != 0) return;      // Get info from UserID 0

                // Get the signal strength
                EdkDll.IEE_SignalStrength_t signalStrength = es.GetWirelessSignalStatus();

                // Get the battery life
                Int32 chargeLevel = 0;
                Int32 maxChargeLevel = 0;
                es.GetBatteryChargeLevel(out chargeLevel, out maxChargeLevel);

                // Console.WriteLine("Signal: " + signalStrength + " Charge: " + chargeLevel);
                updateHeaderDisplay(signalStrength, chargeLevel);
            }
        }

        private void engine_FacialExpressionEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    engine_FacialExpressionEmoStateUpdated(sender, e);
                });
            }
            else
            {
                EmoState es = e.emoState;
                string emotionClassLog = "";
                int forLoopCounter = 0;
                object[] computedChartValues = new object[6];

                Single timeFromStart = es.GetTimeFromStart();

                emotionArray = getEmoStateArray(es);
                
                foreach (emotionClass emotionClassObj in emotionClassArray)
                {
                    emotionClassObj.computeValue(emotionArray);

                    if (FormWindowState.Normal == this.WindowState)
                    {
                        computedChartValues[forLoopCounter] = computeChartUpdate(emotionClassObj.getEmotionValue());
                    }

                    emotionClassLog = concatString(emotionClassLog, emotionClassObj.getEmotionName() + ": " + emotionClassObj.getEmotionValue());
                    forLoopCounter++;
                };

                if (FormWindowState.Normal == this.WindowState && currentTab == 0)
                {
                    updateChart(computedChartValues);
                }

                emotionClass maxEmotionClass = getMaximumEmotionClass();
                updateCloud(maxEmotionClass.getEmotionName(), maxEmotionClass.getEmotionValue(), timeFromStart);
                if (isLogPanel) { logOnConsole(emotionClassLog, maxEmotionClass.getEmotionName(), maxEmotionClass.getEmotionValue(), timeFromStart); }
                
                if (isTextOutput) { writeTextExport(maxEmotionClass.getEmotionName()); }
                if (isLogOutput) { writeLogExport(emotionClassLog, currentDate); emotionClassLog = ""; }

                if (globalVariables.disconnectedEngine)
                {
                    disconnectedElementsReset();
                    globalVariables.disconnectedEngine = false;
                    engine.Disconnect();
                }

            }
        }
        
        private void updateHeaderDisplay(EdkDll.IEE_SignalStrength_t signal, Int32 charge)
        {
            switch (Convert.ToString(signal))
            {
                case "GOOD_SIG":
                    signalDisplay.BackgroundImage = Properties.Resources.wifiFull;
                    break;
                case "BAD_SIG":
                    signalDisplay.BackgroundImage = Properties.Resources.wifiLow;
                    break;
                case "NO_SIG":
                    signalDisplay.BackgroundImage = Properties.Resources.wifiNone;
                    
                    disconnectedElementsReset();
                    engine.Disconnect();
                    break;
            }
            switch (charge)
            {
                case -1:
                    batteryDisplay.BackgroundImage = Properties.Resources.batteryNone;
                    break;
                case 2:
                    batteryDisplay.BackgroundImage = Properties.Resources.batteryLow;
                    break;
                case 3:
                    batteryDisplay.BackgroundImage = Properties.Resources.batteryHalf;
                    break;
                case 4:
                    batteryDisplay.BackgroundImage = Properties.Resources.batteryFull;
                    break;
            }
        }

        private int[] computeChartUpdate(Single value)
        {
            int contentBoxWidth = chartContentBox.Width;
            int iconWidth = 40;

            int chartBarUpdate = Convert.ToInt32((contentBoxWidth - 10 - 14 - iconWidth - 6) * value);

            int chartBarWidth = chartBarUpdate + 6;
            int chartBarX = contentBoxWidth - 10 - 14 - 6 - chartBarUpdate;
            int chartIconX = contentBoxWidth - 10 - 14 - chartBarWidth - 6 - iconWidth;

            return new int[] { chartBarWidth, chartBarX, chartIconX };
        }

        private void updateChart(object[] array)
        {
            if (this.InvokeRequired)
            {
                {
                    updateChart(array);
                };
            }
            else
            {
                int[] joyArray = (int[])array[0];
                int[] loveArray = (int[])array[1];
                int[] wowArray = (int[])array[2];
                int[] sadArray = (int[])array[3];
                int[] fearArray = (int[])array[4];
                int[] angerArray = (int[])array[5];

                joyChartBar.Size = new Size(joyArray[0], joyChartBar.Height);
                joyChartBar.Location = new Point(joyArray[1], joyChartBar.Location.Y);
                joyChartIcon.Visible = false;
                joyChartIcon.Location = new Point(joyArray[2], joyChartIcon.Location.Y);
                joyChartIcon.Visible = true;

                loveChartBar.Size = new Size(loveArray[0], loveChartBar.Height);
                loveChartBar.Location = new Point(loveArray[1], loveChartBar.Location.Y);
                loveChartIcon.Visible = false;
                loveChartIcon.Location = new Point(loveArray[2], loveChartIcon.Location.Y);
                loveChartIcon.Visible = true;

                wowChartBar.Size = new Size(wowArray[0], wowChartBar.Height);
                wowChartBar.Location = new Point(wowArray[1], wowChartBar.Location.Y);
                wowChartIcon.Visible = false;
                wowChartIcon.Location = new Point(wowArray[2], wowChartIcon.Location.Y);
                wowChartIcon.Visible = true;

                sadChartBar.Size = new Size(sadArray[0], sadChartBar.Height);
                sadChartBar.Location = new Point(sadArray[1], sadChartBar.Location.Y);
                sadChartIcon.Visible = false;
                sadChartIcon.Location = new Point(sadArray[2], sadChartIcon.Location.Y);
                sadChartIcon.Visible = true;

                fearChartBar.Size = new Size(fearArray[0], fearChartBar.Height);
                fearChartBar.Location = new Point(fearArray[1], fearChartBar.Location.Y);
                fearChartIcon.Visible = false;
                fearChartIcon.Location = new Point(fearArray[2], fearChartIcon.Location.Y);
                fearChartIcon.Visible = true;

                angerChartBar.Size = new Size(angerArray[0], angerChartBar.Height);
                angerChartBar.Location = new Point(angerArray[1], angerChartBar.Location.Y);
                angerChartIcon.Visible = false;
                angerChartIcon.Location = new Point(angerArray[2], angerChartIcon.Location.Y);
                angerChartIcon.Visible = true;
            }
        }
        
        private void updateThresholdBar(Single emotionThreshold)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    updateThresholdBar(emotionThreshold);
                });
            }
            else
            {
                thresholdBar.Location = new Point(Convert.ToInt32(10 + joyChartIcon.Width + 6 + ((chartContentBox.Width - 10 - joyChartIcon.Width - 6 - 14 - 10) * (1 - emotionThreshold))), 5);
                thresholdLabel.Location = new Point(thresholdBar.Location.X + 5, 5);
                thresholdLabel.Text = String.Format("{0:0.00}", emotionThreshold);
            }
        }

        private void updateCloud(string name, Single value, Single timeStamp)
        {
            if (currentTab == 0)
            {
                if (value >= emotionThreshold)
                {
                    switch (name)
                    {
                        case "Joy":
                            cloudContent.BackgroundImage = Properties.Resources.i_joy;
                            if (FormWindowState.Minimized == this.WindowState)
                                notifyIcon.Icon = Properties.Resources.i_joy_icon;
                            break;
                        case "Love":
                            cloudContent.BackgroundImage = Properties.Resources.i_love;
                            if (FormWindowState.Minimized == this.WindowState)
                                notifyIcon.Icon = Properties.Resources.i_love_icon;
                            break;
                        case "Surprise":
                            cloudContent.BackgroundImage = Properties.Resources.i_wow;
                            if (FormWindowState.Minimized == this.WindowState)
                                notifyIcon.Icon = Properties.Resources.i_wow_icon;
                            break;
                        case "Sad":
                            cloudContent.BackgroundImage = Properties.Resources.i_sad;
                            if (FormWindowState.Minimized == this.WindowState)
                                notifyIcon.Icon = Properties.Resources.i_sad_icon;
                            break;
                        case "Fear":
                            cloudContent.BackgroundImage = Properties.Resources.i_fear;
                            if (FormWindowState.Minimized == this.WindowState)
                                notifyIcon.Icon = Properties.Resources.i_fear_icon;
                            break;
                        case "Anger":
                            cloudContent.BackgroundImage = Properties.Resources.i_anger;
                            if (FormWindowState.Minimized == this.WindowState)
                                notifyIcon.Icon = Properties.Resources.i_anger_icon;
                            break;
                    }
                }
                else
                {
                    cloudContent.BackgroundImage = Properties.Resources.i_neutral;
                    if (FormWindowState.Minimized == this.WindowState)
                        notifyIcon.Icon = Properties.Resources.i_neutral_icon;
                }
            }
        }

        private void logOnConsole(string list, string name, Single value, Single timeStamp)
        {
            consoleBox.AppendText("[ Time Elasped: " + Math.Round(timeStamp, 3) + " (s) ]\r\n" + list + "\r\n" + "MAX: " + name + "    "
            + "\r\n\r\n");
        }

        private Single[] getEmoStateArray(EmoState es)
        {

            // Determining which action was taken, and assigning the value
            Single suprisePower = 0, frownPower = 0, smilePower = 0, smirkLPower = 0, smirkRPower = 0, laughPower = 0, clenchPower = 0;
            // Case switching for upper action
            switch (Convert.ToString(es.FacialExpressionGetUpperFaceAction()))
            {
                case "FE_SUPRISE":
                    suprisePower = es.FacialExpressionGetUpperFaceActionPower();
                    break;
                case "FE_FROWN":
                    frownPower = es.FacialExpressionGetUpperFaceActionPower();
                    break;
            }
            // Case switching for upper action
            switch (Convert.ToString(es.FacialExpressionGetLowerFaceAction()))
            {
                case "FE_SMILE":
                    smilePower = es.FacialExpressionGetLowerFaceActionPower();
                    break;
                case "FE_SMIRK_LEFT":
                    smirkLPower = es.FacialExpressionGetLowerFaceActionPower();
                    break;
                case "FE_SMIRK_RIGHT":
                    smirkRPower = es.FacialExpressionGetLowerFaceActionPower();
                    break;
                case "FE_LAUGH":
                    laughPower = es.FacialExpressionGetLowerFaceActionPower();
                    break;
                case "FE_CLENCH":
                    clenchPower = es.FacialExpressionGetLowerFaceActionPower();
                    break;
            }

            // Packing the emoState values into an array
            Single[] emotionArray = { Convert.ToSingle(es.FacialExpressionIsBlink()),
                                      Convert.ToSingle(es.FacialExpressionIsLeftWink()),
                                      Convert.ToSingle(es.FacialExpressionIsRightWink()),
                                      Convert.ToSingle(es.FacialExpressionIsEyesOpen()),
                                      Convert.ToSingle(es.FacialExpressionIsLookingUp()),
                                      Convert.ToSingle(es.FacialExpressionIsLookingDown()),
                                      smilePower, smirkLPower, smirkRPower, laughPower, clenchPower, suprisePower, frownPower
                                    };

            return emotionArray;
        }

        private emotionClass getMaximumEmotionClass()
        {
            Single maxValue = emotionClassArray.Max(x => x.getEmotionValue());
            emotionClass maxEmotionClass = new emotionClass("Neutral", new float[13]);

            if (maxValue > 0 && maxValue >= emotionThreshold)
            {
                maxEmotionClass = emotionClassArray.First(x => x.getEmotionValue() == maxValue);
            }
            return maxEmotionClass;
        }

        private void writeTextExport(string output)
        {
            if (!Directory.Exists(textFolderDirectory))
                Directory.CreateDirectory(textFolderDirectory);

            using (StreamWriter streamWriter = new StreamWriter(textFolderDirectory + "output.txt"))
            {
                streamWriter.Write(output);
            }
        }

        private string concatString(string _log, string log)
        {
            if (_log != "")
                return _log + ", " + log;
            else
                return log;
        }

        private void writeLogExport(string output, string date)
        {
            string time = DateTime.Now.ToString("HH-mm-ss");

            if (!Directory.Exists(logFolderDirectory))
                Directory.CreateDirectory(logFolderDirectory);

            using (StreamWriter streamWriter = new StreamWriter(logFolderDirectory + "log-" + date + ".log", true))
            {
                streamWriter.WriteLine("[" + time + "] " + output);
            }
        }
        
        private string setLogDate()
        {
            string date = DateTime.Now.ToString("d-MMM-yyyy-HH-mm");
            return date;
        }

        private Boolean checkEmotionVariablesForUpdate(Boolean isChanged)
        {
            if (isChanged)
            {
                updateEmotionVariables();
                updateThresholdBar(emotionThreshold);
            }
            return false;
        }
        
        private void updateEmotionVariables()
        {
            object[] readData;
            readData = readPresetDataFromDisk(globalVariables.lastPreset);
            
            emotionClassArray = (emotionClass[])readData[0];
            emotionThreshold = (Single)readData[1];
        }

        private Boolean checkDirectoryVariablesForUpdate(Boolean isChanged)
        {
            if (isChanged)
            {
                updateDirectoryVariables();
                currentDate = setLogDate();
            }
            return false;
        }

        private void updateDirectoryVariables()
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

            toggleQuickAction(isQuickAction, isTextOutput, isLogOutput);
            toggleLogPanel(isLogPanel);

            if (engineConnected == 1)
                engine.Disconnect();
        }

        private void toggleQuickAction(Boolean isQuickAction, Boolean isTextOutput, Boolean isLogOutput)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    toggleQuickAction(isQuickAction, isTextOutput, isLogOutput);
                });
            }
            else
            {
                if (isQuickAction)
                {
                    quickTextExportBtn.Visible = true;
                    quickTextExportBtn.Enabled = true;
                    quickLogExportBtn.Visible = true;
                    quickLogExportBtn.Enabled = true;

                    toggleTextOutputQuickAction(isTextOutput);
                    toggleLogOutputQuickAction(isLogOutput);
                }
                else
                {
                    quickTextExportBtn.Visible = false;
                    quickTextExportBtn.Enabled = false;
                    quickLogExportBtn.Visible = false;
                    quickLogExportBtn.Enabled = false;
                }
            }

        }

        private void toggleTextOutputQuickAction(Boolean isTextOutput)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    toggleTextOutputQuickAction(isTextOutput);
                });
            }
            else
            {
                if (isTextOutput)
                    quickTextExportBtn.BackgroundImage = Properties.Resources.quick_txt_click;
                else
                    quickTextExportBtn.BackgroundImage = Properties.Resources.quick_txt;
            }
        }

        private void toggleLogOutputQuickAction(Boolean isLogOutput)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    toggleLogOutputQuickAction(isLogOutput);
                });
            }
            else
            {
                if (isLogOutput)
                    quickLogExportBtn.BackgroundImage = Properties.Resources.quick_log_click;
                else
                    quickLogExportBtn.BackgroundImage = Properties.Resources.quick_log;
            }
        }

        private void toggleLogPanel(Boolean isLogPanel)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    toggleLogPanel(isLogPanel);
                });
            }
            else
            {
                if (isLogPanel)
                {
                    consoleBtn.Visible = true;
                    consoleBtn.Enabled = true;
                }
                else
                {
                    consoleBtn.Visible = false;
                    consoleBtn.Enabled = false;
                    showEmotionCharts();
                }
            }
        }

        private void showEmotionCharts()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    showEmotionCharts();
                });
            }
            else
            {
                chartBtn.BackgroundImage = Properties.Resources.chartButton_click;
                consoleBtn.BackgroundImage = Properties.Resources.consoleButton;

                joyChartBar.Visible = true;
                loveChartBar.Visible = true;
                wowChartBar.Visible = true;
                sadChartBar.Visible = true;
                fearChartBar.Visible = true;
                angerChartBar.Visible = true;

                joyChartIcon.Visible = true;
                loveChartIcon.Visible = true;
                wowChartIcon.Visible = true;
                sadChartIcon.Visible = true;
                fearChartIcon.Visible = true;
                angerChartIcon.Visible = true;

                thresholdBar.Visible = true;
                thresholdLabel.Visible = true;

                consoleBox.Visible = false;
                consoleBoxButton.Visible = false;

                currentTab = 0;
            }
        }

        private void disconnectedElementsReset()
        {
            int[] zeroChartValueArray = computeChartUpdate(0);

            updateChart(new object[6] { zeroChartValueArray, zeroChartValueArray, zeroChartValueArray,
                zeroChartValueArray, zeroChartValueArray, zeroChartValueArray });

            cloudContent.BackgroundImage = Properties.Resources.i_null;
            signalDisplay.BackgroundImage = Properties.Resources.wifiNone;
            batteryDisplay.BackgroundImage = Properties.Resources.batteryNone;
            notifyIcon.Icon = Properties.Resources.emotivity;
        }

        private void initializeFormLoad()
        {
            // Adding the cloud box and content into the content box
            cloudContentBox.Controls.Add(cloudBox);
            cloudBox.Location = new Point(15, 15);
            cloudBox.Controls.Add(cloudContent);
            cloudContent.Location = new Point((cloudBox.Size.Width - cloudContent.Width) / 2,
                (cloudBox.Size.Height - (cloudContent.Size.Height + (cloudContent.Size.Height / 10))) / 2);

            // Adding the chart bars and icons into the content box
            chartContentBox.Controls.Add(joyChartBar);
            chartContentBox.Controls.Add(joyChartIcon);
            joyChartIcon.Location = new Point(10, 15);
            joyChartBar.Location = new Point(joyChartIcon.Location.X + joyChartIcon.Width + 6, joyChartIcon.Location.Y);
            // joyChartBar.Size = new Size(chartContentBox.Width - 10 - joyChartIcon.Width - 6 - 14, joyChartBar.Height);

            chartContentBox.Controls.Add(loveChartBar);
            chartContentBox.Controls.Add(loveChartIcon);
            loveChartIcon.Location = new Point(10, joyChartIcon.Location.Y + joyChartIcon.Height + 6);
            loveChartBar.Location = new Point(loveChartIcon.Location.X + loveChartIcon.Width + 6, loveChartIcon.Location.Y);
            // loveChartBar.Size = new Size(chartContentBox.Width - 10 - loveChartIcon.Width - 6 - 14, loveChartBar.Height);

            chartContentBox.Controls.Add(wowChartBar);
            chartContentBox.Controls.Add(wowChartIcon);
            wowChartIcon.Location = new Point(10, loveChartIcon.Location.Y + loveChartIcon.Height + 6);
            wowChartBar.Location = new Point(wowChartIcon.Location.X + wowChartIcon.Width + 6, wowChartIcon.Location.Y);
            // wowChartBar.Size = new Size(chartContentBox.Width - 10 - wowChartIcon.Width - 6 - 14, wowChartBar.Height);

            chartContentBox.Controls.Add(sadChartBar);
            chartContentBox.Controls.Add(sadChartIcon);
            sadChartIcon.Location = new Point(10, wowChartIcon.Location.Y + wowChartIcon.Height + 6);
            sadChartBar.Location = new Point(sadChartIcon.Location.X + sadChartIcon.Width + 6, sadChartIcon.Location.Y);
            // sadChartBar.Size = new Size(chartContentBox.Width - 10 - sadChartIcon.Width - 6 - 14, sadChartBar.Height);

            chartContentBox.Controls.Add(fearChartBar);
            chartContentBox.Controls.Add(fearChartIcon);
            fearChartIcon.Location = new Point(10, sadChartIcon.Location.Y + sadChartIcon.Height + 6);
            fearChartBar.Location = new Point(fearChartIcon.Location.X + fearChartIcon.Width + 6, fearChartIcon.Location.Y);
            // fearChartBar.Size = new Size(chartContentBox.Width - 10 - fearChartIcon.Width - 6 - 14, fearChartIcon.Height);

            chartContentBox.Controls.Add(angerChartBar);
            chartContentBox.Controls.Add(angerChartIcon);
            angerChartIcon.Location = new Point(10, fearChartIcon.Location.Y + fearChartIcon.Height + 6);
            angerChartBar.Location = new Point(angerChartIcon.Location.X + angerChartIcon.Width + 6, angerChartIcon.Location.Y);
            // angerChartBar.Size = new Size(chartContentBox.Width - 10 - angerChartIcon.Width - 6 - 14, angerChartIcon.Height);

            chartContentBox.Controls.Add(thresholdBar);
            chartContentBox.Controls.Add(thresholdLabel);
            // thresholdBar.Location = new Point(Convert.ToInt32(10 + joyChartIcon.Width +  6 + ((chartContentBox.Width - 14) * (1 - emotionThreshold))), 5);
            // thresholdLabel.Location = new Point(thresholdBar.Location.X + 5, 5);
            // thresholdLabel.Text = Convert.ToString(emotionThreshold);
            thresholdBar.BringToFront();

            chartContentBox.Controls.Add(consoleBox);
            chartContentBox.Controls.Add(consoleBoxButton);
            consoleBox.Location = new Point(15, 15);
            consoleBoxButton.Location = new Point(consoleBox.Location.X + consoleBox.Width - consoleBoxButton.Width + 2, consoleBox.Location.Y + consoleBox.Height);

            menuPanel.Location = new Point(0, 0);
            menuPanel.Size = new Size(290, menuPanel.Height);

            int[] zeroChartValueArray = computeChartUpdate(0);
            updateEmotionVariables();

            updateChart(new object[6] { zeroChartValueArray, zeroChartValueArray, zeroChartValueArray,
                zeroChartValueArray, zeroChartValueArray, zeroChartValueArray });

            updateThresholdBar(emotionThreshold);
            updateDirectoryVariables();

            ContextMenu notifyIconContextMenu = new ContextMenu();
            notifyIconContextMenu.MenuItems.Add("View", normailiseWindow);
            notifyIconContextMenu.MenuItems.Add("Exit", exitWindow);
            notifyIcon.ContextMenu = notifyIconContextMenu;
        }

        private void initializeEngine() {

            Console.WriteLine("// ---------- Program started ---------- //");
            
            engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected += new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.EmoStateUpdated += new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
            engine.FacialExpressionEmoStateUpdated += new EmoEngine.FacialExpressionEmoStateUpdatedEventHandler(engine_FacialExpressionEmoStateUpdated);

            while (true)
            {
                // Update variables once customisation is invoked
                globalVariables.variablesChanged = checkEmotionVariablesForUpdate(globalVariables.variablesChanged);
                globalVariables.settingsChanged = checkDirectoryVariablesForUpdate(globalVariables.settingsChanged);

                // Handles the case where the engine is offline
                // Pooling for connection
                if (engineConnected == 0)
                {

                    try
                    {

                        // Try to connect the Emoengine
                        if (isUseEmotivComposer)
                            engine.RemoteConnect("127.0.0.1", 1726);

                        // Try to connect EPOC
                        if (!isUseEmotivComposer)
                            engine.Connect();

                        // Set the first date after successful connection
                        currentDate = setLogDate();
                    }
                    catch /*(Exception exceptionError)*/  { /*Console.WriteLine(exceptionError);*/ }

                }
                else
                {
                    try
                    {
                        // Get the events from the engine
                        engine.ProcessEvents();

                    }
                    catch /*(Exception exceptionError)*/  { /*Console.WriteLine(exceptionError);*/ }
                }

                if (Application.OpenForms.Count == 0)
                {
                    // Disconnect the engine if the engine is connected
                    if (engineConnected == 1)
                        engine.Disconnect();

                    // Break on form closed
                    break;
                }

                if (isInvokeDelay)
                    Thread.Sleep(invokeDelay);
                else
                    Thread.Sleep(10);
                }
        }

        public emotivityMainForm()
        {
            InitializeComponent();

            initializeFormLoad();
            this.Show();
        }
        
        private void emotivityMainForm_Load(object sender, EventArgs e)
        {
            Thread initializeThread = new Thread(initializeEngine);
            initializeThread.Start();
        }

        private void emotivityMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeOptionsDataToDisk(isTextOutput, isLogOutput, textFolderDirectory, logFolderDirectory,
                isLogPanel, isQuickAction, isInvokeDelay, invokeDelay, isUseEmotivComposer);
        }

        private void emotivityMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void chartBtn_MouseEnter(object sender, EventArgs e)
        {
            if (currentTab == 1)
            {
                chartBtn.BackgroundImage = Properties.Resources.chartButton_inv;
            }
        }

        private void chartBtn_MouseLeave(object sender, EventArgs e)
        {
            if (currentTab == 1)
            {
                chartBtn.BackgroundImage = Properties.Resources.chartButton;
            }
        }

        private void chartBtn_MouseDown(object sender, MouseEventArgs e)
        {
            showEmotionCharts();
        }

        private void consoleBtn_MouseEnter(object sender, EventArgs e)
        {
            if (currentTab == 0)
            {
                consoleBtn.BackgroundImage = Properties.Resources.consoleButton_inv;
            }
        }

        private void consoleBtn_MouseLeave(object sender, EventArgs e)
        {
            if (currentTab == 0)
            {
                consoleBtn.BackgroundImage = Properties.Resources.consoleButton;
            }
        }

        private void consoleBtn_MouseDown(object sender, MouseEventArgs e)
        {
            consoleBtn.BackgroundImage = Properties.Resources.consoleButton_click;
            chartBtn.BackgroundImage = Properties.Resources.chartButton;

            joyChartBar.Visible = false;
            loveChartBar.Visible = false;
            wowChartBar.Visible = false;
            sadChartBar.Visible = false;
            fearChartBar.Visible = false;
            angerChartBar.Visible = false;

            joyChartIcon.Visible = false;
            loveChartIcon.Visible = false;
            wowChartIcon.Visible = false;
            sadChartIcon.Visible = false;
            fearChartIcon.Visible = false;
            angerChartIcon.Visible = false;

            thresholdBar.Visible = false;
            thresholdLabel.Visible = false;

            consoleBox.Visible = true;
            consoleBoxButton.Visible = true;

            currentTab = 1;
        }

        private void consoleBoxButton_MouseEnter(object sender, EventArgs e)
        {
            consoleBoxButton.BackgroundImage = Properties.Resources.clearButton_inv;
        }

        private void consoleBoxButton_MouseLeave(object sender, EventArgs e)
        {
            consoleBoxButton.BackgroundImage = Properties.Resources.clearButton;
        }

        private void consoleBoxButton_MouseDown(object sender, MouseEventArgs e)
        {
            consoleBoxButton.BackgroundImage = Properties.Resources.clearButton_click;
            consoleBtnTimer.Start();
            consoleBox.Text = "";
        }
        
        private void consoleBtnTimer_Tick(object sender, EventArgs e)
        {
            consoleBtnTimer.Stop();
            consoleBoxButton.BackgroundImage = Properties.Resources.clearButton_inv;
        }

        private void menuBtn_MouseDown(object sender, MouseEventArgs e)
        {
            menuBtnTimer.Start();
        }

        private void menuBtnTimer_Tick(object sender, EventArgs e)
        {
            int maxCounter = 24;
            if (menuBtnCounter <= maxCounter)
            {
                switch (menuBtnState)
                {
                    case 0:
                        menuPanel.Size = new Size(295 + (5 * menuBtnCounter) + 5, menuPanel.Height);
                        break;
                    case 1:
                        menuPanel.Size = new Size(415 - (5 * menuBtnCounter) - 5, menuPanel.Height);
                        break;
                }
            }
            if (menuBtnCounter > maxCounter)
            {
                switch (menuBtnState)
                {
                    case 0:
                        menuBtnState = 1;
                        menuBtnCounter = 0;
                        break;
                    case 1:
                        menuBtnState = 0;
                        menuBtnCounter = 0;
                        break;
                }
                menuBtnTimer.Stop();
            }
            menuBtnCounter++;
        }

        private void settingBtn_MouseDown(object sender, MouseEventArgs e)
        {
            settingBtn.BackgroundImage = Properties.Resources.settingButton_click;
            settingBtnTimer.Start();

            emotivitySettingScreen settingScreen = new emotivitySettingScreen();
            settingScreen.ShowDialog();
        }

        private void settingBtn_MouseEnter(object sender, EventArgs e)
        {
            settingBtn.BackgroundImage = Properties.Resources.settingButton_inv;
        }

        private void settingBtn_MouseLeave(object sender, EventArgs e)
        {
            settingBtn.BackgroundImage = Properties.Resources.settingButton;
        }
        
        private void settingBtnTimer_Tick(object sender, EventArgs e)
        {
            settingBtnTimer.Stop();
            settingBtn.BackgroundImage = Properties.Resources.settingButton;
        }

        private void customiseBtn_MouseDown(object sender, MouseEventArgs e)
        {
            customiseBtn.BackgroundImage = Properties.Resources.preferenceButton_click;
            customiseBtnTimer.Start();

            emotivityCustomiseScreen customiseScreen = new emotivityCustomiseScreen();
            customiseScreen.ShowDialog();
        }

        private void customiseBtn_MouseEnter(object sender, EventArgs e)
        {
            customiseBtn.BackgroundImage = Properties.Resources.preferenceButton_inv;
        }

        private void customiseBtn_MouseLeave(object sender, EventArgs e)
        {
            customiseBtn.BackgroundImage = Properties.Resources.preferenceButton;
        }

        private void customiseBtnTimer_Tick(object sender, EventArgs e)
        {
            customiseBtnTimer.Stop();
            customiseBtn.BackgroundImage = Properties.Resources.preferenceButton;
        }

        private void aboutBtn_MouseDown(object sender, MouseEventArgs e)
        {
            aboutBtn.BackgroundImage = Properties.Resources.aboutButton_click;
            aboutBtnTimer.Start();

            emotivityAboutScreen aboutScreen = new emotivityAboutScreen();
            aboutScreen.ShowDialog();
        }

        private void aboutBtn_MouseEnter(object sender, EventArgs e)
        {
            aboutBtn.BackgroundImage = Properties.Resources.aboutButton_inv;
        }

        private void aboutBtn_MouseLeave(object sender, EventArgs e)
        {
            aboutBtn.BackgroundImage = Properties.Resources.aboutButton;
        }

        private void aboutBtnTimer_Tick(object sender, EventArgs e)
        {
            aboutBtnTimer.Stop();
            aboutBtn.BackgroundImage = Properties.Resources.aboutButton;
        }

        private void quickTextExportBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (isTextOutput)
            {
                isTextOutput = false;
                quickTextExportBtn.BackgroundImage = Properties.Resources.quick_txt;
            }
            else
            {
                isTextOutput = true;
                quickTextExportBtn.BackgroundImage = Properties.Resources.quick_txt_click;
            }
        }

        private void quickTextExportBtn_MouseEnter(object sender, EventArgs e)
        {
            if (!isTextOutput)
            {
                quickTextExportBtn.BackgroundImage = Properties.Resources.quick_txt_inv;
            }
        }

        private void quickTextExportBtn_MouseLeave(object sender, EventArgs e)
        {
            if (!isTextOutput)
            {
                quickTextExportBtn.BackgroundImage = Properties.Resources.quick_txt;
            }
        }

        private void quickLogExportBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (isLogOutput)
            {
                isLogOutput = false;
                quickLogExportBtn.BackgroundImage = Properties.Resources.quick_log;
            }
            else
            {
                isLogOutput = true;
                quickLogExportBtn.BackgroundImage = Properties.Resources.quick_log_click;
            }
        }

        private void quickLogExportBtn_MouseEnter(object sender, EventArgs e)
        {
            if (!isLogOutput)
            {
                quickLogExportBtn.BackgroundImage = Properties.Resources.quick_log_inv;
            }
        }

        private void quickLogExportBtn_MouseLeave(object sender, EventArgs e)
        {
            if (!isLogOutput)
            {
                quickLogExportBtn.BackgroundImage = Properties.Resources.quick_log;
            }
        }

        private void emotivityMainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon.Visible = true;
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        private void normailiseWindow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            emotivityMainForm_Resize(sender, e);
        }

        private void exitWindow(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }
    }
}
