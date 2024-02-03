using Microsoft.AspNetCore.Mvc;
using RestAPI.Services;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private RestService restService;

        public ClientController(RestService restService)
        {
            this.restService = restService;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            if (!await restService.ClientExists(idClient))
            {
                return StatusCode(204, "Client with the given ID does not exist.");
            }
            if (await restService.ClientHasTours(idClient))
            {
                return StatusCode(409, "Client with the given ID has some tours assigned, he/she cannot be deleted.");
            }

            await restService.DeleteClient(idClient);
            return Ok();
        }
    }
}
