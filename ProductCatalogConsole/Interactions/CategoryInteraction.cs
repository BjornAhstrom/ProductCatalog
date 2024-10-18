using Resources.Interfaces;
using Resources.Models;

namespace ProductCatalogConsole.Interactions;

public class CategoryInteraction
{
    private readonly IProductService _productService;
    private List<Category> _categories = [];

    public CategoryInteraction(IProductService productService)
    {
        _productService = productService;
        GetAllCategories();
    }

    public void ListAllCategories()
    {
        GetAllCategories();
        Console.Clear();
        Console.WriteLine("\n\t\tAlla Kategorier\n\t------------------------------");

        if (_categories.Any())
        {
            _categories.ForEach(x => Console.WriteLine($"\n\tId: {x.Id}\n\tKategori: {x.CategoryName}"));
        }
        else
        {
            Console.WriteLine("\n\tDet finns inga kategorier.");
        }
        Console.ReadKey();
    }

    public void CreateCategory()
    {
        Category category = new Category();

        Console.Clear();
        Console.WriteLine("\n\t\tSkapa en kategorier\n\t----------------------------------------");

        Console.Write("\n\tAnge ett kategorinamn: ");
        category.CategoryName = Console.ReadLine() ?? "";

        _productService.SaveCategory(category);
        Console.WriteLine("\n\tKategori sparad.");
        Console.ReadKey();
    }

    private void GetAllCategories()
    {
        try
        {
            var categories = _productService.GetAllCategories().ToList();

            if (categories != null && categories.Count() > 0)
            {
                _categories = _productService.GetAllCategories().ToList();
            }
        }
        catch (Exception ex) { }
    }
}
