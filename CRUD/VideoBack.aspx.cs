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
            string sql = $"insert into Video (VideoName, VideoDescription, VideoURL, VideoIframe, CategoryID) values(@VideoName, @VideoDescription, @VideoURL, @VideoIframe, @CategoryID)";
            string CategoryID = Request.QueryString["CategoryID"];

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@VideoName", TextBox_VideoName.Text);
            sqlCommand.Parameters.AddWithValue("@VideoDescription", TextBox_VideoDescription.Text);
            sqlCommand.Parameters.AddWithValue("@VideoURL", TextBox_VideoURL.Text);
            sqlCommand.Parameters.AddWithValue("@VideoIframe", TextBox_VideoIframe.Text);
            sqlCommand.Parameters.AddWithValue("CategoryID", CategoryID);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            //執行非查詢的資料庫指令，ExecuteNonQuery() 會回傳受影響的資料數目，如果新增一筆卻顯示四筆，就是有誤
            sqlCommand.ExecuteNonQuery();

            Response.Redirect("VideoBack.aspx?CategoryID=" + CategoryID);

            connection.Close();
        }
    }
}