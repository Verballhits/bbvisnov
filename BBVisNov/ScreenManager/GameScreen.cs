using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class GameScreen
    {
        protected bool isActive = true;
        public Boolean IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        protected ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }

        public GameScreen(ScreenManager sm)
        {
            screenManager = sm;
        }

        public virtual void Initialize()
        {
        }

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
