using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;

namespace SoServices.Models
{
    public class Result
    {
        public Result(bool status, string mess)
        {
            success = status;
            message = mess;
        }
        public bool success = true;
        public string message;
    }
}