using ECommerceAPI.Controller;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ECommerceAPI.xUnit.Testing
{
    public class OrderControllerTest
    {
        private OrderController _orderController;

        public OrderControllerTest()
        {
            var _orderRepoInterface = A.Fake<IOrderRepo>();
            var _productRepoInterface = A.Fake<IProductRepo>();
            _orderController = new OrderController(_orderRepoInterface, _productRepoInterface);
        }


        [Fact]
        public async Task GetAllOrders_returnsList()
        {
            //Arrange

            //Act
            var result = await _orderController.GetAllOrders();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<IEnumerable<Order>>));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(10)]
        public void GetOrderById_returnsProduct(int id)
        {
            //Arrange

            //Act
            var result = _orderController.GetOrderById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Order>>>();
        }




        [Fact]
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



        [Theory]
        [InlineData(1)]
        [InlineData(20)]
        public async Task DeleteOrder_checkForDeletion(int id)
        {
            //Arrange

            //Act
            var resultDeletiom = await _orderController.DeleteOrder(id);

            //Assert
            resultDeletiom.Should().NotBeNull();
            resultDeletiom.Should().BeOfType<NoContentResult>();
        }


        [Theory]
        [InlineData(1)]
        [InlineData(20)]
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