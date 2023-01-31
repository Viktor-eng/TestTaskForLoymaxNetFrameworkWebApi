using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClientAccount.DataBase;
using ClientAccount.Models;

namespace ClientAccount.Controllers
{
    public class ClientsController : ApiController
    {
        private ClientDB db = new ClientDB();

        [HttpPost]
        [ActionName("RegisterClient")]
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> RegisterClient([FromBody]RegisterClientModel registerClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = new Client();
            client.Account = new Account();

            client.Name = registerClient.Name;
            client.LastName = registerClient.LastName;
            client.Patronymic = registerClient.Patronymic;
            client.BirthDate = registerClient.BirthDate;

            db.Clients.Add(client);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
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