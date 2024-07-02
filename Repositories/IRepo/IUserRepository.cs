using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepo
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task UpdateUser(User user);
        Task RemoveUser(User user);
        Task<User> Login(string username, string password);
        Task Register(User user);
        Task<bool> ResetPassword(int userId, string oldPassword, string newPassword);
        Task AssignRole(int userId, string role);
    }
}
