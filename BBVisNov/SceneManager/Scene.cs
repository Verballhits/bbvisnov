using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BBVisNov
{
    public class Scene
    {
        protected bool isActive = true;
        public Boolean IsActive
        {
            get { return isActive; }
        }

        private ScreenManager screenManager;
        private SceneManager sceneManager;

        [XmlElement("BackgroundImage")]
        public string BackgroundImage { get; set; }

        Texture2D background_texture;

        [XmlElement("BackgroundMusic")]
        public string BackgroundMusic { get; set; }

        [XmlElement("InitialDialog")]
        public string InitialDialog { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<SceneItem> Items { get; set; }

        [XmlArray("ClickAreas")]
        [XmlArrayItem("Area")]
        public List<ClickArea> ClickAreas { get; set; }

        [XmlArray("Characters")]
        [XmlArrayItem("Character")]
        public List<SceneCharacter> Characters { get; set; }

        ClickArea hoveredClickArea;
        bool hoveredClickAreaActive;

        bool showClickAreas;
        Texture2D pixel;

        public Scene()
        {
            Items = new List<SceneItem>();
            ClickAreas = new List<ClickArea>();
            Characters = new List<SceneCharacter>();
        }

        public void SetSceenManager(ScreenManager sm)
        {
            screenManager = sm;
        }

        public void SetSceneManager(SceneManager sm)
        {
            sceneManager = sm;
        }

        public void LoadContent()
        {
            background_texture = screenManager.GameManager.TextureManager.GetTextureAddUser(screenManager.GameManager.StoryManager.ContentFolderStoryFull + BackgroundImage);

            pixel = new Texture2D(screenManager.GameManager.Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            showClickAreas = false;

            foreach (SceneItem itm in Items)
            {
                string[] area_split = itm.Area.Split(',');

                int area_x = (int)(float.Parse(area_split[0]) * screenManager.GameManager.Game.Window.ClientBounds.Width);
                int area_y = (int)(float.Parse(area_split[1]) * screenManager.GameManager.Game.Window.ClientBounds.Height);
                int area_x2 = (int)(float.Parse(area_split[2]) * screenManager.GameManager.Game.Window.ClientBounds.Width);
                int area_y2 = (int)(float.Parse(area_split[3]) * screenManager.GameManager.Game.Window.ClientBounds.Height);
                int area_width = area_x2 - area_x;
                int area_heigth = area_y2 - area_y;

                itm.AreaRect = new Rectangle(area_x, area_y, area_width, area_heigth);
            }

            foreach (ClickArea area in ClickAreas)
            {
                string[] area_split = area.Area.Split(',');

                int area_x = (int)(float.Parse(area_split[0]) * screenManager.GameManager.Game.Window.ClientBounds.Width);
                int area_y = (int)(float.Parse(area_split[1]) * screenManager.GameManager.Game.Window.ClientBounds.Height);
                int area_x2 = (int)(float.Parse(area_split[2]) * screenManager.GameManager.Game.Window.ClientBounds.Width);
                int area_y2 = (int)(float.Parse(area_split[3]) * screenManager.GameManager.Game.Window.ClientBounds.Height);
                int area_width = area_x2 - area_x;
                int area_heigth = area_y2 - area_y;
                
                area.AreaRect = new Rectangle(area_x, area_y, area_width, area_heigth);
            }

            foreach (SceneCharacter charac in Characters)
            {
                string[] area_split = charac.Area.Split(',');

                int area_x = (int)(float.Parse(area_split[0]) * screenManager.GameManager.Game.Window.ClientBounds.Width);
                int area_y = (int)(float.Parse(area_split[1]) * screenManager.GameManager.Game.Window.ClientBounds.Height);
                int area_x2 = (int)(float.Parse(area_split[2]) * screenManager.GameManager.Game.Window.ClientBounds.Width);
                int area_y2 = (int)(float.Parse(area_split[3]) * screenManager.GameManager.Game.Window.ClientBounds.Height);
                int area_width = area_x2 - area_x;
                int area_heigth = area_y2 - area_y;

                charac.AreaRect = new Rectangle(area_x, area_y, area_width, area_heigth);
                charac.LoadContent(screenManager.GameManager);
            }
        }

        public void UnloadContent()
        {
            isActive = false;

            foreach (SceneCharacter charac in Characters)
            {
                charac.UnloadContent(screenManager.GameManager.TextureManager);
            }
            Characters.Clear();

            pixel = null;
            background_texture = null;
            screenManager.GameManager.TextureManager.RemoveUser(BackgroundImage);

        }
        
        public void Update(GameTime gameTime)
        {
            sceneManager.Hud.MouseHoverText = "";

            showClickAreas = screenManager.GameManager.InputManager.IsKeyPressed(Keys.LeftControl);
            hoveredClickAreaActive = false;

            // Check is mouse is over a click area and show the name.
            foreach (ClickArea area in ClickAreas)
            {
                if (area.QuestRequired == 0 || sceneManager.QuestManager.HasActiveQuest(area.QuestRequired))
                {
                    if (screenManager.GameManager.InputManager.IsMouseInArea(area.AreaRect))
                    {
                        sceneManager.Hud.MouseHoverText = area.Name;

                        hoveredClickArea = area;
                        hoveredClickAreaActive = true;
                    }

                    if (screenManager.GameManager.InputManager.IsNewMouseClickArea(area.AreaRect))
                    {
                        switch (area.Type)
                        {
                            case "SceneTransition":
                                sceneManager.LoadNewScene(screenManager.GameManager.StoryManager.ContentFolderStoryFull + area.Action, screenManager);
                                sceneManager.Hud.MouseHoverText = "";
                                break;
                        }
                    }
                }
            }

            // Check is mouse is over a click area and show the name.
            foreach (SceneItem itm in Items)
            {
                if (screenManager.GameManager.InputManager.IsMouseInArea(itm.AreaRect))
                {
                    sceneManager.Hud.MouseHoverText = sceneManager.ItemManager.GetItem(itm.ItemID).Name;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw background
            spriteBatch.Draw(background_texture, new Rectangle(0, 0, screenManager.GameManager.Game.Window.ClientBounds.Width, screenManager.GameManager.Game.Window.ClientBounds.Height), Color.White);

            foreach (SceneCharacter charac in Characters)
            {
                charac.Draw(gameTime, spriteBatch);
            }

            foreach (SceneItem itm in Items)
            {
                spriteBatch.Draw(sceneManager.ItemManager.GetItem(itm.ItemID).ImageTexture, itm.AreaRect, Color.White);
            }

            if (showClickAreas)
            {
                // Draw ClickArea Borders
                foreach (ClickArea area in ClickAreas)
                {
                    if (area.QuestRequired == 0 || sceneManager.QuestManager.HasActiveQuest(area.QuestRequired))
                    {
                        DrawBorder(spriteBatch, area.AreaRect, 3, Color.FromNonPremultiplied(0, 0, 255, 96));
                    }
                }

                // Draw SceneItem Borders
                foreach (SceneItem itm in Items)
                {
                    DrawBorder(spriteBatch, itm.AreaRect, 3, Color.FromNonPremultiplied(255, 255, 0, 96));
                }
            }
            else if (hoveredClickAreaActive)
            {
                if (hoveredClickArea.QuestRequired == 0 || sceneManager.QuestManager.HasActiveQuest(hoveredClickArea.QuestRequired))
                {
                    DrawBorder(spriteBatch, hoveredClickArea.AreaRect, 3, Color.FromNonPremultiplied(0, 0, 255, 96));
                }
            }
        }

        private void DrawBorder(SpriteBatch spriteBatch, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }
    }
}
