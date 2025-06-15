namespace AppLogin.Application.DTOs;

public record LoginInput(string Email, string Password);
public record RegisterInput(string Email, string Username, string Password, string ConfirmPassword);