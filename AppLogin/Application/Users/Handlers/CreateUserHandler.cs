using AppLogin.Application.Users.Commands;
using AppLogin.Api.Core;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Handlers;

public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly Mutation<User> _service;
    public CreateUserHandler(Mutation<User> service) => _service = service;

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken) => await _service.Create(request.Input);
}