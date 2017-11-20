using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcEshop.Models;

namespace MvcEshop.Classes
{
    public class CheckUniqUser:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            MvcEshopDBEntities db=new MvcEshopDBEntities();
            bool Ok = !db.Users.Any(u => u.Email == value.ToString().ToLower().Trim());
            return Ok;
        }
    }
}