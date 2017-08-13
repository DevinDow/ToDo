using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace DevinDow.ToDo
{
	public class Util
	{
		public static Control FindControlRecursive(Control root, string id)
		{
			if (root.ID == id)
				return root;

			foreach (Control control in root.Controls)
			{
				Control targetControl = FindControlRecursive(control, id);
				if (targetControl != null)
					return targetControl;
			}

			return null;
		}
	}
}
