using Booking.Source.Domain.DTOs;
using Booking.Source.Domain.Enums;
using Booking.Source.Domain.Requests;
using Booking.Source.Responses;
using Booking.Source.Services.Interfaces;

namespace Booking.Source.Services.Implementations.SearchTypes
{
    public class HotelOnly : IBookingSearch
    {
        private readonly SearchService _searchService;

        public HotelOnly(SearchService searchService)
        {
            _searchService = searchService;
        }


        public SearchEnum SearchType { get; set; }

        public async Task<SearchRes> SearchAsync(SearchReq searchReq)
        {
            var searchRes = new SearchRes();
            var hotels = await _searchService.GetHotelsAsync(searchReq);

            foreach (var hotel in hotels)
            {
                var option = new Option
                {
                    OptionCode = $"{Guid.NewGuid()}",
                    HotelCode = $"{hotel.HotelCode}",
                    ArrivalAirport = searchReq.Destination,
                    SearchType = SearchType
                };

                searchRes.Options.Add(option);
            }

            StoreService.SearchResponses.Add(searchRes);
            return searchRes;
        }
    }
}
