using Microsoft.Extensions.Logging;
using ProductCatalogMaui.Pages;
using ProductCatalogMaui.ViewModels;
using Resources.Services;

namespace ProductCatalogMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(baseDirectory, "products.json");

            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IFileService>(new FileService(filePath));

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<CreateProductPage>();
            builder.Services.AddTransient<CreateProductViewModel>();

            builder.Services.AddTransient<CreateCategoryPage>();
            builder.Services.AddTransient<CreateCategryViewModel>();

            return builder.Build();
        }
    }
}
