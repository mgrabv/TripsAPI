using Microsoft.AspNetCore.Mvc;
using RestAPI.DTOs;
using RestAPI.Services;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private RestService restService;

        public TripsController(RestService restService)
        {
            this.restService = restService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            return Ok(await restService.GetTrips());
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AddClientToTrip(CustomerTripDTO customerTripDTO, int idTrip)
        {
            if (!await restService.ClientExists(customerTripDTO.Pesel))
            {
                await restService.AddClient(customerTripDTO);
            }
            if (await restService.ClientSignedForTour(customerTripDTO.Pesel, customerTripDTO.IdTrip))
            {
                return StatusCode(409, "Given client is already signed up for this tour.");
            }
            if (!await restService.TripExists(customerTripDTO.IdTrip))
            {
                return StatusCode(404, "The given Trip does not exist.");
            }

            await restService.AddClientToTrip(customerTripDTO);
            return Ok();
        }
    }
}
