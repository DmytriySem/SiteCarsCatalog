using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICarService
    {
        CarPageDTO GetCarPageData(int modelId);
        IEnumerable<CarDTO> GetAllCars(int modelId);
        void AddCar(CarDTO carDTO);
        void UpdateCar(CarDTO carDTO);
        void DeleteCar(CarDTO carDTO);
    }
}
