using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("Create"), Authorize]
        public async Task<ActionResult<CreateRoomOfferResponse>> CreateRoomOffer(CreateRoomOfferRequest roomOffer)
        {
            try
            {
                var result = await _roomService.CreateOffer(roomOffer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while processing the room offer: {ex.Message}");
            }
        }

        [HttpPost("CreateReservation"), Authorize]
        public async Task<ActionResult<MakeReservationResponse>> MakeReservation(MakeReservationRequest reservationRequest)
        {
            try
            {
                var result = await _roomService.MakeReservation(reservationRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while processing the room offer: {ex.Message}");
            }
        }

        [HttpPost("CancelReservation"), Authorize]
        public async Task<ActionResult<bool>> CancelReservation(int reservationId)
        {
            try
            {
                var result = await _roomService.CancelReservation(reservationId);
                return Ok(result);

            } catch (Exception ex)
            {
                throw new Exception("En error occured when trying to invoke services", ex);
            }
        }

    }
}
