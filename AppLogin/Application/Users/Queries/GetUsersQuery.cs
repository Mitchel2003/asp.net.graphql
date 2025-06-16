using System.Collections.Generic;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Queries;

/// <summary> CQRS query that returns all users. </summary>
public sealed record GetUsersQuery() : IRequest<IEnumerable<User>>;