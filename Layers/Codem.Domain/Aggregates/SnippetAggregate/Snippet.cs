using Codem.Domain.Common;
using Codem.Domain.Exceptions;
using Codem.Domain.ValueTypes;
using Сodem.Shared.Enums;

namespace Codem.Domain.Aggregates.SnippetAggregate;

public class Snippet : IEntity
{
    #region Private

    private SnippetVisibilityEnum _visibility;
    private Password? _password;

    #endregion

    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public string Title { get; set; }
    public Password? Password
    {
        get => _password;
        set { ChangePassword(value); }
    }
    public SnippetVisibilityEnum Visibility
    {
        get => _visibility;
        set { ChangeVisibility(value); }
    }
    public IEnumerable<SnippetFile> Files { get; private set; }
    
    public Snippet()
    {
        _visibility = SnippetVisibilityEnum.Public;
        _password = null;
        Title = string.Empty;
        Files = new List<SnippetFile>();
    }
    
    public void AddFile(SnippetFile snippetFile)
    {
        if (Files.Any(f => f.Id == snippetFile.Id))
        {
            throw new FileAlreadyExistsException($"File with name '{snippetFile.Name}' already exists.");
        }
        Files = Files.Append(snippetFile).ToList();
    }
    
    public void ChangeVisibility(SnippetVisibilityEnum visibility)
    {
        _visibility = visibility;
        if (_visibility != SnippetVisibilityEnum.ByLink)
            Password = null;
    }

    public void ChangePassword(Password? password)
    {
        if (_visibility == SnippetVisibilityEnum.ByLink)
        {
            _password = password;
            return;
        }
        _password = null;
    }
}