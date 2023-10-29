using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.ValueTypes;
namespace Domain.Models;

public class Snippet : IEntity
{
    public Guid Id { get; }
    
    public string Name { get; set; }
    public Password Password { get; private set; }
    public bool IsPrivate { get; private set; }

    public void ChangePrivate(bool status)
    {
        IsPrivate = status;
        if (IsPrivate == false)
            Password = new(string.Empty);
    }
    
    public void ChangePassword(string password)
    {
        if (IsPrivate == false)
            throw new ValidationException("Password can't be set");
        Password = new(password);
    }

    public Snippet Copy()
    {
        throw new NotImplementedException();
    }
    
}
