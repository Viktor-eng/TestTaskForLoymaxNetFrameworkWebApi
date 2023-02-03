using ClientAccount.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ClientAccount.Controllers;
using ClientAccount.Interfaces;
using NSubstitute;

namespace ClientsAccounts.Tests
{
    public class ClientsControllerTests
    {
        IDBRepository _dbRepository;

        [SetUp]
        public void SetUp()
        {
            _dbRepository = Substitute.For<IDBRepository>();
            _dbRepository.AddOrUpdateClient(Arg.Any<Client>()).ReturnsForAnyArgs(Task.FromResult);
        }

        [Test]
        public void TestMethod1()
        {
            ClientsController clientsController = new ClientsController(_dbRepository);
            Client client = new Client() {Id = 0, Name = "Viktor", Patronymic = "Leonidovich", LastName = "Beliankin", BirthDate = DateTime.Now };
            var clientModelTest = clientsController.RegisterClient(new RegisterClientModel { Name = "Viktor", Patronymic = "Leonidovich", LastName = "Beliankin", BirthDate = DateTime.Now });

            Assert.AreEqual(client, clientModelTest);
            
        }
    }
}
