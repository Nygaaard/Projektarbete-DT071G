using System.Text.Json;
using System.IO;

class Services
{
    public void PressKeyAndContinue()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public void SaveMembers(List<Member> members)
    {
        string filePath = "members.json";
        string jsonData = JsonSerializer.Serialize(members, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, jsonData);
    }

    public List<Member> LoadMembers()
    {
        string filePath = "members.json";
        List<Member> members;

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            members = JsonSerializer.Deserialize<List<Member>>(jsonData) ?? new List<Member>();
        }
        else
        {
            members = new List<Member>();
        }

        return members;
    }
    public string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true); 

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*"); 
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[0..^1]; 
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter); 

        Console.WriteLine(); 
        return password;
    }
    
}
