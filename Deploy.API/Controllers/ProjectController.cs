using Deploy.Application.Services;
using Deploy.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Deploy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(ProjectService projectService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Project>>> GetAll()
    {
        var projects = await projectService.GetAll();
        return Ok(projects);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Project project)
    {
        await projectService.Create(project);
        return NoContent();
    }
}