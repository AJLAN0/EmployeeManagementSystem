using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public Guid GeneratedBy { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string ReportData { get; set; } = string.Empty;
        public User User { get; set; } = null!;
    }
}
