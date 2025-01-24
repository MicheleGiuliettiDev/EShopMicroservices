using MediatR;

namespace Catalog.API.Products.UseCases.CreateProduct;

public record CreateProductCommand(string Name, string Description, List<string> Category, decimal Price, string ImageFile) : IRequest<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // TODO: Business logic to create a product
        throw new NotImplementedException();
    }
}