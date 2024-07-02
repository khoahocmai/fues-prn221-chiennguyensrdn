using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repo
{
    public class UserRepository : IUserRepository
    {
        public Task<List<User>> GetUsers()
            => UserDAO.Instance.GetCustomers();

        public Task<User> GetUserById(int id)
            => UserDAO.Instance.GetCustomerById(id);

        public Task UpdateUser(User user)
            => UserDAO.Instance.UpdateUser(user);

        public Task RemoveUser(User user)
            => UserDAO.Instance.RemoveCustomer(user);

        public Task<User> Login(string username, string password)
            => UserDAO.Instance.Login(username, password);

        public Task Register(User user)
            => UserDAO.Instance.Register(user);

        public Task<bool> ResetPassword(int userId, string oldPassword, string newPassword)
            => UserDAO.Instance.ResetPassword(userId, oldPassword, newPassword);

        public Task AssignRole(int userId, string role)
            => UserDAO.Instance.AssignRole(userId, role);
    }
}
