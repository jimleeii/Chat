using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chat.Pages
{
	/// <summary>
	/// The index model.
	/// </summary>
	public class IndexModel : PageModel
	{
		/// <summary>
		/// On get.
		/// </summary>
		public void OnGet()
		{
			if (HttpContext.User.Identity!.IsAuthenticated)
			{
				// User is authenticated
			}
			else
			{
				// User is not authenticated
			}
		}
	}
}