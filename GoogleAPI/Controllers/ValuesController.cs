using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoogleAPI.Models;
using System.Web.Http.Cors;
namespace GoogleAPI.Controllers
{
     [EnableCors(origins: "http://localhost:17590", headers: "*", methods: "*")]
    [AllowAnonymous]
    public class ValuesController : ApiController
    {
        // GET api/values/5;

        [HttpGet]
         
        public HttpResponseMessage getqr()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("email");
            String email = headerValues.FirstOrDefault(); 
            var rand = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("Test Two Factor", email, rand, 300, 300);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string manualEntrySetupCode = setupInfo.ManualEntryKey;

            return Request.CreateResponse(HttpStatusCode.OK, setupInfo);
        }

        [HttpPost]
        public HttpResponseMessage PostQR()
        {
            IEnumerable<string> QRKey = Request.Headers.GetValues("AccountSecretKey");
            String googleQRKey = QRKey.FirstOrDefault();
            IEnumerable<string> OTP = Request.Headers.GetValues("OTP");
            String googleOTP = OTP.FirstOrDefault();
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var result = tfa.ValidateTwoFactorPIN(googleQRKey, googleOTP);

            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "failure");
            }
        }
        
        
    }

      
}