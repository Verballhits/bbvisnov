using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class Inventory
    {
        private Player player;
        private bool isVisible;

        private Dictionary<int, InventoryItem> items;
        public Dictionary<int, InventoryItem> Items;
 
        public Inventory(Player pl)
        {
            player = pl;
            isVisible = false;
            items = new Dictionary<int, InventoryItem>();
        }

        public void ToggleVisible()
        {
            isVisible = !isVisible;
        }

        public void AddItem(InventoryItem item)
        {
            if (items.ContainsKey(item.ItemID))
            {
                items[item.ItemID].ItemCount += item.ItemCount;
            }
            else
            {
                items.Add(item.ItemID, item);
            }
        }

        public void RemoveItem(InventoryItem item)
        {
            if (items.ContainsKey(item.ItemID))
            {
                if (items[item.ItemID].ItemCount >= item.ItemCount)
                {
                    items[item.ItemID].ItemCount -= item.ItemCount;
                }

                if (items[item.ItemID].ItemCount <= 0)
                {
                    items.Remove(item.ItemID);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                int gridX = 0;
                int gridY = 0;

                // Draw Inventory
                foreach(KeyValuePair<int, InventoryItem> kv in items)
                {
                    Item it = player.GameManager.Player.SceneManager.ItemManager.GetItem(kv.Key);

                    spriteBatch.Draw(it.ImageTexture, new Rectangle(gridX, gridY, 48, 48), Color.White);

                    gridX += 50;

                    if (gridX > 500)
                    {
                        gridX = 0;
                        gridY += 50;
                    }
                }
            }
        }
    }


    public class InventoryItem
    {
        private int itemID;
        public int ItemID { get { return itemID; } }

        private int itemCount;
        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }

        public InventoryItem(int id, int count = 1)
        {
            this.itemID = id;
            this.itemCount = count;
        }
    }
}
