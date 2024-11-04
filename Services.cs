/*
Class for services 
Written by Andreas Nygård
*/
using System.Text.Json;
using System.IO;

class Services
{
    //Method to display message and wait for user input before clearing
    public void PressKeyAndContinue()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
    //Method to save a list of members to a JSON file
    public void SaveMembers(List<Member> members)
    {
        string filePath = "members.json";
        string jsonData = JsonSerializer.Serialize(members, new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true // Viktigt för att inkludera alla fält, inklusive nested objects
        });
        File.WriteAllText(filePath, jsonData);
    }

    public List<Member> LoadMembers()
    {
        string filePath = "members.json";
        List<Member> members = new List<Member>();
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            members = JsonSerializer.Deserialize<List<Member>>(jsonData, new JsonSerializerOptions
            {
                IncludeFields = true
            }) ?? new List<Member>();
        }
        return members;
    }

    //Method to read a password from the console and disply asterisks
    public string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            //Read a key from the console without displaying
            key = Console.ReadKey(true);

            //Add key to password if not backspace or enter
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                //Display asterisks
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                //Remove the last character and asterisk from password and console
                password = password[0..^1];
                Console.Write("\b \b");
            }
        }
        //Wait for enter input
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }
}
