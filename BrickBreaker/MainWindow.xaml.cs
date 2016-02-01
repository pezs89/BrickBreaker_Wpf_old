using System.Windows;
using System.Xml.Linq;
using System.IO;
using System.Windows.Input;

namespace BrickBreaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            CheckIfXmlFileExist();
        }

        #endregion Constructors

        #region Methods

        #region FileHandler

        /// <summary>
        /// Checks if XML file exist.
        /// </summary>
        private void CheckIfXmlFileExist()
        {
            try
            {
                if (!File.Exists(@"..\..\Resources\OptionsSettings.xml"))
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
                // If the file doesn't exist, then create a new.
            }
            catch
            {

            }
        }

        #endregion FileHandler

        #region GuiElements

        /// <summary>
        /// Handles the Click event of the btnNewGame control wich navigates to the map selection window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            MapSelection mapSelectionWindow = new MapSelection();
            mapSelectionWindow.Show();
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnHighScores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnHighScores_Click(object sender, RoutedEventArgs e)
        {
            HighScores highScoresWindow = new HighScores();
            highScoresWindow.Show();
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnOptions control wich navigates to the options window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Options optionsWindow = new Options();
            optionsWindow.Show();
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnInformations control wich navigates to the information window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInformations_Click(object sender, RoutedEventArgs e)
        {
            Informations info = new Informations();
            info.Show();
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnExit control wich exits the application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the KeyDown event of the Window control wich exits the application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        #endregion GuiElements

        #endregion Methods
    }
}
