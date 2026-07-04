using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solumn.Core;
using Solumn.Managers;
using Solumn.UI;

namespace Solumn.Screens
{
    public class PauseScreen : Screen
    {
        private SpriteFont _font;
        private Button _exitButton;
        private Button _menuButton;
    

        private GameWorld _gameWorld;

        public PauseScreen(ScreenManager screenManager, GameWorld gameWorld) : base(screenManager)
        {
            _gameWorld = gameWorld;
        }

        public override void LoadContent()
        {
            int screenWidth = ScreenManager.GraphicsDevice.Viewport.Width;
            int screenHeight = ScreenManager.GraphicsDevice.Viewport.Height;

            int buttonWidth = 200;
            int buttonHeight = 60;

            _font = ScreenManager.Content.Load<SpriteFont>("fonts/solumn");

            _exitButton = new Button(
                new Rectangle(
                    (screenWidth - buttonWidth) / 2,
                    screenHeight - buttonHeight - 50,
                    buttonWidth,
                    buttonHeight
                ),
                "Resume",
                _font,
                ScreenManager.GraphicsDevice
            );  
            _exitButton.OnClick = () =>
            {
                ScreenManager.Pop();
            };

            _menuButton = new Button(
                new Rectangle((int)(ScreenManager.GraphicsDevice.Viewport.Width / 1.5), ScreenManager.GraphicsDevice.Viewport.Height / 2, 200, 60),
                "Menu",
                _font,
                ScreenManager.GraphicsDevice
            );
            _menuButton.OnClick = () => {
                ScreenManager.Pop();
            };
        }

        public override void Update(GameTime gameTime)
        {
            _exitButton.Update();
            _menuButton.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string titre = "Pause";
            Vector2 tailletitre = _font.MeasureString(titre);
            spriteBatch.DrawString(
                _font,
                titre,
                new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - tailletitre.X) / 2, 100),
                Color.White
            );

            spriteBatch.DrawString(
                _font,
                "Score : " + _gameWorld.Score,
                new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - _font.MeasureString("Score : " + _gameWorld.Score).X) / 2, 150),
                Color.White
            );

            spriteBatch.DrawString(
                _font,
                "Level : " + _gameWorld.Level,
                new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - _font.MeasureString("Level : " + _gameWorld.Level).X) / 2, 200),
                Color.White
            );  
            
            _exitButton.Draw(spriteBatch);
            _menuButton.Draw(spriteBatch);
        }
    }
}