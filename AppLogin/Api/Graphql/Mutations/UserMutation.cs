using AppLogin.Application.Handlers;
using AppLogin.Application.DTOs;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Api.Graphql.Mutations;

[ExtendObjectType("Mutation")]
public class UserMutation
{
    /**
     * Create a new user
     * Pass a dictionary with the fields to create
     * e.g., new Dictionary<string, object> { { "Name", "New Name" }, { "Age", 30 } }
     */
    public async Task<User> CreateUser([Service] IMediator mediator, UserInput dto) => await mediator.Send(new CreateUser(dto));

    /**
     * Update an existing user by ID
     * If you want to update only specific fields, pass a dictionary with the fields to update
     * e.g., new Dictionary<string, object> { { "Name", "New Name" }, { "Age", 30 } }
     */
    public async Task<User> UpdateUser([Service] IMediator mediator, int id, UserInput dto) => await mediator.Send(new UpdateUser(id, dto));

    /**
     * Soft delete by setting IsDeleted = true
     * If you want to hard delete, you can implement a different method
     */
    public async Task<bool> DeleteUser([Service] IMediator mediator, int id) => await mediator.Send(new DeleteUser(id));
}