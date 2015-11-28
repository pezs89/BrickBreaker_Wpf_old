
namespace BrickBreaker
{
    /// <summary>
    /// Class for xml handling.
    /// </summary>
    class OptionsSettings
    {
        #region Fields

        private string graphicsResolution;
        // The graphical resolution.

        private string leftKey;
        // The left control key.

        private string rightKey;
        // The right control key.

        private string pauseKey;
        // The pause key.

        private string fireKey;
        // The fire key.

        private int difficultyLevel;
        // The level of the difficulty.

        private bool mouseIsOn;
        // Is the mouse in use.

        private bool keyboardIsOn;
        // Is the keyboard in use.

        private int mapNumber;
        // The number of the map.

        private bool soundIsOn;
        // Is the sound in use.

        #endregion Fields

        #region Parameters

        /// <summary>
        /// Gets or sets the graphics resolution.
        /// </summary>
        /// <value>
        /// The graphics resolution.
        /// </value>
        public string GraphicsResolution
        {
            get { return graphicsResolution; }
            set { graphicsResolution = value; }
        }

        /// <summary>
        /// Gets or sets the left key.
        /// </summary>
        /// <value>
        /// The left key.
        /// </value>
        public string LeftKey
        {
            get { return leftKey; }
            set { leftKey = value; }
        }

        /// <summary>
        /// Gets or sets the right key.
        /// </summary>
        /// <value>
        /// The right key.
        /// </value>
        public string RightKey
        {
            get { return rightKey; }
            set { rightKey = value; }
        }

        /// <summary>
        /// Gets or sets the pause key.
        /// </summary>
        /// <value>
        /// The pause key.
        /// </value>
        public string PauseKey
        {
            get { return pauseKey; }
            set { pauseKey = value; }
        }

        /// <summary>
        /// Gets or sets the fire key.
        /// </summary>
        /// <value>
        /// The fire key.
        /// </value>
        public string FireKey
        {
            get { return fireKey; }
            set { fireKey = value; }
        }

        /// <summary>
        /// Gets or sets the difficulty level.
        /// </summary>
        /// <value>
        /// The difficulty level.
        /// </value>
        public int DifficultyLevel
        {
            get { return difficultyLevel; }
            set { difficultyLevel = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [mouse is on].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [mouse is on]; otherwise, <c>false</c>.
        /// </value>
        public bool MouseIsOn
        {
            get { return mouseIsOn; }
            set { mouseIsOn = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [keyboard is on].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [keyboard is on]; otherwise, <c>false</c>.
        /// </value>
        public bool KeyboardIsOn
        {
            get { return keyboardIsOn; }
            set { keyboardIsOn = value; }
        }

        /// <summary>
        /// Gets or sets the map number.
        /// </summary>
        /// <value>
        /// The map number.
        /// </value>
        public int MapNumber
        {
            get { return mapNumber; }
            set { mapNumber = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [sound is on].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sound is on]; otherwise, <c>false</c>.
        /// </value>
        public bool SoundIsOn
        {
            get { return soundIsOn; }
            set { soundIsOn = value; }
        }

        #endregion Parameters

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsSettings"/> class.
        /// </summary>
        /// <param name="graphicsresolution">The graphicsresolution.</param>
        /// <param name="leftkey">The leftkey.</param>
        /// <param name="rightkey">The rightkey.</param>
        /// <param name="pausekey">The pausekey.</param>
        /// <param name="firekey">The firekey.</param>
        /// <param name="difficultylevel">The difficultylevel.</param>
        /// <param name="mouseison">if set to <c>true</c> [mouseison].</param>
        /// <param name="keyboardison">if set to <c>true</c> [keyboardison].</param>
        /// <param name="mapnumber">The mapnumber.</param>
        /// <param name="soundison">The soundison.</param>
        public OptionsSettings(string graphicsresolution, string leftkey, string rightkey, string pausekey, string firekey, int difficultylevel, bool mouseison, bool keyboardison, int mapnumber, bool soundison)
        {
            graphicsResolution = graphicsresolution;
            leftKey = leftkey;
            rightKey = rightkey;
            pauseKey = pausekey;
            fireKey = firekey;
            difficultyLevel = difficultylevel;
            mouseIsOn = mouseison;
            keyboardIsOn = keyboardison;
            mapNumber = mapnumber;
            soundIsOn = soundison;
        }

        #endregion Constructors

        #region Methods

        #endregion Methods
    }
}
