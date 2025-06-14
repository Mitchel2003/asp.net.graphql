using Microsoft.EntityFrameworkCore;
using AppLogin.Application.Shared;
using AppLogin.Application.DTOs;
using System.Reflection;
using System.Text.Json;

namespace AppLogin.Api;

public class Mutation
{
    public async Task<object> Create(ICreate input, [Service] IDbContextFactory<AppDBContext> dbFactory)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var modelType = Helpers.GetModelType(input.Model);

        var entityJson = JsonSerializer.Serialize(input.Data); //Serialize data to JSON
        var entity = JsonSerializer.Deserialize(entityJson, modelType) //Build model entity
            ?? throw new GraphQLException("Invalid input data."); //or throw an exception if deserialization fails

        db.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<object> Update(IUpdate input, [Service] IDbContextFactory<AppDBContext> dbFactory)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var modelType = Helpers.GetModelType(input.Model);

        var entity = await db.FindAsync(modelType, input.Id) //Build model entity
            ?? throw new GraphQLException("Entity not found."); //or throw an exception if fails

        foreach (var (key, value) in input.Data)
        { // Iterate through the fields to update, using reflection to set properties
            var prop = modelType.GetProperty(key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop != null && prop.CanWrite)
            { // Check if the property exists and is writable "Partial<>"
                var typedValue = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(entity, typedValue);
            }
        }
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(IDelete input, [Service] IDbContextFactory<AppDBContext> dbFactory)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var modelType = Helpers.GetModelType(input.Model);

        var entity = await db.FindAsync(modelType, input.Id) // Find the entity by ID
            ?? throw new GraphQLException("Entity not found."); //or throw an exception if fails

        // Check if the entity has a property named "IsDeleted" using reflection
        var isDeletedProperty = modelType.GetProperty("IsDeleted", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        if (isDeletedProperty != null) { isDeletedProperty.SetValue(entity, true); }
        else { db.Remove(entity); }
        await db.SaveChangesAsync();
        return true;
    }
}