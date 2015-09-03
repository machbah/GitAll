<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FactoryHome.aspx.cs" Inherits="FactoryPattern.FactoryHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Factory Patter Test</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="FactoryTypeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FactoryTypeList_SelectedIndexChanged"></asp:DropDownList>           
    </div>
    <div>
         <asp:TextBox ID="CurrentProductItem" Height="100" Width="300" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
