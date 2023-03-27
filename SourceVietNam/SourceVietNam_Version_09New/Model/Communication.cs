using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceVietNam_Version_09New.Model
{
    public class Communication
    {
        [Key]
        [DefaultValue("newsequentialid()")]
        public Guid CommunicationId { get; set; }
       
        public Guid ReceiverId { get; set; }
        public string? Message { get; set; }
        public string? Media { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey("Account")]
        public string SenderId { get; set; }
        public AppUser Account { get; set; }
    }
}
