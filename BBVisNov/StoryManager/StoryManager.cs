using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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

        private Dictionary<string, Story> allStories;
        public Dictionary<string, Story> AllStories
        {
            get { return allStories; }
        }

        private string activeStoryName = "";

        public StoryManager(GameManager gm)
        {
            gameManager = gm;
        }

        public void Initialize()
        {
            allStories = new Dictionary<string, Story>();
        }

        public void LoadContent()
        {
            contentFolderGame = "Game/";

            LoadStories();
        }

        private void LoadStories()
        {
            allStories.Clear();

            string[] story_filenames = Directory.GetFiles(ContentRoot + "Stories", "*.story.xml");

            foreach (string full_filename in story_filenames)
            {
                string filename = Path.GetFileName(full_filename);
                string storyname = filename.Replace(".story.xml", "");
                
                XmlSerializer serializer = new XmlSerializer(typeof(Story));

                using (TextReader reader = new StreamReader(full_filename))
                {
                    Story s = (Story)serializer.Deserialize(reader);

                    allStories.Add(storyname, s);
                }
            }
        }

        public void SetActiveStory(string storyName)
        {
            activeStoryName = storyName;
            contentFolderStory = "Stories/" + storyName  + "/";
            initialScene = allStories[storyName].InitialScene;
        }

        public string GetActiveStory()
        {
            return activeStoryName;
        }
    }
}
