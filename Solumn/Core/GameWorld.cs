using Microsoft.Xna.Framework;

namespace Solumn.Core
{
    public class GameWorld
    {
        private Rectangle _rectangle;
        private Grid _grid;
        private Piece _actievePiece;
        private Piece _nextPiece;

        private double _fallTimer;
        private double _fallInterval = 1;

        public GameWorld(Rectangle rectangle)
        {
            _rectangle = rectangle;
            _grid = new Grid(_rectangle);
            _actievePiece = new Piece();
            _nextPiece = new Piece();
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
        }
    }
}