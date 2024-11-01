/*
Class representing a single post
Written by Andreas Nygård
*/
class Post
{
    //Fields
    private string? message;
    //Constructors
    public Post() {}
    public Post(string m)
    {
        Message = m;
    }
    public string Message
    {
        get => message!;
        set 
        {
            //Validate if message is null or empty
            if(String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Message field can not be empty...");
            } else
            {
                message = value;
            }
        }
    }
}