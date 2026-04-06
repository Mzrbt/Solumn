using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Solumn.UI;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Solumnn;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Button _playButton;
    private Button _exitButton;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        _graphics.PreferredBackBufferWidth = screenWidth;
        _graphics.PreferredBackBufferHeight = screenHeight;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _font = Content.Load<SpriteFont>("fonts/solumn");

        _playButton = new Button(
            new Rectangle(860, 400, 200, 60),
            "Play",
            _font,
            GraphicsDevice
        );
        _playButton.OnClick = () => { /* TODO */ };

        _exitButton = new Button(
            new Rectangle(860, 500, 200, 60),
            "Exit",
            _font,
            GraphicsDevice
        );
        _exitButton.OnClick = () => Exit();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _playButton.Update();
        _exitButton.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        string titre = "Solumn";
        Vector2 tailletitre = _font.MeasureString(titre);
        _spriteBatch.DrawString(
            _font,
            titre,
            new Vector2((GraphicsDevice.Viewport.Width - tailletitre.X) / 2, 100),
            Color.White
        );

        _playButton.Draw(_spriteBatch);
        _exitButton.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}