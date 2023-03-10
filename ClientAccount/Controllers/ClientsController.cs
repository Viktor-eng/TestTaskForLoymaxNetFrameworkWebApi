using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClientAccount.DataBase;
using ClientAccount.Models;

namespace ClientAccount.Controllers
{
    public class ClientsController : ApiController
    {
        readonly IDBRepository _dbRepository = new DBRepository();


        public ClientsController(IDBRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }


        [HttpPost]
        [ActionName("RegisterClient")]
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> RegisterClient([FromBody] RegisterClientModel registerClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = new Client
            {
                Account = new Account(),

                Name = registerClient.Name,
                LastName = registerClient.LastName,
                Patronymic = registerClient.Patronymic,
                BirthDate = registerClient.BirthDate
            };

            await _dbRepository.AddClient(client);

            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
        }
    }
}
