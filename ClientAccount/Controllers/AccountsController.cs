using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClientAccount.DataBase;
using ClientAccount.Interfaces;
using ClientAccount.Models;

namespace ClientAccount.Controllers
{
    public class AccountsController : ApiController
    {
        readonly IDBRepository _dbRepository = new DBRepository();


        public AccountsController(IDBRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }


        [HttpGet]
        [ActionName("GetBalance")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetBalance(int id)
        {
            Client client = await _dbRepository.GetClients(id);

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

            Client client = await _dbRepository.GetClients(id);
            client.Account.Balance = client.Account.Balance + model.SumInRubles;
            await _dbRepository.AddOrUpdateClient(client);
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

            Client client = await _dbRepository.GetClients(id);
            client.Account.Balance = client.Account.Balance - model.SumInRubles;
            await _dbRepository.AddOrUpdateClient(client);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
