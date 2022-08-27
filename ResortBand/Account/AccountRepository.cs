using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class AccountRepository
    {
        public static Account GetAccountRepository(string jsonFilePath) {
            
            Account deserializedData = new Account();
            if (File.Exists(jsonFilePath))
            {
                deserializedData = DeserializeSettingsData(jsonFilePath);
            }
            return deserializedData;
        }

        public static Account DeserializeSettingsData(string jsonFile)
        {

            Account acctSetting = new Account();
            StreamReader r = new StreamReader(jsonFile);
            string accountInformation = r.ReadToEnd();
            acctSetting = JsonConvert.DeserializeObject<Account>(accountInformation);

            return acctSetting;
        }

        public static int UserNameSearch(string searchUserName, Account account)
        {
            int idFound = -1;
            for (int acctno = 0; acctno < account.account.Count; acctno++)
            {
                if (account.account[acctno].username.Equals(searchUserName))
                {
                    idFound = account.account[acctno].accountid;
                    break;
                }
            }

            return idFound;
        }
    }
}
