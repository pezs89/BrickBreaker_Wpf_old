using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace BrickBreaker
{
    /// <summary>
    /// Interaction logic for Informations.xaml
    /// </summary>
    public partial class Informations : Window
    {
        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Informations"/> class.
        /// </summary>
        public Informations()
        {
            InitializeComponent();
            
            #region SetValues

            InfoTxtBlock.Text = "Name: " + "\n" + "Neptun: " + "\n" + "Application: Brick Breaker" + "\n" + "Version no.: 1.0";
            PressKeyTxtBlock.Text = "Press ESC to continue...";

            #endregion SetValues

            KeyDown += Informations_KeyDown;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the KeyDown event of the Informations window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Informations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow openParent = new MainWindow();
                openParent.Show();
                Close();
            }
        }

        #endregion Methods
    }
}
