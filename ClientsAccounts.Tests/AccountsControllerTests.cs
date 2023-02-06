using ClientAccount.Controllers;
using ClientAccount.DataBase;
using ClientAccount.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace ClientsAccounts.Tests
{

    public class AccountsControllerTests
    {
        private Mock<IDBRepository> _dbRepository;
        private List<Client> clientAccountsTests;
        

        [SetUp]
        public void Setup()
        {
            System.Diagnostics.Debug.WriteLine("Setup");

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

        [Test]
        public void TestMethod2()
        {
            ClientsController clientsController = new ClientsController(_dbRepository.Object);
            var clinetModel = clientsController.RegisterClient(new RegisterClientModel() { Name = "Viktor", Patronymic = "Leonidovich", LastName = "Beliankin", BirthDate = DateTime.Now });
        }

        [Test]
        public async Task TestMethod3()
        {
            AccountsController accountsController = new AccountsController(_dbRepository.Object);
            Account account = new Account();
            DepositModel depositModel= new DepositModel() { SumInRubles= 50 };
            WithdrawModel withdrawModel = new WithdrawModel() { SumInRubles= 75 };

            var account1 =  accountsController.Deposit(1,depositModel);
            account1 =  accountsController.Deposit(1,depositModel);
            account1 =  accountsController.Deposit(1,depositModel);
            account1 =  accountsController.Deposit(1,depositModel);
            account1 =  accountsController.Withdraw(1, withdrawModel);

            int resultBalance = depositModel.SumInRubles + depositModel.SumInRubles + depositModel.SumInRubles + depositModel.SumInRubles - withdrawModel.SumInRubles;

            var client = await _dbRepository.Object.GetClients(1);
            int clientBalance = client.Account.Balance;

            Assert.AreEqual(resultBalance , clientBalance);
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
            Client user = new Client() { Id = id, Name = randomName, LastName = randomLastName, Patronymic = randomPatronymic, BirthDate = randomBirthday, Account = new Account { ClientId = id, Balance = 0 } };

            return user;
        }
    }
}
