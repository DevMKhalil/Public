namespace HotelReservation.Logic
{
    public class Reservation
    {
        public static decimal GetReservationTotal(
            List<RoomTypePricing> roomTypePricingList,
            List<MealPlanPricing> mealPlanPricingList,
            int adultsCount,
            int childrenCount,
            DateTime checkinDate,
            DateTime checkoutDate)
        {
            decimal mealCost = CalculateMealCost(
                (adultsCount + childrenCount),
                mealPlanPricingList,
                checkinDate,
                checkoutDate);

            decimal roomCount = Math.Ceiling((decimal)(adultsCount > childrenCount ? adultsCount : childrenCount) / 2);

            decimal roomCost = CalculateRoomCost(
                roomCount,
                roomTypePricingList,
                checkinDate,
                checkoutDate);

            return mealCost + roomCost;
        }

        private static decimal CalculateMealCost(
            int personCount,
            List<MealPlanPricing> mealPlanPricingList,
            DateTime fromdate,
            DateTime toDate)
        {
            decimal mealCost = default(decimal);

            DateTime seasonStart = fromdate;

            foreach (var mealPlanPricing in mealPlanPricingList
                .Where(z => z.FromDate.Date <= toDate.Date && z.ToDate.Date >= fromdate.Date)
                .OrderBy(x => x.FromDate))
            {
                DateTime seasonEndDate = (toDate < mealPlanPricing.ToDate ? toDate : mealPlanPricing.ToDate);

                var daysCount = (seasonEndDate - seasonStart).TotalDays + 1;

                mealCost += (personCount * (decimal)daysCount * mealPlanPricing.Price);

                seasonStart = seasonEndDate.AddDays(1);
            }

            return mealCost;
        }

        private static decimal CalculateRoomCost(
            decimal roomCount,
            List<RoomTypePricing> roomPlanPricingList,
            DateTime fromdate,
            DateTime toDate)
        {
            decimal roomCost = default(decimal);

            DateTime seasonStart = fromdate;

            foreach (var roomPlanPricing in roomPlanPricingList
                .Where(z => z.FromDate.Date <= toDate.Date && z.ToDate.Date >= fromdate.Date)
                .OrderBy(x => x.FromDate))
            {
                DateTime seasonEndDate = (toDate < roomPlanPricing.ToDate ? toDate : roomPlanPricing.ToDate);

                var daysCount = (seasonEndDate - seasonStart).TotalDays + 1;

                roomCost += (roomCount * (decimal)daysCount * roomPlanPricing.Price);

                seasonStart = seasonEndDate.AddDays(1);
            }

            return roomCost;
        }
    }
}
