using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBQueryTool.DataAccess.Models;
using MySql.Data.Entity;

namespace DBQueryTool.DataAccess.DataProviders
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DBQueryToolDbContext : DbContext
    {
       

        public DbSet<User> Users { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<TemplateType> TemplateTypes { get; set; }
    }
}
