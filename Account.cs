class Account
{
    private List<Post> posts;
    private List<Member> members;
    public Account(List<Post> p, List<Member> m)
    {
        posts = p;
        members = m;
    }
    public List<Post> Posts
    {
        get => posts;
        set => posts = value;
    }
    public List<Member> Members
    {
        get => members;
        set => members = value;
    }
    public void AddPost(Post post)
    {
        posts.Add(post);
    }
    public void DeletePost(int index)
    {
        if(index < 0 || index >= posts.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index not found...");
        } else
        {
            posts.RemoveAt(index);
        }
    }
    public List<Post> GetPosts()
    {
        return posts;
    }
    public List<Member> GetMembers()
    {
        return members;
    }
}