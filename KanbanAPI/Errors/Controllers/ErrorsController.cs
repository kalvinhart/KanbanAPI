using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace KanbanAPI.Errors.Controllers;

[ApiController]
[Route("errors")]
public class ErrorsController : ControllerBase
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return exception != null
            ? Problem(title: exception.Message)
            : Problem();
    }
}