/*
Program class
Written by Andreas Nygård
*/
class Program
{
    static void Main()
    {
        //Create platform object with list of members
        Platform platform = new Platform(new List<Member>());
        bool isRunning = true;

        //Load all current members from members list
        platform.LoadMembers();

        //While loop for user experience
        while (isRunning)
        {
            //Give options in main menu
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

            //Different options depending on input
            switch (userInput)
            {
                case "1":
                    platform.Login();
                    break;
                case "2":
                    platform.Register();
                    break;
                case "3":
                    platform.ViewAllMembers();
                    break;
                case "4":
                    platform.AboutUs();
                    break;
                case "0":
                    //Break loop if input = 0
                    isRunning = false;
                    Console.Clear();
                    System.Console.WriteLine("Exit program...");
                    platform.SaveMembers();
                    break;
                default:
                    //Error message if input is invalid
                    Console.Clear();
                    System.Console.WriteLine("Invalid input...");
                    System.Console.WriteLine();
                    break;
            }
        }
    }
}