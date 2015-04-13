using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class TextureManager
    {
        private GameManager gameManager;

        private Dictionary<string, TextureInstance> texturelist;

        public TextureManager(GameManager gm)
        {
            gameManager = gm;

            texturelist = new Dictionary<string, TextureInstance>();
        }

        public Texture2D GetTextureAddUser(string texturename)
        {
            if (!texturelist.ContainsKey(texturename))
            {
                Texture2D tex = Texture2D.FromStream(gameManager.GraphicsDevice, File.OpenRead("Content/" + texturename));

                texturelist.Add(texturename, new TextureInstance(tex));
            }

            texturelist[texturename].UserCount++;

            return texturelist[texturename].Texture;
        }

        public void RemoveUser(string texturename)
        {
            if (texturelist.ContainsKey(texturename))
            {
                texturelist[texturename].UserCount--;

                if(texturelist[texturename].UserCount <= 0)
                {
                    texturelist.Remove(texturename);
                }
            }
        }
    }

    public class TextureInstance
    {
        public Texture2D Texture { get; set; }
        public int UserCount { get; set; }

        public TextureInstance(Texture2D t)
        {
            Texture = t;
            UserCount = 0;
        }
    }
}
