
/*
Class representing the platform
Written by Andreas Nygård
*/
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
class Platform
{
    //Fields
    private List<Member> members;
    private Services services;
    //Constructors
    public Platform()
    {
        services = new Services();
        members = services.LoadMembers();
    }
    public Platform(List<Member> m)
    {
        members = m;
        services = new Services();
    }
    //Get and set
    public List<Member> Members
    {
        get => members;
        set => members = value;
    }
    //Method for logging in
    public void Login()
    {
        // Create instance of Services class
        Services services = new Services();

        // Clear console and give instructions
        Console.Clear();
        System.Console.WriteLine("=== Login ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Follow the instructions below to login.");
        System.Console.WriteLine();
        System.Console.WriteLine("Type 'back' to go back...");
        System.Console.WriteLine();

        // Ask for username
        string? username;
        do
        {
            System.Console.Write("Username: ");
            username = Console.ReadLine();
            if (username == "back")
            {
                Console.Clear();
                return;
            }
        } while (String.IsNullOrEmpty(username));

        // Ask for password
        string? password;
        do
        {
            System.Console.Write("Password: ");
            password = services.ReadPassword(); // Use ReadPassword from Services class
            if (password == "back")
            {
                Console.Clear();
                return;
            }
        } while (String.IsNullOrEmpty(password));

        // Match username with password
        //If match - create matched member
        Member? matchedMember = null;
        foreach (var m in members)
        {
            if (username == m.Username && password == m.Password)
            {
                matchedMember = m;
                break;
            }
        }

        // Validate and navigate to users page
        if (matchedMember != null)
        {
            MyPage myPage = new MyPage();
            myPage.MyPageMenu(matchedMember);
        }
        else
        {
            System.Console.WriteLine("Incorrect username or password.");
            new Services().PressKeyAndContinue();
        }
    }
    //Method for registration
    public void Register()
    {
        //Clear console and give instructions
        Console.Clear();
        System.Console.WriteLine("=== Login ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Follow the instructions below.");
        System.Console.WriteLine();
        System.Console.WriteLine("Write 'back' to go back");
        System.Console.WriteLine();

        //Ask for firstname
        string? firstname;
        do
        {
            System.Console.Write("Firstname: ");
            firstname = Console.ReadLine();
            //Give option to go back
            if (firstname?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }
        } while (String.IsNullOrEmpty(firstname));

        //Ask for lastname
        string? lastname;
        do
        {
            System.Console.Write("Lastname: ");
            lastname = Console.ReadLine();

            if (lastname?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }
        } while (String.IsNullOrEmpty(lastname));

        //Ask for email
        string? email;
        while (true)
        {
            System.Console.Write("Email: ");
            email = Console.ReadLine();
            if (email?.ToLower() == "back")
            {
                Console.Clear();
                return; // Exit if user writes "back"
            }

            try
            {
                //Create temporary member object to validate email
                //Exit the loop is email is valid
                var tempMember = new Member(firstname, lastname, email!, DateOnly.MinValue, "", "", null!);
                break;
            }
            catch (ArgumentException e)
            {
                //Display error message if email is invalid
                System.Console.WriteLine(e.Message);
            }
        }
        //Ask for birthday
        DateOnly birthday;
        while (true)
        {
            //Ask for right format
            System.Console.Write("Birthday (yyyy-mm-dd): ");
            string? input = Console.ReadLine();

            //If user input is "back" - return
            if (input?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }

            try
            {
                //Create temporary member to validate birtday
                var tempMember = new Member("temp", "temp", "temp@example.com", DateOnly.ParseExact(input!, "yyyy-MM-dd"), "temp", "temp", null!);
                birthday = tempMember.Birthday;
                break;
            }
            catch (FormatException)
            {
                //Handle invalid date format
                System.Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
            }
            catch (ArgumentException ex)
            {
                //Handle other argument exceptions
                System.Console.WriteLine(ex.Message);
            }
        }
        //Ask for username
        string? username;
        do
        {
            System.Console.Write("Username: ");
            username = Console.ReadLine();
            if (username?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }
        } while (String.IsNullOrEmpty(username));

        //Create password variables
        string? firstPassword;
        string? secondPassword;

        //Ask for password
        while (true)
        {
            System.Console.Write("Password: ");
            //Call ReadPassword method from Services class
            firstPassword = services.ReadPassword();
            if (firstPassword?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }
            //Double check password to make sure user input is right
            System.Console.Write("Enter password again: ");
            secondPassword = services.ReadPassword();
            if (secondPassword?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }
            //If passwords match - break out of loop
            //Else display error
            if (firstPassword == secondPassword)
            {
                break;
            }
            else
            {
                System.Console.WriteLine("Passwords did not match. Try again.");
            }
        }

        //Create new account 
        Account newAccount = new Account(new List<Post>(), new List<Member>());

        //Create new member using inputs and new account
        //Add to members list
        Member newMember = new Member(firstname, lastname, email!, birthday, username, firstPassword!, newAccount);
        members.Add(newMember);

        System.Console.WriteLine();
        System.Console.WriteLine("Registration successful!");

        services.SaveMembers(members);

        new Services().PressKeyAndContinue();
    }
    //Method for viewing all members on platform
    public void ViewAllMembers()
    {
        Console.Clear();
        System.Console.WriteLine("=== All members ===");
        System.Console.WriteLine();

        //Check if members list is empty
        if (members.Count < 1)
        {
            System.Console.WriteLine("List of members is empty...");
        }
        else
        {
            //If list != empty - display given information
            foreach (var m in members)
            {
                System.Console.WriteLine("---");
                System.Console.WriteLine($"      {m.Username.ToUpper()}");
                System.Console.WriteLine($"- {m.Firstname} {m.Lastname}");
                System.Console.WriteLine($"- Amount of posts: {m.Account.GetPosts().Count}");
                System.Console.WriteLine($"- Amount of friends; {m.Account.GetFriends().Count}");
                System.Console.WriteLine("----");
            }
        }
        new Services().PressKeyAndContinue();
    }
    //Method for displaying information about the program
    public void AboutUs()
    {
        Console.Clear();
        System.Console.WriteLine("=== About us ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Information about us...");
        System.Console.WriteLine();
        System.Console.WriteLine("Detta är projektarbetet i kurs DT071G. Projektet har skapats av Andreas Nygård.");
        System.Console.WriteLine("Det är en gästbok där användaren har möjlighet att registrera sig och logga in till sin sida.");
        System.Console.WriteLine("Väl där kan man lägga till, ta bort och visa alla inlägg.");
        System.Console.WriteLine("Man har även möjlighet att söka efter andra användare och börja följa dessa.");
        System.Console.WriteLine();

        new Services().PressKeyAndContinue();
    }
    //Method for getting all members 
    public List<Member> GetAllMembers()
    {
        if (members == null)
        {
            members = services.LoadMembers(); // Load members if not loaded
        }
        return members;
    }
    //Call method for Services
    public void LoadMembers()
    {
        members = services.LoadMembers();
    }
    //Call method from Services
    public void SaveMembers()
    {
        services.SaveMembers(members);
    }
}