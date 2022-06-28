using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class ActivityTimeline
    {
        [Key]
        public long Id { get; set; }
        public int AccountId { get; set; }

        [Required, StringLength(50)]
        public string RecordSchemaName { get; set; }

        [Required, StringLength(100)]
        public string RecordName { get; set; }

        [Required, StringLength(20)]
        public string RecordId { get; set; }

        [Required, StringLength(200)]
        public string Command { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}
