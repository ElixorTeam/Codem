using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using SharedCore.Models;
using WebClient.Models;

namespace WebClient.Components;

public sealed partial class CreateSnippetForm : ComponentBase
{
    
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
    
    private string _activeLanguage = String.Empty;
    private SnippetModel Model { get; set; }
    private List<ValueTypeModel<TimeSpan>> ExpireTimeList { get; set; }
    
    public CreateSnippetForm()
    {
        Model = new();
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