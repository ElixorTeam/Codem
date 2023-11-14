using Codem.Domain.Common;
using Codem.Domain.Enums;
using Codem.Domain.Exceptions;
using Codem.Domain.ValueTypes;

namespace Codem.Domain.Aggregates.SnippetAggregate;

public class Snippet : IEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Password? Password { get; private set; }
    public SnippetVisibilityEnum Visibility { get; private set; }
    public IEnumerable<File> Files { get; private set; }
    
    public Snippet()
    {
        Title = string.Empty;
        Visibility = SnippetVisibilityEnum.Public;
        Files = new List<File>();
    }

    public void AddFile(File file)
    {
        if (Files.Any(f => f.Id == file.Id))
        {
            throw new FileAlreadyExistsException($"File with name '{file.Name}' already exists.");
        }
        Files = Files.Append(file).ToList();
    }
    
    public void DeleteFile(File file)
    {
        Files = Files.Where(f => f.Name != file.Name).ToList();
    }
    
    public void ChangePassword(Password password)
    {
        if (Visibility is SnippetVisibilityEnum.Public)
            throw new PasswordCanNotBeSetException("Password can't be set, snippet is public");
        Password = password;
    }
    
    public void ChangeVisibility(SnippetVisibilityEnum visibility)
    {
        Visibility = visibility;
        if (Visibility is SnippetVisibilityEnum.Public) 
            Password = null;
    }

    public Snippet Copy()
    {
        throw new NotImplementedException();
    }
}