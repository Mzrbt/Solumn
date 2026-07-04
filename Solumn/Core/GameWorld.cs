using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Solumn.Core
{
    public class GameWorld
    {
        private Rectangle _rectangle;
        private Grid _grid;
        private Piece _activePiece;
        private Piece _nextPiece;
        private KeyboardState _previousKeyboardState;
        private Texture2D _pixel;

        private double _fallTimer;
        private double _fallInterval = 1;

        public bool IsGameOver { get; private set; } = false;
        public int Score { get; private set; } = 0;
        public int Level { get; private set; } = 1;

        public GameWorld(Rectangle rectangle, GraphicsDevice graphicsDevice)
        {
            _rectangle = rectangle;
            _grid = new Grid(_rectangle);
            _activePiece = new Piece();
            _nextPiece = new Piece();

            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData([Color.White]);
        }

        public void Update(GameTime gameTime)
        {
            _fallTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_fallTimer >= _fallInterval)
            {
                bool canGoDown = _grid.IsInBounds(_activePiece.XPosition, _activePiece.YPosition + 3) && 
                                     _grid.IsEmpty(_activePiece.XPosition, _activePiece.YPosition + 3);

                if (canGoDown)
                {
                    _activePiece.MoveDown();
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        _grid.SetGem(_activePiece.XPosition, _activePiece.YPosition + i, _activePiece.GetGem(i));
                    }
                    int cascadeMultiplier = 1;
                    int removed;
                    do
                    {
                        removed = _grid.DetectAndClear();
                        if (removed > 0)
                        {
                            Score += removed * 3 * cascadeMultiplier;
                            Level = (Score / 100) + 1;
                            _fallInterval = Math.Max(0.1, 1.0 - (Level - 1) * 0.1);
                            cascadeMultiplier++;
                            _grid.ApplyGravity();
                        }
                    } while (removed > 0);
                    _activePiece = _nextPiece;
                    _nextPiece = new Piece();
                    if (!_grid.IsEmpty(_activePiece.XPosition, _activePiece.YPosition))
                    {
                        IsGameOver = true;
                    }
                }

                _fallTimer = 0;
            }

            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Left) && _previousKeyboardState.IsKeyUp(Keys.Left))
            {
                bool canGoLeft = true;
                for (int i = 0; i < 3; i++)
                {
                    if (!_grid.IsInBounds(_activePiece.XPosition - 1, _activePiece.YPosition + i) ||
                        !_grid.IsEmpty(_activePiece.XPosition - 1, _activePiece.YPosition + i))
                    {
                        canGoLeft = false;
                        break;
                    }
                }
                if (canGoLeft)
                {
                    _activePiece.MoveLeft();
                }
            }
            if (currentState.IsKeyDown(Keys.Right) && _previousKeyboardState.IsKeyUp(Keys.Right))
            {
                bool canGoRight = true;
                for (int i = 0; i < 3; i++)
                {
                    if (!_grid.IsInBounds(_activePiece.XPosition + 1, _activePiece.YPosition + i) ||
                        !_grid.IsEmpty(_activePiece.XPosition + 1, _activePiece.YPosition + i))
                    {
                        canGoRight = false;
                        break;
                    }
                }
                if (canGoRight)
                {
                    _activePiece.MoveRight();
                }
            }
            if (currentState.IsKeyDown(Keys.Down) && _previousKeyboardState.IsKeyUp(Keys.Down))
            {
                bool canGoDown = _grid.IsInBounds(_activePiece.XPosition, _activePiece.YPosition + 3) &&
                                _grid.IsEmpty(_activePiece.XPosition, _activePiece.YPosition + 3);
                if (canGoDown)
                {
                    _activePiece.MoveDown();
                }
            }
            if (currentState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
            {
                while (_grid.IsInBounds(_activePiece.XPosition, _activePiece.YPosition + 3) &&
                       _grid.IsEmpty(_activePiece.XPosition, _activePiece.YPosition + 3))
                {
                    _activePiece.MoveDown();
                }
            }
            if (currentState.IsKeyDown(Keys.Up) && _previousKeyboardState.IsKeyUp(Keys.Up))
            {
                _activePiece.Rotate();
            }

            _previousKeyboardState = currentState;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
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

            int ghostY = _activePiece.YPosition;
            while (_grid.IsInBounds(_activePiece.XPosition, ghostY + 3) &&
                _grid.IsEmpty(_activePiece.XPosition, ghostY + 3))
            {
                ghostY++;
            }

            for (int i = 0; i < 3; i++)
            {
                Gem gem = _activePiece.GetGem(i);
                Color color = GetGemColor(gem.Color) * 0.3f;
                Rectangle cellRect = new Rectangle(
                    _rectangle.X + _activePiece.XPosition * cellWidth,
                    _rectangle.Y + (ghostY + i) * cellHeight,  // ghostY ici !
                    cellWidth - 1,
                    cellHeight - 1
                );
                spriteBatch.Draw(_pixel, cellRect, color);
            }

            for (int i = 0; i < 3; i++)
            {
                Gem gem = _activePiece.GetGem(i);
                Color color = GetGemColor(gem.Color);

                Rectangle cellRect = new Rectangle(
                    _rectangle.X + _activePiece.XPosition * cellWidth,
                    _rectangle.Y + (_activePiece.YPosition + i) * cellHeight,
                    cellWidth - 1,
                    cellHeight - 1
                );

                spriteBatch.Draw(_pixel, cellRect, color);
            }

            spriteBatch.DrawString(
                font,
                "Next",
                new Vector2(_rectangle.X + _rectangle.Width + 20, _rectangle.Y + (_rectangle.Height / 2 - 1) - 30),
                Color.White
            );

            for (int i = 0; i < 3; i++)
            {
                Gem gem = _nextPiece.GetGem(i);
                Color color = GetGemColor(gem.Color);

                Rectangle cellRect = new Rectangle(
                    _rectangle.X + _rectangle.Width + 20,
                    _rectangle.Y + (_rectangle.Height / 2 - 1) + i * cellHeight,
                    cellWidth - 1,
                    cellHeight - 1
                );

                spriteBatch.Draw(_pixel, cellRect, color);
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