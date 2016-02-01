using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace BrickBreaker
{
    /// <summary>
    /// Interaction logic for MapSelection.xaml
    /// </summary>
    public partial class MapSelection : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MapSelection"/> class.
        /// </summary>
        public MapSelection()
        {
            InitializeComponent();
            CheckForFiles();
        }

        #endregion Constructors

        #region Methods

        #region FileHandler

        /// <summary>
        /// Checks if the files exist.
        /// </summary>
        private void CheckForFiles()
        {
            if (!File.Exists(@"..\..\Resources\maps\FirstMap.txt"))
            {
                firstMap_Diff.IsEnabled = false;
            }
            if (!File.Exists(@"..\..\Resources\maps\SecondMap.txt"))
            {
                secondMap_Diff.IsEnabled = false;
            }
            if (!File.Exists(@"..\..\Resources\maps\ThirdMap.txt"))
            {
                thirdMap_Diff.IsEnabled = false;
            }
            if (!File.Exists(@"..\..\Resources\maps\FourthMap.txt"))
            {
                fourthMap_Diff.IsEnabled = false;
            }
            if (!File.Exists(@"..\..\Resources\maps\FifthMap.txt"))
            {
                fifthMap_Diff.IsEnabled = false;
            }
            // If the map files doesn't exist, then disable the buttons.

            if (!firstMap_Diff.IsEnabled && !secondMap_Diff.IsEnabled && !thirdMap_Diff.IsEnabled && !fourthMap_Diff.IsEnabled && !fifthMap_Diff.IsEnabled)
            {
                MessageBox.Show("No map file was found!", "Error", MessageBoxButton.OK);
            }
            // If all the buttons are disabled, then send a message.

            if (!File.Exists(@"..\..\Resources\OptionsSettings.xml"))
            {
                if (MessageBox.Show("Couldn't find the xml file for the settings. \n Would you like to create a new with default settings?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                    newDocument.Save(@"..\..\Resources\OptionsSettings.xml");
                }
            }
            // If the OptionsSettings xml doesn't exist, then send message.
        }

        #endregion FileHandler

        #region GuiElements

        /// <summary>
        /// Selects the map difficulty.
        /// </summary>
        private void SelectedMapDiff()
        {
            DifficultySelection diffSelect = new DifficultySelection();
            diffSelect.Show();
            Close();
        }

        /// <summary>
        /// Handles the Click event of the firstMap_Diff control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void firstMap_Diff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load(@"..\..\Resources\OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                fromXml.Single().Element("map").Value = "1";
                // Sets the number of the map to the xml for later use.

                settingsFromXml.Save(@"..\..\Resources\OptionsSettings.xml");
                // Save the changes in the values of the xml.
            }
            catch
            {

            }
            finally
            {
                SelectedMapDiff();
                // Launch the difficulty settings.
            }
        }

        /// <summary>
        /// Handles the Click event of the secondMap_Diff control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void secondMap_Diff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load(@"..\..\Resources\OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                fromXml.Single().Element("map").Value = "2";
                // Sets the number of the map to the xml for later use.

                settingsFromXml.Save(@"..\..\Resources\OptionsSettings.xml");
                // Save the changes in the values of the xml.
            }
            catch
            {

            }
            finally
            {
                SelectedMapDiff();
                // Launch the difficulty settings.
            }
        }

        /// <summary>
        /// Handles the Click event of the thirdMap_Diff control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void thirdMap_Diff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load(@"..\..\Resources\OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                fromXml.Single().Element("map").Value = "3";
                // Sets the number of the map to the xml for later use.

                settingsFromXml.Save(@"..\..\Resources\OptionsSettings.xml");
                // Save the changes in the values of the xml.
            }
            catch
            {

            }
            finally
            {
                SelectedMapDiff();
                // Launch the difficulty settings.
            }
        }

        /// <summary>
        /// Handles the Click event of the fourthMap_Diff control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void fourthMap_Diff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load(@"..\..\Resources\OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                fromXml.Single().Element("map").Value = "4";
                // Sets the number of the map to the xml for later use.

                settingsFromXml.Save(@"..\..\Resources\OptionsSettings.xml");
                // Save the changes in the values of the xml.
            }
            catch
            {

            }
            finally
            {
                SelectedMapDiff();
                // Launch the difficulty settings.
            }
        }

        /// <summary>
        /// Handles the Click event of the fifthMap_Diff control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void fifthMap_Diff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load(@"..\..\Resources\OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                fromXml.Single().Element("map").Value = "5";
                // Sets the number of the map to the xml for later use.

                settingsFromXml.Save(@"..\..\Resources\OptionsSettings.xml");
                // Save the changes in the values of the xml.
            }
            catch
            {

            }
            finally
            {
                SelectedMapDiff();
                // Launch the difficulty settings.
            }
        }

        /// <summary>
        /// Handles the Click event of the back control witch returns to the main window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backToMain = new MainWindow();
            backToMain.Show();
            Close();
        }

        #endregion GuiElements

        #endregion Methods
    }
}
