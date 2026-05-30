using eBuyCars.Domain.Entities.User;
using eBuyCars.Domain.Models.User;
using eBuyCars.Domains.Entities.User;

namespace eBuyCars.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
        UserData? GetUserByCookie(string tokenValue);
        void UserLogout(string tokenValue);
    }
}
