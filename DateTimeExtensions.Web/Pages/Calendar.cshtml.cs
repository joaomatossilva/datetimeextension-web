using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DateTimeExtensions.Web.Pages;

using System.Globalization;
using WorkingDays;

public class CalendarModel : PageModel
{
    public string CountryName { get; set; }
    public string Locale { get; set; }
    public HashSet<string> Regions { get; set; }
    public string Region { get; set; }
    public int Year { get; set; }

    public HolidayObservance[] YearObservances { get; set; }
    public string Language { get; set; }
    public string Country { get; set; }

    private readonly ILogger<IndexModel> _logger;

    public CalendarModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet(string locale, int year, string? region)
    {
        Year = year;

        var regionInfo = new RegionInfo(locale);
        CountryName = regionInfo.DisplayName;
        Country = regionInfo.TwoLetterISORegionName;
        Locale = locale;

        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(locale);
        Language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        Regions = AvailableLocales.Regions.TryGetValue(locale, out var regions)
            ? regions
            : new HashSet<string>();

        var ci = new WorkingDayCultureInfo();
        YearObservances = ci.GetHolidaysOfYear(year).Select(x => new HolidayObservance
        {
            Name = x.Name,
            Date = x.GetInstance(year)
        }).OrderBy(x => x.Date).ToArray();

        return Page();
    }

    public class HolidayObservance
    {
        public DateTime? Date { get; set; }
        public string Name { get; set; }
    }
}