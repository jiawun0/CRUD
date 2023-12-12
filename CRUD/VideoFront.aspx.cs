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
    public partial class VideoFront : System.Web.UI.Page
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
            //SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=LMS;Integrated Security=True");

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "select Id, VideoName, VideoDescription, VideoURL, VideoIframe, CategoryID from Video where CategoryID = @CategoryID";

            sqlCommand.Parameters.AddWithValue("@CategoryID", CategoryID);

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            Repeater_Video.DataSource = reader;

            //進行資料連接
            Repeater_Video.DataBind();

            connection.Close();
        }

        protected string GetVideoID(string fullUrl) //如果忘記將videoIframe加入資料庫
        {
            if (!string.IsNullOrEmpty(fullUrl) && fullUrl.Contains("youtube.com/watch?v="))
            {
                //IndexOf，找到後從v開始算0
                int index = fullUrl.IndexOf("v=") + 2;

                if (index != -1)
                {
                    //substring
                    string videoID = fullUrl.Substring(index);

                    //如果Url有其他符號
                    int endindex = videoID.IndexOf('&');

                    if (endindex != -1)
                    {
                        videoID = videoID.Substring(0,endindex);
                    }
                    return videoID;
                }
            }
            return string.Empty;
        }
        protected string GetThumbnailUrl(string videoIframe) //取得縮圖
        {
            if (!string.IsNullOrEmpty(videoIframe))
            {
                string ThumbnailUrl = @"https:\\img.youtube.com\vi\" + videoIframe + @"\default.jpg";
                return ThumbnailUrl;
            }
            return string.Empty;
        }
        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("VideoBack.aspx");
        }
    }
}