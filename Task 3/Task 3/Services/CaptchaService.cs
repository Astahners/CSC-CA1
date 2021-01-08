using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Task_3.Services
{
    public class CaptchaService
    {
        public bool VerifyResponse(string captchaString)
        {
            string requestUrl = "https://www.google.com/recaptcha/api/siteverify";
            string captchaSecret = "6Ld3HBAaAAAAAIc-T0fQ6Mkvb0njP3oNWPPC_68w";

            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

            //Set timeout to 15 seconds
            request.Timeout = 15 * 1000;
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";

            string postData = string.Format("secret={0}&response={1}", captchaSecret, captchaString);
            byte[] buffer = Encoding.Default.GetBytes(postData);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Console.WriteLine(response.StatusCode);

            JsonDocument jsonDoc = JsonDocument.Parse(response.GetResponseStream());

            JsonElement jsonEle = jsonDoc.RootElement;

            double humanScore = jsonEle.GetProperty("score").GetDouble();

            if(humanScore < 0.5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}