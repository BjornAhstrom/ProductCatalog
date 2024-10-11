using Microsoft.Extensions.DependencyInjection;
using ProductCatalogConsole.Interactions;
using ProductCatalogConsole.Menus;
using Resources.Services;

class Program
{

    private static IServiceProvider? _serviceProvider;
    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureService(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();

        var menuService = _serviceProvider!.GetRequiredService<MainMenuService>();
        menuService.StartMenu();
    }

    private static void ConfigureService(IServiceCollection service)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDirectory, "products.json");

        service.AddSingleton<IProductService, ProductService>();
        service.AddSingleton<IFileService>(new FileService(filePath));

        service.AddSingleton<MainMenuService>();
        service.AddSingleton<ProductInteraction>();

        service.AddSingleton<CategoryMenu>();
        service.AddSingleton<CategoryInteraction>();
    }
}