namespace Deploy.Domain.Models;

public class Project
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string DeployPath { get; set; }
    public List<Build> Builds { get; set; } = [];
}