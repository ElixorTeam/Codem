using AngleSharp.Dom;
using Blazored.Toast;
using Bunit;
using WebClient.Components.CodeEditor;

namespace Codem.WebClient.Tests;

public class CodeFileTabsComponentTests
{
    private readonly TestContext _ctx;
    
    public CodeFileTabsComponentTests()
    {
        _ctx = new TestContext();
        _ctx.Services.AddBlazoredToast();
    }
    
    [Fact]
    public void ShouldRenderCodeFileTabsComponent()
    {
        IRenderedComponent<CodeFileTabs> cut = _ctx.RenderComponent<CodeFileTabs>(
            ("CodeFileManager", new CodeFileManager()),
            ("IsReadOnly", false)
        );
        
        IRefreshableElementCollection<IElement> fileTabs = cut.FindAll(".group\\/tab");
        Assert.Single(fileTabs);
    }
    
    [Fact]
    public void ShouldAddNewFileOnClick()
    {
        CodeFileManager codeFileManager = new();
        IRenderedComponent<CodeFileTabs> cut = _ctx.RenderComponent<CodeFileTabs>(
            ("CodeFileManager", codeFileManager),
            ("IsReadOnly", false)
        );
        
        Guid firstFileId = codeFileManager.GetCurrentFile().Id;
        IElement addFileButton = cut.Find("li:last-child button");
        addFileButton.Click();
        Assert.NotEqual(firstFileId, codeFileManager.GetCurrentFile().Id);
    }
    
    [Fact]
    public void ShouldSwitchFileOnClick()
    {
        CodeFileManager codeFileManager = new();
        Guid firstFileId = codeFileManager.GetCurrentFile().Id;
        codeFileManager.AddFile();
        IRenderedComponent<CodeFileTabs> cut = _ctx.RenderComponent<CodeFileTabs>(
            ("CodeFileManager", codeFileManager),
            ("IsReadOnly", false)
        );
        
        IElement addFileButton = cut.Find("li:first-child button");
        addFileButton.Click();
        Assert.Equal(firstFileId, codeFileManager.GetCurrentFile().Id);
    }
    
    [Fact]
    public void ShouldDeleteFileOnClick()
    {
        CodeFileManager codeFileManager = new();
        codeFileManager.AddFile();
        IRenderedComponent<CodeFileTabs> cut = _ctx.RenderComponent<CodeFileTabs>(
            ("CodeFileManager", codeFileManager),
            ("IsReadOnly", false)
        );

        IElement deleteButton = cut.Find("li:nth-child(2) .group\\/del button");
        deleteButton.Click();
        IRefreshableElementCollection<IElement> fileTabs = cut.FindAll(".group\\/tab");
        Assert.Single(fileTabs);
    }
}