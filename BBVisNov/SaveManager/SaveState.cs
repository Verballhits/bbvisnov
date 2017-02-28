using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBVisNov
{
    public class SaveState
    {
        public string Name = "";
        public string ActiveStory = "";
        public string CurrentScene = "";


        public SerializableDictionary<string, int> DialogCounters = new SerializableDictionary<string, int>();

        public SavedQuests Quests = new SavedQuests();
        public SavedInventory Inventory = new SavedInventory();

        public SaveState()
        {
        }
    }

    public struct SavedQuests
    {
        public SerializableDictionary<int, Quest> ActiveQuests;
        public SerializableDictionary<int, Quest> CompletedQuests;
        public SerializableDictionary<int, Quest> FailedQuests;
    }

    public struct SavedInventory
    {

    }
}
