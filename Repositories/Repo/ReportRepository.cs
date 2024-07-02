using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ReportRepository : IReportRepository
    {
        public Task<List<Report>> GetReports()
            => ReportDAO.Instance.GetReports();

        public Task<Report> GetReportById(int id)
            => ReportDAO.Instance.GetReportById(id);

        public Task AddReport(Report report)
            => ReportDAO.Instance.AddReport(report);

        public Task UpdateReport(Report report)
            => ReportDAO.Instance.UpdateReport(report);

        public Task RemoveReport(int id)
            => ReportDAO.Instance.RemoveReport(id);
    }
}
