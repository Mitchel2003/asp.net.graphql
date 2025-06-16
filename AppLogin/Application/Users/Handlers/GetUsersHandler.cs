using AppLogin.Application.Users.Queries;
using AppLogin.Api.Core;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Handlers;

public sealed class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly Query<User> _service;
    public GetUsersHandler(Query<User> service) => _service = service;

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken) => await _service.GetAll();
}