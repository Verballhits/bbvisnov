using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class ScreenManager
    {
        private GameManager gameManager;
        public GameManager GameManager
        {
            get { return gameManager; }
        }

        List<GameScreen> screens_toupdate = new List<GameScreen>();
        List<GameScreen> screens = new List<GameScreen>();
        public List<GameScreen> Screens
        {
            get { return screens; }
        }

        private SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        private SpriteFont menuFont;
        public SpriteFont MenuFont
        {
            get { return menuFont; }
        }

        private SpriteFont hudFont;
        public SpriteFont HudFont
        {
            get { return hudFont; }
        }

        public Color CursorColor { get; set; }
        private Vector2 cursorPosition;
        private Texture2D cursorTexture;
        public Texture2D CursorTexture
        {
            get { return cursorTexture; }
        }

        public ScreenManager(GameManager gm)
        {
            gameManager = gm;
        }

        public void Initialize()
        {
            spriteBatch = new SpriteBatch(gameManager.GraphicsDevice);

            foreach (GameScreen screen in screens)
            {
                screen.Initialize();
            }
        }

        public void LoadContent()
        {
            CursorColor = Color.White;
            cursorTexture = gameManager.TextureManager.GetTextureAddUser(gameManager.StoryManager.ContentFolderGameFull + "Graphics/Cursors/cursor.png");

            menuFont = gameManager.Game.Content.Load<SpriteFont>(gameManager.StoryManager.ContentFolderGame + "Fonts/menufont");
            hudFont = gameManager.Game.Content.Load<SpriteFont>(gameManager.StoryManager.ContentFolderGame + "Fonts/Arial");

            foreach (GameScreen screen in screens)
            {
                screen.LoadContent();
            }
        }

        public void UnloadContent()
        {
            gameManager.TextureManager.RemoveUser(gameManager.StoryManager.ContentFolderGameFull + "Graphics/cursor.png");
        }

        public void Update(GameTime gameTime)
        {
            screens_toupdate.Clear();

            foreach (GameScreen screen in screens)
            {
                screens_toupdate.Add(screen);
            }

            foreach (GameScreen screen in screens_toupdate)
            {
                if (screen.IsActive)
                {
                    screen.Update(gameTime);
                }
            }

            cursorPosition = gameManager.InputManager.MousePosition;

            gameManager.Player.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (GameScreen screen in screens)
            {
                if (screen.IsActive)
                {
                    screen.Draw(gameTime, spriteBatch);
                }
            }

            // Draw the player / Inventory
            gameManager.Player.Draw(gameTime, spriteBatch);

            // Draw the cursor
            spriteBatch.Draw(cursorTexture, cursorPosition, CursorColor);

            spriteBatch.End();
        }

        public void AddScreen(GameScreen screen)
        {
            if (!screens.Contains(screen))
            {
                screen.Initialize();
                screen.LoadContent();
                screens.Add(screen);
            }
        }

        public void RemoveScreen(GameScreen screen)
        {
            if (screens.Contains(screen))
            {
                screens.Remove(screen);
                screen.UnloadContent();
            }
        }
    }
}
