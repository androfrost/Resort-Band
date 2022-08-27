using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class FastpassRepository
    {
        public static FastpassList Get_FastpassRepository(string jsonFilePath)
        {

            FastpassList deserializedData = new FastpassList();
            if (File.Exists(jsonFilePath))
            {
                deserializedData = DeserializeSettingsData(jsonFilePath);
            }
            return deserializedData;
        }

        public static FastpassList DeserializeSettingsData(string jsonFile)
        {

            FastpassList fastpassSetting = new FastpassList();
            StreamReader r = new StreamReader(jsonFile);
            string fastpassInformation = r.ReadToEnd();
            fastpassSetting = JsonConvert.DeserializeObject<FastpassList>(fastpassInformation);

            return fastpassSetting;
        }

        // Returns the Fastpasses object that contains the fastpassId
        public static Fastpasses Get_FastPass(int fastpassId, FastpassList fastpass)
        {
            Fastpasses returnFastPass = new Fastpasses();
            for (int fastpassNo = 0; fastpassNo < fastpass.fastpassList.Count; fastpassNo++)
            {
                if (fastpass.fastpassList[fastpassNo].fastpassid.Equals(fastpassId))
                {
                    returnFastPass = fastpass.fastpassList[fastpassNo];
                }
            }

            return returnFastPass;
        }

        public static int Get_FastPassAttractionId(Fastpasses fastpass)
        {
            int returnId = fastpass.attractionid;

            return returnId;
        }

        public static int Get_FastPassAttraction(int fastpassId, FastpassList fastpass)
        {
            int returnId = 0;
            for(int fastpassNo = 0; fastpassNo < fastpass.fastpassList.Count; fastpassNo++)
            {
                if (fastpass.fastpassList[fastpassNo].fastpassid.Equals(fastpassId))
                {
                    returnId = Get_FastPassAttractionId(fastpass.fastpassList[fastpassNo]);
                }
            }

            return returnId;
        }
    }
}
