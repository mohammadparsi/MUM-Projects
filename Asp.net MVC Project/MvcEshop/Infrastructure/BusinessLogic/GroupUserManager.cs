using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.BusinessLogic
{
    public class GroupUserManager:Infrastructure.BusinessLogic.UserManager
    {
        public GroupUserManager(MvcEshop.Models.User currentUser)
            :base(currentUser)
        {

        }

        public override bool IsDocSendingAllowed()
        {
            List<MvcEshop.Models.Account> lstAccounts =
                        this.CurrentUser.CreditCards
                        .Select(current => current.Account)
                        .ToList();
            foreach (MvcEshop.Models.Account account in lstAccounts)
            {
                
            }
            return true;
        }

        
    }
}