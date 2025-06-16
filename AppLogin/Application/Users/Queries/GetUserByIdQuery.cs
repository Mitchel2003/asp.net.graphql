using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Queries;

/// <summary> Query that returns a single user by ID. </summary>
/// <param name="Id">Identifier of the user.</param>
public sealed record GetUserByIdQuery(int Id) : IRequest<User?>;
