using System.Diagnostics;
using Deploy.Application.DTOs;
using Deploy.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Deploy.Application.Services;

public class DeployService(DeployContext context, ProjectService projectService)
{
    public async Task Execute(ExecuteDeployDTO executeDeploy)
    {
        var deployPath = projectService.GetProjectDeployPath(executeDeploy.ProjectId);
        const string composeDown = "docker compose down";
        const string composeUp = "docker compose up -d --build";
        const string gitPull = "git pull";
        using (var process = new Process())
        {
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"-c \"cd {deployPath} && {composeDown} && {gitPull} && {composeUp}\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new Exception($"Error during deploy:\n{error}");
            }
        }

        var build = new Build
        {
            ProjectId = executeDeploy.ProjectId,
            Date = DateTime.Now,
            Version = executeDeploy.Version
        };

        await context.Build.AddAsync(build);
        await context.SaveChangesAsync();
    }

    public Task<List<Build>> GetFromProject(int projectId)
    {
        return context
            .Build
            .Where(x => x.ProjectId == projectId)
            .ToListAsync();
    }
}