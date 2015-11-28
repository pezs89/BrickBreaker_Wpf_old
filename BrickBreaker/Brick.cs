using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BrickBreaker
{
    /// <summary>
    /// Base class for brick game element.
    /// </summary>
    class Brick
    {
        #region Fields

        private double positionX;
        // The x coordinate of the brick.

        private double positionY;
        // The y coordinate of the brick.

        private double height;
        // The height of the brick.

        private double width;
        // The width of the brick.

        private int scorePoint;
        // The score value of the brick.

        private int breakNumber;
        // The number of hits to break the brick.

        private string brickImage;
        // The image of the brick.

        public enum brickType
        {
            Easy,
            Medium,
            Hard,
            Steel
        }
        // The types of the bricks.

        private brickType typeOfBrick;
        // Type of the brick.

        #endregion Fields

        #region Parameters

        /// <summary>
        /// Gets or sets the position x.
        /// </summary>
        /// <value>
        /// The position x.
        /// </value>
        public double PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }

        /// <summary>
        /// Gets or sets the position y.
        /// </summary>
        /// <value>
        /// The position y.
        /// </value>
        public double PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets or sets the breakNumber.
        /// </summary>
        /// <value>
        /// The breakNumber.
        /// </value>
        public int BreakNumber
        {
            get { return breakNumber; }
            set { breakNumber = value; }
        }

        /// <summary>
        /// Gets or sets the scorePoint.
        /// </summary>
        /// <value>
        /// The scorePoint.
        /// </value>
        public int ScorePoint
        {
            get { return scorePoint; }
            set { scorePoint = value; }
        }

        /// <summary>
        /// Gets or sets the typeOfBrick.
        /// </summary>
        /// <value>
        /// The typeOfBrick.
        /// </value>
        public brickType TypeOfBrick
        {
            get { return typeOfBrick; }
            set { typeOfBrick = value; }
        }

        /// <summary>
        /// Gets or sets the brickImage.
        /// </summary>
        /// <value>
        /// The brickImage.
        /// </value>
        public string BrickImage
        {
            get { return brickImage; }
            set { brickImage = value; }
        }

        #endregion Parameters

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class.
        /// </summary>
        /// <param name="positionx">The positionx.</param>
        /// <param name="positiony">The positiony.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="typeofbrick">The typeofbrick.</param>
        /// <param name="brickimage">The brickimage.</param>
        public Brick(double positionx, double positiony, double height, double width, brickType typeofbrick, string brickimage)
        {
            positionX = positionx;
            positionY = positiony;
            this.height = height;
            this.width = width;
            typeOfBrick = typeofbrick;
            brickImage = brickimage;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Shape of the racket.
        /// </summary>
        /// <returns>brickRectangle</returns>
        public Rectangle GetRectangle()
        {
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(brickImage, UriKind.Relative));

            Rectangle brickRectangle = new Rectangle();
            brickRectangle.Fill = imgBrush;
            brickRectangle.Width = width;
            brickRectangle.Height = height;

            Binding rectangleXBinding = new Binding("PositionX");
            rectangleXBinding.Source = this;
            brickRectangle.SetBinding(Canvas.LeftProperty, rectangleXBinding);

            Binding rectangleYBinding = new Binding("PositionY");
            rectangleYBinding.Source = this;
            brickRectangle.SetBinding(Canvas.TopProperty, rectangleYBinding);

            return brickRectangle;
        }

        /// <summary>
        /// Calculation of bonus chance.
        /// </summary>
        /// <returns>retVal</returns>
        public bool CalculateBonusChance()
        {
            bool retVal = false;

            if (typeOfBrick == brickType.Medium || typeOfBrick == brickType.Hard)
            {
                // Bonus is only available with medium and hard bricks.
                Random rnd = new Random();

                if (rnd.Next(1, 101) <= 25)
                {
                    // 25% chance of bonus in medium and hard bricks.
                    retVal = true;
                }
            }

            return retVal;
        }

        #endregion Methods
    }
}
