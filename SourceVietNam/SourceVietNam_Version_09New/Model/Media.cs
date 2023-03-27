using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceVietNam_Version_09New.Model
{
    public class Media
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid MediaId { get; set; }
        public string? FileName { get; set; }
        public int? FileSize { get; set; }
        public string? FileType { get; set; }
        public string? MediaType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

    }
}
