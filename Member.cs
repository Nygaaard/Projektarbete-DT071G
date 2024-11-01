/*
Class representing a member
Written by Andreas Nygård
*/

using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
class Member
{
    //Fields
    private string? firstname;
    private string? lastname;
    private string? email;
    private DateOnly birthday;
    private string? username;
    private string? password;
    private Account? account;
    //Constructors
    //Without parameters
    public Member() {}
    //With parameters
    public Member(string first, string last, string e, DateOnly bd, string user, string pass, Account acc)
    {
        Firstname = first;
        Lastname = last;
        Email = e;
        Birthday = bd;
        Username = user;
        Password = pass;
        Account = acc;
    }
    //Get and set
    public string Firstname
    {
        get => firstname!;
        set => firstname = value;
    }
    public string Lastname
    {
        get => lastname!;
        set => lastname = value;
    }
    //Check if mail uses valid format using Regex
    private bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
    //
    public string Email
    {
        get => email!;
        set
        {
            //Call validation method here
            //If true - email = value
            if (IsValidEmail(value))
            {
                email = value;
            }
            else
            {
                throw new ArgumentException("Invalid email format.");
            }
        }
    }
    //Validate date using standard format
    public DateOnly Birthday
    {
        get => birthday;
        set
        {
            string format = "yyyy-MM-dd";
            //Check if format is correct 
            if (DateOnly.TryParseExact(value.ToString(), format, null, System.Globalization.DateTimeStyles.None, out DateOnly parsedDate))
            {
                birthday = parsedDate;
            }
            else
            {
                throw new ArgumentException("Invalid date format. Please use yyyy-mm-dd.");
            }
        }
    }
    public string Username
    {
        get => username!;
        set => username = value;
    }
    public string Password
    {
        get => password!;
        set => password = value;
    }
    public Account Account
    {
        get => account!;
        set => account = value;
    }
}
