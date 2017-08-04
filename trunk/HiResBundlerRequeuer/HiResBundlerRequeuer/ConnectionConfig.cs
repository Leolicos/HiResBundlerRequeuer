using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HiResBundlerRequeuer
{
    public static class ConnectionConfig
    {
        static public string GetConnectionString()
        {
            string url = ConfigurationManager.AppSettings["Connection"].ToString();
            return url;
        }
    }
}
