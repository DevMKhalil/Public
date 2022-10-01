using HotelReservation.Application.Reservation;
using HotelReservation.Data;
using HotelReservation.Logic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotelReservation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HotelReservationContext _context;
        private readonly IMediator _mediator;

        [BindProperty]
        public ReservationDto ReservationDto { get; set; } = new ReservationDto();

        public IndexModel(HotelReservationContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public IEnumerable<RoomType> RoomTypeList { get; set; } = Enumerable.Empty<RoomType>();
        public IEnumerable<MealPlan> MealPlanList { get; set; } = Enumerable.Empty<MealPlan>();
        public async Task<IActionResult> OnGet()
        {
            await GetDataSource();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var getResult = await _mediator.Send(new CalculateReservation()
                {
                    RoomType = this.ReservationDto.RoomTypeId,
                    MealPlan = this.ReservationDto.MealPlanId,
                    AdultsCount = this.ReservationDto.AdultsCount,
                    ChildrenCount = this.ReservationDto.ChildrenCount,
                    CheckinDate = this.ReservationDto.CheckinDate,
                    CheckoutDate = this.ReservationDto.CheckoutDate
                });

                ReservationDto.ReservationCost = getResult;
            }

            await GetDataSource();

            return Page();
        }

        private async Task GetDataSource()
        {
            RoomTypeList = await _context.RoomTypes.ToListAsync();
            MealPlanList = await _context.MealPlans.ToListAsync();
        }
    }
}