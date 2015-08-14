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
    public class DialogManager
    {
        Texture2D background_texture;

        private ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }

        private SceneManager sceneManager;

        private bool dialogActive;
        public bool DialogActive
        {
            get { return dialogActive; }
        }

        private Vector2 position_dialog;
        private Vector2 position_responses;

        private int margin = 5;
        private int padding = 5;
        private int spacing = 5;
        private int lineheight = 25;

        private Vector2 position_dialog_text;
        private Vector2 position_responses_text;

        private Rectangle area_dialog;
        private Rectangle area_responses;
        private Rectangle area_character;

        private int currentNodeID;
        private int currentReponseID;

        private DialogNodeList dialogNodeList;

        private Dictionary<string, Texture2D> characterTextures;

        private Dictionary<int, DialogNode> dialogNodes;
        public Dictionary<int, DialogNode> DialogNodes
        {
            get { return dialogNodes; }
        }

        public DialogManager(ScreenManager sm, SceneManager sc)
        {
            screenManager = sm;
            sceneManager = sc;

            characterTextures = new Dictionary<string, Texture2D>();

            dialogNodes = new Dictionary<int, DialogNode>();
            currentNodeID = -1;
            currentReponseID = -1;
            dialogActive = false;

            int dialogheight = 3 * lineheight + 2 * padding;

            position_dialog = new Vector2(0 + margin, sm.GameManager.Game.Window.ClientBounds.Height - (2 * dialogheight + margin + spacing));
            position_dialog_text = position_dialog + new Vector2(padding, padding);
            position_responses = new Vector2(0 + margin, sm.GameManager.Game.Window.ClientBounds.Height - (dialogheight + margin));
            position_responses_text = position_responses + new Vector2(padding, padding);

            area_dialog = new Rectangle((int)position_dialog.X, (int)position_dialog.Y, sm.GameManager.Game.Window.ClientBounds.Width - 2 * margin, dialogheight);
            area_responses = new Rectangle((int)position_responses.X, (int)position_responses.Y, sm.GameManager.Game.Window.ClientBounds.Width - 2 * margin, dialogheight);
            area_character = new Rectangle((int)(area_dialog.Width - 100 + margin), (int)(area_dialog.Y - 100), 100, 100);
        }

        public void LoadDialogNodes(string dialog)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DialogNodeList));

            using (TextReader reader = new StreamReader(dialog))
            {
                dialogNodeList = (DialogNodeList)serializer.Deserialize(reader);
            }

            UnloadDialogNodes();
            foreach (DialogNode dn in dialogNodeList.Nodes)
            {
                dialogNodes.Add(dn.Id, dn);
                
                if (!string.IsNullOrEmpty(dn.SoundEffect))
                {
                    sceneManager.SoundEffectManager.LoadSoundEff(screenManager.GameManager.StoryManager.ContentFolderStoryFull + dn.SoundEffect);
                }
            }

            UnloadCharacters(screenManager.GameManager.TextureManager);
            foreach (DialogCharacter dc in dialogNodeList.Characters)
            {
                characterTextures.Add(dc.Name, screenManager.GameManager.TextureManager.GetTextureAddUser(screenManager.GameManager.StoryManager.ContentFolderStoryFull + dc.Image));
            }

            foreach (DialogTrigger tr in dialogNodeList.Triggers)
            {
                if (dialogActive)
                {
                    break;
                }

                switch (tr.Type)
                {
                    case "Once":
                        if (screenManager.GameManager.Player.GetDialogCount(dialog) == 0)
                        {
                            screenManager.GameManager.Player.AddDialogCount(dialog);
                            SetCurrentDialogNode(0);
                        }
                        break;

                    case "Repeat":
                        screenManager.GameManager.Player.AddDialogCount(dialog);
                        SetCurrentDialogNode(0);
                        break;
                }
            }
        }

        public void UnloadDialogNodes()
        {
            foreach (KeyValuePair<int, DialogNode> kv in dialogNodes)
            {
                if (!string.IsNullOrEmpty(kv.Value.SoundEffect))
                {
                    sceneManager.SoundEffectManager.UnloadSoundEff(kv.Value.SoundEffect);
                }
            }

            dialogNodes.Clear();
        }

        private void UnloadCharacters(TextureManager tm)
        {
            foreach (DialogCharacter dc in dialogNodeList.Characters)
            {
                if (characterTextures.ContainsKey(dc.Name))
                {
                    characterTextures[dc.Name] = null;
                    tm.RemoveUser(dc.Image);
                }
            }

            characterTextures.Clear();
        }

        public void LoadContent()
        {
            background_texture = new Texture2D(screenManager.GameManager.GraphicsDevice, 1, 1);
            background_texture.SetData<Color>(new Color[] { Color.FromNonPremultiplied(255, 255, 255, 192) });
        }

        public void UnloadContent()
        {
            background_texture = null;
        }

        private void SetCurrentDialogNode(int value)
        {
            currentNodeID = value;
            currentReponseID = -1;
            dialogActive = (currentNodeID != -1);

            if (dialogActive)
            {
                if (!string.IsNullOrEmpty(dialogNodes[currentNodeID].ShowSceneCharacter))
                {
                    sceneManager.ShowCharacter(dialogNodes[currentNodeID].ShowSceneCharacter);
                }

                if (!string.IsNullOrEmpty(dialogNodes[currentNodeID].HideSceneCharacter))
                {
                    sceneManager.HideCharacter(dialogNodes[currentNodeID].HideSceneCharacter);
                }

                if (!string.IsNullOrEmpty(dialogNodes[currentNodeID].SoundEffect))
                {
                    sceneManager.SoundEffectManager.PlaySoundEff(screenManager.GameManager.StoryManager.ContentFolderStoryFull + dialogNodes[currentNodeID].SoundEffect);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            int newReponseID = -1;

            if (currentNodeID != -1 && dialogNodes.ContainsKey(currentNodeID))
            {
                for (int i = 0; i < dialogNodes[currentNodeID].Responses.Count; i++)
                {
                    Rectangle repRect = new Rectangle((int)position_responses_text.X, (int)position_responses_text.Y + ((i) * 25), 600, 25);

                    if (screenManager.GameManager.InputManager.IsMouseInArea(repRect))
                    {
                        newReponseID = i;
                    }

                    if (currentReponseID != -1 && screenManager.GameManager.InputManager.IsNewMouseClickArea(repRect))
                    {
                        SetCurrentDialogNode(dialogNodes[currentNodeID].Responses[i].NextNode);
                        break;
                    }
                }
            }

            currentReponseID = newReponseID;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (dialogActive)
            {
                spriteBatch.Draw(background_texture, area_dialog, Color.DarkBlue);
                spriteBatch.Draw(background_texture, area_responses, Color.DarkGreen);

                spriteBatch.Draw(characterTextures[dialogNodes[currentNodeID].CharacterImage], area_character, Color.White);

                // Draw dialog lines
                for (int i = 0; i < dialogNodes[currentNodeID].Text.Length; i++)
                {
                    spriteBatch.DrawString(screenManager.HudFont, dialogNodes[currentNodeID].Text[i], position_dialog_text + new Vector2(0, i * 25), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                }

                // Draw responses
                for (int i = 0; i < dialogNodes[currentNodeID].Responses.Count; i++)
                {
                    if (i == currentReponseID)
                    {
                        spriteBatch.DrawString(screenManager.HudFont, " • " + dialogNodes[currentNodeID].Responses[i].Text, position_responses_text + new Vector2(0, (i) * 25), Color.Yellow, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.DrawString(screenManager.HudFont, " • " + dialogNodes[currentNodeID].Responses[i].Text, position_responses_text + new Vector2(0, (i) * 25), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}
