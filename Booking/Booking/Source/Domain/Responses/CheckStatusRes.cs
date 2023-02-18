using Booking.Source.Domain.Enums;

namespace Booking.Source.Domain.Responses
{
    public class CheckStatusRes : Response
    {
        public BookingStatusEnum Status { get; set; }
    }
}
