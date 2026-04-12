using Microsoft.Xna.Framework;

namespace Solumn.Core
{
    public class Grid
    {
        public const int Columns = 6;
        public const int Rows = 13;

        private Gem[,] _cells = new Gem[Columns,Rows];

        private Rectangle _rectangle;

        public Grid(Rectangle rectangle)
        {
            _rectangle = rectangle;
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    _cells[i,j] = new Gem(GemColor.Empty);
                }
            }
        }

        public Gem GetGem(int x, int y)
        {
            return _cells[x,y];
        }

        public void SetGem(int x, int y, Gem gem)
        {
            _cells[x,y] = gem;
        }

        public bool IsEmpty(int x, int y)
        {
            return _cells[x, y].Color == GemColor.Empty;
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Columns && y >= 0 && y < Rows;
        }
    }
}