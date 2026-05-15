using BuyCars.Domain.Entities.User;
using BuyCars.Domain.Models.User;

namespace BuyCars.BusinessLogic.Interfaces
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
