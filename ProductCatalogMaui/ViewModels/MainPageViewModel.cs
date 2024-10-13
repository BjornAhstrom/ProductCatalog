using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Resources.Models;
using Resources.Services;
using System.Collections.ObjectModel;

namespace ProductCatalogMaui.ViewModels;

public partial class MainPageViewModel : ObservableObject
{

    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<Product> _products = [];

    public MainPageViewModel(IProductService productService)
    {
        _productService = productService;
        GetAllProducts();
    }

    private void GetAllProducts()
    {
        var products = _productService.GetAllProducts();

        foreach(var product in products)
        {
            Products.Add(product);
        }
    }

    [RelayCommand]
    public void CreateProduct()
    {

    }

    [RelayCommand]
    public void CreateCategory()
    {

    }
}
