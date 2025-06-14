using Microsoft.EntityFrameworkCore;
using AppLogin.Application.Shared;
using AppLogin.Application.DTOs;
using System.Linq.Dynamic.Core;

namespace AppLogin.Api;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<object>> GetAll(IGetAll input, [Service] IDbContextFactory<AppDBContext> dbFactory)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var query = GetQueryable(db, Helpers.GetModelType(input.Model));
        return await query.ToDynamicListAsync();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<object> GetById(IGetById input, [Service] IDbContextFactory<AppDBContext> dbFactory)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var type = Helpers.GetModelType(input.Model);
        var query = await db.FindAsync(type, input.Id)
            ?? throw new GraphQLException("Entity not found.");
        return query;
    }

    public async Task<IEnumerable<object>> GetByFilter(IGetByFilter input, [Service] IDbContextFactory<AppDBContext> dbFactory)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var query = GetQueryable(db, Helpers.GetModelType(input.Model));

        if (input.Filter != null)
        { //Prepare the query if filter provided
            foreach (var (key, value) in input.Filter)
            { //Iterate through the filter dictionary
                query = query.Where($"{key} == @0", value);
            }
        }
        var result = await query.ToDynamicListAsync();
        return result;
    }
    /*---------------------------------------------------------------------------------------------------------*/

    /*--------------------------------------------------Helpers--------------------------------------------------*/
    /**
     * Helper method to get a queryable for the specified entity type.
     * This uses reflection to call DbContext.Set<TEntity>().
     */
    private static IQueryable GetQueryable(DbContext context, Type entityType)
    {
        var method = typeof(DbContext)
            .GetMethod(nameof(DbContext.Set), Type.EmptyTypes)!
            .MakeGenericMethod(entityType);

        // Invoke the method to get DbSet specified entity type
        var dbSet = method.Invoke(context, null);
        return (IQueryable)dbSet!;
    }
}
