using GBC_Travel_Group_45.Data;
using GBC_Travel_Group_45.Filters;
using GBC_Travel_Group_45.Models;
using GBC_Travel_Group_45.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GBC_Travel_Group_45.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            var hotels = await _context.Hotels
                                       .Include(h => h.Images)
                                       .ToListAsync();
            return View(hotels);
        }



        // GET: Hotels/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Hotels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("HotelName,Location,SingleRoomsCount,DoubleRoomsCount,SingleRoomPrice,DoubleRoomPrice")] Hotel hotel, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();

                await UploadHotelImages(hotel.Id, images);

                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }
        // GET: Hotels/Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Hotel hotel, List<IFormFile> images)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(hotel);
                    await _context.SaveChangesAsync();


                    await UploadHotelImages(hotel.Id, images);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }


        // GET: Hotels/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Hotels/Book 
        [Authorize(Roles = "Admin,User")]
        [LoggingFilter("HotelBooking")]
        public async Task<IActionResult> Book()
        {
            var hotels = await _context.Hotels.ToListAsync();
            var hotelSelectList = hotels.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = $"{h.HotelName} - {h.Location}, Single Rooms: {h.SingleRoomsCount}, Double Rooms: {h.DoubleRoomsCount}, Single Room Price: ${h.SingleRoomPrice}, Double Room Price: ${h.DoubleRoomPrice}"
            }).ToList();

            ViewBag.Hotels = new SelectList(hotelSelectList, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggingFilter("HotelBooking")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Book(int hotelId, string guestFirstName, string guestLastName, int guestAge, string governmentId, DateTime bookingStart, DateTime bookingEnd, string roomType, int numberOfRooms)
        {




            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null)
            {
                return Content("Sorry, no hotel listed. Guests cannot make a booking.");
            }



            int totalNights = (bookingEnd - bookingStart).Days;

            totalNights = totalNights == 0 ? 1 : totalNights;

            decimal pricePerNight = roomType == "Single" ? hotel.SingleRoomPrice : hotel.DoubleRoomPrice;
            decimal totalPrice = pricePerNight * totalNights * numberOfRooms;

            var guest = await _context.Guests.FirstOrDefaultAsync(g => g.GovernmentId == governmentId);
            if (guest == null)
            {
                guest = new Guest { FirstName = guestFirstName, LastName = guestLastName, Age = guestAge, GovernmentId = governmentId };
                _context.Guests.Add(guest);
                await _context.SaveChangesAsync();
            }

            var booking = new HotelBooking
            {
                HotelId = hotelId,
                GuestId = guest.Id,
                BookingStart = bookingStart,
                BookingEnd = bookingEnd,
                RoomType = roomType,
                NumberOfRooms = numberOfRooms,
                TotalPrice = totalPrice
            };

            _context.HotelBookings.Add(booking);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(BookingConfirmation), new { id = booking.Id });
        }



        // GET: BookingConfirmation

        public async Task<IActionResult> BookingConfirmation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.HotelBookings
                .Include(b => b.Hotel)
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }


        // GET: HotelBookings
        // GET: Hotel/ViewBookings

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ViewBookings()
        {
            var bookings = await _context.HotelBookings
                .Include(b => b.Hotel)
                .Include(b => b.Guest)
                .ToListAsync();

            return View(bookings);
        }
        // GET: HotelBookings/EditBooking

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.HotelBookings
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        private bool BookingExists(int id)
        {
            return _context.HotelBookings.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditBooking(int id, HotelBooking updatedBooking)
        {
            if (id != updatedBooking.Id)
            {
                return NotFound();
            }

            var bookingToUpdate = await _context.HotelBookings
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookingToUpdate == null)
            {
                return NotFound("Booking not found.");
            }

            try
            {

                bookingToUpdate.BookingStart = updatedBooking.BookingStart;
                bookingToUpdate.BookingEnd = updatedBooking.BookingEnd;


                if (bookingToUpdate.Guest != null)
                {

                    bookingToUpdate.Guest.FirstName = updatedBooking.Guest.FirstName;
                    bookingToUpdate.Guest.LastName = updatedBooking.Guest.LastName;
                    bookingToUpdate.Guest.Age = updatedBooking.Guest.Age;
                    bookingToUpdate.Guest.GovernmentId = updatedBooking.Guest.GovernmentId;
                }
                else
                {

                    bookingToUpdate.Guest = new Guest
                    {
                        FirstName = updatedBooking.Guest.FirstName,
                        LastName = updatedBooking.Guest.LastName,
                        Age = updatedBooking.Guest.Age,
                        GovernmentId = updatedBooking.Guest.GovernmentId
                    };
                }


                _context.Update(bookingToUpdate);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(ViewBookings));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {

                    ModelState.AddModelError("", "Concurrency conflict occurred. Please try again.");
                    return View(updatedBooking);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"An error occurred while updating the booking: {ex.Message}");
                return View(updatedBooking);
            }
        }








        // GET: HotelBookings/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.HotelBookings
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBookingConfirmed(int id)
        {
            var booking = await _context.HotelBookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.HotelBookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewBookings));
        }


        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }



        private async Task UploadHotelImages(int hotelId, List<IFormFile> images)
        {
            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    _context.HotelImages.Add(new HotelImage { HotelId = hotelId, ImagePath = "/images/" + image.FileName });
                }
                await _context.SaveChangesAsync();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Search(string searchQuery)
        {
            var hotelsQuery = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                hotelsQuery = hotelsQuery.Where(h => h.HotelName.Contains(searchQuery) || h.Location.Contains(searchQuery));
            }

            var hotels = await hotelsQuery.Include(h => h.Images).ToListAsync();
            return PartialView("Index", hotels);
        }




    }

}

