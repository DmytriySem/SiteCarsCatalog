using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InitializeDB
{
    public class CatalogDBInitializer
    {
        internal readonly string[] brands = { "audi", "bmw", "mercedes_benz", "mini", "volkswagen" };
        internal const int brandsCount = 5;
        
        internal readonly string[][] models = new string[brandsCount][];
        internal readonly string path = "C:/Users/DEV/source/repos/Catalog/Catalog/DAL/Images/";
        internal readonly string[] descriptionCarWords = { "dynamic", "economical", "functional", "safe", "elegant",
            "luxury", "sporty", "extreme", "ultimate", "ultra", "automatic", "top-level", "agile", "front-wheel drive",
            "expensive", "astonishing", "limitless"};

        internal readonly string[] colors = { "red", "green", "orange", "blue", "purple", "grey" };
        internal readonly float[] VolumeEngine = new float[] { 1.0F, 1.2F, 1.5F, 1.6F, 2.0F, 2.2F, 2.4F, 2.5F, 3.0F, 4.0F };

        internal readonly Random rand;

        internal const int MIN_CARS_LIST = 10; //2;
        internal const int MAX_CARS_LIST = 20; //10;

        internal const int MIN_COSTS_LIST = 2;
        internal const int MAX_COSTS_LIST = 10;

        internal const int MIN_DESCR_CAR_LIST = 2;
        internal const int MAX_DESCR_CAR_LIST = 7;

        internal const int MIN_CAR_YEAR = 1995;
        internal const int MAX_CAR_YEAR = 2018;

        internal const int FIRST_MONTH = 1;
        internal const int LAST_MONTH = 12;

        internal const double MIN_PRICE = 2000.00;
        internal const double MAX_PRICE = 20000.00;

        public CatalogDBInitializer()
        {
            models[0] = new string[2] { "A3", "A6" };
            models[1] = new string[2] { "i3", "X7" };
            models[2] = new string[2] { "Vito", "Sprinter" };
            models[3] = new string[3] { "Cabrio", "Clubman", "Roadster" };
            models[4] = new string[5] { "Jetta", "Golf", "Polo", "Transporter", "Touareg" };

            rand = new Random();
        }

        internal string getRandomColor()
        {
            return colors[rand.Next(0, colors.Length)];
        }

        internal float getRandomVolumeEngine()
        {
            return VolumeEngine[rand.Next(0, VolumeEngine.Length - 1)];
        }

        internal DateTime getRandomDate()
        {
            int year = rand.Next(MIN_CAR_YEAR, MAX_CAR_YEAR);
            int month = rand.Next(FIRST_MONTH, LAST_MONTH);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int day = rand.Next(1, daysInMonth);

            return new DateTime(year, month, day);
        }

        internal decimal getRandomPrice()
        {
            return (decimal)(rand.NextDouble() * (MAX_PRICE - MIN_PRICE) + MIN_PRICE);
        }

        internal string getRandomDescription()
        {
            string description = String.Empty;

            for (int f = 0; f < rand.Next(MIN_DESCR_CAR_LIST, MAX_DESCR_CAR_LIST);)
            {
                string word = descriptionCarWords[rand.Next(0, descriptionCarWords.Length - 1)];

                if (description.Contains(word))
                {
                    continue;
                }
                else
                {
                    description += word + ", ";
                    f++;
                }
            }

            return description.Substring(0, description.Length - 2);
        }
    }
}
