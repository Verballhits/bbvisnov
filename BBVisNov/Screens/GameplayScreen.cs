using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace BBVisNov
{
    public class GameplayScreen : GameScreen
    {
        private SceneManager sceneManager;

        public GameplayScreen(ScreenManager sm)
            : base(sm)
        {
        }

        public override void Initialize()
        {
            sceneManager = new SceneManager(screenManager);
            sceneManager.Initialize();

            screenManager.GameManager.Player.SetSceneManager(sceneManager);
        }

        public override void LoadContent()
        {
            sceneManager.LoadContent();

            // Initial Scene
            sceneManager.LoadNewScene("Content/Scenes/SchoolFrontScene.xml", screenManager);
        }

        public override void UnloadContent()
        {
            sceneManager.UnloadCurrentScene();
            sceneManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            screenManager.CursorColor = Color.White;

            if (screenManager.GameManager.InputManager.IsNewKeyPress(Keys.Escape))
            {
                isActive = false;
                screenManager.RemoveScreen(this);
                screenManager.AddScreen(new MenuScreen(screenManager));
            }

            sceneManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sceneManager.Draw(gameTime, spriteBatch);
        }
    }
}
