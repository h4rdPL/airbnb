using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public RoomsController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<CreateRoomOfferResponse>> CreateRoomOffer(CreateRoomOfferRequest roomOffer)
        {
            try
            {
                var result = await _roomService.CreateOffer(roomOffer);
                var response = _mapper.Map<CreateRoomOfferResponse>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }



    }
}
