﻿using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> CreateOffer(CreateRoomOfferRequest offer);
    }
}
