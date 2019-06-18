using System;

namespace DBQueryTool.DataAccess.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string SerializedTemplate { get; set; }
        public int TypeId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
