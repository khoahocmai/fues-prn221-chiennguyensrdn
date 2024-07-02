using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class ReportDAO
    {
        private readonly FUESManagementContext _context;

        public ReportDAO(FUESManagementContext context)
        {
            _context = context;
        }
        
        public Report GetReport(int id)
        {
            Report report = _context.Reports.Find(id);
            return report;
        }

        public List<Report> GetReports()
        {
            List<Report> reports = _context.Reports.ToList();
            return reports;
        }
        public void CreateReport(Report report)
        {
             _context.Reports.Add(report);
        }

        public void UpdateReport(Report report)
        {
            Report currentReport = _context.Reports.Find(report.Id);
            _context.Reports.Update(currentReport);
        }

        public void DeleteReport(int id)
        {
            Report report = _context.Reports.Find(id);
            _context.Reports.Remove(report);
        }
    }
}
