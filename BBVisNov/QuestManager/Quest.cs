using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BBVisNov
{
    [XmlRoot("Quests")]
    public class QuestList
    {
        [XmlElement("Quest")]
        public List<Quest> Quests { get; set; }

        public QuestList()
        {
            Quests = new List<Quest>();
        }
    }

    public class Quest : ICloneable
    {
        [XmlAttribute("id")]
        public int QuestId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("desc")]
        public string Description { get; set; }

        [XmlAttribute("nextquest")]
        public int  NextQuest { get; set; }

        [XmlIgnore]
        public QuestState State { get; set; }

        public object Clone()
        {
            Quest copy = new Quest();

            copy.QuestId = QuestId;
            copy.Name = Name;
            copy.Description = Description;
            copy.NextQuest = NextQuest;
            copy.State = State;

            return copy;
        }
    }
}
