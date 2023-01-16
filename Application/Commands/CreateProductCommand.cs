using Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public record CreateProductCommand(string Description, int Stock) : IRequest<int>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _repository.CreateAsync(request.Description, request.Stock);
        return await _repository.SaveChangesAsync(cancellationToken);
    }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
    }
}