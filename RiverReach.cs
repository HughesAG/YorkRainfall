using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNano
{
    class RiverReach
    {
        private double reachLength = 0, flowWWTW = 0, flowAtNode = 0;
        private int numReach;
        private int upstreamReach = 0, downstreamReach = 0, tributaryReach = 0;
        private int numCityZones = 0;
        private int[] CityZonesID = new int[3];
        public RiverReach(int numR, int usR, int dsR, int tribR, double reachL, double qWWTW, int numCZ, int[] CZID)
        {
            // Constructor for RiverReach class
            Console.Write(" River reach object no: " + numR);
            numReach = numR;
            Console.Write(" Upstream reach is: " + usR);
            upstreamReach = usR;
            Console.Write(" Downstream reach is: " + dsR);
            downstreamReach = dsR;
            Console.Write(" Tributary is: " + tribR);
            tributaryReach = tribR;
            Console.Write(" reach lenght is: " + reachL);
            reachLength = reachL;
            Console.Write(" flow from WWTW is : " + qWWTW);
            flowWWTW = qWWTW;
            Console.Write(" Number of CityZones associated with reach is : " + numCZ);
            numCityZones = numCZ;
            if (numCityZones > 0)
            {
                for (int iCZ = 1; iCZ <= numCityZones; iCZ++)
                {
                    Console.Write(" CityZones connected :" + CZID[iCZ]);
                    CityZonesID[iCZ] = CZID[iCZ];
                }
            }else
            {
                Console.Write(" None connected ");
            }
            Console.Write("\n");
        }
        // Get methods
        public int GetNumberReach()
        {
            return numReach;
        }
        public int GetUpstreamReach()
        {
            return upstreamReach;
        }
        public int GetDownstreamReach()
        {
            return downstreamReach;
        }
        public int GetTributaryReach()
        {
            return tributaryReach;
        }
        public double GetReachLength()
        {
            return reachLength;
        }
        public double GetFlowWWTW()
        {
            return flowWWTW;
        }
        public double GetFlowAtNode()
        {
            return flowAtNode;
        }
        public int GetNumCZs()
        {
            return numCityZones;
        }
        public int GetIDCZ(int numCZelement)
        {
            return CityZonesID[numCZelement];
        }
        // Set methods
        public void SetFlowAtNode(double qNode)
        {
            flowAtNode = qNode;
            return;
        }
    }
}
