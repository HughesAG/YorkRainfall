using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNano
{

    class HydroZones
    {
        string lineCZInput = String.Empty;
        int numCityZones = 0;
        private CityZone[] cityZoneYork = new CityZone[13];

        public HydroZones(string filenameCityZones)
        {
            // Constructor for HydroZones class
            Console.Write("Creating HydroZones object\n");
            System.IO.StreamReader fileCityZones = new System.IO.StreamReader(filenameCityZones);
            lineCZInput = fileCityZones.ReadLine();
            numCityZones = int.Parse(lineCZInput);
            Console.WriteLine("Number of City Zones is: " + numCityZones + "\n");
            // read dummy line
            lineCZInput = fileCityZones.ReadLine();
            // Read input file and create reach objects
            for (int i = 1; i <= numCityZones; i++)
            {
                lineCZInput = fileCityZones.ReadLine();
                var Tokens = lineCZInput.Split();
                cityZoneYork[i] = new CityZone(i, Tokens[1], Tokens[2], double.Parse(Tokens[3]), double.Parse(Tokens[4]));
            }
        }
        public double GetOverallAreaHZ(int IDCZ)
        {
            return cityZoneYork[IDCZ].GetAreaCityZone();
        }
        public double GetUrbanAreaHZ(int IDCZ)
        {
            return cityZoneYork[IDCZ].GetUrbanAreaCityZone();
        }
    }
}

