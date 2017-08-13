using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Drawing;
using System.Data.SqlClient;
using ToDo;
using System.Xml;
using System.IO;

namespace DevinDow.ToDo
{
	public partial class Print : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
				{
					SqlCommand sqlCommand = new SqlCommand("SELECT Text " +
						"FROM Notes " +
						"WHERE UserName = @UserName", sqlConnection);

					sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = User.Identity.Name;

					sqlConnection.Open();
					txtNotes.Text = (string)sqlCommand.ExecuteScalar();
				}
			}
		}

		protected void sdsTasks_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}
		protected void sdsDone_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}
		protected void sdsActivities_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}
		protected void sdsPriorities_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}
		protected void sdsGoals_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}
		protected void sdsDesires_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}

		protected void rptTasks_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			Label lblTimeFrame = (Label)e.Item.FindControl("TimeFrameLabel");
			if (lblTimeFrame == null)
				return;
			switch (lblTimeFrame.Text)
			{
				case "Now":
					lblTimeFrame.BackColor = Color.Red;
					break;
				case "Today":
					lblTimeFrame.BackColor = Color.Orange;
					break;
				case "Tomorrow":
					lblTimeFrame.BackColor = Color.Yellow;
					lblTimeFrame.ForeColor = Color.Gray;
					break;
				case "This Week":
					lblTimeFrame.BackColor = Color.Green;
					break;
				case "Next Week":
					lblTimeFrame.BackColor = Color.Blue;
					break;
				case "This Month":
					lblTimeFrame.BackColor = Color.Purple;
					break;
				case "Next Month":
					lblTimeFrame.BackColor = Color.Black;
					break;
				case "This Year":
					lblTimeFrame.BackColor = Color.DarkGray;
					break;
				case "Some Day":
					lblTimeFrame.BackColor = Color.LightGray;
					lblTimeFrame.ForeColor = Color.Black;
					break;
			}
		}
	}
}
