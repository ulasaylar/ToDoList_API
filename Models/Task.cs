namespace Api.Models;

public class ToDoTask
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Priority { get; set; }
    public DateTime? ExpireDate { get; set; }
    public int Status { get; set; } = 1;
    public int UserId { get; set; }
    public int? CategoryId { get; set; }
    public User User { get; set; } = null!;
    public Category? Category { get; set; }
}