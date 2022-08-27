using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBand
{
    internal class AttractionRepository
    {
        public static Attractions GetAttractionRepository(string jsonFilePath)
        {

            Attractions deserializedData = new Attractions();
            if (File.Exists(jsonFilePath))
            {
                deserializedData = DeserializeSettingsData(jsonFilePath);
            }
            return deserializedData;
        }

        public static Attractions DeserializeSettingsData(string jsonFile)
        {

            Attractions attractionSetting = new Attractions();
            StreamReader r = new StreamReader(jsonFile);
            string attractionInformation = r.ReadToEnd();
            attractionSetting = JsonConvert.DeserializeObject<Attractions>(attractionInformation);

            return attractionSetting;
        }
        public static Attraction Get_Attraction(int attractionId, Attractions attractions)
        {
            Attraction attractionFound = new Attraction();
            for (int attractionno = 0; attractionno < attractions.attraction.Count; attractionno++)
            {
                if (attractions.attraction[attractionno].attractionid.Equals(attractionId))
                {
                    attractionFound = attractions.attraction[attractionno];
                    break;
                }
            }

            return attractionFound;
        }

        public static Attraction Get_Attraction(int attractionId, List<Attractions> attractions)
        {
            Attraction attractionFound = new Attraction();
            for (int attractionday = 0; attractionday < attractions[0].attraction.Count;) { 
                for (int attractionno = 0; attractionno < attractions[0].attraction.Count; attractionno++)
                {
                    if (attractions[attractionno].attraction[attractionno].attractionid.Equals(attractionId))
                    {
                        attractionFound = attractions[0].attraction[attractionno];
                        break;
                    }
                }
            }

            return attractionFound;
        }
    }
}
