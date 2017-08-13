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

namespace DevinDow.ToDo
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
		{
			switch (Login1.UserName.ToLower())
			{
				case "devin":
					if (Login1.Password.ToLower() == "riley")
						FormsAuthentication.RedirectFromLoginPage("Devin", Login1.RememberMeSet);
					break;

				case "andrea":
					if (Login1.Password == "riley")
						FormsAuthentication.RedirectFromLoginPage("Andrea", Login1.RememberMeSet);
					break;
			}
		}
	}
}
