using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Resources.Interfaces;
using Resources.Models;
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
        try
        {
            IntermediateStorage.CurrentProduct = null!;
            Products.Clear();
            var products = _productService.GetAllProducts();

            if (products != null)
            {
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
        }
        catch (Exception ex) { }
    }

    [RelayCommand]
    public async Task EditProduct()
    {
        try
        {
            IntermediateStorage.CurrentProduct = SelectedProduct;

            if (IntermediateStorage.CurrentProduct != null)
            {
                await Shell.Current.GoToAsync("CreateProductPage");
            }
        }
        catch (Exception ex) { }
    }


    [RelayCommand]
    public async Task CreateProduct()
    {
        try
        {
            await Shell.Current.GoToAsync("CreateProductPage");
        }
        catch (Exception ex) { }
    }

    [RelayCommand]
    public async Task CreateCategory()
    {
        try
        {
            await Shell.Current.GoToAsync("CreateCategoryPage");
        }
        catch (Exception ex) { }
    }
}
