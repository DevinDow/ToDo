<%@ Page Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DevinDow.ToDo.Login" Title="ToDo - Login" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="cphHead" runat="server">

    <style type="text/css">
        .login
        {
            border-style: solid;
            border-width: 1px;
            border-color: Black;
            margin: 1em;
            background-color: #F0F0F0; 
            padding: 1em;
        }
    </style>

</asp:Content>


<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">   
    
    <div class="login">
        <asp:Login ID="Login1" runat="server" RememberMeSet="True" 
            TitleText="Please Log In" onloggingin="Login1_LoggingIn">
            <TextBoxStyle/>
        </asp:Login>
    </div>

</asp:Content>