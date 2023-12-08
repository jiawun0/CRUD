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
                    // 取得albumId
                    string albumId = Request.QueryString["AlbumId"];

                    // 取得albumName，在ShowDB()內存成session
                    //string albumName = Session["AlbumName"].ToString();

                    // 建立一個GetAlbumName方法可以抓取albumId對應的reader.AlbumName
                    string albumName = GetAlbumName(albumId, connection);

                    //先檢查session是否為空
                    if (!string.IsNullOrEmpty(albumName))
                    {
                        //string FileName = FileUpload1.FileName;
                        //savePath = savePath + FileName;
                        //string saveDirectory = Server.MapPath("~ADO2/ADO/photo/");
                        //FileUpload1.SaveAs(savePath);
                        string FileName = Path.GetFileName(FileUpload1.FileName);
                        //string saveDirectory = @"C:\Users\88691\source\repos\ADO2\ADO\photo\";
                        string saveDirectory = Server.MapPath("~/Album/" + albumName + "/");// 相簿名稱路徑
                        string savePath = Path.Combine(saveDirectory, FileName);
                        FileUpload1.SaveAs(savePath);
                        //Label1.Text = "已成功上傳: " + FileName;

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

                        Response.Write("<script>alert('相片新增成功');</script>");
                        connection.Close();

                        Response.Redirect("PhotoBack.aspx?AlbumId=" + albumId);
                    }
                    
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }

            }
            else
            {
                Response.Write("<script>alert('沒有檔案可新增');</script>");
            }

            
        }
        void ShowDB(string albumId) //顯示目前相簿中相片
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

            string sql = "select Album.AlbumName, Photo.Id, Photo.PhotoName, Photo.PhotoDescription, Photo.PhotoPath, Photo.IsCover from Photo " +
                " left join Album on Album.Id = Photo.AlbumId " +
                " where AlbumId = @AlbumId ";

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@AlbumId", albumId);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //reader.NextResult();

            //使用這個reader物件的資料來取得內容
            GridView_PhotoUpload.DataSource = reader;

            //進行資料連接
            GridView_PhotoUpload.DataBind();

            //if (reader.HasRows) //將AlbumName存入session
            //{
            //    while (reader.Read())
            //    {
            //        string albumName = reader["AlbumName"].ToString();
            //        HttpContext.Current.Session["AlbumName"] = albumName;
            //    }
            //}
            //reader.Close();

            connection.Close();
        }
        
        private string GetAlbumName(string albumId, SqlConnection connection)
        {
            string albumName = string.Empty;

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;

                string sql = "select AlbumName from Album where Id = @AlbumId";

                sqlCommand.Parameters.AddWithValue("@AlbumId", albumId);

                //將準備的SQL指令給操作物件
                sqlCommand.CommandText = sql;

                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    albumName = reader["AlbumName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return albumName;
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

        protected void GridView_PhotoUpload_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_PhotoUpload.EditIndex = e.NewEditIndex;

            string albumId = Request.QueryString["AlbumId"];
            ShowDB(albumId);
        }

        protected void GridView_PhotoUpload_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_PhotoUpload.Rows[e.RowIndex]; //找到目前gridview的編輯行數
            //DropDownList dropDownList = (DropDownList)row.FindControl("dropDownList");

            //string isActive = dropDownList.SelectedValue;
            int boardId = Convert.ToInt32(GridView_PhotoUpload.DataKeys[e.RowIndex].Value); //取得資料表ID

            TextBox textBoxPN = row.FindControl("TextBox_TemplatePN") as TextBox;
            string changeTextPN = textBoxPN.Text;

            TextBox textBoxPD = row.FindControl("TextBox_TemplatePD") as TextBox;
            string changeTextPD = textBoxPD.Text;

            CheckBox checkBoxIC = row.FindControl("CheckBox_TemplateIC") as CheckBox;
            bool isCover = Convert.ToBoolean(checkBoxIC.Checked);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw12"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update Photo set PhotoName = @PhotoName, PhotoDescription = @PhotoDescription, IsCover = @IsCover where Id = @BoardId";


            sqlCommand.Parameters.AddWithValue("@PhotoName", changeTextPN);
            sqlCommand.Parameters.AddWithValue("@PhotoDescription", changeTextPD);
            sqlCommand.Parameters.AddWithValue("@IsCover", isCover);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;

            //執行該SQL查詢，用reader接資料
            //SqlDataReader reader = sqlCommand.ExecuteReader();
            sqlCommand.ExecuteNonQuery();

            //使用這個reader物件的資料來取得內容
            //GridView_AlbumUpload.DataSource = reader;

            //GridView進行資料連接
            //GridView_AlbumUpload.DataBind();

            connection.Close();
            string albumId = Request.QueryString["AlbumId"];
            if (isCover)
            {
                SetSingleCoverTrue(albumId, boardId);
            }

            Response.Write("<script>alert('相片更新成功');</script>");
            GridView_PhotoUpload.EditIndex = -1;

            ShowDB(albumId);
            //string albumId = Request.QueryString["AlbumId"];
            //bool isCoverUnique = IsSingleCover(albumId, boardId);
            //if (isCoverUnique)
            //{
            //    ShowDB(albumId);
            //}
        }
        private void SetSingleCoverTrue(string albumId, int boardId)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw12"].ConnectionString);

            try
            {
                connection.Open();

                // 先將目前相片設定為 IsCover = false
                string updateSql = "UPDATE Photo SET IsCover = 0 WHERE AlbumId = @AlbumId";
                SqlCommand updateCommand = new SqlCommand(updateSql, connection);
                updateCommand.Parameters.AddWithValue("@AlbumId", albumId);
                updateCommand.ExecuteNonQuery();

                // 再將指定的相片設定為 IsCover = true
                string setTrueSql = "UPDATE Photo SET IsCover = 1 WHERE Id = @BoardId";
                SqlCommand setTrueCommand = new SqlCommand(setTrueSql, connection);
                setTrueCommand.Parameters.AddWithValue("@BoardId", boardId);
                setTrueCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // 處理例外
            }
            finally
            {
                connection.Close();
            }
        }
        //檢查是否只有一個IsCover為true
        //private bool IsSingleCover(string albumId, int boardId)
        //{
        //    bool IsSingle = false;
        //    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw12"].ConnectionString);

        //    try
        //    {
        //        connection.Open();
        //        string sql = "select count(*) from Photo " +
        //            " where AlbumId = @AlbumId and IsCover = 1 and Id != @BoardId";

        //        SqlCommand sqlCommand = new SqlCommand(sql, connection);
        //        sqlCommand.Parameters.AddWithValue("@AlbumId", albumId);
        //        sqlCommand.Parameters.AddWithValue("@BoardId", boardId);

        //        int f = Convert.ToInt32(sqlCommand.ExecuteScalar());
        //        if(f == 0) IsSingle = true;
        //        else IsSingle = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        //異常
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return IsSingle;
        //}

        protected void GridView_PhotoUpload_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_PhotoUpload.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectsqlhw12"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteReplySql = $"delete from Photo where Id = @boardId";
            SqlCommand deleteReplyCommand = new SqlCommand(deleteReplySql, connection);
            deleteReplyCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteReplyCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('資料刪除成功');</script>");

            string albumId = Request.QueryString["AlbumId"];
            ShowDB(albumId);
        }

        protected void GridView_PhotoUpload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_PhotoUpload.EditIndex = -1;

            string albumId = Request.QueryString["AlbumId"];
            ShowDB(albumId);
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

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AlbumBack.aspx");
        }
    }
}