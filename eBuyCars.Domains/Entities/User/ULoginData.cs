using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBuyCars.Domains.Entities.User
{
    public class ULoginData
    {
        public string Credential { get; set; } = string.Empty;
        public string Password { get; set; }    = string.Empty;
        public string LoginIp { get; set; } = string.Empty;
        public DateTime LoginDateTime { get; set; }

    }
}
