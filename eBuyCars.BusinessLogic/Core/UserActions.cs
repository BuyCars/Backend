using eBuyCars.BusinessLogic.Interfaces;
using eBuyCars.BusinessLogic.Structure;
using eBuyCars.DataAccess.Context;
using eBuyCars.Domain.Entities.User;
using eBuyCars.Domain.Models.User;
using eBuyCars.Domains.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace eBuyCars.BusinessLogic.Core
{
    public class UserActions : ISession, IUserService
    {
        public ULoginResp UserLogin(ULoginData data)
        {
            var validate = new EmailAddressAttribute();
            UserData? user;

            using (var db = new BuyCarsContext())
            {
                var hashedPassword = TokenService.HashPassword(data.Password);

                if (validate.IsValid(data.Credential))
                {
                    user = db.Users.FirstOrDefault(u =>
                        u.Email == data.Credential && u.Password == hashedPassword);
                }
                else
                {
                    user = db.Users.FirstOrDefault(u =>
                        u.UserName == data.Credential && u.Password == hashedPassword);
                }
            }

            if (user == null)
            {
                return new ULoginResp
                {
                    Status = false,
                    StatusMsg = "Неверный логин или пароль"
                };
            }

            var token = TokenService.GenerateToken();

            using (var db = new BuyCarsContext())
            {
                var dbUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
                if (dbUser != null)
                {
                    dbUser.CurrentToken = token;
                    dbUser.TokenExpiry = DateTime.UtcNow.AddMinutes(60);
                    dbUser.LastLogin = data.LoginDateTime;
                    dbUser.LasIp = data.LoginIp;
                    db.SaveChanges();
                }
            }

            return new ULoginResp
            {
                Status = true,
                StatusMsg = "Успешный вход",
                Token = token,
                Role = user.Role,
                UserId = user.Id,
                UserName = user.UserName
            };
        }

        public UserData? GetUserByCookie(string tokenValue)
        {
            if (string.IsNullOrEmpty(tokenValue)) return null;

            using var db = new BuyCarsContext();
            return db.Users.FirstOrDefault(u =>
                u.CurrentToken == tokenValue &&
                u.TokenExpiry != null &&
                u.TokenExpiry > DateTime.UtcNow);
        }

        public void UserLogout(string tokenValue)
        {
            using var db = new BuyCarsContext();
            var user = db.Users.FirstOrDefault(u => u.CurrentToken == tokenValue);
            if (user != null)
            {
                user.CurrentToken = null;
                user.TokenExpiry = null;
                db.SaveChanges();
            }
        }

        public ULoginResp Register(UserRegisterDto dto, string ip)
        {
            using var db = new BuyCarsContext();

            if (db.Users.Any(u => u.Email == dto.Email))
            {
                return new ULoginResp { Status = false, StatusMsg = "Email уже используется" };
            }
            if (db.Users.Any(u => u.UserName == dto.UserName))
            {
                return new ULoginResp { Status = false, StatusMsg = "Username уже занят" };
            }

            var newUser = new UserData
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Email = dto.Email,
                Password = TokenService.HashPassword(dto.Password),
                Phone = dto.Phone,
                Role = "user",
                RegisteredOn = DateTime.UtcNow,
                LasIp = ip
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            var loginData = new ULoginData
            {
                Credential = dto.Email,
                Password = dto.Password,
                LoginIp = ip,
                LoginDateTime = DateTime.UtcNow
            };

            return UserLogin(loginData);
        }

        public List<UserData> GetAllUsers()
        {
            using var db = new BuyCarsContext();
            return db.Users.ToList();
        }

        public UserData? GetUserById(int id)
        {
            using var db = new BuyCarsContext();
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public UserData? UpdateUser(int id, UserRegisterDto dto)
        {
            using var db = new BuyCarsContext();
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return null;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Phone = dto.Phone;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.Password = TokenService.HashPassword(dto.Password);
            }

            db.SaveChanges();
            return user;
        }

        public bool DeleteUser(int id)
        {
            using var db = new BuyCarsContext();
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            db.Users.Remove(user);
            db.SaveChanges();
            return true;
        }
    }
}
