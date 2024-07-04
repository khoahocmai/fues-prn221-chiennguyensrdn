using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class ReportDAO
    {
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
            using var db = new FUESManagementContext();
            return await db.Reports.ToListAsync();
        }

        public async Task<Report> GetReportById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.Reports.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddReport(Report report)
        {
            using var db = new FUESManagementContext();
            await db.Reports.AddAsync(report);
            await db.SaveChangesAsync();
        }

        public async Task UpdateReport(Report report)
        {
            using var db = new FUESManagementContext();
            var existingReport = await db.Reports.FindAsync(report.Id);
            if (existingReport != null)
            {
                existingReport.ReporterId = report.ReporterId;
                existingReport.ProductId = report.ProductId;
                existingReport.Status = report.Status;
                existingReport.UpdatedAt = DateTime.Now;

                db.Reports.Update(existingReport);
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveReport(int id)
        {
            using var db = new FUESManagementContext();
            var report = await db.Reports.FindAsync(id);
            if (report != null)
            {
                db.Reports.Remove(report);
                await db.SaveChangesAsync();
            }
        }
    }
}
