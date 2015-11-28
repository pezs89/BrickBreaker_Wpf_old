using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;

namespace BrickBreaker
{
    /// <summary>
    /// Interaction logic for Drawer.xaml
    /// </summary>
    public partial class Drawer : Window
    {
        #region Fields

        #region PrimitiveTypes

        private double racketDifference;
        // The modification size of the racket.

        private double racketMaxSize;
        // The max size of the racket.

        private double racketMinSize;
        // The min size of the racket.

        private double smallBall;
        // The size of the small ball.

        private double bigBall;
        // The size of the big ball.

        private double speedScale;
        // The scale number for speed.

        private double ballExaminationProximity;
        // the examination proximity of the ball.

        private int mapMaxNumber;
        // The maximum number of maps.

        private bool gameIsPaused;
        // Shows if the game is paused.

        private double bonusSpeed;
        // The speed of the bonus.

        public int lifePoint;
        // The life of the user.

        private double ballHorizontalMovement;
        // The horizontal movement of the ball(s).

        private double ballVerticalMovement;
        // The vertical movement of the ball(s).

        private double ballSpeed;
        // The speed of the ball(s), contains the slow, normal and fast speed.

        private double ballRadius;
        // The radius of the ball(s), contains the small, normal and big size.

        private double racketWidth;
        // The width of the racket(s).

        private double racketHeight;
        // The heigth of the racket(s).

        private double brickWidth;
        // The width of the brick(s).

        private double brickHeight;
        // The heigth of the brick(s).

        private double racketHorizontalMovement;
        // The horizontal movement of the racket.  
     
        public int scoreValue;
        // The sum of the score.

        private int timeSpan;
        // Timer timespan.

        private string mapName;
        // The name of the map.

        private double bonusWidth;
        // The width of the bonus.

        private double bonusHeigth;
        // The heigth of the bonus.

        private bool gameOver;
        // Shows if the game is over.

        private string gameOverStatus;
        // The status of the game when over.

        private double horizontalScaleNumber;
        // The number to scale the objects with horizontally.

        private double verticalScaleNumber;
        // The number to scale the objects with vartically.

        private bool gameInSession;
        // Shows if the game is in session.

        #endregion PrimitiveTypes

        #region Lists

        private List<Bonus> bonusList = new List<Bonus>();
        // List of bonuses.

        private List<Racket> racketList = new List<Racket>();
        // List of rackets.

        private List<Ball> ballList = new List<Ball>();
        // List of balls.

        private List<Brick> brickList = new List<Brick>();
        // List of bricks.

        #endregion Lists

        #region ComplexTypes

        OptionsSettings optionsSettings = new OptionsSettings("", "", "", "", "", 0, false, false, 0, false); 
        // OptionSettings object that contains the values of the xml.    
   
        private DateTime timeOfGame;
        // Time of the game.

        private static DispatcherTimer movingTimer = new DispatcherTimer(); 
        // A timer.

        private MediaPlayer mPlayer = new MediaPlayer();
        // Plays sound.

        Random rnd = new Random();
        // A random.

        internal GamePresets gamePresets;
        // Internal field for giving values from another window.

        #endregion ComplexTypes

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Drawer"/> class.
        /// </summary>
        public Drawer()
        {
            InitializeComponent();
            LoadValuesFromFile();

            #region Timer

            movingTimer.Tick += movingTimer_Tick;
            // This Method will be refreshed in every tick.
            movingTimer.Interval = new TimeSpan(0, 0, 0, 0, timeSpan);
            // Set the timespan.

            #endregion Timer

            #region SetValues

            canvasLayer.Background = new SolidColorBrush(Colors.White);
            // Backgroundcolor for the canvas.
            ScoreLabel.Content = "Score: " + scoreValue;
            // Show the scorepoints.
            LifeLabel.Content = "Life: " + lifePoint;
            // Show the lifepoints.
            TimeLabel.Content = "Time: " + timeOfGame.ToString("HH:mm:ss");
            // Show the time.

            #endregion SetValues
        }

        #endregion Constructors

        #region Methods

        #region FileHandling

        /// <summary>
        /// Loads the values from the xml file.
        /// </summary>
        private void LoadValuesFromFile()
        {
            #region PresetValues

            lifePoint = 3;
            scoreValue = 0;
            gameInSession = false;
            gameIsPaused = false;
            gameOver = false;
            gameOverStatus = "";
            mapName = "notfound";
            bonusSpeed = 1;
            mapMaxNumber = 5;
            timeSpan = 3;
            ballHorizontalMovement = 5;
            ballVerticalMovement = 5;
            racketHorizontalMovement = 10;
            ballSpeed = 1;
            speedScale = 1;
            brickWidth = 27.7;
            brickHeight = 8;
            racketWidth = 80;
            racketHeight = 8;
            bonusWidth = 24;
            bonusHeigth = 24;
            ballRadius = 5;
            smallBall = 3;
            bigBall = 15;
            ballExaminationProximity = 4;
            horizontalScaleNumber = 1;
            verticalScaleNumber = 1;
            racketMaxSize = 160;
            racketMinSize = 40;
            racketDifference = 16;

            if (bonusList.Count != 0)
            {
                bonusList.Clear();
            }
            if (racketList.Count != 0)
            {
                racketList.Clear();
            }
            if (ballList.Count != 0)
            {
                ballList.Clear();
            }
            if (brickList.Count != 0)
            {
                brickList.Clear();
            }

            optionsSettings = new OptionsSettings("", "", "", "", "", 0, false, false, 0, false);
            timeOfGame = new DateTime();
            movingTimer = new DispatcherTimer();
            mPlayer = new MediaPlayer();
            rnd = new Random();

            #endregion PresetValues

            #region OptionsHandler

            try
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
                    // If the OptionsSettings xml doesn't exist, then send message.
                }

                if (File.Exists("OptionsSettings.xml"))
                {
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
                }
            }
            catch
            {

            }

            #endregion OptionsHandler

            #region SetScaling

            switch (optionsSettings.GraphicsResolution)
            {
                case "640x480":
                    horizontalScaleNumber = 1;
                    verticalScaleNumber = 1;
                    break;
                case "800x600":
                    horizontalScaleNumber = 1.25;
                    verticalScaleNumber = 1.25;
                    break;
                case "1024x768":
                    horizontalScaleNumber = 1.6;
                    verticalScaleNumber = 1.6;
                    break;
            }

