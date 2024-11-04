/*
Class representing users page
Written by Andreas Nygård
*/
class MyPage
{
    //Method for users my page menu
    //Matched member from login method in platform class
    public void MyPageMenu(Member matchedMember)
    {
        bool isRunning = true;

        //While loop for dynamic user exeperience 
        while (isRunning)
        {
            //Menu options 
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

            //Different options depending on user input
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
                    AccountInformation(matchedMember);
                    break;
                //If 0 while loop ends - ending program
                case "0":
                    isRunning = false;
                    Console.Clear();
                    System.Console.WriteLine("Go back...");
                    break;
                //If input is anything but above options
                default:
                    Console.Clear();
                    System.Console.WriteLine("Invalid input...");
                    System.Console.WriteLine();
                    break;
            }
        }
    }
    //Method for adding a post
    public void AddPost(Member matchedMember) //Using logged in user
    {
        //First - get all posts from account class
        List<Post> posts = matchedMember.Account.GetPosts();
        Console.Clear();
        //Create menu options
        System.Console.WriteLine("=== Add a post ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Write your message below.");
        System.Console.WriteLine();

        System.Console.Write("Message: ");
        string? message = Console.ReadLine();
        //Validate message
        if (String.IsNullOrEmpty(message))
        {
            System.Console.WriteLine();
            System.Console.WriteLine("You must write something...");
            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
        else
        {
            //If valid - create new post and call add method 
            Post newPost = new Post(message);
            posts.Add(newPost);
        }
        System.Console.WriteLine();
        System.Console.WriteLine("Post added successfully!");
        new Services().PressKeyAndContinue();
    }
    //Method for deleting posts
    public void DeletePost(Member matchedMember) // Using logged in user 
    {
        //Get all posts from that specific user
        List<Post> posts = matchedMember.Account.GetPosts();

        //Create menu options
        Console.Clear();
        System.Console.WriteLine("=== Delete a post ===");
        System.Console.WriteLine();
        System.Console.WriteLine("Choose the index of the post you want to delete.");
        System.Console.WriteLine();

        System.Console.Write("Index: ");
        System.Console.WriteLine();

        int i = 0;

        //Read all posts
        //Add index in front of message
        foreach (var post in posts)
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"[{i}] - {post.Message}");
            i++;
        }

        System.Console.WriteLine();
        System.Console.Write("Index: ");
        string? userInput = Console.ReadLine();

        //Validate user input 
        //Make sure input is a valid number
        if (!String.IsNullOrEmpty(userInput) && int.TryParse(userInput, out int indexNumber))
        {
            if (indexNumber >= 0 && indexNumber < posts.Count)
            {
                //If valid - remove post on given index
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
    //Method for reading posts
    public void ReadAllPosts(Member matchedMember)
    {
        Console.Clear();
        System.Console.WriteLine("=== All your posts ===");
        System.Console.WriteLine();

        //Get posts from that user
        List<Post> posts = matchedMember.Account.GetPosts();
        int i = 0;

        //Check if posts > 0
        if (posts.Count < 1)
        {
            System.Console.WriteLine("You have no current posts...");
        }
        else
        {
            //Write all posts
            //Add index number on front
            foreach (var post in posts)
            {
                System.Console.WriteLine($"[{i}] - {post.Message}");
                System.Console.WriteLine();
                i++;
            }
        }

        new Services().PressKeyAndContinue();
    }
    //Method for searching other users on platform 
    public void SearchFriends(Member matchedMember) // Logged in as matched user
    {
        //Create object of platform class
        Platform platform = new Platform();
        //Get all members on the platform
        List<Member> members = platform.GetAllMembers();

        //Check if members list is empty
        if (members == null || members.Count == 0)
        {
            Console.Clear();
            //Return if empty
            System.Console.WriteLine("No members found. Please make sure the platform has members loaded.");
            new Services().PressKeyAndContinue();
            return;
        }

        Console.Clear();
        System.Console.WriteLine("=== Search and follow friends ===");
        System.Console.WriteLine();

        //Filter out the current user from members list
        List<Member> filteredMembers = members.Where(m => m.Username != matchedMember.Username).ToList();

        //Check if there are any other members to follow
        if (filteredMembers.Count == 0)
        {
            System.Console.WriteLine("There are no other members to follow.");
            new Services().PressKeyAndContinue();
            return;
        }

        //Display list of other members
        int index = 0;
        foreach (var member in filteredMembers)
        {
            System.Console.WriteLine($"[{index}] {member.Username} - {member.Firstname} {member.Lastname}");
            index++;
        }

        System.Console.WriteLine();
        System.Console.Write("Enter the index of the user you want to follow: ");
        string? input = Console.ReadLine();

        //Validate user input 
        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int selectedIndex))
        {   
            //Check if selected index is valid 
            if (selectedIndex >= 0 && selectedIndex < filteredMembers.Count)
            {
                Member selectedMember = filteredMembers[selectedIndex];

                //Check if current user is already following selected member
                if (matchedMember.Account.GetFriends().Any(f => f.Username == selectedMember.Username))
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("You are already following this user.");
                }
                else
                {
                    //Add selected member to current users friends list 
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
    //Method for displaying logged in users friends
    public void MyFriends(Member matchedMember)
    {
        bool stayInFriendsMenu = true;
        //While loop for dynamic user experience
        while (stayInFriendsMenu)
        {
            Console.Clear();
            System.Console.WriteLine("=== My friends ===");
            System.Console.WriteLine();
            System.Console.WriteLine("Here is a list of all your friends:");
            System.Console.WriteLine();

            //Get users friends
            List<Member> friends = matchedMember.Account.GetFriends();

            //Check if friends list is empty
            if (friends.Count == 0)
            {
                System.Console.WriteLine("You don't have any friends yet.");
                new Services().PressKeyAndContinue();
                return;
            }

            int i = 0;
            //Display friends from friends list
            foreach (var friend in friends)
            {
                System.Console.WriteLine($"[{i}] - {friend.Username}");
                i++;
            }
            //Option to enter index of friend
            System.Console.WriteLine();
            System.Console.WriteLine("Enter the index of the friend you want to view");
            System.Console.WriteLine();
            System.Console.WriteLine("Type 'back' to go back");
            System.Console.WriteLine();

            string? input = Console.ReadLine();

            //If input = "back" - break while loop
            if (input?.ToLower() == "back")
            {
                stayInFriendsMenu = false;
            }
            else if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int selectedIndex))
            {
                //If user input is within range of friends index
                if (selectedIndex >= 0 && selectedIndex < friends.Count)
                {   
                    
                    Member selectedFriend = friends[selectedIndex];
                    
                    //Get and display friends information from registration
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
                    
                    //Check if user has any friends on platform
                    //Get users friends 
                    List<Member> friendsFriends = selectedFriend.Account.GetFriends();
                    if (friendsFriends.Count < 1)
                    {
                        System.Console.WriteLine("This member has no friends on here...");
                    }
                    else
                    {
                        //Display friends if list is not empty
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
    //Method for displaying account information for logged in user
    public void AccountInformation(Member matchedMember)
    {
        Console.Clear();
        System.Console.WriteLine("=== Account information ===");
        System.Console.WriteLine();

        System.Console.WriteLine($"Username: {matchedMember.Username}");
        System.Console.WriteLine($"Firstname: {matchedMember.Firstname}");
        System.Console.WriteLine($"Lastname: {matchedMember.Lastname}");
        System.Console.WriteLine($"Email: {matchedMember.Email}");
        System.Console.WriteLine($"Birthday: {matchedMember.Birthday}");

        //Using Count method to display amount of posts
        System.Console.WriteLine($"Amount of posts: {matchedMember.Account.GetPosts().Count}");
        //Display friends if list is not empty
        List<Member> friendsFriends = matchedMember.Account.GetFriends();
        if (friendsFriends.Count < 1)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("You have no friends on here...");
        }
        else
        {
            System.Console.WriteLine("Friends: ");
            foreach (var f in friendsFriends)
            {
                System.Console.WriteLine($" - {f.Username}");
            }
        }

        new Services().PressKeyAndContinue();
    }
}