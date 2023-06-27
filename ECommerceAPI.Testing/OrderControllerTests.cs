using ECommerceAPI.Controller;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Testing
{
    public class OrderControllerTests
    {
        private OrderController _orderController;


        [SetUp]
        public void Setup()
        {
            var _orderRepoInterface = A.Fake<IOrderRepo>();
            var _productRepoInterface = A.Fake<IProductRepo>();
            _orderController = new OrderController(_orderRepoInterface, _productRepoInterface);
        }


        [Test]
        public async Task GetAllOrders_returnsList()
        {
            //Arrange

            //Act
            var result = await _orderController.GetAllOrders();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<IEnumerable<Order>>));
        }


        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(10)]
        [Parallelizable(ParallelScope.All)]
        public void GetOrderById_returnsProduct(int id)
        {
            //Arrange

            //Act
            var result = _orderController.GetOrderById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Order>>>();
        }

        [Test]
        public async Task CreateOrder_checkForCreation()
        {
            //Arrange
            Order ord = new Order(2, 256.99, 2);

            //Act
            var result = await _orderController.CreateOrder(ord);

            //Assert
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType<CreatedAtRouteResult>();
        }


        [Test]
        [TestCase(1)]
        [TestCase(20)]
        [Parallelizable(ParallelScope.All)]
        public async Task DeleteOrder_checkForDeletion(int id)
        {
            //Arrange

            //Act
            var resultDeletiom = await _orderController.DeleteOrder(id);

            //Assert
            resultDeletiom.Should().NotBeNull();
            resultDeletiom.Should().BeOfType<NoContentResult>();
        }


        [Test]
        [TestCase(1)]
        [TestCase(20)]
        [Parallelizable(ParallelScope.All)]
        public async Task UpdateOrder_returnPositiveAnswer(int id)
        {
            //Arrange
            var findProduct = _orderController.GetOrderById(id);
            Order ord = new Order(2, 256.99, 2);

            //Act
            var UpdatedProduct = _orderController.UpdateOrder(id, ord);

            //Assert
            UpdatedProduct.Should().NotBeNull();
            UpdatedProduct.Should().BeOfType<Task<ActionResult>>();
        }
    }
}
