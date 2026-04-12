namespace Solumn.Game
{
    public enum GemColor { Red, Blue, Green, Yellow, Purple, Empty }

    public class Gem
    {
        public GemColor Color { get; private set; }
        
        public Gem(GemColor color)
        {
            Color = color;
        }
    }
}