using Booking.Source.Domain.DTOs;
using Booking.Source.Domain.Requests;
using Booking.Source.Domain.Responses;
using Booking.Source.Domain.Validation;
using Booking.Source.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        [HttpPost]
        public Response Book([FromBody] BookReq bookReq)
        {
            var bookingOption = StoreService.SearchResponses.SelectMany(sr => sr.Options)
                                                            .SingleOrDefault(o => o.OptionCode == bookReq.OptionCode);

            if (bookingOption is null)
                return new ErrorRes { Message = "Booking failed, invalid option code." };


            var bookInfo = new BookInfo(bookingOption.OptionCode);
            StoreService.BookInfos.Add(bookInfo);


            var bookRes = new BookRes
            {
                BookingCode = bookInfo.BookingCode,
                BookingTime = bookInfo.BookingTime
            };

            return bookRes;
        }
    }
}
