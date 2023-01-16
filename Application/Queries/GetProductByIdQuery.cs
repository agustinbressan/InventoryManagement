using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Product>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?> {

    private readonly IProductRepository _repository;
    
    public GetProductByIdQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }

}