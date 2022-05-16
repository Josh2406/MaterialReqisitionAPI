using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(150)]
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
} 
