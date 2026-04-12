using Microsoft.Xna.Framework;

namespace Solumn.Game
{
    public class GameWorld
    {
        private Rectangle _rectangle;
        private Grid _grid;
        private Piece _actievePiece;
        private Piece _nextPiece;

        public GameWorld(Rectangle rectangle)
        {
            _rectangle = rectangle;
            _grid = new Grid(_rectangle);
            _actievePiece = new Piece();
            _nextPiece = new Piece();
        }
    }
}