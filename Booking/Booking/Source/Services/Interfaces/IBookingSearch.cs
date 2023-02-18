using Booking.Source.Domain.Requests;
using Booking.Source.Responses;

namespace Booking.Source.Services.Interfaces
{
    public interface IBookingSearch
    {
        Task<SearchRes> SearchAsync(SearchReq searchReq);
    }
}
