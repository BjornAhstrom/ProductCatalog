using Resources.Models;
using Resources.Services;

namespace ProductCatalogConsole.Interactions;

public class ProductInteraction
{
    private readonly IProductService _productService;
    private List<Product> _products = [];
    private List<Category> _categories = [];

    public ProductInteraction(IProductService productService)
    {
        _productService = productService;
        GetAllCategories();
    }

    public void ListAllProducts()
    {
        GetProductsFromFile();
        Console.Clear();
        Console.WriteLine("\n\t\tAlla Produkter\n\t------------------------------");

        if (_products.Any())
        {
            _products.ForEach(x => Console.WriteLine($"\n\tId: {x.Id}\n\tProdukt: {x.ProductName}\n\tPris: {x.ProductPrice} kr\n\tKategori: {x.ProductCategory.CategoryName}"));
        }
        else
        {
            Console.WriteLine("\n\tDet finns inga produkter.");
        }
        Console.ReadKey();
    }

    public void CreateProduct()
    {
        Product product = new();

        Console.Clear();
        Console.WriteLine("\n\t\tSkapa en Produkt\n\t--------------------------------");

        CheckIfProductExists(product);
        SetDescriptonOnProduct(product);
        SetPriceOnProduct(product);
        AddCategoryToProduct(product);
        SaveProduct(product);
    }

    private void SetPriceOnProduct(Product product)
    {
        while (true)
        {
            Console.Write("\tPris: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                product.ProductPrice = price;
                break;
            }
            else
            {
                Console.WriteLine("\n\tPriset måste vara i siffror.");
            }
        }
    }

