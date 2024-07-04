using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        public static readonly object Lock = new object();
        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<List<User>> GetCustomers()
        {
            using var db = new FUESManagementContext();
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetCustomerById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.Users.SingleOrDefaultAsync(er => er.Id == id);
        }

        public async Task UpdateUser(User user)
        {
            using var db = new FUESManagementContext();
            var existingUser = await db.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;

                db.Users.Update(existingUser);
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveCustomer(User user)
        {
            using var db = new FUESManagementContext();
            var u = await db.Users.SingleOrDefaultAsync(m => m.Id == user.Id);
            if (u != null)
            {
                db.Users.Remove(u);
                await db.SaveChangesAsync();
            }
        }

        public async Task<User> Login(string username, string password)
        {
            using var db = new FUESManagementContext();
            return await db.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }


        public async Task Register(User user)
        {
            using var db = new FUESManagementContext();
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<bool> ResetPassword(int userId, string oldPassword, string newPassword)
        {
            using var db = new FUESManagementContext();
            var user = await db.Users.FindAsync(userId);
            if (user != null && user.Password == oldPassword)
            {
                user.Password = newPassword;
                db.Users.Update(user);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AssignRole(int userId, string role)
        {
            using var db = new FUESManagementContext();
            var user = await db.Users.FindAsync(userId);
            if (user != null && (role == "Buyer" || role == "Seller" || role == "Administrator"))
            {
                user.Role = role;
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalUsersByRole(string role)
        {
            using var db = new FUESManagementContext();
            return await db.Users.CountAsync(u => u.Role == role);
        }

        public async Task<List<User>> GetUsersByRole(string role)
        {
            using var db = new FUESManagementContext();
            return await db.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }
    }
}
