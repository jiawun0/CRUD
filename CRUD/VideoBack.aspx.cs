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
    public partial class VideoBack : System.Web.UI.Page
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
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoBack"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //string sql = $"select PostTopic, PostAuthor, PostDate, ReplyDate from post, reply where post.Id = reply.postId";
            string sql = $"select v.Id, v.VideoName, v.VideoDescription, v.VideoCreatTime " +
             $"from Video v " +
             $"join VideoCategory vc on v.CategoryID = vc.ID " +
             $"where v.CategoryID = @CategoryID ";

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@CategoryID", CategoryID);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            //執行該SQL查詢，用reader接資料
            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            GridView_VideoBack.DataSource = reader;

            //GridView進行資料連接
            GridView_VideoBack.DataBind();


            connection.Close();
        }

        protected void CreateVideoBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoBack"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //查詢跟參數的部分很難寫成方法，需自行研究
            string sql = $"insert into Video (VideoName, VideoDescription, VideoURL, CategoryID) values(@VideoName, @VideoDescription, @VideoURL, @CategoryID)";
            string CategoryID = Request.QueryString["CategoryID"];

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@VideoName", TextBox_VideoName.Text);
            sqlCommand.Parameters.AddWithValue("@VideoDescription", TextBox_VideoDescription.Text);
            sqlCommand.Parameters.AddWithValue("@VideoURL", TextBox_VideoURL.Text);
            //sqlCommand.Parameters.AddWithValue("@VideoIframe", TextBox_VideoIframe.Text);
            sqlCommand.Parameters.AddWithValue("CategoryID", CategoryID);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            //執行非查詢的資料庫指令，ExecuteNonQuery() 會回傳受影響的資料數目，如果新增一筆卻顯示四筆，就是有誤
            sqlCommand.ExecuteNonQuery();

            Response.Redirect("VideoBack.aspx?CategoryID=" + CategoryID);

            connection.Close();
        }

        protected void FrontBtn_Click(object sender, EventArgs e)
        {
            string CategoryID = Request.QueryString["CategoryID"];
            Response.Redirect("VideoFront.aspx?CategoryID=" + CategoryID);
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("VideoCategoryBack.aspx");
        }

        protected void GridView_VideoBack_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_VideoBack.EditIndex = e.NewEditIndex;

            string CategoryID = Request.QueryString["CategoryID"];
            ShowDB(CategoryID);
        }

        protected void GridView_VideoBack_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_VideoBack.Rows[e.RowIndex]; //找到目前gridview的編輯行數
            //DropDownList dropDownList = (DropDownList)row.FindControl("dropDownList");

            //string isActive = dropDownList.SelectedValue;
            int boardId = Convert.ToInt32(GridView_VideoBack.DataKeys[e.RowIndex].Value); //取得資料表ID

            TextBox textBoxVN = row.FindControl("TextBox_TemplateVN") as TextBox;
            string changeTextVN = textBoxVN.Text;

            TextBox textBoxVD = row.FindControl("TextBox_TemplateVD") as TextBox;
            string changeTextVD = textBoxVD.Text;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoBack"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update Video set VideoName = @VideoName, VideoDescription = @VideoDescription where Id = @BoardId";


            sqlCommand.Parameters.AddWithValue("@VideoName", changeTextVN);
            sqlCommand.Parameters.AddWithValue("@VideoDescription", changeTextVD);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;

            //執行該SQL查詢，用reader接資料
            //SqlDataReader reader = sqlCommand.ExecuteReader();
            sqlCommand.ExecuteNonQuery();

            //使用這個reader物件的資料來取得內容
            //GridView_AlbumUpload.DataSource = reader;

            //GridView進行資料連接
            //GridView_AlbumUpload.DataBind();

            Response.Write("<script>alert('影片資料更新成功');</script>");
            GridView_VideoBack.EditIndex = -1;

            string CategoryID = Request.QueryString["CategoryID"];
            ShowDB(CategoryID);
        }

        protected void GridView_VideoBack_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_VideoBack.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectVideoBack"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from Video where Id = @boardId";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('影片資料刪除成功');</script>");

            string CategoryID = Request.QueryString["CategoryID"];
            ShowDB(CategoryID);
        }

        protected void GridView_VideoBack_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_VideoBack.EditIndex = -1;

            string CategoryID = Request.QueryString["CategoryID"];
            ShowDB(CategoryID);
        }
    }
}