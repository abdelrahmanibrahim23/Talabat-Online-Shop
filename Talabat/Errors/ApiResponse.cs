using System;

namespace Talabat.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
       

        public ApiResponse(int statuscode,string errorMessage=null)
        {
            StatusCode = statuscode;
            ErrorMessage = errorMessage??GetMessagefromStatusCode(statuscode);
        }

        private string GetMessagefromStatusCode(int statuscode)
        {
            return statuscode switch {
                400 => "BadRequst",
                401=>"Not Authorized",
                404=>"NotFound",
                500=>"Server Request ",
                _=>null
            };

        }
    }
}
