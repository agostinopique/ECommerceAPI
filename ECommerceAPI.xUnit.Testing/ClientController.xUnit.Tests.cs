using ECommerceAPI.Controller;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ECommerceAPI.xUnit.Testing
{
    public class ClientControllerTest
    {
        private ClientController _clientController;

        public ClientControllerTest()
        {
            var _clientRepoInterface = A.Fake<IClientRepo>();
            var _orerRepoInterface = A.Fake<IOrderRepo>();
            _clientController = new ClientController(_clientRepoInterface, _orerRepoInterface);
        }


        [Fact]
        public async Task GetAllClients_returnsListAsync()
        {
            //Arrange

            //Act
            var result = await _clientController.GetAllClients();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<IEnumerable<Client>>));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        //[InlineData(7)]
        //[InlineData(41)]
        //[InlineData(42)]
        //[InlineData(43)]
        public void GetClientById_returnsClient(int id)
        {
            //Arrange

            //Act
            var result = _clientController.GetClientById(id);

            //Assert
            //result.Result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Client>>>();
        }



        [Fact]
        public async Task CreateClient_checkForCreation()
        {
            //Arrange
            Client clt = new Client("Mario Rossi", "mario.rossi@gmail.com");

            //Act
            var result = await _clientController.CreateClient(clt);

            //Assert
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(20456)]
        public async Task DeleteClient_checkForDeletion(int id)
        {
            //Arrange

            //Act
            var resultDeletiom = await _clientController.DeleteClient(id);

            //Assert
            resultDeletiom.Should().NotBeNull();
            resultDeletiom.Should().BeOfType<NoContentResult>();
        }


        [Theory]
        [InlineData(1)]
        [InlineData(20898)]
        public async Task UpdateClient_returnPositiveAnswer(int id)
        {
            //Arrange
            var findClient = _clientController.GetClientById(id);
            Client clt = new Client("Mario Rossi", "mario.rossi@gmail.com");

            //Act
            var UpdatedClient = _clientController.UpdateClient(id, clt);

            //Assert
            UpdatedClient.Should().NotBeNull();
            UpdatedClient.Should().BeOfType<Task<ActionResult>>();
        }
    }
}