using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTSPWinForms
{
    class Tour : List<int>, IComparable<Tour>
    {
        private double distance;

        public void UpdateDistance(double[,] distances)
        {
            distance = 0;
            for (int i = 0; i < this.Count - 1; i++)
            {
                distance += distances[this[i], this[i + 1]];
            }
            distance += distances[this[0], this[this.Count - 1]];
        }

        public int Hash()
        {
            int hash = this[0];
            for (int i = 0; i < this.Count; i++)
            {
                hash ^= this[i];
            
            }
            return hash;
        }

        public double GetDistance()
        {
            return distance;
        }

        public void MakeRandomTour(int numberOfCities, Random rand)
        {
            for (int i = 0; i < numberOfCities; i++)
            {
                this.Add(i);
            }
            while (numberOfCities > 1)
            {
                int k = rand.Next(numberOfCities);
                numberOfCities--;
                int temp = this[k];
                this[k] = this[numberOfCities];
                this[numberOfCities] = temp;
            }
        }

        public int CompareTo(Tour other)
        {
            /*if (Math.Abs(this.distance - other.distance) < 0.1)
            {
                return 0;
            }
            else if (this.distance>other.distance)
            {
                return 1;
            }
            else
            {
                return -1;
            }*/
            if (this.distance > other.distance)
            {
                return 1;
            }
            if (this.distance < other.distance)
            {
                return -1;
            }
            return 0;
        }
    }
}
