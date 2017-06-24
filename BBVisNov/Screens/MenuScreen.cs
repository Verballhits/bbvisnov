using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BBVisNov
{
    public class MenuScreen : GameScreen
    {
        string background_name;
        Texture2D background_texture;

        Rectangle menu_background_area;
        Texture2D menubackground_texture;

        Menu menu;

        public MenuScreen(ScreenManager sm)
            : base(sm)
        {
            menu = new Menu(sm, "BB Visual Novel");

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

            // Create and add menu items
            MenuItem item_new = new MenuItem(menu, "New Game", new Vector2(0, 100));
            MenuItem item_load = new MenuItem(menu, "Load Game", new Vector2(0, 5), item_new);
            MenuItem item_options = new MenuItem(menu, "Options", new Vector2(0, 5), item_load);
            MenuItem item_quit = new MenuItem(menu, "Quit", new Vector2(0, 5), item_options);

            menu.AddMenuItem(item_new);
            menu.AddMenuItem(item_load);
            menu.AddMenuItem(item_options);
            menu.AddMenuItem(item_quit);


            int menu_back_x = (int)(screenManager.GameManager.Game.Window.ClientBounds.Width * 0.4);
            int menu_back_y = 130;
            int menu_back_width = (int)(screenManager.GameManager.Game.Window.ClientBounds.Width * 0.2);
            int menu_back_heigth = (int)(menu_back_y + menu.MenuItems.Count * 42 - 70);

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

                if (selectedItem.Text == "New Game")
                {
                    isActive = false;
                    screenManager.RemoveScreen(this);
                    screenManager.AddScreen(new StoriesScreen(screenManager));
                }
                else if (selectedItem.Text == "Load Game")
                {
                    isActive = false;
                    screenManager.AddScreen(new LoadScreen(screenManager));
                }
                else if (selectedItem.Text == "Options")
                {
                }
                else if (selectedItem.Text == "Quit")
                {
                    screenManager.GameManager.Game.Exit();
                }
            }

            for (int i = 0; i < menu.MenuItems.Count; i++)
            {
                if (screenManager.GameManager.InputManager.IsMouseInArea(menu.MenuItems[i].Area))
                {
                    menu.SetSelectedItem(i);
                }

                if (screenManager.GameManager.InputManager.IsNewMouseClickArea(menu.MenuItems[i].Area))
                {
                    if (menu.MenuItems[i].Text == "New Game")
                    {
                        isActive = false;
                        screenManager.RemoveScreen(this);
                        screenManager.AddScreen(new StoriesScreen(screenManager));
                    }
                    else if (menu.MenuItems[i].Text == "Load Game")
                    {
                        isActive = false;
                        screenManager.AddScreen(new LoadScreen(screenManager));
                    }
                    else if (menu.MenuItems[i].Text == "Options")
                    {
                    }
                    else if (menu.MenuItems[i].Text == "Quit")
                    {
                        screenManager.GameManager.Game.Exit();
                    }
                }
            }

            if (screenManager.GameManager.InputManager.IsNewKeyPress(Keys.Escape))
            {
                screenManager.GameManager.Game.Exit();
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
