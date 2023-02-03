using ClientAccount.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ClientAccount.Controllers;
using ClientAccount.Interfaces;
using System.Web.Http.Results;
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
        public async Task TestMethod1()
        {
            ClientsController clientsController = new ClientsController(_dbRepository);
            RegisterClientModel reg = new RegisterClientModel() {Name = "Viktor", Patronymic = "Leonidovich", LastName = "Beliankin", BirthDate = new DateTime(1989, 05, 14) };
            var getClientTestModel = await clientsController.RegisterClient(reg);
            var actualClient = (getClientTestModel as System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<Client>).Content;

            Assert.AreEqual(0, actualClient.Id);
            Assert.AreEqual(reg.Name, actualClient.Name);
            Assert.AreEqual(reg.Patronymic, actualClient.Patronymic);
            Assert.AreEqual(reg.BirthDate, actualClient.BirthDate);
            Assert.AreEqual(reg.LastName, actualClient.LastName);
        }
    }
}
