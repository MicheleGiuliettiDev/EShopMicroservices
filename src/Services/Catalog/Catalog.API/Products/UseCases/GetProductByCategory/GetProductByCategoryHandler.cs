
namespace Catalog.API.Products.UseCases.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProuctByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var result = await session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(result);
    }
}
