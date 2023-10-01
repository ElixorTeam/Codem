using Microsoft.JSInterop;

namespace CodeMirror6;

/// <summary>
/// Wraps JavaScript functionality in a .NET class for easy consumption.
/// </summary>
public class CodeMirrorJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private DotNetObjectReference<CodeMirrorWrapper> _dotnetHelperRef;
    private CodeMirrorWrapper _codeMirror;
    
    public CodeMirrorJsInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/CodeMirror6/index.js").AsTask());
    }
    
    public async Task InitCodeMirror(CodeMirrorWrapper codeMirror)
    {
        _codeMirror = codeMirror;
        _dotnetHelperRef = DotNetObjectReference.Create(_codeMirror);
        IJSObjectReference module = await _moduleTask.Value;
        await module.InvokeVoidAsync(
            "initCodeMirror",
            _dotnetHelperRef,
            _codeMirror.Id,
            _codeMirror.Text,
            _codeMirror.ReadOnly,
            _codeMirror.TabSize,
            "Python"
        );
    }
    
    public async Task SetTabSize()
    {
        IJSObjectReference module = await _moduleTask.Value;
        await module.InvokeVoidAsync("setTabSize", _codeMirror.Id, _codeMirror.TabSize);
    }
    
    public async Task SetLanguage()
    {
        IJSObjectReference module = await _moduleTask.Value;
        await module.InvokeVoidAsync("setLanguage", _codeMirror.Id, _codeMirror.Language);
    }

    public async Task SetText()
    {
        IJSObjectReference module = await _moduleTask.Value;
        await module.InvokeVoidAsync(
            "setText",
            _codeMirror.Id,
            _codeMirror.Text
        );
    }
    
    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_moduleTask.IsValueCreated)
            {
                IJSObjectReference module = await _moduleTask.Value;
                await module.InvokeVoidAsync("dispose");
                await module.DisposeAsync();
            }
        }
        catch (JSDisconnectedException e)
        {
            // ignore 
        }
    }
}
