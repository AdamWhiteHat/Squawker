using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SquawkerWebApplication.SqlDataObjects;

namespace SquawkerWebApplication
{
    public partial class Manage : System.Web.UI.Page
    {
        private static Random _rand = null;
        private static DbDataContext _dataContext = null;
        static Manage()
        {
            _rand = new Random();
            _dataContext = new DbDataContext(Settings.GetConnectionString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowAllPosts();
        }

        protected void ManageGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ManageGridView.EditIndex = e.NewEditIndex;
            ShowAllPosts();
        }

        protected void ManageGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ManageGridView.EditIndex = -1;
            ShowAllPosts();
        }

        protected void ManageGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int clientAuthAsUserId = Common.GetUserId(Page);
            if (clientAuthAsUserId == -1)
            {
                return;
            }

            var vals = e.NewValues;
            var id = vals["Id"];
            int postId = int.Parse(vals["Id"].ToString());

            var toModify = _dataContext.Squawks.Where(s => s.Id == postId).First();


            var newValues = e.NewValues;


            //_dataContext.SubmitChanges();

            ManageGridView.EditIndex = -1;
            ShowAllPosts();
        }

        protected void ManageGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int clientAuthAsUserId = Common.GetUserId(Page);
            if (clientAuthAsUserId == -1)
            {
                return;
            }

            var vals = e.Values;
            var id = vals["Id"];
            int postId = int.Parse(vals["Id"].ToString());

            var toDelete = _dataContext.Squawks.Where(s => s.Id == postId).First();
            _dataContext.Squawks.DeleteOnSubmit(toDelete);
            _dataContext.SubmitChanges();

            ShowAllPosts();
        }

        protected void GenerateDummyRow_Click(object sender, EventArgs e)
        {
            int clientAuthAsUserId = Common.GetUserId(Page);
            if (clientAuthAsUserId == -1)
            {
                return;
            }

            Squawks newSquawk = new Squawks()
            {
                CreationDate = DateTime.UtcNow,
                UserId = 1,
                Content = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()),
                Latitude = (decimal)(_rand.Next(-90, 90) + (_rand.NextDouble() % 1)),
                Longitude = (decimal)(_rand.Next(-180, 180) + (_rand.NextDouble() % 1))
            };

            Common.InsertNewSquawk(newSquawk);

            ShowAllPosts();
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            DateCriteria.Text = "";
            UsernameCriteria.Text = "";
            ContentCriteria.Text = "";
            LengthCriteria.Text = "";
            ShowAllPosts();
        }

        protected void ShowAllPosts()
        {
            BindData(AddUserInfo(_dataContext.Squawks.Select(s => s)));
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            int clientAuthAsUserId = Common.GetUserId(Page);
            if (clientAuthAsUserId == -1)
            {
                return;
            }

            var posts = _dataContext.Squawks.Select(s => s);

            // Post Id
            int postIdCriteria = -1;
            if (!string.IsNullOrWhiteSpace(PostIdCriteria.Text) && int.TryParse(PostIdCriteria.Text, out postIdCriteria))
            {
                posts = posts.Where(p => p.Id.Equals(postIdCriteria));
            }

            // User Id
            int userIdCriteria = -1;
            if (!string.IsNullOrWhiteSpace(UserIdCriteria.Text) && int.TryParse(UserIdCriteria.Text, out userIdCriteria))
            {
                posts = posts.Where(p => p.UserId.Equals(userIdCriteria));
            }

            // Date
            if (!string.IsNullOrWhiteSpace(DateCriteria.Text))
            {
                posts = posts.Where(p => p.Content.Contains(DateCriteria.Text));
            }

            // Username
            if (!string.IsNullOrWhiteSpace(UsernameCriteria.Text))
            {
                posts = posts.Where(p =>
                                         p.Users.UserName.Contains(UsernameCriteria.Text)
                                      || p.Users.FirstName.Contains(UsernameCriteria.Text)
                                      || p.Users.Surname.Contains(UsernameCriteria.Text)
                                    );
            }

            // Email
            if (!string.IsNullOrWhiteSpace(EmailCriteria.Text))
            {
                posts = posts.Where(p => p.Users.Email.Contains(EmailCriteria.Text));
            }

            // Length
            int lengthCriteria = -1;
            if (!string.IsNullOrWhiteSpace(LengthCriteria.Text) && int.TryParse(LengthCriteria.Text, out lengthCriteria))
            {
                posts = posts.Where(p => p.Content.Length.Equals(lengthCriteria));
            }

            // Date
            DateTime dateCriteria = default(DateTime);
            if (!string.IsNullOrWhiteSpace(DateCriteria.Text) && DateTime.TryParse(DateCriteria.Text, out dateCriteria))
            {
                // Obviously this could be improved, such as letting the user specify the range, but im already late on this
                posts = posts.Where(p => p.CreationDate.Subtract(dateCriteria) < TimeSpan.FromHours(12));
            }

            var results = AddUserInfo(posts);
            BindData(results);
        }

        private object AddUserInfo(IQueryable<Squawks> posts)
        {
            return posts.Select(p => new
            {
                p.Id,
                p.UserId,
                p.Users.FirstName,
                p.Users.Surname,
                p.Users.UserName,
                p.Users.Email,
                p.CreationDate,
                p.Users.TimezoneOffset,
                p.Content,
                p.Latitude,
                p.Longitude
            });
        }

        private void BindData(object data)
        {
            ManageGridView.DataSource = data;
            ManageGridView.DataBind();
        }


    }
}