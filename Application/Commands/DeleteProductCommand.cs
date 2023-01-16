using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<int>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return await _repository.SaveChangesAsync(cancellationToken);
    }
}