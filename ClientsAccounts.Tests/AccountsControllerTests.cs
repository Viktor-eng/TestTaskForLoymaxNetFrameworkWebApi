using ClientAccount.Controllers;
using ClientAccount.DataBase;
using ClientAccount.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsAccounts.Tests
{
    public class AccountsControllerTests
    {
        private Mock<IDBRepository> _dbRepository;
        private List<Client> _clientAccountsTests;
        private AccountsController _accountsController;

        [SetUp]
        public void Setup()
        {
            _dbRepository = new Mock<IDBRepository>();
            _clientAccountsTests = Enumerable.Range(0, 51).Select(x => GenerateUser(x)).ToList();
            _dbRepository.Setup(x => x.GetClients(It.IsAny<int>())).Returns((int x) => Task.FromResult(_clientAccountsTests.FirstOrDefault(c => c.Id == x)));
            _accountsController = new AccountsController(_dbRepository.Object);
        }

        [Test]
        public void IsNotNullDataBaseMoqTest()
        {
            var client = _dbRepository.Object.GetClients(5);
            Assert.IsNotNull(client);
        }

        [Test]
        public async Task DepositAndWithdrawInThreadTest()
        {
            int value = 10;

            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = DepositClientAsync(1, value);
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = WithdrawClientAsync(1, value);
            }

            Task.WhenAll(tasks).Wait();

            var client = await _dbRepository.Object.GetClients(1);
            int clientBalance = client.Account.Balance;

            Assert.AreEqual(clientBalance, 0);
        }

        public Task DepositClientAsync(int id, int value)
        {
            DepositModel depositModel = new DepositModel() { SumInRubles = value };
             return _accountsController.Deposit(id, depositModel);
        }

        public Task WithdrawClientAsync(int id, int value)
        {
            WithdrawModel withdrawModel = new WithdrawModel() { SumInRubles = value };
            return _accountsController.Withdraw(id, withdrawModel);
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
