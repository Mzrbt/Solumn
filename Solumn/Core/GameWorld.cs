using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Solumn.Core
{
    public class GameWorld
    {
        private Rectangle _rectangle;
        private Grid _grid;
        private Piece _actievePiece;
        private Piece _nextPiece;
        private KeyboardState _previousKeyboardState;
        private Texture2D _pixel;

        private double _fallTimer;
        private double _fallInterval = 1;

        public GameWorld(Rectangle rectangle, GraphicsDevice graphicsDevice)
        {
            _rectangle = rectangle;
            _grid = new Grid(_rectangle);
            _actievePiece = new Piece();
            _nextPiece = new Piece();

            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData([Color.White]);
        }

        public void Update(GameTime gameTime)
        {
            _fallTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_fallTimer >= _fallInterval)
            {
                bool canGoDown = _grid.IsInBounds(_actievePiece.XPosition, _actievePiece.YPosition + 3) && 
                                     _grid.IsEmpty(_actievePiece.XPosition, _actievePiece.YPosition + 3);

                if (canGoDown)
                {
                    _actievePiece.MoveDown();
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        _grid.SetGem(_actievePiece.XPosition, _actievePiece.YPosition + i, _actievePiece.GetGem(i));
                    }
                    _actievePiece = _nextPiece;
                    _nextPiece = new Piece();
                }

                _fallTimer = 0;
            }

            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Left) && _previousKeyboardState.IsKeyUp(Keys.Left))
            {
                if (_grid.IsInBounds(_actievePiece.XPosition - 1, _actievePiece.YPosition) &&
                    _grid.IsEmpty(_actievePiece.XPosition - 1, _actievePiece.YPosition))
                {
                    _actievePiece.MoveLeft();
                }
            }
            if (currentState.IsKeyDown(Keys.Right) && _previousKeyboardState.IsKeyUp(Keys.Right))
            {
                if (_grid.IsInBounds(_actievePiece.XPosition + 1, _actievePiece.YPosition) &&
                    _grid.IsEmpty(_actievePiece.XPosition + 1, _actievePiece.YPosition))
                {
                    _actievePiece.MoveRight();
                }
            }
            if (currentState.IsKeyDown(Keys.Down) && _previousKeyboardState.IsKeyUp(Keys.Down))
            {
                bool canGoDown = _grid.IsInBounds(_actievePiece.XPosition, _actievePiece.YPosition + 3) &&
                                _grid.IsEmpty(_actievePiece.XPosition, _actievePiece.YPosition + 3);
                if (canGoDown)
                {
                    _actievePiece.MoveDown();
                }
            }
            if (currentState.IsKeyDown(Keys.Up) && _previousKeyboardState.IsKeyUp(Keys.Up))
            {
                _actievePiece.Rotate();
            }

            _previousKeyboardState = currentState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int cellWidth = _rectangle.Width / Grid.Columns;
            int cellHeight = _rectangle.Height / Grid.Rows;

            spriteBatch.Draw(_pixel, _rectangle, Color.Black);

            for (int i = 0; i < Grid.Columns; i++)
            {
                for (int j = 0; j < Grid.Rows; j++)
                {
                    Gem gem = _grid.GetGem(i,j);
                    Color color = GetGemColor(gem.Color);

                    Rectangle cellRect = new Rectangle(
                        _rectangle.X + i * cellWidth,
                        _rectangle.Y + j * cellHeight,
                        cellWidth - 1,
                        cellHeight - 1
                    );

                    spriteBatch.Draw(_pixel, cellRect, color);
                }
            }
        }

        private Color GetGemColor(GemColor gemColor)
        {
            return gemColor switch
            {
                GemColor.Red    => Color.Red,
                GemColor.Blue   => Color.Blue,
                GemColor.Green  => Color.Green,
                GemColor.Yellow => Color.Yellow,
                GemColor.Purple => Color.Purple,
                _               => Color.Black
            };
        }
    }
}