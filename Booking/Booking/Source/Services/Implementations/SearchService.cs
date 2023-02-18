using Booking.Source.Domain.DTOs;
using Booking.Source.Domain.Requests;
using Booking.Source.Services.Interfaces;
using System.Text.Json;

namespace Booking.Source.Services.Implementations
{
    public class SearchService
    {
        private readonly HttpClient _httpClient;
        private const string TRIPX_ROOT_API_URL = "https://tripx-test-functions.azurewebsites.net/api";
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        public SearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public IBookingSearch BookingSearch { get; set; }

        public async Task<List<Hotel>> GetHotelsAsync(SearchReq searchReq)
        {
            var hotelsJSONs = await _httpClient.GetStringAsync($"{TRIPX_ROOT_API_URL}/SearchHotels" +
                                                               $"?destinationCode={searchReq.Destination}");

            var hotels = JsonSerializer.Deserialize<List<Hotel>>(hotelsJSONs, jsonSerializerOptions);

            return hotels ?? new List<Hotel>();
        }

        public async Task<List<Flight>> GetFlightsAsync(SearchReq searchReq)
        {
            var flightsJSONs = await _httpClient.GetStringAsync($"{TRIPX_ROOT_API_URL}/SearchFlights" +
                                                                    $"?departureAirport={searchReq.DepartureAirport}" +
                                                                    $"&arrivalAirport={searchReq.Destination}");

            var flights = JsonSerializer.Deserialize<List<Flight>>(flightsJSONs, jsonSerializerOptions);

            return flights ?? new List<Flight>();
        }
    }
}
