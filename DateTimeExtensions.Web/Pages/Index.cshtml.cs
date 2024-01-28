using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DateTimeExtensions.Web.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    public IActionResult OnGet()
    {
        var year = DateTime.Today.Year;

        var userLanguages = Request.GetTypedHeaders()
            .AcceptLanguage
            .OrderByDescending(x => x.Quality ?? 1)
            .Select(x => x.Value.ToString());

        string? locale = null;
        foreach (var userLanguage in userLanguages)
        {
            if (userLanguage.Length == 2)
            {
                var foundMatch = AvailableLocales.List.Value.FirstOrDefault(x => x.Substring(0, 2) == userLanguage);
                if (foundMatch != null)
                {
                    locale = foundMatch;
                    break;
                }
            }

            if (AvailableLocales.List.Value.Contains(userLanguage))
            {
                locale = userLanguage;
                break;
            }
        }

        locale ??= "pt-PT";
        return RedirectToPage("Calendar", new { locale, year });
    }
}