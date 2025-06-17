namespace AppLogin.Application.DTOs;

/// <summary>
/// DTO used for creating or updating <see cref="AppLogin.Models.Product"/>.
/// </summary>
public sealed class ProductInput
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
