using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repo
{
    public class BanRepository : IBanRepository
    {
        public Task<List<Ban>> GetBans()
            => BanDAO.Instance.GetBans();

        public Task<Ban> GetBanById(int id)
            => BanDAO.Instance.GetBanById(id);

        public Task<List<Ban>> GetBanByModeratorId(int moderatorId)
            => BanDAO.Instance.GetBanByModeratorId(moderatorId);

        public Task AddBan(Ban ban)
            => BanDAO.Instance.AddBan(ban);

        public Task UpdateBan(Ban ban)
            => BanDAO.Instance.UpdateBan(ban);

        public Task RemoveBan(int id)
            => BanDAO.Instance.RemoveBan(id);

        public Task<int> GetBannedProducts()
            => BanDAO.Instance.GetBannedProducts();
    }
}
