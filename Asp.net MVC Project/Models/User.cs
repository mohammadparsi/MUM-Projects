namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            Roles = new HashSet<Role>();
        }

        public Guid UserId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int? GenderId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "ntext")]
        public string SecretQuestion { get; set; }

        [Column(TypeName = "ntext")]
        public string SecretAnswer { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public Guid ActiveCode { get; set; }

        public bool IsProfileCompleted { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
