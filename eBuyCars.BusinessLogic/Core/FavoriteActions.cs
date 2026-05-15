using BuyCars.BusinessLogic.Interfaces;
using BuyCars.DataAccess.Context;
using BuyCars.Domain.Entities.Favorite;

namespace BuyCars.BusinessLogic.Core
{
    public class FavoriteActions : IFavoriteService
    {
        public List<FavoriteData> GetUserFavorites(int userId)
        {
            using var db = new BuyCarsContext();
            return db.Favorites
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CreatedAt)
                .ToList();
        }

        public FavoriteData? AddFavorite(int userId, int carId)
        {
            using var db = new BuyCarsContext();

            if (db.Favorites.Any(f => f.UserId == userId && f.CarId == carId))
                return null;

            if (!db.Cars.Any(c => c.Id == carId && c.IsActive))
                return null;

            var favorite = new FavoriteData
            {
                UserId = userId,
                CarId = carId,
                CreatedAt = DateTime.UtcNow
            };

            db.Favorites.Add(favorite);
            db.SaveChanges();
            return favorite;
        }

        public bool RemoveFavorite(int userId, int carId)
        {
            using var db = new BuyCarsContext();
            var favorite = db.Favorites.FirstOrDefault(f => f.UserId == userId && f.CarId == carId);

            if (favorite == null) return false;

            db.Favorites.Remove(favorite);
            db.SaveChanges();
            return true;
        }

        public bool IsFavorite(int userId, int carId)
        {
            using var db = new BuyCarsContext();
            return db.Favorites.Any(f => f.UserId == userId && f.CarId == carId);
        }
    }
}
