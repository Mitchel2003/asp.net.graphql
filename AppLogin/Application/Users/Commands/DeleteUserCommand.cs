using MediatR;

namespace AppLogin.Application.Users.Commands;

/// <summary>
/// Command to soft-delete a <see cref="User"/>.
/// </summary>
/// <param name="Id">Identifier of the user to delete.</param>
public sealed record DeleteUserCommand(int Id) : IRequest<bool>;