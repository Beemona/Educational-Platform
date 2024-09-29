using Microsoft.AspNetCore.Mvc;
using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booking.Controllers
{
    public class BookingController : Controller
    {
        // Hardcoded data
        private readonly List<Department> _departments = new List<Department>
        {
            new Department { DepartmentId = 1, DepartmentName = "Mathematics" },
            new Department { DepartmentId = 2, DepartmentName = "Physics" },
            new Department { DepartmentId = 3, DepartmentName = "Computer Science" }
        };

        private readonly List<Teacher> _teachers = new List<Teacher>
        {
            new Teacher { TeacherId = 1, Name = "Alice Smith", DepartmentId = 1 },
            new Teacher { TeacherId = 2, Name = "Bob Johnson", DepartmentId = 2 },
            new Teacher { TeacherId = 3, Name = "Charlie Brown", DepartmentId = 3 }
        };

        private readonly List<Availability> _availability = new List<Availability>
        {
            new Availability { TeacherId = 1, DayOfWeek = "Monday", StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(20, 0, 0) },
            new Availability { TeacherId = 1, DayOfWeek = "Tuesday", StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(20, 0, 0) },
            new Availability { TeacherId = 2, DayOfWeek = "Wednesday", StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(20, 0, 0) },
            new Availability { TeacherId = 2, DayOfWeek = "Thursday", StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(20, 0, 0) },
            new Availability { TeacherId = 3, DayOfWeek = "Monday", StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(20, 0, 0) },
            new Availability { TeacherId = 3, DayOfWeek = "Thursday", StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(20, 0, 0) }
        };

        public IActionResult BookMeeting()
        {
            var viewModel = new BookingViewModel
            {
                Departments = _departments,
                Teachers = _teachers
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Book(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Normally, you would save to the database here
                // For this example, we just redirect after booking

                // You could also add a success message to ViewBag
                ViewBag.Message = "Booking successful!";
                return RedirectToAction("Index"); // Redirect to a success page or index
            }

            // If the model is invalid, re-populate the view model
            model.Departments = _departments;
            model.Teachers = _teachers;
            return View("BookMeeting", model);
        }
        [HttpGet]
        public JsonResult GetAvailableDates(int teacherId)
        {
            // List to hold available dates
            var availableDates = new List<DateTime>();

            var today = DateTime.Now;
            for (int i = 0; i < 30; i++) // Check availability for the next 30 days
            {
                var date = today.AddDays(i);
                var dayOfWeek = date.DayOfWeek.ToString();

                // Compare day of week in a case-insensitive manner
                var availability = _availability.FirstOrDefault(a =>
                    a.TeacherId == teacherId &&
                    string.Equals(a.DayOfWeek, dayOfWeek, StringComparison.OrdinalIgnoreCase)
                );

                // Add the date to availableDates if availability exists
                if (availability != null)
                {
                    availableDates.Add(date);
                }
            }

            // Return the available dates in "yyyy-MM-dd" format
            return Json(availableDates.Select(d => d.ToString("yyyy-MM-dd")));
        }
    }


    public class Availability
    {
        public int TeacherId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }


}
