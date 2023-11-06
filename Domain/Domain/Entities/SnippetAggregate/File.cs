using Domain.Common;
namespace Domain.Entities.SnippetAggregate;

public class File : IEntity
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Data { get; private set; }

    public File()
    {
        Name = string.Empty;    
        Data = string.Empty;
    }
    
    public void Rename(string name)
    {
        Name = name;
    }
    
    public File Copy()
    {
        throw new NotImplementedException();
    }
}