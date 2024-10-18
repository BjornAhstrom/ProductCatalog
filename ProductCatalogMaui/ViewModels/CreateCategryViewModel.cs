using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Resources.Interfaces;
using Resources.Models;
using System.Collections.ObjectModel;

namespace ProductCatalogMaui.ViewModels;

public partial class CreateCategryViewModel : ObservableObject
{
    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<Category> _categories = [];

    [ObservableProperty]
    private string _categoryNameEntry;
    [ObservableProperty]
    private string _errorMessage = "";
    [ObservableProperty]
    private string _categoryNameLabel;
    [ObservableProperty]
    private bool _categoryExists = false;

    public CreateCategryViewModel(IProductService productService)
    {
        _productService = productService;
        Variables();
        GetAllCategories();
        ClearTextCommand = new RelayCommand(ClearText);
    }

    // Took help from ChatGpt to clear entry text
    public IRelayCommand ClearTextCommand { get; set; }

    private void ClearText()
    {
        CategoryNameEntry = string.Empty;
    }

    private void Variables()
    {
        CategoryNameLabel = "Kategorinamn";
        CategoryExists = false;
        ClearText();
    }

    private void GetAllCategories()
    {
        Categories.Clear();
        var categories = _productService.GetAllCategories();

        if (categories != null)
        {
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
    }

    [RelayCommand]
    public void SaveCategory()
    {
        Category category = new();

        if (!string.IsNullOrWhiteSpace(CategoryNameEntry))
        {
            category.CategoryName = CategoryNameEntry;

            var response = _productService.SaveCategory(category);

            switch (response)
            {
                case Resources.Enums.StatusCodes.Success:
                    Variables();
                    break;
                case Resources.Enums.StatusCodes.Exists:
                    ErrorMessage = "Product alredy exists.";

                    CategoryExists = true;
                    if (CategoryExists)
                    {
                        CategoryNameLabel = ErrorMessage;
                    }
                    break;
                case Resources.Enums.StatusCodes.Failed:
                    ErrorMessage = "Something went wrong.";
                    break;
            }

        }
        GetAllCategories();
    }

    [RelayCommand]
    public void Cancel()
    {
        _ = GoBack();
    }

    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
