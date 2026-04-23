namespace Api.Models;

public class ToDoTask
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Priority { get; set; }
    public DateTime? ExpireDate { get; set; }
    public int Status { get; set; } = 1;
}