using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class Settings
    {
        public static LookupSettings SetSettings() {
            string jsonFilePath = @"C:\Users\andro\source\repos\ResortBand\ResortBand\Settings\LookupSettings.json";
            LookupSettings deserializedData = new LookupSettings();
            if (File.Exists(jsonFilePath))
            {
                deserializedData = DeserializeSettingsData(jsonFilePath);
            }
            return deserializedData;
        }

        public static LookupSettings DeserializeSettingsData(string jsonFile)
        {

            LookupSettings lookSetting = new LookupSettings();
            StreamReader r = new StreamReader(jsonFile);
            string guestInformation = r.ReadToEnd();
            lookSetting = JsonConvert.DeserializeObject<LookupSettings>(guestInformation);

            return lookSetting;
        }
    }

}
