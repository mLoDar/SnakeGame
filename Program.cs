using System.Text;





namespace SnakeGame
{
    internal class Program
    {
        static void Main()
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

            goto LabelMethodBeginning;
        }
    }
}