using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solumn.Managers;
using Solumn.Screens;
using Solumn.UI;

namespace Solumns.Screens 
{
    public class PlayScreen : Screen
    {
        private SpriteFont _font;

        private Button _menuButton;

        public PlayScreen(ScreenManager screenManager) : base(screenManager)
        {
    
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

            _menuButton.Draw(spriteBatch);
        }

        public override void LoadContent()
        {
            _font = ScreenManager.Content.Load<SpriteFont>("fonts/solumn");

            _menuButton = new Button(
                new Rectangle(860, 500, 200, 60),
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
            _menuButton.Update();
        }
    }
}