using Resources.Enums;
using Resources.Models;

namespace Resources.Services
{
    public interface IProductService
    {
        StatusCodes Delete(string id);
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Product> GetAllProducts();
        StatusCodes SaveCategory(Category category);
        StatusCodes SaveProduct(Product product);
        StatusCodes UpdateProduct(Product product);
    }
}