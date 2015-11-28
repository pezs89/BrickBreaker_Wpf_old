using System.Linq;
using System.IO;
using System.Xml.Linq;

namespace BrickBreaker
{
    /// <summary>
    /// Base class for HighScores_Class element.
    /// </summary>
    class HighScores_Class
    {
        #region Fields

        string player_name;
        // The player name.

        string score;
        // The score.

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public string Score
        {
            get { return score; }
            set { score = value; }
        }

        /// <summary>
        /// Gets or sets the player name.
        /// </summary>
        /// <value>
        /// The player name.
        /// </value>
        public string Player_name
        {
            get { return player_name; }
            set { player_name = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScores_Class"/> class.
        /// </summary>
        /// <param name="player_name">The player_name.</param>
        /// <param name="score">The score.</param>
        public HighScores_Class(string player_name, string score)
        {
            this.score = score;
            this.player_name = player_name;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates the xml file if it doesn't exist.
        /// </summary>
        public void Create(string playerName, int playerScore)
        {
            if (!File.Exists("Scores.xml"))
            {
                if (!string.IsNullOrEmpty(playerName))
                {
                    playerName = "";
                }

                XAttribute Data = new XAttribute("ID", 1);
                XElement Name = new XElement("Name", playerName);
                XElement Score = new XElement("Score", playerScore.ToString());
                XElement newElement = new XElement("Datas", Data, Name, Score);
                XDocument newXML = new XDocument(newElement);
                newXML.Save("Scores.xml");
                
            }
        }

        /// <summary>
        /// Injection to xml.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="points">The points.</param>
        public void InjectiontoXML(string name, int points)
        {
            if (!File.Exists("Scores.xml"))
            {
                XDocument settingsFromXml = XDocument.Load("Scores.xml");
                var readDataFromXml = settingsFromXml.Descendants("Data");
                var fromXml = from x in readDataFromXml
                              select x;
                // Load the values stored in the xml.

                int idNumber = 0;

                foreach (var oneElement in fromXml)
                {
                    idNumber += 1;
                }
                // See how many records are in the file.

                if (idNumber < 10)
                {
                    // Only add a new record if there are less than 10.
                    settingsFromXml.Root.Add(new XElement("Data", new XAttribute("ID", idNumber++), new XElement("Name", name), new XElement("Score", points)));
                    settingsFromXml.Save("Scores.xml");
                }
                else
                {
                    // If there are 10 records, then overwrite the smallest.
                    var toOverwrite = fromXml.Min(x => x.Element("Score"));

                    foreach (var oneElement in fromXml)
                    {
                        if (oneElement.Element("Name") == toOverwrite.Element("Name") && oneElement.Element("Score") == toOverwrite.Element("Score"))
                        {
                            oneElement.Element("Name").Value = name;
                            oneElement.Element("Score").Value = points.ToString();
                        }
                    }
                    // Overwrite the minimum element.

                    settingsFromXml.Save("OptionsSettings.xml");
                    // Save the changes in the values of the xml.
                }
            }
            else
            {
                // If the file doesn't exist, then create a new one.
                Create(name, points);
            }
        }

        /// <summary>
        /// Orders the elements.
        /// </summary>
        public void OrderBy()
        {
            XElement root = XElement.Load("Scores.xml");
            var scores = root.Elements("Data").OrderByDescending(xtab => (int)xtab.Element("Score")).ToArray();
            root.RemoveAll();

            int idNumber = 0;
            foreach (XElement score in scores)
            {
                idNumber += 1;
                score.Attribute("ID").Value = idNumber.ToString();
                root.Add(score);
            }

            root.Save("Scores.xml");
        }

        #endregion Methods
    }
}
