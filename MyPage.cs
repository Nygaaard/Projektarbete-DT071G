class MyPage
{
    public void MyPageMenu(Member matchedMember)
    {
        Console.Clear();
        System.Console.WriteLine("=== My page ===");
        System.Console.WriteLine($"Hello {matchedMember.Firstname}");



        new Services().PressKeyAndContinue();
    }
}