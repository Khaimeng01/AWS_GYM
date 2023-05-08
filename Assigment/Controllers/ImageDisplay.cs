using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon; //aws account access
using Amazon.S3; //s3
using Amazon.S3.Model; //item in bucket
using Microsoft.Extensions.Configuration; //appsettings.json
using System.IO; //appsettings.json
using Microsoft.AspNetCore.Http; //binary object transfer in network
using Microsoft.AspNetCore.Routing.Constraints;
using System.Net.Mime;
namespace Assigment.Controllers
{
    public class ImageDisplay : Controller
    {
        private const string bucketname = "gymfacilities";

        //get back the key values from the appsettings.json
        private List<string> getKeyValues()
        {
            List<string> accessKeyList = new List<string>();

            //read the key values from the appsettings.json and store in keyList
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration conf = builder.Build();
            accessKeyList.Add(conf["Keys:Key1"]);
            accessKeyList.Add(conf["Keys:Key2"]);
            accessKeyList.Add(conf["Keys:Key3"]);

            return accessKeyList;

        }
        public async Task<IActionResult> DisplayGymImages()
        {
            //step 1: authenticate your AWS Identity first
            List<string> keys = getKeyValues();
            AmazonS3Client connectionClient =
                new AmazonS3Client(keys[0], keys[1], keys[2], RegionEndpoint.USEast1);

            List<S3Object> imageList = new List<S3Object>();
            try
            {
                string anyNextItem = null;
                do
                {
                    //request item 1 by 1 back from the S3
                    ListObjectsRequest request = new ListObjectsRequest
                    {
                        BucketName = bucketname,
                    };
                    ListObjectsResponse response = await connectionClient.ListObjectsAsync(request);
                    imageList.AddRange(response.S3Objects);
                    anyNextItem = response.NextMarker;
                }
                while (anyNextItem != null);
            }
            catch (AmazonS3Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            return View(imageList);
        }
    }
}



