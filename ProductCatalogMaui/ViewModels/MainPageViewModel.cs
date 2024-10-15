using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductCatalogMaui.Pages;
using Resources.Models;
using Resources.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ProductCatalogMaui.ViewModels;

public partial class MainPageViewModel : ObservableObject
{

    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<Product> _products = [];

    // Took help from ChatGpt to update the ListView
    public IRelayCommand LoadDataCommand { get; }
    public MainPageViewModel(IProductService productService)
    {
        _productService = productService;
        // Took help from ChatGpt to update the ListView
        LoadDataCommand = new RelayCommand(GetAllProducts);
    }

    private void GetAllProducts()
    {

        Products.Clear();
        var products = _productService.GetAllProducts();

        foreach(var product in products)
        {
            Products.Add(product);
        }
    }


    [RelayCommand]
    public async Task CreateProduct()
    {
       await Shell.Current.GoToAsync(nameof(CreateProductPage));
    }

    [RelayCommand]
    public async Task CreateCategory()
    {
        await Shell.Current.GoToAsync(nameof(CreateCategoryPage));
    }
}
