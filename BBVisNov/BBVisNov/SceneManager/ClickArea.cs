using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;

namespace BBVisNov
{
    public class ClickArea
    {
        [XmlAttribute("area")]
        public string Area { get; set; }
        public Rectangle AreaRect { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("action")]
        public string Action { get; set; }

    }
}
