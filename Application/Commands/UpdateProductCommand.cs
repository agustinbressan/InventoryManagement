using Application.Interfaces;
using Domain.Models;
using FluentValidation;
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
            throw new ValidationException("Product not found.");
        }

        existingProduct.Description = request.Description;
        existingProduct.Stock = request.Stock;
        await _repository.SaveChangesAsync(cancellationToken);

        return existingProduct;
    }
}