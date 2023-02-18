﻿namespace Booking.Source.Domain.DTOs
{
    public class Hotel
    {
        public int Id { get; set; }
        public int HotelCode { get; set; }
        public string? HotelName { get; set; }
        public string? DestinationCode { get; set; }
        public string? City { get; set; }
    }
}
