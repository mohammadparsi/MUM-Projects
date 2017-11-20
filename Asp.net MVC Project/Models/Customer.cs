namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
            Contracts = new HashSet<Contract>();
        }

        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
