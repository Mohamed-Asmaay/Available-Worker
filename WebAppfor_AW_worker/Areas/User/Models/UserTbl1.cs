using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebAppfor_AW_worker.Models
{
    public class UserTbl
    {
        [Key]
        [ScaffoldColumn(false)]
        public int UsId { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Please enter Email ID")]
        [Display(Name = "User email")]
        [StringLength(60)]
        //[Index(IsUnique = true)]
        public string UsEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
        [Display(Name = "User password")]
        public string UsPassword { get; set; }

        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid Name.")]
        [Required(ErrorMessage = "Please write your Name")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        public string UsName { get; set; }

        [Display(Name = "address")]
        [Required(ErrorMessage = "Please write your address")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Must be at least 8 characters long.")]
        public string UsAddress { get; set; }

        [Required(ErrorMessage = "select your gender"), Display(Name = "User gender")]
        public string UsGender { get; set; }

        [Required(ErrorMessage = "Please enter Mobile Number")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "Invalid Mobile Number.")]
        public string UsPhone { get; set; }

        [Required(ErrorMessage = "select your region"), Display(Name = "Region")]
        public string RegionName { get; set; }

        public virtual RegionTbl1? RegionNameNavigation { get; set; }

        public virtual ICollection<ComplaintTbl> ComplaintTbls { get; } = new List<ComplaintTbl>();

        public virtual ICollection<RequestTbl> RequestTbls { get; } = new List<RequestTbl>();
    }
}
