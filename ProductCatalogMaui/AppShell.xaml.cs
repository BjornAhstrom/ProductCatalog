using ProductCatalogMaui.Pages;

namespace ProductCatalogMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateProductPage), typeof(CreateProductPage));
        }
    }
}
