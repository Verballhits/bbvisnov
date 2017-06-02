using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BBVisNov
{
    public class SaveScreen : GameScreen
    {
        string background_name;
        Texture2D background_texture;

        Rectangle menu_background_area;
        Texture2D menubackground_texture;

        Menu menu;

        string _save_name;

        public SaveScreen(ScreenManager sm)
            : base(sm)
        {
            menu = new Menu(sm, "Save Game");
            
            background_name = screenManager.GameManager.StoryManager.ContentFolderGameFull + "Graphics/Backgrounds/menu.jpg";
        }

        public override void Initialize()
        {
            screenManager.GameManager.Game.Window.TextInput += Window_TextInput;
        }
        
        public override void LoadContent()
        {
            background_texture = screenManager.GameManager.TextureManager.GetTextureAddUser(background_name);

            menubackground_texture = new Texture2D(screenManager.GameManager.GraphicsDevice, 1, 1);
            menubackground_texture.SetData<Color>(new Color[] { Color.FromNonPremultiplied(20, 20, 20, 120) });

            _save_name = "_";

            MenuItem item = new MenuItem(menu, _save_name, new Vector2(0, 100));
            menu.MenuItems.Clear();
            menu.AddMenuItem(item);

            int menu_back_x = (int)(screenManager.GameManager.Game.Window.ClientBounds.Width * 0.4);
            int menu_back_y = 130;
            int menu_back_width = (int)(screenManager.GameManager.Game.Window.ClientBounds.Width * 0.2);
            int menu_back_heigth = (int)(menu_back_y + 1 * 20 + 5);

            menu_background_area = new Rectangle(menu_back_x, menu_back_y, menu_back_width, menu_back_heigth);
        }

        public override void UnloadContent()
        {
            background_texture = null;
            screenManager.GameManager.TextureManager.RemoveUser(background_name);

            menubackground_texture = null;
        }

        Regex valid_input = new Regex("[A-Za-z0-9]");

        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (valid_input.IsMatch(e.Character.ToString()))
            {
                _save_name = _save_name.Insert(_save_name.Length - 1, "" + e.Character);

                MenuItem item = new MenuItem(menu, _save_name, new Vector2(0, 100));
                menu.MenuItems.Clear();
                menu.AddMenuItem(item);
            }
            else if (e.Character == '\b')
            {
                if (_save_name.Length > 1)
                {
                    _save_name = _save_name.Remove(_save_name.Length - 2, 1);

                    MenuItem item = new MenuItem(menu, _save_name, new Vector2(0, 100));
                    menu.MenuItems.Clear();
                    menu.AddMenuItem(item);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
            
            if (screenManager.GameManager.InputManager.IsNewKeyPress(Keys.Enter))
            {
                _save_name = _save_name.Trim('_');

                screenManager.GameManager.SaveManager.PrepareSave(_save_name, screenManager.GameManager.Player);
                screenManager.GameManager.SaveManager.SaveGame(_save_name);

                isActive = false;
                screenManager.RemoveScreen(this);
                screenManager.Screens[screenManager.Screens.Count - 1].IsActive = true;
            }

            if (screenManager.GameManager.InputManager.IsNewKeyPress(Keys.Escape))
            {
                isActive = false;
                screenManager.RemoveScreen(this);
                screenManager.Screens[screenManager.Screens.Count - 1].IsActive = true;
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
