using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories.IRepo
{
    public interface IReportRepository
    {
        Task<List<Report>> GetReports();
        Task<Report> GetReportById(int id);
        Task<List<Report>> GetReportByProductId(int productId);
        Task AddReport(Report report);
        Task UpdateReport(Report report);
        Task RemoveReport(int id);
        Task<bool> HasBuyerAlreadyReportedProduct(int buyerId, int productId);
    }
}
