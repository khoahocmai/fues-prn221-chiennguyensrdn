using BusinessObjects;
using Microsoft.EntityFrameworkCore;
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

        private static ReportDAO instance = null;
        public static readonly object Lock = new object();
        private ReportDAO() { }
        public static ReportDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new ReportDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Report>> GetReports()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> GetReportById(int id)
        {
            return await _context.Reports.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddReport(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReport(Report report)
        {
            var existingReport = await _context.Reports.FindAsync(report.Id);
            if (existingReport != null)
            {
                existingReport.ReporterId = report.ReporterId;
                existingReport.ProductId = report.ProductId;
                existingReport.Status = report.Status;
                existingReport.UpdatedAt = DateTime.Now;

                _context.Reports.Update(existingReport);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
