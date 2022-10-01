namespace HotelReservation.Logic
{
    public class RoomTypePricing
    {
        private RoomTypePricing()
        {

        }
        public int RoomTypePricingId { get; private set; }
        public int RoomTypeId { get; private set; }
        public decimal Price { get; private set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
