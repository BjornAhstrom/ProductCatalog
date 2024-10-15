using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Resources.Models;
using Resources.Services;
using System.Collections.ObjectModel;

namespace ProductCatalogMaui.ViewModels;

public partial class CreateCategryViewModel : ObservableObject
{
    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<Category> _categories = [];

    [ObservableProperty]
    private string _name;

    public CreateCategryViewModel(IProductService productService)
    {
        _productService = productService;
        GetAllCategories();
    }

    private void GetAllCategories()
    {
        var categories = _productService.GetAllCategories();

        if (categories != null)
        {
            foreach(var category in categories)
            {
                Categories.Add(category);
            }
        }
    }

    [RelayCommand]
    public void SaveCategory()
    {
        Category category = new();

        if (!string.IsNullOrWhiteSpace(Name))
        {
            category.CategoryName = Name;

            _productService.SaveCategory(category);
            
        }
        
    }

    [RelayCommand]
    public void Cancel()
    {
        Shell.Current.GoToAsync("..");
    }
}
