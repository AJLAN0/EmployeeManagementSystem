namespace DataAccessLayer.Models
{
    public class Work
    {
        public int Id { get; set; }
        public string CarNumber { get; set; } = string.Empty;
        public string WorkDescription { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
        public WorkStatuses workStatuses { get; set; } = null!;
    }
}