//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcEshop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Responsibility
    {
        public Responsibility()
        {
            this.Users = new HashSet<User>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string PersianTitle { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
    }
}