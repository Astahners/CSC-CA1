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
            if (!ReCaptchaPassed(model.captcha))
            {
                ModelState.AddModelError(string.Empty, "You failed the CAPTCHA.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You failed the Captcha.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "You passed the captcha.");
            }
        }

        public static bool ReCaptchaPassed(string gRecaptcharesponse)
        {
            HttpClient httpClient = new HttpClient();

            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6Ld7rRcaAAAAAKM-dpLo52R45_PGLtS5eISRiIJ_&response={gRecaptcharesponse}").Result;

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
