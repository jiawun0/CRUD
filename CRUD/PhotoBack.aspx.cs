using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD
{
    public partial class PhotoBack : System.Web.UI.Page
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

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw12"].ConnectionString);
            //有檔案才能上傳
            if (FileUpload1.HasFile) //FileUpload1.PostedFile != null
            {
                try
                {
                    //string FileName = FileUpload1.FileName;
                    //savePath = savePath + FileName;
                    //string saveDirectory = Server.MapPath("~ADO2/ADO/photo/");
                    //FileUpload1.SaveAs(savePath);
                    string FileName = Path.GetFileName(FileUpload1.FileName);
                    //string saveDirectory = @"C:\Users\88691\source\repos\ADO2\ADO\photo\";
                    string saveDirectory = Server.MapPath("~/Album/");// 實際伺服器路徑
                    string savePath = Path.Combine(saveDirectory, FileName);
                    FileUpload1.SaveAs(savePath);
                    Label1.Text = "已成功上傳: " + FileName;

                    // 取得albumId
                    string albumId = Request.QueryString["AlbumId"];

                    connection.Open();
                    string sql = "Insert into Photo (PhotoName, PhotoDescription, PhotoPath, AlbumId) values(@PhotoName, @PhotoDescription, @PhotoPath, @AlbumId)";
                    SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    sqlCommand.Parameters.AddWithValue("@PhotoName", TextBox_PhotoName.Text);
                    sqlCommand.Parameters.AddWithValue("@PhotoDescription", TextBox_PhotoDescription.Text);
                    sqlCommand.Parameters.AddWithValue("@PhotoPath", savePath);
                    sqlCommand.Parameters.AddWithValue("@AlbumId", albumId);

                    // 將相對路徑存入資料庫
                    //string relativePath = "~/Album/" + saveDirectory;
                    //sqlCommand.Parameters.AddWithValue("@PhotoPath", relativePath);

                    //將準備的SQL指令給操作物件
                    sqlCommand.CommandText = sql;

                    sqlCommand.ExecuteNonQuery();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }

            }
            else
            {
                Label1.Text = "No File.";
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

            string sql = "select Photo.Id, Photo.PhotoName, Photo.PhotoDescription, Photo.PhotoPath, Photo.IsCover from Photo " +
                " left join Album on Album.Id = Photo.AlbumId " +
                " where AlbumId = @AlbumId ";

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@AlbumId", albumId);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            GridView_PhotoUpload.DataSource = reader;

            //進行資料連接
            GridView_PhotoUpload.DataBind();

            connection.Close();
        }

        protected void FrontBtn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["AlbumId"] != null)
            {
                string albumId = Request.QueryString["AlbumId"];

                // 將目前頁面的 QueryString 保留並添加到新的 URL 中
                string redirectUrl = "PhotoFront.aspx?AlbumId=" + albumId;

                // 重新導向到下一個頁面
                Response.Redirect(redirectUrl);
            }
            else
            {
                // 如果 AlbumId 為空，您可以定義一個預設的重定向 URL
                Response.Redirect("PhotoFront.aspx");
            }
        }
    }
}