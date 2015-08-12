using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class SceneCharacter
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("area")]
        public string Area { get; set; }
        public Rectangle AreaRect { get; set; }
        
        [XmlAttribute("image")]
        public string Image { get; set; }

        [XmlIgnore]
        public Texture2D ImageTexture { get; set; }

        [XmlAttribute("visible")]
        public bool Visible { get; set; }

        public void LoadContent(TextureManager tm)
        {
            ImageTexture = tm.GetTextureAddUser(Image);
        }

        public void UnloadContent(TextureManager tm)
        {
            ImageTexture = null;
            tm.RemoveUser(Image);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(ImageTexture, AreaRect, Color.White);
            }
        }
    }
}
