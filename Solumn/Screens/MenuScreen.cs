using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solumn.Managers;
using Solumn.UI;
using Solumns.Screens;

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
            _font = ScreenManager.Content.Load<SpriteFont>("fonts/solumn");

            _playButton = new Button(
                new Rectangle(860, 400, 200, 60),
                "Play",
                _font,
                ScreenManager.GraphicsDevice
            );
            _playButton.OnClick = () => { 
                ScreenManager.Push(new PlayScreen(ScreenManager));
            };

            _exitButton = new Button(
                new Rectangle(860, 500, 200, 60),
                "Exit",
                _font,
                ScreenManager.GraphicsDevice
            );
            _exitButton.OnClick = () =>
            {
                Environment.Exit(0);
            };
        }

        public override void Update(GameTime gameTime)
        {
            _playButton.Update();
            _exitButton.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string titre = "Solumn";
            Vector2 tailletitre = _font.MeasureString(titre);
            spriteBatch.DrawString(
                _font,
                titre,
                new Vector2((ScreenManager.GraphicsDevice.Viewport.Width - tailletitre.X) / 2, 100),
                Color.White
            );

            _playButton.Draw(spriteBatch);
            _exitButton.Draw(spriteBatch);
        }
    }
}