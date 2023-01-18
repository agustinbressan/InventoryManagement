namespace Application.Exceptions;

[Serializable]
public class ProductNotFoundException : Exception
{
    public required Guid Id { get; init; }

    public override string Message => $"Not found a Product with Id: {Id}"; 
}