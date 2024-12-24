using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Chat.Pages.Account
{
	/// <summary>
	/// The login model.
	/// </summary>
	[AllowAnonymous]
	public class LoginModel : PageModel
	{
		/// <summary>
		/// On get.
		/// </summary>
		public void OnGet()
		{
		}

		/// <summary>
		/// On post.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>An IActionResult</returns>
		public async Task<IActionResult> OnPostAsync(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				ModelState.AddModelError(string.Empty, "Username is required.");
				return Page();
			}

			var claims = new List<Claim>
			{
				new(ClaimTypes.Name, username)
			};

			// CookieAuthenticationDefaults.AuthenticationScheme
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				IsPersistent = true
			};

			// CookieAuthenticationDefaults.AuthenticationScheme
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

			return RedirectToPage("/Index");
		}
	}
}