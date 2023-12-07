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
    public partial class AlbumFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowDB();
        }

        void ShowDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw1"].ConnectionString);
            //SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=LMS;Integrated Security=True");

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string sql = "select Id, AlbumName, AlbumDescription, AlbumPath from Album ";

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            //sqlCommand.Connection = connection;


            //將準備的SQL指令給操作物件
            //sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            Repeater_album.DataSource = reader;

            //進行資料連接
            Repeater_album.DataBind();

            connection.Close();
        }

        protected string GetRelativeImagePath(string albumPath)
        {
            if (!string.IsNullOrEmpty(albumPath))
            {
                string relativePath = albumPath.Replace(Server.MapPath("~"), "").Replace(Server.MapPath("\\"), "/");
                return relativePath;
            }
            return string.Empty;
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AlbumBack.aspx");
        }
    }
}