using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CSC_Assignment1_Task5.Controllers
{
    public class UploadController : ApiController
    {
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            };

            var provider = new MultipartMemoryStreamProvider();

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                List<byte[]> files = new List<byte[]>();

                // This illustrates how to get the file names.
                foreach (HttpContent file in provider.Contents)
                {
                    files.Add(await file.ReadAsByteArrayAsync());
                }

                byte[] photos = files[0];
                Stream content = new MemoryStream(photos);

                string awsAccessKeyId = "";
                string awsSecretAccessKey = "";
                string token = "";
                SessionAWSCredentials credentials = new SessionAWSCredentials(awsAccessKeyId,awsSecretAccessKey,token);
                RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
                IAmazonS3 client;
                client = new AmazonS3Client(bucketRegion);

                var fileTransferUtility = new TransferUtility(client);
                string bucketName = "mattcscmedia";
                Guid key = new Guid();

                fileTransferUtility.Upload(content, bucketName, key.ToString());
                Console.WriteLine("Upload completed");

                string longUrl = "https://" + bucketName + ".s3." + bucketRegion.SystemName + ".amazonaws.com/" + key;

                return Request.CreateResponse(HttpStatusCode.OK, longUrl);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
