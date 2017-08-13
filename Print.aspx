<%@ Page Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="DevinDow.ToDo.Print" Title="ToDo" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">   
 
  
	<!-- Tasks -->
	<div style="float:left; width:600px; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
		
		<asp:Repeater ID="rptTasks" runat="server" DataSourceID="sdsTasks" OnItemDataBound="rptTasks_ItemDataBound">
			<HeaderTemplate>
				<center><b>Tasks</b></center> 
				<hr />
				<table cellpadding="3">
			</HeaderTemplate>
			
			<ItemTemplate>
                <tr>
                    <td align="right" style="font-size:small; color:White; width:80px; ">
                        <asp:Label ID="TimeFrameLabel" runat="server" Text='<%# Eval("TimeFrameName") %>' style="padding:2px;" /> </td>
                    <td> <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' /> </td>
                </tr>
			</ItemTemplate>
			
			<FooterTemplate>
				</table> 
			</FooterTemplate>
		</asp:Repeater>

		<asp:Repeater ID="rptDone" runat="server" DataSourceID="sdsDone" >
			<HeaderTemplate>
				<hr />
				Completed Tasks : 
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-bottom:5px">
					<asp:CheckBox ID="chkDone" runat="server" Checked='<%# Eval("Done") %>' Enabled="false" />
					<asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("Name") %>' />
					<asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>' />
				</div>
			</ItemTemplate>
			
		</asp:Repeater>
	 
		
		<asp:SqlDataSource ID="sdsTimeFrame" runat="server"  
			ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
			SelectCommand="SELECT Name, ID FROM TimeFrames" />

        <asp:SqlDataSource ID="sdsTasks" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
                SelectCommand="SELECT Tasks.ID, Tasks.Name, Tasks.TimeFrame, TimeFrames.Name AS TimeFrameName 
                                FROM Tasks
                                INNER JOIN TimeFrames ON Tasks.TimeFrame = TimeFrames.ID 
                                WHERE UserName = @UserName AND Done = 0 
                                ORDER BY TimeFrame, Name" 
				OnSelecting="sdsTasks_Selecting" >
			<SelectParameters>
				<asp:Parameter Name="UserName" Type="String" />
			</SelectParameters>
		</asp:SqlDataSource>

		<asp:SqlDataSource ID="sdsDone" runat="server" 
				ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT Tasks.ID, Tasks.Name, Tasks.Done 
								FROM Tasks 
								WHERE UserName = @UserName AND Done = 1 
								ORDER BY Name" 
				OnSelecting="sdsDone_Selecting" >
			<SelectParameters>
				<asp:Parameter Name="UserName" Type="String" />
			</SelectParameters>
		</asp:SqlDataSource>
		
	</div>
 
 
 	<!-- Activities -->
	<div style="float:left; width:600px; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
		
		<asp:Repeater ID="rptActivities" runat="server" DataSourceID="sdsActivities">
			<HeaderTemplate>
				<center><b>Activities</b></center>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlActivity" runat="server">
						<asp:Label ID="lblActivityPriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" />
						<asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("Name") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>


		<asp:SqlDataSource ID="sdsActivities" runat="server" ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ID, Name, Priority
								FROM Activities 
								WHERE UserName = @UserName 
								ORDER BY Priority, Name" 
				OnSelecting="sdsActivities_Selecting" >
			<SelectParameters>
				<asp:Parameter Name="UserName" Type="String" />
			</SelectParameters>
		</asp:SqlDataSource>
		
	</div>
		
		
	<!-- Priorities -->
	<div style="float:left; width:600px; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
	   
		<asp:Repeater ID="rptPriorities" runat="server" DataSourceID="sdsPriorities">
			<HeaderTemplate>
				<center><b>Priorities</b></center>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlPriority" runat="server">
						<asp:Label ID="lblPriorityPriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" />
						<asp:Label ID="lblPriorityName" runat="server" Text='<%# Eval("Name") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>


		<asp:SqlDataSource ID="sdsPriorities" runat="server" ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ID, Name, Priority
								FROM Priorities 
								WHERE UserName = @UserName 
								ORDER BY Priority, Name" 
				OnSelecting="sdsPriorities_Selecting" >
			<SelectParameters>
				<asp:Parameter Name="UserName" Type="String" />
			</SelectParameters>
		</asp:SqlDataSource>
	</div>
	
	
	<!-- Goals -->
	<div style="float:left; width:600px; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
	   
		<asp:Repeater ID="rptGoals" runat="server" DataSourceID="sdsGoals">
			<HeaderTemplate>
				<center><b>Goals</b></center>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlGoal" runat="server">
						<asp:Label ID="lblGoalPriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" />
						<asp:Label ID="lblGoalName" runat="server" Text='<%# Eval("Name") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>


		<asp:SqlDataSource ID="sdsGoals" runat="server" ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ID, Name, Priority
								FROM Goals 
								WHERE UserName = @UserName 
								ORDER BY Priority, Name" 
				OnSelecting="sdsGoals_Selecting" >
			<SelectParameters>
				<asp:Parameter Name="UserName" Type="String" />
			</SelectParameters>
		</asp:SqlDataSource>
	</div>
		
		
	<!-- Desires -->
	<div style="float:left; width:600px; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
	   
		<asp:Repeater ID="rptDesires" runat="server" DataSourceID="sdsDesires">
			<HeaderTemplate>
				<center><b>Desires</b></center>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlDesire" runat="server">
						<asp:Label ID="lblDesirePriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" />
						<asp:Label ID="lblDesireName" runat="server" Text='<%# Eval("Name") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>


		<asp:SqlDataSource ID="sdsDesires" runat="server" ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ID, Name, Priority
								FROM Desires 
								WHERE UserName = @UserName 
								ORDER BY Priority, Name" 
				OnSelecting="sdsDesires_Selecting" >
			<SelectParameters>
				<asp:Parameter Name="UserName" Type="String" />
			</SelectParameters>
		</asp:SqlDataSource>
		
	</div>
	
	
	<!-- Notes -->
	<div style="float:left; width:600px; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">

		<b style="vertical-align:top">Notes</b>
		<hr />
		
		<asp:Label ID="txtNotes" runat="server" Width="300" Rows="5" TextMode="MultiLine" Font-Names="Verdana" />

	</div>


	
</asp:Content>
