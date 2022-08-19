using System.ComponentModel.DataAnnotations;

namespace Assets.Management.Common.Models;

public class LoginModel
{
    [Required]
    public string UserName { get; set; }
    public string Password { get; set; }
}