using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class MenuItem
    {
        Menu menu;

        private string text;
        public string Text
        {
            get { return text; }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        private Rectangle area;
        public Rectangle Area
        {
            get { return area; }
        }

        private float selectionFade;

        public MenuItem(Menu parentmenu, string txt, Vector2 pos)
        {
            menu = parentmenu;
            text = txt;
            position = new Vector2(menu.ScreenManager.GameManager.Game.Window.ClientBounds.Width / 2, 80) + pos;

            Vector2 textOrigin = menu.ScreenManager.MenuFont.MeasureString(text) / 2;
            Vector2 areaPos = position - textOrigin;
            Vector2 areaSize =  menu.ScreenManager.MenuFont.MeasureString(text);
            area = new Rectangle((int)areaPos.X, (int)areaPos.Y, (int)areaSize.X, (int)areaSize.Y);

        }

        public MenuItem(Menu parentmenu, string txt, Vector2 relative_pos, MenuItem parentitem)
        {
            menu = parentmenu;
            text = txt;
            Vector2 textSize = menu.ScreenManager.MenuFont.MeasureString("txt");
            textSize.X = 0;

            position = parentitem.Position + textSize + relative_pos;

            Vector2 textOrigin = menu.ScreenManager.MenuFont.MeasureString(text) / 2;
            Vector2 areaPos = position - textOrigin;
            Vector2 areaSize = menu.ScreenManager.MenuFont.MeasureString(text);
            area = new Rectangle((int)areaPos.X, (int)areaPos.Y, (int)areaSize.X, (int)areaSize.Y);
        }

        public void Update(GameTime gameTime, bool isSelected)
        {
            // When the menu selection changes, entries gradually fade between
            // their selected and deselected appearance, rather than instantly
            // popping to the new state.
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool isSelected)
        {
            Vector2 textOrigin = menu.ScreenManager.MenuFont.MeasureString(text) / 2;
            Color textColor = isSelected ? Color.Yellow : Color.White;

            // Pulsate the size of the selected menu entry.
            double time = gameTime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(time * 6) + 1;
            float textScale = 1 + pulsate * 0.05f * selectionFade;

            spriteBatch.DrawString(menu.ScreenManager.MenuFont, text, position, textColor, 0, textOrigin, textScale, SpriteEffects.None, 0);
        }
    }
}
