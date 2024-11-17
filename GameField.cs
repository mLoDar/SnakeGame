using System.Drawing;





namespace SnakeGame
{
    internal class Gamefield
    {
        private static readonly ApplicationSettings.Symbols _symbolSettings = new();

        internal char[,] fieldLayout = new char[0, 0];

        private static readonly Random numberGenerator = new();



        public Gamefield(int fieldHeight, int fieldWidth)
        {
            if (fieldHeight < 5 || fieldWidth < 5)
            {
                Console.Clear();
                Console.WriteLine("\r\n\r\n\r\n\tWARNING: The gamefield is to small! Check the settings and set the heigth/width to at least 5.");

                Thread.Sleep(3000);

                Environment.Exit(0);
            }

            fieldLayout = new char[fieldHeight, fieldWidth];
        }

        internal void Init()
        {
            for (int row = 0; row < fieldLayout.GetLength(0); row++)
            {
                for (int column = 0; column < fieldLayout.GetLength(1); column++)
                {
                    fieldLayout[row, column] = _symbolSettings.empty;
                }
            }

            int snakeStartY = fieldLayout.GetLength(0) / 2;
            int snakeStartX = fieldLayout.GetLength(1) / 2;

            fieldLayout[snakeStartY, snakeStartX] = _symbolSettings.snakeHead;

            fieldLayout[snakeStartY, snakeStartX + 2] = _symbolSettings.fruit;
        }

        internal void Display()
        {
            int fieldWidth = fieldLayout.GetLength(1);
            int fieldHeigth = fieldLayout.GetLength(0);



            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("             ┏┓        ┓          ");
            Console.WriteLine("             ┃┓┏┓┏┳┓┏┓ ┃ ┏┓┓┏┏┓┓┏╋");
            Console.WriteLine("             ┗┛┗┻┛┗┗┗  ┗┛┗┻┗┫┗┛┗┻┗");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("          ┌────────────────────────┘");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"          ┌{new string('─', fieldWidth)}┐");

            for (int row = 0; row < fieldHeigth; row++)
            {
                Console.Write("          │");

                for (int column = 0; column < fieldWidth; column++)
                {
                    char currentChar = fieldLayout[row, column];

                    Console.ForegroundColor = ConsoleColor.White;

                    if (currentChar.Equals(_symbolSettings.fruit))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (currentChar.Equals(_symbolSettings.snakeHead))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (currentChar.Equals(_symbolSettings.snakeBody))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.Write(currentChar);
                }

                Console.WriteLine("│");
            }

            Console.WriteLine($"          └{new string('─', fieldWidth)}┘");
        }

        internal void PlaceNewFruit()
        {
            List<Point> emptyPositions = GetEmptyPositions();

            if (emptyPositions.Count <= 0)
            {
                return;
            }

            Point newFruitPosition = emptyPositions[numberGenerator.Next(0, emptyPositions.Count)];

            fieldLayout[newFruitPosition.Y, newFruitPosition.X] = _symbolSettings.fruit;
        }

        private List<Point> GetEmptyPositions()
        {
            int fieldWidth = fieldLayout.GetLength(1);
            int fieldHeigth = fieldLayout.GetLength(0);

            List<Point> positions = [];

            for (int row = 0; row < fieldHeigth; row++)
            {
                for (int column = 0; column < fieldWidth; column++)
                {
                    char currentChar = fieldLayout[row, column];

                    if (currentChar.Equals(_symbolSettings.empty))
                    {
                        positions.Add(new Point(column, row));
                    }
                }
            }

            return positions;
        }
    }
}