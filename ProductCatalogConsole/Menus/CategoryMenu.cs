using ProductCatalogConsole.Interactions;

namespace ProductCatalogConsole.Menus;

public class CategoryMenu
{
    private readonly CategoryInteraction _categoryInteraction;

    public CategoryMenu(CategoryInteraction categoryInteraction)
    {
        _categoryInteraction = categoryInteraction;
    }

    public void DisplayCategoryMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\t[1] Lista alla kategorier");
            Console.WriteLine("\t[2] Lägg till en kategori");
            Console.WriteLine("\t[0] Gå tillbaka");

            Console.Write("\n\tAnge ett menyalternativ: ");
            int.TryParse(Console.ReadLine(), out int option);

            switch (option)
            {
                case 0:
                    return;
                case 1:
                    //_categoryInteraction.GetAllCategories();
                    break;
                case 2:
                    //_categoryInteraction.CreatCatergory();
                    break;
                default:
                    Console.WriteLine("\n\tMenyalternativet finns inte");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
