using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace KitchenGeeks
{
    /// <summary>
    /// Contains all of the details and results for a single League. This can be an Achievement league, a
    /// standard gameplay league, or both.
    /// </summary>
    public class League
    {
        /// <summary>
        /// The name of the League.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Location associated with the League.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The starting date of the League.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// The ending date of the League.
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// The scoring Format of the League, if any.
        /// </summary>
        public EventFormat Format { get; set; }
        /// <summary>
        /// Notes and information about the League.
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// The URL where this event's post is on the official Wyrd forums.
        /// </summary>
        public string ForumURL { get; set; }
        /// <summary>
        /// If true, the Event Organizer has already sent a Pre-Event Report.
        /// </summary>
        public bool PreEventSent { get; set; }
        /// <summary>
        /// If true, the Event Organizer has already sent a Post-Event Report.
        /// </summary>
        public bool PostEventSent { get; set; }

        /// <summary>
        /// The Players involved in the League.
        /// </summary>
        public List<string> Players
        {
            get
            {
                List<string> players = new List<string>();
                foreach (MatchResult match in MatchesPlayed)
                    if (!players.Contains(match.PlayerID)) players.Add(match.PlayerID);
                return players;
            }
        }
        /// <summary>
        /// The Achievements available during this League.
        /// </summary>
        public List<Achievement> Achievements { get; set; }
        /// <summary>
        /// The Matches played and recorded during this League.
        /// </summary>
        public List<MatchResult> MatchesPlayed { get; set; }

        /// <summary>
        /// Declares a new League instance.
        /// </summary>
        public League()
        {
            Name = "";
            Location = "";
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Format = EventFormat.None;
            Notes = "";
            ForumURL = "";
            PreEventSent = false;
            PostEventSent = false;

            Achievements = new List<Achievement>();
            MatchesPlayed = new List<MatchResult>();
        }

        /// <summary>
        /// Load a League object using the provided XmlNode object.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public League(XmlNode sourceNode)
            : this()
        {
            XmlNode node = sourceNode.SelectSingleNode("Name");
            if (node != null)
                Name = node.InnerText;
            node = sourceNode.SelectSingleNode("Location");
            if (node != null)
                Location = node.InnerText;
            node = sourceNode.SelectSingleNode("StartDate");
            if(node != null)
                StartDate = DateTime.ParseExact(node.InnerText, "yyyyMMdd", null);
            node = sourceNode.SelectSingleNode("EndDate");
            if(node != null)
                EndDate = DateTime.ParseExact(node.InnerText, "yyyyMMdd", null);
            node = sourceNode.SelectSingleNode("Format");
            if (node != null)
                Format = (EventFormat)Enum.Parse(typeof(EventFormat), node.InnerText);
            node = sourceNode.SelectSingleNode("Notes");
            if (node != null)
                Notes = node.InnerText;
            node = sourceNode.SelectSingleNode("ForumURL");
            if (node != null)
                ForumURL = node.InnerText;
            node = sourceNode.SelectSingleNode("PreEventSent");
            if (node != null)
                PreEventSent = Convert.ToBoolean(node.InnerText);
            node = sourceNode.SelectSingleNode("PostEventSent");
            if (node != null)
                PostEventSent = Convert.ToBoolean(node.InnerText);

            XmlNodeList achievementNodes = sourceNode.SelectNodes("Achievements/Achievement");
            foreach (XmlNode achievementNode in achievementNodes)
                Achievements.Add(new Achievement(achievementNode));

            XmlNodeList matchNodes = sourceNode.SelectNodes("Results/Result");
            foreach (XmlNode matchNode in matchNodes)
                MatchesPlayed.Add(new MatchResult(matchNode));
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXML(XmlDocument xml)
        {
            XmlNode baseNode = xml.CreateElement("League");
            baseNode.AppendChild(xml.CreateElement("Name")).InnerText = Name;
            baseNode.AppendChild(xml.CreateElement("Location")).InnerText = Location;
            baseNode.AppendChild(xml.CreateElement("StartDate")).InnerText = StartDate.ToString("yyyyMMdd");
            baseNode.AppendChild(xml.CreateElement("EndDate")).InnerText = EndDate.ToString("yyyyMMdd");
            baseNode.AppendChild(xml.CreateElement("Format")).InnerText = Format.ToString();
            baseNode.AppendChild(xml.CreateElement("Notes")).AppendChild(xml.CreateCDataSection(Notes));
            baseNode.AppendChild(xml.CreateElement("ForumURL")).InnerText = ForumURL;
            baseNode.AppendChild(xml.CreateElement("PreEventSent")).InnerText = PreEventSent.ToString();
            baseNode.AppendChild(xml.CreateElement("PostEventSent")).InnerText = PostEventSent.ToString();

            XmlNode achievementNode = baseNode.AppendChild(xml.CreateElement("Achievements"));
            foreach (Achievement achievement in Achievements)
                achievementNode.AppendChild(achievement.ToXML(xml));

            XmlNode matchesNode = baseNode.AppendChild(xml.CreateElement("Results"));
            foreach (MatchResult result in MatchesPlayed)
                matchesNode.AppendChild(result.ToXML(xml));

            return baseNode;
        }

        /// <summary>
        /// Returns the results of a given player in the League.
        /// </summary>
        /// <param name="ID">The ID of the player to look up.</param>
        /// <returns>A LeagueResults object containing the player's results, or null if the player was not
        /// found.</returns>
        public LeagueResults GetPlayerResults(string ID)
        {
            LeagueResults results = new LeagueResults(ID);
            bool found = false;
            foreach (MatchResult match in MatchesPlayed)
                if (match.PlayerID == ID)
                {
                    found = true;
                    results.Achievements.AddRange(match.Achievements);
                    results.MatchResults.VictoryPoints += match.VictoryPoints;
                    results.MatchResults.TournamentPoints += match.TournamentPoints;
                    results.MatchResults.Differential += match.Differential;
                }

            return found ? results : null;
        }
    }

    /// <summary>
    /// A single Achievement that can be scored in a league or tournament.
    /// </summary>
    public class Achievement
    {
        /// <summary>
        /// The name or description of the Achievement.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The category of this achievement, e.g. "Painting".
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// The maximum number of times this Achievement a player can earn this Achievement.
        /// </summary>
        public int MaxAllowed { get; set; }
        /// <summary>
        /// The number of times a player has earned this Achievement.
        /// </summary>
        public int Earned
        {
            get { return _earned; }
            set
            {
                if (value > MaxAllowed) value = MaxAllowed;
                _earned = value;
            }
        }
        /// <summary>
        /// The number of points gained each time the Achievement is earned.
        /// </summary>
        public int Points { get; set; }
        /// <summary>
        /// If true, the player earns the points each time they earn the Achievement. Otherwise, they gain the
        /// points only once for this Achievement.
        /// </summary>
        public bool PointsEarnedEachTime { get; set; }
        /// <summary>
        /// If true, the player must earn the Achievement a number of times equal to the MaxAllowed value in order
        /// to score the points.
        /// </summary>
        public bool MustEarnAllToGetPoints { get; set; }

        private int _earned;

        /// <summary>
        /// Declares a new instance of the Achievement class.
        /// </summary>
        public Achievement()
        {
            Name = "";
            Category = "";
            MaxAllowed = 0;
            _earned = 0;
            Points = 1;
            PointsEarnedEachTime = true;
            MustEarnAllToGetPoints = false;
        }

        /// <summary>
        /// Declares a new instance of the Achievement class.
        /// </summary>
        /// <param name="name">The name or description of the Achievement.</param>
        /// <param name="category">The category of this Achievement, e.g. "Painting".</param>
        /// <param name="maxAllowed">The maximum number of times a player can earn this Achievement.</param>
        /// <param name="points">The number of points gained each time the Achievement is earned.</param>
        /// <param name="earnedEachTime">If true, the player earns the points each time they earn the
        /// Achievement. Otherwise, they gain the points only once for this Achievement.</param>
        /// <param name="mustEarnAll">If true, the player must earn the Achievement a number of times equal to
        /// the MaxAllowed value in order to score the points.</param>
        public Achievement(string name, string category, int maxAllowed = 1, int points = 1, bool earnedEachTime = true,
            bool mustEarnAll = false)
            : this()
        {
            Name = name;
            Category = category;
            MaxAllowed = maxAllowed;
            Points = points;
            PointsEarnedEachTime = earnedEachTime;
            MustEarnAllToGetPoints = mustEarnAll;
        }

        /// <summary>
        /// Loads an instance of the Achievement class from a XML node.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public Achievement(XmlNode sourceNode)
            : this()
        {
            XmlNode node = sourceNode.SelectSingleNode("Name");
            if (node != null)
                Name = node.InnerText;
            node = sourceNode.SelectSingleNode("Category");
            if (node != null)
                Category = node.InnerText;
            node = sourceNode.SelectSingleNode("MaxAllowed");
            if (node != null)
                MaxAllowed = Convert.ToInt32(node.InnerText);
            node = sourceNode.SelectSingleNode("Earned");
            if (node != null)
                Earned = Convert.ToInt32(node.InnerText);
            node = sourceNode.SelectSingleNode("Points");
            if (node != null)
                Points = Convert.ToInt32(node.InnerText);
            node = sourceNode.SelectSingleNode("PointsEarnedEachTime");
            if (node != null)
                PointsEarnedEachTime = Convert.ToBoolean(node.InnerText);
            node = sourceNode.SelectSingleNode("MustEarnAllToGetPoints");
            if (node != null)
                MustEarnAllToGetPoints = Convert.ToBoolean(node.InnerText);
        }

        /// <summary>
        /// Returns a string representation of this Achievement object, specifically its Name prepended by the
        /// Category if available.
        /// </summary>
        /// <returns>A string that represents this Achievement object.</returns>
        public override string ToString()
        {
            return Category + (Category.Length > 0 ? ": " : "") + Name;
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXML(XmlDocument xml)
        {
            XmlNode baseNode = xml.CreateElement("Achievement");
            baseNode.AppendChild(xml.CreateElement("Name")).InnerText = Name;
            baseNode.AppendChild(xml.CreateElement("Category")).InnerText = Category;
            baseNode.AppendChild(xml.CreateElement("MaxAllowed")).InnerText = MaxAllowed.ToString();
            if (Earned > 0)
                baseNode.AppendChild(xml.CreateElement("Earned")).InnerText = Earned.ToString();
            baseNode.AppendChild(xml.CreateElement("Points")).InnerText = Points.ToString();
            baseNode.AppendChild(xml.CreateElement("PointsEarnedEachTime")).InnerText = PointsEarnedEachTime.ToString();
            baseNode.AppendChild(xml.CreateElement("MustEarnAllToGetPoints")).InnerText = MustEarnAllToGetPoints.ToString();

            return baseNode;
        }

        /// <summary>
        /// Return a shallow clone of this Achievement object.
        /// </summary>
        /// <returns>An Achievement object containing a copy of the information within this Achievement object.</returns>
        public Achievement Clone()
        {
            var copy = new Achievement()
                {
                    Earned = this.Earned,
                    Category = this.Category,
                    MaxAllowed = this.MaxAllowed,
                    MustEarnAllToGetPoints = this.MustEarnAllToGetPoints,
                    Name = this.Name,
                    Points = this.Points,
                    PointsEarnedEachTime = this.PointsEarnedEachTime
                };
            return copy;
        }
    }

    /// <summary>
    /// Represents a single player's results in a League.
    /// </summary>
    public class LeagueResults
    {
        /// <summary>
        /// The ID of the player who earned these results.
        /// </summary>
        public string PlayerID { get; set; }
        /// <summary>
        /// The list of Achievements earned by this player.
        /// </summary>
        public List<Achievement> Achievements { get; set; }
        /// <summary>
        /// The results of the Matches played by the player.
        /// </summary>
        public MatchResult MatchResults { get; set; }

        /// <summary>
        /// Declares a new instance of the LeagueResults class.
        /// </summary>
        /// <param name="ID">The ID of the player associated with this instance.</param>
        public LeagueResults(string ID)
        {
            PlayerID = ID;
            Achievements = new List<Achievement>();
            MatchResults = new MatchResult(ID);
        }

        /// <summary>
        /// Load a LeagueResults object using the provided XmlNode object.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public LeagueResults(XmlNode sourceNode)
        {
            Achievements = new List<Achievement>();
            XmlAttribute attrib = sourceNode.Attributes["ID"];
            if (attrib != null)
            {
                PlayerID = attrib.Value;

                XmlNode node = sourceNode.SelectSingleNode("Results");
                if (node != null)
                    MatchResults = new MatchResult(node);

                XmlNodeList achievements = sourceNode.SelectNodes("Achievement");
                foreach (XmlNode achievement in achievements)
                    Achievements.Add(new Achievement(achievement));
            }
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXML(XmlDocument xml)
        {
            XmlNode achievementsNode = xml.CreateElement("Achievements");
            achievementsNode.Attributes.Append(xml.CreateAttribute("ID")).Value = PlayerID;
            achievementsNode.AppendChild(MatchResults.ToXML(xml));
            foreach (Achievement achievement in Achievements)
                achievementsNode.AppendChild(achievement.ToXML(xml));
            return achievementsNode;
        }

        /// <summary>
        /// Returns the number of Achievement points earned by the player.
        /// </summary>
        /// <param name="category">If provided, limits the results to just the given category.</param>
        /// <returns>The number of Achievement points earned.</returns>
        public int PointsEarned(string category = null)
        {
            int points = 0;
            foreach (Achievement achievement in Achievements)
            {
                if (category != null && achievement.Category.ToLower() != category.ToLower()) continue;

                if (achievement.MustEarnAllToGetPoints)
                {
                    if (achievement.Earned == achievement.MaxAllowed)
                        if (achievement.PointsEarnedEachTime)
                            points += (achievement.Points * achievement.Earned);
                        else
                            points += achievement.Points;
                }
                else
                {
                    if (achievement.PointsEarnedEachTime)
                        points += (achievement.Points * achievement.Earned);
                    else if (achievement.Earned > 0)
                        points += achievement.Points;
                }
            }
            return points;
        }

        /// <summary>
        /// Returns the number of individual Achievements earned by the player.
        /// </summary>
        /// <param name="countEachAchievementOnce">If true, each Achievement earned counts as one, regardless of
        /// how many times it was earned.</param>
        /// <param name="category">If provided, limits the results to just the given category.</param>
        /// <returns>The number of individual Achievements earned by the player.</returns>
        public int QuantityEarned(bool countEachAchievementOnce = false, string category = null)
        {
            int quantity = 0;
            foreach (Achievement achievement in Achievements)
                if (category == null || achievement.Category.ToLower() == category.ToLower())
                    if (countEachAchievementOnce)
                        quantity += achievement.Earned > 0 ? 1 : 0;
                    else
                        quantity += achievement.Earned;
            return quantity;
        }

    }
}
