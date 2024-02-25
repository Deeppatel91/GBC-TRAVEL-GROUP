using GBC_Travel_Group_45.Data;
using GBC_Travel_Group_45.Models;
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
    public class CarRentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarRentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Car.Include(c => c.CarImages).ToListAsync();
            return View(cars);
        }





        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Make,Model,Year,Color,NumOfSeats,PricePerDay,CarType")] Car car, List<IFormFile> carImages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();

                await UploadCarImages(car.Id, carImages);

                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }


        private async Task UploadCarImages(int carId, List<IFormFile> images)
        {
            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/cars", image.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    var carImage = new CarImage { CarId = carId, ImagePath = "/images/cars/" + image.FileName };
                    _context.CarImage.Add(carImage); 
                }
                await _context.SaveChangesAsync(); 
            }
        }
        // GET: Cars/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Make,Model,Year,Color,NumOfSeats,PricePerDay,CarType")] Car car, List<IFormFile> carImages)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();

                    if (carImages != null && carImages.Count > 0)
                    {
                        await UploadCarImages(car.Id, carImages);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(car);
        }
        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.CarImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }







        public async Task<IActionResult> Book()
        {
            var cars = await _context.Car.ToListAsync();
            var carSelectList = cars.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Make} {c.Model} - Year: {c.Year}, Seats: {c.NumOfSeats}, Price Per Day: ${c.PricePerDay}, Type: {c.CarType}"
            }).ToList();

            ViewBag.Cars = new SelectList(carSelectList, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int carId, string guestFirstName, string guestLastName, int guestAge, string governmentId, DateTime bookingStart, DateTime bookingEnd, string pickupLocation, string dropLocation, string email, string phoneNumber)
        {
            

            var car = await _context.Car.FindAsync(carId);
            if (car == null)
            {
                return Content("Sorry, the selected car is not available for booking.");
            }

            
            decimal totalPrice = CalculateTotalPrice(car.PricePerDay, bookingStart, bookingEnd);

           
            var guest = await _context.Customer.FirstOrDefaultAsync(g => g.GovernmentId == governmentId);
            if (guest == null)
            {
                guest = new Customer { FirstName = guestFirstName, LastName = guestLastName, Age = guestAge, GovernmentId = governmentId };
                _context.Customer.Add(guest);
                await _context.SaveChangesAsync();
            }

           
            var booking = new CarBooking
            {
                CarId = carId,
                CustomerId = guest.Id,
                BookingStart = bookingStart,
                BookingEnd = bookingEnd,
                PickupLocation = pickupLocation,
                DropLocation = dropLocation,
                Email = email,
                PhoneNumber = phoneNumber,
                TotalPrice = totalPrice
            };

            _context.CarBooking.Add(booking);
            await _context.SaveChangesAsync();

            
            return RedirectToAction(nameof(BookingConfirmation), new { id = booking.Id });
        }

       

        private decimal CalculateTotalPrice(decimal pricePerDay, DateTime start, DateTime end)
        {
            if((end - start).Days == 0)
            {
                return pricePerDay;
            }
            else
            {
                int totalDays = (end - start).Days;
                return pricePerDay * totalDays;
            }
        }


        public async Task<IActionResult> BookingConfirmation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carBooking = await _context.CarBooking
                .Include(cb => cb.Car)
                .Include(cb => cb.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (carBooking == null)
            {
                return NotFound();
            }

            return View(carBooking);
        }




        // GET: CarRental/ViewBookings
        public async Task<IActionResult> ViewBookings()
        {
            var bookings = await _context.CarBooking
                                          .Include(cb => cb.Car)
                                          .Include(cb => cb.Customer)
                                          .ToListAsync();
            return View(bookings);
        }
        // GET: CarRental/EditBooking
        public async Task<IActionResult> EditBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.CarBooking
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBooking(int id, CarBooking updatedBooking)
        {
            if (id != updatedBooking.Id)
            {
                return NotFound();
            }

            var bookingToUpdate = await _context.CarBooking
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookingToUpdate == null)
            {
                return NotFound("Car booking not found.");
            }

            try
            {
               
                bookingToUpdate.BookingStart = updatedBooking.BookingStart;
                bookingToUpdate.BookingEnd = updatedBooking.BookingEnd;
                bookingToUpdate.PickupLocation = updatedBooking.PickupLocation;
                bookingToUpdate.DropLocation = updatedBooking.DropLocation;
                bookingToUpdate.Email = updatedBooking.Email;
                bookingToUpdate.PhoneNumber = updatedBooking.PhoneNumber;

                
                if (bookingToUpdate.Customer != null)
                {
                    
                    bookingToUpdate.Customer.FirstName = updatedBooking.Customer.FirstName;
                    bookingToUpdate.Customer.LastName = updatedBooking.Customer.LastName;
                  
                }
                else
                {
                    
                    bookingToUpdate.Customer = new Customer
                    {
                        FirstName = updatedBooking.Customer.FirstName,
                        LastName = updatedBooking.Customer.LastName,
                        
                    };
                }

               
                _context.Update(bookingToUpdate);
                await _context.SaveChangesAsync();

                
                return RedirectToAction(nameof(ViewBookings));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarBookingExists(id))
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
               
                ModelState.AddModelError("", $"An error occurred while updating the car booking: {ex.Message}");
                return View(updatedBooking);
            }
        }

        private bool CarBookingExists(int id)
        {
            return _context.CarBooking.Any(e => e.Id == id);
        }
        // GET: CarRental/DeleteBooking
        public async Task<IActionResult> DeleteBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.CarBooking
                                        .Include(cb => cb.Car)
                                        .Include(cb => cb.Customer)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost, ActionName("DeleteBooking")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBookingConfirmed(int id)
        {
            var booking = await _context.CarBooking.FindAsync(id);
            _context.CarBooking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewBookings));
        }


       
        public async Task<IActionResult> Search(string searchQuery)
        {
            var carsQuery = _context.Car.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                
                carsQuery = carsQuery.Where(c => c.Make.Contains(searchQuery)
                                                 || c.Model.Contains(searchQuery)
                                                 || c.CarType.Contains(searchQuery)
                                                 || c.Year.ToString().Contains(searchQuery)
                                                 || c.NumOfSeats.ToString().Contains(searchQuery)
                                                 || c.Color.Contains(searchQuery));
            }

            var cars = await carsQuery.Include(c => c.CarImages).ToListAsync();

            return View("~/Views/CarRental/Index.cshtml", cars);
        }


    }
}
