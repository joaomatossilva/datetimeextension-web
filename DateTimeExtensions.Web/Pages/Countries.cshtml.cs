using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DateTimeExtensions.Web.Pages;

using System.Globalization;

public class Countries : PageModel
{
    public IEnumerable<Locale> Locales { get; set; }
    public int? Year { get; set; }

    public void OnGet(int? year)
    {
        Year = year;
        Locales = AvailableLocales.List.Value.Select(x =>
            {
                var regionInfo = new RegionInfo(x);
                return new Locale
                {
                    Identifier = x,
                    Name = regionInfo.DisplayName,
                    Country = regionInfo.TwoLetterISORegionName
                };
            })
            .OrderBy(x => x.Name);
    }

    public struct Locale
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}