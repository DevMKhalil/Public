using HotelReservation.Logic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Application.Reservation
{
    public class CalculateReservation : IRequest<decimal>
    {
        public int RoomType { get; set; }
        public int MealPlan { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public DateTime CheckinDate { get; set; } = DateTime.Now;
        public DateTime CheckoutDate { get; set; } = DateTime.Now;
    }

    public class CalculateReservationHandler : IRequestHandler<CalculateReservation, decimal>
    {
        private readonly IHotelReservationContext _context;

        public CalculateReservationHandler(IHotelReservationContext context)
        {
            _context = context;
        }

        public async Task<decimal> Handle(CalculateReservation request, CancellationToken cancellationToken)
        {
            var RoomTypePricingList = await _context.RoomTypePricings.Where(x => x.RoomTypeId == request.RoomType).ToListAsync();
            var MealPlanPricingList = await _context.MealPlanPricings.Where(x => x.MealPlanId == request.MealPlan).ToListAsync();

            return Logic.Reservation.GetReservationTotal(
                RoomTypePricingList,
                MealPlanPricingList,
                request.AdultsCount,
                request.ChildrenCount,
                request.CheckinDate,
                request.CheckoutDate);
        }
    }
}
