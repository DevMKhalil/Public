using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Errors
{
    public class DublicateEmailException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "User With Given Email Already Exist";
    }
}
