using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.Controllers
{
    public partial class ValidationController : Infrastructure.BaseControllerWithDatabaseContext
    {
     
        // GET: Validation
        [NonAction]
        public bool ValidateDataVolume(object value)
        {
            if (value == null)
            {
                return true;
            }

            bool blnResult = false;
            double dblResult = 0;

            blnResult = Double.TryParse(value.ToString(), out dblResult);

            if (dblResult < 0)
            {
                blnResult = false;
            }
            //if (oUser != null)
            //{
            //    blnResult = true;
            //}

            return blnResult;
        }


        public bool ValidateMoney(object value)
        {
            if (value == null)
            {
                return true;
            }

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