using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class SceneItem
    {
        [XmlAttribute("id")]
        public int ItemID { get; set; }

        [XmlAttribute("area")]
        public string Area { get; set; }
        public Rectangle AreaRect { get; set; }

        [XmlAttribute("visible")]
        public bool Visible { get; set; }
    }
}
