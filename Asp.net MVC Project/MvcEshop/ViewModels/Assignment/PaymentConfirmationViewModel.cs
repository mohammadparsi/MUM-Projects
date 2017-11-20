using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Assignment
{
    public class PaymentConfirmationViewModel
    {
        public string SysId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }
        public string Fp { get; set; }
        public string TimeStamp { get; set; }

        public string FactorNum { get; set; }

    }
}