using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class ConsoleControl
    {
        private static string UserName = "";
        public static string MainScreen()
        {
            string option = "";
            UserName = "";

            DisplayUserHeader();

            Console.WriteLine("Choose what option you would like to do:");
            Console.WriteLine("1. Lookup Account Information by ID");
            Console.WriteLine("2. Lookup Account Information by User Name");
            Console.WriteLine("X. Exit");
            option = Console.ReadLine();

            return option;
        }

        public static List<string> LookupOptionRun(string option) {

            List<string> returnVal = new List<string>();

            switch (option)
            {
                case "1":
                    returnVal.Add(EnterSearchItem("ID"));
                    break;
                case "2":
                    returnVal.Add(EnterSearchItem("User Name"));
                    returnVal.Add(EnterAdditionalSearchItem("Password"));
                    break;
            }

            return returnVal;
        }

        public static void setUserNameDisplay(string userName)
        {
            UserName = userName;
        }

        public static string EnterSearchItem(string searchItem)
        {
            string returnOption = "0";
            UserName = "";

            DisplayUserHeader();
            Console.WriteLine("Enter guest information: ");
            Console.Write(searchItem + ": ");
            returnOption = Console.ReadLine();

            return returnOption;
        }

        public static string EnterAdditionalSearchItem(string searchItem)
        {
            string returnOption;
            UserName = "";

            Console.Write(searchItem + ": ");
            returnOption = Console.ReadLine();

            return returnOption;
        }

        public static void DisplayUserHeader()
        {
            string addUserName = "";
            if (UserName != "")
            {
                addUserName = " --- " + UserName;
            }
            Console.Clear();
            Console.WriteLine("Resort Band" + addUserName);
        }

        public static string DisplayAccountOptions()
        {
            string returnOption = "0";

            DisplayUserHeader();
            Console.WriteLine("Choose what information about this account you would like to display:");
            Console.WriteLine("1. Account information");
            Console.WriteLine("2. FastPasses");
            Console.WriteLine("3. Dining");
            Console.WriteLine("X. Exit");
            returnOption = Console.ReadLine();

            return returnOption;
        }

        public static void DisplayAccount(Page page, int accountId)
        {
            DisplayUserHeader();
            Console.WriteLine("Name:    " + page.guest[accountId].firstname + " " + page.guest[accountId].lastname);
            Console.WriteLine("Phone #: " + page.guest[accountId].telephone);
            Console.WriteLine("Address: " + page.guest[accountId].billingaddress[0].addressnumber + " " + page.guest[accountId].billingaddress[0].addressdirection + " " + page.guest[accountId].billingaddress[0].address);
            Console.WriteLine("Email  : " + page.guest[accountId].email);
            Console.ReadLine();
        }

        public static void DisplayFastPasses(List<string> dates, List<List<UserFastPass>> fastpassIdList, List<Attraction> attractionList)
        {
            int dayCount = 0;
            int fastpassCount = 0;
            DisplayUserHeader();
            Console.WriteLine("FastPasses");
            foreach (string date in dates)
            {
                dayCount++;
                Console.WriteLine();
                Console.Write("Day ");
                Console.Write(dayCount);
                Console.Write(": ");
                Console.WriteLine(DateInformation.dateIdToDate(date));
                
                foreach (UserFastPass fastpassId in fastpassIdList[dayCount-1])
                {
                    
                    Console.WriteLine(attractionList[fastpassCount].attractionname);
                    Console.WriteLine(attractionList[fastpassCount].attractiondesc);
                    fastpassCount++;
                }
            }
            Console.ReadLine();
        }
        public static void DisplayDining(Page page, int accountId)
        {
            DisplayUserHeader();

            Console.ReadLine();
        }
    }
}