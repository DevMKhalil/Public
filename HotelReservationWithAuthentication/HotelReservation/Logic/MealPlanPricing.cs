namespace HotelReservationWithAuthentication.Logic
{
    public class MealPlanPricing
    {
        private MealPlanPricing()
        {

        }
        public int MealPlanPricingId { get; private set; }
        public int MealPlanId { get; private set; }
        public decimal Price { get; private set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
