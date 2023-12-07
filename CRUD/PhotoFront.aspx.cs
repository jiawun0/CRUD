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
    public partial class PhotoFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["AlbumId"] != null)
                {
                    string albumId = Request.QueryString["AlbumId"];

                    ShowDB(albumId);
                }
            }
        }

        void ShowDB(string albumId)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw12"].ConnectionString);
            //SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=LMS;Integrated Security=True");

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "select Photo.Id, Photo.PhotoName, Photo.PhotoDescription, Photo.PhotoPath from Photo " +
                " left join Album on Album.Id = Photo.AlbumId " +
                " where AlbumId = @AlbumId ";

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@AlbumId", albumId);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            Repeater_photo.DataSource = reader;

            //進行資料連接
            Repeater_photo.DataBind();

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

        //protected void BackBtn_Click(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["AlbumId"] != null)
        //    {
        //        string albumId = Request.QueryString["AlbumId"];

        //        // 將目前頁面的 QueryString 保留並添加到新的 URL 中
        //        string redirectUrl = "PhotoBack.aspx?AlbumId=" + albumId;

        //        // 重新導向到下一個頁面
        //        Response.Redirect(redirectUrl);
        //    }
        //    else
        //    {
        //        // 如果 AlbumId 為空，您可以定義一個預設的重定向 URL
        //        Response.Redirect("PhotoBack.aspx");
        //    }

        //}
    }
}