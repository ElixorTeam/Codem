using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using SharedCore.Models;
using WebClient.Models;

namespace WebClient.Components;

public sealed partial class CreateSnippetForm : ComponentBase
{
    
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }
    private SnippetModel Model { get; set; }
    private List<ValueTypeModel<TimeSpan>> ExpireTimeList { get; set; }
    
    public CreateSnippetForm()
    {
        ExpireTimeList = new() {
            new("Never", TimeSpan.FromDays(365)),
            new("1 hour", TimeSpan.FromHours(1)),
            new("1 day", TimeSpan.FromDays(1)),
            new("1 week", TimeSpan.FromDays(7)),
            new("1 month", TimeSpan.FromDays(31))
        };
        
        Model = new SnippetModel
        {
            ExpireTime = ExpireTimeList[0].Value,
            Title = string.Empty,
            IsPrivate = false,
            Password = string.Empty
        };
    }

    private void HandleSubmit()
    {
        DateTime finalDate = DateTime.Now.Add(Model.ExpireTime);
        Console.WriteLine($"Title: {Model.Title}");
        Console.WriteLine($"ExpireTime: {finalDate}");
        Console.WriteLine($"IsHidden: {Model.IsPrivate}");
        Console.WriteLine($"Password: {Model.Password}");
    }
}