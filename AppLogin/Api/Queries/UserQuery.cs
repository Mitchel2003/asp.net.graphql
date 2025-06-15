using AppLogin.Api.Core;
using AppLogin.Models;

namespace AppLogin.Api.Queries;

[ExtendObjectType("Query")]
public class UserQuery
{
    private readonly QueryService<User> _users;
    public UserQuery(QueryService<User> users) { _users = users; }

    /**
     * Retrieves all users from the database.
     * @return A list of all users.
     */
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<User>> GetUsers() => await _users.GetAll();

    /**
     * Retrieves a user by their ID.
     * @param id The ID of the user to retrieve.
     * @return The user with the specified ID, or null if not found.
     */
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<User?> GetUserById(string id) => await _users.GetById(id);

    /**
     * Retrieves users by their email address.
     * @param email The email address of the users to retrieve.
     * @return A list of users with the specified email address.
     */
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<User>> GetUsersByEmail(string email) => await _users.GetByFilter(u => u.Email == email);
}