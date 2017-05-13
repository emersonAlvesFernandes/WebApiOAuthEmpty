using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiOAuthEmpty.Controllers
{
    public class ValuesController : ApiController
    {
        [Authorize(Roles = "Admin")]
        public string Get()
        {            
            return User.Identity.Name;
        }
    }
}
