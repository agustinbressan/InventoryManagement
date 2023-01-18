using Application.Exceptions;
using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands;

public record UpdateProductCommand(Guid Id, string Description, int Stock) : IRequest<Product>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _repository.GetByIdAsync(request.Id);
        if (existingProduct is null) {
            throw new ProductNotFoundException() { Id = request.Id };
        }

        existingProduct.Description = request.Description;
        existingProduct.Stock = request.Stock;
        await _repository.SaveChangesAsync(cancellationToken);

        return existingProduct;
    }
}