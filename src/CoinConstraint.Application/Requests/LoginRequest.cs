using System.ComponentModel.DataAnnotations;

namespace CoinConstraint.Application.Requests;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
