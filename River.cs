using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNano
{
    class River
    {
        private int numRiverReaches = 12, iUpstream = 0;
        private RiverReach[] riverReach = new RiverReach[12];
        private string lineRiverInput = String.Empty;
        private double flowAtNode = 0, inflowCityZone = 0, flowUpstream = 0, flowTributary = 0;
        private int numCityZones = 0;
        private int[] CityZonesID = new int[3];
        public River(string filenameRiverInput)
        {
            // Constructor for River class
            Console.Write("Creating River object\n");
            System.IO.StreamReader fileRiverInput = new System.IO.StreamReader(filenameRiverInput);
            lineRiverInput = fileRiverInput.ReadLine();
            numRiverReaches = int.Parse(lineRiverInput);
            Console.WriteLine("Number of River reaches is: "+numRiverReaches+"\n");
            // Read input file and create reach objects
            for (int i = 1;  i <= numRiverReaches; i++)
            {
                lineRiverInput = fileRiverInput.ReadLine();
                var Tokens = lineRiverInput.Split();
                // populate array with CityZones IDs
                numCityZones = int.Parse(Tokens[6]);
                if(numCityZones > 0)
                {
                    for (int iCZ = 1; iCZ <= numCityZones; iCZ++)
                    {
                        CityZonesID[iCZ] = int.Parse(Tokens[6 + iCZ]);
                    }
                }
                Console.Write("\n");
                riverReach[i] = new RiverReach(i, int.Parse(Tokens[1]), int.Parse(Tokens[2]), int.Parse(Tokens[3]), float.Parse(Tokens[4]), float.Parse(Tokens[5]), numCityZones, CityZonesID);
            }
        }
        public double calcRiverFlow(HydroZones HZ, BusMovement BM, Rainfall Rf, RiverFlow RFlow)
        {
            double cumArea = 0, cumConc = 0;
            for(int i=1; i <= numRiverReaches; i++)
            {
                // Get upstream node and check if node exists or not
                iUpstream = riverReach[i].GetUpstreamReach();
                if(iUpstream != 0)
                {
                    flowUpstream = riverReach[iUpstream].GetFlowAtNode();
                }else
                {
                    // Check to see if need to add other inflows
                    if(i == 1)
                    {
                        // Add in flow for Ouse at Skelton
                        flowUpstream = RFlow.GetFlowOuseSkelton() * 86400;
                    }else if(i == 6)
                    {
                        // Add in flow for Foss at Huntington
                        flowUpstream = RFlow.GetFlowFossHuntington() * 86400;
                    }
                    else
                    {
                        flowUpstream = 0;
                    }
                    
                }
                // Get tributary node and check if it exists or not
                iUpstream = riverReach[i].GetTributaryReach();
                if (iUpstream != 0)
                {
                    flowTributary = riverReach[iUpstream].GetFlowAtNode();
                }
                else
                {
                    flowTributary = 0;
                }
                // Calc inflow from CityZone associated with river reach (if any)
                numCityZones = riverReach[i].GetNumCZs();
                if(numCityZones > 0)
                {
                    inflowCityZone = 0;
                    for(int iCZs = 1; iCZs <= numCityZones; iCZs++)
                    {
                        int IDCZ = riverReach[i].GetIDCZ(iCZs);
                        inflowCityZone =  inflowCityZone + ( Rf.GetcumRF() / 1000 ) * HZ.GetOverallAreaHZ(IDCZ); 
                        cumArea = cumArea + HZ.GetUrbanAreaHZ(IDCZ);
                        cumConc = cumConc + BM.GetROEmissions(IDCZ) / HZ.GetUrbanAreaHZ(IDCZ);
                    }
                }else
                {
                    inflowCityZone = 0;
                }
                // calc flow based on flow from upstream node, tributary, inflow from city zone and any WWTW inflow
                flowAtNode = flowUpstream + flowTributary + inflowCityZone + riverReach[i].GetFlowWWTW();
                riverReach[i].SetFlowAtNode(flowAtNode);
            }
            Console.Write(" Cum. Area is : " + cumArea + "\n");
            Console.Write(" Cum. Conc is : " + cumConc + "\n");
            return riverReach[numRiverReaches].GetFlowAtNode();
        }

    }
}
