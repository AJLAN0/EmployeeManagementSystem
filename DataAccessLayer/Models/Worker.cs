using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int WorkId { get; set; }
        public User User { get; set; } = null!;
        public Work Work { get; set; } = null!;
    }
}
