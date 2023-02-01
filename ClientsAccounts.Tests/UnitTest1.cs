using ClientAccount.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace ClientsAccounts.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            Random random = new Random();

            string[] names = { "Viktor", "Maks", "Ivan", "Nikolay", "Evgeniy", "Dmitriy", "Konstantin" };

            string[] lastNames = { "Belyankin", "Chervakov", "Pogonichev", "Gutorov", "Samoylov", "Volkov", "Aksenov" };

            string[] patronymics = { "Leonidovish", "Vladimirovich", "Nikolaevich", "Sergeevich", "Dmitrievich", "Anatolievich", "Viktorovich" };

            DateTime[] birthDates = { new DateTime(1989, 05, 14), 
                new DateTime(1989, 05, 14), new DateTime(1989, 05, 14), 
                new DateTime(1989, 05, 14), new DateTime(1989, 05, 14), 
                new DateTime(1989, 05, 14), new DateTime(1989, 05, 14) };

            RegisterClientModel GenerateUser()
            {
                int randomValueName = random.Next(0, names.Length);
                string randomName = names[randomValueName];

                int randomValueLastName = random.Next(0, lastNames.Length);
                string randomLastName = lastNames[randomValueLastName];

                int randomValuePatronymic = random.Next(0, patronymics.Length);
                string randomPatronymic = patronymics[randomValuePatronymic];

                int randomValueBirthDate = random.Next(0, birthDates.Length);
                DateTime randomBirthday= birthDates[randomValueBirthDate];
                RegisterClientModel user = new RegisterClientModel() { Name = randomName, LastName = randomLastName, Patronymic = randomPatronymic, BirthDate = randomBirthday };

                return user;
            }

            List<RegisterClientModel> clientAccountsTests = Enumerable.Range(0, 51).Select(x => GenerateUser()).ToList();

            var mock = new Mock<IClient>();
            var sut = new ControllerServices(mock.Object);
        }
    }
}
