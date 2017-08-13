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
	public partial class MiniGoal : System.Web.UI.Page
	{
		protected void sdsGoals_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}

		protected void sdsGoals_Inserting(object sender, SqlDataSourceCommandEventArgs e)
		{
			if (e.Command.Parameters["@Goal"].Value == null)
			{
				e.Cancel = true;
				return;
			}

			e.Command.Parameters["@UserName"].Value = User.Identity.Name;
		}
	}
}
