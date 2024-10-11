using ProductCatalogConsole.Interactions;

namespace ProductCatalogConsole.Menus;

public class MainMenuService
{
    private readonly ProductInteraction _productInteraction;
    private readonly CategoryMenu _categoryMenu;

    public MainMenuService(ProductInteraction productInteraction, CategoryMenu categoryMenu)
    {
        _productInteraction = productInteraction;
        _categoryMenu = categoryMenu;
    }

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
                    _productInteraction.ExitProgram();  
                    break;
                case 1:
                    _productInteraction.ListAllProducts();
                    break;
                case 2:
                    _productInteraction.CheckIfCategoriesExistsBeforeCreatingProduct();
                    break;
                case 3:
                    _productInteraction.ChangeProductInfo();
                    break;
                case 4:
                    _productInteraction.DeleteProduct();
                    break;
                case 5:
                    _categoryMenu.DisplayCategoryMenu();    
                    break;
                default:
                    Console.WriteLine("\n\tMenyalternativet finns inte.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}


