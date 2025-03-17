using System.Collections;
using System.Collections.Generic;

namespace Talabat.Errors
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse():base(400)
        {
            
        }
    }
}
