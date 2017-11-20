using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.BusinessLogic
{
    public class FreeUserManager:Infrastructure.BusinessLogic.UserManager
    {
        public FreeUserManager(MvcEshop.Models.User currentUser)
            :base(currentUser)
        {

        }
        public override bool IsDocSendingAllowed()
        {
            return true;
        }


    }
}