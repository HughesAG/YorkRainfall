using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNano
{
    class CityZone
    {
        private int numCityZone = 0;
        private string nameCityZone = String.Empty;
        private double sizeCityZoneOverall = 0, sizeCityZoneUrban = 0;

        // Constructor for CityZone class
        public CityZone(int nCZ, string namCZ, string namRR, double sCZ, double sUCZ)
        {
            Console.WriteLine(" City Zone no: " + nCZ);
            numCityZone = nCZ;
            Console.WriteLine(" for CityZone: " + namCZ);
            nameCityZone = namCZ;
            Console.WriteLine(" for River Reach: " + namRR);
            Console.WriteLine(" which has size: " + sCZ +"\n");
            sizeCityZoneOverall = sCZ;
            Console.WriteLine(" which has Urban size: " + sUCZ + "\n");
            sizeCityZoneUrban = sUCZ;
        }
        public int GetIDCityZone()
        {
            return numCityZone;
        }
        public string GetNameCityZone()
        {
            return nameCityZone;
        }
        public double GetAreaCityZone()
        {
            return sizeCityZoneOverall;
        }
        public double GetUrbanAreaCityZone()
        {
            return sizeCityZoneUrban;
        }
    }
}
