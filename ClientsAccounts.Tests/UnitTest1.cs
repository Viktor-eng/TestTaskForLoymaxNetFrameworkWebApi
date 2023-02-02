using ClientAccount.Interfaces;
using ClientAccount.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsAccounts.Tests
{
    
    public class UnitTest1
    {
        private Mock<IDBRepository> _dbRepository;
        private List<Client> clientAccountsTests;

        [SetUp]
        public void Setup()
        {
            _dbRepository = new Mock<IDBRepository>();
            clientAccountsTests = Enumerable.Range(0, 51).Select(x => GenerateUser(x)).ToList();
            _dbRepository.Setup(x => x.GetClients(It.IsAny<int>())).Returns((int x) => Task.FromResult(clientAccountsTests.FirstOrDefault(c => c.Id == x)));
        }

        [Test]
        public void TestMethod1()
        {
            var client = _dbRepository.Object.GetClients(5);

            Assert.IsNotNull(client);
        }

        private Client GenerateUser(int id)
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
            Client user = new Client() { Id = id, Name = randomName, LastName = randomLastName, Patronymic = randomPatronymic, BirthDate = randomBirthday };

            return user;
        }

    }
}