    private void CheckIfProductExists(Product product)
    {
        try
        {
            GetProductsFromFile();

            while (true)
            {
                Console.Write("\n\tProdukt namn: ");
                string productName = Console.ReadLine() ?? "";
                bool existingProdukt = _products.Any(p => p.ProductName.ToLower() == productName.ToLower());

                if (!existingProdukt)
                {
                    product.ProductName = productName;
                    break;
                }
                else
                {
                    Console.Write($"\n\t{productName} existerar redan. Ange ett nytt produktnamn.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    private void SetDescriptonOnProduct(Product product)
    {
        Console.Write("\n\tAnge en beskrivning: ");
        product.ProductDescription = Console.ReadLine() ?? "";
    }

    private void SaveProduct(Product product)
    {
        var result = _productService.SaveProduct(product);
        switch (result)
        {
            case Resources.Enums.StatusCodes.Success:
                Console.WriteLine("\n\tProdukten är sparad.");
                Console.ReadKey();
                break;
            case Resources.Enums.StatusCodes.Exists:
                Console.WriteLine("\n\tProdukten existerar redan.");
                Console.ReadKey();
                break;
            case Resources.Enums.StatusCodes.Failed:
                Console.WriteLine("\n\tDet gick inte att spara.");
                Console.ReadKey();
                break;
        }
    }


    public void AddCategoryToProduct(Product product)
    {
        GetAllCategories();
        Console.WriteLine("\n\t\tVälj en kategori\n\t------------------------------------");

        if (_categories != null && _categories.Count != 0)
        {
            for (int i = 0; i < _categories.Count; i++)
            {
                Console.WriteLine($"\n\t[{i + 1}]\n\tKategori: {_categories[i].CategoryName}");
            }

            while (true)
            {
                Console.Write($"\n\tAnge platsnummer för att välja kategori: ");

                if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= _categories.Count)
                {
                    product.ProductCategory = _categories[option - 1];
                    break;
                }
                else
                {
                    Console.WriteLine("\n\tOgiltigt platsnummer");
                }
            }
        }
    }

    //// Tog hjälp av ChatGpt för att kunna antingen ändra namn eller pris, eller välja att behålla namnet eller priset som redan finns på en produkt
    public void ChangeProductInfo()
    {
        GetProductsFromFile();
        Product product = new Product();
        Console.Clear();
        Console.WriteLine("\n\t\tUppdatera en produkt\n\t------------------------------------");

        for (int i = 0; i < _products.Count; ++i)
        {
            Console.WriteLine($"\n\t[{i + 1}]\n\tProdukt id: {_products[i].Id}\n\tProdukt: {_products[i].ProductName}\n\tPris: {_products[i].ProductPrice}\n\tKategori: {_products[i].ProductCategory.CategoryName}");
        }

        Console.Write("\n\tAnge platsnummer för den produkt du vill uppdatera: ");

        if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= _products.Count)
        {
            product = _productService.GetProduct(_products[option - 1].Id);
            string defaultName = product.ProductName;
            decimal defaultPrice = product.ProductPrice;

            Console.WriteLine($"\n\t{product.Id}");

            Console.WriteLine("\n\t\tÄndra namn\n\t-------------------------");
            Console.Write($"\n\tNuvarande namn är {product.ProductName}. Ändra eller tryck enter för att behålla: ");
            string inputName = Console.ReadLine() ?? "";
            product.ProductName = string.IsNullOrWhiteSpace(inputName) ? defaultName : inputName;

            Console.WriteLine("\n\t\tÄndra pris\n\t-------------------------");
            Console.Write($"\n\tNuvarande pris är {product.ProductPrice}. Ändra eller tryck enter för att behålla: ");

            ChangePriceOnProduct(product, defaultPrice);

            var result = _productService.UpdateProduct(product);

            if (result == Resources.Enums.StatusCodes.Success)
            {
                Console.WriteLine("\n\tProdukten har uppdaterats.");
            }
            else
            {
                Console.WriteLine($"\n\tKunde inte uppdatera produkten.");
            }
        }
        Console.ReadKey();
    }

    private void ChangePriceOnProduct(Product product, decimal defaultPrice)
    {
        while (true)
        {
            string inputPrice = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(inputPrice))
            {
                product.ProductPrice = defaultPrice;
                break;
            }
            else if (decimal.TryParse(inputPrice, out decimal price))
            {
                product.ProductPrice = price;
                break;
            }
            else
            {
                Console.Write("\n\tFel: Ange ett giltigt pris i siffror.\n\tFörsök igen eller tryck enter för att behålla nuvarande pris: ");
            }
        }
    }

    public void DeleteProduct()
    {
        GetProductsFromFile();
        Console.Clear();
        Console.WriteLine("\n\t\tTa bort en produkt\n\t------------------------------------");

        for (int i = 0; i < _products.Count; ++i)
        {
            Console.WriteLine($"\n\t[{i + 1}]\n\tProdukt id: {_products[i].Id}\n\tProdukt: {_products[i].ProductName}\n\tPris: {_products[i].ProductPrice}\n\tKategori: {_products[i].ProductCategory.CategoryName}");
        }

        Console.Write("\n\tAnge platsnummer för att ta bort en produkt: ");

        if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= _products.Count)
        {
            var result = _productService.DeleteProduct(_products[option - 1].Id);

            if (result == Resources.Enums.StatusCodes.Success)
            {
                Console.WriteLine($"\n\tProdukt {_products[option - 1].ProductName} är borttaget.");
            }
            else
            {
                Console.WriteLine($"Bortagningen av produkten {_products[option - 1].ProductName} misslyckades.");
            }
        }
        Console.ReadKey();
    }

    private void CheckIfCategoriesExists()
    {

        if (_categories.Count! > 0)
        {
            Console.Clear();
            Console.WriteLine("\n\tDet finns inga kategorier!\n\tDu måste lägga till en kategori innan du kan skapa en produkt.");
            Console.ReadKey();

            // _categoryInteraction.CreatCatergory();
        }
    }

    private void GetAllCategories()
    {
        _categories = _productService.GetAllCategories().ToList();
    }

    private void GetCategoriesFromFile()
    {
        try
        {
            var categories = _productService.GetAllCategories().ToList();
            if (categories != null && categories.Count() > 0)
            {
                _categories = categories;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }
    private void GetProductsFromFile()
    {
        try
        {
            var products = _productService.GetAllProducts().ToList();
            if (products != null && products.Count() > 0)
            {
                _products = products;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }
}
