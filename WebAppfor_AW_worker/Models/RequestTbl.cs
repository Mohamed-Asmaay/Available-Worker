using System.ComponentModel.DataAnnotations;

namespace WebAppfor_AW_worker.Models
{
    public class RequestTbl
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ReqId { get; set; }


        public DateTime ReqTime { get; set; }

        [Required(ErrorMessage = "enter your problem , please ."),Display(Name = "Problem name"),StringLength(100)]
            public string ReqProblem { get; set; }

        [Required(ErrorMessage ="enter your problem description, please ."), Display(Name = "Problem description")]
        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        public string ReqDescription { get; set; }


        [DataType(DataType.Currency)]
        
        public float? ReqCost { get; set; }

        [Required(ErrorMessage ="please select your worker")]
        public int WrId { get; set; }

        public int UsId { get; set; }

        public bool? ReqAccept { get; set; }

        public bool? ReqDecline { get; set; }

        public bool? ReqConfirmation { get; set; }

        public virtual UserTbl1? Us { get; set; }

        public virtual WorkerTbl? Wr { get; set; }
        public virtual ICollection<ComplaintTbl> ComplaintTbls { get; } = new List<ComplaintTbl>();
    }
}
