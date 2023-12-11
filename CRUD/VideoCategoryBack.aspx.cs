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
    public partial class VideoCategoryBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowDB();
            }
        }

        protected void CreateVideoCategoryBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoCategory"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //查詢跟參數的部分很難寫成方法，需自行研究
            string sql = $"insert into VideoCategory (CategoryName) values(@CategoryName)";

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@CategoryName", TextBox_CategoryName.Text);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();
            //if (f != 0)
            //{
            //    Response.Write("<script>alert('種類新增成功');</script>");
            //}
            //else
            //{
            //    Response.Write("<script>alert('種類新增失敗');</script>");
            //}

            connection.Close();

            Response.Redirect("VideoCategoryBack.aspx");
            ShowDB();
        }

        void ShowDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoCategory"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "select * from VideoCategory ";

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            //執行該SQL查詢，用reader接資料
            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            GridView_VideoCategoryBack.DataSource = reader;

            //GridView進行資料連接
            GridView_VideoCategoryBack.DataBind();

            connection.Close();
        }

        protected void GridView_VideoCategoryBack_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_VideoCategoryBack.EditIndex = e.NewEditIndex;
            ShowDB();
        }

        protected void GridView_VideoCategoryBack_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_VideoCategoryBack.Rows[e.RowIndex]; //找到目前gridview的編輯行數
            //DropDownList dropDownList = (DropDownList)row.FindControl("dropDownList");

            //string isActive = dropDownList.SelectedValue;
            int boardId = Convert.ToInt32(GridView_VideoCategoryBack.DataKeys[e.RowIndex].Value); //取得資料表ID

            TextBox textBoxCN = row.FindControl("TextBox_TemplateCN") as TextBox;
            string changeTextCN = textBoxCN.Text;


            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoCategory"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update VideoCategory set CategoryName = @CategoryName where Id = @BoardId";


            sqlCommand.Parameters.AddWithValue("@CategoryName", changeTextCN);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;

            //int s = helper.ExecuteSQL(sql);
            //if (s > 0) Response.Write("<script>alert('更新成功');</script>");
            //else Response.Write("<script>alert('更新失敗');</script>");

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('更新成功');</script>");
            GridView_VideoCategoryBack.EditIndex = -1;
            ShowDB();
        }

        protected void GridView_VideoCategoryBack_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_VideoCategoryBack.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoCategory"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from VideoCategory where Id = @boardId";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('刪除成功');</script>");

            ShowDB();
        }

        protected void GridView_VideoCategoryBack_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_VideoCategoryBack.EditIndex = -1;
            ShowDB();
        }

        protected void BtnRedirect_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string CategoryID = btn.CommandArgument;

                // 將 Id 添加到 QueryString 並進行重定向
                Response.Redirect("VideoBack.aspx?CategoryID=" + CategoryID);
            }
            else
            {
                // 如果 Id 為空，定義一個預設的重定向 URL
                Response.Redirect("VideoBack.aspx");
            }
        }

        protected void FrontBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("VideoCategoryFront.aspx");
        }
    }
}