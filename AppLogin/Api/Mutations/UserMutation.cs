using AppLogin.Application.Users.Commands;
using AppLogin.Application.DTOs;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Api.Mutations;

[ExtendObjectType("Mutation")]
public class UserMutation
{
    private readonly IMediator _mediator;
    public UserMutation(IMediator mediator) { _mediator = mediator; }

    /**
     * Create a new user
     * Pass a dictionary with the fields to create
     * e.g., new Dictionary<string, object> { { "Name", "New Name" }, { "Age", 30 } }
     */
    public async Task<User> CreateUser(UserInput dto) => await _mediator.Send(new CreateUserCommand(dto));

    /**
     * Update an existing user by ID
     * If you want to update only specific fields, pass a dictionary with the fields to update
     * e.g., new Dictionary<string, object> { { "Name", "New Name" }, { "Age", 30 } }
     */
    public async Task<User> UpdateUser(int id, UserInput dto) => await _mediator.Send(new UpdateUserCommand(id, dto));

    /**
     * Soft delete by setting IsDeleted = true
     * If you want to hard delete, you can implement a different method
     */
    public async Task<bool> DeleteUser(int id) => await _mediator.Send(new DeleteUserCommand(id));
}