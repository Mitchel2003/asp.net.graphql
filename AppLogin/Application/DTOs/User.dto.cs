namespace AppLogin.Application.DTOs;

public class UserInput
{
    public string Email { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}