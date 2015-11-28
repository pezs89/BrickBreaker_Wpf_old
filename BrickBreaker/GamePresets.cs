using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    /// <summary>
    /// Base class for GamePresets game element.
    /// </summary>
    class GamePresets
    {
        #region Fields

        private int scorePoint;
        // The scorepoint.

        private int lifePoint;
        // The lifepoint.

        #endregion Fields

        #region Properties

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
        /// Gets or sets the lifePoint.
        /// </summary>
        /// <value>
        /// The lifePoint.
        /// </value>
        public int LifePoint
        {
            get { return lifePoint; }
            set { lifePoint = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePresets"/> class.
        /// </summary>
        /// <param name="lifepoint">The lifepoint.</param>
        /// <param name="scorepoint">The scorepoint.</param>
        public GamePresets(int lifepoint, int scorepoint)
        {
            lifePoint = lifepoint;
            scorePoint = scorepoint;
        }

        #endregion Constructors

        #region Methods

        #endregion Methods
    }
}
