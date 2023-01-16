using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>> {

    public class Handler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;
        
        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}