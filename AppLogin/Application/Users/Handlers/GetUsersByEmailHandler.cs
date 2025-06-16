using AppLogin.Application.Users.Queries;
using AppLogin.Api.Core;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Handlers;

public sealed class GetUsersByEmailHandler : IRequestHandler<GetUsersByEmailQuery, IEnumerable<User>>
{
    private readonly Query<User> _service;
    public GetUsersByEmailHandler(Query<User> service) => _service = service;

    public async Task<IEnumerable<User>> Handle(GetUsersByEmailQuery request, CancellationToken cancellationToken) => await _service.GetByFilter(u => u.Email == request.Email);
}