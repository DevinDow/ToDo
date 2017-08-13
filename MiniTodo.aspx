<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiniTodo.aspx.cs" Inherits="DevinDow.ToDo.MiniTodo" Title="TODOs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>ToDo</title>
	    <link rel="stylesheet" href="ToDo.css" type="text/css" />
    </head>

    <body>
        <form id="form1" runat="server">
            <label class="title"><asp:LoginName ID="LoginName" runat="server" />'s ToDos</label>
            <asp:LoginStatus ID="LoginStatus" runat="server" />

            <div>
                <asp:ListView ID="lsvTasks" runat="server" 
                        DataSourceID="sdsTasks" DataKeyNames="ID" 
                        InsertItemPosition="FirstItem" onitemdatabound="ListView1_ItemDataBound" >

                    <EmptyDataTemplate>
                        No tasks.
                    </EmptyDataTemplate>

                    <LayoutTemplate>
                        <table>
                            <tr ID="itemPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>

                    <InsertItemTemplate>
                        <tr>
                            <td align="right">
                                <asp:DropDownList ID="ddlPriority_insert" runat="server" 
                                        DataSourceID="sdsPriorities" DataTextField="Name" DataValueField="ID" 
                                        AppendDataBoundItems="true" />
                            </td>
                            <td>
                                <asp:TextBox ID="TaskTextBox_Insert" runat="server" Text='<%# Bind("Task") %>' />
                            </td>
                            <td>
                                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="Add" />
                            </td>
                        </tr>
                    </InsertItemTemplate>

                    <ItemTemplate>
                        <tr>
                            <td align="right" style="font-size:small; color:White;">
                                <asp:Label ID="PriorityLabel" runat="server" Text='<%# Eval("PriorityName") %>' style="padding:2px;" /> </td>
                            <td> <asp:Label ID="TaskLabel" runat="server" Text='<%# Eval("Task") %>' /> </td>
                            <td> <asp:Button ID="btnDone" runat="server" CommandName="Done" Text="Done" OnCommand="btnDone_OnCommand" CommandArgument='<%# Eval("ID") %>' /> </td>
                            <td> <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" /> </td>
                        </tr>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlPriority_Edit" runat="server" 
                                        DataSourceID="sdsPriorities" DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" 
                                        SelectedValue='<%# Bind("Priority") %>' /> </td>
                            <td> <asp:TextBox ID="TaskTextBox_Edit" runat="server" Text='<%# Bind("Task") %>' /> </td>
                            <td> <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" /> </td>
                            <td> <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" /> </td>
                            <td> <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this Task?');" /> </td>
                        </tr>
                    </EditItemTemplate>

                </asp:ListView>
            </div>
            
            <div>    
                <asp:ListView ID="lsvDone" runat="server" DataSourceID="sdsDone" DataKeyNames="ID" >

                    <LayoutTemplate>
                        <hr />
                        Completed Tasks:
                        <table>
                            <tr ID="itemPlaceholder" runat="server" />
                        </table>
                        <asp:Button ID="btnClear" runat="server" Text="Clear Completed" OnClick="btnClear_OnClick" />
                    </LayoutTemplate>

                    <ItemTemplate>
                        <tr>
                            <td> <asp:CheckBox runat="server" Checked="true" /> </td>
                            <td> <asp:Label ID="TaskLabel" runat="server" Text='<%# Eval("Task") %>' /> </td>
                            <td> <asp:Button ID="btnNotDone" runat="server" CommandName="NotDone" Text="Not Done" OnCommand="btnNotDone_OnCommand" CommandArgument='<%# Eval("ID") %>' /> </td>
                        </tr>
                    </ItemTemplate>

                </asp:ListView>
            </div>

                
            <asp:SqlDataSource ID="sdsPriorities" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
                SelectCommand="SELECT Name, ID FROM Priorities">
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="sdsTasks" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
                    SelectCommand="SELECT Tasks.ID, Tasks.Task, Tasks.Priority, Priorities.Name AS PriorityName 
                                    FROM [Tasks] 
                                    INNER JOIN Priorities ON Tasks.Priority = Priorities.ID 
                                    WHERE [UserName] = @UserName AND Done = 0 
                                    ORDER BY [Priority], [Task]" 
                    UpdateCommand="UPDATE [Tasks] SET [Task] = @Task, [Priority] = @Priority WHERE [ID] = @ID"
                    DeleteCommand="DELETE FROM [Tasks] WHERE [ID] = @ID" 
                    InsertCommand="INSERT INTO [Tasks] ([UserName], [Task], [Priority], [Done]) VALUES (@UserName, @Task, @Priority, 0)" 
                    oninserting="sdsTasks_Inserting" 
                    onselecting="sdsTasks_Selecting" >
                <SelectParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Task" Type="String" />
                    <asp:Parameter Name="Priority" Type="Int32" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="Task" Type="String" />
                    <asp:Parameter Name="Priority" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="sdsDone" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
                    SelectCommand="SELECT Tasks.ID, Tasks.Task
                                    FROM Tasks 
                                    WHERE UserName = @UserName AND Done = 1 
                                    ORDER BY Task" 
                    UpdateCommand="UPDATE Tasks SET Done = 0 WHERE ID = @ID"
                    DeleteCommand="DELETE FROM [Tasks] WHERE [ID] = @ID" 
                    onselecting="sdsDone_Selecting" >
                <SelectParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

            <hr />

            <a href="Default.aspx">Full Version</a> <br />
			<a href="MiniNotes.aspx">Handheld Notes</a> <br />
            <a href="MiniActivity.aspx">Handheld Activities</a> <br />
            <a href="MiniGoal.aspx">Handheld Goals</a> <br />

        </form>
    </body>
</html>
