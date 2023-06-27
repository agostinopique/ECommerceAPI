using ECommerceAPI.Controller;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ECommerceAPI.xUnit.Testing
{
    public class ProductControllerTest
    {
        private ProductController _productController;

        public ProductControllerTest()
        {
            IProductRepo _productRepoInterface = A.Fake<IProductRepo>();
            _productController = new ProductController(_productRepoInterface);
        }


        [Fact]
        public async Task GetAllProduct_returnsList()
        {
            //Arrange

            //Act
            var result = await _productController.GetAllProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<IEnumerable<Product>>));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(1050)]
        public void GetProductById_returnsProduct(int id)
        {
            //Arrange

            //Act
            var result = _productController.GetProductById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Product>>>();
        }




        [Fact]
        public async Task CreateProduct_checkForCreation()
        {
            //Arrange
            Product prod = new Product("Test Product", 4.99);

            //Act
            var result = await _productController.CreateProduct(prod);

            //Assert
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType<CreatedAtRouteResult>();
        }



        [Theory]
        [InlineData(1)]
        [InlineData(20)]
        public async Task DeleteProduct_checkForDeletion(int id)
        {
            //Arrange

            //Act
            var resultDeletiom = await _productController.DeleteProduct(id);

            //Assert
            resultDeletiom.Should().NotBeNull();
            resultDeletiom.Should().BeOfType<NoContentResult>();
        }


        [Theory]
        [InlineData(1)]
        [InlineData(20)]
        public async Task UpdateProduct_returnPositiveAnswer(int id)
        {
            //Arrange
            var findProduct = _productController.GetProductById(id);
            Product prod = new Product("Test Product", 4.99);

            //Act
            var UpdatedProduct = _productController.UpdateProduct(id, prod);

            //Assert
            UpdatedProduct.Should().NotBeNull();
            UpdatedProduct.Should().BeOfType<Task<ActionResult>>();
        }
    }
}