using AppLogin.Application.Users.Commands;
using AppLogin.Api.Core;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Handlers;

public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly Mutation<User> _service;
    public UpdateUserHandler(Mutation<User> service) => _service = service;

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken) => await _service.Update(request.Id, request.Input);
}