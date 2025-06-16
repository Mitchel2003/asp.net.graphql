using AppLogin.Application.DTOs;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Commands;

/// <summary>
/// Command to update an existing <see cref="User"/>.
/// </summary>
/// <param name="Id">Entity identifier.</param>
/// <param name="Input">DTO with updated values.</param>
public sealed record UpdateUserCommand(int Id, UserInput Input) : IRequest<User>;