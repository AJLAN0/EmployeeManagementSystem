using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class WorkNote
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public int EmployeeId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Work Work { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
