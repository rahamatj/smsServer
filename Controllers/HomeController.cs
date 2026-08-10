using Microsoft.AspNetCore.Mvc;

namespace smsServer.Controllers;


[ApiController]
public class HomeController
{
    [HttpGet("")]
    public string hello()
    {
        return "Hello, World!";
    }
}