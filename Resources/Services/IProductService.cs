using Resources.Enums;
using Resources.Models;

namespace Resources.Services
{
    public interface IProductService
    {
        StatusCodes DeleteProduct(string id);
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(string id);
        StatusCodes SaveCategory(Category category);
        StatusCodes SaveProduct(Product product);
        StatusCodes UpdateProduct(Product product);
    }
}