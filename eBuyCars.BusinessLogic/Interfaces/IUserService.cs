using eBuyCars.Domain.Entities.User;
using eBuyCars.Domain.Models.User;

namespace eBuyCars.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        ULoginResp Register(UserRegisterDto dto, string ip);
        List<UserData> GetAllUsers();
        UserData? GetUserById(int id);
        UserData? UpdateUser(int id, UserRegisterDto dto);
        bool DeleteUser(int id);
    }
}
