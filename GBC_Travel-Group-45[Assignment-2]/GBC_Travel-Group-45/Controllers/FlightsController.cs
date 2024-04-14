namespace GBC_Travel_Group_45.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using GBC_Travel_Group_45.Data;
    using GBC_Travel_Group_45.Models;
    using System.Threading.Tasks;
    using System.Linq;
    using GBC_Travel_Group_45.Data;
    using GBC_Travel_Group_45.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Authorization;
    using GBC_Travel_Group_45.Filters;

    [Authorize(Roles = "Admin,User")]
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            var flights = await _context.Flights.ToListAsync();
            return View(flights);
        }


        [Authorize(Roles = "Admin")]
        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]

        // POST: Flights/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FlightNumber,Name,Origin,Destination,DepartureTime,ArrivalTime,Price")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Flight created successfully" });
            }
            return Json(new { success = false, message = "Validation failed. Flight not created." });
        }



        // GET: Flights/Edit

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FlightNumber,Origin,Destination,DepartureTime,ArrivalTime,Price")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var flightToUpdate = await _context.Flights.FindAsync(id);
                    if (flightToUpdate == null)
                    {
                        return NotFound();
                    }

                    flightToUpdate.Name = flight.Name;
                    flightToUpdate.FlightNumber = flight.FlightNumber;
                    flightToUpdate.Origin = flight.Origin;
                    flightToUpdate.Destination = flight.Destination;
                    flightToUpdate.DepartureTime = flight.DepartureTime;
                    flightToUpdate.ArrivalTime = flight.ArrivalTime;
                    flightToUpdate.Price = flight.Price;

                    _context.Entry(flightToUpdate).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the flight. Please try again.");
                }
            }

            return View(flight);
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }



        // GET: Flights/Delete

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var flight = await _context.Flights.FindAsync(id);
                if (flight == null)
                {
                    return NotFound(); // Flight not found, return 404 Not Found
                }

                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return View("Error");
            }
        }





        // GET: Flights/Book 

        [Authorize(Roles = "Admin,User")]
        [LoggingFilter("FlightBooking")]
        public async Task<IActionResult> Book()
        {
            var flights = await _context.Flights.ToListAsync();
            var flightSelectList = flights.Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = $"{f.Name} - Flight {f.FlightNumber} - {f.Origin} to {f.Destination}, Depart: {f.DepartureTime.ToString("g")}, Arrive: {f.ArrivalTime.ToString("g")}, Price: ${f.Price}"
            }).ToList();

            ViewBag.Flights = new SelectList(flightSelectList, "Value", "Text");

            return View();
        }



        // POST: Flights/BookFlight 
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin,User")]
        [LoggingFilter("FlightBooking")]
        public async Task<IActionResult> BookFlight(int flightId, string firstName, string lastName, string email)
        {
            var passenger = await _context.Passengers.FirstOrDefaultAsync(p => p.Email == email);
            if (passenger == null)
            {
                passenger = new Passenger { FirstName = firstName, LastName = lastName, Email = email };
                _context.Passengers.Add(passenger);
                await _context.SaveChangesAsync();
            }


            var booking = new Booking
            {
                FlightId = flightId,
                PassengerId = passenger.Id,
                BookingDate = DateTime.Now
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(BookingSummary), new { bookingId = booking.Id });
        }


        // GET: Flights/BookingSummary
        public async Task<IActionResult> BookingSummary(int? bookingId)
        {
            if (bookingId == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
            .Include(b => b.Flight)
            .Include(b => b.Passenger)
            .FirstOrDefaultAsync(m => m.Id == bookingId);


            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }


        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ViewBooking()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Passenger)
                .ToListAsync();

            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            var flights = await _context.Flights.ToListAsync();
            ViewBag.Flights = new SelectList(flights, "Id", "FlightNumber", booking.FlightId);

            return View(booking);
        }


        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditBooking(int id, Booking updatedBooking)
        {
            if (id != updatedBooking.Id)
            {
                return NotFound();
            }

            var bookingToUpdate = await _context.Bookings
                .Include(b => b.Passenger)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookingToUpdate == null)
            {
                return NotFound("Booking not found.");
            }

            try
            {

                updatedBooking.BookingDate = bookingToUpdate.BookingDate;


                bookingToUpdate.FlightId = updatedBooking.FlightId;


                if (bookingToUpdate.Passenger != null)
                {

                    bookingToUpdate.Passenger.FirstName = updatedBooking.Passenger.FirstName;
                    bookingToUpdate.Passenger.LastName = updatedBooking.Passenger.LastName;
                    bookingToUpdate.Passenger.Email = updatedBooking.Passenger.Email;
                }
                else
                {

                    bookingToUpdate.Passenger = new Passenger
                    {
                        FirstName = updatedBooking.Passenger.FirstName,
                        LastName = updatedBooking.Passenger.LastName,
                        Email = updatedBooking.Passenger.Email
                    };
                }

                // Update the booking in the database
                _context.Update(bookingToUpdate);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(ViewBooking));
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
                    var flights = await _context.Flights.ToListAsync();
                    ViewBag.Flights = new SelectList(flights, "Id", "FlightNumber", updatedBooking.FlightId);
                    return View(updatedBooking);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while updating the booking.");
                var flights = await _context.Flights.ToListAsync();
                ViewBag.Flights = new SelectList(flights, "Id", "FlightNumber", updatedBooking.FlightId);
                return View(updatedBooking);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ViewBooking));
        }


        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Search(string searchQuery)
        {
            var flightsQuery = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                flightsQuery = flightsQuery.Where(f => f.Origin.Contains(searchQuery)
                    || f.Destination.Contains(searchQuery)
                    || f.FlightNumber.Contains(searchQuery)
                    || f.Name.Contains(searchQuery));
            }

            var flights = await flightsQuery.ToListAsync();
            return PartialView("Index", flights);
        }





    }

}
