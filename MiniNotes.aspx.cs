using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Data.SqlClient;

namespace DevinDow.ToDo
{
	public partial class MiniNotes : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string text;

				using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
				{
					SqlCommand sqlCommand = new SqlCommand("SELECT Text " +
						"FROM Notes " +
						"WHERE UserName = @UserName", sqlConnection);

					sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = User.Identity.Name;

					sqlConnection.Open();
					text = (string)sqlCommand.ExecuteScalar();
				}

				if (text == null)
					using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
					{
						SqlCommand sqlCommand = new SqlCommand("INSERT INTO Notes (Text, UserName) " +
							"VALUES ('', @UserName)", sqlConnection);

						sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = User.Identity.Name;

						sqlConnection.Open();
						sqlCommand.ExecuteNonQuery();
					}
				else
					txtNotes.Text = text;
			}
		}

		public void btnUpdateNotes_OnClick(Object sender, EventArgs e)
		{
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE Notes SET Text=@Text WHERE UserName=@UserName", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = txtNotes.Text;
				sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = User.Identity.Name;

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}
	}
}
