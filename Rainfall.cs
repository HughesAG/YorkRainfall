using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusNano
{
    class Rainfall
    {
        private float[] storeRF = new float[96];
        private float rainfallValue = 0, cumRF = 0;
        public Rainfall(string directoryOutput)
        {
            using (WebClient wcSH = new WebClient())
            {
                wcSH.Headers[HttpRequestHeader.ContentType] = "application/json";
                string RFData = string.Empty;
                RFData = wcSH.DownloadString("https://environment.data.gov.uk/flood-monitoring/id/stations/059793/readings?_sorted&today");
                dynamic RFYork = JsonConvert.DeserializeObject<dynamic>(RFData);
                System.IO.File.WriteAllText(directoryOutput, Convert.ToString(RFYork) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " No. values " + Convert.ToString(RFYork.items.Count) + "\n");
                System.IO.File.WriteAllText(@"RainfallYork.txt", "Date, Rf, intervals" + "\n");
                for (int i = 0; i < RFYork.items.Count; i++)
                {
                    rainfallValue = RFYork.items[i].value;
                    System.IO.File.AppendAllText(@"RainfallYork.txt", Convert.ToString(RFYork.items[i].dateTime) + "," + Convert.ToString(rainfallValue) + "\n");
                    storeRF[i] = rainfallValue;
                    cumRF = cumRF + rainfallValue;
                }
            }
        }
        public float GetcumRF()
        {
            return cumRF;
        }
    }
}
