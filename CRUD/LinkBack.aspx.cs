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
    public partial class LinkBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CategoryID"] != null)
                {
                    string CategoryID = Request.QueryString["CategoryID"];

                    ShowDB(CategoryID);
                }
            }
        }
        void ShowDB(string CategoryID)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLB"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //string sql = $"select PostTopic, PostAuthor, PostDate, ReplyDate from post, reply where post.Id = reply.postId";
            string sql = $"select l.Id, l.LinkName, l.LinkDescription, l.LinkCreatTime, l.LinkURL, l.LinkThumbnail " +
             $"from Link l " +
             $"join LinkCategory lc on l.CategoryID = lc.ID " +
             $"where l.CategoryID = @CategoryID ";

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@CategoryID", CategoryID);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            //執行該SQL查詢，用reader接資料
            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            GridView_LinkUpload.DataSource = reader;

            //GridView進行資料連接
            GridView_LinkUpload.DataBind();


            connection.Close();
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLB"].ConnectionString);
            //有檔案才能上傳
            if (FileUpload_LinkT.HasFile) //FileUpload1.PostedFile != null
            {
                try
                {
                    // 取得CategoryID
                    string CategoryID = Request.QueryString["CategoryID"];


                    //先檢查是否為空
                    if (!string.IsNullOrEmpty(CategoryID))
                    {
   
                        string FileName = Path.GetFileName(FileUpload_LinkT.FileName);
                        //string saveDirectory = @"C:\Users\88691\source\repos\ADO2\ADO\photo\";
                        string saveDirectory = Server.MapPath("~/Album/");
                        string savePath = Path.Combine(saveDirectory, FileName);
                        FileUpload_LinkT.SaveAs(savePath);

                        connection.Open();
                        string sql = "Insert into Link (LinkName, LinkDescription, LinkURL, LinkThumbnail, CategoryID) values(@LinkName, @LinkDescription, @LinkURL, @LinkThumbnail, @CategoryID) ";
                        SqlCommand sqlCommand = new SqlCommand(sql, connection);
                        sqlCommand.Parameters.AddWithValue("@LinkName", TextBox_LinkName.Text);
                        sqlCommand.Parameters.AddWithValue("@LinkDescription", TextBox_LinkDescription.Text);
                        sqlCommand.Parameters.AddWithValue("@LinkURL", TextBox_LinkURL.Text);
                        sqlCommand.Parameters.AddWithValue("@LinkThumbnail", savePath);
                        sqlCommand.Parameters.AddWithValue("@CategoryID", CategoryID);

                        // 將相對路徑存入資料庫
                        //string relativePath = "~/Album/" + saveDirectory;
                        //sqlCommand.Parameters.AddWithValue("@PhotoPath", relativePath);

                        //將準備的SQL指令給操作物件
                        sqlCommand.CommandText = sql;

                        sqlCommand.ExecuteNonQuery();

                        Response.Write("<script>alert('連結新增成功');</script>");
                        connection.Close();

                        Response.Redirect("LinkBack.aspx?CategoryID=" + CategoryID);
                    }

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }

            }
            else
            {
                Response.Write("<script>alert('沒有連結可新增');</script>");
            }

        }

        protected string GetRelativeImagePath(string linkThumbnail) //相對路徑
        {
            if (!string.IsNullOrEmpty(linkThumbnail))
            {
                string relativePath = linkThumbnail.Replace(Server.MapPath("~"), "").Replace(Server.MapPath("\\"), "/");
                return relativePath;
            }
            return string.Empty;
        }

        protected void GridView_LinkUpload_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_LinkUpload.EditIndex = e.NewEditIndex;

            string CategoryID = Request.QueryString["CategoryID"];
            ShowDB(CategoryID);
        }

        protected void GridView_LinkUpload_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_LinkUpload.Rows[e.RowIndex]; //找到目前gridview的編輯行數
            //DropDownList dropDownList = (DropDownList)row.FindControl("dropDownList");

            //string isActive = dropDownList.SelectedValue;
            int boardId = Convert.ToInt32(GridView_LinkUpload.DataKeys[e.RowIndex].Value); //取得資料表ID

            TextBox textBoxLN = row.FindControl("TextBox_TemplateLN") as TextBox;
            string changeTextLN = textBoxLN.Text;

            TextBox textBoxLD = row.FindControl("TextBox_TemplateLD") as TextBox;
            string changeTextLD = textBoxLD.Text;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLB"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update Link set LinkName = @LinkName, LinkDescription = @LinkDescription where Id = @BoardId";

            sqlCommand.Parameters.AddWithValue("@LinkName", changeTextLN);
            sqlCommand.Parameters.AddWithValue("@LinkDescription", changeTextLD);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('連結更新成功');</script>");
            GridView_LinkUpload.EditIndex = -1;

            string CategoryID = Request.QueryString["CategoryID"];
            ShowDB(CategoryID);
        }

        protected void GridView_LinkUpload_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_LinkUpload.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLB"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from Link where Id = @boardId";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('連結刪除成功');</script>");

            string CategoryID = Request.QueryString["CategoryID"];
            ShowDB(CategoryID);
        }

        protected void GridView_LinkUpload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_LinkUpload.EditIndex = -1;

            string CategoryID = Request.QueryString["CategoryID"];
            ShowDB(CategoryID);
        }
    }
}