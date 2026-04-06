using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solumn.Screens;

namespace Solumn.Managers
{
    public class ScreenManager
    {
        private Stack<Screen> _screens = new Stack<Screen>();
        private GraphicsDevice _graphicsDevice;

        public ScreenManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void Push(Screen screen)
        {
            _screens.Push(screen);
            screen.LoadContent();
        }

        public void Pop()
        {
            _screens.Pop();
        }

        public void Replace(Screen screen)
        {
            _screens.Pop();
            Push(screen);
        }

        public void Update(GameTime gameTime)
        {
            if (_screens.Count > 0)
            {
                _screens.Peek().Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_screens.Count > 0)
            {
                _screens.Peek().Draw(spriteBatch);
            }
        }

        public GraphicsDevice GraphicsDevice => _graphicsDevice;
    }
}