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

    public MainPageViewModel(IProductService productService)
    {
        _productService = productService;
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

    public IRelayCommand LoadDataCommand { get; }

    [RelayCommand]
    public async Task CreateProduct()
    {
       await Shell.Current.GoToAsync(nameof(CreateProductPage));
    }

    [RelayCommand]
    public void CreateCategory()
    {

    }
}
