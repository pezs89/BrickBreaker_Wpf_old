using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace BrickBreaker
{
    /// <summary>
    /// Interaction logic for HighScores.xaml
    /// </summary>
    public partial class HighScores : Window
    {
        #region Fields

        HighScores_Class HighScore = new HighScores_Class("", "");
        HighScores_Class highscore = new HighScores_Class("", "");

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScores"/> class.
        /// </summary>
        public HighScores()
        {
            InitializeComponent();

            HighScore.Create("", 0);
            highscore.OrderBy();

            var xml = XDocument.Load(@"..\..\Resources\Scores.xml").Root;
            dataGrid1.DataContext = xml;           

            PressKeyTxtBlock.Text = "Press ESC to continue...";
        }

        #endregion Constructors
        
        #region Methods

        /// <summary>
        /// Returns to the Main window.
        /// </summary>
        private void BackToMain()
        {
            MainWindow returnToParent = new MainWindow();
            returnToParent.Show();
            Close();
        }

        /// <summary>
        /// Handles the KeyDown event of the Window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key  == Key.Escape)
            {
                BackToMain();
            }
        }

        #endregion Methods
    }
}
