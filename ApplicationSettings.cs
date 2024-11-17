namespace SnakeGame
{
    internal class ApplicationSettings
    {
        internal class Playfield
        {
            internal const int Height = 25;
            internal const int Width = 25;
        }

        internal class Game
        {
            internal const int SpeedInMilliseconds = 100;
        }

        internal class Symbols
        {
            internal const char Empty = ' ';
            internal const char SnakeHead = '+';
            internal const char SnakeBody = '■';
            internal const char Fruit = '#';
        }
    }
}