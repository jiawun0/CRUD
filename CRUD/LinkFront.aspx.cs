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
    public partial class LinkFront : System.Web.UI.Page
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
            Repeater_link.DataSource = reader;

            //GridView進行資料連接
            Repeater_link.DataBind();


            connection.Close();
        }

        protected string GetRelativeImagePath(string linkThumbnail)
        {
            if (!string.IsNullOrEmpty(linkThumbnail))
            {
                string relativePath = linkThumbnail.Replace(Server.MapPath("~"), "").Replace(Server.MapPath("\\"), "/");
                return relativePath;
            }
            return string.Empty;
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CategoryID"] != null)
            {
                string CategoryID = Request.QueryString["CategoryID"];

                // 將目前頁面的 QueryString 保留並添加到新的 URL 中
                string redirectUrl = "LinkBack.aspx?CategoryID=" + CategoryID;

                // 重新導向到下一個頁面
                Response.Redirect(redirectUrl);
            }
            else
            {
                // 如果 AlbumId 為空，您可以定義一個預設的重定向 URL
                Response.Redirect("LinkBack.aspx");
            }

        }
    }
}