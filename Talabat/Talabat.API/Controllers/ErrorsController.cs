using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    [Route("/error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase 
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code , "Endpoint is not NotFound"));
        }
    }
}
