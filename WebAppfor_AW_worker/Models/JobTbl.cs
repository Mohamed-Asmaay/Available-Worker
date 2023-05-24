using System.ComponentModel.DataAnnotations;

namespace WebAppfor_AW_worker.Models
{
    public class JobTbl
    {
        [Key]
        public int JobId { get; set; }
        public string JobName { get; set; } 

        public string JobDescription { get; set; }
        public string? JopPhoto { get; set; }

        public virtual ICollection<WorkerTbl> WorkerTbls { get; } = new List<WorkerTbl>();
    }
}
