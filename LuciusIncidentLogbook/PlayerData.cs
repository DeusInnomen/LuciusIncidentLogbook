using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace KitchenGeeks
{
    /// <summary>
    /// Represents a single Player in the system.
    /// </summary>
    public class PlayerRecord
    {
        /// <summary>
        /// The ID value of this PlayerRecord object.
        /// </summary>
        public string ID
        {
            get
            {
                return _id;
            }
        }
        /// <summary>
        /// The player's First Name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The player's Last Name.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The player's Email Address.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The player's name on the official Malifaux forums.
        /// </summary>
        public string ForumName { get; set; }
        /// <summary>
        /// The player's hometown.
        /// </summary>
        public string Hometown { get; set; }
        /// <summary>
        /// The region the player is from. This is less granular than Hometown, and is used in match making
        /// calculations.
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// The player's preferred Faction.
        /// </summary>
        public Factions Faction { get; set; }
        /// <summary>
        /// Returns the full name of the player, or just the first name if no Last Name is given.
        /// </summary>
        public string Name
        {
            get
            {
                if (LastName.Length > 0)
                    return FirstName + " " + LastName;
                else
                    return FirstName;
            }
        }

        private string _id;

        /// <summary>
        /// Create a brand new PlayerRecord object.
        /// </summary>
        /// <param name="newID">The ID value to assign to the player.</param>
        public PlayerRecord()
        {
            _id = Guid.NewGuid().ToString();
            FirstName = "";
            LastName = "";
            Email = "";
            ForumName = "";
            Hometown = "";
            Region = "";
            Faction = Factions.Undeclared;
        }

        /// <summary>
        /// Load a PlayerRecord object from the provided XmlNode object.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public PlayerRecord(XmlNode sourceNode)
        {
            XmlAttribute idAttrib = sourceNode.Attributes["ID"];
            if (idAttrib != null)
                _id = idAttrib.Value;
            else
                throw new ApplicationException("The Player Record is missing its ID attribute.");

            FirstName = "";
            LastName = "";
            Email = "";
            ForumName = "";
            Hometown = "";
            Region = "";
            Faction = Factions.Undeclared;

            XmlNode node = sourceNode.SelectSingleNode("FirstName");
            if (node != null)
                FirstName = node.InnerText;
            node = sourceNode.SelectSingleNode("LastName");
            if (node != null)
                LastName = node.InnerText;
            node = sourceNode.SelectSingleNode("Email");
            if (node != null)
                Email = node.InnerText;
            node = sourceNode.SelectSingleNode("ForumName");
            if (node != null)
                ForumName = node.InnerText;
            node = sourceNode.SelectSingleNode("Hometown");
            if (node != null)
                Hometown = node.InnerText;
            node = sourceNode.SelectSingleNode("Region");
            if (node != null)
                Region = node.InnerText;
            node = sourceNode.SelectSingleNode("Faction");
            if (node != null)
                Faction = (Factions)Enum.Parse(typeof(Factions), node.InnerText);
        }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXML(XmlDocument xml)
        {
            XmlNode baseNode = xml.CreateElement("Player");
            baseNode.Attributes.Append(xml.CreateAttribute("ID")).Value = ID.ToString();
            baseNode.AppendChild(xml.CreateElement("FirstName")).InnerText = FirstName;
            baseNode.AppendChild(xml.CreateElement("LastName")).InnerText = LastName;
            baseNode.AppendChild(xml.CreateElement("Email")).InnerText = Email;
            baseNode.AppendChild(xml.CreateElement("ForumName")).InnerText = ForumName;
            baseNode.AppendChild(xml.CreateElement("Hometown")).InnerText = Hometown;
            baseNode.AppendChild(xml.CreateElement("Region")).InnerText = Region;
            baseNode.AppendChild(xml.CreateElement("Faction")).InnerText = Faction.ToString();

            return baseNode;
        }
    }
}
