﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class GameManager : DrawableGameComponent
    {
        private ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }

        private Player player;
        public Player Player
        {
            get { return player; }
        }

        private InputManager inputManager;
        public InputManager InputManager
        {
            get { return inputManager; }
        }

        private TextureManager textureManager;
        public TextureManager TextureManager
        {
            get { return textureManager; }
        }

        private StoryManager storyManager;
        public StoryManager StoryManager
        {
            get { return storyManager; }
        }

        private SaveManager saveManager;
        public SaveManager SaveManager
        {
            get { return saveManager; }
        }
        
        public GameManager(Game game)
            : base(game)
        {
            textureManager = new TextureManager(this);
            inputManager = new InputManager(this);
            player = new Player(this);
            storyManager = new StoryManager(this);
            screenManager = new ScreenManager(this);
            saveManager = new SaveManager();
        }

        public override void Initialize()
        {
            inputManager.Initialize();
            player.Initialize();
            storyManager.Initialize();
            screenManager.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            player.LoadContent();
            storyManager.LoadContent();
            screenManager.LoadContent();

            screenManager.AddScreen(new MenuScreen(screenManager));

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            screenManager.UnloadContent();
            player.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            screenManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
