using Booking.Source.Domain.DTOs;

namespace Booking.Source.Responses
{
    public class SearchRes
    {
        public List<Option> Options { get; set; } = new List<Option>();
    }
}
