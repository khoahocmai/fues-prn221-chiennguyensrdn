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
            using var db = new FUESManagementContext();
            return await db.Bans
                .Include(b => b.Product)
                .Include(b => b.Moderator)
                .ToListAsync();
        }

        public async Task<Ban> GetBanById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.Bans.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Ban>> GetBanByModeratorId(int moderatorId)
        {
            using var db = new FUESManagementContext();
            return await db.Bans
                .Where(b => b.ModeratorId == moderatorId)
                .Include(b => b.Product)
                .ToListAsync();
        }

        public async Task AddBan(Ban ban)
        {
            using var db = new FUESManagementContext();
            await db.Bans.AddAsync(ban);
            await db.SaveChangesAsync();
        }

        public async Task UpdateBan(Ban ban)
        {
            using var db = new FUESManagementContext();
            var existingBan = await db.Bans.FindAsync(ban.Id);
            if (existingBan != null)
            {
                existingBan.ProductId = ban.ProductId;
                existingBan.ModeratorId = ban.ModeratorId;
                existingBan.Reason = ban.Reason;
                existingBan.StartDate = ban.StartDate;
                existingBan.EndDate = ban.EndDate;

                db.Bans.Update(existingBan);
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveBan(int id)
        {
            using var db = new FUESManagementContext();
            var ban = await db.Bans.FindAsync(id);
            if (ban != null)
            {
                db.Bans.Remove(ban);
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> GetBannedProducts()
        {
            using var db = new FUESManagementContext();
            return await db.Bans
                  .Where(b => b.EndDate == null || b.EndDate > DateTime.Now)
                  .CountAsync();
        }
    }
}
