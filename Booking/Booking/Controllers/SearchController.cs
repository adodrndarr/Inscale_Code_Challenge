using Booking.Source.Domain.Requests;
using Booking.Source.Responses;
using Booking.Source.Services.Implementations;
using Booking.Source.Services.Implementations.SearchTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Q: What if we have in next 45 days from date AND also have departure airport?
// should we do a lastMinuteHotels search or HotelAndFlight search?

namespace Booking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }


        [HttpGet]
        public async Task<SearchRes> SearchAsync([FromQuery] SearchReq searchReq)
        {
            var hasDepartureAirport = !string.IsNullOrWhiteSpace(searchReq.DepartureAirport);
            if (!hasDepartureAirport)
                _searchService.BookingSearch = new HotelOnly(_searchService);

            if (hasDepartureAirport)
                _searchService.BookingSearch = new HotelAndFlight(_searchService);


            var next45Days = DateTime.Now.AddDays(45);
            var isInNext45Days = searchReq.FromDate <= next45Days;

            if (isInNext45Days)
                _searchService.BookingSearch = new LastMinuteHotels(_searchService);


            return await _searchService.BookingSearch.SearchAsync(searchReq);
        }
    }
}
