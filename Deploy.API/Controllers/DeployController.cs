using Microsoft.AspNetCore.Mvc;

namespace Deploy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeployController : ControllerBase
{
    [HttpPost("{applicationId}")]
    public ActionResult Deploy([FromRoute] string applicationId)
    {
        return Ok();
    } 
}