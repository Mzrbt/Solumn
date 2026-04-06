using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solumn.Managers;

namespace Solumn.Screens
{
    public abstract class Screen
    {
        protected ScreenManager ScreenManager { get; set; }

        public Screen(ScreenManager screenManager)
        {
            ScreenManager = screenManager;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}