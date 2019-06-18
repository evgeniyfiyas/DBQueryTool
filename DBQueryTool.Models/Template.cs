using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQueryTool.Models
{
    class Template
    {
        public int Id { get; set; }
        public string SerializedTemplate { get; set; }
        public int TypeId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
