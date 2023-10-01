using CodeMirror6.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CodeMirror6;

public partial class CodeMirrorWrapper : ComponentBase
{
    private int _prevTabSize;
    
    private bool _hasFocus;
    private bool _shouldRender = true;
    
    private string _text;
    private string _prevText;
    private string _prevLanguage;

    #region Inject

    [Inject] private CodeMirrorJsInterop JsInterop { get; set; }

    #endregion

    #region Parameters
    
    public readonly string Id;
    public string Text 
    {
        get => _text;
        set { _text = value.Replace("\r", ""); }
    }
    [Parameter] public int TabSize { get; set; }
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter] public string Language { get; set; }
    [Parameter] public EventCallback<string> TextChanged { get; set; }
    [Parameter] public EventCallback<bool> FocusChanged { get; set; }

    #endregion
    
    #region ComponentBase

    public CodeMirrorWrapper()
    {
        TabSize = 4;
        Id = Guid.NewGuid().ToString();
        Text = string.Empty;
    }
    
    protected override void OnInitialized()
    {
        _prevLanguage = Language;
        _prevTabSize = TabSize;
        _prevText = Text;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await JsInterop.InitCodeMirror(this);
    }

    protected override async Task OnParametersSetAsync()
    {
        _shouldRender = false;
        await UpdateLanguageForRender();
        await UpdateTabForRender();
        await UpdateTextForRender();
    }
    
    protected override bool ShouldRender() => _shouldRender;

    #endregion

    #region Public JSInvokable

    [JSInvokable]
    public async Task OnJsTextChanged(string value)
    {
        if (Text == value.Replace("\r", "")) return;
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
    
    private async Task UpdateLanguageForRender()
    {
        if (Language == _prevLanguage) return;
        _prevLanguage = Language;
        await JsInterop.SetLanguage();
        _shouldRender = true;
    }
    
    private async Task UpdateTabForRender()
    {
        if (_prevTabSize == TabSize) return;
        _prevTabSize = TabSize;
        await JsInterop.SetTabSize();
        _shouldRender = true;
    }
    
    private async Task UpdateTextForRender()
    {
        if (_prevText.Replace("\r", "") == Text.Replace("\r", ""))
            return;
        _prevText = Text;
        await JsInterop.SetText();
        _shouldRender = true;
    }

    #endregion
}