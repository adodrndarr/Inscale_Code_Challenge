using Booking.Source.Domain.DTOs;
using Booking.Source.Domain.Enums;
using Booking.Source.Domain.Requests;
using Booking.Source.Responses;
using Booking.Source.Services.Interfaces;

namespace Booking.Source.Services.Implementations.SearchTypes
{
    public class HotelAndFlight : IBookingSearch
    {
        private readonly SearchService _searchService;

        public HotelAndFlight(SearchService searchService)
        {
            _searchService = searchService;
        }


        public async Task<SearchRes> SearchAsync(SearchReq searchReq)
        {
            var searchRes = new SearchRes();
            var hotels = await _searchService.GetHotelsAsync(searchReq);
            var flights = await _searchService.GetFlightsAsync(searchReq);

            foreach (var flight in flights)
            {
                foreach (var hotel in hotels)
                {
                    var option = new Option
                    {
                        OptionCode = $"{Guid.NewGuid()}",
                        HotelCode = $"{hotel.HotelCode}",
                        FlightCode = $"{flight.FlightCode}",

                        ArrivalAirport = flight.ArrivalAirport,
                        SearchType = SearchEnum.HotelAndFlight
                    };

                    searchRes.Options.Add(option);
                }
            }

            StoreService.SearchResponses.Add(searchRes);
            return searchRes;
        }
    }
}
