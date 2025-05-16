using Deploy.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Deploy.Application.Services;

public class ProjectService(DeployContext context)
{
    public Task<List<Project>> GetAll()
    {
        return context.Project.ToListAsync();
    }

    public async Task Create(Project project)
    {
        if (DeployPathExists(project.DeployPath))
            throw new Exception($"Project with path: '{project.DeployPath}' already exists.");
        
        await context.AddAsync(project);
        await context.SaveChangesAsync();
    }

    public async Task<string> GetProjectDeployPath(int id)
    {
        var project = await context.Project.FirstOrDefaultAsync(x => x.ProjectId == id)
            ?? throw new Exception("Project not found");
        
        return project.DeployPath;
    }

    private bool DeployPathExists(string deployPath)
    {
        return context
            .Project
            .Any(x => x.DeployPath.Equals(deployPath, StringComparison.OrdinalIgnoreCase));
    }
}