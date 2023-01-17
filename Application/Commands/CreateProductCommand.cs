using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands;

public record CreateProductCommand(string Description, int Stock) : IRequest<Product>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var createdProduct = await _repository.CreateAsync(request.Description, request.Stock);
        await _repository.SaveChangesAsync(cancellationToken);

        return createdProduct;
    }
}