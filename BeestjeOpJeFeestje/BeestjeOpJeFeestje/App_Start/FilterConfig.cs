using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje
{
    [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}