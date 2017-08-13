<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiniNotes.aspx.cs" Inherits="DevinDow.ToDo.MiniNotes" Title="Notes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>ToDo</title>
	    <link rel="stylesheet" href="ToDo.css" type="text/css" />
    </head>

    <body>
        <form id="form1" runat="server">
            <label class="title"><asp:LoginName ID="LoginName" runat="server" />'s Notes</label>
            <asp:LoginStatus ID="LoginStatus" runat="server" />
            
            <br />

			<asp:TextBox ID="txtNotes" runat="server" Width="300" Rows="5" TextMode="MultiLine" Font-Names="Verdana" />
			
			<asp:Button ID="btnUpdate" runat="server" Text="Update" style="position:relative; top:-5px;" OnClick="btnUpdateNotes_OnClick" />


            <hr />

            <a href="Default.aspx">Full Version</a> <br />
            <a href="MiniTasks.aspx">Handheld Tasks</a> <br />
            <a href="MiniActivity.aspx">Handheld Activities</a> <br />
            <a href="MiniGoal.aspx">Handheld Goals</a> <br />

        </form>
    </body>
</html>
