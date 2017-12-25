using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BusinessEntities
{

   [DataContract]
    public class EmployeeEntity
    {

       [DataMember(IsRequired = true)]
       [Required]
       public string Id { get; set; }

       [DataMember(IsRequired = true)]
       [Required]
       [StringLength(maximumLength: 250)]
        public string FirstName { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        [StringLength(maximumLength: 250)]
        public string LastName { get; set; }

        [DataMember(IsRequired = true)]
        public double Salary { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        public double NetPay { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [Range(0, 99)]
        public double TaxesPercentage { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [Range(0, 99)]
        public double FICAPercentage { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [Range(0, 99)]
        public double PreTaxPercentage { get; set; }

       
       

        [Required]
        [Range(0, 99)]
        [DataMember(IsRequired = true)]
        public double PostTaxPercentage { get; set; }
    
    }
}
