using AppLogin.Application.Users.Queries;
using AppLogin.Api.Core;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Handlers;

public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly Query<User> _service;
    public GetUserByIdHandler(Query<User> service) => _service = service;

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) => await _service.GetById(request.Id);
}