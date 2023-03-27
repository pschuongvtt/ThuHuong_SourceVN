using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceVietNam_Version_09New.Model
{
    public class ProductStg
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid ProductStgId { get; set; }

        public Guid VariantId { get; set; }
        [Required]
        public Guid RequestStatus { get; set; }
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
        public Guid CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

    }
}
