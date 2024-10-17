class Program
{
    static void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            System.Console.WriteLine("---- Welcome to ----");
            System.Console.WriteLine("T H E  G U E S T B O O K");
            System.Console.WriteLine();
            System.Console.WriteLine("1. Login");
            System.Console.WriteLine("2. Register");
            System.Console.WriteLine("3. View all members");
            System.Console.WriteLine("4. About us");
            System.Console.WriteLine();
            System.Console.WriteLine("0. Exit program");
            System.Console.WriteLine();

            System.Console.Write("Write here: ");
            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    //Login();
                    break;
                case "2":
                    //Register();
                    break;
                case "3":
                    //ViewAllMembers()
                    break;
                case "4":
                    //AboutUs();
                    break;
                case "0":
                    isRunning = false;
                    Console.Clear();
                    System.Console.WriteLine("Exit program...");
                    break;
                default:
                    Console.Clear();
                    System.Console.WriteLine("Invalid input...");
                    break;
            }
        }
    }
}