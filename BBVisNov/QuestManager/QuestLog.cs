using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BBVisNov
{
    public class QuestLog
    {
        private QuestManager questManager;
        private bool isVisible;

        private Dictionary<int, InventoryItem> items;
        public Dictionary<int, InventoryItem> Items;

        Texture2D background_texture;

        private Vector2 position;
        private Vector2 size;
        private Rectangle area;

        string header_text;
        Vector2 header_size;

        public QuestLog(QuestManager qm)
        {
            questManager = qm;
            isVisible = false;
            items = new Dictionary<int, InventoryItem>();
        }

        public void Initialize()
        {
            position = new Vector2(10, 30);
            size = new Vector2(questManager.ScreenManager.GameManager.Game.Window.ClientBounds.Width * 0.5f, 200);
            area = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            header_text = "Quest Log";
            header_size = questManager.ScreenManager.HudFont.MeasureString(header_text);
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
            background_texture = new Texture2D(questManager.ScreenManager.GameManager.GraphicsDevice, 1, 1);
            background_texture.SetData<Color>(new Color[] { Color.FromNonPremultiplied(255, 255, 255, 192) });
        }

        public void UnloadContent()
        {
            background_texture = null;
        }

        public void Update(GameTime gameTime)
        {
            if (questManager.ScreenManager.GameManager.InputManager.IsNewKeyPress(Keys.L))
            {
                ToggleVisible();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(background_texture, area, Color.Black);

                int i = 0;
                
                // Draw Header
                spriteBatch.DrawString(questManager.ScreenManager.HudFont, header_text, new Vector2(position.X + size.X * 0.5f, position.Y), Color.Gold, 0, new Vector2(header_size.X * 0.5f, 0), 1, SpriteEffects.None, 0);
                i++;

                // Draw Quests
                foreach (KeyValuePair<int, Quest> kv in questManager.ActiveQuests)
                {
                    string questname = kv.Value.Name;
                    string questdesc = "    " + kv.Value.Description;

                    spriteBatch.DrawString(questManager.ScreenManager.HudFont, questname, position + new Vector2(0, i * 25), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    i++;
                    spriteBatch.DrawString(questManager.ScreenManager.HudFont, questdesc, position + new Vector2(0, i * 25), Color.LightGray, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    i++;
                }
            }
        }
    }
}
