<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiniGoal.aspx.cs" Inherits="DevinDow.ToDo.MiniGoal" Title="Goals" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>ToDo</title>
	    <link rel="stylesheet" href="ToDo.css" type="text/css" />
    </head>

    <body>
        <form id="form1" runat="server">
            <label class="title"><asp:LoginName ID="LoginName" runat="server" />'s Goals</label>
            <asp:LoginStatus ID="LoginStatus" runat="server" />

            <asp:ListView ID="lsvGoals" runat="server" 
                    DataSourceID="sdsGoals" DataKeyNames="ID" 
                    InsertItemPosition="FirstItem" >

                <EmptyDataTemplate>
                    No Goals.
                </EmptyDataTemplate>

                <LayoutTemplate>
                    <table>
                        <tr ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>

                <InsertItemTemplate>
                    <tr>
                        <td> <asp:TextBox ID="txtPriority_Insert" runat="server" Text='<%# Bind("Priority") %>' Width="25" /> </td>
                        <td> <asp:TextBox ID="txtName_Insert" runat="server" Text='<%# Bind("Name") %>' /> </td>
                        <td> <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="Add" /> </td>
                    </tr>
                </InsertItemTemplate>

                <ItemTemplate>
                    <tr>
                        <td> <asp:Label ID="lblPriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" /> </td>
                        <td> <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' /> </td>
                        <td> <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" /> </td>
                        <td> <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this Goal?');" /> </td>
                    </tr>
                </ItemTemplate>

                <EditItemTemplate>
                    <tr>
                        <td> <asp:TextBox ID="txtPriority" runat="server" Text='<%# Bind("Priority") %>' Width="25" /> </td>
                        <td> <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' /> </td>
                        <td> <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" /> </td>
                        <td> <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" /> </td>
                        <td> <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this Goal?');" /> </td>
                    </tr>
                </EditItemTemplate>

            </asp:ListView>
            
            
            <asp:SqlDataSource ID="sdsGoals" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
                    SelectCommand="SELECT ID, Name, Priority 
                                    FROM Goals 
                                    WHERE UserName = @UserName 
                                    ORDER BY Priority, Name" 
                    UpdateCommand="UPDATE Goals SET Name=@Name, Priority=@Priority WHERE ID=@ID"
                    DeleteCommand="DELETE FROM Goals WHERE ID=@ID" 
                    InsertCommand="INSERT INTO Goals (UserName, Name, Priority) VALUES (@UserName, @Name, @Priority)" 
                    oninserting="sdsGoals_Inserting" 
                    onselecting="sdsGoals_Selecting" >
                <SelectParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Priority" Type="Int32" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Priority" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>


            <hr />

            <a href="Default.aspx">Full Version</a> <br />
            <a href="MiniTasks.aspx">Handheld Tasks</a> <br />
            <a href="MiniActivity.aspx">Handheld Activities</a> <br />
			<a href="MiniNotes.aspx">Handheld Notes</a> <br />

        </form>
    </body>
</html>
