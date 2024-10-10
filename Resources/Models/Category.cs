namespace Resources.Models;

public class Category
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CategoryName { get; set; } = null!;
}
