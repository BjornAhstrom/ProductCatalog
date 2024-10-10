namespace ProductCatalogConsole.Menus;

public class MainMenuService
{
    //private static readonly string _productsFile = Path.Combine(AppContext.BaseDirectory, "Products.json");
    //private static readonly string _categoriesFile = Path.Combine(AppContext.BaseDirectory, "Categories.json");

    //private static ProductInteraction _productInteraction = new ProductInteraction(_productsFile, _categoriesFile);
    //private static CategoryMenu _categoryMenu = new CategoryMenu(_categoriesFile);
    public void StartMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\t[1] Lista alla produkter");
            Console.WriteLine("\t[2] Lägg till en produkt");
            Console.WriteLine("\t[3] Ändra en produkt");
            Console.WriteLine("\t[4] Ta bort en produkt");
            Console.WriteLine("\t[5] Kategori meny");
            Console.WriteLine("\t[0] Avsluta");

            Console.Write("\n\tAnge ett menyalternativ: ");
            int.TryParse(Console.ReadLine(), out int option);

            switch (option)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    //_productInteraction.ListAllProducts();
                    break;
                case 2:
                    //_productInteraction.CreateProductInteraction();
                    break;
                case 3:
                    //_productInteraction.ChangeProductInfo();
                    break;
                case 4:
                    //_productInteraction.DeleteProduct();
                    break;
                case 5:
                    //_categoryMenu.DisplayCategoryMenu();
                    break;
                default:
                    Console.WriteLine("\n\tMenyalternativet finns inte.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
