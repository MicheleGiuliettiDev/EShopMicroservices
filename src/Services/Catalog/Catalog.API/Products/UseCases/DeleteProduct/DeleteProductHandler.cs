namespace Catalog.API.Products.UseCases.DeleteProduct;

public record DeleteProductResult(bool IsSuccess);

public record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResult>;

public class DeleteCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

internal class DeleteProductCommandHandler(IDocumentSession session)
    : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product =
            await session.LoadAsync<Product>(request.Id, cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);
        session.Delete(product);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
