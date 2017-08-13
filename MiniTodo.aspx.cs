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
	public partial class MiniTodo : System.Web.UI.Page
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

			DropDownList ddlDPriority = (DropDownList)Util.FindControlRecursive(lsvTasks, "ddlPriority_insert");
			e.Command.Parameters["@Priority"].Value = ddlDPriority.SelectedValue;
		}

		protected void btnDone_OnCommand(Object sender, CommandEventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE Tasks SET Done=1, Priority=-1 WHERE ID=@ID", sqlConnection);
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

		protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
		{
			if (e.Item.ItemType == ListViewItemType.DataItem)
			{
				Label lblPriority = (Label)e.Item.FindControl("PriorityLabel");
				if (lblPriority == null)
					return;
				switch (lblPriority.Text)
				{
					case "Now":
						lblPriority.BackColor = Color.Red;
						break;
					case "Today":
						lblPriority.BackColor = Color.Orange;
						break;
					case "Tomorrow":
						lblPriority.BackColor = Color.Yellow;
						lblPriority.ForeColor = Color.Gray;
						break;
					case "This Week":
						lblPriority.BackColor = Color.Green;
						break;
					case "Next Week":
						lblPriority.BackColor = Color.Blue;
						break;
					case "This Month":
						lblPriority.BackColor = Color.Purple;
						break;
					case "Next Month":
						lblPriority.BackColor = Color.Black;
						break;
					case "This Year":
						lblPriority.BackColor = Color.DarkGray;
						break;
					case "Some Day":
						lblPriority.BackColor = Color.LightGray;
						lblPriority.ForeColor = Color.Black;
						break;
				}
			}
		}
	}
}
