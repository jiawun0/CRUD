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
    public partial class VideoCategoryFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowDB();
        }
        void ShowDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoCategoryFront"].ConnectionString);
            //SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=LMS;Integrated Security=True");

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string sql = "select * from VideoCategory ";

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            //sqlCommand.Connection = connection;


            //將準備的SQL指令給操作物件
            //sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            GridView_VideoCategoryFront.DataSource = reader;

            //進行資料連接
            GridView_VideoCategoryFront.DataBind();

            connection.Close();
        }
        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("VideoCategoryBack.aspx");
        }
    }
}