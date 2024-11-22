using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; }


    [Required]
    [EmailAddress]
    public string Email { get; set; }
}


public class UpdateUserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}


