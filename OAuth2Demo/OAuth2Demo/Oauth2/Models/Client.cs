using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2Demo.Oauth2.Models
{
    public class Client
    {
        public string Id { get; set; }

        public string Secret { get; set; }

        public string RedirectUrl { get; set; }
    }
}