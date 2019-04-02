using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        public string Color { get; set; }
        public double VolumeEngine { get; set; }
        public string Description { get; set; }

        public List<Cost> Prices { get; set; }

        public Car()
        {
            Prices = new List<Cost>();
        }
    }
}
