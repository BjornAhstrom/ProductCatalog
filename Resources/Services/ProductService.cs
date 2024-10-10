using Newtonsoft.Json;
using Resources.Enums;
using Resources.Models;

namespace Resources.Services;

public class ProductService : IProductService
{
    private readonly IFileService _fileService;
    private List<Product> _products = [];

    public ProductService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public StatusCodes SaveProduct(Product product)
    {
        if (_products.Any(p => p.ProductName == product.ProductName))
        {
            return StatusCodes.Exists;
        }
        else
        {
            try
            {
                _products.Add(product);
                var result = SaveProduct();
                return result;
            }
            catch (Exception ex)
            {
                return StatusCodes.Failed;
            }
        }
    }

    public StatusCodes UpdateProduct(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct == null)
        {
            return StatusCodes.NotFound;
        }
        else
        {
            try
            {
                existingProduct = product;
                var result = SaveProduct();
                return result;
            }
            catch (Exception ex)
            {
                return StatusCodes.Failed;
            }
        }
    }

    public IEnumerable<Product> GetAllProducts()
    {
        try
        {
            var content = _fileService.GetFromFile();

            if (!string.IsNullOrEmpty(content))
            {
                var tempProducts = JsonConvert.DeserializeObject<List<Product>>(content);
                if (tempProducts != null && tempProducts.Count > 0)
                {
                    _products = tempProducts;
                }
            }
        }
        catch (Exception ex) { }

        return _products;
    }

    public StatusCodes Delete(string id)
    {
        try
        {
            _products = GetAllProducts().ToList();

            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return StatusCodes.NotFound;
            }
            else
            {
                _products.Remove(product);
                var result = SaveProduct();
                return result;
            }
        }
        catch
        {
            return StatusCodes.Failed;
        }
    }

    private StatusCodes SaveProduct()
    {
        return _fileService.SaveToFile(JsonConvert.SerializeObject(_products, Formatting.Indented));
    }
}
