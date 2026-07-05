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
            if (IsInBounds(x, y))
            {
                _cells[x,y] = gem;
            }
        }

        public bool IsEmpty(int x, int y)
        {
            return _cells[x, y].Color == GemColor.Empty;
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Columns && y >= 0 && y < Rows;
        }

        public int DetectAndClear()
        {
            bool[,] toRemove = new bool[Columns, Rows];
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (_cells[x, y].Color != GemColor.Empty)
                    {
                        if (x <= Columns - 3 && _cells[x, y].Color == _cells[x + 1, y].Color && _cells[x, y].Color == _cells[x + 2, y].Color)
                        {
                            toRemove[x, y] = true;
                            toRemove[x + 1, y] = true;
                            toRemove[x + 2, y] = true;
                        }
                        if (y <= Rows - 3 && _cells[x, y].Color == _cells[x, y + 1].Color && _cells[x, y].Color == _cells[x, y + 2].Color)
                        {
                            toRemove[x, y] = true;
                            toRemove[x, y + 1] = true;
                            toRemove[x, y + 2] = true;
                        }
                        if (x <= Columns - 3 && y <= Rows - 3 && _cells[x, y].Color == _cells[x + 1, y + 1].Color && _cells[x, y].Color == _cells[x + 2, y + 2].Color)
                        {
                            toRemove[x, y] = true;
                            toRemove[x + 1, y + 1] = true;
                            toRemove[x + 2, y + 2] = true;
                        }
                        if (x >= 2 && y <= Rows - 3 && _cells[x, y].Color == _cells[x - 1, y + 1].Color && _cells[x, y].Color == _cells[x - 2, y + 2].Color)
                        {
                            toRemove[x, y] = true;
                            toRemove[x - 1, y + 1] = true;
                            toRemove[x - 2, y + 2] = true;
                        }
                    }
                }
            }
            int count = 0;
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (toRemove[x, y])
                    {
                        _cells[x, y] = new Gem(GemColor.Empty);
                        count++;
                    }
                }
            }
            return count;
        }

        public void ApplyGravity()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = Rows - 1; y > 0; y--)
                {
                    if (_cells[x, y].Color == GemColor.Empty)
                    {
                        for (int k = y - 1; k >= 0; k--)
                        {
                            if (_cells[x, k].Color != GemColor.Empty)
                            {
                                _cells[x, y] = _cells[x, k];
                                _cells[x, k] = new Gem(GemColor.Empty);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}