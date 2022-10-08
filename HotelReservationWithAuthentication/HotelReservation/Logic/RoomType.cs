using System.Collections.Generic;

namespace HotelReservationWithAuthentication.Logic
{
    public class RoomType
    {
        private RoomType()
        {

        }

        public int RoomTypeId { get; private set; }
        public string Name { get; private set; }
    }
}
