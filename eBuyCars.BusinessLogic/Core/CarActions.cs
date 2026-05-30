using eBuyCars.BusinessLogic.Interfaces;
using eBuyCars.DataAccess.Context;
using eBuyCars.Domain.Entities.Car;
using eBuyCars.Domain.Models.Car;


namespace eBuyCars.BusinessLogic.Core
{
    public class CarActions : ICarService
    {
        public List<CarData> GetAllCars(
            string? category = null,
            string? condition = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? search = null)
        {
            using var db = new BuyCarsContext();

            var query = db.Cars.Where(c => c.IsActive).AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(c => c.Category == category);

            if (!string.IsNullOrEmpty(condition))
                query = query.Where(c => c.Condition == condition);

            if (minPrice.HasValue)
                query = query.Where(c => c.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(c => c.Price <= maxPrice.Value);

            if (!string.IsNullOrEmpty(search))
            {
                var s = search.ToLower();
                query = query.Where(c =>
                    c.Title.ToLower().Contains(s) ||
                    c.Brand.ToLower().Contains(s) ||
                    c.Model.ToLower().Contains(s));
            }

            return query.OrderByDescending(c => c.CreatedAt).ToList();
        }

        public CarData? GetCarById(int id)
        {
            using var db = new BuyCarsContext();
            return db.Cars.FirstOrDefault(c => c.Id == id && c.IsActive);
        }

        public CarData CreateCar(CarCreateDto dto, int userId)
        {
            using var db = new BuyCarsContext();

            var car = new CarData
            {
                Title = string.IsNullOrWhiteSpace(dto.Title)
                    ? $"{dto.Brand} {dto.Model}"
                    : dto.Title,
                Brand = dto.Brand,
                Model = dto.Model,
                Price = dto.Price,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category,
                Year = dto.Year,
                Mileage = dto.Mileage,
                Fuel = dto.Fuel,
                Transmission = dto.Transmission,
                Condition = dto.Condition,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            db.Cars.Add(car);
            db.SaveChanges();
            return car;
        }

        public CarData? UpdateCar(int id, CarCreateDto dto, int userId, string userRole)
        {
            using var db = new BuyCarsContext();
            var car = db.Cars.FirstOrDefault(c => c.Id == id && c.IsActive);

            if (car == null) return null;

            if (car.UserId != userId && userRole != "admin") return null;

            car.Title = string.IsNullOrWhiteSpace(dto.Title)
                ? $"{dto.Brand} {dto.Model}"
                : dto.Title;
            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.Price = dto.Price;
            car.Description = dto.Description;
            car.ImageUrl = dto.ImageUrl;
            car.Category = dto.Category;
            car.Year = dto.Year;
            car.Mileage = dto.Mileage;
            car.Fuel = dto.Fuel;
            car.Transmission = dto.Transmission;
            car.Condition = dto.Condition;

            db.SaveChanges();
            return car;
        }

        public bool DeleteCar(int id, int userId, string userRole)
        {
            using var db = new BuyCarsContext();
            var car = db.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null) return false;

            if (car.UserId != userId && userRole != "admin") return false;

            car.IsActive = false;
            db.SaveChanges();
            return true;
        }
    }
}
