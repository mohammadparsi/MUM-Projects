using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.BusinessLogic
{
    public class SystemAdministratorManager:Infrastructure.BusinessLogic.UserManager
    {
        public SystemAdministratorManager(MvcEshop.Models.User currentUser):base(currentUser)
        {

        }
        public override bool IsDocSendingAllowed()
        {
            throw new NotImplementedException();
        }
    }
}