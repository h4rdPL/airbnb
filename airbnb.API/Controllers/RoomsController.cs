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
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomOffer"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="reservationRequest"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
   
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("GetAllRooms")]
        public async Task<ActionResult<List<ListOfRoomsResponse>>> GetAllRooms()
        {
            try
            {
                var result = await _roomService.GetAllRooms();
                return Ok(result);
            } catch (Exception ex)
            {
                throw new Exception("En Error occured while invoke service", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomId">Id of the individual room</param>
        /// <returns>True of false based on the logic</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete("RemoveItem"), Authorize]
        public async Task<ActionResult<bool>> RemoveRoom(int roomId)
        {
            try
            {
                var result = await _roomService.RemoveRoomById(roomId);
                return Ok(result);
            } catch (Exception ex)
            {
                throw new Exception("An error occured while get the data and invoke service", ex);
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
                throw new Exception($"An error occurred while invoke service {ex.Message}");
            }
        }
    }
}
