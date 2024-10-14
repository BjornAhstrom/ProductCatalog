using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Resources.Models;
using Resources.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ProductCatalogMaui.ViewModels;

public partial class CreateProductViewModel : ObservableObject
{
    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<Category> _categories = [];
    [ObservableProperty]
    private Category _selectedCategory;
    [ObservableProperty]
    private Product _product = new();

    public CreateProductViewModel(IProductService productService)
    {
        _productService = productService;
        GetAllCategories();
    }

    private void GetAllCategories()
    {
        var categories = _productService.GetAllCategories();

        foreach(var category in categories)
        {
            Categories.Add(category);
            Debug.WriteLine(category);
        }
    }

    [RelayCommand]
    public async Task SaveProduct()
    {
       if (Product != null)
        {
            _productService.SaveProduct(Product);
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    public void Cancel()
    {
        Shell.Current.GoToAsync("..");
    }

}
