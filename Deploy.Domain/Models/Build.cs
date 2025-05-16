namespace Deploy.Domain.Models;

public class Build
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Version { get; set; }
    public int ProjectId { get; set; }
}