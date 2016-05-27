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

        private bool isActive = false;

        public void Initialize()
        {
            inventory.Initialize();

            inventory.AddItem(new InventoryItem(11));
            inventory.AddItem(new InventoryItem(12));
            inventory.AddItem(new InventoryItem(13));

            inventory.AddItem(new InventoryItem(1));
            inventory.AddItem(new InventoryItem(2));
            inventory.AddItem(new InventoryItem(3));
            inventory.AddItem(new InventoryItem(4));
        }

        public void LoadContent()
        {
            inventory.LoadContent();
        }

        public void UnloadContent()
        {
            inventory.UnloadContent();
        }

        public void HandleInput()
        {
            if (gameManager.InputManager.IsNewKeyPress(Keys.Escape))
            {
                SetInactive();
            }

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
            if (isActive)
            {
                inventory.Draw(gameTime, spriteBatch);
            }
        }

        public void SetActive()
        {
            isActive = true;
        }

        public void SetInactive()
        {
            inventory.Hide();
            isActive = false;
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
