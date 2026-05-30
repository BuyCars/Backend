using eBuyCars.Domain.Entities.Favorite;

namespace eBuyCars.BusinessLogic.Interfaces
{
    public interface IFavoriteService
    {
        List<FavoriteData> GetUserFavorites(int userId);
        FavoriteData? AddFavorite(int userId, int carId);
        bool RemoveFavorite(int userId, int carId);
        bool IsFavorite(int userId, int carId);
    }
}
