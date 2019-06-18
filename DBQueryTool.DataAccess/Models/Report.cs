using System;
using System.Data.SqlTypes;

namespace DBQueryTool.DataAccess.Models
{
    public class Report
    {
        public int Id { get; set; }
        public SqlFileStream ReportFile { get; set; }
        public int UserId { get; set; }
        public int TemplateId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
