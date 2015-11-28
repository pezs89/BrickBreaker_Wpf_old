using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using System.Windows.Threading;
using System.IO;
using System.Windows.Controls;

namespace BrickBreaker
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        #region Fields

        OptionsSettings optionsSettings = new OptionsSettings("", "", "", "", "", 0, false, false, 0, false);
        // OptionSettings object that handles the changes in the options window.
        DispatcherTimer LabelHide = new DispatcherTimer();
        // A timer.

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Options"/> class.
        /// </summary>
        public Options()
        {
            InitializeComponent();
            LoadValuesFromFile();

            #region Timer

            LabelHide.Interval = TimeSpan.FromSeconds(3);
            LabelHide.Tick += LabelHide_Tick;
            // Sets the timespan to 3 seconds.

            #endregion Timer
        }

        #endregion Constructors

        #region Methods

        #region GuiElements

        /// <summary>
        /// Returns to the main window.
        /// </summary>
        private void ReturnToMainWindow()
        {
            MainWindow parentWindow = new MainWindow();
            parentWindow.Show();
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnBack control witch returns to the main window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ReturnToMainWindow();
        }

        /// <summary>
        /// Hides the label after the designated timespan.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void LabelHide_Tick(object sender, EventArgs e)
        {
            SettingsSaved_Res.Content = "";
            SettingsSaved.Content = "";
            LabelHide.Stop();
        }

        /// <summary>
        /// Handles the Click event of the btnAppChng control witch saves the changes made in the options window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAppChng_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load("OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml. 
                
                if (fromXml.Single().Element("leftkey").Value != optionsSettings.LeftKey)
                {
                    fromXml.Single().Element("leftkey").Value = optionsSettings.LeftKey;
                    SettingsSaved.Content = "Settings has been updated!"; 
                } 
                if (fromXml.Single().Element("rightkey").Value != optionsSettings.RightKey)
                {
                    fromXml.Single().Element("rightkey").Value = optionsSettings.RightKey;
                    SettingsSaved.Content = "Settings has been updated!";
                }
                if (fromXml.Single().Element("pausekey").Value != optionsSettings.PauseKey) 
                {
                    fromXml.Single().Element("pausekey").Value = optionsSettings.PauseKey;
                    SettingsSaved.Content = "Settings has been updated!";
                }
                if (fromXml.Single().Element("resolution").Value != optionsSettings.GraphicsResolution)
                {
                    fromXml.Single().Element("resolution").Value = optionsSettings.GraphicsResolution;
                    SettingsSaved_Res.Content = "Settings has been updated!";
                }
                if (fromXml.Single().Element("firekey").Value != optionsSettings.FireKey)
                {
                    fromXml.Single().Element("firekey").Value = optionsSettings.FireKey;
                    SettingsSaved.Content = "Settings has been updated!";
                }
                if (fromXml.Single().Element("mouse").Value != optionsSettings.MouseIsOn.ToString())
                {
                    fromXml.Single().Element("mouse").Value = optionsSettings.MouseIsOn.ToString();
                    SettingsSaved.Content = "Settings has been updated!";
                }
                if (fromXml.Single().Element("keyboard").Value != optionsSettings.KeyboardIsOn.ToString())
                {
                    fromXml.Single().Element("keyboard").Value = optionsSettings.KeyboardIsOn.ToString();
                    SettingsSaved.Content = "Settings has been updated!";
                }
                if (fromXml.Single().Element("sound").Value != optionsSettings.SoundIsOn.ToString())
                {
                    fromXml.Single().Element("sound").Value = optionsSettings.SoundIsOn.ToString();
                    SettingsSaved.Content = "Settings has been updated!";
                }
                // If the values from the xml doesn't match the values from the OptionSettings object, than refresh the the xml values.

                settingsFromXml.Save("OptionsSettings.xml");
                // Save the changes in the values of the xml.

                LabelHide.Start();
                // Hides the label after the designated timespan.
            }
            catch
            {

            }
        }

        /// <summary>
        /// Handles the Click event of the LeftKeyChangeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void LeftKeyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            leftMovetxtbox.Text = "";
            leftMovetxtbox.IsEnabled = true;
            // Enales and empty the textbox.

            LeftKeyChangeButton.IsEnabled = false;
            RightKeyChangeButton.IsEnabled = false;
            PauseKeyChangeButton.IsEnabled = false;
            FireKeyChangeButton.IsEnabled = false;
            // Disables simultanious changes.
        }

        /// <summary>
        /// Handles the Click event of the RightKeyChangeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RightKeyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            rightMovetxtbox.Text = "";
            rightMovetxtbox.IsEnabled = true;
            // Enales and empty the textbox.

            LeftKeyChangeButton.IsEnabled = false;
            RightKeyChangeButton.IsEnabled = false;
            PauseKeyChangeButton.IsEnabled = false;
            FireKeyChangeButton.IsEnabled = false;
            // Disables simultanious changes.
        }

        /// <summary>
        /// Handles the Click event of the PauseKeyChangeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PauseKeyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            pausetxtbox.Text = "";
            pausetxtbox.IsEnabled = true;
            // Enales and empty the textbox.

            LeftKeyChangeButton.IsEnabled = false;
            RightKeyChangeButton.IsEnabled = false;
            PauseKeyChangeButton.IsEnabled = false;
            FireKeyChangeButton.IsEnabled = false;
            // Disables simultanious changes.
        }

        /// <summary>
        /// Handles the Click event of the FireKeyChangeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FireKeyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            firetxtbox.Text = "";
            firetxtbox.IsEnabled = true;
            // Enales and empty the textbox.

            LeftKeyChangeButton.IsEnabled = false;
            RightKeyChangeButton.IsEnabled = false;
            PauseKeyChangeButton.IsEnabled = false;
            FireKeyChangeButton.IsEnabled = false;
            // Disables simultanious changes.
        }

        /// <summary>
        /// Handles the KeyUp event of the leftMovetxtbox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void leftMovetxtbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (CheckForMatch(LookUpKey(e.Key), "left"))
            {
                // If match was found then send message and restore previous value.
                SettingsSaved.Content = "Key is already assigned.";

                LabelHide.Start();
                // Hides the label after the designated timespan.
            }
            else
            {
                // If no match was found then refresh the OptionSettings object value.
                optionsSettings.LeftKey = LookUpKey(e.Key);
            }
            leftMovetxtbox.Text = optionsSettings.LeftKey;
            // Searches for match and handles the input.

            leftMovetxtbox.IsEnabled = false;
            LeftKeyChangeButton.IsEnabled = true;
            RightKeyChangeButton.IsEnabled = true;
            PauseKeyChangeButton.IsEnabled = true;
            FireKeyChangeButton.IsEnabled = true;
            // Sets the enable fields.
        }

        /// <summary>
        /// Handles the KeyUp event of the rightMovetxtbox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void rightMovetxtbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (CheckForMatch(LookUpKey(e.Key), "right"))
            {
                // If match was found then send message and restore previous value.
                SettingsSaved.Content = "Key is already assigned.";

                LabelHide.Start();
                // Hides the label after the designated timespan.
            }
            else
            {
                // If no match was found then refresh the OptionSettings object value.
                optionsSettings.RightKey = LookUpKey(e.Key);
            }
            rightMovetxtbox.Text = optionsSettings.RightKey;
            // Searches for match and handles the input.

            rightMovetxtbox.IsEnabled = false;
            LeftKeyChangeButton.IsEnabled = true;
            RightKeyChangeButton.IsEnabled = true;
            PauseKeyChangeButton.IsEnabled = true;
            FireKeyChangeButton.IsEnabled = true;
            // Sets the enable fields.
        }

        /// <summary>
        /// Handles the KeyUp event of the pausetxtbox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void pausetxtbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (CheckForMatch(LookUpKey(e.Key), "pause"))
            {
                // If match was found then send message and restore previous value.
                SettingsSaved.Content = "Key is already assigned.";

                LabelHide.Start();
                // Hides the label after the designated timespan.
            }
            else
            {
                // If no match was found then refresh the OptionSettings object value.
                optionsSettings.PauseKey = LookUpKey(e.Key);
            }
            pausetxtbox.Text = optionsSettings.PauseKey;
            // Searches for match and handles the input.

            pausetxtbox.IsEnabled = false;
            LeftKeyChangeButton.IsEnabled = true;
            RightKeyChangeButton.IsEnabled = true;
            PauseKeyChangeButton.IsEnabled = true;
            FireKeyChangeButton.IsEnabled = true;
            // Sets the enable fields.
        }

        /// <summary>
        /// Handles the KeyUp event of the firetxtbox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void firetxtbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (CheckForMatch(LookUpKey(e.Key), "fire"))
            {
                // If match was found then send message and restore previous value.
                SettingsSaved.Content = "Key is already assigned.";

                LabelHide.Start();
                // Hides the label after the designated timespan.
            }
            else
            {
                // If no match was found then refresh the OptionSettings object value.
                optionsSettings.FireKey = LookUpKey(e.Key);
            }
            firetxtbox.Text = optionsSettings.FireKey;
            // Searches for match and handles the input.

            firetxtbox.IsEnabled = false;
            LeftKeyChangeButton.IsEnabled = true;
            RightKeyChangeButton.IsEnabled = true;
            PauseKeyChangeButton.IsEnabled = true;
            FireKeyChangeButton.IsEnabled = true;
            // Sets the enable fields.
        }

        /// <summary>
        /// Looks up the input key.
        /// </summary>
        /// <param name="inputKey">The inputKey.</param>
        /// <returns></returns>
        private string LookUpKey(Key inputKey)
        {
            string retVal;
            // The return value.

            switch (inputKey)
            {
                case Key.Left:
                    retVal = "Left";
                    break;
                case Key.Right:
                    retVal = "Right";
                    break;
                case Key.Up:
                    retVal = "Up";
                    break;
                case Key.Down:
                    retVal = "Down";
                    break;
                case Key.Enter:
                    // Also known as Key.Return.
                    retVal = "Enter";
                    break;
                case Key.Space:
                    retVal = "Space";
                    break;
                case Key.LeftShift:
                    retVal = "LeftShift";
                    break;
                case Key.RightShift:
                    retVal = "RightShift";
                    break;
                case Key.LeftCtrl:
                    retVal = "LeftCtrl";
                    break;
                case Key.RightCtrl:
                    retVal = "RightCtrl";
                    break;
                case Key.LeftAlt:
                    retVal = "LeftAlt";
                    break;
                case Key.RightAlt:
                    retVal = "RightAlt";
                    break;
                case Key.Tab:
                    retVal = "Tab";
                    break;
                case Key.F1:
                    retVal = "F1";
                    break;
                case Key.F2:
                    retVal = "F2";
                    break;
                case Key.F3:
                    retVal = "F3";
                    break;
                case Key.F4:
                    retVal = "F4";
                    break;
                case Key.F5:
                    retVal = "F5";
                    break;
                case Key.F6:
                    retVal = "F6";
                    break;
                case Key.F7:
                    retVal = "F7";
                    break;
                case Key.F8:
                    retVal = "F8";
                    break;
                case Key.F9:
                    retVal = "F9";
                    break;
                case Key.F10:
                    retVal = "F10";
                    break;
                case Key.F11:
                    retVal = "F11";
                    break;
                case Key.F12:
                    retVal = "F12";
                    break;
                case Key.PageUp:
                    retVal = "PageUp";
                    break;
                case Key.PageDown:
                    retVal = "PageDown";
                    break;
                case Key.Home:
                    retVal = "Home";
                    break;
                case Key.Insert:
                    retVal = "Insert";
                    break;
                case Key.End:
                    retVal = "End";
                    break;
                case Key.Delete:
                    retVal = "Delete";
                    break;
                default:
                    retVal = inputKey.ToString();
                    break;
            }
            // Sets the return value of the input key.

            return retVal;
        }

        /// <summary>
        /// Checks for matches.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="fromWhere">From where.</param>
        /// <returns></returns>
        private bool CheckForMatch(string newValue, string fromWhere)
        {
            bool match = false;
            // True if match is found.

            if (!string.IsNullOrEmpty(newValue))
            {
                if (fromWhere == "left")
                {
                    if (newValue == rightMovetxtbox.Text || newValue == pausetxtbox.Text || newValue == firetxtbox.Text)
                    {
                        match = true;
                    }
                }
                else if (fromWhere == "right")
                {
                    if (newValue == leftMovetxtbox.Text || newValue == pausetxtbox.Text || newValue == firetxtbox.Text)
                    {
                        match = true;
                    }
                }
                else if (fromWhere == "pause")
                {
                    if (newValue == rightMovetxtbox.Text || newValue == leftMovetxtbox.Text || newValue == firetxtbox.Text)
                    {
                        match = true;
                    }
                }
                else if (fromWhere == "fire")
                {
                    if (newValue == rightMovetxtbox.Text || newValue == pausetxtbox.Text || newValue == leftMovetxtbox.Text)
                    {
                        match = true;
                    }
                }
            }
            // Searches form matches in the keybinding.

            return match;
        }

        /// <summary>
        /// Handles the Click event of the mouseCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void mouseCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (!mouseCheckBox.IsChecked.Value && !keyboardCheckBox.IsChecked.Value)
            {
                // If both control is checked out.
                SettingsSaved.Content = "At least one control must be checked in.";

                LabelHide.Start();
                // Hides the label after the designated timespan.
            }
            else
            {
                // If not then refresh the OptionSettings object value.
                optionsSettings.MouseIsOn = mouseCheckBox.IsChecked.Value;
            }
            mouseCheckBox.IsChecked = optionsSettings.MouseIsOn;
            // Checks the control options and handles the input.
        }

        /// <summary>
        /// Handles the Click event of the keyboardCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void keyboardCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (!mouseCheckBox.IsChecked.Value && !keyboardCheckBox.IsChecked.Value)
            {
                // If both control is checked out.
                SettingsSaved.Content = "At least one control \n must be checked in.";

                LabelHide.Start();
                // Hides the label after the designated timespan.
            }
            else
            {
                // If not then refresh the OptionSettings object value.
                optionsSettings.KeyboardIsOn = keyboardCheckBox.IsChecked.Value;
            }
            keyboardCheckBox.IsChecked = optionsSettings.KeyboardIsOn;
            // Checks the control options and handles the input.
        }

        /// <summary>
        /// Handles the Click event of the soundCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void soundCheckBox_Click(object sender, RoutedEventArgs e)
        {
            optionsSettings.SoundIsOn = soundCheckBox.IsChecked.Value;
            // Refresh the OptionSettings object value.
        }

        /// <summary>
        /// Handles the SelectionChanged event of the ResolutionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ResolutionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            optionsSettings.GraphicsResolution = ResolutionComboBox.SelectedItem.ToString();
            // Refresh the OptionSettings object value.
        }

        /// <summary>
        /// Handles the KeyDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow openParent = new MainWindow();
                openParent.Show();
                Close();
            }
        }

        #endregion GuiElements

        #region FileHandler

        /// <summary>
        /// Loads the values from the xml file.
        /// </summary>
        void LoadValuesFromFile()
        {
            ResolutionComboBox.Items.Add("640x480");
            ResolutionComboBox.Items.Add("800x600");
            ResolutionComboBox.Items.Add("1024x768");
            
            // Fills the combo box.

            leftMovetxtbox.IsEnabled = false;
            rightMovetxtbox.IsEnabled = false;
            pausetxtbox.IsEnabled = false;
            firetxtbox.IsEnabled = false;
            // Sets the enable fields of the textboxes.

            try
            {
                if (!File.Exists("OptionsSettings.xml"))
                {
                    XElement mouseElement = new XElement("mouse", "true");
                    XElement keyboardElement = new XElement("keyboard", "true");
                    XElement soundElement = new XElement("sound", "true");
                    XElement resolutionElement = new XElement("resolution", "1024x768");
                    XElement leftkeyElement = new XElement("leftkey", "Left");
                    XElement rightkeyElement = new XElement("rightkey", "Right");
                    XElement firekeyElement = new XElement("firekey", "Space");
                    XElement pausekeyElement = new XElement("pausekey", "P");
                    XElement difficultyElement = new XElement("difficulty", "1");
                    XElement mapElement = new XElement("map", "1");
                    XAttribute newAttribute = new XAttribute("id", 1);
                    XElement newElements = new XElement("option", newAttribute, mouseElement, keyboardElement, soundElement, resolutionElement, leftkeyElement, rightkeyElement, firekeyElement, pausekeyElement, difficultyElement, mapElement);
                    XElement newOptions = new XElement("Options", newElements);
                    XDocument newDocument = new XDocument(newOptions);
                    newDocument.Save("OptionsSettings.xml");
                }
                // If the file doesn't exist, then create a new.

                XDocument settingsFromXml = XDocument.Load("OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                foreach (var oneElement in fromXml)
                {
                    optionsSettings.GraphicsResolution = (string)oneElement.Element("resolution");
                    optionsSettings.LeftKey = (string)oneElement.Element("leftkey");
                    optionsSettings.RightKey = (string)oneElement.Element("rightkey");
                    optionsSettings.PauseKey = (string)oneElement.Element("pausekey");
                    optionsSettings.FireKey = (string)oneElement.Element("firekey");
                    optionsSettings.DifficultyLevel = (int)oneElement.Element("difficulty");
                    optionsSettings.MouseIsOn = (bool)oneElement.Element("mouse");
                    optionsSettings.KeyboardIsOn = (bool)oneElement.Element("keyboard");
                    optionsSettings.MapNumber = (int)oneElement.Element("map");
                    optionsSettings.SoundIsOn = (bool)oneElement.Element("sound");
                }
                // Gives the values from the xml to the OptionSettings object, thus sets the object with the default values.

                leftMovetxtbox.Text = optionsSettings.LeftKey;
                rightMovetxtbox.Text = optionsSettings.RightKey;
                pausetxtbox.Text = optionsSettings.PauseKey;
                firetxtbox.Text = optionsSettings.FireKey;
                ResolutionComboBox.SelectedItem = optionsSettings.GraphicsResolution;
                mouseCheckBox.IsChecked = optionsSettings.MouseIsOn;
                keyboardCheckBox.IsChecked = optionsSettings.KeyboardIsOn;
                soundCheckBox.IsChecked = optionsSettings.SoundIsOn;
                // Sets the default values to the GUI elements.
            }
            catch
            {

            }
        }

        #endregion FileHandler

        #endregion Methods
    }
}
