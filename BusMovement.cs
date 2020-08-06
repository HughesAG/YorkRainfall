using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNano
{
    class BusMovement
    {
        private string lineBusMovementInput = String.Empty;
        private int numCityZones = 0;
        private double[] ROEmissions = new double[13];
        public BusMovement(string filenameBusMovement)
        {
            Console.WriteLine(" Creating BusMovement object");
            System.IO.StreamReader fileBusMovementInput = new System.IO.StreamReader(filenameBusMovement);
            lineBusMovementInput = fileBusMovementInput.ReadLine();
            numCityZones = int.Parse(lineBusMovementInput);
            Console.WriteLine("Number of River reaches is: " + numCityZones + "\n");
            // read dummy line
            lineBusMovementInput = fileBusMovementInput.ReadLine();
            // Read input file and create reach objects
            for (int i = 1; i <= numCityZones; i++)
            {
                lineBusMovementInput = fileBusMovementInput.ReadLine();
                var Tokens = lineBusMovementInput.Split();
                // populate array with ROEmission
                ROEmissions[i] = double.Parse(Tokens[4]);
            }
        }
        public double GetROEmissions(int CityZone)
        {
            return ROEmissions[CityZone];
        }
    }
}
