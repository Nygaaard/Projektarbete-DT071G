class Platform
{
    private List<Member> members;
    public Platform(List<Member> m)
    {
        members = m;
    }
    public List<Member> Members
    {
        get => members;
        set => members = value;
    }
    public void Login()
    {

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
            System.Console.Write("Firstname");
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
            System.Console.Write("Lastname");
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

        System.Console.WriteLine("Registration successful!");

        new Services().PressKeyAndContinue();
    }
    public void ViewAllMembers()
    {
        
    }
    public void AboutUs()
    {

    }
    public List<Member> GetAllMembers()
    {
        return members;
    }
}