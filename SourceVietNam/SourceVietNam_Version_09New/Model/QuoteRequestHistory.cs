using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceVietNam_Version_09New.Model
{
    public class QuoteRequestHistory
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid QuoteHistoryId { get; set; }
        public Guid AccountId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime QuoteDate { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
