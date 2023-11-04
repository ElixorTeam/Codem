using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using SharedCore.Models;
using WebClient.Models;

namespace WebClient.Components;

public sealed partial class CreateSnippetForm : ComponentBase
{
    private string _activeLanguage = String.Empty;
    
    #region Parameters

    [Parameter] public string ActiveLanguage 
    { 
        get => _activeLanguage; 
        set
        {
            if (_activeLanguage == value)
                return;
            _activeLanguage = value;
            ActiveLanguageChanged.InvokeAsync(value);
        }
    }
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }

    #endregion
    private SnippetModel Model { get; set; }
    private List<string> Languages { get; set; }
    private List<ValueTypeModel<TimeSpan>> ExpireTimeList { get; set; }
    
    public CreateSnippetForm()
    {
        ActiveLanguage = string.Empty;
        Model = new();
        Languages = new() {
            "C", "C++", "CSS", "HTML", "Java", "JavaScript", "JSON", "JSX", "MariaDB SQL", "Markdown", "MS SQL",
            "MySQL", "PHP", "PostgreSQL", "Python", "Rust", "SQL", "SQLite", "TSX", "TypeScript", "XML", "C#", 
            "CMake", "Cython", "Dart", "Dockerfile", "Erlang", "Fortran", "F#", "Go", "Groovy", "Haskell", "HTTP", 
            "Jinja2", "Kotlin", "LESS", "Lua", "Nginx", "Objective-C", "Objective-C++", "Pascal", "Perl", "PowerShell", 
            "Ruby", "Sass", "SCSS", "Shell", "Swift", "sTeX", "LaTeX", "TOML", "VB.NET", "YAML",
        };
        Languages.Sort();
        ExpireTimeList = new() {
            new("Never", TimeSpan.FromDays(365)),
            new("1 hour", TimeSpan.FromHours(1)),
            new("1 day", TimeSpan.FromDays(1)),
            new("1 week", TimeSpan.FromDays(7)),
            new("1 month", TimeSpan.FromDays(31))
        };

        ActiveLanguage = "Markdown";
        Model.ExpireTime = ExpireTimeList[0].Value;
        Model.Title = string.Empty;
        Model.IsPrivate = false;
        Model.Password = string.Empty;
    }

    private void HandleSubmit()
    {
        DateTime finalDate = DateTime.Now.Add(Model.ExpireTime);
        Console.WriteLine($"Title: {Model.Title}");
        Console.WriteLine($"ActiveLanguage: {ActiveLanguage}");
        Console.WriteLine($"ExpireTime: {finalDate}");
        Console.WriteLine($"IsHidden: {Model.IsPrivate}");
        Console.WriteLine($"Password: {Model.Password}");
    }
}