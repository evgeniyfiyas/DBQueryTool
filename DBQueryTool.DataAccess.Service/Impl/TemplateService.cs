using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using DBQueryTool.DataAccess.Models;
using DBQueryTool.DataAccess.Service.Interfaces;
using DBQueryTool.Migrations;

namespace DBQueryTool.DataAccess.Service.Impl
{
    public class TemplateService : ITemplateService
    {
        public Template Add(Template entity)
        {
            using (var dbContext = new DBQueryTool.Migrations.DBQueryToolDbContext())
            {
                dbContext.Templates.Add(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public List<Template> GetAll()
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                return dbContext.Templates.ToList();
            }
        }

        public Template Update(Template entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                var found = dbContext.Templates.Find(entity);
                if (found == null) return null;

                dbContext.Templates.Add(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public void Remove(Template entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                dbContext.Templates.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public ObservableCollection<Template> GetObservableCollection()
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                dbContext.Templates.Load();
                return dbContext.Templates.Local;
            }
        }
    }
}