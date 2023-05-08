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

        //private const string bucketname = "gym3musk";
        private readonly ILogger<BookingSessionController> _logger;

        public BookingSessionController(MyDbContext context, ILogger<BookingSessionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private List<string> getKeyValues()
        {
            List<string> values = new List<string>();

            //1. link to appsettings.json and get back the values
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build(); //build the json file

            //2. read the info from json using configure instance
            values.Add(configure["Values:Key1"]);
            values.Add(configure["Values:Key2"]);
            values.Add(configure["Values:Key3"]);

            Console.WriteLine(values.ToString());

            return values;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var gymSessions = await _context.GymSessions.ToListAsync();

            var gymSessions = await _context.GymSessions.ToListAsync();
            var bookingSessions = await _context.BookingSessions.FirstOrDefaultAsync();

            var viewModel = new MyViewModel
            {
                GymSession = gymSessions,
                BookingSession = bookingSessions
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(bookingSession bookingSession)
        {
            if (ModelState.IsValid)
            {
                _context.BookingSessions.Add(bookingSession);
                _context.SaveChanges();

                return RedirectToAction("Index", "BookingSession");
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult RedirectToSNSIndex(bookingSession bookingSession)
        {
            return RedirectToAction("registerNewsletter", "SNS", bookingSession.bookingSession_Username);
        }

        //public async Task<IActionResult> DisplayImagesAsGallery()
        //{
        //    //step 1: connection
        //    List<string> keys = getKeyValues();
        //    AmazonS3Client conncetionClient = new AmazonS3Client(keys[0], keys[1], keys[2], RegionEndpoint.USEast1);

        //    //step 2: create an empty list to kepp images from S3
        //    List<S3Object> imageList = new List<S3Object>();

        //    //step 3: keep image 1 by 1 into imageList
        //    try
        //    {
        //        string anyNextItem = null;
        //        do
        //        {
        //            //create List object request to the S3
        //            ListObjectsRequest request = new ListObjectsRequest
        //            {
        //                BucketName = bucketname
        //            };

        //            //getting response (images) back from the S3
        //            ListObjectsResponse response = await conncetionClient.ListObjectsAsync(request).ConfigureAwait(false);
        //            imageList.AddRange(response.S3Objects);
        //            anyNextItem = response.NextMarker;
        //        } while (anyNextItem != null);
        //    }
        //    catch (AmazonS3Exception ex)
        //    {
        //        return BadRequest("Error: " + ex.Message);
        //    }
        //    return View(imageList);
        //}

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
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] BookingSessions user)
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