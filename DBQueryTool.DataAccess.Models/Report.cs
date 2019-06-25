using System;

namespace DBQueryTool.DataAccess.Models
{
    public class Report
    {
        public int Id { get; set; }
        public byte[] ReportFileBytes { get; set; }
        public int UserId { get; set; }
        public int TemplateId { get; set; }
        public DateTime CreatedAt { get; set; } = new DateTime();
    }
}
