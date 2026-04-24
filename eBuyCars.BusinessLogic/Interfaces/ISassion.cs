using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBuyCars.BusinessLogic 
{
    public class BussinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
