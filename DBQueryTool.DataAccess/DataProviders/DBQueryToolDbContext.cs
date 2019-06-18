using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBQueryTool.DataAccess.Models;

namespace DBQueryTool.DataAccess.DataProviders
{
    public class DBQueryToolDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<TemplateType> TemplateTypes { get; set; }
    }
}
