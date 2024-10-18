class MyPage
{
    // private Account? account;
    // public MyPage(){}
    // public MyPage(Account acc)
    // {
    //     account = acc;
    // }
    // public Account Account
    // {
    //     get => account!;
    //     set => account = value;
    // }
    public void MyPageMenu(Member matchedMember)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            System.Console.WriteLine("=== My page ===");
            System.Console.WriteLine();
            System.Console.WriteLine($"Hello {matchedMember.Firstname}");
            System.Console.WriteLine();
            System.Console.WriteLine("1. Add post");
            System.Console.WriteLine("2. Delete post");
            System.Console.WriteLine("3. Read all posts");
            System.Console.WriteLine("4. Search friends");
            System.Console.WriteLine("5. My friends");
            System.Console.WriteLine("6. Account information");
            System.Console.WriteLine();
            System.Console.WriteLine("0. Go back");
            System.Console.WriteLine();

            System.Console.Write("Write here: ");
            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    AddPost(matchedMember);
                    break;
                case "2":
                    DeletePost(matchedMember);
                    break;
                case "3":
                    ReadAllPosts(matchedMember);
                    break;
                case "4":
                    SearchFriends(matchedMember);
                    break;
                case "5":
                    MyFriends(matchedMember);
                    break;
                case "6":
                    //AccountInformation();
                    break;
                case "0":
                    isRunning = false;
                    Console.Clear();
                    System.Console.WriteLine("Go back...");
                    break;
                default:
                    Console.Clear();
                    System.Console.WriteLine("Invalid input...");
                    System.Console.WriteLine();
                    break;
            }
        }
    }
    public void AddPost(Member matchedMember)
    {
        List<Post> posts = matchedMember.Account.GetPosts();
        Console.Clear();
        System.Console.WriteLine("=== Add a post ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Write your message below.");
        System.Console.WriteLine();

        System.Console.Write("Message: ");
        string? message = Console.ReadLine();

        if (String.IsNullOrEmpty(message))
        {
            System.Console.WriteLine("Write something...");
            return;
        }
        else
        {
            Post newPost = new Post(message);
            posts.Add(newPost);
        }
        System.Console.WriteLine();
        System.Console.WriteLine("Post added successfully!");
        new Services().PressKeyAndContinue();
    }
    public void DeletePost(Member matchedMember)
    {
        List<Post> posts = matchedMember.Account.GetPosts();

        Console.Clear();
        System.Console.WriteLine("=== Delete a post ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Choose the index of the post you want to delete.");
        System.Console.WriteLine();

        System.Console.Write("Index: ");
        System.Console.WriteLine();

        int i = 0;

        foreach (var post in posts)
        {
            System.Console.WriteLine($"[{i}] - {post.Message}");
            i++;
        }

        string? userInput = Console.ReadLine();

        if (!String.IsNullOrEmpty(userInput) && int.TryParse(userInput, out int indexNumber))
        {
            if (indexNumber >= 0 || indexNumber < posts.Count)
            {
                System.Console.WriteLine("Post deleted...");
                posts.RemoveAt(indexNumber);
            }
        }
        else
        {
            System.Console.WriteLine("Index not found...");
            return;
        }

        new Services().PressKeyAndContinue();
    }
    public void ReadAllPosts(Member matchedMember)
    {
        Console.Clear();
        System.Console.WriteLine("=== All your posts ===");
        System.Console.WriteLine();

        List<Post> posts = matchedMember.Account.GetPosts();
        int i = 0;

        if (posts.Count < 1)
        {
            System.Console.WriteLine("You have no current posts...");
            return;
        }
        else
        {
            foreach (var post in posts)
            {
                System.Console.WriteLine($"[{i}] - {post.Message}");
                System.Console.WriteLine();
                i++;
            }
        }

        new Services().PressKeyAndContinue();
    }
    public void SearchFriends(Member matchedMember)
    {
        Platform platform = new Platform();

        List<Member> members = platform.GetAllMembers();

        if (members == null || members.Count == 0)
        {
            Console.Clear();
            System.Console.WriteLine("No members found. Please make sure the platform has members loaded.");
            new Services().PressKeyAndContinue();
            return;
        }

        Console.Clear();
        System.Console.WriteLine("=== Search and follow friends ===");
        System.Console.WriteLine();

        List<Member> filteredMembers = members.Where(m => m.Username != matchedMember.Username).ToList();

        if (filteredMembers.Count == 0)
        {
            System.Console.WriteLine("There are no other members to follow.");
            new Services().PressKeyAndContinue();
            return;
        }

        int index = 0;
        foreach (var member in filteredMembers)
        {
            System.Console.WriteLine($"[{index}] {member.Username} - {member.Firstname} {member.Lastname}");
            index++;
        }

        System.Console.WriteLine();
        System.Console.Write("Enter the index of the user you want to follow: ");
        string? input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int selectedIndex))
        {
            if (selectedIndex >= 0 && selectedIndex < filteredMembers.Count)
            {
                Member selectedMember = filteredMembers[selectedIndex];

                if (matchedMember.Account.GetFriends().Any(f => f.Username == selectedMember.Username))
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("You are already following this user.");
                }
                else
                {
                    matchedMember.Account.GetFriends().Add(selectedMember);
                    System.Console.WriteLine();
                    System.Console.WriteLine($"{selectedMember.Username} has been added to your friends list!");
                }
            }
            else
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Invalid index.");
            }
        }
        else
        {
            System.Console.WriteLine("Invalid input.");
        }

        new Services().PressKeyAndContinue();
    }
    public void MyFriends(Member matchedMember)
    {
        bool stayInFriendsMenu = true; 

        while (stayInFriendsMenu)
        {
            Console.Clear();
            System.Console.WriteLine("=== My friends ===");
            System.Console.WriteLine();
            System.Console.WriteLine("Here is a list of all your friends:");
            System.Console.WriteLine();

            List<Member> friends = matchedMember.Account.GetFriends();

            if (friends.Count == 0)
            {
                System.Console.WriteLine("You don't have any friends yet.");
                new Services().PressKeyAndContinue();
                return;
            }

            int i = 0;
            foreach (var friend in friends)
            {
                System.Console.WriteLine($"[{i}] - {friend.Username}");
                i++;
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Enter the index of the friend you want to view");
            System.Console.WriteLine();
            System.Console.WriteLine("Type 'back' to go back");
            System.Console.WriteLine();

            string? input = Console.ReadLine();

            if (input?.ToLower() == "back")
            {
                stayInFriendsMenu = false; 
            }
            else if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int selectedIndex))
            {
                if (selectedIndex >= 0 && selectedIndex < friends.Count)
                {
                    Member selectedFriend = friends[selectedIndex];

                    Console.Clear();
                    System.Console.WriteLine("=== Friend's Information ===");
                    System.Console.WriteLine();
                    System.Console.WriteLine($"Username: {selectedFriend.Username}");
                    System.Console.WriteLine($"Firstname: {selectedFriend.Firstname}");
                    System.Console.WriteLine($"Lastname: {selectedFriend.Lastname}");
                    System.Console.WriteLine($"Email: {selectedFriend.Email}");
                    System.Console.WriteLine($"Birthday: {selectedFriend.Birthday}");
                    System.Console.WriteLine();
                    System.Console.WriteLine($"Number of posts: {selectedFriend.Account.GetPosts().Count}");

                    List<Member> friendsFriends = selectedFriend.Account.GetFriends();
                    if (friendsFriends.Count < 1)
                    {
                        System.Console.WriteLine("This member has no friends on here...");
                    }
                    else
                    {
                        System.Console.WriteLine("Friends: ");
                        foreach (var f in friendsFriends)
                        {
                            System.Console.WriteLine($" - {f.Username}");
                        }
                    }

                    System.Console.WriteLine();
                    new Services().PressKeyAndContinue(); 
                }
                else
                {
                    System.Console.WriteLine("Invalid index. Please try again.");
                    new Services().PressKeyAndContinue();
                }
            }
            else
            {
                System.Console.WriteLine("Invalid input. Please enter a valid index.");
                new Services().PressKeyAndContinue();
            }
        }
    }
}