using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Services
{
    public static class Internet
    {
        public const string DOMAIN_NAME = "google.com";
        public static bool IsWork()
        {
            try
            {
                Dns.GetHostEntry(DOMAIN_NAME);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
