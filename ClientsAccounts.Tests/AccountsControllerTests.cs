using ClientAccount.Controllers;
using ClientAccount.DataBase;
using ClientAccount.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

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
            Task deposit1 = Task.Factory.StartNew(() => { DepositClient(1, 50); });
            Task deposit2 = Task.Factory.StartNew(() => { DepositClient(1, 50); });
            Task deposit3 = Task.Factory.StartNew(() => { DepositClient(1, 50); });
            Task deposit4 = Task.Factory.StartNew(() => { DepositClient(1, 50); });
            Task deposit5 = Task.Factory.StartNew(() => { DepositClient(1, 50); });
            Task deposit6 = Task.Factory.StartNew(() => { DepositClient(1, 50); });
            Task deposit7 = Task.Factory.StartNew(() => { DepositClient(1, 50); });
            Task withdraw1 = Task.Factory.StartNew(() => { WithdrawClient(1, 75); });
            Task withdraw2 = Task.Factory.StartNew(() => { WithdrawClient(1, 75); });
            Task withdraw3 = Task.Factory.StartNew(() => { WithdrawClient(1, 75); });

            var client = await _dbRepository.Object.GetClients(1);
            int clientBalance = client.Account.Balance;

            int sumBalance = 125;

            Assert.AreEqual(clientBalance, sumBalance);
        }

        public void DepositClient (int id, int sum)
        {
            Thread thr = new Thread(() => { 
                DepositModel depositModel = new DepositModel() { SumInRubles = sum };
                AccountsController accountsController = new AccountsController(_dbRepository.Object);
                accountsController.Deposit(id, depositModel);
            });
            thr.Start();
        }

        public void WithdrawClient(int id, int sum)
        {
            Thread thr = new Thread(() => {
                WithdrawModel withdrawModel = new WithdrawModel() { SumInRubles = sum };
                AccountsController accountsController = new AccountsController(_dbRepository.Object);
                accountsController.Withdraw(id, withdrawModel);
            });
            thr.Start();
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
