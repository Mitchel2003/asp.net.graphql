using AppLogin.Application.DTOs;
using AppLogin.Api.Core;
using AppLogin.Models;

namespace AppLogin.Api.Mutations;

[ExtendObjectType("Mutation")]
public class UserMutation
{
    private readonly MutationService<User> _service;
    public UserMutation(MutationService<User> service) { _service = service; }

    /**
     * Create a new user
     * Pass a dictionary with the fields to create
     * e.g., new Dictionary<string, object> { { "Name", "New Name" }, { "Age", 30 } }
     */
    public async Task<User> CreateUser(UserInput dto) => await _service.Create(dto);

    /**
     * Update an existing entity by ID
     * If you want to update only specific fields, pass a dictionary with the fields to update
     * e.g., new Dictionary<string, object> { { "Name", "New Name" }, { "Age", 30 } }
     */
    public async Task<User> UpdateUser(object id, UserInput dto) => await _service.Update(id, dto);

    /**
     * Soft delete by setting IsDeleted = true
     * If you want to hard delete, you can implement a different method
     */
    public async Task<bool> DeleteUser(object id) => await _service.Delete(id);
}