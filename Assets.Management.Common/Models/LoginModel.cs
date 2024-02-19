using System.ComponentModel.DataAnnotations;

namespace Assets.Management.Common.Models;

public class LoginModel
{
    [Required]
    public required string UserName { get; set; }
    public required string Password { get; set; }
}