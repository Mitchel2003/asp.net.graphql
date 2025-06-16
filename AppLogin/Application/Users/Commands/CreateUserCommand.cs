using AppLogin.Application.DTOs;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Commands;

/// <summary>
/// Command (CQRS) that encapsulates the data required to create a new <see cref="User"/>.
/// </summary>
/// <param name="Input">DTO with the data for the new user.</param>
public sealed record CreateUserCommand(UserInput Input) : IRequest<User>;