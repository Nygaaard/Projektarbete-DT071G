using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
class Member
{
    private string? firstname;
    private string? lastname;
    private string? email;
    private DateOnly birthday;
    private string? username;
    private string? password;
    private Account? account;
    public Member() {}
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
    private bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
    public string Email
    {
        get => email!;
        set
        {
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
    public DateOnly Birthday
    {
        get => birthday;
        set
        {
            string format = "yyyy-MM-dd";

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
