using System.Text;





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



            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task mainGame = MainGameLoop(cancellationToken);
            Task readingTask = ReadDirection(cancellationToken);

            await Task.WhenAny(mainGame, readingTask);

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

        private static async Task MainGameLoop(CancellationToken cancellationToken)
        {
            while (_gameEnded == false)
            {
                // Todo: Move snake

                // Todo: Display updated game field

                await Task.Delay(_gameSettings.speedInMilliseconds, cancellationToken);
            }
        }

        private static async Task ReadDirection(CancellationToken cancellationToken)
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

                // Todo: Read key and change direction
            }
        }
    }
}