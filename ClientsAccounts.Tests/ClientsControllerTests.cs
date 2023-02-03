using ClientAccount.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientAccount.Controllers;
using ClientAccount.Interfaces;
using ClientAccount.Models;
using ClientAccount.DataBase;
using NSubstitute;
using System.Security.Cryptography.X509Certificates;

namespace ClientsAccounts.Tests
{
    public class ClientsControllerTests
    {
        IDBRepository _dbRepository;
        private RegisterClientModel registrUser;

        [SetUp]
        public void SetUp()
        {
            _dbRepository = Substitute.For<IDBRepository>();
            //var clientTest = new RegisterClientModel { Name = "Viktor", Patronymic = "Leonidovich", LastName = "Beliankin", BirthDate = DateTime.Now };
            //_dbRepository.AddOrUpdateClient(clientTest);

            _dbRepository.AddOrUpdateClient(Arg.Any<Client>()).ReturnsForAnyArgs(Task.FromResult);
        }

        [Test]
        public void TestMethod1()
        {
            ClientsController clientsController = new ClientsController(_dbRepository);
            Client client = new Client() {Id = 0, Name = "Viktor", Patronymic = "Leonidovich", LastName = "Beliankin", BirthDate = DateTime.Now };
            var clientModelTest = clientsController.RegisterClient(new RegisterClientModel { Name = "Viktor", Patronymic = "Leonidovich", LastName = "Beliankin", BirthDate = DateTime.Now }).Result;

            Assert.AreEqual(client, clientModelTest);
            
        }

        private RegisterClientModel RegisterUser()
        {
            Random random = new Random();

            string[] names = { "Viktor", "Maks", "Ivan", "Nikolay", "Evgeniy", "Dmitriy", "Konstantin" };

            string[] lastNames = { "Belyankin", "Chervakov", "Pogonichev", "Gutorov", "Samoylov", "Volkov", "Aksenov" };

            string[] patronymics = { "Leonidovish", "Vladimirovich", "Nikolaevich", "Sergeevich", "Dmitrievich", "Anatolievich", "Viktorovich" };

            DateTime[] birthDates = { new DateTime(1989, 05, 14),
                new DateTime(1989, 05, 14), new DateTime(1989, 05, 14),
                new DateTime(1989, 05, 14), new DateTime(1989, 05, 14),
                new DateTime(1989, 05, 14), new DateTime(1989, 05, 14) };

            int randomValueName = random.Next(0, names.Length);
            string randomName = names[randomValueName];

            int randomValueLastName = random.Next(0, lastNames.Length);
            string randomLastName = lastNames[randomValueLastName];

            int randomValuePatronymic = random.Next(0, patronymics.Length);
            string randomPatronymic = patronymics[randomValuePatronymic];

            int randomValueBirthDate = random.Next(0, birthDates.Length);
            DateTime randomBirthday = birthDates[randomValueBirthDate];
            RegisterClientModel user = new RegisterClientModel() { Name = randomName, LastName = randomLastName, Patronymic = randomPatronymic, BirthDate = randomBirthday };

            return user;
        }
    }
}
