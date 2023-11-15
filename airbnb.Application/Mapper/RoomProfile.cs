using AutoMapper;
using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;

namespace airbnb.Application.Mapper
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<CreateRoomOfferRequest, Room>();
            CreateMap<Room, CreateRoomOfferResponse>();
        }
    }
}
