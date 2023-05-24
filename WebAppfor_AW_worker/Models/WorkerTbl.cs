
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace WebAppfor_AW_worker.Models
{
    public class WorkerTbl
    {

        [Key]
        [ScaffoldColumn(false)]
        public int WrId { get; set; }

        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z- ]+$", ErrorMessage = "Invalid Name.")]
        [Required(ErrorMessage = "Please write your Name")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Must be at least 4 characters long.")]
        public string WrName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID")]
        [Display(Name = "Worker email")]
        public string WrEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
        [Display(Name = "Worker password")]
        public string WrPassword { get; set; }

        [Required(ErrorMessage = "select your gender"), Display(Name = "User gender")]
        public string WrGender { get; set; }

        [Required(ErrorMessage = "Please enter Mobile Number")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "Invalid Mobile Number.")]
        public string WrPhone { get; set; }

        [Display(Name = "address")]
        [Required(ErrorMessage = "Please write your address")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Must be at least 8 characters long.")]
        public string WrAddress { get; set; }

        [Required(ErrorMessage = "select your job name"), Display(Name = "job")]
        public string JobName { get; set; }

        [Required(ErrorMessage = "select your region"), Display(Name = "Region")]
        public string RegionName { get; set; }


        [DefaultValue("~/images/avatar-01.png")]
        public string? WrPhoto { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Upload a picture of yourself, please"), Display(Name = "Worker photo")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "enter your national Id "), Display(Name = "national Id")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "enter a valid national Id,please.")]
        [RegularExpression(@"(?<BirthMillennium>[23])\x20?(?:(?<BirthYear>[0-9]{2})\x20?(?:(?:(?<BirthMonth>0[13578]|1[02])\x20?(?<BirthDay>0[1-9]|[12][0-9]|3[01]))\x20?|(?:(?<BirthMonth>0[469]|11)\x20?(?<BirthDay>0[1-9]|[12][0-9]|30))\x20?|(?:(?<BirthMonth>02)\x20?(?<BirthDay>0[1-9]|1[0-9]|2[0-8]))\x20?)|(?:(?<BirthYear>04|08|[2468][048]|[13579][26]|(?<=3)00)\x20?(?<BirthMonth>02)\x20?(?<BirthDay>29)\x20?))(?<ProvinceCode>0[1-34]|[12][1-9]|3[1-5]|88)\x20?(?<RegistryDigit>[0-9]{3}(?<GenderDigit>[0-9]))\x20?(?<CheckDigit>[0-9])", ErrorMessage = "enter a vaid national Id,please.")]
        public string NationalId { get; set; }
        
        public bool? WrAvability { get; set; }
        public virtual JobTbl? JobNameNavigation { get; set; }

        public virtual RegionTbl? RegionNameNavigation { get; set; }

        public virtual ICollection<RequestTbl> RequestTbls { get; } = new List<RequestTbl>();
    }
}
