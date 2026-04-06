using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solumn.Managers;
using Solumn.UI;

namespace Solumn.Screens
{
    public class MenuScreen : Screen
    {
        private SpriteFont _font;
        private Button _playButton;
        private Button _exitButton;

        public MenuScreen(ScreenManager screenManager) : base(screenManager)
        {
            
        }

        public override void LoadContent()
        {
            // TODO
        }

        public override void Update(GameTime gameTime)
        {
            _playButton.Update();
            _exitButton.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _playButton.Draw(spriteBatch);
            _exitButton.Draw(spriteBatch);
        }
    }
}