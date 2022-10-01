namespace HotelReservation.Logic
{
    public class RoomTypePricing
    {
        private RoomTypePricing()
        {

        }
        public int RoomTypePricingId { get; private set; }
        public int RoomTypeId { get; private set; }
        public int SeasonId { get; private set; }
        public decimal Price { get; private set; }
        public RoomType RoomType { get; set; }
        public Season Season { get; set; }
    }
}
