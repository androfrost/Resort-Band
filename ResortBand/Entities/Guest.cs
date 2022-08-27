using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class Guest
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int pin { get; set; }
        public string telephone { get; set; }
        public List<Address> billingaddress { get; set; }
        public CreditCard creditcard { get; set; }
        public List<DateItem> dateitem { get; set; }
        public string email { get; set; }

    }
}
