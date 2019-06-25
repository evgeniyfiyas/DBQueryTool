using System.Collections.Generic;
using System.Linq;
using DBQueryTool.DataAccess.Models;
using DBQueryTool.DataAccess.Service.Interfaces;
using DBQueryTool.Migrations;

namespace DBQueryTool.DataAccess.Service.Impl
{
    public class TemplateTypeService : ITemplateTypeService
    {
        public TemplateType Add(TemplateType entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                dbContext.TemplateTypes.Add(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public List<TemplateType> GetAll()
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                return dbContext.TemplateTypes.ToList();
            }
        }

        public TemplateType Update(TemplateType entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                var found = dbContext.TemplateTypes.Find(entity);
                if (found == null) return null;

                dbContext.TemplateTypes.Add(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public void Remove(TemplateType entity)
        {
            using (var dbContext = new DBQueryToolDbContext())
            {
                dbContext.TemplateTypes.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}