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
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class User
    {
        public User()
        {
            this.ClassesOfTheInstructor = new HashSet<Class>();
            this.ClassesOfTheCreator = new HashSet<Class>();
            this.Accounts = new HashSet<Account>();
            this.ClassesOfTheStudent = new HashSet<Class>();
            this.Roles = new HashSet<Role>();
            this.AssignmentFiles = new HashSet<AssignmentFile>();
            this.CreditCards = new List<CreditCard>();
            this.AccountsForManagement = new List<Account>();
            //this.Responsibilities = new List<Responsibility>();

            UserId = System.Guid.NewGuid();
            CreateDate = System.DateTime.Now;
            IsActive = false;
            IsDeleted = false;
            ActiveCode = System.Guid.NewGuid();
            Password = System.Guid.NewGuid().ToString().Replace("-", "").Substring(1, 8);
        }

        public System.Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string CellPhoneNumber { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }

        }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> GenderId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string SecretAnswer { get; set; }
        public System.DateTime CreateDate { get; set; }
       
        public System.Guid ActiveCode { get; set; }
        public bool IsProfileCompleted { get; set; }
        public Nullable<bool> IsLocked { get; set; }
        public int NumberOfWrongPasswords { get; set; }
        public Nullable<System.Guid> NewEmailConfirmationCode { get; set; }
        //public Nullable<int> VolumeForUse { get; set; }
        //public System.DateTime ExpirationDateForVolumeUse { get; set; }


        //[Display(ResourceType = typeof(Resources.DisplayNames),
        //    Name = Resources.Strings.DisplayNamesKeys.ExpirationDateForVolumeUse)]
        //public string DisplayExpirationDateForVolumeUse
        //{
        //    get
        //    {
        //        return Infrastructure.Convert.ToDisplayDate
        //                    (Infrastructure.Convert.ToPersian(ExpirationDateForVolumeUse));
        //    }

        //}
        public bool? SendSms { get; set; }
        public string NationalCode { get; set; }
        public string FatherName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<bool> AdvertisementByIrandoc { get; set; }
        public Nullable<bool> AdvertisementByOtherOrgs { get; set; }
        public Nullable<int> SecretQuestionId { get; set; }

        //public virtual ICollection<Responsibility> Responsibilities { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual ICollection<Class> ClassesOfTheInstructor { get; set; }
        public virtual ICollection<Class> ClassesOfTheCreator { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Class> ClassesOfTheStudent { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<AssignmentFile> AssignmentFiles { get; set; }
        public virtual SecretQuestion SecretQuestion { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Account> AccountsForManagement { get; set; }

        public MvcEshop.Models.CreditCard GetCreditCardByAccountId(System.Guid accountId)
        {
            MvcEshop.Models.CreditCard oCreditCard = (from current in this.CreditCards where current.AccountId == accountId select current)
                .ToList().FirstOrDefault();

            return oCreditCard;
        }

    }
}