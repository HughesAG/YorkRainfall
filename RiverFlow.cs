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
    class RiverFlow
    {
        private string ShoothillAPIkey = String.Empty;
        private string OverallSessionId = String.Empty;
        private double dailyStageOuseSkelton = 0, dailyStageFossHuntington = 0;
        private double dailyFlowOuseSkelton = 0, dailyFlowFossHuntington = 0;
        private double SDCoeffAOuseSkelton = 65, SDCoeffAFlossHuntinton = 4;
        private double SDCoeffBOuseSkelton = 1.05, SDCoeffBFlossHuntington = 1.0;
        private double SDMinStageOuseSkelton = 0.502, SDMinStageFlossHuntington = 0.669;

        public RiverFlow(string directoryOutput)
        {

            
            using (WebClient wcSH = new WebClient())
            {
                wcSH.Headers[HttpRequestHeader.ContentType] = "application/json";
                string uri = "http://riverlevelsapi.shoothill.com/ApiAccount/ApiLogin";
                string data = "{'PublicApiKey':'4a361716-61ef-4f42-9768-9393cf68c96d', 'ApiVersion':'2'}";
                string result = wcSH.UploadString(uri, data);
                dynamic authorizedSession = JsonConvert.DeserializeObject<dynamic>(result);
                Console.WriteLine(authorizedSession);
                Console.WriteLine(" Result is: " + result);
                OverallSessionId = authorizedSession.SessionHeaderId;
            }

            using (WebClient wcSH = new WebClient())
            {
                wcSH.Headers["SessionHeaderId"] = OverallSessionId;
                string GaugeData = string.Empty;
                GaugeData =
                    wcSH.DownloadString("http://riverlevelsapi.shoothill.com/TimeSeries/GetTimeSeriesRecentDatapoints/?stationId=1897&dataType=3&numberDays=1");
                dynamic RiverLevelsOuseSkelton = JsonConvert.DeserializeObject<dynamic>(GaugeData);
                System.IO.File.WriteAllText(directoryOutput, Convert.ToString(RiverLevelsOuseSkelton) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " River gauge id:" + Convert.ToString(RiverLevelsOuseSkelton.gauge.id) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " Q95:" + Convert.ToString(RiverLevelsOuseSkelton.gauge.additionalDataObject.percentile95) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " Q5:" + Convert.ToString(RiverLevelsOuseSkelton.gauge.additionalDataObject.percentile5) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " No. values " + Convert.ToString(RiverLevelsOuseSkelton.values.Count) + "\n");
                System.IO.File.WriteAllText(@"StageOuseSkelton.txt", "Date, stage, intervals" + "\n");
                int dayCounter = 0;
                float dayCumStage = 0;
                for (int i = 0; i < RiverLevelsOuseSkelton.values.Count; i++)
                {
                    // System.IO.File.AppendAllText(@"StageOuseSkelton.txt", Convert.ToString(RiverLevelsOuseSkelton.values[i].time) + "," + Convert.ToString(RiverLevelsOuseSkelton.values[i].value) + "\n");
                    dayCumStage = dayCumStage + (float)RiverLevelsOuseSkelton.values[i].value;
                    dayCounter++;
                    if ((Convert.ToString(RiverLevelsOuseSkelton.values[i].time)).Contains("00:00:00"))
                    {
                        dailyStageOuseSkelton = dayCumStage / dayCounter;
                        System.IO.File.AppendAllText(@"StageOuseSkelton.txt", Convert.ToString(RiverLevelsOuseSkelton.values[i].time) + " , " + Convert.ToString(dailyStageOuseSkelton) + " , " + dayCounter + "\n");
                        dayCounter = 0;
                        dayCumStage = 0;
                    }
                }
            }

            using (WebClient wcSH = new WebClient())
            {
                wcSH.Headers["SessionHeaderId"] = OverallSessionId;
                string GaugeData = string.Empty;
                GaugeData =
                    wcSH.DownloadString("http://riverlevelsapi.shoothill.com/TimeSeries/GetTimeSeriesRecentDatapoints/?stationId=1898&dataType=3&numberDays=1");
                dynamic RiverLevelsFossHuntington = JsonConvert.DeserializeObject<dynamic>(GaugeData);
                System.IO.File.AppendAllText(directoryOutput, Convert.ToString(RiverLevelsFossHuntington) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " River gauge id:" + Convert.ToString(RiverLevelsFossHuntington.gauge.id) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " Q95:" + Convert.ToString(RiverLevelsFossHuntington.gauge.additionalDataObject.percentile95) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " Q5:" + Convert.ToString(RiverLevelsFossHuntington.gauge.additionalDataObject.percentile5) + "\n");
                System.IO.File.AppendAllText(directoryOutput, " No. values " + Convert.ToString(RiverLevelsFossHuntington.values.Count) + "\n");
                System.IO.File.WriteAllText(@"StageFossHuntington.txt", "Date, Stage, intervals" + "\n");
                int dayCounter = 0;
                float dayCumStage = 0;
                for (int i = 0; i < RiverLevelsFossHuntington.values.Count; i++)
                {
                    // System.IO.File.AppendAllText(@"StageFossHuntington.txt", Convert.ToString(RiverLevelsFossHuntington.values[i].time) + "," + Convert.ToString(RiverLevelsFossHuntington.values[i].value) + "\n");
                    dayCumStage = dayCumStage + (float)RiverLevelsFossHuntington.values[i].value;
                    dayCounter++;
                    if ((Convert.ToString(RiverLevelsFossHuntington.values[i].time)).Contains("00:00:00"))
                    {
                        dailyStageFossHuntington = dayCumStage / dayCounter;
                        System.IO.File.AppendAllText(@"StageFossHuntington.txt", Convert.ToString(RiverLevelsFossHuntington.values[i].time) + " , " + Convert.ToString(dailyStageFossHuntington) + " , " + dayCounter + " \n");
                        dayCounter = 0;
                        dayCumStage = 0;
                    }
                }
            }

        }
        public double GetFlowOuseSkelton()
        {
            // convert stage to flow for Ouse at Skelton
            dailyFlowOuseSkelton = SDCoeffAOuseSkelton * Math.Pow((dailyStageOuseSkelton - SDMinStageOuseSkelton), SDCoeffBOuseSkelton);
            return dailyFlowOuseSkelton;
        }
        public double GetFlowFossHuntington()
        {
            // comvert stage to flow for Foss at Huntington
            dailyFlowFossHuntington = SDCoeffAFlossHuntinton * Math.Pow((dailyStageFossHuntington - SDMinStageFlossHuntington), SDCoeffBFlossHuntington);
            return dailyStageFossHuntington;
        }
    }
}
