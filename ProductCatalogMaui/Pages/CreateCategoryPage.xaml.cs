using ProductCatalogMaui.ViewModels;

namespace ProductCatalogMaui.Pages;

public partial class CreateCategoryPage : ContentPage
{
	public CreateCategoryPage(CreateCategryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}
}