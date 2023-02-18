using System.ComponentModel.DataAnnotations;

namespace Booking.Source.Domain.Requests
{
    public class CheckStatusReq
    {
        [Required]
        public string BookingCode { get; set; } = string.Empty;
    }
}
