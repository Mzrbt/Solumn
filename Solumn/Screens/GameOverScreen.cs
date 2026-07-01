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

        private int _finalScore;

        public GameOverScreen(ScreenManager screenManager, int score) : base(screenManager)
        {
            _finalScore = score;
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
        }

        public override void Update(GameTime gameTime)
        {
            _exitButton.Update();
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

            _exitButton.Draw(spriteBatch);
        }
    }
}