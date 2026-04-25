using System.ComponentModel.DataAnnotations;

public class CreateTaskRequest
{
    public required string Name { get; set; }
    public required int Priority { get; set; }
    public DateTime? ExpireDate { get; set; }
}