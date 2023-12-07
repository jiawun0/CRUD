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
    public partial class AlbumBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowDB();
        }
        protected void CreateFileBtn_Click(object sender, EventArgs e)
        {
            // 取得資料夾名稱
            string albumName = TextBox_AlbumName.Text.Trim();

            if (!string.IsNullOrEmpty(albumName))
            {
                try
                {
                    // 指定要創建資料夾的路徑
                    string folderPath = Server.MapPath("~/Album/" + albumName);

                    // 檢查資料夾是否已經存在，若不存在則創建
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                        ResultLabel.Text = "資料夾已成功創建。";

                        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw1"].ConnectionString);

                        if (connection.State != System.Data.ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        //發送SQL語法，取得結果
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = connection;

                        //查詢跟參數的部分很難寫成方法，需自行研究
                        string sql = $"insert into Album (AlbumName, AlbumDescription, AlbumPath) values(@AlbumName, @AlbumDescription, @AlbumPath)";

                        //增加參數並設定值，記得用.叫出來
                        sqlCommand.Parameters.AddWithValue("@AlbumName", TextBox_AlbumName.Text);
                        sqlCommand.Parameters.AddWithValue("@AlbumDescription", TextBox_AlbumDescription.Text);
                        sqlCommand.Parameters.AddWithValue("@AlbumPath", folderPath);

                        // 將相對路徑存入資料庫
                        //string relativePath = "~/Album/" + albumName;
                        //sqlCommand.Parameters.AddWithValue("@AlbumPath", relativePath);

                        //將準備的SQL指令給操作物件
                        sqlCommand.CommandText = sql;

                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        ResultLabel.Text = "資料夾已存在。";
                    }
                }
                catch (Exception ex)
                {
                    ResultLabel.Text = "資料夾創建失敗：" + ex.Message;
                }
            }
            else
            {
                ResultLabel.Text = "請輸入資料夾名稱。";
            }
        }

        void ShowDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw1"].ConnectionString);
            //SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=LMS;Integrated Security=True");

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string sql = "select AlbumName, AlbumDescription, AlbumPath from Album ";

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.Connection = connection;


            //將準備的SQL指令給操作物件
            //sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            GridView_AlbumUpload.DataSource = reader;

            //進行資料連接
            GridView_AlbumUpload.DataBind();

            connection.Close();
        }


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}