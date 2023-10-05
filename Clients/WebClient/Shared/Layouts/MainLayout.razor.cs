using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Shared.Layouts;

public partial class MainLayout : LayoutComponentBase
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JSRuntime.InvokeVoidAsync("initFlowbite");
    }
}

