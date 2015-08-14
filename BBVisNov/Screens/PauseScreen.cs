using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBVisNov
{
    public class PauseScreen : GameScreen
    {
        string background_name;
        Texture2D background_texture;

        Rectangle menu_background_area;
        Texture2D menubackground_texture;

        Menu menu;

        public PauseScreen(ScreenManager sm)
            : base(sm)
        {
            menu = new Menu(sm, "Paused");
            
            background_name = screenManager.GameManager.StoryManager.ContentFolderGameFull + "Graphics/Backgrounds/menu.jpg";
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            background_texture = screenManager.GameManager.TextureManager.GetTextureAddUser(background_name);

            menubackground_texture = new Texture2D(screenManager.GameManager.GraphicsDevice, 1, 1);
            menubackground_texture.SetData<Color>(new Color[] { Color.FromNonPremultiplied(0, 0, 255, 120) });

            // Create and add menu items
            MenuItem item_resume = new MenuItem(menu, "Resume Game", new Vector2(0, 100));
            MenuItem item_save = new MenuItem(menu, "Save Game", new Vector2(0, 5), item_resume);
            MenuItem item_mainmenu = new MenuItem(menu, "Main Menu", new Vector2(0, 5), item_save);

            menu.AddMenuItem(item_resume);
            menu.AddMenuItem(item_save);
            menu.AddMenuItem(item_mainmenu);
            
            int menu_back_x = (int)(screenManager.GameManager.Game.Window.ClientBounds.Width * 0.4);
            int menu_back_y = 130;
            int menu_back_width = (int)(screenManager.GameManager.Game.Window.ClientBounds.Width * 0.2);
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

                if (selectedItem.Text == "Resume Game")
                {
                    isActive = false;
                    screenManager.RemoveScreen(this);
                    screenManager.Screens[0].IsActive = true;
                    screenManager.GameManager.Player.SetActive();
                }
                else if (selectedItem.Text == "Save Game")
                {
                }
                else if (selectedItem.Text == "Main Menu")
                {
                    isActive = false;
                    screenManager.RemoveScreen(this);
                    screenManager.RemoveScreen(screenManager.Screens[0]);
                    screenManager.AddScreen(new MenuScreen(screenManager));
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
                    if (menu.MenuItems[i].Text == "Resume Game")
                    {
                        isActive = false;
                        screenManager.RemoveScreen(this);
                        screenManager.Screens[0].IsActive = true;
                        screenManager.GameManager.Player.SetActive();
                    }
                    else if (menu.MenuItems[i].Text == "Save Game")
                    {
                    }
                    else if (menu.MenuItems[i].Text == "Main Menu")
                    {
                        isActive = false;
                        screenManager.RemoveScreen(this);
                        screenManager.RemoveScreen(screenManager.Screens[0]);
                        screenManager.AddScreen(new MenuScreen(screenManager));
                    }
                }
            }

            if (screenManager.GameManager.InputManager.IsNewKeyPress(Keys.Escape))
            {
                isActive = false;
                screenManager.RemoveScreen(this);
                screenManager.Screens[0].IsActive = true;
                screenManager.GameManager.Player.SetActive();
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
