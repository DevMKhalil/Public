using System;

namespace HotelReservation.Logic
{
    public class Season
    {
        private Season()
        {

        }
        public int SeasonId { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsMealSeason { get; private set; }
    }
}
