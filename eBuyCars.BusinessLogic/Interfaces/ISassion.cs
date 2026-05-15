using BuyCars.Domain.Entities.User;
using BuyCars.Domain.Models.User;
using eBuyCars.Domains.Entities.User;

namespace BuyCars.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
        UserData? GetUserByCookie(string tokenValue);
        void UserLogout(string tokenValue);
    }
}
