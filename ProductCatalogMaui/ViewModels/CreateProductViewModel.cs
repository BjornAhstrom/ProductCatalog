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
    private string _name;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    private decimal _price;



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
        }
    }

    public string SelectedCategoryText => SelectedCategory == null ? "Välj en kategori" : SelectedCategory.CategoryName;

    partial void OnSelectedCategoryChanged(Category value)
    {
        OnPropertyChanged(nameof(SelectedCategoryText));
    }

    [RelayCommand]
    public async Task SaveProduct()
    {
        var product = new Product()
        {
            ProductName = Name,
            ProductDescription = Description,
            ProductPrice = Price,
            ProductCategory = SelectedCategory,
        };

       if (product != null)
        {
            _productService.SaveProduct(product);
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    public void Cancel()
    {
        Shell.Current.GoToAsync("..");
    }

}
