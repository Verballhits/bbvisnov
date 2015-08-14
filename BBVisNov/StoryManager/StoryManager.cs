using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBVisNov
{
    public class StoryManager
    {
        private GameManager gameManager;
        public GameManager GameManager
        {
            get { return gameManager; }
        }

        public string ContentRoot
        {
            get { return gameManager.Game.Content.RootDirectory + "/"; }
        }
        
        private string contentFolderGame;
        public string ContentFolderGame
        {
            get { return contentFolderGame; }
        }
        public string ContentFolderGameFull
        {
            get { return ContentRoot + contentFolderGame; }
        }

        private string contentFolderStory;
        public string ContentFolderStory
        {
            get { return contentFolderStory; }
        }
        public string ContentFolderStoryFull
        {
            get { return ContentRoot + contentFolderStory; }
        }

        private string initialScene;
        public string InitialScene
        {
            get { return initialScene; }
        }

        public StoryManager(GameManager gm)
        {
            gameManager = gm;
        }

        public void Initialize()
        {
        }

        public void LoadContent()
        {
            contentFolderGame = "Game/";
            contentFolderStory = "Stories/SchoolNovelDemo/";
            initialScene = "Scenes/SchoolFrontScene.xml";
        }
    }
}
