using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceVietNam_Version_09New.Model
{
    public class Product
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid ProductId { get; set; }

        [Required]
        public Guid ProductStatus { get; set; }
        [Required]
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public string? SKU { get; set; }
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [ForeignKey("Variant")]
        public Guid VariantId { get; set; }
        public Variant Variant { get; set; }

        [Required]
        public Guid RequestStatus { get; set; }

        [Required]
        [ForeignKey("Manufacture")]
        public Guid ManufactureId { get; set; }
        public Manufacture Manufacture { get; set; }
    }
}
