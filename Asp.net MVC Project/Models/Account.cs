namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        public Account()
        {
            Users = new HashSet<User>();
        }

        public Guid AccountId { get; set; }

        public Guid CreatorUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public Guid ManagerUserId { get; set; }

        public Guid ParentAccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string JoinPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountTitle { get; set; }

        public Guid CustomerId { get; set; }

        public virtual Account Accounts1 { get; set; }

        public virtual Account Account1 { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
