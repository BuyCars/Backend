using eBuyCars.Domain.Entities.Car;
using eBuyCars.Domain.Models.Car;

namespace eBuyCars.BusinessLogic.Interfaces
{
    public interface ICarService
    {
        List<CarData> GetAllCars(string? category = null, string? condition = null,
                                  decimal? minPrice = null, decimal? maxPrice = null,
                                  string? search = null);
        CarData? GetCarById(int id);
        CarData CreateCar(CarCreateDto dto, int userId);
        CarData? UpdateCar(int id, CarCreateDto dto, int userId, string userRole);
        bool DeleteCar(int id, int userId, string userRole);
    }
}
