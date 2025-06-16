using AppLogin.Application.Users.Commands;
using AppLogin.Api.Core;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Users.Handlers;

public sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly Mutation<User> _service;
    public DeleteUserHandler(Mutation<User> service) => _service = service;

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken) => await _service.Delete(request.Id);
}