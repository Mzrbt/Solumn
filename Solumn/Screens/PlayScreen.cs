using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Solumn.Core;
using Solumn.Managers;
using Solumn.UI;

namespace Solumn.Screens 
{
    public class PlayScreen : Screen
    {
        private SpriteFont _font;
        private GameWorld _gameWorld;

        private Button _menuButton;
        private KeyboardState _previousKeyboardState;
        private bool _gameOverHandled = false;

        public PlayScreen(ScreenManager screenManager) : base(screenManager)
        {
    
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            _gameWorld.Draw(spriteBatch, _font);
            _menuButton.Draw(spriteBatch);
            spriteBatch.DrawString(
                _font,
                "Score : " + _gameWorld.Score,
                new Vector2(50, 50),
                Color.White
            );
            spriteBatch.DrawString(
                _font,
                "Level : " + _gameWorld.Level,
                new Vector2(50, 80),
                Color.White
            );
        }

        public override void LoadContent()
        {
            _font = ScreenManager.Content.Load<SpriteFont>("fonts/solumn");

            Rectangle gameRect = new Rectangle(
                100,
                100,
                ScreenManager.GraphicsDevice.Viewport.Width / 2,
                ScreenManager.GraphicsDevice.Viewport.Height - 200
            );

            _gameWorld = new GameWorld(gameRect, ScreenManager.GraphicsDevice);

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
            _gameWorld.Update(gameTime);
            _menuButton.Update();

            if (_gameWorld.IsGameOver && !_gameOverHandled)
            {
                _gameOverHandled = true;
                ScreenManager.Push(new GameOverScreen(ScreenManager, _gameWorld.Score, _gameWorld.Level));
            }

            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape))
            {
                ScreenManager.Push(new PauseScreen(ScreenManager, _gameWorld));
            }

            _previousKeyboardState = currentState;
        }
    }
}