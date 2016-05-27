using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BBVisNov
{
    [XmlRoot("Story")]
    public class Story
    {
        [XmlElement("Name")]
        public string Name;

        [XmlElement("Description")]
        public string Description;

        [XmlElement("InitialScene")]
        public string InitialScene;
    }
}
