using JayrideCodeChallengeAPI.Dto;
using JayrideCodeChallengeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JayrideCodeChallengeAPI.Controllers
{
    [ApiController]
    public class TaskController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TaskController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("/candidate")]
        public ActionResult<Candidate> Candidate() => new Candidate { Name = "test", Phone = "test" };

        [HttpGet]
        [Route("/Location")]
        public async Task<ActionResult> GetLocationFromIpAsync(string ip, CancellationToken cancellationToken)
        {
            string APIKEY = "2ff11da961af44d996133235e7b3f9a9";
            IpLocationResponse? ipLocationResponse = new IpLocationResponse();

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.GetAsync($"https://api.ipgeolocation.io/ipgeo?apiKey={APIKEY}&ip={ip}", cancellationToken);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                ipLocationResponse = await JsonSerializer.DeserializeAsync<IpLocationResponse>(contentStream);
                if (ipLocationResponse is null)
                    return BadRequest();
            }
            else
                return BadRequest();

            return Ok(ipLocationResponse.City);
        }

        [HttpGet]
        [Route("/Listings")]
        public async Task<ActionResult<IEnumerable<ListingDto>>> GetListingsTotalPriceByPassengers(int noOfPassengers, CancellationToken cancellationToken)
        {
            List<ListingDto> listingsResponseDto = new List<ListingDto>();
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.GetAsync($"https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest", cancellationToken);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var listingsResponse = await JsonSerializer.DeserializeAsync<ListingsResponse>(contentStream);
                if (listingsResponse is not null)
                {
                    var filteredListings = listingsResponse.listings.Where(x => x.vehicleType.maxPassengers == noOfPassengers)
                        .ToList();

                    if (filteredListings.Any())
                    {
                        listingsResponseDto = filteredListings
                            .Select(x => new ListingDto
                            {
                                TotalPrice = x.pricePerPassenger * x.vehicleType.maxPassengers,
                                name = x.name,
                                pricePerPassenger = x.pricePerPassenger,
                                vehicleType = new Dto.VehicleType { name = x.vehicleType.name, maxPassengers = x.vehicleType.maxPassengers }
                            })
                            .OrderBy(x=>x.TotalPrice)
                            .ToList();
                    }
                }
            }
            else
                return BadRequest();

            return listingsResponseDto;
        }
    }
}
