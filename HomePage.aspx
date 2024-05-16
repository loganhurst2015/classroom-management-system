<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Classroom_Manager_System.HomePage" %>
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Abril+Fatface&family=Jura:wght@700&display=swap" rel="stylesheet">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #BtAddStudent{
            background-color: #34BA57
        }
        #BtAddStudent:hover{
            background-color: #8A95A5;
            cursor: pointer;
        }
        #BtSortAsc {
            background-color: #34BA57;

        }
        #BtSortAsc:hover{
            background-color: #8A95A5;
            cursor: pointer;
        }
        #BtSortDesc {
            background-color: #34BA57;
        }
        #BtSortDesc:hover{
            background-color: #8A95A5;
            cursor: pointer;
        }
        #DDLClass:hover{
           
            cursor: pointer;
        }
        .auto-style1 {
            margin-left: 4px;
        }

        .auto-style2 {
            width: 145px;
        }
        .auto-style4 {
            margin-left: 3px;
        }
        .auto-style5 {
            margin-left: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1 style="font-family: 'Abril Fatface', serif;
font-family: 'Jura', sans-serif; color: #34BA57; text-shadow:
    -1px -1px 0 #000,
    1px -1px 0 #000,
    -1px 1px 0 #000,
    1px 1px 0 #000; ">&nbsp&nbsp&nbsp&nbsp Classroom Manager</h1>
        <table>
            <tr>
                <td id="td1" class="auto-style2">Class Name:</td>
                <td id="td2">
                   
                    <asp:DropDownList ID="DDLClass" OnSelectedIndexChanged = "DropDownIndexChange_TextChanged" runat="server" AutoPostBack="True" AppendDataBoundItems="true" CssClass="auto-style1" Width="209px" DataSourceID="SqlDataSource1" DataTextField="CourseName" DataValueField="CourseName">
                        <asp:ListItem Value="0" Text="-- Select Class --" ></asp:ListItem>
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ClassroomManagerSystemConnectionString %>" ProviderName="<%$ ConnectionStrings:ClassroomManagerSystemConnectionString.ProviderName %>" SelectCommand="SELECT [CourseName] FROM [CourseTable]"></asp:SqlDataSource>
                </td>
            </tr>
            
            <tr height="5px" id="tr2">
                <td id="td3" class="auto-style2"></td>
                <td id="td4"></td>
            </tr>
            
            <tr>
                <td id="td5" class="auto-style2">Teacher:</td>
                <td class="Ctd1">
                    <asp:TextBox ID="TeacherTextBox" runat="server" Width="209px" OnTextChanged="TeacherTextBox_TextChanged" CssClass="auto-style4"></asp:TextBox></td>
            </tr>
            <tr id="tr4">
                <td class="auto-style2"></td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style2"></td>
                <td class="Ctd1">
                    <asp:TextBox ID="AddStudentTextBox" runat="server" Width="209px" CssClass="auto-style4"></asp:TextBox></td>
                <td class="Ctd1">
                    <asp:Button ID="BtAddStudent" runat="server" Text="Add Student" OnClick="BtAddStudent_Click" /></td>

            </tr>
            <tr id="tr6">
                <td class="auto-style2"></td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style2"></td>
                <td class="Ctd1">
                    <asp:TextBox ID="StudentListTextBox" runat="server" Name="tbMultiLine" TextWrapping="Wrap" AcceptsReturn="True" VerticalScrollBarVisibility="Visible" Width="209px" Height="236px" OnTextChanged="StudentListTextBox_TextChanged1" TextMode="MultiLine" CssClass="auto-style5"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="auto-style2"></td>
                <td>
                    <table>
                        <tr>
                            <td id="td17" class="Ctd1">
                                <asp:Button ID="BtSortAsc" runat="server" Text="Sort Asc" OnClick="BtSortAsc_Click" /></td>
                            <td id="td18" class="Ctd1">
                                <asp:Button ID="BtSortDesc" runat="server" Text="Sort Desc" OnClick="BtSortDesc_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
