using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DateTimeExtensions.Web.Pages;

using WorkingDays;

public class IndexModel : PageModel
{
    public HolidayObservance[] YearObservances { get; set; }
    public int StartYear { get; set; } = DateTime.Today.Year - 1;
    public int EndYear { get; set; } = DateTime.Today.Year + 3;
    public string Language { get; set; }

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
        Language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    }

    public void OnGet()
    {
        var year = DateTime.Today.Year;
        var ci = new WorkingDayCultureInfo();
        YearObservances = ci.GetHolidaysOfYear(year).Select(x => new HolidayObservance
        {
            Name = x.Name,
            Date = x.GetInstance(year)
        }).OrderBy(x => x.Date).ToArray();
    }

    public class HolidayObservance
    {
        public DateTime? Date { get; set; }
        public string Name { get; set; }
    }
}