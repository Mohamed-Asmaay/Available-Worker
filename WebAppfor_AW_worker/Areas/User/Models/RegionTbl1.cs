using System.ComponentModel.DataAnnotations;
using WebAppfor_AW_worker.Models;

namespace WebAppfor_AW_worker.Areas.User.Models
{
    public class RegionTbl
    {
        [Key]
        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public virtual ICollection<UserTbl1> UserTbls { get; } = new List<UserTbl1>();

        public virtual ICollection<WorkerTbl> WorkerTbls { get; } = new List<WorkerTbl>();

    }
}
