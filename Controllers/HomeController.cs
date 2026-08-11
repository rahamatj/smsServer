using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace smsServer.Controllers;


[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Hello()
    {
        return Ok(new { Message = "Hello World!" });
    }
}