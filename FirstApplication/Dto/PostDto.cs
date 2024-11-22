using System.ComponentModel.DataAnnotations;

public class CreatePostDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
}

public class UpdatePostDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    public int CategoryId { get; set; }
}
