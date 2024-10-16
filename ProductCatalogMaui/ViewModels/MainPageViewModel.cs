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

    [ObservableProperty]
    private Product _selectedProduct = new();

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
    public async Task EditProduct()
    {
        try
        {
            if (SelectedProduct != null)
            {
                IntermediateStorage.CurrentProduct = SelectedProduct;
                await Shell.Current.GoToAsync("CreateProductPage");
            }
        }
        catch (Exception ex) { }
    }


    [RelayCommand]
    public async Task CreateProduct()
    {
        //await Shell.Current.GoToAsync(nameof(CreateProductPage));
        await Shell.Current.GoToAsync("CreateProductPage");
    }

    [RelayCommand]
    public async Task CreateCategory()
    {
        await Shell.Current.GoToAsync("CreateCategoryPage");
    }
}
