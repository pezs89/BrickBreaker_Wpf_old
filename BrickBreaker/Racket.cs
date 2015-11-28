using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BrickBreaker
{
    /// <summary>
    /// Base class for racket game element.
    /// </summary>
    class Racket
    {
        #region Fields

        private double positionX;
        // The x coordinate of the racket.

        private double positionY;
        // The y coordinate of the racket.

        private double height;
        // The height of the racket.

        private double width;
        // The width of the racket.

        private bool stickyRacket;
        // Bonus: the racket gets sticky, so the ball sticks to it.

        private string racketImage;
        // The image of the racket.

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
        /// Gets or sets the stickyRacket.
        /// </summary>
        /// <value>
        /// The stickyRacket.
        /// </value>
        public bool StickyRacket
        {
            get { return stickyRacket; }
            set { stickyRacket = value; }
        }

        /// <summary>
        /// Gets or sets the racketImage.
        /// </summary>
        /// <value>
        /// The racketImage.
        /// </value>
        public string RacketImage
        {
            get { return racketImage; }
            set { racketImage = value; }
        }

        #endregion Parameters

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Racket"/> class.
        /// </summary>
        /// <param name="positionx">The positionx.</param>
        /// <param name="positiony">The positiony.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="racketimage">The racketimage.</param>
        public Racket(double positionx, double positiony, double height, double width, string racketimage)
        {
            positionX = positionx;
            positionY = positiony;
            this.height = height;
            this.width = width;
            racketImage = racketimage;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Shape of the racket.
        /// </summary>
        /// <returns>ballEllipse</returns>
        public Rectangle GetRectangle()
        {
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(racketImage, UriKind.Relative));

            Rectangle racketRectangle = new Rectangle();
            racketRectangle.Fill = imgBrush;
            racketRectangle.Width = width;
            racketRectangle.Height = height;

            Binding rectangleXBinding = new Binding("PositionX");
            rectangleXBinding.Source = this;
            racketRectangle.SetBinding(Canvas.LeftProperty, rectangleXBinding);

            Binding rectangleYBinding = new Binding("PositionY");
            rectangleYBinding.Source = this;
            racketRectangle.SetBinding(Canvas.TopProperty, rectangleYBinding);

            return racketRectangle;
        }

        /// <summary>
        /// Handles the movement of the mouse on the canvas.
        /// </summary>
        /// <param name="mouseOnCanvas">The mouseOnCanvas.</param>
        /// <param name="canvas">The canvas.</param>
        public void CanvasMouseMove(Point mouseOnCanvas, Canvas canvas)
        {
            if (mouseOnCanvas.X <= Width / 2)
            {
                // Left side of the canvas.
                PositionX = 0;
            }
            else if (mouseOnCanvas.X + Width / 2 >= canvas.Width)
            {
                // Rigth side of the canvas.
                PositionX = canvas.Width - Width;
            }
            else
            {
                // Between the sides.
                PositionX = mouseOnCanvas.X - Width / 2;
            }
        }

        /// <summary>
        /// Handles the movement with keys.
        /// </summary>
        /// <param name="horizontalMovement">The horizontalMovement.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="canvas">The canvas.</param>
        public void MoveKey(double horizontalMovement, string direction, Canvas canvas)
        {
            if (direction.ToLower() == "left")
            {
                // Movement to left side.
                if (PositionX <= 0)
                {
                    // Left side of the canvas.
                    PositionX = 0;
                }
                else
                {
                    // Move left.
                    PositionX -= horizontalMovement;
                }
            }
            else if(direction.ToLower() == "right")
            {
                // Movement to right side.
                if (PositionX + Width >= canvas.Width)
                {
                    // Right side of the canvas.
                    PositionX = canvas.Width - Width;
                }
                else
                {
                    // Move right.
                    PositionX += horizontalMovement;
                }
            }
        }

        #endregion Methods
    }
}
