using HotelReservation;
using HotelReservation.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservation2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HotelReservationContext _context;

        public IndexModel(HotelReservationContext context) => _context = context;

        public List<RoomType> RoomTypes { get; set; } = new List<RoomType>();

        public void OnGet()
        {
            RoomTypes = _context.RoomTypes.ToList();
        }
    }
}
