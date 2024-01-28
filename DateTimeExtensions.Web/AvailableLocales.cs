namespace DateTimeExtensions.Web;

using System.Reflection;
using Common;
using WorkingDays;
using WorkingDays.RegionIdentifiers;

public static class AvailableLocales
{
    public static Lazy<HashSet<string>> List { get; } = new(() =>
        Assembly.GetAssembly(typeof(IWorkingDayCultureInfo))?
            .GetTypes()
            .Where(x => x.IsClass && x.IsAssignableTo(typeof(IHolidayStrategy)))
            .SelectMany(x => x.GetCustomAttributes<LocaleAttribute>().Select(y => y.Locale)).ToHashSet()
        ?? new HashSet<string>());

    public static IDictionary<string, HashSet<string>> Regions { get; } = new Dictionary<string, HashSet<string>>
    {
        { "pt-PT", new HashSet<string> { PortugalRegion.Lisboa, PortugalRegion.Porto, PortugalRegion.CasteloBranco } }
    };
}