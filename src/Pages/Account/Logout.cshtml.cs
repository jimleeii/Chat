using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chat.Pages.Account
{
	/// <summary>
	/// The logout model.
	/// </summary>
	public class LogoutModel : PageModel
	{
		/// <summary>
		/// On get asynchronously.
		/// </summary>
		/// <returns>A Task of type IActionResult</returns>
		public async Task<IActionResult> OnGetAsync()
		{
			// Sign out from the application
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			// Sign out from Azure AD
			await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
			{
				RedirectUri = Url.Page("/Index")
			});

			return RedirectToPage("/Index");
		}
	}
}