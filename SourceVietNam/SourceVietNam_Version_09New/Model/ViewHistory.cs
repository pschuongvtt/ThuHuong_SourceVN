using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceVietNam_Version_09New.Model
{
    public class ViewHistory
    {
        [Required]
        [ForeignKey("Manufacture")]
        public Guid ViewHistoryId { get; set; }
        public Guid AccountId { get; set; }
        public string ViewType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ViewDate { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [ForeignKey("Manufacture")]
        public Guid? ManufactureId { get; set; }
        public Manufacture Manufacture { get; set; }
    }
}
