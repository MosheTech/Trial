<%@ Page Language="C#" MasterPageFile="~/KidSteps.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="MyPage" %>
<%@ MasterType TypeName="BaseMasterWebsite" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Canvas" runat="Server">
    <h3>Login In</h3>
    <div style="z-index:20"><moshe:LoginPanel runat="server" ID="LoginPanel1" GoTo="/" /></div>

    <h2>Sign Up</h2>
        <moshe:ProfileEditor runat="server" ID="ProfileEditor1" CreateNewProfile="true" NextPage="/groups/get-involved.aspx" ValidationWidth="200" />
</asp:Content>

<script runat="server">
    void Page_Init(object sender, EventArgs e)
    {
    }

    void Page_Load(object sender, EventArgs e)
    {
    }


</script>