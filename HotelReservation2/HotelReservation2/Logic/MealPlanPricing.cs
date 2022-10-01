namespace HotelReservation.Logic
{
    public class MealPlanPricing
    {
        private MealPlanPricing()
        {

        }
        public int MealPlanPricingId { get; private set; }
        public int MealPlanId { get; private set; }
        public int SeasonId { get; private set; }
        public decimal Price { get; private set; }
        public MealPlan MealPlan { get; set; }
        public Season Season { get; set; }
    }
}
