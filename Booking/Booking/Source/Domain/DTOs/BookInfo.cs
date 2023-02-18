namespace Booking.Source.Domain.DTOs
{
    public class BookInfo
    {
        private readonly Random _random = new Random();

        public BookInfo(string? optionCode)
        {
            BookingCode = GenerateBookingCode(6);
            SleepTime = _random.Next(30, 60);

            BookingTime = DateTime.Now;
            OptionCode = optionCode;
        }


        public string BookingCode { get; set; }
        public string? OptionCode { get; set; }

        public DateTime BookingTime { get; set; }
        public int SleepTime { get; set; }

        public string GenerateBookingCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var charsList = Enumerable.Repeat(chars, length);

            var bookingCode = charsList.Select(s => s[_random.Next(chars.Length)])
                                       .ToArray();

            return new String(bookingCode);
        }
    }
}
