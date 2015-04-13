using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BBVisNov
{
    public class Hud
    {
        ScreenManager screenManager;

        Texture2D background_texture;

        Rectangle area_header;

        private Vector2 mouseHoverTextPosition;
        private string mouseHoverText;
        public string MouseHoverText
        {
            get { return mouseHoverText; }
            set { mouseHoverText = value; }
        }
        
        public Hud(ScreenManager sm)
        {
            screenManager = sm;
        }

        public void Initialize()
        {
            area_header = new Rectangle(0, 0, screenManager.GameManager.Game.Window.ClientBounds.Width, 25);

            mouseHoverText = "";
            mouseHoverTextPosition = new Vector2(screenManager.GameManager.Game.Window.ClientBounds.Width / 2, 0);
        }

        public void LoadContent()
        {
            background_texture = new Texture2D(screenManager.GameManager.GraphicsDevice, 1, 1);
            background_texture.SetData<Color>(new Color[] { Color.FromNonPremultiplied(255, 255, 255, 128) });
        }

        public void UnloadContent()
        {
            background_texture = null;
        }

        public void Update(GameTime gameTime)
        {
            if(MouseHoverText != "")
            {
                screenManager.CursorColor = Color.Red;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background_texture, area_header, Color.Black);

            if (MouseHoverText != "")
            {
                Vector2 textOrigin = screenManager.HudFont.MeasureString(MouseHoverText) / 2;
                textOrigin.Y = 0;

                spriteBatch.DrawString(screenManager.HudFont, MouseHoverText, mouseHoverTextPosition, Color.Red, 0, textOrigin, 1, SpriteEffects.None, 0);
            }
        }
    }
}
