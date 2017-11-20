using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class BaseControllerWithDatabaseContext : BaseController
    {
        public BaseControllerWithDatabaseContext()
        {

        }

        private MvcEshop.Models.DFEntities _DFEntities;
        public MvcEshop.Models.DFEntities DFEntities
        {
            get
            {
                if (_DFEntities == null)
                {
                    _DFEntities = new MvcEshop.Models.DFEntities();
                }

                return _DFEntities;
            }

            set
            {
                _DFEntities = value; 
            }
        }

        private Infrastructure.MyRoleProvider _myRoleProvider;
        public Infrastructure.MyRoleProvider MyRoleProvider
        {
            get
            {
                if (_myRoleProvider == null)
                {
                    _myRoleProvider = new Infrastructure.MyRoleProvider();
                }

                return _myRoleProvider;
            }
        }

        private MvcEshop.Models.Setting _setting;
        public MvcEshop.Models.Setting Setting
        {
            get
            {
                if (_setting == null)
                {
                    _setting = DFEntities.Settings.FirstOrDefault();
                }

                return _setting;
            }
        }
    }
}