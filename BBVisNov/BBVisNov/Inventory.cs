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

        Texture2D background_texture;

        private Vector2 position_inventory;
        private Vector2 size_inventory;
        private Rectangle area_inventory;

        public Inventory(Player pl)
        {
            player = pl;
            isVisible = false;
            items = new Dictionary<int, InventoryItem>();

            position_inventory = new Vector2(player.GameManager.Game.Window.ClientBounds.Width * 0.66f, 30);
            size_inventory = new Vector2(player.GameManager.Game.Window.ClientBounds.Width - position_inventory.X - 10, 200);
            area_inventory = new Rectangle((int)position_inventory.X, (int)position_inventory.Y, (int)size_inventory.X, (int)size_inventory.Y);
        }

        public void Hide()
        {
            isVisible = false;
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

        public void LoadContent()
        {
            background_texture = new Texture2D(player.GameManager.GraphicsDevice, 1, 1);
            background_texture.SetData<Color>(new Color[] { Color.FromNonPremultiplied(255, 255, 255, 192) });
        }

        public void UnloadContent()
        {
            background_texture = null;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(background_texture, area_inventory, Color.Black);

                int gridX = 1;
                int gridY = 1;

                // Draw Inventory
                foreach(KeyValuePair<int, InventoryItem> kv in items)
                {
                    Item it = player.GameManager.Player.SceneManager.ItemManager.GetItem(kv.Key);

                    spriteBatch.Draw(it.ImageTexture, new Rectangle((int)position_inventory.X + gridX, (int)position_inventory.Y + gridY, 48, 48), Color.White);

                    gridX += 49;

                    if (gridX + 49 > size_inventory.X)
                    {
                        gridX = 0;
                        gridY += 49;
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
