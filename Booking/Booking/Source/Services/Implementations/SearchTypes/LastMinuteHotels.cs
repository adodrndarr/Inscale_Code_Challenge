using Booking.Source.Domain.Enums;
using Booking.Source.Domain.Requests;
using Booking.Source.Responses;
using Booking.Source.Services.Interfaces;

namespace Booking.Source.Services.Implementations.SearchTypes
{
    public class LastMinuteHotels : IBookingSearch
    {
        private readonly SearchService _searchService;

        public LastMinuteHotels(SearchService searchService)
        {
            _searchService = searchService;
        }


        public async Task<SearchRes> SearchAsync(SearchReq searchReq)
        {
            var hotelOnlySearch = new HotelOnly(_searchService);
            hotelOnlySearch.SearchType = SearchEnum.LastMinuteHotels;

            return await hotelOnlySearch.SearchAsync(searchReq);
        }
    }
}
