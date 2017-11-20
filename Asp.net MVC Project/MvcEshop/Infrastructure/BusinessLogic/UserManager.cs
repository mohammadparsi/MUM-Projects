using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.BusinessLogic
{
    public abstract class UserManager
    {
        private MvcEshop.Models.User _currentUser;
        //abstract private int _currentRoleId;
        //private MvcEshop.Models.CreditCard _selectedCreditCard;

        //public MvcEshop.Models.User SelectedCreditCard
        //{
        //    get { return _currentUser; }
        //    set { _currentUser = value; }
        //}

        public MvcEshop.Models.User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public UserManager(MvcEshop.Models.User currentUser)
        {
            //this._currentRoleId = currentRoleId;
            this._currentUser = currentUser;
        }
        abstract public bool IsDocSendingAllowed();
        
        
      //public void RegisterRequest();
      //  public void CreateReceipt();

      //  public void SubtractFromCredit();
    }
}