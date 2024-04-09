using DDD.EscapeRoom.Core.DomainModelLayer.Events;
using DDD.EscapeRoom.Core.DomainModelLayer.Interfaces;
using DDD.EscapeRoom.Core.DomainModelLayer.Models;
using DDD.SharedKernel.ApplicationLayer;

namespace DDD.EscapeRoom.Core.ApplicationLayer.DomainEventListeners
{
    public class EnterToRoomWhenVisitStartedDomainEventHandler: IEventHandler<VisitStartedDomainEvent>
    {
        private IRoomRepository _roomRepository;

        public EnterToRoomWhenVisitStartedDomainEventHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void Handle(VisitStartedDomainEvent eventData)
        {
            // get room
            var room = _roomRepository.Get(eventData.Visit.RoomId);

            // set room as busy
            room.EnterTheRoom();
        }
    }
}
