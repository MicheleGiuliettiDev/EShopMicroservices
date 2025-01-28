
namespace Catalog.API.Products.UseCases.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, decimal Price, string ImageFile) : ICommand<UpdateProductResponse>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductResultCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken) ?? throw new ProductNotFoundException();
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
