using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;

namespace Codem.WebClient.Tests;

public class SelectLanguageComponentTests
{
    private readonly TestContext _ctx = new();

    public SelectLanguageComponentTests()
    {
        _ctx.JSInterop.SetupModule("./js/langDropdown.js");
        _ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        _ctx.JSInterop.SetupVoid("initLangDropdown").SetVoidResult();
    }
    
    [Fact]
    public void ShouldRenderCodeFileTabsComponent()
    {
        IRenderedComponent<SelectLanguage> cut = _ctx.RenderComponent<SelectLanguage>();
        IElement button = cut.Find("#langDropdownButton");
        Assert.NotNull(button);
        IRefreshableElementCollection<IElement> languages = cut.FindAll("li");
        Assert.Equal(53, languages.Count);
    }

    [Fact]
    public async Task ShouldFilterItemsAfterInputSearchString()
    {
        IRenderedComponent<SelectLanguage> cut = _ctx.RenderComponent<SelectLanguage>();
        IElement searchInput = cut.Find("input");
        ChangeEventArgs changeEventArgs = new ChangeEventArgs { Value = "JavaScript" };
        await searchInput.TriggerEventAsync("oninput", changeEventArgs);
        IRefreshableElementCollection<IElement> languages = cut.FindAll("li");
        Assert.Equal(1, languages.Count);
        Assert.Equal("JavaScript", languages.First().TextContent);
    }
    
    [Fact]
    public Task ShouldSelectLanguage()
    {
        IRenderedComponent<SelectLanguage> cut = _ctx.RenderComponent<SelectLanguage>();
        IRefreshableElementCollection<IElement> liElements = cut.FindAll("li");
        Assert.NotEmpty(liElements);
        IElement firstLi = liElements.First();
        IElement? button = firstLi.FirstElementChild;
        Assert.NotNull(button);
        button.Click();
        string expectedActiveLanguage = firstLi.TextContent.Trim();
        Assert.Equal(expectedActiveLanguage, cut.Instance.ActiveLanguage);
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task ShouldClearInputSearchString()
    {
        IRenderedComponent<SelectLanguage> cut = _ctx.RenderComponent<SelectLanguage>();
        IElement searchInput = cut.Find("input");
        ChangeEventArgs changeEventArgs = new ChangeEventArgs { Value = "JavaScript" };
        await searchInput.TriggerEventAsync("oninput", changeEventArgs);
        IElement clearSearchButton = cut.Find("button[type='button']");
        clearSearchButton.Click();
        Assert.Equal(string.Empty, cut.Instance.SearchString);
    }
}