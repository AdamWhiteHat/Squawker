using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SquawkerWebApplication.SqlDataObjects;

namespace SquawkerWebApplication
{
    public static class Common
    {
        private static DbDataContext dataContext = null;
        static Common()
        {
            dataContext = new DbDataContext(Settings.GetConnectionString());
        }

        public static Users GetUserFromId(int userId)
        {
            var result = (from users in dataContext.Users
                          where users.Id == userId
                          select users).First();
            return result;
        }

        public static int GetUserId(Page page)
        {
            var principal = page.User;
            if (principal == null)
            {
                return -1;
            }

            var userIdent = principal.Identity;
            if (!userIdent.IsAuthenticated)
            {
                return -1;
            }

            string username = userIdent.Name;
            if (string.IsNullOrEmpty(username))
            {
                return -1;
            }

            var result = from user in dataContext.Users
                         where user.Email == username
                         select user.Id;

            if (!result.Any())
            {
                return -1;
            }

            return result.First();
        }

        public static void InsertNewSquawk(Squawks squawk)
        {
            dataContext.Squawks.InsertOnSubmit(squawk);
            dataContext.SubmitChanges();
        }
    }
}