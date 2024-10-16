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
    private Category _selectedCategory = null!;

    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    private decimal _price;
    [ObservableProperty]
    private string _categoryName;
    [ObservableProperty]
    private string _saveOrUpdateBtnText;
    [ObservableProperty]
    private string _cancelOrBackBtnText;
    [ObservableProperty]
    private bool _isVisible = true;

    private Product _currentProduct = null!;


    public CreateProductViewModel(IProductService productService)
    {
        _productService = productService;
        GetAllCategories();
        SaveOrUpdateBtnText = "Spara";
        CancelOrBackBtnText = "Avbryt";
        EditProduct();
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

    public void EditProduct()
    {
        try
        {
            if (IntermediateStorage.CurrentProduct != null)
            {
                IsVisible = false;
                SaveOrUpdateBtnText = "Uppdatera";
                CancelOrBackBtnText = "Tillbaka";
                var id = IntermediateStorage.CurrentProduct.Id;

                if (!string.IsNullOrEmpty(id))
                {
                    _currentProduct = _productService.GetProduct(id);

                    Name = _currentProduct.ProductName;
                    Description = _currentProduct.ProductDescription;
                    Price = _currentProduct.ProductPrice;

                }
            }
        }
        catch (Exception ex) { }
    }

    [RelayCommand]
    public async Task SaveProduct()
    {
        try
        {
            if (IsVisible)
            {
                var product = new Product()
                {
                    ProductName = Name,
                    ProductDescription = Description,
                    ProductPrice = Price,
                    ProductCategory = SelectedCategory,
                };

                if (!string.IsNullOrWhiteSpace(product.ProductName) && !string.IsNullOrWhiteSpace(product.ProductDescription) && !decimal.IsNegative(product.ProductPrice) && SelectedCategory != null)
                {
                    _productService.SaveProduct(product);
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                _currentProduct.ProductName = Name;
                _currentProduct.ProductDescription = Description;
                _currentProduct.ProductPrice = Price;

                _productService.UpdateProduct(_currentProduct);
                await Shell.Current.GoToAsync("..");
            }
        }
        catch { }
    }

    [RelayCommand]
    public void Cancel()
    {
        try
        {
            Shell.Current.GoToAsync("..");
        }
        catch { }
    }

}
