using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsController"/> class.
        /// </summary>
        /// <param name="roomService">The room service.</param>
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Creates a new room offer.
        /// </summary>
        /// <param name="roomOffer">The request to create a room offer.</param>
        /// <returns>The result of the room offer creation.</returns>
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

        /// <summary>
        /// Makes a reservation for a room.
        /// </summary>
        /// <param name="reservationRequest">The request to make a reservation.</param>
        /// <returns>The result of the reservation.</returns>
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

        /// <summary>
        /// Cancels a room reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to be canceled.</param>
        /// <returns>True if the reservation was canceled successfully; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while invoking services.</exception>
        [HttpPost("CancelReservation"), Authorize]
        public async Task<ActionResult<bool>> CancelReservation(int reservationId)
        {
            try
            {
                var result = await _roomService.CancelReservation(reservationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when trying to invoke services", ex);
            }
        }

        /// <summary>
        /// Gets a list of all rooms.
        /// </summary>
        /// <returns>A list of room details.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while invoking the service.</exception>
        [HttpGet("GetAllRooms")]
        public async Task<ActionResult<List<ListOfRoomsResponse>>> GetAllRooms()
        {
            try
            {
                var result = await _roomService.GetAllRooms();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An Error occurred while invoking the service", ex);
            }
        }

        /// <summary>
        /// Removes a room by its ID.
        /// </summary>
        /// <param name="roomId">The ID of the room to be removed.</param>
        /// <returns>True if the room was successfully removed; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while getting the data and invoking the service.</exception>
        [HttpDelete("RemoveItem"), Authorize]
        public async Task<ActionResult<bool>> RemoveRoom(int roomId)
        {
            try
            {
                var result = await _roomService.RemoveRoomById(roomId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the data and invoking the service", ex);
            }
        }

        /// <summary>
        /// Gets a list of rooms based on the specified home type.
        /// </summary>
        /// <param name="homeType">The type of home to filter rooms by.</param>
        /// <returns>A list of rooms matching the specified home type.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while invoking the service.</exception>
        [HttpGet("GetRoomByHomeType")]
        public async Task<ActionResult<List<ListOfRoomsResponse>>> GetRoomsByHomeType(HomeType homeType)
        {
            try
            {
                var result = await _roomService.GetRoomsByHomeType(homeType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while invoking service {ex.Message}");
            }
        }
    }
}
