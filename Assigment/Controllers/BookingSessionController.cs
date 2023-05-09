using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assigment.Database;
using Assigment.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace Assigment.Controllers
{
    [Route("BookingSession")]
    public class BookingSessionController : Controller
    {
        private readonly MyDbContext _context;

        private const string bucketname = "gym3musk";
        private readonly ILogger<BookingSessionController> _logger;

        public BookingSessionController(MyDbContext context, ILogger<BookingSessionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private List<string> getKeyValues()
        {
            List<string> values = new List<string>();

            //1.link to appsettings.json and get back the values
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build(); //build the json file

            //2.read the info from json using configure instance
            values.Add(configure["Values:Key1"]);
            values.Add(configure["Values:Key2"]);
            values.Add(configure["Values:Key3"]);

            Console.WriteLine(values.ToString());

            return values;
        }

        [HttpGet]
        public async Task<IActionResult> Index(String parameterName)
        {
            _logger.LogInformation($"Category: {parameterName}");

            var gymSessions = await _context.GymSessions.Where(g => g.gymSession_Category == parameterName).ToListAsync();

            if (gymSessions.Count > 0)
            {
                var bookingSessions = await _context.BookingSessions.FirstOrDefaultAsync();

                var viewModel = new MyViewModel
                {
                    GymSession = gymSessions,
                    BookingSession = bookingSessions
                };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Views_Home_Facilities", "Home");
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(MyViewModel test)
        {
            var booking = test.BookingSession;
            _logger.LogInformation($"CustomerName: {booking.bookingSession_CustomerName}");
            _logger.LogInformation($"TrainerName: {booking.bookingSession_TrainerName}");
            _logger.LogInformation($"Category: {test.BookingSession.bookingSession_Category}");
            _logger.LogInformation($"Category 2: {booking.bookingSession_Category}");
            _logger.LogInformation($"Date: {booking.bookingSession_Date}");
            _logger.LogInformation($"Time: {booking.bookingSession_Time}");
            _logger.LogInformation($"IC: {booking.gymSessions_ID}");
            var gymSession = _context.GymSessions.Find(booking.gymSessions_ID);
            string trainerName = "";
            if (gymSession != null)
            {
                trainerName = gymSession.gymSession_TrainerName;
                test.BookingSession.bookingSession_Date = gymSession.gymSession_Date;
            }

            _logger.LogInformation("Pass");


            bookingSession bd = new bookingSession()
            {
                bookingSession_CustomerName = booking.bookingSession_CustomerName,
                bookingSession_Category = booking.bookingSession_Category,
                bookingSession_Date = booking.bookingSession_Date,
                bookingSession_TrainerName = trainerName,
                bookingSession_Time = booking.bookingSession_Time,
                gymSessions_ID = booking.gymSessions_ID,
                
            };

            _context.BookingSessions.Add(bd);
            _context.SaveChanges();

            gymSession gym1 = _context.GymSessions.Find(booking.gymSessions_ID);
            gym1.gymSession_Slots = gym1.gymSession_Slots - 1;
            _context.SaveChanges();


            return RedirectToAction("Views_Home_Facilities", "Home");

            //if (ModelState.IsValid)
            //{
            //    _logger.LogInformation("Pass");


            //    bookingSession bd = new bookingSession()
            //    {
            //        bookingSession_CustomerName = booking.bookingSession_CustomerName,
            //        bookingSession_Category = booking.bookingSession_Category,
            //        bookingSession_Date = booking.bookingSession_Date,
            //        bookingSession_TrainerName = trainerName,
            //        bookingSession_Time = booking.bookingSession_Time
            //    };

            //    _context.BookingSessions.Add(bd);
            //    _context.SaveChanges();

            //    return RedirectToAction("Index", "ManageAdmin");
            //}
            //else
            //{
            //    _logger.LogInformation("Failed");
            //    // Log validation errors
            //    var errors = ModelState
            //        .Where(x => x.Value.Errors.Count > 0)
            //        .Select(x => new { x.Key, x.Value.Errors })
            //        .ToArray();

            //    foreach (var error in errors)
            //    {
            //        _logger.LogError($"Property: {error.Key}");
            //        foreach (var errorMessage in error.Errors)
            //        {
            //            _logger.LogError($"Error: {errorMessage.ErrorMessage}");
            //        }
            //    }

            //    return RedirectToAction("Index", "ManageCustomer");

            //}

        }

        //public IActionResult RedirectToSNSIndex(bookingSession bookingSession)
        //{
        //    return RedirectToAction("registerNewsletter", "SNS", bookingSession.bookingSession_Username);
        //}

        public async Task<IActionResult> DisplayImagesAsGallery()
        {
            //step 1: connection
            List<string> keys = getKeyValues();
            AmazonS3Client conncetionClient = new AmazonS3Client(keys[0], keys[1], keys[2], RegionEndpoint.USEast1);

            //step 2: create an empty list to kepp images from S3
            List<S3Object> imageList = new List<S3Object>();

            //step 3: keep image 1 by 1 into imageList
            try
            {
                string anyNextItem = null;
                do
                {
                    //create List object request to the S3
                    ListObjectsRequest request = new ListObjectsRequest
                    {
                        BucketName = bucketname
                    };

                    //getting response (images) back from the S3
                    ListObjectsResponse response = await conncetionClient.ListObjectsAsync(request).ConfigureAwait(false);
                    imageList.AddRange(response.S3Objects);
                    anyNextItem = response.NextMarker;
                } while (anyNextItem != null);
            }
            catch (AmazonS3Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            return View(imageList);
        }

        public async Task<IActionResult> AddBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.BookingSessions.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //// POST: Users/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] bookingSession user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}
    }
}