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
	public partial class MiniTasks : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void sdsTasks_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}
		protected void sdsDone_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}

		protected void sdsTasks_Inserting(object sender, SqlDataSourceCommandEventArgs e)
		{
			if (e.Command.Parameters["@Task"].Value == null)
			{
				e.Cancel = true;
				return;
			}

			e.Command.Parameters["@UserName"].Value = User.Identity.Name;

			DropDownList ddlTimeFrame = (DropDownList)Util.FindControlRecursive(lsvTasks, "ddlTimeFrame_insert");
			e.Command.Parameters["@TimeFrame"].Value = ddlTimeFrame.SelectedValue;
		}

		protected void btnDone_OnCommand(Object sender, CommandEventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE Tasks SET Done=1, TimeFrame=-1 WHERE ID=@ID", sqlConnection);
				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(e.CommandArgument.ToString());

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}
		protected void btnNotDone_OnCommand(Object sender, CommandEventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE Tasks SET Done=0 WHERE ID=@ID", sqlConnection);
				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(e.CommandArgument.ToString());

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnClear_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("DELETE Tasks WHERE UserName=@UserName AND Done=1", sqlConnection);
				sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = User.Identity.Name;

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		protected void lsvTasks_ItemDataBound(object sender, ListViewItemEventArgs e)
		{
			if (e.Item.ItemType == ListViewItemType.DataItem)
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
}
