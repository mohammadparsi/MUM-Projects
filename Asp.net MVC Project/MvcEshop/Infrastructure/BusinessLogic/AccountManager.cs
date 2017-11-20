using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.BusinessLogic
{
    public class AccountManagerUserManager:Infrastructure.BusinessLogic.UserManager
    {
        public AccountManagerUserManager(MvcEshop.Models.User currentUser)
            :base(currentUser)
        {

        }
        public override bool IsDocSendingAllowed()
        {
            throw new NotImplementedException();
        }
    }
}