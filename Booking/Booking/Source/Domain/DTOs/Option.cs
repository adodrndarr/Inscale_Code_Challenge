using Booking.Source.Domain.Enums;

namespace Booking.Source.Domain.DTOs
{
    public class Option
    {
        public string? OptionCode { get; set; }
        public string? HotelCode { get; set; }
        public string? FlightCode { get; set; }

        public string? ArrivalAirport { get; set; }
        public double Price { get; set; } = new Random().Next(100, 200); // INFO: Couldn't find price from API, so generating random one..
        internal SearchEnum SearchType { get; set; }
    }
}
