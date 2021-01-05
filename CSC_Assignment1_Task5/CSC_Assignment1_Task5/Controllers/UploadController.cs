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
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;

namespace CSC_Assignment1_Task5.Controllers
{
    public class UploadController : ApiController
    {
        [Route("api/upload")]
        [HttpPost]
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
                client = new AmazonS3Client(credentials, bucketRegion);

                var fileTransferUtility = new TransferUtility(client);
                string bucketName = "mattcscmedia";
                Guid key = Guid.NewGuid();
                string objectKey = key.ToString();

                fileTransferUtility.Upload(content, bucketName, objectKey);
                Console.WriteLine("Upload completed");

                string longUrl = "https://" + bucketName + ".s3." + bucketRegion.SystemName + ".amazonaws.com/" + objectKey;

                string bitly_token = "";

                string requestUrl = "https://api-ssl.bitly.com/v4/shorten";

                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

                //Set timeout to 15 seconds
                request.Timeout = 15 * 1000;
                request.Method = "POST";
                request.KeepAlive = false;
                request.Headers.Add("Authorization", String.Format("Bearer {0}", bitly_token));

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {

                    string jsonRequestData = JsonSerializer.Serialize(new { long_url = longUrl });

                    streamWriter.Write(jsonRequestData);
                }

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Console.WriteLine(response.StatusCode);

                JsonDocument jsonDoc = JsonDocument.Parse(response.GetResponseStream());

                JsonElement jsonEle = jsonDoc.RootElement;

                string shorten_link = jsonEle.GetProperty("link").GetString();

                return Request.CreateResponse(HttpStatusCode.OK, new { shortlink = shorten_link });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
