using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SourceVietNam_Version_09New.Model
{
    public class SearchHistory
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid SearchHistoryId { get; set; }
       
        public string SearchingText { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SearchingDate { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey("Account")]
        public string AccountId { get; set; }
        public AppUser Account { get; set; }
    }
}
