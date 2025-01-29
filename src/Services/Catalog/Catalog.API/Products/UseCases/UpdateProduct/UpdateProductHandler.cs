namespace Catalog.API.Products.UseCases.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    decimal Price,
    string ImageFile
) : ICommand<UpdateProductResponse>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(2, 150)
            .WithMessage("Name must be between 2 and 150 characters");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class UpdateProductResultCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product =
            await session.LoadAsync<Product>(request.Id, cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);
        product.Name = request.Name;
        product.Description = request.Description;
        product.Category = request.Category;
        product.Price = request.Price;
        product.ImageFile = request.ImageFile;

        session.Update(product);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResponse(true);
    }
}
