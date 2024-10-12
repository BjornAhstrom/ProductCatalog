using ProductCatalogMaui.ViewModels;

namespace ProductCatalogMaui.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}