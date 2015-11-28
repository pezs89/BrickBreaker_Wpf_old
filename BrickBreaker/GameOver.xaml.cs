using System;
using System.Windows;

namespace BrickBreaker
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        #region Fields

        HighScores_Class HighScore = new HighScores_Class("", "");

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOver"/> class.
        /// </summary>
        public GameOver()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the Click event of the cancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MapSelection returnToMapWindow = new MapSelection();
            returnToMapWindow.Show();
            Close();
        }

        /// <summary>
        /// Handles the Click event of the okButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(nameTextBox.Text))
            {
                HighScore.InjectiontoXML(nameTextBox.Text, int.Parse(ScoreLabel.Content.ToString()));
                HighScore.OrderBy();

                MapSelection returnToMapWindow = new MapSelection();
                returnToMapWindow.Show();
                Close();
            }
        }

        #endregion Methods
    }
}
