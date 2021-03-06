using System.ComponentModel.DataAnnotations;
public class UserRegisterInfo
{
    [Required]
    public string  Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required,EmailAddress]
    public string Email { get; set; }
}