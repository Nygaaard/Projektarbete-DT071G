class Post
{
    private string? message;
    public Post(string m)
    {
        Message = m;
    }
    public string Message
    {
        get => message!;
        set 
        {
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