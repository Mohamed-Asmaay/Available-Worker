using System.ComponentModel.DataAnnotations;

namespace WebAppfor_AW_worker.Models
{
    public class ComplaintTbl
    {
        [Key]
        public int ComId { get; set; }

        public int? UsId { get; set; }
        public virtual UserTbl1? Us { get; set; }

        [Required(ErrorMessage = "please enter your Request Id, you can get it from your requests table")]
        public int? ReqId { get; set; }
        public virtual RequestTbl? Req { get; set; }

        public DateTime? ComDate { get; set; }

        [Required(ErrorMessage = "enter your complaint description, please ."), Display(Name = "Complaint description")]
        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        public string ComDescription { get; set; }

    }
}
