using System;
using System.Data.SqlTypes;

namespace DBQueryTool.DataAccess.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SqlFileStream TemplateFile { get; set; }
        public int TypeId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
