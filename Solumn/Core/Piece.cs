using System;

namespace Solumn.Core
{
    public class Piece
    {
        private Gem[] piece = new Gem[3];
        public Gem GetGem(int index) => piece[index];

        public int XPosition { get; private set; } = Grid.Columns / 2;
        public int YPosition { get; private set; } = 0;

        private static Random r = new Random();

        public Piece()
        {
            for (int i = 0; i < 3; i++)
            {
                GemColor randomColor = (GemColor)r.Next(0, Enum.GetValues(typeof(GemColor)).Length - 1);
                piece[i] = new Gem(randomColor);
            }
        }

        public void MoveLeft()
        {
            XPosition--;
        }

        public void MoveRight()
        {
            XPosition++;
        }

        public void MoveDown()
        {
            YPosition++;
        }

        public void Rotate()
        {
            Gem temp = piece[0];
            piece[0] = piece[1];
            piece[1] = piece[2];
            piece[2] = temp;
        }
    }
}