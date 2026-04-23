public class CreateTaskRequest
{
    public required string Name { get; set; }
    public required string Priority { get; set; }
    public DateTime? ExpireDate { get; set; }
}