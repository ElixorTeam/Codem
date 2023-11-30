using AngleSharp.Dom;
using Blazored.Toast;
using Bunit;
using WebClient.Components.CodeEditor;

namespace Codem.WebClient.Tests;

public class EditFileNameTests
{

    private readonly TestContext _ctx;
    
    public EditFileNameTests()
    {
        _ctx = new TestContext();
        _ctx.JSInterop.SetupModule("./Components/CodeEditor/EditFileNameModal.razor.js");
        _ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        _ctx.JSInterop.SetupVoid("initFileNameModal").SetVoidResult();
        _ctx.Services.AddBlazoredToast();
    }
    
    [Theory]
    [InlineData("newFileName", "newFileName.txt")]
    [InlineData("nameWithDot.", "nameWithDot.txt")]
    [InlineData("nameWithExt.py", "nameWithExt.py")]
    [InlineData("nameWith..dots.py", "nameWith..dots.py")]
    [InlineData(".hiddenfile", ".hiddenfile")]
    [InlineData("nameWithMultiple..Extensions.txt.py", "nameWithMultiple..Extensions.txt.py")]
    [InlineData("nameWithUnicodeÜmlauts.äöü", "nameWithUnicodeÜmlauts.äöü")]
    [InlineData("invalidFileName.@", "File name is incorrect")]
    [InlineData("invalidExt.@", "File name is incorrect")]
    [InlineData("Spaced Name", "File name is incorrect")]
    [InlineData("nameWithSpecial/", "File name is incorrect")]
    [InlineData("", "File name is incorrect")]
    [InlineData(" ", "File name is incorrect")]
    [InlineData(".", "File name is incorrect")]
    [InlineData("..", "File name is incorrect")]
    [InlineData("nameWith\\Slash", "File name is incorrect")]
    [InlineData("nameWith/Slash", "File name is incorrect")]
    [InlineData("nameWith:Colon", "File name is incorrect")]
    [InlineData("nameWith*Star", "File name is incorrect")]
    [InlineData("nameWith?Question", "File name is incorrect")]
    [InlineData("nameWith\"Quote", "File name is incorrect")]
    [InlineData("nameWith|Pipe", "File name is incorrect")]
    [InlineData("nameWith<Less", "File name is incorrect")]
    [InlineData("nameWith>More", "File name is incorrect")]
    [InlineData("VERYLONGFILENAMEWITHMORETHAN48CHARACTERS......................txt", "File name is incorrect")]
    public void TestChangeFileNameWithParams(string inputFileName, string expectedProcessedFileName)
    {
        CodeFileManager codeFileManager = new();
        IRenderedComponent<EditFileNameModal> cut = _ctx.RenderComponent<EditFileNameModal>(
            ("CodeFileManager", codeFileManager)
        );

        IElement inputFileNameElement = cut.Find("#fileName");
        IElement changeButton = cut.Find("button:contains('Change')");
        inputFileNameElement.Change(inputFileName);
        changeButton.Click();

        string actualFileName = codeFileManager.GetCurrentFile().Title;
        if (expectedProcessedFileName == "File name is incorrect")
            Assert.NotEqual(expectedProcessedFileName, actualFileName);
        else
            Assert.Equal(expectedProcessedFileName, actualFileName);
    }

}