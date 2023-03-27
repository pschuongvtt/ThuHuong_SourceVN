using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SourceVietNam_Version_09New.Model
{
    public class ProductHistory
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid ProductHistoryId { get; set; }

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
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ManufactureId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
