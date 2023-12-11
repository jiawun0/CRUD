﻿using System;
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
        protected string GetThumbnailUrl(string videoIframe)
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