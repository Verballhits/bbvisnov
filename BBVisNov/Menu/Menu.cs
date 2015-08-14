using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BBVisNov
{
    public class Menu
    {
        private List<MenuItem> menuItems = new List<MenuItem>();
        public List<MenuItem> MenuItems
        {
            get { return menuItems; }
        }

        private int selectedItem;

        private ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }

        string menuTitle;

        public Menu(ScreenManager sm, string title)
        {
            screenManager = sm;

            selectedItem = 0;

            menuTitle = title;
        }

        public void Update(GameTime gameTime)
        {
            if (screenManager.GameManager.InputManager.IsNewMenuUpPress())
            {
                selectedItem--;

                if (selectedItem < 0)
                {
                    selectedItem = menuItems.Count - 1;
                }
            }

            if (screenManager.GameManager.InputManager.IsNewMenuDownPress())
            {
                selectedItem++;

                if (selectedItem >= menuItems.Count)
                {
                    selectedItem = 0;
                }
            }

            for (int i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].Update(gameTime, (i == selectedItem));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the menu title centered on the screen
            Vector2 titlePosition = new Vector2(screenManager.GameManager.Game.Window.ClientBounds.Width / 2, 80);
            Vector2 titleOrigin = screenManager.MenuFont.MeasureString(menuTitle) / 2;
            Color titleColor = Color.DarkRed;
            float titleScale = 1.25f;

            spriteBatch.DrawString(screenManager.MenuFont, menuTitle, titlePosition, titleColor, 0, titleOrigin, titleScale, SpriteEffects.None, 0);

            for (int i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].Draw(gameTime, spriteBatch, (i == selectedItem));
            }
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            if (!menuItems.Contains(menuItem))
            {
                menuItems.Add(menuItem);
            }
        }

        public void RemoveMenuItem(MenuItem menuItem)
        {
            if (menuItems.Contains(menuItem))
            {
                menuItems.Remove(menuItem);
            }
        }

        public MenuItem GetSelectedItem()
        {
            return menuItems[selectedItem];
        }

        public void SetSelectedItem(int value)
        {
            selectedItem = value;
        }
    }
}
