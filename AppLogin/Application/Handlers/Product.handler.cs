using AppLogin.Application.Core;
using AppLogin.Application.DTOs;
using AppLogin.Api.Core;
using AppLogin.Models;
using MediatR;

namespace AppLogin.Application.Handlers;

/** Implements the IRequestHandler interface for the CreateProduct request. */
public sealed class CreateProductHandler : IRequestHandler<CreateProduct, Product>
{
    private readonly Mutation<Product> _mutation;
    public CreateProductHandler(Mutation<Product> mutation) => _mutation = mutation;

    public async Task<Product> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        if (request.Input.Price <= 0) throw new ArgumentException("Product price must be greater than zero.");
        return await _mutation.Create(request.Input);
    }
}

/** Product-specific CQRS records that wrap the generic primitives. */
#region Records ----------------------------------------------------------------

public sealed record GetProducts() : GetEntities<Product>();
public sealed record GetProductById(int Id) : GetEntityById<Product>(Id);
public sealed record GetProductsByPrice(decimal MaxPrice) : GetEntitiesByFilter<Product>(p => p.Price <= MaxPrice);

public sealed record CreateProduct(ProductInput Input) : CreateEntity<ProductInput, Product>(Input);
public sealed record UpdateProduct(int Id, ProductInput Input) : UpdateEntity<ProductInput, Product>(Id, Input);
public sealed record DeleteProduct(int Id) : DeleteEntity<Product>(Id);

#endregion --------------------------------------------------------------------