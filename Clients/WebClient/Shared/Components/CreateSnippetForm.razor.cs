using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Components;

public class SnippetModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    
    [Required]
    public TimeSpan ExpireTime { get; set; }

    public bool IsPrivate { get; set; }
    
    [StringLength(32)]
    public string Password { get; set; }
    
}

public class ExpireTimeItem
{
    public string Text { get; set; }
    public TimeSpan TimeValue { get; set; }

    public ExpireTimeItem(string inputText, TimeSpan timeValueInput)
    {
        Text = inputText;
        TimeValue = timeValueInput;
    }
}

public partial class CreateSnippetForm : ComponentBase
{
    private string _activeLanguage;
    
    [Parameter]
    public string ActiveLanguage 
    { 
        get => _activeLanguage; 
        set
        {
            if (_activeLanguage != value)
            {
                _activeLanguage = value;
                ActiveLanguageChanged.InvokeAsync(value);
            }
        }
    }
    [Parameter] 
    public EventCallback<string> ActiveLanguageChanged { get; set; }
    
    private SnippetModel Model { get; set; }
    private List<string> Languages { get; set; }
    private List<ExpireTimeItem> ExpireTimeList { get; set; }
    

    public CreateSnippetForm()
    {
        Model = new SnippetModel();
        Languages = new() {
            "C", "C++", "CSS", "HTML", "Java", "JavaScript", "JSON", "JSX", "MariaDB SQL", "Markdown", "MS SQL",
            "MySQL", "PHP", "PostgreSQL", "Python", "Rust", "SQL", "SQLite", "TSX", "TypeScript", "XML", "C#", 
            "CMake", "Cython", "Dart", "Dockerfile", "Erlang", "Fortran", "F#", "Go", "Groovy", "Haskell", "HTTP", 
            "Jinja2", "Kotlin", "LESS", "Lua", "Nginx", "Objective-C", "Objective-C++", "Pascal", "Perl", "PowerShell", 
            "Ruby", "Sass", "SCSS", "Shell", "Swift", "sTeX", "LaTeX", "TOML", "VB.NET", "YAML",
        };
        Languages.Sort();
        ExpireTimeList = new List<ExpireTimeItem>() {
            new ExpireTimeItem("Never", TimeSpan.FromDays(365)),
            new ExpireTimeItem("1 hour", TimeSpan.FromHours(1)),
            new ExpireTimeItem("1 day", TimeSpan.FromDays(1)),
            new ExpireTimeItem("1 week", TimeSpan.FromDays(7)),
            new ExpireTimeItem("1 month", TimeSpan.FromDays(31))
        };

        ActiveLanguage = "Markdown";
        Model.ExpireTime = ExpireTimeList[0].TimeValue;
        Model.Title = String.Empty;
        Model.IsPrivate = false;
        Model.Password = String.Empty;
    }

    private void HandleSubmit()
    {
        DateTime Date = DateTime.Now;
        DateTime FinalDate = Date.Add(Model.ExpireTime);
        Console.WriteLine($"Title: {Model.Title}");
        Console.WriteLine($"ActiveLanguage: {ActiveLanguage}");
        Console.WriteLine($"ExpireTime: {FinalDate}");
        Console.WriteLine($"IsPrivate: {Model.IsPrivate}");
        Console.WriteLine($"Password: {Model.Password}");
    }
}