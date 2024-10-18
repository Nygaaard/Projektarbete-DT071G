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
}
