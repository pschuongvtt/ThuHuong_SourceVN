using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceVietNam_Version_09New.Model
{
    public class Variant
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid VariantId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Price { get; set; }
        public string? Image { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool IsDelete { get; set; }
    }
}
