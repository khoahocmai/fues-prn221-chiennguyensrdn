using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class BanDAO
    {
        private readonly FUESManagementContext _context;

        public BanDAO(FUESManagementContext context)
        {
            _context = context;
        }

        private static BanDAO instance = null;
        public static readonly object Lock = new object();
        private BanDAO() { }
        public static BanDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new BanDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Ban>> GetBans()
        {
            return await _context.Bans.ToListAsync();
        }

        public async Task<Ban> GetBanById(int id)
        {
            return await _context.Bans.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBan(Ban ban)
        {
            await _context.Bans.AddAsync(ban);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBan(Ban ban)
        {
            var existingBan = await _context.Bans.FindAsync(ban.Id);
            if (existingBan != null)
            {
                existingBan.UserId = ban.UserId;
                existingBan.AdminId = ban.AdminId;
                existingBan.Reason = ban.Reason;
                existingBan.StartDate = ban.StartDate;
                existingBan.EndDate = ban.EndDate;

                _context.Bans.Update(existingBan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveBan(int id)
        {
            var ban = await _context.Bans.FindAsync(id);
            if (ban != null)
            {
                _context.Bans.Remove(ban);
                await _context.SaveChangesAsync();
            }
        }
    }
}
