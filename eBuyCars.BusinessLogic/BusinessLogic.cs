using BuyCars.BusinessLogic.Core;
using BuyCars.BusinessLogic.Interfaces;

namespace BuyCars.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL() => new UserActions();

        public IUserService GetUserBL() => new UserActions();

        public ICarService GetCarBL() => new CarActions();

        public IFavoriteService GetFavoriteBL() => new FavoriteActions();
    }
}
