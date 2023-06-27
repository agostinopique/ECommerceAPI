using ECommerceAPI.Controller;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Testing
{
    [TestFixture]
    public class ProductControllerTests
    {

        // Testing with Mocking 
        //private Mock<ProductController> _productController;
        //private Mock<IProductRepo> _productRepoInterface;



        // Testing with FakeItEasy 
        private ProductController _productController;

        //public ProductControllerTests()
        //{
        //    _productRepoInterface = new Mock<IProductRepo>();
        //}

        [SetUp]
        public void Setup()
        {
            //_productRepoInterface = new Mock<IProductRepo>();

            IProductRepo _productRepoInterface = A.Fake<IProductRepo>();
            _productController = new ProductController(_productRepoInterface);
            
            //_productController = new Mock<ProductController>(_productRepoInterface);
        }


        [Test]
        public async Task GetAllProduct_returnsList()
        {
            //Arrange

            //Act
            var result = await _productController.GetAllProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<IEnumerable<Product>>));

            //arrange

            //var productList = GetAllProducts();

            //_productRepoInterface.Setup(x => x.GetAllProducts())
            //    .Returns(productList);

            //var productController = new ProductController(_productRepoInterface.Object);

            ////act
            //var productResult = productController.GetAllProducts();

            //Assert.NotNull(productResult);
            //Assert.Equal(GetProductsData().Count(), productResult.Count());
            //Assert.Equal(GetProductsData().ToString(), productResult.ToString());
            //Assert.True(productList.Equals(productResult));
        }


        //private Task<IEnumerable<Product>> GetAllProducts()
        //{
        //    List<Product> products = new List<Product> {
        //        new Product("Test1", 4.99),
        //        new Product("Test2", 6.99),
        //        new Product("Test3", 5.99),
        //        new Product("Test4", 3.99)
        //    };

        //    Task<IEnumerable<Product>> prods = products;

        //    return prods;
        //}

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(10)]
        [Parallelizable(ParallelScope.All)]
        public void GetProductById_returnsProduct(int id)
        {
            //Arrange

            //Act
            var result = _productController.GetProductById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Product>>>();
        }

        [Test]
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


        [Test]
        [TestCase(1)]
        [TestCase(20)]
        [Parallelizable(ParallelScope.All)]
        public async Task DeleteProduct_checkForDeletion(int id)
        {
            //Arrange

            //Act
            var resultDeletiom = await _productController.DeleteProduct(id);

            //Assert
            resultDeletiom.Should().NotBeNull();
            resultDeletiom.Should().BeOfType<NoContentResult>();
        }


        [Test]
        [TestCase(1)]
        [TestCase(20)]
        [Parallelizable(ParallelScope.All)]
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
