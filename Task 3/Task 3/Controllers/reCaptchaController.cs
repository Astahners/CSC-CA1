using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task_3.Models;

namespace Task_3.Controllers
{
    public class reCaptchaController : ApiController
    {
        [HttpPost]
        [Route("api/recaptcha")]
        public HttpResponseMessage checkRecaptcha(reCaptcha model)
        {
            if (ModelState.IsValid)
            {

            }
        }

        public static bool ReCaptchaPassed(string gRecaptcharesponse)
        {
            HttpClient httpClient = new HttpClient();

            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6Ld3HBAaAAAAAIc-T0fQ6Mkvb0njP3oNWPPC_68w&response={gRecaptcharesponse}").Result;

            if (res.StatusCode != HttpStatusCode.OK)
                return false;

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);

            if (JSONdata.success != "true")
                return false;

            return true;
        }
    }
}
