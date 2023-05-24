using System.ComponentModel.DataAnnotations;

namespace WebAppfor_AW_worker.Models
{
    public class RegionTbl
    {
        [Key]
        public int RegionId { get; set; }
        
        public string RegionName { get; set; } 

        public virtual ICollection<UserTbl> UserTbls { get; } = new List<UserTbl>();

        public virtual ICollection<WorkerTbl> WorkerTbls { get; } = new List<WorkerTbl>();
    
}
}
