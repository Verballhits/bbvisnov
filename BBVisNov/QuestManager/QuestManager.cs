using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BBVisNov
{
    public class QuestManager
    {
        private ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }

        private QuestList questList;
        private Dictionary<int, Quest> quests;

        public Dictionary<int, Quest> ActiveQuests;
        public Dictionary<int, Quest> CompletedQuests;
        public Dictionary<int, Quest> FailedQuests;

        private QuestLog questLog;

        public QuestManager(ScreenManager sm)
        {
            screenManager = sm;

            questList = new QuestList();
            quests = new Dictionary<int, Quest>();

            ActiveQuests = new Dictionary<int, Quest>();
            CompletedQuests = new Dictionary<int, Quest>();
            FailedQuests = new Dictionary<int, Quest>();

            questLog = new QuestLog(this);
        }

        public void Initialize()
        {
            questLog.Initialize();
        }

        public void LoadContent()
        {
            string questlist = screenManager.GameManager.StoryManager.ContentFolderStoryFull + "Quests/Quests.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(QuestList));

            using (TextReader reader = new StreamReader(questlist))
            {
                questList = (QuestList)serializer.Deserialize(reader);
            }

            foreach (Quest q in questList.Quests)
            {
                quests.Add(q.QuestId, q);
                //q.LoadContent(screenManager.GameManager);
            }

            questLog.LoadContent();
        }

        public void UnloadContent()
        {
            questLog.UnloadContent();

            foreach (Quest q in questList.Quests)
            {
                //q.UnloadContent(screenManager.GameManager.TextureManager);
            }

            quests.Clear();
            questList.Quests.Clear();
        }

        public void Update(GameTime gameTime)
        {
            questLog.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            questLog.Draw(gameTime, spriteBatch);
        }

        public bool HasActiveQuest(int questid)
        {
            return ActiveQuests.ContainsKey(questid);
        }

        public void StartQuest(int questid)
        {
            Quest q = (Quest)quests[questid].Clone();

            ActiveQuests.Add(questid, q);
        }

        public void CompleteQuest(int questid)
        {
            Quest q = ActiveQuests[questid];
            q.State = QuestState.Completed;

            ActiveQuests.Remove(questid);

            if (q.NextQuest != 0)
            {
                StartQuest(q.NextQuest);
            }

            CompletedQuests.Add(questid, q);
        }
    }
}
