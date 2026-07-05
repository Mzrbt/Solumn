using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solumn.Managers;
using Solumn.UI;

namespace Solumn.Screens
{
    public class GameOverScreen : Screen
    {
        private SpriteFont _font;
        private Button _exitButton;
        private Button _restartButton;

        private int _finalScore;
        private int _finalLevel;

        public GameOverScreen(ScreenManager screenManager, int score, int level) : base(screenManager)
        {
            _finalScore = score;
            _finalLevel = level;
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
                "Exit",
                _font,
                ScreenManager.GraphicsDevice
            );  
            _exitButton.OnClick = () =>
            {
                ScreenManager.Pop();
                ScreenManager.Pop();
            };

            _restartButton = new Button(
                new Rectangle(
                    (screenWidth - buttonWidth) / 2,
                    screenHeight - buttonHeight * 2 - 70,
                    buttonWidth,
                    buttonHeight
                ),
                "Restart",
                _font,
                ScreenManager.GraphicsDevice
            );
            _restartButton.OnClick = () =>
            {
                ScreenManager.Pop();
                ScreenManager.Pop();
                ScreenManager.Push(new PlayScreen(ScreenManager));
            };
        }

        public override void Update(GameTime gameTime)
        {
            _exitButton.Update();
            _restartButton.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string titre = "Game Over";
            Vector2 tailletitre = _font.MeasureString(titre);
            spriteBatch.DrawString(
                _font,
                titre,
                new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - tailletitre.X) / 2, 100),
                Color.White
            );

            spriteBatch.DrawString(
                _font,
                "Score : " + _finalScore,
                new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - _font.MeasureString("Score : " + _finalScore).X) / 2, 150),
                Color.White
            );

            spriteBatch.DrawString(
                _font,
                "Level : " + _finalLevel,
                new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - _font.MeasureString("Level : " + _finalLevel).X) / 2, 200),
                Color.White
            );  

            spriteBatch.DrawString(
                _font,
                "Best : " + ScreenManager.ScoreManager.Score,
                new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - _font.MeasureString("Best : " + ScreenManager.ScoreManager.Score).X) / 2, 250),
                Color.White
            );
            
            _exitButton.Draw(spriteBatch);
            _restartButton.Draw(spriteBatch);
        }
    }
}