using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class SceneManager
    {
        private ScreenManager screenManager;
        private DialogManager dialogManager;


        private Hud hud;
        public Hud Hud
        {
            get { return hud; }
        }

        private MusicManager musicManager;
        public MusicManager MusicManager
        {
            get { return musicManager; }
        }

        private SoundEffectManager soundEffectManager;
        public SoundEffectManager SoundEffectManager
        {
            get { return soundEffectManager; }
        }

        private ItemManager itemManager;
        public ItemManager ItemManager
        {
            get { return itemManager; }
        }

        private Scene currentScene;
        public Scene CurrentScene
        {
            get { return currentScene; }
        }

        public SceneManager(ScreenManager sm)
        {
            screenManager = sm;
            dialogManager = new DialogManager(screenManager, this);
            musicManager = new MusicManager();
            soundEffectManager = new SoundEffectManager();
            itemManager = new ItemManager(screenManager);
            hud = new Hud(screenManager);
        }

        public void Initialize()
        {
            hud.Initialize();
        }

        public void LoadNewScene(string scene, ScreenManager sm)
        {
            UnloadCurrentScene();

            XmlSerializer serializer = new XmlSerializer(typeof(Scene));

            using (TextReader reader = new StreamReader(scene))
            {
                currentScene = (Scene)serializer.Deserialize(reader);
                currentScene.SetSceenManager(sm);
                currentScene.SetSceneManager(this);
                currentScene.LoadContent();
            }

            // Load Dialog
            if (!string.IsNullOrEmpty(currentScene.InitialDialog))
            {
                dialogManager.LoadDialogNodes("Content/" + currentScene.InitialDialog);
            }
            
            // Change Music
            musicManager.PlayBackgroundMusic("Content/" + currentScene.BackgroundMusic);
        }

        public void UnloadCurrentScene()
        {
            if (currentScene != null)
            {
                currentScene.UnloadContent();
                currentScene = null;
            }
        }

        public void ShowCharacter(string name)
        {
            foreach (SceneCharacter charac in currentScene.Characters)
            {
                if (charac.Name == name)
                {
                    charac.Visible = true;
                }
            }
        }

        public void HideCharacter(string name)
        {
            foreach (SceneCharacter charac in currentScene.Characters)
            {
                if (charac.Name == name)
                {
                    charac.Visible = false;
                }
            }
        }

        public void LoadContent()
        {
            hud.LoadContent();
            dialogManager.LoadContent();
            itemManager.LoadContent();
        }

        public void UnloadContent()
        {
            itemManager.UnloadContent();
            dialogManager.UnloadContent();
            musicManager.StopBackgroundMusic();
            hud.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (!dialogManager.DialogActive)
            {
                if (currentScene != null)
                {
                    currentScene.Update(gameTime);
                }
            }

            dialogManager.Update(gameTime);
            musicManager.Update();
            hud.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (currentScene != null)
            {
                if (currentScene.IsActive)
                {
                    currentScene.Draw(gameTime, spriteBatch);
                }
            }

            dialogManager.Draw(gameTime, spriteBatch);
            hud.Draw(gameTime, spriteBatch);
        }
    }
}
