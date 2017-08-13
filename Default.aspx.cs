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
	public partial class Default : System.Web.UI.Page
	{
		public void ddlTimeFrame_OnSelectedIndexChanged(Object sender, EventArgs e)
		{
			DropDownList dropDownList = (DropDownList)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE ListItems SET Priority=@Priority WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = dropDownList.SelectedValue;
				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptTasks, dropDownList);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnInsertTask_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO ListItems (UserID, ListID, Text, Priority) VALUES (1, 2, @Text, @Priority)", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = ((TextBox)Util.FindControlRecursive(rptTasks, "txtTaskText_Insert")).Text;
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = ((DropDownList)Util.FindControlRecursive(rptTasks, "ddlTimeFrame_Insert")).SelectedValue;

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnUpdateTask_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE ListItems SET Text=@Text WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = getTask(button);
				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptTasks, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnDeleteTask_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("DELETE FROM ListItems WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptTasks, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		private int getItemID(Repeater repeater, Control control)
		{
			foreach (RepeaterItem item in repeater.Items)
				if (item.FindControl(control.ID) == control)
				{
					Label label = (Label)item.FindControl("lblID");
					return parsePriority(label.Text);
				}

			return -1;
		}
		private string getTask(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptTasks.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtTaskText");
					return textBox.Text;
				}

			return string.Empty;
		}
		private int getTaskTimeFrame(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptTasks.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					DropDownList dropDownList = (DropDownList)item.FindControl("ddlTimeFrame");
					return int.Parse(dropDownList.SelectedValue);
				}

			return -1;
		}

		public void ddlTimeFrame_OnDataBound(Object sender, EventArgs e)
		{
			DropDownList dropDownList = (DropDownList)sender;
			switch (dropDownList.SelectedValue)
			{
				case "0":
					dropDownList.BackColor = Color.Red;
					dropDownList.ForeColor = Color.White;
					break;
				case "1":
					dropDownList.BackColor = Color.Orange;
					dropDownList.ForeColor = Color.White;
					break;
				case "2":
					dropDownList.BackColor = Color.Yellow;
					dropDownList.ForeColor = Color.Gray;
					break;
				case "3":
					dropDownList.BackColor = Color.Green;
					dropDownList.ForeColor = Color.White;
					break;
				case "4":
					dropDownList.BackColor = Color.Blue;
					dropDownList.ForeColor = Color.White;
					break;
				case "5":
					dropDownList.BackColor = Color.Purple;
					dropDownList.ForeColor = Color.White;
					break;
				case "6":
					dropDownList.BackColor = Color.Black;
					dropDownList.ForeColor = Color.White;
					break;
				case "7":
					dropDownList.BackColor = Color.DarkGray;
					dropDownList.ForeColor = Color.White;
					break;
				case "8":
					dropDownList.BackColor = Color.LightGray;
					dropDownList.ForeColor = Color.Black;
					break;
			}
		}


		// Activities
		public void btnInsertActivity_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO ListItems (UserID, ListID, Text, Priority) VALUES (1, 3, @Text, @Priority)", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = ((TextBox)Util.FindControlRecursive(rptActivities, "txtActivityText_Insert")).Text;
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = parsePriority(((TextBox)Util.FindControlRecursive(rptActivities, "txtActivityPriority_Insert")).Text);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnUpdateActivity_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE ListItems SET Text=@Text, Priority=@Priority WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = getActivity(button);
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = getActivityPriority(button);
				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptActivities, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnDeleteActivity_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("DELETE FROM ListItems WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptActivities, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		private int getActivityPriority(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptActivities.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtActivityPriority");
					return parsePriority(textBox.Text);
				}

			return 0;
		}

		private string getActivity(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptActivities.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtActivityText");
					return textBox.Text;
				}

			return string.Empty;
		}


		// Priorities
		public void btnInsertPriority_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO ListItems (UserID, ListID, Text, Priority) VALUES (1, 4, @Text, @Priority)", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = ((TextBox)Util.FindControlRecursive(rptPriorities, "txtPriorityText_Insert")).Text;
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = parsePriority(((TextBox)Util.FindControlRecursive(rptPriorities, "txtPriorityPriority_Insert")).Text);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnUpdatePriority_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE ListItems SET Text=@Text, Priority=@Priority WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = getPriority(button);
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = getPriorityPriority(button);
				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptPriorities, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnDeletePriority_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("DELETE FROM ListItems WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptPriorities, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		private int getPriorityPriority(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptPriorities.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtPriorityPriority");
					return parsePriority(textBox.Text);
				}

			return 0;
		}

		private string getPriority(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptPriorities.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtPriorityText");
					return textBox.Text;
				}

			return string.Empty;
		}


		// Goals
		public void btnInsertGoal_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO ListItems (UserID, ListID, Text, Priority) VALUES (1, 5, @Text, @Priority)", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = ((TextBox)Util.FindControlRecursive(rptGoals, "txtGoalText_Insert")).Text;
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = parsePriority(((TextBox)Util.FindControlRecursive(rptGoals, "txtGoalPriority_Insert")).Text);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnUpdateGoal_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE ListItems SET Text=@Text, Priority=@Priority WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = getGoal(button);
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = getGoalPriority(button);
				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptGoals, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnDeleteGoal_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("DELETE FROM ListItems WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptGoals, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		private int getGoalPriority(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptGoals.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtGoalPriority");
					return parsePriority(textBox.Text);
				}

			return 0;
		}

		private string getGoal(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptGoals.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtGoalText");
					return textBox.Text;
				}

			return string.Empty;
		}


		// Desires
		public void btnInsertDesire_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO ListItems (UserID, ListID, Text, Priority) VALUES (1, 6, @Text, @Priority)", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = ((TextBox)Util.FindControlRecursive(rptDesires, "txtDesireText_Insert")).Text;
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = parsePriority(((TextBox)Util.FindControlRecursive(rptDesires, "txtDesirePriority_Insert")).Text);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnUpdateDesire_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("UPDATE ListItems SET Text=@Text, Priority=@Priority WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = getDesire(button);
				sqlCommand.Parameters.Add("@Priority", SqlDbType.Int).Value = getDesirePriority(button);
				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptDesires, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		public void btnDeleteDesire_OnClick(Object sender, EventArgs e)
		{
			Button button = (Button)sender;
			using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevinDowConnectionString"].ConnectionString))
			{
				SqlCommand sqlCommand = new SqlCommand("DELETE FROM ListItems WHERE ID=@ID", sqlConnection);

				sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = getItemID(rptDesires, button);

				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			Response.Redirect(Request.Url.ToString());
		}

		private int getDesirePriority(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptDesires.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtDesirePriority");
					return parsePriority(textBox.Text);
				}

			return 0;
		}

		private string getDesire(Button btnUpdate)
		{
			foreach (RepeaterItem item in rptDesires.Items)
				if (item.FindControl(btnUpdate.ID) == btnUpdate)
				{
					TextBox textBox = (TextBox)item.FindControl("txtDesireText");
					return textBox.Text;
				}

			return string.Empty;
		}



		// Priority
		private int parsePriority(string priorityString)
		{
			try
			{
				int priority = int.Parse(priorityString);
				return priority;
			}
			catch (Exception)
			{
				return 0;
			}
		}


		// Export
		protected void btnExport_Click(object sender, EventArgs e)
		{
			string userName = User.Identity.Name;
			DateTime dateTime = DateTime.Now;

			string filename = string.Format("{0}'s ToDos - {1}", userName, dateTime.ToString("s"));
			filename = filename.Replace(':', '-');
			string outputFileName = Request.PhysicalApplicationPath + string.Format("Download/{0}.xml", filename);
			// Server.MapPath() issues
			//string outputFileName = HttpContext.Current.Server.MapPath(string.Format("/ToDo/Download/{0}.xml", filename));

			//string dir = HttpContext.Current.Server.MapPath("/ToDo/Download");
			string dir = Request.PhysicalApplicationPath + "Download";
			/*Trace.Warn("Exists " + Directory.Exists(dir).ToString() + "\n");
			if (Directory.Exists(dir))
			{
				string[] files = Directory.GetFiles(dir);
				foreach (string file in files)
					Trace.Warn("  - " + file + "\n");
			}*/

			XmlWriterSettings xmlSettings = new XmlWriterSettings();
			xmlSettings.Indent = true;
			XmlWriter xml = XmlTextWriter.Create(outputFileName, xmlSettings);

			xml.WriteStartElement("ToDos");
			xml.WriteAttributeString("User", userName);
			xml.WriteAttributeString("DateTime", dateTime.ToString());

			ToDoDataContext db = new ToDoDataContext();


			// Tasks
			var tasks = 
				from task in db.Tasks
				join timeFrame in db.TimeFrames on task.TimeFrame equals timeFrame.ID
				where task.UserName == userName
				orderby task.TimeFrame
				select new { TimeFrame = timeFrame.Name, Name = task.Name };

			xml.WriteStartElement("Tasks");
			foreach (var task in tasks)
			{
				xml.WriteStartElement("Task");
				xml.WriteAttributeString("TimeFrame", task.TimeFrame);
				xml.WriteAttributeString("Name", task.Name);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();


			// Activities
			var activities = 
				from activity in db.Activities
				where activity.UserName == userName
				orderby activity.Priority
				select activity;

			xml.WriteStartElement("Activities");
			foreach (var activity in activities)
			{
				xml.WriteStartElement("Activity");
				xml.WriteAttributeString("Priority", activity.Priority.ToString());
				xml.WriteAttributeString("Name", activity.Name);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();


			// Priorities
			var priorities = 
				from priority in db.Priorities
				where priority.UserName == userName
				orderby priority.Priority
				select priority;

			xml.WriteStartElement("Priorities");
			foreach (var priority in priorities)
			{
				xml.WriteStartElement("Priority");
				xml.WriteAttributeString("Priority", priority.Priority.ToString());
				xml.WriteAttributeString("Name", priority.Name);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();


			// Goals
			var goals = 
				from goal in db.Goals
				where goal.UserName == userName
				orderby goal.Priority
				select goal;

			xml.WriteStartElement("Goals");
			foreach (var goal in goals)
			{
				xml.WriteStartElement("Goal");
				xml.WriteAttributeString("Priority", goal.Priority.ToString());
				xml.WriteAttributeString("Name", goal.Name);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();


			// Desires
			var desires = 
				from desire in db.Desires
				where desire.UserName == userName
				orderby desire.Priority
				select desire;

			xml.WriteStartElement("Desires");
			foreach (var desire in desires)
			{
				xml.WriteStartElement("Desire");
				xml.WriteAttributeString("Priority", desire.Priority.ToString());
				xml.WriteAttributeString("Name", desire.Name);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();


			// Notes
			var notes = 
				from note in db.Notes
				where note.UserName == userName
				select note;

			xml.WriteStartElement("Notes");
			foreach (var note in notes)
			{
				xml.WriteStartElement("Note");
				xml.WriteAttributeString("Text", note.Text);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();


			xml.WriteEndElement();
			xml.Close();


			Response.Clear();
			Response.AddHeader("content-disposition", "attachment;filename=" + filename);
			Response.ContentType = "text/xml";
			Response.TransmitFile(outputFileName);
			Response.Flush();
			Response.End();	
		}
	}
}
