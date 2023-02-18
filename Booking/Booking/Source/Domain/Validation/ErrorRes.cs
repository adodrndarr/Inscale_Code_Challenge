using Booking.Source.Domain.Responses;

namespace Booking.Source.Domain.Validation
{
    public class ErrorRes : Response
    {
        public string Message { get; set; }
    }
}
