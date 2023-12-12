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
            if (!IsPostBack)
            {
                ShowDB();
            }
            
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
                        //sqlCommand.Parameters.AddWithValue("@AlbumPath", folderPath);

                        // 將相對路徑存入資料庫，存入整串有風險
                        string relativePath = "~/Album/" + albumName;
                        sqlCommand.Parameters.AddWithValue("@AlbumPath", relativePath);

                        //將準備的SQL指令給操作物件
                        sqlCommand.CommandText = sql;

                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        ResultLabel.Text = "資料夾已存在";
                    }
                }
                catch (Exception ex)
                {
                    ResultLabel.Text = "資料夾創建失敗：" + ex.Message;
                }
            }
            else
            {
                ResultLabel.Text = "請輸入資料夾名稱";
            }
            Response.Redirect("AlbumBack.aspx");
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
            GridView_AlbumUpload.EditIndex = e.NewEditIndex;
            ShowDB();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_AlbumUpload.Rows[e.RowIndex]; //找到目前gridview的編輯行數
            //DropDownList dropDownList = (DropDownList)row.FindControl("dropDownList");

            //string isActive = dropDownList.SelectedValue;
            int boardId = Convert.ToInt32(GridView_AlbumUpload.DataKeys[e.RowIndex].Value); //取得資料表ID

            TextBox textBoxAN = row.FindControl("TextBox_TemplateAN") as TextBox;
            string changeTextAN = textBoxAN.Text;

            TextBox textBoxAD = row.FindControl("TextBox_TemplateAD") as TextBox;
            string changeTextAD = textBoxAD.Text;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw1"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update Album set AlbumName = @AlbumName, AlbumDescription = @AlbumDescription, AlbumPath = @AlbumPath where Id = @BoardId";


            sqlCommand.Parameters.AddWithValue("@AlbumName", changeTextAN);
            sqlCommand.Parameters.AddWithValue("@AlbumDescription", changeTextAD);
            sqlCommand.Parameters.AddWithValue("@AlbumPath", "~/Album/" + changeTextAN);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;

            //int s = helper.ExecuteSQL(sql);
            //if (s > 0) Response.Write("<script>alert('更新成功');</script>");
            //else Response.Write("<script>alert('更新失敗');</script>");

            sqlCommand.ExecuteNonQuery();

            //// 取得舊的、新的目錄名稱
            //string oldAlbumPath = "~/Album/" + e.OldValues["AlbumName"].ToString();
            //string newAlbumPath = "~/Album/" + changeTextAN;

            //// MapPath~取得實際路徑
            //string oldPhysicalPath = Server.MapPath(oldAlbumPath);
            //string newPhysicalPath = Server.MapPath(newAlbumPath);

            ////使用System.Io重新命名目錄名稱
            //if (Directory.Exists(oldPhysicalPath))
            //{
            //    Directory.Move(oldPhysicalPath, newPhysicalPath);
            //}

            connection.Close();

            Response.Write("<script>alert('相簿更新成功');</script>");
            GridView_AlbumUpload.EditIndex = -1;
            ShowDB();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_AlbumUpload.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw1"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from Album where Id = @boardId";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('相簿刪除成功');</script>");

            ShowDB();
        }
        protected void GridView_AlbumUpload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_AlbumUpload.EditIndex = -1;
            ShowDB();
        }

        protected void FrontBtn_Click(object sender, EventArgs e) 
        {
            Response.Redirect("AlbumFront.aspx");
        }
        protected void BtnRedirect_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string albumId = btn.CommandArgument;

                // 將 albumId 添加到 QueryString 並進行重定向
                Response.Redirect("PhotoBack.aspx?AlbumId=" + albumId);
            }
            else
            {
                // 如果 Id 為空，定義一個預設的重定向 URL
                Response.Redirect("PhotoBack.aspx");
            }
        }
    }
}