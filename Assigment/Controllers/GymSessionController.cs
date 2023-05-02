using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Assigment.Database;
using Assigment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class GymSessionController : Controller
    {
        private readonly MyDbContext _context;
        private const string bucketname = "gym3musk";


        public GymSessionController(MyDbContext context)
        {
            _context = context;
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

        gymSession myGymSession = new gymSession();
        public List<gymSession> GetGymSessions()
        {
            // Replace this with actual data from a database or API
            var sessions = new List<gymSession>
            {
                new gymSession { gymSession_ID = 1, gymSession_Category = "Weights", gymSession_TrainerName = "Mark Otto", 
                    gymSession_Date= DateTime.Now, gymSession_Slots = 5 },
            };

            return sessions;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            List<string> values = getKeyValues();
            AmazonS3Client connectionClient = new AmazonS3Client(values[0], values[1], values[2], RegionEndpoint.USEast1);

           

            var sessionToDelete = _context.GymSessions.FirstOrDefault(s => s.gymSession_ID == id);
            if (sessionToDelete != null)
            {
                try
                {
                    DeleteObjectRequest deleteRequest = new DeleteObjectRequest
                    {
                        BucketName = bucketname,
                        Key = sessionToDelete.gymSession_Trainer_Image_S3Key
                    };
                    await connectionClient.DeleteObjectAsync(deleteRequest);
                }
                catch (AmazonS3Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                _context.GymSessions.Remove(sessionToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "GymSession");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var models = _context.GymSessions.ToList();
            return View(models);
        }

        public IActionResult EditGymSession(int id)
        {
            var session = _context.GymSessions.FirstOrDefault(s => s.gymSession_ID == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProcessData(gymSession session,IFormFile imageFile)
        {
            List<string> values = getKeyValues();
            AmazonS3Client connectionClient = new AmazonS3Client(values[0], values[1], values[2], RegionEndpoint.USEast1);

            if (!ModelState.IsValid)
            {
                return View("AddGymSessions");
            }

            if (session.IsImageUpdated)
            {
                try
                {
                    DeleteObjectRequest deleteRequest = new DeleteObjectRequest
                    {
                        BucketName = bucketname,
                        Key = session.gymSession_Trainer_Prev_Image_URL
                    };

                    await connectionClient.DeleteObjectAsync(deleteRequest);

                    PutObjectRequest uploadRequest = new PutObjectRequest
                    {
                        InputStream = imageFile.OpenReadStream(),
                        BucketName = bucketname + "/images",
                        Key = imageFile.FileName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    await connectionClient.PutObjectAsync(uploadRequest);

                    session.gymSession_Trainer_Image_URL = "https://" + bucketname + ".s3.amazonaws.com/images/" + imageFile.FileName;
                    session.gymSession_Trainer_Image_S3Key = "images/" + imageFile.FileName;


                }
                catch (AmazonS3Exception ex) { }
            }


            session.IsImageUpdated = false;
            session.gymSession_Trainer_Prev_Image_URL = "";

            _context.GymSessions.Update(session);
            _context.SaveChanges();

            return RedirectToAction("Index","GymSession");
        }


        public IActionResult AddGymSession()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessAdditonalData(gymSession gymSession, IFormFile imageFile)
        {
            try
            {
                List<string> values = getKeyValues();
                AmazonS3Client connectionClient = new AmazonS3Client(values[0], values[1], values[2], RegionEndpoint.USEast1);

                PutObjectRequest uploadRequest = new PutObjectRequest
                {
                    InputStream = imageFile.OpenReadStream(),
                    BucketName = bucketname + "/images",
                    Key = imageFile.FileName,
                    CannedACL = S3CannedACL.PublicRead
                };

                await connectionClient.PutObjectAsync(uploadRequest);
            }catch (Exception ex)
            {
                return BadRequest("Error " + ex.Message);
                //return RedirectToAction("AddGymSession", "GymSession");
            }

            gymSession.gymSession_Trainer_Image_URL = "https://"+bucketname +".s3.amazonaws.com/images/"+imageFile.FileName;
            gymSession.gymSession_Trainer_Image_S3Key= "images/"+imageFile.FileName;

            Console.WriteLine(gymSession);

            if (ModelState.IsValid)
            {
                _context.GymSessions.Add(gymSession);
                _context.SaveChanges();
                return RedirectToAction("Index", "GymSession");
            }
   

            return RedirectToAction("AddGymSession", "GymSession");

            
        }


    }
}
