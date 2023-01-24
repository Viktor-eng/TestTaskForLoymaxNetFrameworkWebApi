using ClientAccount.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ClientsAccounts.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {


            var controller =  new AccountsController();
            var result = await controller.GetBalance(3);
            Assert.AreEqual(1, result);
        }
    }
}
