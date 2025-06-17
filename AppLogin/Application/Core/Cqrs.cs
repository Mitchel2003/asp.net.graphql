using System.Linq.Expressions;
using MediatR;

namespace AppLogin.Application.Core;

/*
 * Generic CQRS primitives (Commands, Queries)
 * for free by simply deriving concrete commands/queries from these records.
 */

#region Queries --------------------------------------------------------------

public record GetEntities<TEntity>() : IRequest<IEnumerable<TEntity>>
    where TEntity : class, new();

public record GetEntityById<TEntity>(int Id) : IRequest<TEntity?>
    where TEntity : class, new();

public record GetEntitiesByFilter<TEntity>(Expression<Func<TEntity, bool>> Filter) : IRequest<IEnumerable<TEntity>>
    where TEntity : class, new();

#endregion --------------------------------------------------------------------

#region Commands ------------------------------------------------------------

public abstract record CreateEntity<TInput, TEntity>(TInput Input) : IRequest<TEntity>
    where TEntity : class, new()
    where TInput  : class;

public abstract record UpdateEntity<TInput, TEntity>(int Id, TInput Input) : IRequest<TEntity>
    where TEntity : class, new()
    where TInput  : class;

public record DeleteEntity<TEntity>(int Id) : IRequest<bool>
    where TEntity : class, new();

#endregion --------------------------------------------------------------------