/*
Class representing an account
Written by Andreas Nygård
*/
class Account
{
    //Fields
    private List<Post> posts;
    private List<Member> friends;
    //Constructor
    public Account()
    {
        posts = new List<Post>();
        friends = new List<Member>();
    }
    public Account(List<Post> p, List<Member> m)
    {
        posts = p;
        friends = m;
    }
    //Get and set 
    public List<Post> Posts
    {
        get => posts;
        set => posts = value;
    }
    public List<Member> Friends
    {
        get => friends;
        set => friends = value;
    }
    //Method for adding post to list of posts
    public void AddPost(Post post)
    {
        posts.Add(post);
    }
    //Class for deleting post using index
    public void DeletePost(int index)
    {
        //Check if index exists
        if (index < 0 || index >= posts.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index not found...");
        }
        else
        {
            //If index match - remove at that index
            posts.RemoveAt(index);
        }
    }
    //Get all posts
    public List<Post> GetPosts()
    {
        return posts;
    }
    //Get all friends
    public List<Member> GetFriends()
    {
        return friends;
    }
}