<%@ Page Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DevinDow.ToDo.Default" Title="ToDo" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">   
 
  
	<!-- Tasks -->
	<div style="float:left; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
		
		<asp:Repeater ID="rptTasks" runat="server" DataSourceID="sdsTasks">
			<HeaderTemplate>
				<center><b>Tasks</b></center> 
				<hr />
				<asp:Panel ID="pnlInsertTask" runat="server" DefaultButton="btnInsert">
					<asp:DropDownList ID="ddlTimeFrame_Insert" runat="server" DataSourceID="sdsTimeFrame" DataTextField="Name" DataValueField="ID" />
					<asp:TextBox ID="txtTaskText_Insert" runat="server" />
					<asp:Button ID="btnInsert" runat="server" Text="+" OnClick="btnInsertTask_OnClick" />
				</asp:Panel>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlTask" runat="server" DefaultButton="btnUpdate">
						<asp:DropDownList ID="ddlTimeFrame" runat="server" DataSourceID="sdsTimeFrame" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Eval("Priority") %>' AutoPostBack="true" OnSelectedIndexChanged="ddlTimeFrame_OnSelectedIndexChanged" OnDataBound="ddlTimeFrame_OnDataBound" />
						<asp:TextBox ID="txtTaskText" runat="server" Text='<%# Eval("Text") %>' Width="225" />
						<asp:Button ID="btnUpdate" runat="server" Text="+" OnClick="btnUpdateTask_OnClick" />
						<asp:Button ID="btnDelete" runat="server" Text="-" OnClick="btnDeleteTask_OnClick" OnClientClick='<%# Eval("Text", "return confirm(\"Delete &#39;{0}&#39; task?\");") %>' />
						<asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>

		<asp:SqlDataSource ID="sdsTimeFrame" runat="server"  
			ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
			SelectCommand="SELECT Name, ID FROM TimeFrames" />

		<asp:SqlDataSource ID="sdsTasks" runat="server" 
				ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ListItems.ID, ListItems.Text, ListItems.Priority
								FROM ListItems 
								INNER JOIN TimeFrames ON ListItems.Priority = TimeFrames.ID 
								WHERE ListItems.UserID=1 AND ListItems.ListID=2 
								ORDER BY Priority, Name" />
		
	</div>
 
 
 	<!-- Activities -->
	<div style="float:left; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
		
		<asp:Repeater ID="rptActivities" runat="server" DataSourceID="sdsActivities">
			<HeaderTemplate>
				<center><b>Activities</b></center>
				<hr />
				<asp:Panel ID="pnlInsertActivity" runat="server" DefaultButton="btnInsertActivity">
					<asp:TextBox ID="txtActivityPriority_Insert" runat="server" Width="25" />
					<asp:TextBox ID="txtActivityText_Insert" runat="server" />
					<asp:Button ID="btnInsertActivity" runat="server" Text="+" OnClick="btnInsertActivity_OnClick" />
				</asp:Panel>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlActivity" runat="server" DefaultButton="btnUpdate">
						<asp:TextBox ID="txtActivityPriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" />
						<asp:TextBox ID="txtActivityText" runat="server" Text='<%# Eval("Text") %>' Width="225" />
						<asp:Button ID="btnUpdate" runat="server" Text="+" OnClick="btnUpdateActivity_OnClick" />
						<asp:Button ID="btnDelete" runat="server" Text="-" OnClick="btnDeleteActivity_OnClick" OnClientClick='<%# Eval("Text", "return confirm(\"Delete &#39;{0}&#39; activity?\");") %>' />
						<asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>


		<asp:SqlDataSource ID="sdsActivities" runat="server" ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ID, Text, Priority
								FROM ListItems 
								WHERE UserID=1 AND ListID=3
								ORDER BY Priority, Text" />
		
	</div>
		
		
	<!-- Priorities -->
	<div style="float:left; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
	   
		<asp:Repeater ID="rptPriorities" runat="server" DataSourceID="sdsPriorities">
			<HeaderTemplate>
				<center><b>Priorities</b></center>
				<hr />
				<asp:Panel ID="pnlInsertPriority" runat="server" DefaultButton="btnInsertPriority">
					<asp:TextBox ID="txtPriorityPriority_Insert" runat="server" Width="25" />
					<asp:TextBox ID="txtPriorityText_Insert" runat="server" />
					<asp:Button ID="btnInsertPriority" runat="server" Text="+" OnClick="btnInsertPriority_OnClick" />
				</asp:Panel>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlPriority" runat="server" DefaultButton="btnUpdate">
						<asp:TextBox ID="txtPriorityPriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" />
						<asp:TextBox ID="txtPriorityText" runat="server" Text='<%# Eval("Text") %>' Width="225" />
						<asp:Button ID="btnUpdate" runat="server" Text="+" OnClick="btnUpdatePriority_OnClick" />
						<asp:Button ID="btnDelete" runat="server" Text="-" OnClick="btnDeletePriority_OnClick" OnClientClick='<%# Eval("Text", "return confirm(\"Delete &#39;{0}&#39; priority?\");") %>' />
						<asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>


		<asp:SqlDataSource ID="sdsPriorities" runat="server" ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ID, Text, Priority
								FROM ListItems 
								WHERE UserID=1 AND ListID=4
								ORDER BY Priority, Text" />
	</div>
	
	
	<!-- Goals -->
	<div style="float:left; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
	   
		<asp:Repeater ID="rptGoals" runat="server" DataSourceID="sdsGoals">
			<HeaderTemplate>
				<center><b>Goals</b></center>
				<hr />
				<asp:Panel ID="pnlInsertGoal" runat="server" DefaultButton="btnInsertGoal">
					<asp:TextBox ID="txtGoalPriority_Insert" runat="server" Width="25" />
					<asp:TextBox ID="txtGoalText_Insert" runat="server" />
					<asp:Button ID="btnInsertGoal" runat="server" Text="+" OnClick="btnInsertGoal_OnClick" />
				</asp:Panel>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlGoal" runat="server" DefaultButton="btnUpdate">
						<asp:TextBox ID="txtGoalPriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" />
						<asp:TextBox ID="txtGoalText" runat="server" Text='<%# Eval("Text") %>' Width="225" />
						<asp:Button ID="btnUpdate" runat="server" Text="+" OnClick="btnUpdateGoal_OnClick" />
						<asp:Button ID="btnDelete" runat="server" Text="-" OnClick="btnDeleteGoal_OnClick" OnClientClick='<%# Eval("Text", "return confirm(\"Delete &#39;{0}&#39; goal?\");") %>' />
						<asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>


		<asp:SqlDataSource ID="sdsGoals" runat="server" ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ID, Text, Priority
								FROM ListItems 
								WHERE UserID=1 AND ListID=5
								ORDER BY Priority, Text" />
	</div>
		
		
	<!-- Desires -->
	<div style="float:left; margin-right:24px; margin-bottom:12px; border:black 1px solid; padding:10px">
	   
		<asp:Repeater ID="rptDesires" runat="server" DataSourceID="sdsDesires">
			<HeaderTemplate>
				<center><b>Desires</b></center>
				<hr />
				<asp:Panel ID="pnlInsertDesire" runat="server" DefaultButton="btnInsertDesire">
					<asp:TextBox ID="txtDesirePriority_Insert" runat="server" Width="25" />
					<asp:TextBox ID="txtDesireText_Insert" runat="server" />
					<asp:Button ID="btnInsertDesire" runat="server" Text="+" OnClick="btnInsertDesire_OnClick" />
				</asp:Panel>
				<hr />
			</HeaderTemplate>
			
			<ItemTemplate>
				<div style="margin-top:5px">
					<asp:Panel ID="pnlDesire" runat="server" DefaultButton="btnUpdate">
						<asp:TextBox ID="txtDesirePriority" runat="server" Text='<%# Eval("Priority") %>' Width="25" />
						<asp:TextBox ID="txtDesireText" runat="server" Text='<%# Eval("Text") %>' Width="225" />
						<asp:Button ID="btnUpdate" runat="server" Text="+" OnClick="btnUpdateDesire_OnClick" />
						<asp:Button ID="btnDelete" runat="server" Text="-" OnClick="btnDeleteDesire_OnClick" OnClientClick='<%# Eval("Text", "return confirm(\"Delete &#39;{0}&#39; desire?\");") %>' />
						<asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>' />
					</asp:Panel>
				</div>
			</ItemTemplate>
		</asp:Repeater>


		<asp:SqlDataSource ID="sdsDesires" runat="server" ConnectionString="<%$ ConnectionStrings:DevinDowConnectionString %>" 
				SelectCommand="SELECT ID, Text, Priority
								FROM ListItems 
								WHERE UserID=1 AND ListID=6
								ORDER BY Priority, Text" />
		
	</div>
		
		
		
	<div style="clear:both">
		<hr />
		<!--
		<a href="Print.aspx">Print</a> <br />
		<a href="MiniTasks.aspx">Handheld Tasks</a> <br />
		<a href="MiniActivity.aspx">Handheld Activities</a> <br />
		<a href="MiniGoal.aspx">Handheld Goals</a> <br />
		<a href="MiniNotes.aspx">Handheld Notes</a> <br />
		-->
		<asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"	/>
	</div>
	
</asp:Content>
