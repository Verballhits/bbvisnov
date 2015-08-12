using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    [XmlRoot("Items")]
    public class ItemList
    {
        [XmlElement("Item")]
        public List<Item> Items { get; set; }

        public ItemList()
        {
            Items = new List<Item>();
        }
    }

    public class Item
    {
        [XmlAttribute("id")]
        public int ItemID { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("image")]
        public string Image { get; set; }

        [XmlIgnore]
        public Texture2D ImageTexture { get; set; }

        [XmlAttribute("value")]
        public int Value { get; set; }

        public void LoadContent(TextureManager tm)
        {
            ImageTexture = tm.GetTextureAddUser(Image);
        }

        public void UnloadContent(TextureManager tm)
        {
            ImageTexture = null;
            tm.RemoveUser(Image);
        }
    }
}
