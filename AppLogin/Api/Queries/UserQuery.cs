using AppLogin.Application.Users.Queries;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Api.Queries;

[ExtendObjectType("Query")]
public class UserQuery
{
    private readonly IMediator _mediator;
    public UserQuery(IMediator mediator) { _mediator = mediator; }

    /**
     * Retrieves all users from the database.
     * @return A list of all users.
     */
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<User>> GetUsers() => await _mediator.Send(new GetUsersQuery());

    /**
     * Retrieves a user by their ID.
     * @param id The ID of the user to retrieve.
     * @return The user with the specified ID, or null if not found.
     */
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<User?> GetUserById(int id) => await _mediator.Send(new GetUserByIdQuery(id));

    /**
     * Retrieves users by their email address.
     * @param email The email address of the users to retrieve.
     * @return A list of users with the specified email address.
     */
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<User>> GetUsersByEmail(string email) => await _mediator.Send(new GetUsersByEmailQuery(email));
}