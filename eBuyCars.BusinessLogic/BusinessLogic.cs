using eBuyCars.BusinessLogic.Core;
using eBuyCars.BusinessLogic.Interfaces;

namespace eBuyCars.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL() => new UserActions();

        public IUserService GetUserBL() => new UserActions();

        public ICarService GetCarBL() => new CarActions();

        public IFavoriteService GetFavoriteBL() => new FavoriteActions();
    }
}
