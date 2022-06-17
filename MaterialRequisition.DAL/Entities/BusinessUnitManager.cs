using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class BusinessUnitManager
    {
        [Key]
        public int Id { get; set; }
        public int BusinessUnitId { get; set; }
        public int ManagerId { get; set; }

        [ForeignKey("BusinessUnitId")]
        public virtual BusinessUnit BusinessUnit { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Account Manager { get; set; }
    }
}
