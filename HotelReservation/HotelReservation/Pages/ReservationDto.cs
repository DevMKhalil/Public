using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Pages
{
    public class ReservationDto
    {
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please Select Room Type")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Room Type")]
        public int RoomTypeId { get; set; }
        [Required(ErrorMessage = "Please Select Meal Plan")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Meal Plan")]
        public int MealPlanId { get; set; }
        [Required(ErrorMessage = "Please Enter Adults Count")]
        [Range(0, int.MaxValue, ErrorMessage = "Please Enter Valid Adults Count")]
        public int AdultsCount { get; set; }
        [Required(ErrorMessage = "Please Enter Children Count")]
        [Range(0, int.MaxValue, ErrorMessage = "Please Enter Valid Children Count")]
        public int ChildrenCount { get; set; }
        [Required(ErrorMessage = "Please Enter Checkin Date")]
        public DateTime CheckinDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Please Enter Checkout Date")]
        public DateTime CheckoutDate { get; set; } = DateTime.Now;
        public decimal ReservationCost { get; set; }
    }
}
