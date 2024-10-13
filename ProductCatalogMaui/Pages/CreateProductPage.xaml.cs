using ProductCatalogMaui.ViewModels;

namespace ProductCatalogMaui.Pages;

public partial class CreateProductPage : ContentPage
{
	public CreateProductPage(CreateProductViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}
}