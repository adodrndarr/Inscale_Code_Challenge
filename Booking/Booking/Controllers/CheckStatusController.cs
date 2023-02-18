using Booking.Source.Domain.Enums;
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
    public class CheckStatusController : ControllerBase
    {
        [HttpGet]
        public Response CheckStatus([FromQuery] CheckStatusReq checkStatusReq)
        {
            var checkStatusRes = new CheckStatusRes { Status = BookingStatusEnum.Pending };
            var booking = StoreService.BookInfos.FirstOrDefault(b => b.BookingCode == checkStatusReq.BookingCode);

            if (booking is null) 
                return new ErrorRes { Message = "Status check failed, invalid booking code."};

            var isCompleted = (DateTime.Now - booking.BookingTime).TotalSeconds >= booking.SleepTime;
            if (!isCompleted)
                return checkStatusRes;

            var bookingOption = StoreService.SearchResponses.SelectMany(sr => sr.Options)
                                                            .SingleOrDefault(o => o.OptionCode == booking.OptionCode);

            if (bookingOption is null)
                return new ErrorRes { Message = $"Status check failed, " +
                                                $"there is currently no valid option for the booking code {booking.BookingCode}." };


            switch (bookingOption.SearchType)
            {
                case SearchEnum.HotelOnly:
                case SearchEnum.HotelAndFlight:
                    checkStatusRes.Status = BookingStatusEnum.Success;
                    break;

                case SearchEnum.LastMinuteHotels:
                    checkStatusRes.Status = BookingStatusEnum.Failed;
                    break;
            }
           
            return checkStatusRes;
        }
    }
}
