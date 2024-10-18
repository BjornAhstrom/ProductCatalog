using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Resources.Interfaces;
using Resources.Models;
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
    private string _ErrorMessage = "";
    [ObservableProperty]
    private string _productNameLabel = "";
    [ObservableProperty]
    private bool _isLabelAndEntryVisible = true;
    [ObservableProperty]
    private bool _isCategoryVisible = true;
    [ObservableProperty]
    private bool _isDescriptionVisible = false;
    [ObservableProperty]
    private bool _productExists = false;


    [ObservableProperty]
    private bool _saveChangedProduct = false;
    [ObservableProperty]
    private bool _saveNewCreatedProduct = true;

    private Product _currentProduct = null!;


    public CreateProductViewModel(IProductService productService)
    {
        _productService = productService;
        GetAllCategories();
        Variables();
        DisplayProduct();
    }

    private void Variables()
    {
        ProductNameLabel = "Produktnamn";
        SaveOrUpdateBtnText = "Spara";
        CancelOrBackBtnText = "Avbryt";
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
                SaveNewCreatedProduct = false;

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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public void EditProduct()
    {
        IsLabelAndEntryVisible = true;
        IsDescriptionVisible = false;
        IsCategoryVisible = false;
        SaveNewCreatedProduct = true;
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

            var response = _productService.UpdateProduct(_currentProduct);
            if (response == Resources.Enums.StatusCodes.Success)
            {
                _ = GoBack();
            }
            else
            {
                ErrorMessage = "Something went wrong.";
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    [RelayCommand]
    public void DeleteProduct()
    {
        try
        {
            if (IntermediateStorage.CurrentProduct != null)
            {
                var response = _productService.DeleteProduct(IntermediateStorage.CurrentProduct.Id);
                if (response == Resources.Enums.StatusCodes.Success)
                {
                    _ = GoBack();
                }
                else
                {
                    ErrorMessage = "Something went wrong.";
                }

            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
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
                var response = _productService.SaveProduct(product);
                switch (response)
                {
                    case Resources.Enums.StatusCodes.Success:
                        _ = GoBack();
                        break;
                    case Resources.Enums.StatusCodes.Exists:
                        ErrorMessage = "Product alredy exists.";
                        ProductExists = true;

                        if (ProductExists)
                        {
                            ProductNameLabel = ErrorMessage;
                        }
                        break;
                    case Resources.Enums.StatusCodes.Failed:
                        ErrorMessage = "Something went wrong.";
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    public void SaveProduct()
    {
        if (SaveNewCreatedProduct && !SaveChangedProduct)
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
        if (!SaveNewCreatedProduct)
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
