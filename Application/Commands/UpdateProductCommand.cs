using Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public record UpdateProductCommand(Guid Id, string Description, int Stock) : IRequest<int>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
{
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(request.Id, request.Description, request.Stock);
        return await _repository.SaveChangesAsync(cancellationToken);
    }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
    }
}