using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories.IRepo
{
    public interface IReportRepository
    {
        Task<List<Report>> GetReports();
        Task<Report> GetReportById(int id);
        Task AddReport(Report report);
        Task UpdateReport(Report report);
        Task RemoveReport(int id);
    }
}
