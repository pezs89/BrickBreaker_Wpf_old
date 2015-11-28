using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BrickBreaker
{
    /// <summary>
    /// Base class for bonus game element.
    /// </summary>
    class Bonus
    {
        #region Fields

        private double positionX;
        // The x coordinate of the bonus.

        private double positionY;
        // The y coordinate of the bonus.

        private double height;
        // The height of the bonus.

        private double width;
        // The width of the bonus.

        private int scorePoint;
        // The score value of the bouns.

        private string bonusImage;
        // The image of the bonus.

        public enum bonusType
        {
            LifeUp,
            LifeDown,
            NewBall,
            RacketLengthen,
            RacketShorten,
            BallBigger,
            BallSmaller,
            StickyRacket,
            HardBall,
            SteelBall
        }
        // The types of the bonuses.

        private bonusType typeOfBonus;
        // The type of the bonus.

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
        /// Gets the scorePoint.
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
        /// Gets or sets the typeOfBonus.
        /// </summary>
        /// <value>
        /// The typeOfBonus.
        /// </value>
        public bonusType TypeOfBonus
        {
            get { return typeOfBonus; }
            set { typeOfBonus = value; }
        }

        /// <summary>
        /// Gets or sets the bonusImage.
        /// </summary>
        /// <value>
        /// The bonusImage.
        /// </value>
        public string BonusImage
        {
            get { return bonusImage; }
            set { bonusImage = value; }
        }

        #endregion Parameters

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bonus"/> class.
        /// </summary>
        /// <param name="positionx">The positionx.</param>
        /// <param name="positiony">The positiony.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="typeofbonus">The typeofbonus.</param>
        /// <param name="bonusimage">The bonusimage.</param>
        public Bonus(double positionx, double positiony, double height, double width, bonusType typeofbonus, string bonusimage)
        {
            positionX = positionx;
            positionY = positiony;
            this.height = height;
            this.width = width;
            typeOfBonus = typeofbonus;
            bonusImage = bonusimage;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Shape of the bonus.
        /// </summary>
        /// <returns>bonusRectangle</returns>
        public Rectangle GetRectangle()
        {
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(bonusImage, UriKind.Relative));

            Rectangle bonusRectangle = new Rectangle();
            bonusRectangle.Fill = imgBrush;
            bonusRectangle.Width = width;
            bonusRectangle.Height = height;

            Binding rectangleXBinding = new Binding("PositionX");
            rectangleXBinding.Source = this;
            bonusRectangle.SetBinding(Canvas.LeftProperty, rectangleXBinding);

            Binding rectangleYBinding = new Binding("PositionY");
            rectangleYBinding.Source = this;
            bonusRectangle.SetBinding(Canvas.TopProperty, rectangleYBinding);

            return bonusRectangle;
        }

        /// <summary>
        /// Descends the specified speed.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="canvas">The canvas.</param>
        /// <returns></returns>
        public bool Descend(double speed, Canvas canvas)
        {
            if (PositionY >= canvas.Height)
            {
                // If the bouns top reaches the bottom of the canvas, then it can be removed.
                return true;
            }
            else
            {
                // If the bouns top didn't reaches the bottom of the canvas, then move it down.
                PositionY += speed;
                return false;
            }
        }

        #endregion Methods
    }
}
