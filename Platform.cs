using System.Text.Json;
using System.Text.Json.Serialization;
class Platform
{
    private List<Member> members;
    private Services services;
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
    public List<Member> Members
    {
        get => members;
        set => members = value;
    }
    public void Login()
    {
        Console.Clear();
        System.Console.WriteLine("=== Login ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Follow the instructions below to login.");
        System.Console.WriteLine();
        System.Console.WriteLine("Type 'back' to go back...");
        System.Console.WriteLine();

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

        string? password;
        do
        {
            System.Console.Write("Password: ");
            password = Console.ReadLine();
            if (password == "back")
            {
                Console.Clear();
                return;
            }
        } while (String.IsNullOrEmpty(password));

        Member? matchedMember = null;

        foreach (var m in members)
        {
            if (username == m.Username && password == m.Password)
            {
                matchedMember = m;
                break; 
            }
        }

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

    public void Register()
    {
        Console.Clear();
        System.Console.WriteLine("=== Login ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Follow the instructions below.");
        System.Console.WriteLine();
        System.Console.WriteLine("Write 'back' to go back");
        System.Console.WriteLine();

        string? firstname;
        do
        {
            System.Console.Write("Firstname: ");
            firstname = Console.ReadLine();

            if (firstname?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }
        } while (String.IsNullOrEmpty(firstname));

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

        string? email;
        while (true)
        {
            System.Console.Write("Email: ");
            email = Console.ReadLine();
            if (email?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }

            try
            {
                var tempMember = new Member(firstname, lastname, email!, DateOnly.MinValue, "", "", null!);
                break;
            }
            catch (ArgumentException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        DateOnly birthday;
        while (true)
        {
            System.Console.Write("Birthday (yyyy-mm-dd): ");
            string? input = Console.ReadLine();

            if (input?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }

            try
            {
                var tempMember = new Member("temp", "temp", "temp@example.com", DateOnly.ParseExact(input!, "yyyy-MM-dd"), "temp", "temp", null!);
                birthday = tempMember.Birthday;
                break;
            }
            catch (FormatException)
            {
                System.Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

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

        string? firstPassword;
        string? secondPassword;

        while (true)
        {
            System.Console.Write("Password: ");
            firstPassword = Console.ReadLine();
            if (firstPassword?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }

            System.Console.Write("Enter password again: ");
            secondPassword = Console.ReadLine();
            if (secondPassword?.ToLower() == "back")
            {
                Console.Clear();
                return;
            }

            if (firstPassword == secondPassword)
            {
                break;
            }
            else
            {
                System.Console.WriteLine("Passwords did not match. Try again.");
            }
        }

        Account newAccount = new Account(new List<Post>(), new List<Member>());

        Member newMember = new Member(firstname, lastname, email!, birthday, username, firstPassword!, newAccount);
        members.Add(newMember);

        System.Console.WriteLine();
        System.Console.WriteLine("Registration successful!");

        new Services().PressKeyAndContinue();
    }
    public void ViewAllMembers()
    {
        Console.Clear();
        System.Console.WriteLine("=== All members ===");
        System.Console.WriteLine();

        if (members.Count < 1)
        {
            System.Console.WriteLine("List of members is empty...");
        }
        else
        {
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
    public void AboutUs()
    {
        Console.Clear();
        System.Console.WriteLine("=== About us ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Information about us...");
        System.Console.WriteLine();

        new Services().PressKeyAndContinue();
    }
    public List<Member> GetAllMembers()
    {
        if (members == null)
        {
            members = services.LoadMembers(); // Ladda medlemmar om de inte är laddade
        }
        return members;
    }
    public void LoadMembers()
    {
        members = services.LoadMembers();
    }
    public void SaveMembers()
    {
        services.SaveMembers(members);
    }
}