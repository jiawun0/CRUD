using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD
{
    public partial class LinkCategoryBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowDB();
            }
        }

        protected void CreateLinkCategoryBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLinkCB2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"insert into LinkCategory (CategoryName) values(@CategoryName)";

            sqlCommand.Parameters.AddWithValue("@CategoryName", TextBox_LinkCategoryName.Text);

            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Redirect("LinkCategoryBack.aspx");
            ShowDB();
        }
        void ShowDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLinkCB2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "select * from LinkCategory ";

            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            GridView_LinkCategoryBack.DataSource = reader;

            GridView_LinkCategoryBack.DataBind();

            connection.Close();
        }

        protected void GridView_LinkCategoryBack_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_LinkCategoryBack.EditIndex = e.NewEditIndex;
            ShowDB();
        }

        protected void GridView_LinkCategoryBack_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_LinkCategoryBack.Rows[e.RowIndex];

            int boardId = Convert.ToInt32(GridView_LinkCategoryBack.DataKeys[e.RowIndex].Value);

            TextBox textBoxCN = row.FindControl("TextBox_TemplateCN") as TextBox;
            string changeTextCN = textBoxCN.Text;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLinkCB2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update LinkCategory set CategoryName = @CategoryName where Id = @BoardId ";

            sqlCommand.Parameters.AddWithValue("@CategoryName", changeTextCN);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('更新成功');</script>");
            GridView_LinkCategoryBack.EditIndex = -1;
            ShowDB();
        }

        protected void GridView_LinkCategoryBack_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_LinkCategoryBack.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLinkCB2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from LinkCategory where Id = @boardId ";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('刪除成功');</script>");

            ShowDB();
        }

        protected void GridView_LinkCategoryBack_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_LinkCategoryBack.EditIndex = -1;
            ShowDB();
        }

        protected void FrontBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LinkCategoryFront.aspx");
        }

    }
}