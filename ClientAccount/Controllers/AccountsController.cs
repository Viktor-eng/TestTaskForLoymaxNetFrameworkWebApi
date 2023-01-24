using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClientAccount.DataBase;
using ClientAccount.Models;

namespace ClientAccount.Controllers
{
    public class AccountsController : ApiController
    {
        private ClientDB db = new ClientDB();

        [HttpGet]
        [ActionName("GetBalance")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetBalance(int id)
        {
            Client client = await db.Clients.Include(x => x.Account).FirstOrDefaultAsync(x => x.Id == id);

            if (client == null)
            {
                return NotFound();
            }
            return Ok(client.Account.Balance);
        }

        [HttpPost]
        [ActionName("Deposit")]
        [ResponseType(typeof(Account))]
        public async Task<IHttpActionResult> Deposit(int id, [FromBody]DepositModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await db.Clients.Include(x => x.Account).FirstOrDefaultAsync(x => x.Id == id);
            client.Account.Balance = client.Account.Balance + model.SumInRubles;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ActionName("Withdraw")]
        [ResponseType(typeof(Account))]
        public async Task<IHttpActionResult> Withdraw(int id, [FromBody]WithdrawModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await db.Clients.Include(x => x.Account).FirstOrDefaultAsync(x => x.Id == id);
            client.Account.Balance = client.Account.Balance - model.SumInRubles;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}