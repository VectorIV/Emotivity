using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace emotivityMain
{
    class emotionClass
    {
        // Unique modifiers for each new class
        private Single[] emotionModifiers;
        private string emotionName;
        private Single emotionValue = 0;

        // Constructor
        public emotionClass(string name, Single[] array)
        {
            emotionModifiers = array;
            emotionName = name;
        }

        // Set values of the class
        public void setEmotionModifiers(Single[] array)
        {
            emotionModifiers = array;
        }

        public void setEmotionModifier(Single value, int index)
        {
            emotionModifiers[index] = value;
        }

        public void setEmotionName(string name)
        {
            emotionName = name;
        }

        // Get values from the class
        public Single[] getEmotionModifiers()
        {
            return emotionModifiers;
        }

        public string getEmotionName()
        {
            return emotionName;
        }

        public Single getEmotionValue()
        {
            return emotionValue;
        }

        // Computing the class value
        public Single computeValue(Single[] emotionArray)
        {
            Single computingValue = 0;
            Single modifiersSumValue = 0;

            for (int i = 0; i < emotionArray.Length; i++)
            {
                computingValue += emotionModifiers[i] * emotionArray[i];
                //Console.WriteLine(emotionModifiers[i]+"x"+emotionArray[i]);
                if ((emotionModifiers[i] * emotionArray[i]) > 0)
                {
                    modifiersSumValue += 1;
                }
            }
            //Console.WriteLine("----------------------");
            if (modifiersSumValue == 0)
                modifiersSumValue = 1;
            // Dividing gives us values in static range across all classes despite of their modifiers
            // MIN: 0 MAX: 1
            if (computingValue > 0) { computingValue = computingValue / modifiersSumValue; }
            else { computingValue = 0; }
            computingValue = Convert.ToSingle(Math.Round(computingValue, 2));
            
            emotionValue = computingValue;
            return computingValue;
        }
    }
    
    static class emotivityBase
    {
        static private Properties.Settings globalVariables = Properties.Settings.Default;

        static public string emptyComboxText = "<New Preset>";

        static public emotionClass[] getDefaultEmotionClassArray()
        {
            emotionClass[] emotionClassArray = new emotionClass[6];
            // Initialising emotionClass
            emotionClassArray[0] = new emotionClass("Joy", new Single[13] { 0, 0, 0, 0, 0, 0, 1, 0.2f, 0.2f, 0.5f, 0.1f, 0.3f, -0.8f });
            emotionClassArray[1] = new emotionClass("Love", new Single[13] { 0.2f, 0.2f, 0.2f, 0, 0, 0.3f, 0.5f, 0.2f, 0.2f, 0.8f, 0.1f, 0.8f, -1 });
            emotionClassArray[2] = new emotionClass("Surprise", new Single[13] { 0.4f, 0, 0, 0, 0.5f, 0, 0.2f, 0.2f, 0.2f, 0.4f, 0.3f, 1, 0.3f });
            emotionClassArray[3] = new emotionClass("Sad", new Single[13] { 0, 0, 0, 0, 0, 0.6f, 0.2f, 0.1f, 0.1f, 0.3f, 0.3f, 0.6f, 0.1f });
            emotionClassArray[4] = new emotionClass("Fear", new Single[13] { 0, 0, 0, -0.2f, 0, 0, 0.5f, 0, 0, 0, 0.8f, 0.6f, 0.5f });
            emotionClassArray[5] = new emotionClass("Anger", new Single[13] { 0.2f, -0.2f, 0.2f, 0.2f, -0.2f, -0.2f, -0.8f, 0.2f, 0.2f, 0.2f, 1, 0, 1 });
            return emotionClassArray;
        }

        static public Single getDefaultEmotionThreshold()
        {
            Single emotionThreshold = 0.25f;

            return emotionThreshold;
        }

        static public int[] getInvokeDelayRange()
        {
            return new int[] {10, 1000};
        }

        static public object[] readPresetDataFromDisk(string name)
        {
            int currentClassIndex = -1;

            string currentDirectory = Directory.GetCurrentDirectory()
            + "\\UserData\\Presets\\" + name + "\\";

            string emotionName = "";
            emotionClass[] emotionClassArray = new emotionClass[6];
            Single emotionThreshold = 0;

            if (name != emptyComboxText)
            {
                try
                {
                    using (XmlReader root = XmlReader.Create(currentDirectory + "data.xml"))
                    {
                        while (root.Read())
                        {
                            if (root.IsStartElement())
                            {
                                switch (root.Name)
                                {
                                    case "preset":
                                        // Console.WriteLine("Start <preset> element.");

                                        if (root.GetAttribute("threshold") != null)
                                            emotionThreshold = Convert.ToSingle(root.GetAttribute("threshold"));
                                        break;
                                    case "emtionclass":
                                        // Console.WriteLine("Start <emtionclass> element.");

                                        if (root.GetAttribute("name") != null) emotionName = root.GetAttribute("name");
                                        currentClassIndex++;
                                        break;
                                    case "modifiers":
                                        // Console.WriteLine("Start <modifiers> element.");

                                        // Declaring this outside somehow fucks things up
                                        Single[] emotionModifiersArray = new Single[13];

                                        if (root.GetAttribute("blink") != null)
                                            emotionModifiersArray[0] = Convert.ToSingle(root.GetAttribute("blink"));
                                        if (root.GetAttribute("leftWink") != null)
                                            emotionModifiersArray[1] = Convert.ToSingle(root.GetAttribute("leftWink"));
                                        if (root.GetAttribute("rightWink") != null)
                                            emotionModifiersArray[2] = Convert.ToSingle(root.GetAttribute("rightWink"));
                                        if (root.GetAttribute("eyesOpen") != null)
                                            emotionModifiersArray[3] = Convert.ToSingle(root.GetAttribute("eyesOpen"));
                                        if (root.GetAttribute("lookUp") != null)
                                            emotionModifiersArray[4] = Convert.ToSingle(root.GetAttribute("lookUp"));
                                        if (root.GetAttribute("lookDown") != null)
                                            emotionModifiersArray[5] = Convert.ToSingle(root.GetAttribute("lookDown"));
                                        if (root.GetAttribute("smile") != null)
                                            emotionModifiersArray[6] = Convert.ToSingle(root.GetAttribute("smile"));
                                        if (root.GetAttribute("leftSmirk") != null)
                                            emotionModifiersArray[7] = Convert.ToSingle(root.GetAttribute("leftSmirk"));
                                        if (root.GetAttribute("rightSmirk") != null)
                                            emotionModifiersArray[8] = Convert.ToSingle(root.GetAttribute("rightSmirk"));
                                        if (root.GetAttribute("laugh") != null)
                                            emotionModifiersArray[9] = Convert.ToSingle(root.GetAttribute("laugh"));
                                        if (root.GetAttribute("teethClench") != null)
                                            emotionModifiersArray[10] = Convert.ToSingle(root.GetAttribute("teethClench"));
                                        if (root.GetAttribute("browRaise") != null)
                                            emotionModifiersArray[11] = Convert.ToSingle(root.GetAttribute("browRaise"));
                                        if (root.GetAttribute("browFurrow") != null)
                                            emotionModifiersArray[12] = Convert.ToSingle(root.GetAttribute("browFurrow"));

                                        emotionClassArray[currentClassIndex] = new emotionClass(emotionName, emotionModifiersArray);
                                        break;
                                }
                            }
                        }
                    }

                    return new object[] { emotionClassArray, emotionThreshold };
                }
                catch
                {
                    emotionClassArray = getDefaultEmotionClassArray();
                    emotionThreshold = getDefaultEmotionThreshold();
                    globalVariables.lastPreset = emptyComboxText;
                    globalVariables.Save();

                    return new object[] { emotionClassArray, emotionThreshold };
                }
            }
            else
            {
                
                emotionClassArray = getDefaultEmotionClassArray();
                emotionThreshold = getDefaultEmotionThreshold();

                return new object[] { emotionClassArray, emotionThreshold };
            }
        }

        static public object[] getDefaultOptionData()
        {
            Boolean b_textOutput = false;
            Boolean b_logOutput = false;
            string s_textOutputPath = Directory.GetCurrentDirectory() + "\\Output\\Dump\\";
            string s_logOutputPath = Directory.GetCurrentDirectory() + "\\Output\\Log\\";

            Boolean b_invokeDelay = false;
            int i_invokeDelayAmount = 10;

            Boolean b_logPanel = false;
            Boolean b_quickAction = false;
            
            Boolean b_useEmotivComposer = false;

            return new object[] { new Boolean[] { b_textOutput, b_logOutput, b_logPanel, b_quickAction, b_invokeDelay, b_useEmotivComposer }
            , new string[] { s_textOutputPath, s_logOutputPath }, new int[] { i_invokeDelayAmount } };
        }

        static public object[] readOptionDataFromDisk()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory() + "\\UserData\\";

            Boolean[] booleanArray = new Boolean[6];
            string[] stringArray = new string[2];
            int[] integerArray = new int[1];

            object[] defaultObject = getDefaultOptionData();
            Boolean[] defaultBooleanArray = (Boolean[])defaultObject[0];
            string[] defaultStringArray = (string[])defaultObject[1];
            int[] defaultIntegerArray = (int[])defaultObject[2];

            int[] invokeRange = getInvokeDelayRange();

            try
            {
                using (XmlReader root = XmlReader.Create(currentDirectory + "options.xml"))
                {
                    IXmlLineInfo xmlInfo = (IXmlLineInfo)root;
                    int lineNumber = xmlInfo.LineNumber;

                    while (root.Read())
                    {
                        if (root.IsStartElement())
                        {
                            switch (root.Name)
                            {
                                case "options":
                                    break;
                                case "output":
                                    // Console.WriteLine("Start <output> element.");

                                    if (root.GetAttribute("b_textOutput") != null) {
                                        try { booleanArray[0] = Convert.ToBoolean(root.GetAttribute("b_textOutput")); }
                                        catch {
                                            displayIOMessage("b_textOutput", lineNumber);
                                            booleanArray[0] = defaultBooleanArray[0];
                                        }
                                    }
                                    else
                                        booleanArray[0] = defaultBooleanArray[0];
                                    if (root.GetAttribute("b_logOutput") != null) {
                                        try { booleanArray[1] = Convert.ToBoolean(root.GetAttribute("b_logOutput")); }
                                        catch {
                                            displayIOMessage("b_logOutput", lineNumber);
                                            booleanArray[1] = defaultBooleanArray[1];
                                        }
                                    }
                                    else
                                        booleanArray[1] = defaultBooleanArray[1];
                                    if (root.GetAttribute("s_textOutputPath") != null) {
                                        try { stringArray[0] = root.GetAttribute("s_textOutputPath"); }
                                        catch {
                                            displayIOMessage("s_textOutputPath", lineNumber);
                                            stringArray[0] = defaultStringArray[0];
                                        }
                                    }
                                    else
                                        stringArray[0] = defaultStringArray[0];
                                    if (root.GetAttribute("s_logOutputPath") != null) {
                                        try { stringArray[1] = root.GetAttribute("s_logOutputPath"); }
                                        catch {
                                            displayIOMessage("s_logOutputPath", lineNumber);
                                            stringArray[1] = defaultStringArray[1];
                                        }
                                    }
                                    else
                                        stringArray[1] = defaultStringArray[1];
                                    
                                    break;
                                case "system":

                                    if (root.GetAttribute("b_invokeDelay") != null) {
                                        try { booleanArray[4] = Convert.ToBoolean(root.GetAttribute("b_invokeDelay")); }
                                        catch
                                        {
                                            displayIOMessage("b_invokeDelay", lineNumber);
                                            booleanArray[4] = defaultBooleanArray[4];
                                        }
                                    }
                                    else
                                        booleanArray[4] = defaultBooleanArray[4];
                                    if (root.GetAttribute("i_invokeDelayAmount") != null
                                        && Convert.ToInt32(root.GetAttribute("i_invokeDelayAmount")) >= invokeRange[0]
                                        && Convert.ToInt32(root.GetAttribute("i_invokeDelayAmount")) <= invokeRange[1]) {
                                        try { integerArray[0] = Convert.ToInt32(root.GetAttribute("i_invokeDelayAmount")); }
                                        catch {
                                            displayIOMessage("i_invokeDelayAmount", lineNumber);
                                            integerArray[0] = defaultIntegerArray[0];
                                        }
                                    }
                                    else
                                        integerArray[0] = defaultIntegerArray[0];
                                    if (root.GetAttribute("b_useEmotivComposer") != null) {
                                        try { booleanArray[5] = Convert.ToBoolean(root.GetAttribute("b_useEmotivComposer")); }
                                        catch
                                        {
                                            displayIOMessage("b_useEmotivComposer", lineNumber);
                                            booleanArray[5] = defaultBooleanArray[5];
                                        }
                                    }
                                    else
                                        booleanArray[5] = defaultBooleanArray[5];
                                    
                                    break;
                                case "misc":
                                    
                                    if (root.GetAttribute("b_logPanel") != null) {
                                        try {
                                            booleanArray[2] = Convert.ToBoolean(root.GetAttribute("b_logPanel")); }
                                        catch
                                        {
                                            displayIOMessage("b_logPanel", lineNumber);
                                            booleanArray[2] = defaultBooleanArray[2];
                                        }
                                    }
                                    else
                                        booleanArray[2] = defaultBooleanArray[2];
                                    if (root.GetAttribute("b_quickAction") != null) {
                                        try { booleanArray[3] = Convert.ToBoolean(root.GetAttribute("b_quickAction")); }
                                        catch
                                        {
                                            displayIOMessage("b_quickAction", lineNumber);
                                            booleanArray[3] = defaultBooleanArray[3];
                                        }
                                    }
                                    else
                                        booleanArray[3] = defaultBooleanArray[3];

                                    break;
                            }
                        }
                    }
                }

                return new object[] { booleanArray, stringArray, integerArray };
            }
            catch
            {
                writeOptionsDataToDisk(defaultBooleanArray[0], defaultBooleanArray[1], defaultStringArray[0],
                    defaultStringArray[1], defaultBooleanArray[2], defaultBooleanArray[3], defaultBooleanArray[4], defaultIntegerArray[0], defaultBooleanArray[5]);

                return defaultObject;
            }
        }

        static public void writeOptionsDataToDisk(Boolean isTextOutput, Boolean isLogOutput, string textFolderDirectory, string logFolderDirectory, Boolean isLogPanel, Boolean isQuickAction, Boolean isInvokeDelay, int invokeDelay, Boolean isUseEmotivComposer)
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory() + "/UserData/";

            if (!Directory.Exists(currentDirectory))
                Directory.CreateDirectory(currentDirectory);

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(currentDirectory + "options.xml", xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("settings");
                xmlWriter.WriteStartElement("output");
                xmlWriter.WriteAttributeString("b_textOutput", Convert.ToString(isTextOutput));
                xmlWriter.WriteAttributeString("b_logOutput", Convert.ToString(isLogOutput));
                xmlWriter.WriteAttributeString("s_textOutputPath", textFolderDirectory);
                xmlWriter.WriteAttributeString("s_logOutputPath", logFolderDirectory);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("system");
                xmlWriter.WriteAttributeString("b_invokeDelay", Convert.ToString(isInvokeDelay));
                xmlWriter.WriteAttributeString("i_invokeDelayAmount", Convert.ToString(invokeDelay));
                xmlWriter.WriteAttributeString("b_useEmotivComposer", Convert.ToString(isUseEmotivComposer));
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("misc");
                xmlWriter.WriteAttributeString("b_logPanel", Convert.ToString(isLogPanel));
                xmlWriter.WriteAttributeString("b_quickAction", Convert.ToString(isQuickAction));
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }

        static public void displayIOMessage(string attributeName, int lineNumber)
        {
            MessageBox.Show("An error has occured at line '" + lineNumber + "' for attribute '"+attributeName+ "'. The program will now use the default setting for this attribute.\n Please re-enter the desired value.", "XML I/O Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
