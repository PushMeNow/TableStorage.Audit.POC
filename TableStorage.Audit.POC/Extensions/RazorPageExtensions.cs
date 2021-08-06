using Microsoft.AspNetCore.Mvc.Razor;

namespace TableStorage.Audit.POC.Extensions
{
    public static class RazorPageExtensions
    {
        public static void AddTitle<TModel>(this RazorPage<TModel> page, string title)
        {
            page.ViewData["Title"] = title;
        }
    }
}