using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        public async Task OnGet()
        {
            AuthenticationProperties authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                 .WithRedirectUri("/logout")
                 .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

