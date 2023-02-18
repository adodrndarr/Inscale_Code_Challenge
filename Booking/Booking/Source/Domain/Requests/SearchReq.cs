using System.ComponentModel.DataAnnotations;

namespace Booking.Source.Domain.Requests
{
    public class SearchReq
    {
        [Required]
        public string Destination { get; set; } = string.Empty; // INFO: Ex. SKP, BCN, CPH

        public string? DepartureAirport { get; set; }

        [Required]
        public DateTime? FromDate { get; set; }

        [Required]
        public DateTime? ToDate { get; set; }
    }
}
