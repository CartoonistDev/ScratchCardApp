using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("Error/{statusCode}")]
    public IActionResult HandleErrorCode(int statusCode)
    {
        var viewName = statusCode switch
        {
            404 => "NotFound",
            500 => "ServerError",
            _ => "Error"
        };

        return View(viewName);
    }
}

