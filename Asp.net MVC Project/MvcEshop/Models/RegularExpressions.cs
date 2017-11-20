using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.Models
{
    public static class RegularExpressions
    {
        public const string Password =
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,20}$";

        public const string NationalCode = @"^\d{10}$";

        public const string CellPhoneNumber = @"^09[1-3][0-9]{8}$";
    }
}