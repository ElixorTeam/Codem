namespace Сodem.Shared.Models;

public class UserModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    
    public UserModel(string id, string name, string avatar)
    {
        Id = id;
        Name = name;
        Avatar = avatar;
    }
}