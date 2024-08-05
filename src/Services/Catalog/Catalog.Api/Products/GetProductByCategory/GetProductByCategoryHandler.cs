using Catalog.Api.Products.GetProductById;
using Marten.Linq.QueryHandlers;

namespace Catalog.Api.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category): IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query);
            var products = await session.Query<Product>().Where(p=>p.Category.Contains(query.Category)).ToListAsync(cancellationToken);
            if (products is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByCategoryResult(products);
        }
    }
}
