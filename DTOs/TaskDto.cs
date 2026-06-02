public class TaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public DateTime? ExpireDate { get; set; }
    public int Status { get; set; }
    public CategoryDto? Category { get; set; }
}

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}