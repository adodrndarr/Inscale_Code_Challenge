namespace Booking.Source.Domain.Responses
{
    public class BookRes : Response
    {
        public string? BookingCode { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
