/**
 * Here are the DTOs (Data Transfer Object) for context user
 * inputs in the application layer of the AppLogin project
 */
namespace AppLogin.Application.DTOs;

public class UserInput
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}