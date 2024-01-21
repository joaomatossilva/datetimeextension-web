using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DateTimeExtensions.Web.Pages;

using WorkingDays;

public class IndexModel : PageModel
{
    public Dictionary<int, HolidayObservance[]> YearObservances { get; set; }
    public int StartYear { get; set; } = DateTime.Today.Year - 1;
    public int EndYear { get; set; } = DateTime.Today.Year + 3;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var ci = new WorkingDayCultureInfo("pt-PT");
        YearObservances = new Dictionary<int, HolidayObservance[]>();
        for (int year = StartYear; year < EndYear; year++)
        {
            var observances = ci.GetHolidaysOfYear(year).Select(x => new HolidayObservance
            {
                Name = x.Name,
                Date = x.GetInstance(year)
            }).ToArray();
            YearObservances[year] = observances;
        }
    }

    public class HolidayObservance
    {
        public DateTime? Date { get; set; }
        public string Name { get; set; }
    }
}