using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Login.Controllers
{
    public class SNSController : Controller
    {
        private const string TopicARN = "arn:aws:sns:us-east-1:706328508339:EmailBroadcastGroup1";

        private List<string> getKeyValues()
        {
            List<string> keys = new List<string>() { };
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration configure = builder.Build(); //build the file

            keys.Add(configure["Keys:keys1"]);
            keys.Add(configure["Keys:keys2"]);
            keys.Add(configure["Keys:keys3"]);

            return keys;
        }

        //make index function as newsletter registration function
        public IActionResult Index(string? msg)
        {
            ViewBag.result = msg;
            return View();
        }

        //register user in the SNS
        public async Task<IActionResult> registerNewsletter(string EmailId)
        {
            List<string> keys = getKeyValues();
            AmazonSimpleNotificationServiceClient conncetionClient = new AmazonSimpleNotificationServiceClient("ASIA2I5DVW6ZTBNQ3XWI", "9hB0Qo6nnjsoTyCyQ6/QvLp6vUKyK9psh0hNsY+k", "FwoGZXIvYXdzEFIaDKWHAAwK4AuQ4gisDyLJAdh2oz6djG31UgeyUt46a9CmNXmCcgQiCNYBkob6vZ2qUS6sT91iK+ZaSrBdBjtroXoSg76ywaOqzuiWZjqT5LBoB7Za/IznWv+Y3C6csKpMVuk4/hl6+1lx8hS24Ms7WFI8b5vqrnTUtjNf4GdNe2l/exX31Gb1T7IubDQdQu66nHewLYqeMZThD0sHYEhqkwTYxccP75e4i8nOLZtoyK8DRpMz+Z/bIpqWtDT9R7B9qutFJp6+98XpHgQQFFQn+fAuHt+SkfxghCil8uKiBjItADfrI1TI6a1M1EuwHt2vfVNVf829yq8tPxV1/3aRhe+QsoNTQMnoGAHjWdfN", RegionEndpoint.USEast1);

            //AmazonSimpleNotificationServiceClient conncetionClient = new AmazonSimpleNotificationServiceClient(keys[0], keys[1], keys[2], RegionEndpoint.USEast1);
            string SubscriptionID = "";
            if (ModelState.IsValid)
            {
                try
                {
                    SubscribeRequest request = new SubscribeRequest
                    {
                        TopicArn = TopicARN,
                        Protocol = "email",
                        Endpoint = EmailId
                    };
                    SubscribeResponse response = await conncetionClient.SubscribeAsync(request);
                    SubscriptionID = response.ResponseMetadata.RequestId;
                }
                catch (AmazonSimpleNotificationServiceException ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
            }
            return RedirectToAction("Index", "SNS", new
            {
                msg = "You have done the newsletter subscription with ID: " + SubscriptionID + ". Plase check your mail for confirmation!"
            });
        }

        public IActionResult broadcastMessageForm()
        {
            return View();
        }

        //process the broadcast message
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> broadcastMessage(string subject, string message)
        {
            List<string> keys = getKeyValues();
            AmazonSimpleNotificationServiceClient conncetionClient = new AmazonSimpleNotificationServiceClient("ASIA2I5DVW6ZTBNQ3XWI", "9hB0Qo6nnjsoTyCyQ6/QvLp6vUKyK9psh0hNsY+k", "FwoGZXIvYXdzEFIaDKWHAAwK4AuQ4gisDyLJAdh2oz6djG31UgeyUt46a9CmNXmCcgQiCNYBkob6vZ2qUS6sT91iK+ZaSrBdBjtroXoSg76ywaOqzuiWZjqT5LBoB7Za/IznWv+Y3C6csKpMVuk4/hl6+1lx8hS24Ms7WFI8b5vqrnTUtjNf4GdNe2l/exX31Gb1T7IubDQdQu66nHewLYqeMZThD0sHYEhqkwTYxccP75e4i8nOLZtoyK8DRpMz+Z/bIpqWtDT9R7B9qutFJp6+98XpHgQQFFQn+fAuHt+SkfxghCil8uKiBjItADfrI1TI6a1M1EuwHt2vfVNVf829yq8tPxV1/3aRhe+QsoNTQMnoGAHjWdfN", RegionEndpoint.USEast1);

            try
            {
                PublishRequest request = new PublishRequest
                {
                    TopicArn = TopicARN,
                    Subject = subject,
                    Message = message
                };
                await conncetionClient.PublishAsync(request);
            }
            catch (AmazonSimpleNotificationServiceException ex)
            {
                throw new Exception("Error Message: " + ex.Message);
            }
            return RedirectToAction("broadcastMessageForm", "SNS");
        }
    }
}