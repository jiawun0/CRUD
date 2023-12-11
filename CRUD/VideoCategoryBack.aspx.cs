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
    }
}