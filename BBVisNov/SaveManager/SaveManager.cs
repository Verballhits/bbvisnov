using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BBVisNov
{
    public class SaveManager
    {
        private string m_save_location;
        private SaveState m_savestate = new SaveState();

        public SaveManager()
        {
            string mydocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string mygames = Path.Combine(mydocs, "My Games");
            string bbvisnov = Path.Combine(mygames, "BBVisNov");
            string saves = Path.Combine(bbvisnov, "Saves");

            if (!Directory.Exists(mygames)) { Directory.CreateDirectory(mygames); }
            if (!Directory.Exists(bbvisnov)) { Directory.CreateDirectory(bbvisnov); }
            if (!Directory.Exists(saves)) { Directory.CreateDirectory(saves); }

            m_save_location = saves;
        }

        public void SaveGame(string a_name)
        {
            string savename = Path.Combine(m_save_location, a_name) + ".sav";

            XmlSerializer serializer = new XmlSerializer(typeof(SaveState));

            using (TextWriter writer = new StreamWriter(savename))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                serializer.Serialize(writer, m_savestate, ns);
            }
        }

        public void LoadGame(string a_name)
        {
            string savename = Path.Combine(m_save_location, a_name) + ".sav";

            XmlSerializer serializer = new XmlSerializer(typeof(SaveState));

            using (TextReader reader = new StreamReader(savename))
            {
                m_savestate = (SaveState)serializer.Deserialize(reader);
            }
        }

        public void PrepareSave(string name, Player pl)
        {
            m_savestate = new SaveState();
            
            m_savestate.Name = name;
            m_savestate.ActiveStory = pl.GameManager.StoryManager.GetActiveStory();
            m_savestate.CurrentScene = pl.SceneManager.GetCurrentSceneName();
            m_savestate.DialogCounters = pl.DialogCounters;

            m_savestate.Quests.ActiveQuests = pl.SceneManager.QuestManager.ActiveQuests;
            m_savestate.Quests.CompletedQuests = pl.SceneManager.QuestManager.CompletedQuests;
            m_savestate.Quests.FailedQuests = pl.SceneManager.QuestManager.FailedQuests;
        }

        public void RestoreSaveStory(GameManager gm)
        {
            gm.StoryManager.SetActiveStory(m_savestate.ActiveStory);

            gm.Player.DialogCounters = m_savestate.DialogCounters;
        }

        public void RestoreSaveScene(GameManager gm)
        {
            gm.Player.SceneManager.LoadNewScene(m_savestate.CurrentScene, gm.ScreenManager);

            gm.Player.SceneManager.QuestManager.ActiveQuests = m_savestate.Quests.ActiveQuests;
            gm.Player.SceneManager.QuestManager.CompletedQuests = m_savestate.Quests.CompletedQuests;
            gm.Player.SceneManager.QuestManager.FailedQuests = m_savestate.Quests.FailedQuests;
        }
    }
}
