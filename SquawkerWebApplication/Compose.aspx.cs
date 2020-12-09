using System;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Collections.Generic;
using SquawkerWebApplication.SqlDataObjects;

namespace SquawkerWebApplication
{
    public partial class Compose : System.Web.UI.Page
    {
        private static DbDataContext _dataContext = null;
        static Compose()
        {
            _dataContext = new DbDataContext(Settings.GetConnectionString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SquawksDataSource_ContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = _dataContext;
        }

        protected void SquawksDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = GetUserPosts();
        }

        public object GetUserPosts()
        {
            int clientAuthAsUserId = Common.GetUserId(Page);
            if (clientAuthAsUserId == -1)
            {
                return null;
            }

            var results = from post in _dataContext.Squawks
                          join user in _dataContext.Users on post.UserId equals user.Id
                          where post.UserId == clientAuthAsUserId
                          orderby post.CreationDate
                          select new
                          {
                              user.FirstName,
                              user.Surname,
                              post.CreationDate,
                              post.Content
                          };

            return results;
        }

        [WebMethod]
        public static void PostBackNewSquawk(string content, string latitude, string longitude)
        {
            decimal decLat = decimal.Parse(latitude);
            decimal decLong = decimal.Parse(longitude);

            Squawks newSquawk = new Squawks()
            {
                CreationDate = DateTime.UtcNow,
                Content = content,
                Latitude = decLat,
                Longitude = decLong
            };

            Common.InsertNewSquawk(newSquawk);
        }

        protected void PostNewSquawk_Click(object sender, EventArgs e)
        {
            int clientAuthAsUserId = Common.GetUserId(Page);
            if (clientAuthAsUserId == -1)
            {
                return;
            }

            Squawks newSquawk = new Squawks()
            {
                UserId = clientAuthAsUserId,
                CreationDate = DateTime.UtcNow,
                Content = NewSquawkText.Text,
                Latitude = 0.0m,
                Longitude = 0.0m
            };

            Common.InsertNewSquawk(newSquawk);

            NewSquawkText.Text = "";
            SquawksDataSource.DataBind();
        }
    }
}