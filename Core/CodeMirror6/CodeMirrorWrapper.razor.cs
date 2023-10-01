using CodeMirror6.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CodeMirror6;

public partial class CodeMirrorWrapper : ComponentBase, IAsyncDisposable
{
    private int _prevTabSize;
    
    private bool _hasFocus;
    private bool _shouldRender = true;
    
    private string _text = string.Empty;
    private string _prevText = string.Empty;
    public readonly string Id = Guid.NewGuid().ToString();
    
    private CodeMirrorJsInterop? _jsInterop;
    
    [Inject] private IJSRuntime JSRuntime { get; set; }

    #region Parameters

    [Parameter] public int TabSize { get; set; } = 4;
    [Parameter] public string Text 
    {
        get => _text;
        set { _text = value.Replace("\r", ""); }
    }
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter] public EventCallback<string> TextChanged { get; set; }
    [Parameter] public EventCallback<bool> FocusChanged { get; set; }

    #endregion
    
    #region ComponentBase

    protected override void OnInitialized()
    {
        _prevTabSize = TabSize;
        _prevText = Text;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        if (_jsInterop == null)
        {
            _jsInterop = new(JSRuntime, this);
            await _jsInterop.InitCodeMirror();
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        _shouldRender = false;
        await UpdateTabForRender();
        await UpdateTextForRender();
    }
    
    protected override bool ShouldRender() => _shouldRender;

    #endregion

    #region Public

    [JSInvokable]
    public async Task OnJsTextChanged(string value)
    {
        if (Text.Replace("\r", "") == value.Replace("\r", "")) return;
        Text = value;
        _prevText = Text;
        await TextChanged.InvokeAsync(Text);
    }

    [JSInvokable]
    public async Task OnJsFocusChanged(bool value)
    {
        if (_hasFocus == value) return;
        _hasFocus = value;
        await FocusChanged.InvokeAsync(_hasFocus);
        await InvokeAsync(StateHasChanged);
    }
    
    [JSInvokable]
    public async Task OnReadOnlyChanged(bool value)
    {
        await FocusChanged.InvokeAsync(_hasFocus);
        await InvokeAsync(StateHasChanged);
    }

    #endregion
    
    #region Private

    private async Task UpdateTabForRender()
    {
        if (_prevTabSize == TabSize || _jsInterop == null) return;
        _prevTabSize = TabSize;
        await _jsInterop.SetTabSize();
        _shouldRender = true;
    }
    
    private async Task UpdateTextForRender()
    {
        if (_prevText.Replace("\r", "") == Text.Replace("\r", "") || _jsInterop == null)
            return;
        _prevText = Text;
        await _jsInterop.SetText();
        _shouldRender = true;
    }

    #endregion
    
    public async ValueTask DisposeAsync()
    {
        if (_jsInterop != null)
            await _jsInterop.DisposeAsync();
    }
}