            switch (optionsSettings.DifficultyLevel)
            {
                case 1:
                    speedScale = 1;
                    break;
                case 2:
                    speedScale = 1.2;
                    break;
                case 3:
                    speedScale = 1.4;
                    break;
            }

            bonusWidth *= horizontalScaleNumber;
            bonusHeigth *= verticalScaleNumber;
            racketWidth *= horizontalScaleNumber;
            racketHeight *= verticalScaleNumber;
            brickWidth *= horizontalScaleNumber;
            brickHeight *= verticalScaleNumber;
            ballHorizontalMovement *= speedScale;
            ballVerticalMovement *= speedScale;
            ballRadius *= horizontalScaleNumber;
            smallBall *= horizontalScaleNumber;
            bigBall *= horizontalScaleNumber;
            bonusSpeed *= speedScale;
            ballSpeed *= speedScale;
            racketHorizontalMovement *= speedScale;
            ballExaminationProximity *= horizontalScaleNumber;
            racketMaxSize *= horizontalScaleNumber;
            racketMinSize *= horizontalScaleNumber;
            racketDifference *= horizontalScaleNumber;
            // Set the scaling.

            #endregion SetScaling

            #region MapSelection

            try
            {
                if (optionsSettings.MapNumber > 0 && optionsSettings.MapNumber < 6)
                {
                    switch (optionsSettings.MapNumber)
                    {
                        case 1:
                            if (!File.Exists(@"maps\FirstMap.txt"))
                            {
                                mapName = "notfound";
                            }
                            else
                            {
                                mapName = @"maps\FirstMap.txt";
                            }
                            break;
                        case 2:
                            if (!File.Exists(@"maps\SecondMap.txt"))
                            {
                                mapName = "notfound";
                            }
                            else
                            {
                                mapName = @"maps\SecondMap.txt";
                            }
                            break;
                        case 3:
                            if (!File.Exists(@"maps\ThirdMap.txt"))
                            {
                                mapName = "notfound";
                            }
                            else
                            {
                                mapName = @"maps\ThirdMap.txt";
                            }
                            break;
                        case 4:
                            if (!File.Exists(@"maps\FourthMap.txt"))
                            {
                                mapName = "notfound";
                            }
                            else
                            {
                                mapName = @"maps\FourthMap.txt";
                            }
                            break;
                        case 5:
                            if (!File.Exists(@"maps\FifthMap.txt"))
                            {
                                mapName = "notfound";
                            }
                            else
                            {
                                mapName = @"maps\FifthMap.txt";
                            }
                            break;
                    }
                    // Based on the selected map sets the name of the map that will open if it exists.
                }
                // Only switch if the number is correct.
            }
            catch
            {

            }

            #endregion MapSelection

            #region BrickListFill

            try
            {
                if (!String.IsNullOrEmpty(mapName) && mapName != "notfound")
                {
                    double X = 0;
                    double Y = 0;
                    FileStream fs = new FileStream(mapName, FileMode.Open, FileAccess.Read, FileShare.None);
                    BinaryReader rd = new BinaryReader(fs);

                    while (rd.BaseStream.Position != rd.BaseStream.Length)
                    {
                        char[] oneLine = rd.ReadChars(24);

                        for (int i = 0; i < oneLine.Length; i++)
                        {
                            switch (oneLine[i])
                            {
                                case '.':
                                    break;
                                case '1':
                                    Brick brick1 = new Brick(X, Y, brickHeight, brickWidth, Brick.brickType.Easy, @"media\brick\easybrick.jpg");
                                    brick1.ScorePoint = 10;
                                    brick1.BreakNumber = 1;
                                    brickList.Add(brick1);
                                    canvasLayer.Children.Add(brick1.GetRectangle());
                                    break;
                                case '2':
                                    Brick brick2 = new Brick(X, Y, brickHeight, brickWidth, Brick.brickType.Medium, @"media\brick\mediumbrick.jpg");
                                    brick2.ScorePoint = 20;
                                    brick2.BreakNumber = 2;
                                    brickList.Add(brick2);
                                    canvasLayer.Children.Add(brick2.GetRectangle());
                                    break;
                                case '3':
                                    Brick brick3 = new Brick(X, Y, brickHeight, brickWidth, Brick.brickType.Hard, @"media\brick\hardbrick.jpg");
                                    brick3.ScorePoint = 30;
                                    brick3.BreakNumber = 5;
                                    brickList.Add(brick3);
                                    canvasLayer.Children.Add(brick3.GetRectangle());
                                    break;
                                case '4':
                                    Brick brick4 = new Brick(X, Y, brickHeight, brickWidth, Brick.brickType.Steel, @"media\brick\steelbrick.jpg");
                                    brick4.ScorePoint = 40;
                                    brick4.BreakNumber = 1;
                                    brickList.Add(brick4);
                                    canvasLayer.Children.Add(brick4.GetRectangle());
                                    break;
                            }

                            X += brickWidth;
                        }

                        X = 0;
                        Y += brickHeight;
                    }

                    rd.Close();
                    fs.Close();
                    // Adds the bricks as they are stored in the map file.
                }
                // If the map name is valid, then load map.
            }
            catch
            {

            }

            #endregion BrickListFill

            #region SetValues

            mPlayer.Open(new Uri(@"media\sounds\play_this.mp3", UriKind.Relative));

            Width = int.Parse(optionsSettings.GraphicsResolution.Split('x')[0]);
            Height = int.Parse(optionsSettings.GraphicsResolution.Split('x')[1]);
            canvasLayer.Width = int.Parse(optionsSettings.GraphicsResolution.Split('x')[0]) - 30;
            canvasLayer.Height = int.Parse(optionsSettings.GraphicsResolution.Split('x')[1]) - 50;
            // Set resolution.

            Racket racket = new Racket((canvasLayer.Width / 2) - (racketWidth / 2), canvasLayer.Height - racketHeight, racketHeight, racketWidth, @"media\racket\normalracket.jpg");
            racketList.Add(racket);
            canvasLayer.Children.Add(racket.GetRectangle());
            // First racket.

