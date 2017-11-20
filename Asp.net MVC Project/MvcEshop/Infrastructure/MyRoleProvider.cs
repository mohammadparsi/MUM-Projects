using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Infrastructure
{
    public class MyRoleProvider : System.Web.Security.RoleProvider
    {
        public MyRoleProvider()
        {

        }

        MvcEshop.Models.DFEntities oDFEntities =
            new MvcEshop.Models.DFEntities();

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public void AddUserToRole(string username, string roleTitle)
        {
            MvcEshop.Models.Role oRole =
                MVC.User.GetRoleByRoleTitle(roleTitle);

            //MvcEshop.Models.Role oRole = new MvcEshop.Models.Role();
            //oRole.RoleTitle=roleName;
            MvcEshop.Models.Role oExistingRole = MVC.User.GetUserByUsername(username)
                .Roles
                .Where(current => current != null && current.RoleTitle == roleTitle)
                .FirstOrDefault()
                ;

            if (oExistingRole == null)
            {
                MvcEshop.Models.User oUser = MVC.User.GetUserByUsername(username);

                oUser.Roles.Add(oRole);

                MVC.User.UpdateUser(oUser);

                //oDFEntities.Entry(oUser).State = EntityState.Modified; 


                //oDFEntities.SaveChanges();

            }
        }

        public override string ApplicationName
        {
            get
            {
                return ("DocumentFinder");
            }
            set
            {
                value = "DocumentFinder";
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            return MVC.User.GetRoleTitlesByUsername(username);
            //MvcEshop.Models.User oUser = oDFEntities.Users
            //    .Where(current => current.Email == username)
            //    .FirstOrDefault()
            //    ;

            //string[] varRoles=null;

            //if (oUser != null)
            //{
            //    varRoles =
            //           (from r in oUser.Roles select r.RoleTitle).ToArray();
            //}

            //return varRoles;
            //return (from u in oDFEntities.Users
            //        join r in oDFEntities.Roles on u.RoleID equals r.RoleID
            //        where u.UserName == username
            //        select r.RoleNameInSystem).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var varRoles = MVC.User.GetRoleTitlesByUsername(username);
            bool blnResult = true;

            string oRoleName = varRoles.
                Where(current => current == roleName).
                FirstOrDefault();

            if (string.IsNullOrEmpty(oRoleName))
            {
                blnResult = false;
            }

            return blnResult;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromRole(string username, string roleTitle)
        {
            MvcEshop.Models.Role oRole =
                MVC.User.GetRoleByRoleTitle(roleTitle);

            MvcEshop.Models.Role oExistingRole = MVC.User.GetUserByUsername(username)
                .Roles
                .Where(current => current!=null && current.RoleTitle == roleTitle)
                .FirstOrDefault()
                ;

            if (oExistingRole != null)
            {

                MvcEshop.Models.User oUser =
                    MVC.User.GetUserByUsername(username);

                oUser.Roles.Remove(oRole);
                MVC.User.UpdateUser(oUser);

            }

        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public MvcEshop.Models.User FindUserByUsername(string username)
        {
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
               .Where(current => current.Email == username)
               .FirstOrDefault()
               ;

            return oUser;
        }

        public List<MvcEshop.Models.Role> GetRolesForNavBarDropDownList(string username)
        {
            List<MvcEshop.Models.Role> lstRoles =
                                        MVC.User.GetUserByUsername(username).Roles
                                        .Where(current => current.RoleId != 6 && current.RoleId < 12)
                                        .ToList();

            return lstRoles;
        }

        public MvcEshop.Models.Role GetRoleByRoleId(int roleId)
        {
            MvcEshop.Models.Role oRole = oDFEntities.Roles
                .Where(current => current.RoleId == roleId)
                .ToList()
                .FirstOrDefault();

            return oRole;
        }
    }
}