using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNano
{
    class Program
    {
        private string fileRiverInput = String.Empty;
        static void Main(string[] args)
        {
            BusMovement testBM = new BusMovement("BusMovement.txt");
            HydroZones testHZ = new HydroZones("CityZones.txt");
            River testR = new River("Riverinput.txt");
            Rainfall testRf = new Rainfall("TestRF.out");
            RiverFlow testRFlow = new RiverFlow("TestRFlow.out");
            // Test output
            Console.Write(" Rainfall is: " + testRf.GetcumRF() + "\n");
            Console.Write(" Riverflow at Ouse at Skelton is: " + testRFlow.GetFlowOuseSkelton() + "\n");
            Console.Write(" Riverflow at Foss at Huntington is: " + testRFlow.GetFlowFossHuntington() +"\n");
            Console.Write(" Flow at the furthest downstream node is: " + testR.calcRiverFlow(testHZ, testBM, testRf, testRFlow)+"\n");
        }
    }
}
