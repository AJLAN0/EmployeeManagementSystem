using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Worker
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid WorkId { get; set; }
        public User User { get; set; } = null!;
        public Work Work { get; set; } = null!;
    }
}
