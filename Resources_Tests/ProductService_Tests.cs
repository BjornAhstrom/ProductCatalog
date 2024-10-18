using Moq;
using Resources.Interfaces;
using Resources.Models;

namespace Resources_Tests;

public class ProductService_Tests
{

    private readonly Mock<IProductService> _productService;

    public ProductService_Tests()
    {
        _productService = new Mock<IProductService>();
    }


    // Product tests

    // Create ------------------------------------------------------------------------------

    [Fact]
    public void CreateNewProduct_ShouldReturnSuccess_WhenProductAddedToList()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };
        Product product = new() { Id = id, ProductName = "Iphone 18", ProductDescription = "Senaste mobilen", ProductPrice = 11999m, ProductCategory = category };

        _productService.Setup(service => service.SaveProduct(It.IsAny<Product>())).Returns(Resources.Enums.StatusCodes.Success);

        // act
        var productResponse = _productService.Object.SaveProduct(product);


        // assert
        Assert.Equal(Resources.Enums.StatusCodes.Success, productResponse);
    }

    [Fact]
    public void CreatNewProduct_ShouldReturnExists_WhenProductAlreadyExists()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };
        Product product = new() { Id = id, ProductName = "Iphone 18", ProductDescription = "Senaste mobilen", ProductPrice = 11999m, ProductCategory = category };

        _productService.Setup(service => service.SaveProduct(It.Is<Product>(p => p.ProductName == product.ProductName))).Returns(Resources.Enums.StatusCodes.Exists);

        // act
        var productResponse = _productService.Object.SaveProduct(product);


        // assert
        Assert.Equal(Resources.Enums.StatusCodes.Exists, productResponse);
    }

    // Read ------------------------------------------------------------------------------

    [Fact]
    public void GetAllProducts_ShouldResturnSuccess_WhenFetchingProducts()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };
        Product product = new() { Id = id, ProductName = "Iphone 18", ProductDescription = "Senaste mobilen", ProductPrice = 11999m, ProductCategory = category };

        var productList = new List<Product> { product };

        _productService.Setup(service => service.GetAllProducts()).Returns(productList);

        // act
        var getAllproductsList = _productService.Object.GetAllProducts();


        // assert
        Assert.Single(getAllproductsList);
    }

    // Update ------------------------------------------------------------------------------

    [Fact]
    public void UpdateProduct_ShouldReturnSucces_WhenProductIsUpdated()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };
        Product product = new() { Id = id, ProductName = "Iphone 18", ProductDescription = "Senaste mobilen", ProductPrice = 11999m, ProductCategory = category };

        _productService.Setup(service => service.UpdateProduct(It.Is<Product>(p => p.Id == id))).Returns(Resources.Enums.StatusCodes.Success);

        // act
        product.ProductName = "New name";
        var updateResponse = _productService.Object.UpdateProduct(product);

        // assert
        Assert.Equal(Resources.Enums.StatusCodes.Success, updateResponse);
    }

    // Delete ------------------------------------------------------------------------------

    [Fact]
    public void DeleteProduct_ShouldReturnSuccess_WhenProductIsDeleted()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };
        Product product = new() { Id = id, ProductName = "Iphone 18", ProductDescription = "Senaste mobilen", ProductPrice = 11999m, ProductCategory = category };

        _productService.Setup(service => service.DeleteProduct(id)).Returns(Resources.Enums.StatusCodes.Success);

        // act
        var deleteResponse = _productService.Object.DeleteProduct(id);

        // assert
        Assert.Equal(Resources.Enums.StatusCodes.Success, deleteResponse);
    }




    // Category tests

    // Create ------------------------------------------------------------------------------
    [Fact]
    public void CreateCategory_ShouldReturnSuccess_WhenCategoryAddedToList()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };

        _productService.Setup(service => service.SaveCategory(It.IsAny<Category>())).Returns(Resources.Enums.StatusCodes.Success);

        // act
        var productResponse = _productService.Object.SaveCategory(category);


        // assert
        Assert.Equal(Resources.Enums.StatusCodes.Success, productResponse);
    }

    [Fact]
    public void CreatNewCategory_ShouldReturnExists_WhenCategoryAlreadyExists()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };

        _productService.Setup(service => service.SaveCategory(It.Is<Category>(c => c.CategoryName == category.CategoryName))).Returns(Resources.Enums.StatusCodes.Exists);

        // act
        var categorytResponse = _productService.Object.SaveCategory(category);


        // assert
        Assert.Equal(Resources.Enums.StatusCodes.Exists, categorytResponse);
    }

    // Read ------------------------------------------------------------------------------

    [Fact]
    public void GetAllCategories_ShouldResturnSuccess_WhenFetchingCategories()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };

        var categoriesList = new List<Category> { category };

        _productService.Setup(service => service.GetAllCategories()).Returns(categoriesList);

        // act
        var getAllCategoriesList = _productService.Object.GetAllCategories();


        // assert
        Assert.Single(getAllCategoriesList);
    }
}
