using Newtonsoft.Json;
using Resources.Enums;
using Resources.Models;

namespace Resources.Services;

public class ProductService : IProductService
{
    private readonly IFileService _fileService;
    private Catalog _catalog;

    public ProductService(IFileService fileService)
    {
        _fileService = fileService;
        _catalog = GetCatalogFromFile() ?? new Catalog();
    }

    // Took some help from ChatGpt to save and get products and categories from same json file 
    private Catalog GetCatalogFromFile()
    {
        try
        {
            var content = _fileService.GetFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                var catalog = JsonConvert.DeserializeObject<Catalog>(content);
                return catalog ?? new Catalog();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading catalog from file: {ex.Message}");

        }

        return new Catalog();
    }

    private void SaveCatalogToFile()
    {
        var content = JsonConvert.SerializeObject(_catalog, Formatting.Indented);
        _fileService.SaveToFile(content);
    }

    public StatusCodes SaveProduct(Product product)
    {
        if (_catalog.Products.Any(p => p.ProductName == product.ProductName))
        {
            return StatusCodes.Exists;
        }
        else
        {
            try
            {
                _catalog.Products.Add(product);
                SaveCatalogToFile();
                return StatusCodes.Success;
            }
            catch (Exception ex)
            {
                return StatusCodes.Failed;
            }
        }
    }

    public StatusCodes SaveCategory(Category category)
    {
        if (_catalog.Categories.Any(c => c.CategoryName == category.CategoryName))
        {
            return StatusCodes.Exists;
        }
        else
        {
            try
            {
                _catalog.Categories.Add(category);
                SaveCatalogToFile();
                return StatusCodes.Success;
            }
            catch (Exception ex)
            {
                return StatusCodes.Failed;
            }
        }
    }

    public StatusCodes UpdateProduct(Product product)
    {
        var existingProduct = _catalog.Products.FirstOrDefault(p => p.Id == product.Id);

        if (existingProduct == null)
        {
            return StatusCodes.NotFound;
        }
        else
        {
            try
            {
                var index = _catalog.Products.IndexOf(existingProduct);
                _catalog.Products[index] = product;
                SaveCatalogToFile();
                return StatusCodes.Success;
            }
            catch (Exception ex)
            {
                return StatusCodes.Failed;
            }
        }
    }

    public Product GetProduct(string id)
    {
        GetAllProducts();

        try
        {
            Product product = _catalog.Products.FirstOrDefault(p => p.Id == id)!;
            return product;
        }
        catch { }
        return null!;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        try
        {
            GetCatalogFromFile();
            if (_catalog.Products == null)
            {
                return null!;
            }

            return _catalog.Products;
        }
        catch (Exception ex) { }
        return null!;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _catalog.Categories;
    }

    public StatusCodes DeleteProduct(string id)
    {
        try
        {

            var product = _catalog.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return StatusCodes.NotFound;
            }
            else
            {
                _catalog.Products.Remove(product);
                SaveCatalogToFile();
                return StatusCodes.Success;
            }
        }
        catch
        {
            return StatusCodes.Failed;
        }
    }
}
