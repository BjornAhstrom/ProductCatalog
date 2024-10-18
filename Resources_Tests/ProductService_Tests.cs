using Moq;
using Resources.Interfaces;
using Resources.Models;
using Resources.Services;

namespace Resources_Tests;

public class ProductService_Tests
{

    private readonly Mock<IProductService> _mockProductService;
    private readonly IFileService _fileService;

    public ProductService_Tests()
    {
        _mockProductService = new Mock<IProductService>();
    }


    // Took help from ChatGpt
    // File service tests

    [Fact]
    public void SaveToFile_ShouldReturnSuccess_WhenContentIsSaved()
    {
        // arrange
        string filePath = Path.GetTempFileName();
        string content = "Det här ska finnas i filen!";
        var fileService = new FileService(filePath);

        // act
        var saveToFileResponse = fileService.SaveToFile(content);

        // assert
        Assert.Equal(Resources.Enums.StatusCodes.Success, saveToFileResponse);

        // Remove temporary file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    [Fact]
    public void GetFromFile_ShouldReturnSuccess_WhenFetchingFromFile()
    {
        // arrange
        string filePath = Path.GetTempFileName();
        string content = "Det här ska finnas i filen!";
        File.WriteAllText(filePath, content);
        var fileService = new FileService(filePath);

        // act
        var getFromFileResponse = fileService.GetFromFile();

        // assert
        Assert.Equal(content, getFromFileResponse);

        // Remove temporary file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
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

        _mockProductService.Setup(service => service.SaveProduct(It.IsAny<Product>())).Returns(Resources.Enums.StatusCodes.Success);

        // act
        var productResponse = _mockProductService.Object.SaveProduct(product);

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

        _mockProductService.Setup(service => service.SaveProduct(It.Is<Product>(p => p.ProductName == product.ProductName))).Returns(Resources.Enums.StatusCodes.Exists);

        // act
        var productResponse = _mockProductService.Object.SaveProduct(product);


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

        _mockProductService.Setup(service => service.GetAllProducts()).Returns(productList);

        // act
        var getAllproductsList = _mockProductService.Object.GetAllProducts();


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

        _mockProductService.Setup(service => service.UpdateProduct(It.Is<Product>(p => p.Id == id))).Returns(Resources.Enums.StatusCodes.Success);

        // act
        product.ProductName = "New name";
        var updateResponse = _mockProductService.Object.UpdateProduct(product);

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

        _mockProductService.Setup(service => service.DeleteProduct(id)).Returns(Resources.Enums.StatusCodes.Success);

        // act
        var deleteResponse = _mockProductService.Object.DeleteProduct(id);

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

        _mockProductService.Setup(service => service.SaveCategory(It.IsAny<Category>())).Returns(Resources.Enums.StatusCodes.Success);

        // act
        var productResponse = _mockProductService.Object.SaveCategory(category);


        // assert
        Assert.Equal(Resources.Enums.StatusCodes.Success, productResponse);
    }

    [Fact]
    public void CreatNewCategory_ShouldReturnExists_WhenCategoryAlreadyExists()
    {
        // arrange
        string id = Guid.NewGuid().ToString();
        Category category = new Category { Id = id, CategoryName = "Elektronik" };

        _mockProductService.Setup(service => service.SaveCategory(It.Is<Category>(c => c.CategoryName == category.CategoryName))).Returns(Resources.Enums.StatusCodes.Exists);

        // act
        var categorytResponse = _mockProductService.Object.SaveCategory(category);


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

        _mockProductService.Setup(service => service.GetAllCategories()).Returns(categoriesList);

        // act
        var getAllCategoriesList = _mockProductService.Object.GetAllCategories();


        // assert
        Assert.Single(getAllCategoriesList);
    }
}
