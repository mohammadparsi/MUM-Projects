using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infrastructure.CustomValidation
{
   
        public class UniqueEmailAddress : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool blnResult = false;

                MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                    .Where(current => current.Email == value.ToString().ToLower().Trim())
                    .FirstOrDefault()
                    ;

                if (oUser == null)
                {
                    blnResult = true;
                }

                return blnResult;
            }
        }

        public class ExistingEmailAddress : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool blnResult = false;

                MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                    .Where(current => current.Email == value.ToString().ToLower().Trim())
                    .FirstOrDefault()
                    ;

                if (oUser != null)
                {
                    blnResult = true;
                }

                return blnResult;
            }
        }

        public class DataVolume : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value==null)
                {
                    return true;
                }

                bool blnResult = false;
                double dblResult=0;

                blnResult=Double.TryParse(value.ToString(), out dblResult);

                if (dblResult<0)
                {
                    blnResult = false;
                }
                //if (oUser != null)
                //{
                //    blnResult = true;
                //}

                return blnResult;
            }
        }

        public class Money : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool blnResult = false;
                int intResult = 0;

                blnResult = int.TryParse(value.ToString(), out intResult);

                if (intResult < 0)
                {
                    blnResult = false;
                }
                //if (oUser != null)
                //{
                //    blnResult = true;
                //}

                return blnResult;
            }
        }
    }
