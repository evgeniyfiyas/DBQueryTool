using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQueryTool.Models
{
    class Report
    {
        public int Id { get; set; }
        public string SerializedReport { get; set; }
        public int UserId { get; set; }
        public int TemplateId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
