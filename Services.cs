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
        //Define file path
        string filePath = "members.json";
        string jsonData = JsonSerializer.Serialize(members, new JsonSerializerOptions
        {
            //Format JSON with indentation for readability
            WriteIndented = true
        });
        //Write JSON data to the file
        File.WriteAllText(filePath, jsonData);
    }
    //Method to load a list of members from a JSON file
    public List<Member> LoadMembers()
    {
        //File path
        string filePath = "members.json";
        List<Member> members;
        //Check if file exists
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            //Deserialize JSON data to a list of members
            members = JsonSerializer.Deserialize<List<Member>>(jsonData) ?? new List<Member>();
        }
        else
        {
            //Initialize an empty list if the file doesnt exist
            members = new List<Member>();
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
