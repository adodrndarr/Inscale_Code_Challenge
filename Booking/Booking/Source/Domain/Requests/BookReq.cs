using System.ComponentModel.DataAnnotations;

namespace Booking.Source.Domain.Requests
{
    public class BookReq
    {
        [Required]
        public string OptionCode { get; set; } = string.Empty;

        [Required]
        public SearchReq? SearchReq { get; set; }
    }
}
