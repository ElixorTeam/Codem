using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.Enums;
using Codem.Domain.Exceptions;
using Codem.Domain.ValueTypes;
using Xunit.Abstractions;
namespace Codem.Domain.Tests.Models;

public class SnippetTests
{
    private readonly ITestOutputHelper _output;

    public SnippetTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void Password_is_set_when_private()
    {
        Snippet snippet = new();
        Password pwd = new("test");
        
        snippet.ChangeVisibility(SnippetVisibilityEnum.Private);
        snippet.ChangePassword(pwd);
        
        _output.WriteLine($"IsHidden: {snippet.Visibility}, Password: {pwd}");
        Assert.True(pwd.Equals(snippet.Password));
    }
    
    [Fact]
    public void Password_is_not_set_when_public()
    {
        Snippet snippet = new();
        
        snippet.ChangeVisibility(SnippetVisibilityEnum.Public);
        
        _output.WriteLine($"IsHidden: {snippet.Visibility}");
        Assert.Throws<PasswordCanNotBeSetException>(() => snippet.ChangePassword(new((string)"test")));
    }

    [Fact]
    public void Change_visibility_to_Public()
    {
        Snippet snippet = new();
    
        snippet.ChangeVisibility(SnippetVisibilityEnum.Public);
    
        _output.WriteLine($"IsPublic: {snippet.Visibility}");
        Assert.True(snippet.Visibility.Equals(SnippetVisibilityEnum.Public));
    }

    [Fact]
    public void Change_visibility_to_Private()
    {
        Snippet snippet = new();
    
        snippet.ChangeVisibility(SnippetVisibilityEnum.Private);
    
        _output.WriteLine($"IsPublic: {snippet.Visibility}");
        Assert.True(snippet.Visibility.Equals(SnippetVisibilityEnum.Private));
    }
    
    [Fact]
    public void Change_visibility_to_true_with_password()
    {
        Password password = new("test");
        Snippet snippet = new();
        
        snippet.ChangeVisibility(SnippetVisibilityEnum.Private);
        snippet.ChangePassword(password);
        snippet.ChangeVisibility(SnippetVisibilityEnum.Public);
    
        _output.WriteLine($"IsPublic: {snippet.Visibility}");
        Assert.Null(snippet.Password);
    }
}