using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WebClient.Components.CodeEditor;

namespace WebClientsTests;

public class SelectLanguageTest
{
    private TestContext ctx = new();

    public SelectLanguageTest()
    {
        ctx.JSInterop.SetupModule("./js/langDropdown.js");
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        ctx.JSInterop.SetupVoid("initLangDropdown").SetVoidResult();
    }
    
    [Fact]
    public void SelectLanguageRendersCorrectly()
    {
        IRenderedComponent<SelectLanguage> cut = ctx.RenderComponent<SelectLanguage>();
        IElement button = cut.Find("#langDropdownButton");
        Assert.NotNull(button);
        IRefreshableElementCollection<IElement> languages = cut.FindAll("li");
        Assert.Equal(53, languages.Count);
    }

    [Fact]
    public async Task SelectLanguageFiltersItems()
    {
        IRenderedComponent<SelectLanguage> cut = ctx.RenderComponent<SelectLanguage>();
        IElement searchInput = cut.Find("input");
        ChangeEventArgs changeEventArgs = new ChangeEventArgs { Value = "JavaScript" };
        await searchInput.TriggerEventAsync("oninput", changeEventArgs);
        IRefreshableElementCollection<IElement> languages = cut.FindAll("li");
        Assert.NotEmpty(languages);
        Assert.Equal("JavaScript", languages.First().TextContent);
    }
    
    [Fact]
    public async Task SelectLanguageChooseLanguage()
    {
        IRenderedComponent<SelectLanguage> cut = ctx.RenderComponent<SelectLanguage>();
        IRefreshableElementCollection<IElement> liElements = cut.FindAll("li");
        Assert.NotEmpty(liElements);
        IElement firstLi = liElements.First();
        IElement? button = firstLi.FirstElementChild;
        Assert.NotNull(button);
        MouseEventArgs clickEvent = new MouseEventArgs { Button = 0 };
        await button.TriggerEventAsync("onclick", clickEvent);
        string expectedActiveLanguage = firstLi.TextContent.Trim();
        Assert.Equal(expectedActiveLanguage, cut.Instance.ActiveLanguage);
    }
}