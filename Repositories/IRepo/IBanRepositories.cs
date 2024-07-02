using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

public interface IBanRepository
{
    Task<List<Ban>> GetBans();
    Task<Ban> GetBanById(int id);
    Task AddBan(Ban ban);
    Task UpdateBan(Ban ban);
    Task RemoveBan(int id);
}
