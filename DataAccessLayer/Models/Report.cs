﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int GeneratedBy { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string ReportData { get; set; } = string.Empty;
        public User User { get; set; } = null!;
    }
}
