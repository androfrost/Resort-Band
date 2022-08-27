using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class DateInformation
    {
        // Takes the integer date Id and formats a string of the date represented in normal date format
        public static string dateIdToDate(int dateId)
        {
            string dateFormat = "";
            string dateIdString = Convert.ToString(dateId).PadLeft(8, '0');

            dateFormat = dateIdToDate(dateIdString);

            return dateFormat;
        }

        // Takes the string date Id and formats a string of the date represented in normal date format
        public static string dateIdToDate(string dateId)
        {
            string dateFormat = "";
            string year = "", month = "", day = "";

            year = dateId.Substring(0, 4);
            month = dateId.Substring(4, 2);
            day = dateId.Substring(6, 2);

            dateFormat = month + "/" + day + "/" + year;

            return dateFormat;
        }
    }
}
