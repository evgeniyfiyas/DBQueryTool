using System;
using System.Collections.Generic;
using System.Linq;
using DBQueryTool.DataAccess.Models;
using DBQueryTool.DataAccess.Service.Interfaces;
using DBQueryTool.Migrations;

namespace DBQueryTool.DataAccess.Service.Impl
{
    public class ReportService : IReportService
    {
        public Report Add(Report entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                dbContext.Reports.Add(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public List<Report> GetAll()
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                return dbContext.Reports.ToList();
            }
        }

        public Report Update(Report entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                var found = dbContext.Reports.Find(entity);
                if (found == null)
                {
                    return null;
                }

                dbContext.Reports.Add(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public void Remove(Report entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                dbContext.Reports.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}