using Microsoft.AspNetCore.Mvc.Filters;
using PSPlywoodWeb.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PSPlywoodWeb.Common
{
    public class CommonDataFilter : IActionFilter
    {
        private readonly IPSPlywoodService _dataService;

        public CommonDataFilter(IPSPlywoodService dataService)
        {
            _dataService = dataService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var commonData = _dataService.GetSettingsAsync();
            context.HttpContext.Items["CommonData"] = commonData;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Optional: Implement if needed
        }
    }
}
