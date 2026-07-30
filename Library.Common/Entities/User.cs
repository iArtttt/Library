using Library.Common.Enums;
using Library.Common.Interfaces.DAL;
using System.ComponentModel.DataAnnotations;

namespace Library.Common.Entities
{
    public class User : IUser, IPerson
    {
        public Guid Id {  get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string? Email { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public Role Role { get; set; }

        public string? DocumentNumber { get; set; }
        public DocumentType? DocumentType { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
