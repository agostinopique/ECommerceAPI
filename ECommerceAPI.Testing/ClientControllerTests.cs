using ECommerceAPI;
using ECommerceAPI.Controller;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace ECommerceAPI.Testing
{
    [TestFixture]
    public class ClientControllerTests
    {
        private ClientController _clientController;


        [SetUp]
        public void Setup()
        {
            var _clientRepoInterface = A.Fake<IClientRepo>();
            var _orerRepoInterface = A.Fake<IOrderRepo>();
            _clientController = new ClientController(_clientRepoInterface, _orerRepoInterface);
        }


        [Test]
        public async Task GetAllClients_returnsListAsync()
        {
            //Arrange

            //Act
            var result = await _clientController.GetAllClients();
            
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<IEnumerable<Client>>));
        }


        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(10)]
        [Parallelizable(ParallelScope.All)]
        public void GetClientById_returnsClient(int id)
        {
            //Arrange
            
            //Act
            var result = _clientController.GetClientById(id);

            //Assert
            //result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Client>>>();
        }


        [Test]
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


        [Test]
        [TestCase(1)]
        [TestCase(20)]
        [Parallelizable(ParallelScope.All)]
        public async Task DeleteClient_checkForDeletion(int id)
        {
            //Arrange

            //Act
            var resultDeletiom = await _clientController.DeleteClient(id);

            //Assert
            resultDeletiom.Should().NotBeNull();
            resultDeletiom.Should().BeOfType<NoContentResult>();
        }


        [Test]
        [TestCase(1)]
        [TestCase(20)]
        [Parallelizable(ParallelScope.All)]
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