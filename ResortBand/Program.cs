using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class Program
    {
        public static Page page;
        public static string pageNo = "";
        public static int settingId = 0;

        static void Main(string[] args)
        {
            string jsonFilePath = @"C:\Users\andro\source\repos\ResortBand\ResortBand\";
            string jsonFileName = "guest" + pageNo + ".json";
            string accountFileAll = "", attractionFileAll = "", fastpassFileAll = "";

            if (args == null || args.Length == 0)
            {

            }
            
            LookupSettings lookSet = new LookupSettings();
            lookSet = Settings.SetSettings();
            if (lookSet.settings == null)
            {
                Console.WriteLine("No settings information found, using default values");
                Console.ReadLine();
            }
            else
            {
                jsonFilePath = lookSet.settings[settingId].corefilepath;
                jsonFileName = lookSet.settings[settingId].corefile + pageNo + "." + lookSet.settings[settingId].corefiletype;
                accountFileAll = lookSet.settings[settingId].accountfilepath + lookSet.settings[settingId].accountfile + "." + lookSet.settings[settingId].accountfiletype;
                attractionFileAll = lookSet.settings[settingId].attractionfilepath + lookSet.settings[settingId].attractionfile + "." + lookSet.settings[settingId].attractionfiletype;
                fastpassFileAll = lookSet.settings[settingId].fastpassfilepath + lookSet.settings[settingId].fastpassfile + "." + lookSet.settings[settingId].fastpassfiletype;
            }
            
            DeserializeData(jsonFilePath+jsonFileName);

            List<string> displayOptionList = new List<string>();
            string returnOption = "", displayOption = "";
            int acctIdFound;
            List<string> dateOpt = new List<string>();
            List<List<int>> groupIdList = new List<List<int>>();
            Account account = AccountRepository.GetAccountRepository(accountFileAll);
            Attractions AttractionList = AttractionRepository.GetAttractionRepository(attractionFileAll);
            FastpassList fastpassList = FastpassRepository.Get_FastpassRepository(fastpassFileAll);

            while (!returnOption.ToUpper().Equals("X")) {
                acctIdFound = -1;
                returnOption = ConsoleControl.MainScreen();
                if (returnOption != "" && returnOption != null && returnOption != "X")
                {
                    displayOptionList = ConsoleControl.LookupOptionRun(returnOption);
                    switch (returnOption)
                    {
                        case "1":
                            acctIdFound = AcctIdSearch(Convert.ToInt16(displayOptionList[0]), page);
                            break;
                        case "2":
                            acctIdFound = AccountRepository.UserNameSearch(displayOptionList[0], account);
                            acctIdFound = AcctIdSearch(acctIdFound, page);
                            break;
                    }
                    
                }
                if (acctIdFound >= 0)
                {
                    while (!returnOption.ToUpper().Equals("X"))
                    {
                        displayOption = ConsoleControl.DisplayAccountOptions();

                        switch (displayOption.ToUpper())
                        {
                            case "1":
                                ConsoleControl.DisplayAccount(page, acctIdFound);
                                break;
                            case "2":
                                dateOpt = DateOptions(acctIdFound, page);
                                List<List<UserFastPass>> fastpassIdList = Get_UserFastPassList(acctIdFound, dateOpt, page);
                                List<Attraction> attractionList = new List<Attraction>();

                                for (int dateno = 0; dateno < fastpassIdList.Count; dateno++)
                                {
                                    for (int fastpassno = 0; fastpassno < fastpassIdList[dateno].Count; fastpassno++)
                                    {
                                        Fastpasses fps = FastpassRepository.Get_FastPass(fastpassIdList[dateno][fastpassno].fastpassid, fastpassList);
                                        int attractionId = FastpassRepository.Get_FastPassAttractionId(fps);
                                        attractionList.Add(AttractionRepository.Get_Attraction(attractionId, AttractionList));
                                    }
                                }
                                ConsoleControl.DisplayFastPasses(dateOpt, fastpassIdList, attractionList);
                                break;
                            case "3":
                                dateOpt = DateOptions(acctIdFound, page);
                                ConsoleControl.DisplayDining(page, acctIdFound);
                                break;
                            case "X":
                                returnOption = "X";
                                break;
                        }
                        
                    }
                    returnOption = "";
                }    
            }

            //Console.WriteLine("Hello " + page.guest[0].firstname + " " + page.guest[0].lastname + "! Have a Magical Day!");
            //Console.WriteLine("Hello " + page.guest[1].firstname + " " + page.guest[1].lastname + "! Have a Magical Day!");

            //Console.Read();
        }

        public static void DeserializeData(string jsonFilePath)
        {
            
            page = new Page();
            StreamReader r = new StreamReader(jsonFilePath);
            string guestInformation = r.ReadToEnd();
            page = JsonConvert.DeserializeObject<Page>(guestInformation);
        }

        

        //
        public static int SearchId(string option)
        {
            int idAcctFound = 0;

            idAcctFound = AcctIdSearch(Convert.ToInt16(option), page);
            if (idAcctFound >= 0)
            {
                ConsoleControl.setUserNameDisplay(page.guest[idAcctFound].firstname);
            }
            else
            {
                Console.WriteLine("Account not found!");
                Console.ReadLine();
            }

            return idAcctFound;
        }


        public static int AcctIdSearch(int searchId, Page page)
        {
            int idFound = -1;
            for (int guestid = 0; guestid < page.guest.Count; guestid++)
            {
                if (page.guest[guestid].id.Equals(searchId))
                {
                    idFound = guestid;
                    break;
                }
            }

            if (idFound >= 0)
            {
                ConsoleControl.setUserNameDisplay(page.guest[idFound].firstname);
            }
            else
            {
                Console.WriteLine("Account not found!");
                Console.ReadLine();
            }

            return idFound;
        }

        // Creates an array of all dates the user has in their system
        public static List<string> DateOptions(int acctId, Page page)
        {
            List<string> returnArray = new List<string>();
            for (int dateItem = 0; dateItem < page.guest[acctId].dateitem.Count; dateItem++)
            {
                returnArray.Add(page.guest[acctId].dateitem[dateItem].date.dateId);
            }

            return returnArray;
        }

        // Creates an array of all Fastpasses based on a list of dates
        public static List<List<UserFastPass>> Get_UserFastPassList(int acctId, List<string> dateIdList, Page page)
        {
            List<List<UserFastPass>> returnFastPassList = new List<List<UserFastPass>>();
            for (int dateItem = 0; dateItem < dateIdList.Count; dateItem++)
            {
                returnFastPassList.Add(new List<UserFastPass>());
                for (int fastpassItem = 0; fastpassItem < page.guest[acctId].dateitem[dateItem].fastpass.Count; fastpassItem++)
                {
                    returnFastPassList[dateItem].Add(page.guest[acctId].dateitem[dateItem].fastpass[fastpassItem]);
                }
            }

            return returnFastPassList;
        }
    }
}
