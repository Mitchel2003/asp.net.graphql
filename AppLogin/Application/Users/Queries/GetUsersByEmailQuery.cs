using System.Collections.Generic;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Queries;

/// <summary> Query that returns all users matching the given e-mail. </summary>
/// <param name="Email">Email filter.</param>
public sealed record GetUsersByEmailQuery(string Email) : IRequest<IEnumerable<User>>;