using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceVietNam_Version_09New.Model
{
    public class Manufacture
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid ManufactureId { get; set; }
        [Required]
        public Guid RequestStatus { get; set; }
        [Required]
        public Guid ManufactureStatus { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(11)]
        public string? Phone { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }
        [MaxLength(10)]
        public string? District { get; set; }
        [MaxLength(10)]
        public string? City { get; set; }
        public string? Country { get; set; }
        public int CompanySize { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateEstablished { get; set; }
        public string? MainProduct { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        public string? Media { get; set; }
        public string? Website { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)]
        public Guid CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(100)]
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public Guid MainCategory { get; set; }

        [Required]
        [ForeignKey("Account")]
        public string AccountId { get; set; }
        public AppUser Account { get; set; }
    }
}
