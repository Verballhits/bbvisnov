using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BBVisNov
{
    public class Quest
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        public QuestState State { get; set; }
    }
}
