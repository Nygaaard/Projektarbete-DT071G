class Account
{
    private List<Post> posts;
    private List<Member> friends;
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
    public void AddPost(Post post)
    {
        posts.Add(post);
    }
    public void DeletePost(int index)
    {
        if (index < 0 || index >= posts.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index not found...");
        }
        else
        {
            posts.RemoveAt(index);
        }
    }
    public List<Post> GetPosts()
    {
        return posts;
    }
    public List<Member> GetFriends()
    {
        return friends;
    }
}