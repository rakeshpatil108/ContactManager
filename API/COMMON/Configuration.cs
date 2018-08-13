using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace API.COMMON
{
    public static class Configuration
    {
        public static string AuthToken = ConfigurationManager.AppSettings["AuthToken"];
    }
}