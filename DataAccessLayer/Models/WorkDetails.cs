using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class WorkDetails
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Work Work { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
