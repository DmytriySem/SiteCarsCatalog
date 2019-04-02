using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Model : AbsModel
    {
        public string PhotoUrl { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public List<Car> Cars { get; set; }

        public Model()
        {
            Cars = new List<Car>();
        }
    }
}
