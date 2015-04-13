using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BBVisNov
{
    public class InputManager
    {
        GameManager gameManager;

        KeyboardState previous_keyboardstate;
        KeyboardState current_keyboardstate;

        MouseState previous_mousestate;
        MouseState current_mousestate;

        Vector2 mouseposition;

        public InputManager(GameManager gm)
        {
            gameManager = gm;
        }

        public void Initialize()
        {
            previous_keyboardstate = Keyboard.GetState();
            current_keyboardstate = Keyboard.GetState();

            previous_mousestate = Mouse.GetState();
            current_mousestate = Mouse.GetState();

            mouseposition = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {
            previous_keyboardstate = current_keyboardstate;
            current_keyboardstate = Keyboard.GetState();

            previous_mousestate = current_mousestate;
            current_mousestate = Mouse.GetState();

            mouseposition = new Vector2(current_mousestate.X, current_mousestate.Y);
        }

        public bool IsKeyPressed(Keys key)
        {
            if (!gameManager.Game.IsActive) { return false; }

            return current_keyboardstate.IsKeyDown(key);
        }

        public bool IsNewKeyPress(Keys key)
        {
            if (!gameManager.Game.IsActive) { return false; }

            return current_keyboardstate.IsKeyDown(key) && previous_keyboardstate.IsKeyUp(key);
        }

        private Keys[] keys_menu_up = new Keys[] { Keys.W, Keys.Up };

        public bool IsNewMenuUpPress()
        {
            if (!gameManager.Game.IsActive) { return false; }

            foreach (Keys key in keys_menu_up)
            {
                if (current_keyboardstate.IsKeyDown(key) && previous_keyboardstate.IsKeyUp(key))
                {
                    return true;
                }
            }

            return false;
        }

        private Keys[] keys_menu_down = new Keys[] { Keys.S, Keys.Down };

        public bool IsNewMenuDownPress()
        {
            if (!gameManager.Game.IsActive) { return false; }

            foreach (Keys key in keys_menu_down)
            {
                if (current_keyboardstate.IsKeyDown(key) && previous_keyboardstate.IsKeyUp(key))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsMouseInArea(Rectangle area)
        {
            if (!gameManager.Game.IsActive) { return false; }

            return area.Contains(new Point(current_mousestate.X, current_mousestate.Y));
        }

        public bool IsNewMouseClickArea(Rectangle area)
        {
            if (!gameManager.Game.IsActive) { return false; }

            if (current_mousestate.LeftButton == ButtonState.Pressed &&
                previous_mousestate.LeftButton == ButtonState.Released &&
                area.Contains(new Point(current_mousestate.X, current_mousestate.Y)))
            {
                return true;
            }

            return false;
        }

        public Vector2 MousePosition
        {
            get { return mouseposition; }
        }
    }
}
