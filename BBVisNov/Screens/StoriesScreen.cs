using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBVisNov
{
    public class StoriesScreen : GameScreen
    {
        string background_name;
        Texture2D background_texture;

        Rectangle menu_background_area;
        Texture2D menubackground_texture;

        Menu menu;

        public StoriesScreen(ScreenManager sm)
            : base(sm)
        {
            menu = new Menu(sm, "Stories");

            background_name = screenManager.GameManager.StoryManager.ContentFolderGameFull + "Graphics/Backgrounds/menu.jpg";
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            background_texture = screenManager.GameManager.TextureManager.GetTextureAddUser(background_name);

            menubackground_texture = new Texture2D(screenManager.GameManager.GraphicsDevice, 1, 1);
            menubackground_texture.SetData<Color>(new Color[] { Color.FromNonPremultiplied(20, 20, 20, 120) });

            // Create and add menu items (from stories)
            MenuItem item_previous = null;

            foreach (KeyValuePair<string, Story> pair in screenManager.GameManager.StoryManager.AllStories)
            {
                MenuItem item;

                if (item_previous == null)
                {
                    item = new MenuItem(menu, pair.Key, new Vector2(0, 100));
                }
                else
                {
                    item = new MenuItem(menu, pair.Key, new Vector2(0, 5), item_previous);
                }

                menu.AddMenuItem(item);
                item_previous = item;
            }
            
            int menu_back_x = (int)(screenManager.GameManager.Game.Window.ClientBounds.Width * 0.3);
            int menu_back_y = 130;
            int menu_back_width = (int)(screenManager.GameManager.Game.Window.ClientBounds.Width * 0.4);
            int menu_back_heigth = (int)(menu_back_y + menu.MenuItems.Count * 20 + 5);

            menu_background_area = new Rectangle(menu_back_x, menu_back_y, menu_back_width, menu_back_heigth);
        }

        public override void UnloadContent()
        {
            background_texture = null;
            screenManager.GameManager.TextureManager.RemoveUser(background_name);

            menubackground_texture = null;
        }

        public override void Update(GameTime gameTime)
        {
            menu.Update(gameTime);

            if (screenManager.GameManager.InputManager.IsNewKeyPress(Keys.Enter))
            {
                MenuItem selectedItem = menu.GetSelectedItem();
                
                screenManager.GameManager.StoryManager.SetActiveStory(selectedItem.Text);

                isActive = false;
                screenManager.RemoveScreen(this);
                screenManager.GameManager.Player.DialogCounters.Clear();
                screenManager.AddScreen(new GameplayScreen(screenManager));
                screenManager.GameManager.Player.SetActive();
            }

            for (int i = 0; i < menu.MenuItems.Count; i++)
            {
                if (screenManager.GameManager.InputManager.IsMouseInArea(menu.MenuItems[i].Area))
                {
                    menu.SetSelectedItem(i);
                }

                if (screenManager.GameManager.InputManager.IsNewMouseClickArea(menu.MenuItems[i].Area))
                {
                    screenManager.GameManager.StoryManager.SetActiveStory(menu.MenuItems[i].Text);

                    isActive = false;
                    screenManager.RemoveScreen(this);
                    screenManager.GameManager.Player.DialogCounters.Clear();
                    screenManager.AddScreen(new GameplayScreen(screenManager));
                    screenManager.GameManager.Player.SetActive();
                }
            }

            if (screenManager.GameManager.InputManager.IsNewKeyPress(Keys.Escape))
            {
                isActive = false;
                screenManager.RemoveScreen(this);
                screenManager.AddScreen(new MenuScreen(screenManager));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw background
            spriteBatch.Draw(background_texture, new Rectangle(0, 0, screenManager.GameManager.Game.Window.ClientBounds.Width, screenManager.GameManager.Game.Window.ClientBounds.Height), Color.White);

            // Draw Menu background
            spriteBatch.Draw(menubackground_texture, menu_background_area, Color.White);

            menu.Draw(gameTime, spriteBatch);
        }
    }
}
