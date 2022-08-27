using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class DateItem
    {
        public Date date { get; set; }
        public List<UserFastPass> fastpass { get; set; }
        public List<Dining> dining { get; set; }
    }
}
