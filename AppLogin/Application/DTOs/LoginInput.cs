/**
 * Here are the DTOs (Data Transfer Object) for context authentication
 * inputs in the application layer of the AppLogin project
 */
namespace AppLogin.Application.DTOs;

public record LoginInput(string Email, string Password);