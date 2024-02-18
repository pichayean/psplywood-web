using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PSPlywoodWeb.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/404")]
        public IActionResult PageNotFound()
        {
            Response.Clear();
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("_PageNotFoundError");
        }

        [Route("error/handle-exception")]
        public IActionResult HandleException()
        {
            // log error
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (feature != null)
            {
                //_logger.LogException(feature.Error);
            }
            return View("_PagesError");
        }
    }
}
