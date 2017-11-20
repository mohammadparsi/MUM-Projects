namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contract
    {
        [Column(TypeName = "datetime2")]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ExpirationDate { get; set; }

        public byte? Situation { get; set; }

        public int VolumeInKBytes { get; set; }

        [Column(TypeName = "money")]
        public decimal PriceInRials { get; set; }

        public string History { get; set; }

        public Guid ContractId { get; set; }

        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string NotificationEmail { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
