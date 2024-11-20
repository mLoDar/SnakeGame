namespace SnakeGame
{
    internal class ApplicationSettings
    {
        internal class Playfield
        {
            internal readonly int height = 10;
            internal readonly int width = 30;
        }

        internal class Game
        {
            internal readonly int speedInMilliseconds = 100;
        }

        internal class Symbols
        {
            internal readonly char empty = ' ';
            internal readonly char snakeHead = '+';
            internal readonly char snakeBody = '■';
            internal readonly char fruit = '#';
        }
    }
}