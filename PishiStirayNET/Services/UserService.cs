using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models.DbEntities;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PishiStirayNET.Services
{
    internal class UserService
    {
        private readonly TradeContext _trade;

        public UserService(TradeContext trade)
        {
            _trade = trade;
        }

        public async Task<bool> Authorization(string userLogin, string userPassword)
        {
            UserDB user = await _trade.Users.Where(user => user.UserLogin == userLogin && user.UserPassword == userPassword).SingleOrDefaultAsync();

           
                if (user != null)
                {

                    CurrentUser.User = new Models.User
                    {
                        UserID = user.UserId,
                        UserName = user.UserName,
                        UserLogin = user.UserLogin,
                        UserPassword = user.UserPassword,
                        UserPatronymic = user.UserPatronymic,
                        UserRole = user.UserRoleNavigation.RoleName,
                        UserSurname = user.UserSurname
                    };

                   return true;
                }
                return false;
            
        }
    }
}
