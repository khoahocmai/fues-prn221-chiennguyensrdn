using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class BanRepository : IBanRepository
    {
        public Task<List<Ban>> GetBans()
            => BanDAO.Instance.GetBans();

        public Task<Ban> GetBanById(int id)
            => BanDAO.Instance.GetBanById(id);

        public Task AddBan(Ban ban)
            => BanDAO.Instance.AddBan(ban);

        public Task UpdateBan(Ban ban)
            => BanDAO.Instance.UpdateBan(ban);

        public Task RemoveBan(int id)
            => BanDAO.Instance.RemoveBan(id);
    }
}
