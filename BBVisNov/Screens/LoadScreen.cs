using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBVisNov
{
    public class LoadScreen : GameScreen
    {
        string background_name;
        Texture2D background_texture;

        Rectangle menu_background_area;
        Texture2D menubackground_texture;

        Menu menu;

        public LoadScreen(ScreenManager sm)
            : base(sm)
        {
            menu = new Menu(sm, "Load Game");
            
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
            string[] available_saves = screenManager.GameManager.SaveManager.GetSaves();

            MenuItem previtem = null;

            foreach (string savefile in available_saves)
            {
                MenuItem item;

                if (previtem == null)
                {
                    item = new MenuItem(menu, savefile, new Vector2(0, 100));
                    menu.AddMenuItem(item);
                }
                else
                {
                    item = new MenuItem(menu, savefile, new Vector2(0, 5), previtem);
                    menu.AddMenuItem(item);
                }
                
                previtem = item;
            }

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

                // Restore the story paths
                screenManager.GameManager.SaveManager.LoadGame(selectedItem.Text);
                screenManager.GameManager.SaveManager.RestoreSaveStory(screenManager.GameManager);

                // Activate the gameplay screen to initialize the scenemanager
                isActive = false;
                screenManager.RemoveScreen(this);
                screenManager.RemoveScreen(screenManager.Screens[0]);
                screenManager.AddScreen(new GameplayScreen(screenManager));

                // Restore the scene
                screenManager.GameManager.SaveManager.RestoreSaveScene(screenManager.GameManager);

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
                    // Restore the story paths
                    screenManager.GameManager.SaveManager.LoadGame(menu.MenuItems[i].Text);
                    screenManager.GameManager.SaveManager.RestoreSaveStory(screenManager.GameManager);

                    // Activate the gameplay screen to initialize the scenemanager
                    isActive = false;
                    screenManager.RemoveScreen(this);
                    screenManager.RemoveScreen(screenManager.Screens[0]);
                    screenManager.AddScreen(new GameplayScreen(screenManager));

                    // Restore the scene
                    screenManager.GameManager.SaveManager.RestoreSaveScene(screenManager.GameManager);

                    screenManager.GameManager.Player.SetActive();
                }
            }

            if (screenManager.GameManager.InputManager.IsNewKeyPress(Keys.Escape))
            {
                isActive = false;
                screenManager.RemoveScreen(this);
                screenManager.Screens[0].IsActive = true;
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
