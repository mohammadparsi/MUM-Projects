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

    public partial class Receipt
    {
        public Receipt()
        {
            ReceiptId = System.Guid.NewGuid();
            CreateDate = Infrastructure.Convert.ToPersian(System.DateTime.Now);
            IsDeleted = false;
            UsedVolume = 0;
            PaymentDate = null;
        }

        public bool? MustSearchInIrandoc { get; set; }
        public bool? MustSearchInInternet { get; set; }
        public System.DateTime? PaymentDate { get; set; }
        public int PaidAmountInRial { get; set; }
        public int CurrentIrandocTariff { get; set; }
        public int CurrentInternetTariff { get; set; }
        public double? UsedVolume { get; set; }
        public double AllocatedVolume { get; set; }
        public System.Guid ReceiptId { get; set; }
        public Nullable<System.Guid> AccountId { get; set; }
        public System.Guid? AssignmentFileId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime? ExpirationDateForUse { get; set; }
        public System.DateTime? ContractStartDate { get; set; }
        public bool IsDeleted { get; set; }
        public int PaymentTypeId { get; set; }
        public string BillId { get; set; }
        public string Fp { get; set; }
        public string SourceBillId { get; set; }
        public string TimeStamp { get; set; }
        public string FactorNum { get; set; }
        public Nullable<bool> FinalStatus { get; set; }
        public Nullable<System.Guid> CreatorUserId { get; set; }
        public virtual Account Account { get; set; }
        public virtual AssignmentFile AssignmentFile { get; set; }

        public virtual File ImageFile { get; set; }
        public int? ReceiptTypeId { get; set; }
        public ReceiptType ReceiptType { get; set; }

        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
           Name = Resources.Strings.DisplayNamesKeys.CreateDate)]
        public string DisplayCreateDate
        {
            get
            {
                return Infrastructure.Convert.ToDisplayDate(CreateDate);
            }

        }

        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
           Name = Resources.Strings.DisplayNamesKeys.ExpirationDateForVolumeUse)]
        public string DisplayExpirationDate
        {
            get
            {
                return Infrastructure.Convert.ToDisplayDate(ExpirationDateForUse.Value);
            }

        }

        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
          Name = Resources.Strings.DisplayNamesKeys.PaymentDate)]
        public string DisplayPaymentDate
        {
            get
            {
                if (PaymentDate==null)
                {
                    return Resources.DisplayNames.Nothing;
                }

                return Infrastructure.Convert.ToDisplayDate(PaymentDate.Value);
            }

        }


        //[System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
        // Name = Resources.Strings.DisplayNamesKeys.AllocatedVolume)]
        //public string DisplayAllocatedVolume
        //{
        //    get
        //    {
        //        return AllocatedVolume.ToString().Replace("/", ".");
        //    }

        //}


        public double RemainingVolume
        {
            get
            {
                return (AllocatedVolume - UsedVolume.Value);
            }
        }

        public bool IsExpired
        {
            get
            {

                if (ExpirationDateForUse!=null)
                {
                    return (Infrastructure.Convert.ToMiladi(ExpirationDateForUse.Value) < DateTime.Now); 
                }

                return false;
            }
        }


        public double UsedToAllocatedRatio()
        {
            return ((double)UsedVolume / AllocatedVolume * 100);
        }


        public double UsedToAllocatedTimeRatio()
        {
            return ((double)(Infrastructure.Convert.ToPersian(System.DateTime.Now) - CreateDate).Days / 
                (ExpirationDateForUse.Value - CreateDate).Days * 100);
        }
    }
}