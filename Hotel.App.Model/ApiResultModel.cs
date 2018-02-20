using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hotel.App.Model
{
    public class ApiResultModel
    {
        public HttpStatusCode Status { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
