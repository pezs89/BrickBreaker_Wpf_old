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
    /// Base class for ball game element.
    /// </summary>
    class Ball
    {
        #region Fields

        private double positionX;
        // The x coordinate of the ball.

        private double positionY;
        // The y coordinate of the ball.

        private double radius;
        // The radius of the ball.

        private double horizontalMovement;
        // The horizontal movement of the ball.

        private double verticalMovement;
        // The vertical movement of the ball.

        private string ballImage;
        // The image of the ball.

        private bool ballInMove;
        // Shows if the ball is in move.

        public enum ballType
        {
            Normal,
            Hard,
            Steel
        }
        // The types of the balls.

        private ballType typeOfBall;
        // The type of the ball.

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
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal movement.
        /// </summary>
        /// <value>
        /// The horizontal movement.
        /// </value>
        public double HorizontalMovement
        {
            get { return horizontalMovement; }
            set { horizontalMovement = value; }
        }


        /// <summary>
        /// Gets or sets the vertical movement.
        /// </summary>
        /// <value>
        /// The vertical movement.
        /// </value>
        public double VerticalMovement
        {
            get { return verticalMovement; }
            set { verticalMovement = value; }
        }

        /// <summary>
        /// Gets or sets the ballImage.
        /// </summary>
        /// <value>
        /// The ballImage.
        /// </value>
        public string BallImage
        {
            get { return ballImage; }
            set { ballImage = value; }
        }

        /// <summary>
        /// Gets or sets the typeOfBall.
        /// </summary>
        /// <value>
        /// The typeOfBall.
        /// </value>
        public ballType TypeOfBall
        {
            get { return typeOfBall; }
            set { typeOfBall = value; }
        }

        /// <summary>
        /// Gets or sets the ballInMove.
        /// </summary>
        /// <value>
        /// The ballInMove.
        /// </value>
        public bool BallInMove
        {
            get { return ballInMove; }
            set { ballInMove = value; }
        }

        #endregion Parameters

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Ball"/> class.
        /// </summary>
        /// <param name="positionx">The positionx.</param>
        /// <param name="positiony">The positiony.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="typeofball">The typeofball.</param>
        /// <param name="ballimage">The ballimage.</param>
        public Ball(double positionx, double positiony, double radius, ballType typeofball, string ballimage)
        {
            positionX = positionx;
            positionY = positiony;
            this.radius = radius;
            typeOfBall = typeofball;
            ballImage = ballimage;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Shape of the ball.
        /// </summary>
        /// <returns>ballEllipse</returns>
        public Ellipse GetEllipse()
        {
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(ballImage, UriKind.Relative));

            Ellipse ballEllipse = new Ellipse();
            ballEllipse.Fill = imgBrush;
            ballEllipse.Width = radius * 2;
            ballEllipse.Height = radius * 2;

            Binding ellipseXBinding = new Binding("PositionX");
            ellipseXBinding.Source = this;
            ballEllipse.SetBinding(Canvas.LeftProperty, ellipseXBinding);

            Binding ellipseYBinding = new Binding("PositionY");
            ellipseYBinding.Source = this;
            ballEllipse.SetBinding(Canvas.TopProperty, ellipseYBinding);

            return ballEllipse;
        }

        /// <summary>
        /// Movement of the ball.
        /// </summary>
        public void Move(double speed)
        {
            PositionX += VerticalMovement * speed;
            PositionY += HorizontalMovement * speed;
        }

        /// <summary>
        /// Handles the movement with the mouse on the canvas.
        /// </summary>
        /// <param name="mouseOnCanvas">The mouseOnCanvas.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="racket">The racket.</param>
        public void CanvasMouseMove(Point mouseOnCanvas, Canvas canvas, Racket racket)
        {
// TODO: bug: the relative position on the racket gets overwritten
            if (PositionX - racket.PositionX < 0)
            {
                // The ball's x position is before the racket's x position.
                if (mouseOnCanvas.X <= 0)
                {
                    PositionX = 0;
                }
                else if (mouseOnCanvas.X + (racket.PositionX + racket.Width - positionX) >= canvas.Width) //
                {
                    PositionX = canvas.Width - (racket.PositionX + racket.Width - positionX);
                }
                else
                {
                    PositionX = mouseOnCanvas.X;
                }
            }
            else
            {
                // The ball's x position is after the racket's x position.
                if (mouseOnCanvas.X <= (PositionX - racket.PositionX))
                {
                    PositionX = (PositionX - racket.PositionX);
                }
                else if (mouseOnCanvas.X + (PositionX - racket.PositionX) >= canvas.Width)
                {
                    PositionX = canvas.Width - (PositionX - racket.PositionX);
                }
                else
                {
                    PositionX = mouseOnCanvas.X;
                }
            }
        }

        /// <summary>
        /// Handles the movement with keys.
        /// </summary>
        /// <param name="horizontalDisplacement">The horizontalDisplacement.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="racket">The racket.</param>
        public void MoveKey(double horizontalDisplacement, string direction, Canvas canvas, Racket racket)
        {
// TODO: bug: the relative position on the racket gets overwritten
            if (direction.ToLower() == "left")
            {
                if ((PositionX - racket.PositionX) < 0)
                {
                    // The ball's x position is before the racket's x position.
                    if (PositionX <= 0)
                    {
                        PositionX = 0;
                    }
                    else
                    {
                        PositionX -= horizontalDisplacement;
                    }
                }
                else
                {
                    // The ball's x position is after the racket's x position.
                    if (PositionX <= (PositionX - racket.PositionX))
                    {
                        PositionX = (PositionX - racket.PositionX);
                    }
                    else
                    {
                        PositionX -= horizontalDisplacement;
                    }
                }
            }
            else if (direction.ToLower() == "right")
            {
                if ((racket.PositionX + racket.Width - PositionX) < Radius)
                {
                    // The ball's x position is before the racket's x position.
                    if (PositionX + (racket.PositionX + racket.Width - PositionX) >= canvas.ActualWidth)
                    {
                        PositionX = canvas.ActualWidth - (racket.PositionX + racket.Width - PositionX);
                    }
                    else
                    {
                        PositionX += horizontalDisplacement;
                    }
                }
                else
                {
                    // The ball's x position is after the racket's x position.
                    if (PositionX >= canvas.ActualWidth - (racket.PositionX + racket.Width - PositionX))
                    {
                        PositionX = canvas.ActualWidth - (racket.PositionX + racket.Width - PositionX);
                    }
                    else
                    {
                        PositionX += horizontalDisplacement;
                    }
                }
            }
        }

        #endregion Methods
    }
}
