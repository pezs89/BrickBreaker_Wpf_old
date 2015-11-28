using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace BrickBreaker
{
    /// <summary>
    /// Interaction logic for DifficultySelection.xaml
    /// </summary>
    public partial class DifficultySelection : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DifficultySelection"/> class.
        /// </summary>
        public DifficultySelection()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        #region FileHandler

        /// <summary>
        /// Handles the Loaded event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CheckForFiles();
        }

        /// <summary>
        /// Checks if the files exist.
        /// </summary>
        private void CheckForFiles()
        {
            if (!File.Exists("OptionsSettings.xml"))
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
                    newDocument.Save("OptionsSettings.xml");
                }
                else
                {
                    MapSelection returnToMap = new MapSelection();
                    returnToMap.Show();
                    Close();
                }
            }
            // If the OptionsSettings xml doesn't exist, then send message.
        }

        #endregion FileHandler

        #region GuiElements

        /// <summary>
        /// Initializes the game.
        /// </summary>
        void InitGame()
        { 
            Drawer initGame = new Drawer();
            initGame.gamePresets = new GamePresets(3, 0);
            initGame.Show();
            Close();                
        }

        /// <summary>
        /// Handles the Click event of the HardBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void HardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load("OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                fromXml.Single().Element("difficulty").Value = "3";
                // Sets the difficulty of the game to the xml for later use.

                settingsFromXml.Save("OptionsSettings.xml");
                // Save the changes in the values of the xml.
            }
            catch
            {

            }
            finally
            {
                InitGame();
                // Launch the game.
            }
        }

        /// <summary>
        /// Handles the Click event of the MediumBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MediumBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load("OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                fromXml.Single().Element("difficulty").Value = "2";
                // Sets the difficulty of the game to the xml for later use.

                settingsFromXml.Save("OptionsSettings.xml");
                // Save the changes in the values of the xml.
            }
            catch
            {

            }
            finally
            {
                InitGame();
                // Launch the game.
            }
        }

        /// <summary>
        /// Handles the Click event of the EasyBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EasyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument settingsFromXml = XDocument.Load("OptionsSettings.xml");
                var readDataFromXml = settingsFromXml.Descendants("option");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                fromXml.Single().Element("difficulty").Value = "1";
                // Sets the difficulty of the game to the xml for later use.

                settingsFromXml.Save("OptionsSettings.xml");
                // Save the changes in the values of the xml.
            }
            catch
            {

            }
            finally
            {
                InitGame();
                // Launch the game.
            }
        }

        #endregion GuiElements

        #endregion Methods
    }
}
