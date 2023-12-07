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
                    string saveDirectory = Server.MapPath("photo/");// 實際伺服器路徑
                    string savePath = Path.Combine(saveDirectory, FileName);
                    FileUpload1.SaveAs(savePath);
                    Label1.Text = "已成功上傳: " + FileName;

                    connection.Open();
                    string sql = "Insert into Album (AlbumName,AlbumPath) values(@AlbumName, @AlbumPath)";
                    SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    sqlCommand.Parameters.AddWithValue("@AlbumName", FileName); // 使用檔案名稱作為相簿名稱
                    sqlCommand.Parameters.AddWithValue("@AlbumPath", savePath);
                    //sqlCommand.Parameters.AddWithValue("@AlbumPath", @"C:\Users\88691\source\repos\ADO2\ADO\photo\" + FileName);
                    //sqlCommand.Parameters.AddWithValue("@AlbumPath", "photo/" + FileName);
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

            string sql = "select Photo.Id, Photo.PhotoName, Photo.PhotoDescription, PhotoPath from Photo " +
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
    }
}