            Ball ball = new Ball((canvasLayer.Width / 2) - ballRadius, canvasLayer.Height - racketHeight - (ballRadius * 2), ballRadius, Ball.ballType.Normal, @"media\ball\normalball.jpg");
            ball.VerticalMovement = ballVerticalMovement > 0 ? ballVerticalMovement : -ballVerticalMovement;
            ball.HorizontalMovement = ballHorizontalMovement < 0 ? ballHorizontalMovement : -ballHorizontalMovement;
            ball.BallInMove = false;
            ballList.Add(ball);
            canvasLayer.Children.Add(ball.GetEllipse());
            // First ball.

            #endregion SetValues
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(mapName) && mapName == "notfound")
            {
                MessageBox.Show("Couldn't find the map file.", "Error");

                MapSelection returnToMapSelection = new MapSelection();
                returnToMapSelection.Show();
                Close();
            }
            else if (String.IsNullOrEmpty(mapName))
            {
                MessageBox.Show("Couldn't find the options xml file.", "Error");

                MapSelection returnToMapSelection = new MapSelection();
                returnToMapSelection.Show();
                Close();
            }
            // If no map or no options file was found then close the window.

            lifePoint = gamePresets != null && gamePresets.LifePoint != 0 ? gamePresets.LifePoint : 3;
            scoreValue = gamePresets != null && gamePresets.ScorePoint != 0 ? gamePresets.ScorePoint : 0;

            ScoreLabel.Content = "Score: " + scoreValue;
            // Show the scorepoints.
            LifeLabel.Content = "Life: " + lifePoint;
            // Show the lifepoints.
            TimeLabel.Content = "Time: " + timeOfGame.ToString("HH:mm:ss");
            // Show the time.
        }

        #endregion FileHandling

        #region Drawing

        /// <summary>
        /// Handles the Tick event of the movingTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void movingTimer_Tick(object sender, EventArgs e)
        {
            // Draw.
            MoveObjects();
            BallAtContact();
            RacketAtContactWithBonus();

            #region RefreshValues

            timeOfGame = timeOfGame.AddMilliseconds(timeSpan);
            // Add the timeSpan value to the timeOfGame in every tick to make the time go.
            TimeLabel.Content = "Time: " + timeOfGame.ToString("HH:mm:ss");
            // Show the time.

            #endregion RefreshValues

            RefreshCanvas();

            #region GameOver

            if (ballList.Count > 0)
            {
                int steelBallCount = 0;
                
                foreach (var oneBall in ballList)
                {
                    // See if there is any steel ball.
                    if (oneBall.TypeOfBall == Ball.ballType.Steel)
                    {
                        steelBallCount++;
                    }
                }

                if (steelBallCount > 0)
                {
                    // If there is at least one steel ball.
                    if (brickList.Count == 0)
                    {
                        // If no brick left.
                        if (bonusList.Count == 0)
                        {
                            // If no bonus left.
                            gameOver = true;
                            gameOverStatus = "success";
                        }
                    }

                }
                else
                {
                    // If no steel ball.
                    if (brickList.Count > 0)
                    {
                        // There are still bricks left.
                        int notSteelBrickCount = 0;

                        foreach (var oneBrick in brickList)
                        {
                            // See how many not steel bricks are there
                            if (oneBrick.TypeOfBrick != Brick.brickType.Steel)
                            {
                                notSteelBrickCount++;
                            }
                        }

                        if (notSteelBrickCount == 0)
                        {
                            // If only steel bricks left.
                            gameOver = true;
                            gameOverStatus = "success";
                        }
                    }
                    else
                    {
                        // If no brick left.
                        if (bonusList.Count == 0)
                        {
                            // If no bonus left.
                            gameOver = true;
                            gameOverStatus = "success";
                        }
                    }
                }
            }

            if (gameOver && !String.IsNullOrEmpty(gameOverStatus) && gameInSession)
            {
                GameOver(gameOverStatus);
            }

            #endregion GameOver
        }

        /// <summary>
        /// Checks if the racket is in contact with a bonus.
        /// </summary>
        private void RacketAtContactWithBonus()
        {
            if (racketList.Count > 0)
            {
                foreach (var oneRacket in racketList)
                {
                    if (bonusList.Count > 0)
                    {
                        for (int i = 0; i < bonusList.Count; i++)
                        {
                            if (oneRacket.PositionX < bonusList[i].PositionX + bonusList[i].Width &&    /* bonus rigth side */
                                oneRacket.PositionX + oneRacket.Width > bonusList[i].PositionX &&       /* bonus left side */
                                oneRacket.PositionY < bonusList[i].PositionY + bonusList[i].Height)     /* bonus bottom */
                            {
                                scoreValue += bonusList[i].ScorePoint;
                                ScoreLabel.Content = "Score: " + scoreValue;

                                #region AddBonusEffect

                                switch (bonusList[i].TypeOfBonus)
                                {
                                    case Bonus.bonusType.LifeUp:
                                        lifePoint++;
                                        break;
                                    case Bonus.bonusType.LifeDown:
                                        lifePoint--;
                                        break;
                                    case Bonus.bonusType.NewBall:
                                        Ball ball = new Ball(oneRacket.PositionX + (oneRacket.Width / 2) - ballRadius, oneRacket.PositionY - (ballRadius * 2), ballRadius, Ball.ballType.Normal, @"media\ball\normalball.jpg");
                                        ball.VerticalMovement = ballVerticalMovement > 0 ? ballVerticalMovement : -ballVerticalMovement;
                                        ball.HorizontalMovement = ballHorizontalMovement < 0 ? ballHorizontalMovement : -ballHorizontalMovement;
                                        ball.BallInMove = false;
                                        ballList.Add(ball);
                                        canvasLayer.Children.Add(ball.GetEllipse());
                                        break;
                                    case Bonus.bonusType.RacketLengthen:
                                        oneRacket.Width += (oneRacket.Width < racketMaxSize ? racketDifference : 0);
                                        break;
                                    case Bonus.bonusType.RacketShorten:
                                        oneRacket.Width -= (oneRacket.Width > racketMinSize ? racketDifference : 0);
                                        break;
                                    case Bonus.bonusType.BallBigger:
                                        if (ballList.Count > 0)
                                        {
                                            foreach (var oneBall in ballList)
                                            {
                                                if (oneBall.PositionX + (bigBall * 2) > canvasLayer.Width)
                                                {
                                                    oneBall.PositionX -= ((bigBall - ballRadius) * 2);
                                                }
                                                else if (oneBall.PositionY + (bigBall * 2) + racketHeight > canvasLayer.Height)
                                                {
                                                    oneBall.PositionY -= ((bigBall - ballRadius) * 2);
                                                }
                                                // Reposition the ball, so that it will not trigger an error when obtaining bonus effect.

                                                oneBall.Radius = bigBall;
                                            }
                                        }
                                        break;
                                    case Bonus.bonusType.BallSmaller:
                                        if (ballList.Count > 0)
                                        {
                                            foreach (var oneBall in ballList)
                                            {
                                                oneBall.Radius = smallBall;
                                            }
                                        }
                                        break;
                                    case Bonus.bonusType.StickyRacket:
                                        if (!oneRacket.StickyRacket)
                                        {
                                            oneRacket.StickyRacket = true;
                                            oneRacket.RacketImage = @"media\racket\stickyracket.jpg";
                                        }
                                        break;
                                    case Bonus.bonusType.HardBall:
                                        if (ballList.Count > 0)
                                        {
                                            foreach (var oneBall in ballList)
                                            {
                                                oneBall.TypeOfBall = Ball.ballType.Hard;
                                                oneBall.BallImage = @"media\ball\hardball.jpg";
                                            }
                                        }
                                        break;
                                    case Bonus.bonusType.SteelBall:
                                        if (ballList.Count > 0)
                                        {
                                            foreach (var oneBall in ballList)
                                            {
                                                oneBall.TypeOfBall = Ball.ballType.Steel;
                                                oneBall.BallImage = @"media\ball\steelball.jpg";
                                            }
                                        }
                                        break;
                                }
                                // Add the bonus effect.

                                LifeLabel.Content = "Life: " + lifePoint;

                                if (lifePoint <= 0)
                                {
                                    gameOverStatus = "fail";
                                    gameOver = true;
                                }

                                #endregion AddBonusEffect

                                bonusList.Remove(bonusList[i]);
                            }
                        }
                    }
                }
            }

            RefreshCanvas();
        }

        /// <summary>
        /// Checks if the ball is in contact with an object.
        /// </summary>
        private void BallAtContact()
        {
            if (ballList.Count > 0)
            {
                for (int j = 0; j < ballList.Count; j++)
                {
                    // Check every ball.
                    #region BallAndWallContact

                    if (ballList[j].PositionX < 0 ||                                                    /* Left wall */
                        ballList[j].PositionX + (ballList[j].Radius * 2) > canvasLayer.ActualWidth ||   /* Right wall */
                        ballList[j].PositionY < 0 ||                                                    /* Top wall */
                        ballList[j].PositionY + (ballList[j].Radius * 2) > canvasLayer.ActualHeight)    /* Bottom wall */
                    {
                        // If the ball is at the walls of the canvas.
                        if (ballList[j].PositionX < 0 || ballList[j].PositionX + (ballList[j].Radius * 2) > canvasLayer.ActualWidth)
                        {
                            // Side walls.
                            // Vertical direction change.
                            ballList[j].VerticalMovement *= -1;
                        }
                        else if (ballList[j].PositionY <= 0)
                        {
                            // Top wall.
                            // Horizontal direction change.
                            ballList[j].HorizontalMovement *= -1;
                        }
                        else if (ballList[j].PositionY + (ballList[j].Radius * 2) > canvasLayer.ActualHeight)
                        {
                            // Bottom wall.
                            if (ballList.Count == 1)
                            {
                                lifePoint -= 1;
                                LifeLabel.Content = "Life: " + lifePoint;

                                DisposeOfBall();
                            }
                            else
                            {
                                ballList.RemoveAt(j);
                            }

                            if (lifePoint == 0)
                            {
                                gameOver = true;
                                gameOverStatus = "fail";
                            }
                            else if (lifePoint < 0)
                            {
                                MessageBox.Show("An error occured.", "Error");
                                gameOver = true;
                                gameOverStatus = "fail";
                            }
                            break;
                        }
                        else
                        {
                            // Corners of the canvas.
                            // Horizontal direction change.
                            ballList[j].HorizontalMovement *= -1;
                            // Vertical direction change.
                            ballList[j].VerticalMovement *= -1;
                        }

                        if (optionsSettings.SoundIsOn)
                        {
                            mPlayer.Position = new TimeSpan(0);
                            mPlayer.Play();
                        }
                    }

                    #endregion BallAndWallContact

                    else
                    {
                        // If the ball is not at the sides of the canvas.
                        if (racketList.Count > 0)
                        {
                            foreach (var oneRacket in racketList)
                            {
                                // Check with racket.
                                #region BallAndRacketContact

                                if (ballList[j].PositionX + ballList[j].Radius > oneRacket.PositionX &&                     /*racket left side*/
                                    ballList[j].PositionX + ballList[j].Radius < oneRacket.PositionX + oneRacket.Width &&   /*racket right side*/
                                    ballList[j].PositionY + (ballList[j].Radius * 2) > canvasLayer.Height - racketHeight)   /*racket top*/
                                {
                                    // Horizontal direction change.
                                    ballList[j].HorizontalMovement *= -1;

                                    //if (ballList[j].PositionX + ballList[j].Radius < oneRacket.PositionX + (oneRacket.Width / 2) - ballExaminationProximity)
                                    //{
                                    //    // The left of the racket.
                                    //    ballVerticalMovement *= ballVerticalMovement < 0 ? 1 : -1;
                                    //    double diff = () / ((oneRacket.Width / 2) - ballExaminationProximity);
                                    //}
                                    //else if (ballList[j].PositionX + ballList[j].Radius >= oneRacket.PositionX + (oneRacket.Width / 2) - ballExaminationProximity &&
                                    //         ballList[j].PositionX + ballList[j].Radius <= oneRacket.PositionX + (oneRacket.Width / 2) + ballExaminationProximity)
                                    //{
                                    //    // The middle of the racket.
                                    //    ballVerticalMovement = 0;
                                    //}
                                    //else if (ballList[j].PositionX + ballList[j].Radius > oneRacket.PositionX + (oneRacket.Width / 2) + ballExaminationProximity)
                                    //{
                                    //    // The right of the racket.
                                    //    ballVerticalMovement *= ballVerticalMovement > 0 ? 1 : -1;

                                    //}
                                    // TODO: complex movement calculation based on where the ball landed

                                    if (oneRacket.StickyRacket)
                                    {
                                        ballList[j].BallInMove = false;

                                        if (ballList[j].PositionY + (ballList[j].Radius * 2) > oneRacket.PositionY)
                                        {
                                            ballList[j].PositionY = oneRacket.PositionY - (ballList[j].Radius * 2);
                                        }
                                    }
                                    // TODO: bug with ball when racker is sticky

                                    if (optionsSettings.SoundIsOn)
                                    {
                                        mPlayer.Position = new TimeSpan(0);
                                        mPlayer.Play();
                                    }
                                }

                                #endregion BallAndRacketContact
                            }
                        }

                        if (brickList.Count > 0)
                        {
                            for (int i = 0; i < brickList.Count; i++)
                            {
                                // Check every brick.
                                #region BallAndBrickContact

                                #region OldVersion

                                //if (!(ballList[j].PositionX + ballList[j].Radius < brickList[i].PositionX || /**/
                                //    ballList[j].PositionX - ballList[j].Radius > brickList[i].PositionX + brickList[i].Width || /**/
                                //    ballList[j].PositionY + ballList[j].Radius < brickList[i].PositionY || /**/
                                //    ballList[j].PositionY - ballList[j].Radius > brickList[i].PositionY + brickList[i].Height)) /**/
                                //{
                                //    // Sides
                                //    // TODO: Bug at contact with brick.
                                //    // If the ball is in contact with the brick
                                //    if (ballList[j].TypeOfBall != Ball.ballType.Steel)
                                //    {
                                //        if (ballList[j].PositionX + ballList[j].Radius > brickList[i].PositionX) // r
                                //        {
                                //            if (ballList[j].PositionY + ballList[j].Radius > brickList[i].PositionY) // rb
                                //            {
                                //                ballList[j].HorizontalMovement *= -1; // horizontal?
                                //            }
                                //            else if (ballList[j].PositionY - ballList[j].Radius < brickList[i].PositionY + brickList[i].Height) // rt
                                //            {
                                //                ballList[j].HorizontalMovement *= -1;
                                //            }
                                //            else
                                //            {
                                //                ballList[j].VerticalMovement *= -1;
                                //            }
                                //        }
                                //        else if (ballList[j].PositionX + ballList[j].Radius < brickList[i].PositionX + ballHorizontalMovement) // l
                                //        {
                                //            if (ballList[j].PositionY + ballList[j].Radius > brickList[i].PositionY) // lb
                                //            {
                                //                ballList[j].HorizontalMovement *= -1;
                                //            }
                                //            else if (ballList[j].PositionY - ballList[j].Radius < brickList[i].PositionY + brickList[i].Height) // lt
                                //            {
                                //                ballList[j].HorizontalMovement *= -1;
                                //            }
                                //            else
                                //            {
                                //                ballList[j].VerticalMovement *= -1;
                                //            }
                                //        }
                                //        else if (ballList[j].PositionY + ballList[j].Radius > brickList[i].PositionY) // r
                                //        {
                                //            ballList[j].HorizontalMovement *= -1;
                                //        }
                                //        else if (ballList[j].PositionY - ballList[j].Radius < brickList[i].PositionY + brickList[i].Width) // l
                                //        {
                                //            ballList[j].HorizontalMovement *= -1;
                                //        }
                                //        else if (ballList[j].PositionY + ballList[j].Radius > brickList[i].PositionY) // b
                                //        {
                                //            ballList[j].VerticalMovement *= -1;
                                //        }
                                //        else if (ballList[j].PositionY - ballList[j].Radius < brickList[i].PositionY + brickList[i].Height) // t
                                //        {
                                //            ballList[j].VerticalMovement *= -1;
                                //        }
                                //    }

                                //    BrickContact(ballList[j], brickList[i]);
                                //}

                                #endregion OldVersion

                                #region TestVersion

                                if (!(ballList[j].PositionX + ballList[j].Radius < brickList[i].PositionX ||                      /* brick left side */
                                    ballList[j].PositionX + ballList[j].Radius > brickList[i].PositionX + brickList[i].Width ||   /* brick right side */
                                    ballList[j].PositionY + ballList[j].Radius < brickList[i].PositionY ||                        /* brick top */
                                    ballList[j].PositionY + ballList[j].Radius > brickList[i].PositionY + brickList[i].Height))   /* brick bottom */
                                {
                                    // Sides
                                    // If the ball is in contact with the brick and the examination proximity
                                    if (ballList[j].TypeOfBall != Ball.ballType.Steel)
                                    {
                                        // TODO: check points
                                        if (ballList[j].PositionX + ballList[j].Radius >= brickList[i].PositionX &&
                                            ballList[j].PositionX + ballList[j].Radius <= brickList[i].PositionX + brickList[i].Width &&
                                            ((ballList[j].PositionY + ballList[j].Radius >= brickList[i].PositionY &&
                                            ballList[j].PositionY + ballList[j].Radius - ballHorizontalMovement < brickList[i].PositionY) ||
                                            (ballList[j].PositionY + ballList[j].Radius <= brickList[i].PositionY + brickList[i].Height &&
                                            ballList[j].PositionY + ballList[j].Radius - ballHorizontalMovement > brickList[i].PositionY + brickList[i].Height)))
                                        {
                                            // Horizontal direction change.
                                            ballList[j].HorizontalMovement *= -1;
                                        }
                                        else if (ballList[j].PositionY + ballList[j].Radius >= brickList[i].PositionY &&
                                                 ballList[j].PositionY + ballList[j].Radius <= brickList[i].PositionY + brickList[i].Height &&
                                                 ((ballList[j].PositionX + ballList[j].Radius >= brickList[i].PositionX &&
                                                 ballList[j].PositionX + ballList[j].Radius - ballVerticalMovement < brickList[i].PositionX) ||
                                                 (ballList[j].PositionX + ballList[j].Radius <= brickList[i].PositionX + brickList[i].Width &&
                                                 ballList[j].PositionX + ballList[j].Radius - ballVerticalMovement > brickList[i].PositionX + brickList[i].Width)))
                                        {
                                            // Vertical direction change.
                                            ballList[j].VerticalMovement *= -1;
                                        }

                                        //    else if (ballList[j].PositionX + ballExaminationProximity > brickList[i].PositionX) // r
                                        //    {
                                        //        if (ballList[j].PositionY + ballExaminationProximity > brickList[i].PositionY) // rb
                                        //        {
                                        //            // Horizontal direction change.
                                        //            ballList[j].HorizontalMovement *= -1;
                                        //        }
                                        //        else if (ballList[j].PositionY - ballExaminationProximity < brickList[i].PositionY + brickList[i].Height) // rt
                                        //        {
                                        //            // Horizontal direction change.
                                        //            ballList[j].HorizontalMovement *= -1;
                                        //        }
                                        //        else
                                        //        {
                                        //            // Vertical direction change.
                                        //            ballList[j].VerticalMovement *= -1;
                                        //        }
                                        //    }
                                        //    else if (ballList[j].PositionX + ballExaminationProximity < brickList[i].PositionX + ballHorizontalMovement) // l
                                        //    {
                                        //        if (ballList[j].PositionY + ballExaminationProximity > brickList[i].PositionY) // lb
                                        //        {
                                        //            // Horizontal direction change.
                                        //            ballList[j].HorizontalMovement *= -1;
                                        //        }
                                        //        else if (ballList[j].PositionY - ballExaminationProximity < brickList[i].PositionY + brickList[i].Height) // lt
                                        //        {
                                        //            // Horizontal direction change.
                                        //            ballList[j].HorizontalMovement *= -1;
                                        //        }
                                        //        else
                                        //        {
                                        //            // Vertical direction change.
                                        //            ballList[j].VerticalMovement *= -1;
                                        //        }
                                        //    }
                                    }

                                    BrickContact(ballList[j], brickList[i]);
                                }
                                else
                                {
                                    // No corner detection implemented.
                                }

                                #endregion TestVersion

                                #endregion BallAndBrickContact
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Movethe objects.
        /// </summary>
        private void MoveObjects()
        {
            if (ballList.Count > 0)
            {
                foreach (var oneBall in ballList)
                {
                    if (oneBall.BallInMove)
                    {
                        oneBall.Move(ballSpeed);
                    }
                }
            }
            // Move balls.

            if (bonusList.Count > 0)
            {
                for (int i = 0; i < bonusList.Count; i++)
                {
                    if (bonusList[i].Descend(bonusSpeed, canvasLayer))
                    {
                        bonusList.Remove(bonusList[i]);
                    }
                }
            }
            // Move bonuses.
        }

        /// <summary>
        /// Brick is at contact with ball.
        /// </summary>
        /// <param name="oneBall">One ball.</param>
        /// <param name="oneBrick">One brick.</param>
        private void BrickContact(Ball oneBall, Brick oneBrick)
        {
            if (oneBall.TypeOfBall != Ball.ballType.Steel)
            {
                if (oneBrick.TypeOfBrick != Brick.brickType.Steel)
                {
                    if (oneBall.TypeOfBall != Ball.ballType.Hard)
                    {
                        if (oneBrick.BreakNumber == 1)
                        {
                            // If the brick is at breaking point.
                            scoreValue += oneBrick.ScorePoint;
                            // Add points to the score.
                            ScoreLabel.Content = "Score: " + scoreValue;
                            // Show the scorepints.

                            if (oneBrick.CalculateBonusChance())
                            {
                                // If the brick is lucky, then add bonus.
                                AddBonus(oneBrick);
                            }

                            brickList.Remove(oneBrick);
                        }
                        else
                        {
                            // If brick is not at breaking point, then decrement the breaking number.
                            oneBrick.BreakNumber -= 1;

                            if (oneBrick.TypeOfBrick == Brick.brickType.Hard)
                            {
                                oneBrick.BrickImage = @"media\brick\brokenhardbrick.jpg";
                            }
                            else if (oneBrick.TypeOfBrick == Brick.brickType.Medium)
                            {
                                oneBrick.BrickImage = @"media\brick\brokenmediumbrick.jpg";
                            }
                        }
                    }
                    else
                    {
                        scoreValue += oneBrick.ScorePoint;
                        // Add points to the score.
                        ScoreLabel.Content = "Score: " + scoreValue;
                        // Show the scorepints.

                        if (oneBrick.CalculateBonusChance())
                        {
                            // If the brick is lucky, then add bonus.
                            AddBonus(oneBrick);
                        }

                        brickList.Remove(oneBrick);
                    }
                }
            }
            else
            {
                // If the ball is steel then no breaknumber scan's needed, every brick breaks at first contact.
                scoreValue += oneBrick.ScorePoint;
                ScoreLabel.Content = "Score: " + scoreValue;

                if (oneBrick.CalculateBonusChance())
                {
                    // If the brick is lucky, then add bonus.
                    AddBonus(oneBrick);
                }

                brickList.Remove(oneBrick);
            }

            if (optionsSettings.SoundIsOn)
            {
                mPlayer.Position = new TimeSpan(0);
                mPlayer.Play();
            }
        }

        /// <summary>
        /// Adds a bonus.
        /// </summary>
        /// <param name="oneBrick">The one brick.</param>
        private void AddBonus(Brick oneBrick)
        {
            string bonusImage = "";
            Bonus.bonusType type = Bonus.bonusType.BallBigger;

            switch (rnd.Next(1, 11))
            {
                case 1:
                    type = Bonus.bonusType.BallBigger;
                    bonusImage = @"media\bonus\ballbigger.jpg";
                    break;
                case 2:
                    type = Bonus.bonusType.BallSmaller;
                    bonusImage = @"media\bonus\ballsmaller.jpg";
                    break;
                case 3:
                    type = Bonus.bonusType.HardBall;
                    bonusImage = @"media\bonus\hardball.jpg";
                    break;
                case 4:
                    type = Bonus.bonusType.LifeDown;
                    bonusImage = @"media\bonus\lifedown.jpg";
                    break;
                case 5:
                    type = Bonus.bonusType.LifeUp;
                    bonusImage = @"media\bonus\lifeup.jpg";
                    break;
                case 6:
                    type = Bonus.bonusType.NewBall;
                    bonusImage = @"media\bonus\newball.jpg";
                    break;
                case 7:
                    type = Bonus.bonusType.RacketLengthen;
                    bonusImage = @"media\bonus\racketlengthen.jpg";
                    break;
                case 8:
                    type = Bonus.bonusType.RacketShorten;
                    bonusImage = @"media\bonus\racketshorten.jpg";
                    break;
                case 9:
                    type = Bonus.bonusType.SteelBall;
                    bonusImage = @"media\bonus\steelball.jpg";
                    break;
                case 10:
                    type = Bonus.bonusType.StickyRacket;
                    bonusImage = @"media\bonus\stickyracket.jpg";
                    break;
            }

            Bonus bonus = new Bonus(oneBrick.PositionX + (oneBrick.Width / 2) - (bonusWidth / 2), oneBrick.PositionY + oneBrick.Height, bonusHeigth, bonusWidth, type, bonusImage);
            bonus.ScorePoint = 5;
            bonusList.Add(bonus);
            if (bonus.Descend(bonusSpeed, canvasLayer))
            {
                bonusList.Remove(bonus);
            }
        }

        /// <summary>
        /// Handles the event when the ball falls down.
        /// </summary>
        private void DisposeOfBall()
        {
            canvasLayer.Children.Clear();
            racketList.Clear();
            ballList.Clear();
            bonusList.Clear();
            // Dispose balls, rackets, bonuses.

            Racket racket = new Racket((canvasLayer.Width / 2) - (racketWidth / 2), canvasLayer.Height - racketHeight, racketHeight, racketWidth, @"media\racket\normalracket.jpg");
            racketList.Add(racket);
            canvasLayer.Children.Add(racket.GetRectangle());
            // Add new racket.

            Ball ball = new Ball((canvasLayer.Width / 2) - ballRadius, canvasLayer.Height - racketHeight - (ballRadius * 2), ballRadius, Ball.ballType.Normal, @"media\ball\normalball.jpg");
            ball.VerticalMovement = ballVerticalMovement > 0 ? ballVerticalMovement : -ballVerticalMovement;
            ball.HorizontalMovement = ballHorizontalMovement < 0 ? ballHorizontalMovement : -ballHorizontalMovement;
// TODO: bug at new ball movement
            ball.BallInMove = false;
            ballList.Add(ball);
            canvasLayer.Children.Add(ball.GetEllipse());
            // Add new ball.

            RefreshCanvas();
        }

        /// <summary>
        /// Refreshes the canvas.
        /// </summary>
        private void RefreshCanvas()
        {
            #region OldVersion

            canvasLayer.Children.Clear();
            // Remove every object from the canvas.

            if (racketList.Count > 0)
            {
                foreach (var oneRacket in racketList)
                {
                    canvasLayer.Children.Add(oneRacket.GetRectangle());
                }
            }
            // Add every racket to the canvas.

            if (brickList.Count > 0)
            {
                foreach (var oneBrick in brickList)
                {
                    canvasLayer.Children.Add(oneBrick.GetRectangle());
                }
            }
            // Add every brick to the canvas.

            if (ballList.Count > 0)
            {
                foreach (var oneBall in ballList)
                {
                    canvasLayer.Children.Add(oneBall.GetEllipse());
                }
            }
            // Add every ball to the canvas.

            if (bonusList.Count > 0)
            {
                for (int i = 0; i < bonusList.Count; i++)
                {
                    canvasLayer.Children.Add(bonusList[i].GetRectangle());
                }
            }
            // Add every bonus to the canvas.

            #endregion OldVersion

            #region TestVersion

            //Action emptyDelegate = delegate { };

            //canvasLayer.Dispatcher.Invoke(DispatcherPriority.Render, emptyDelegate);
            //canvasLayer.UpdateLayout();
            //canvasLayer.InvalidateVisual();

            #endregion TestVersion
        }

        #endregion Drawing

        #region KeyboardAndMouse

        #region MouseConrols

        /// <summary>
        /// Handles the MouseMove event of the canvasLayer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void canvasLayer_MouseMove(object sender, MouseEventArgs e)
        {
            if (optionsSettings.MouseIsOn && !gameIsPaused)
            {
                if (racketList.Count > 0)
                {
                    foreach (var oneRacket in racketList)
                    {
                        oneRacket.CanvasMouseMove(e.GetPosition(canvasLayer), canvasLayer);

                        if (ballList.Count > 0)
                        {
                            foreach (var oneBall in ballList)
                            {
                                if (!oneBall.BallInMove)
                                {
                                    // Move the ball with the racket if the ball is not in move.
                                    oneBall.CanvasMouseMove(e.GetPosition(canvasLayer), canvasLayer, oneRacket);
                                }
                            }
                        }
                    }
                }

                RefreshCanvas();
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (optionsSettings.MouseIsOn && !gameIsPaused)
            {
                if (!movingTimer.IsEnabled && !gameInSession)
                {
                    // Start the game.
                    movingTimer.Start();
                    gameInSession = true;
                }

                if (ballList.Count > 0)
                {
                    bool oneGo = false;
                    int iteratorCounter = 0;

                    while (!oneGo && iteratorCounter < ballList.Count)
                    {
                        // Start a ball.
                        if (!ballList[iteratorCounter].BallInMove)
                        {
                            ballList[iteratorCounter].BallInMove = true;
                            oneGo = true;
                        }

                        iteratorCounter++;
                    }
                }
            }
        }

        #endregion MouseControls

        #region KeyboardControls

        /// <summary>
        /// Handles the KeyDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                // Pause the game and send message.
                movingTimer.Stop();
                gameIsPaused = true;
                TimeLabel.Content = "The game is paused.";

                if (MessageBox.Show("Do you want to quit?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MapSelection returnToMapSelection = new MapSelection();
                    returnToMapSelection.Show();
                    Close();
                }
            }
            else if (LookUpKey(e.Key) == optionsSettings.FireKey || LookUpKey(e.Key) == optionsSettings.PauseKey || LookUpKey(e.Key) == optionsSettings.LeftKey || LookUpKey(e.Key) == optionsSettings.RightKey)
            {
                if ((LookUpKey(e.Key) == optionsSettings.LeftKey || LookUpKey(e.Key) == optionsSettings.RightKey) && optionsSettings.KeyboardIsOn && !gameIsPaused) 
                {
                    if (LookUpKey(e.Key) == optionsSettings.LeftKey)
                    {
                        // Move the racket left.
                        if (racketList.Count > 0)
                        {
                            foreach (var oneRacket in racketList)
                            {
                                oneRacket.MoveKey(racketHorizontalMovement, "left", canvasLayer);

                                if (ballList.Count > 0)
                                {
                                    foreach (var oneBall in ballList)
                                    {
                                        if (!oneBall.BallInMove)
                                        {
                                            // Move the ball with the racket left if ball is not moving.
                                            oneBall.MoveKey(racketHorizontalMovement, "left", canvasLayer, oneRacket);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (LookUpKey(e.Key) == optionsSettings.RightKey)
                    {
                        // Move the racket right.
                        if (racketList.Count > 0)
                        {
                            foreach (var oneRacket in racketList)
                            {
                                oneRacket.MoveKey(racketHorizontalMovement, "right", canvasLayer);

                                if (ballList.Count > 0)
                                {
                                    foreach (var oneBall in ballList)
                                    {
                                        if (!oneBall.BallInMove)
                                        {
                                            // Move the ball with the racket right if ball is not moving.
                                            oneBall.MoveKey(racketHorizontalMovement, "right", canvasLayer, oneRacket);
                                        }
                                    }
                                }
                            }
                        }
                    } 

                    RefreshCanvas();
                }
                else if (LookUpKey(e.Key) == optionsSettings.FireKey)
                {
                    if (optionsSettings.KeyboardIsOn && !gameIsPaused && !gameInSession)
                    {
                        if (!movingTimer.IsEnabled)
                        {
                            // Start the game.
                            movingTimer.Start();
                            gameInSession = true;
                        }

                        if (ballList.Count > 0)
                        {
                            bool oneGo = false;
                            int iteratorCounter = 0;

                            while (!oneGo && iteratorCounter < ballList.Count)
                            {
                                // Start a ball.
                                if (!ballList[iteratorCounter].BallInMove)
                                {
                                    ballList[iteratorCounter].BallInMove = true;
                                    oneGo = true;
                                }

                                iteratorCounter++;
                            }
                        }
                    }
                }
                else if (LookUpKey(e.Key) == optionsSettings.PauseKey)
                {
                    // Pause the game.
                    if (!gameIsPaused)
                    {
                        movingTimer.Stop();
                        gameIsPaused = true;
                        TimeLabel.Content = "The game is paused.";
                    }
                    else
                    {
                        movingTimer.Start();
                        gameIsPaused = false;
                        TimeLabel.Content = "Time: " + timeOfGame.ToString("HH:mm:ss");
                    }
                }
                // Process the control keys.
            }
        }

        /// <summary>
        /// Looks up the input key.
        /// </summary>
        /// <param name="inputKey">The inputKey.</param>
        /// <returns>retVal</returns>
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

        #endregion KeyboardControls

        #endregion KeyboardAndMouse

        #region GameOver

        /// <summary>
        /// Handles the end of the game.
        /// </summary>
        /// <param name="status">The status.</param>
        private void GameOver(string status)
        {
            if (status == "fail")
            {
                movingTimer.Stop();

                MessageBox.Show("You've failed.", "Game Over");

                gameOver = false;
                gameOverStatus = null;

                if (CheckHighScores(scoreValue))
                {
                    HighScores(scoreValue);
                    Close();
                }
                else
                {
                    MapSelection returnToMap = new MapSelection();
                    returnToMap.Show();
                    Close();
                }
            }
            else if (status == "success")
            {
                movingTimer.Stop();

                MessageBox.Show("You've succeeded.", "Game Over");
                
                gameOver = false;
                gameOverStatus = null;

                if (optionsSettings.MapNumber < mapMaxNumber)
                {
                    if (MessageBox.Show("You've succeeded. \n Would you like to continue.", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            XDocument settingsFromXml = XDocument.Load("OptionsSettings.xml");
                            var readDataFromXml = settingsFromXml.Descendants("option");
                            var fromXml = from x in readDataFromXml
                                          select x;
                            // Load the values stored in the xml.

                            fromXml.Single().Element("map").Value = (optionsSettings.MapNumber + 1).ToString();
                            // Sets the number of the map to the xml for later use.

                            settingsFromXml.Save("OptionsSettings.xml");
                            // Save the changes in the values of the xml.
                        }
                        catch
                        {

                        }

                        Drawer newMap = new Drawer();
                        newMap.gamePresets = new GamePresets(lifePoint, scoreValue);
                        newMap.Show();
                        Close();
                    }
                    else
                    {
                        if (CheckHighScores(scoreValue))
                        {
                            HighScores(scoreValue);
                            Close();
                        }
                        else
                        {
                            MapSelection returnToMap = new MapSelection();
                            returnToMap.Show();
                            Close();
                        }
                    }
                }
                else
                {
                    if (CheckHighScores(scoreValue))
                    {
                        HighScores(scoreValue);
                        Close();
                    }
                    else
                    {
                        MapSelection returnToMap = new MapSelection();
                        returnToMap.Show();
                        Close();
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the score is in the highscores.
        /// </summary>
        /// <param name="scoreValue">The score.</param>
        /// <returns></returns>
        private bool CheckHighScores(int score)
        {
            bool retVal = false;

            if (File.Exists("Scores.xml"))
            {
                XDocument settingsFromXml = XDocument.Load("Scores.xml");
                var readDataFromXml = settingsFromXml.Descendants("Data");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                int elementNumber = 0;

                foreach (var oneElement in fromXml)
                {
                    if (scoreValue > (int)oneElement.Element("Score") && !retVal)
                    {
                        retVal = true;
                    }

                    elementNumber += 1;
                }

                if (elementNumber < 10 && !retVal)
                {
                    retVal = true;
                }
                // See if there is a smaller scorepoint then the player's score.
            }

            return retVal;
        }

        /// <summary>
        /// Handles the highscores.
        /// </summary>
        private void HighScores(int score)
        {
            GameOver submitScore = new GameOver();
            submitScore.ScoreLabel.Content = score;
            submitScore.Show();
        }

        #endregion GameOver

        #endregion Methods
    }
}
