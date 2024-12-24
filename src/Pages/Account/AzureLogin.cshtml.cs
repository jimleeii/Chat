using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chat.Pages.Account
{
	/// <summary>
	/// The azure login model.
	/// </summary>
	public class AzureLoginModel : PageModel
	{
		/// <summary>
		/// On get.
		/// </summary>
		/// <returns>An IActionResult</returns>
		public IActionResult OnGet()
		{
			var redirectUrl = Url.Page("/Index");
			var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
			return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
		}
	}
}