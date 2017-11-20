namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenderId { get; set; }

        [Required]
        [StringLength(10)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
