using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Components.Forms;

public sealed partial class PasswordForm : ComponentBase
{
    [Parameter, EditorRequired] public string CorrectPassword { get; set; } = string.Empty;
    [Parameter] public Action SuccessCallbackFunction { get; set; } = () => { };
    [Parameter] public Action CancelCallbackFunction { get; set; } = () => { };
    private string InputPassword { get; set; } = string.Empty;
    private bool IsErrorInput { get; set; } = false;

    private void HandlePasswordEnterByEnter(KeyboardEventArgs e)
    {
        if (e.Code is "Enter" or "NumpadEnter")
            HandlePasswordEnter();
    }

    private void HandlePasswordEnter()
    {
        if (InputPassword.Trim() != CorrectPassword)
        {
            IsErrorInput = true;
            return;
        }
        SuccessCallbackFunction.Invoke();
    }
}