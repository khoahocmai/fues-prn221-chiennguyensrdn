using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;

namespace DataAccessObjects.DAO
{
    public class UserDAO
    {
        private readonly FUESManagementContext _context;

        public UserDAO(FUESManagementContext context)
        {
            _context = context;
        }

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
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetCustomerById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(er => er.Id == id);
        }

        public async Task UpdateUser(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveCustomer(User user)
        {
            var u = await _context.Users.SingleOrDefaultAsync(m => m.Id == user.Id);
            if (u != null)
            {
                _context.Users.Remove(u);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> Login(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }


        public async Task Register(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ResetPassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null && user.Password == oldPassword)
            {
                user.Password = newPassword;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AssignRole(int userId, string role)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null && (role == "Guest" || role == "Buyer" || role == "Seller" || role == "Administrator"))
            {
                user.Role = role;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
