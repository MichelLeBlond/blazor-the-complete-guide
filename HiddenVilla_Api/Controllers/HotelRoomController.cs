using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(string checkInDate = null, string checkOutDate = null)
        {
            if(string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameteres need to be suplied"
                });
            }
            if(!DateTime.TryParseExact(checkInDate,"MM/dd/yyyy",CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None,out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckOut date format. valid format will be MM/dd/yyyyy"
                });
            }

            var allRooms = await _hotelRoomRepository.GetAllHotelRooms(checkInDate,checkOutDate);
            return Ok(allRooms);
        }
        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId, string checkInDate = null, string checkOutDate = null)
        {
            if (roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameteres need to be suplied"
                });
            }
            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckOut date format. valid format will be MM/dd/yyyy"
                });
            }

            var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value, checkInDate,checkOutDate);
            if (roomDetails == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(roomDetails);

        }
    }
}

