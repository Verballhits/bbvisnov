using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BBVisNov
{
    public class Player
    {
        GameManager gameManager;
        public GameManager GameManager { get { return gameManager; } }

        SceneManager sceneManager;
        public SceneManager SceneManager { get { return sceneManager; } }

        Inventory inventory;
        public Dictionary<string, int> DialogCounters { get; set; }

        public Player(GameManager gm)
        {
            gameManager = gm;

            inventory = new Inventory(this);
            DialogCounters = new Dictionary<string, int>();
        }

        public void SetSceneManager(SceneManager sm)
        {
            sceneManager = sm;
        }

        public void Initialize()
        {
            inventory.AddItem(new InventoryItem(11));
            inventory.AddItem(new InventoryItem(12));
            inventory.AddItem(new InventoryItem(13));
        }

        public void LoadContent()
        {
        }

        public void HandleInput()
        {
            if (gameManager.InputManager.IsNewKeyPress(Keys.I))
            {
                inventory.ToggleVisible();
            }
        }

        public void Update(GameTime gameTime)
        {
            HandleInput();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            inventory.Draw(gameTime, spriteBatch);
        }

        public int GetDialogCount(string dialog)
        {
            if (DialogCounters.ContainsKey(dialog))
            {
                return DialogCounters[dialog];
            }

            return 0;
        }

        public void AddDialogCount(string dialog)
        {
            if (!DialogCounters.ContainsKey(dialog))
            {
                DialogCounters.Add(dialog, 0);
            }

            DialogCounters[dialog]++;
        }
    }
}
