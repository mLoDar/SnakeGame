using System.Text;
using System.Drawing;





namespace SnakeGame
{
    internal enum SnakeDirection
    {
        Up,
        Down,
        Left,
        Right
    }



    internal class Program
    {
        private static SnakeDirection _currentDirection = SnakeDirection.Right;

        private static readonly ApplicationSettings.Game _gameSettings = new();
        private static readonly ApplicationSettings.Symbols _symbolSettings = new();
        private static readonly ApplicationSettings.Playfield _playfieldSettings = new();

        private static Gamefield _gameField;

        private static bool _gameEnded = false;



        static async Task Main()
        {
        LabelMethodBeginning:

            Console.Title = "SnakeGame";
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            Console.Clear();
            Console.SetCursorPosition(0, 4);
            Console.ForegroundColor = ConsoleColor.DarkBlue;



            Console.WriteLine("             ┏┓    ┓    ┏┓         ");
            Console.WriteLine("             ┗┓┏┓┏┓┃┏┏┓ ┃┓┏┓┏┳┓┏┓  ");
            Console.Write("             ┗┛┛┗┗┻┛┗┗  ┗┛┗┻┛┗┗┗   ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("│");
            Console.WriteLine("          ┌────────────────────────┘");
            Console.WriteLine("          │ Movement -> Arrow Keys");
            Console.WriteLine("          │ Exit at any time with ESC");
            Console.WriteLine("          │                          ");
            Console.WriteLine("          │ Press any key to start   ");
            Console.WriteLine("          └──                        ");



            if (Console.ReadKey().Key.Equals(ConsoleKey.Escape))
            {
                Environment.Exit(0);
            }



            Console.Clear();
            Console.SetCursorPosition(0, 4);

            _gameEnded = false;
            _currentDirection = SnakeDirection.Right;

            _gameField = new Gamefield(_playfieldSettings.height, _playfieldSettings.width);
            _gameField.Init();

            _gameField.Display();



            CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task gameLoop = GameLoop(cancellationToken);
            Task listenForNewDirection = ListenForNewDirection(cancellationToken);

            await Task.WhenAny(gameLoop, listenForNewDirection);

            cancellationTokenSource.Cancel();



            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("          GAME OVER");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("          Press any key to continue ...");



            if (Console.ReadKey().Key.Equals(ConsoleKey.Escape))
            {
                Environment.Exit(0);
            }

            goto LabelMethodBeginning;
        }

        private static async Task GameLoop(CancellationToken cancellationToken)
        {
            while (_gameEnded == false)
            {
                MoveSnake();

                Console.SetCursorPosition(0, 4);
                _gameField.Display();

                await Task.Delay(_gameSettings.speedInMilliseconds, cancellationToken);
            }
        }

        private static async Task ListenForNewDirection(CancellationToken cancellationToken)
        {
            while (_gameEnded == false)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (Console.KeyAvailable == false)
                {
                    await Task.Delay(50, cancellationToken);
                    continue;
                }



                ConsoleKey pressedKey = Console.ReadKey(true).Key;

                ChangeSnakeDirection(pressedKey);
            }
        }

        private static void MoveSnake()
        {
            int snakeHeadX = _gameField.snakeHeadPosition.X;
            int snakeHeadY = _gameField.snakeHeadPosition.Y;

            bool collectedFruit = false;

            int deltaX = 0;
            int deltaY = 0;

            switch (_currentDirection)
            {
                case SnakeDirection.Right:
                    deltaX = 1;
                    break;

                case SnakeDirection.Left:
                    deltaX = -1;
                    break;

                case SnakeDirection.Down:
                    deltaY = 1;
                    break;

                case SnakeDirection.Up:
                    deltaY = -1;
                    break;

                default:
                    break;
            }

            int newHeadX = snakeHeadX + deltaX;
            int newHeadY = snakeHeadY + deltaY;



            if (_gameField.fieldLayout[newHeadY, newHeadX].Equals(_symbolSettings.fruit))
            {
                collectedFruit = true;
                _gameField.snakeLength++;
            }

            if (_gameField.CollisionDetected(_currentDirection) == true)
            {
                _gameEnded = true;
                return;
            }



            _gameField.snakeHeadPosition = new Point(newHeadX, newHeadY);
            _gameField.snakeTailPositions.AddFirst(new Point(snakeHeadX, snakeHeadY));

            _gameField.fieldLayout[newHeadY, newHeadX] = _symbolSettings.snakeHead;
            _gameField.fieldLayout[snakeHeadY, snakeHeadX] = _symbolSettings.snakeBody;



            if (collectedFruit == true)
            {
                _gameField.PlaceNewFruit();
                return;
            }

            _gameField.RemoveSnakeTail();
        }

        private static void ChangeSnakeDirection(ConsoleKey pressedKey)
        {
            switch (pressedKey)
            {
                case ConsoleKey.RightArrow:
                    if (_currentDirection != SnakeDirection.Left)
                    {
                        _currentDirection = SnakeDirection.Right;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (_currentDirection != SnakeDirection.Right)
                    {
                        _currentDirection = SnakeDirection.Left;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (_currentDirection != SnakeDirection.Up)
                    {
                        _currentDirection = SnakeDirection.Down;
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (_currentDirection != SnakeDirection.Down)
                    {
                        _currentDirection = SnakeDirection.Up;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}