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
    public class ItemManager
    {
        private ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }

        private ItemList itemList;
        private Dictionary<int, Item> items;

        public ItemManager(ScreenManager sm)
        {
            screenManager = sm;

            itemList = new ItemList();
            items = new Dictionary<int, Item>();
        }

        public void LoadContent()
        {
            string itemlist = screenManager.GameManager.StoryManager.ContentFolderStoryFull + "Items/Items.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(ItemList));

            using (TextReader reader = new StreamReader(itemlist))
            {
                itemList = (ItemList)serializer.Deserialize(reader);
            }

            foreach (Item itm in itemList.Items)
            {
                items.Add(itm.ItemID, itm);
                itm.LoadContent(screenManager.GameManager);
            }
        }

        public void UnloadContent()
        {
            foreach (Item itm in itemList.Items)
            {
                itm.UnloadContent(screenManager.GameManager.TextureManager);
            }

            items.Clear();
            itemList.Items.Clear();
        }

        public Item GetItem(int itemid)
        {
            if (items.ContainsKey(itemid))
            {
                return items[itemid];
            }

            return new Item();
        }
    }
}
