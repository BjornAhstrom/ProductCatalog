using Resources.Enums;
using Resources.Models;

namespace Resources.Services
{
    public interface IProductService
    {
        StatusCodes Delete(string id);
        IEnumerable<Product> GetAllProducts();
        StatusCodes SaveProduct(Product product);
        StatusCodes UpdateProduct(Product product);
    }
}