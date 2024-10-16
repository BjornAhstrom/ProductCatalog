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
    private bool _isLabelAndEntryVisible = true;
    [ObservableProperty]
    private bool _isCategoryVisible = true;
    [ObservableProperty]
    private bool _isDescriptionVisible = false;


    [ObservableProperty]
    private bool _saveChangedProduct = false;
    [ObservableProperty]
    private bool _changeProduct = true;

    private Product _currentProduct = null!;


    public CreateProductViewModel(IProductService productService)
    {
        _productService = productService;
        GetAllCategories();
        SaveOrUpdateBtnText = "Spara";
        CancelOrBackBtnText = "Avbryt";
        DisplayProduct();
    }

    private void GetAllCategories()
    {
        var categories = _productService.GetAllCategories();

        foreach (var category in categories)
        {
            Categories.Add(category);
        }
    }

    public string SelectedCategoryText => SelectedCategory == null ? "Välj en kategori" : SelectedCategory.CategoryName;

    partial void OnSelectedCategoryChanged(Category value)
    {
        OnPropertyChanged(nameof(SelectedCategoryText));
    }

    public void DisplayProduct()
    {
        try
        {
            if (IntermediateStorage.CurrentProduct != null)
            {
                SaveChangedProduct = false;
                IsLabelAndEntryVisible = false;
                IsDescriptionVisible = true;
                IsCategoryVisible = false;
                ChangeProduct = false;

                SaveOrUpdateBtnText = "Ändra produkten";
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

    public void EditProduct()
    {
        Debug.WriteLine("INSIDE Edit product");

        IsLabelAndEntryVisible = true;
        IsDescriptionVisible = false;
        IsCategoryVisible = false;
        ChangeProduct = true;
        SaveChangedProduct = true;

        SaveOrUpdateBtnText = "Spara";
        CancelOrBackBtnText = "Avbryt";
    }

    public void UpdateProduct()
    {
        try
        {
            _currentProduct.ProductName = Name;
            _currentProduct.ProductDescription = Description;
            _currentProduct.ProductPrice = Price;

            _productService.UpdateProduct(_currentProduct);

            _ = GoBack();
        }
        catch { }
    }

    private void SaveNewProduct()
    {
        try
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
                _ = GoBack();
            }
        }
        catch { }
    }

    [RelayCommand]
    public void SaveProduct()
    {
        if (ChangeProduct && !SaveChangedProduct)
        {
            SaveNewProduct();
        }
        else if (SaveChangedProduct)
        {
            UpdateProduct();
        }
        else
        {
            EditProduct();
        }
    }


    [RelayCommand]
    public void Cancel()
    {
        // If no changes go back
        if (!ChangeProduct)
        {
            _ = GoBack();
        }
        // Undo edit and restore the product
        else if (SaveChangedProduct)
        {
            DisplayProduct();
        }
        // Cancel when creating new product
        else
        {
            _ = GoBack();
        }
    }

    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

}
