<%@ Page Language="C#" MasterPageFile="~/KidSteps.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="MyPage" %>
<%@ MasterType TypeName="BaseMasterWebsite" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Canvas" runat="Server">
    <h2>
        Add your child
    </h2>
    <div>
        <asp:TextBox runat="server" ID="FirstName" Width="220px" Text="first name" ForeColor="#333333" onfocus="ClearIf(this, 'first name');" />
    </div>
    <div>
        <asp:TextBox runat="server" ID="LastName" Width="220px" Text="last name" ForeColor="#333333" onfocus="ClearIf(this, 'last name');" />
    </div>
    <div>
        <asp:TextBox runat="server" ID="Email" Width="220px" Text="email" ForeColor="#333333" onfocus="ClearIf(this, 'email');" />
    </div>
    <div>
        <asp:Button runat="server" ID="AddFamilyMemberButton" OnClick="AddFamilyMemberButton_Click" Text="Submit" Width="50" Font-Size="8pt" />
    </div>
</asp:Content>

<script runat="server">
    void Page_Init(object sender, EventArgs e)
    {
    }

    void Page_Load(object sender, EventArgs e)
    {
    }

    void AddFamilyMemberButton_Click(object sender, EventArgs e)
    {
        int new_id = MoshesNetwork.CreateNetwork(
            user.ID, 
            user.StateID, 
            MoshesNetwork.NetworkType.Websites, 
            FirstName.Text + " " + LastName.Text, 
            string.Empty, 
            Email.Text);

        Response.Redirect(Request.Path, true);
    }

</script>

<%--@model KidSteps.ViewModels.AddFamilyMemberViewModel
@{
    ViewBag.Title = "Add Family Member";
}
<h2>
    @Model.PageHeader</h2>
@using (Html.BeginForm())
{
    @Html.HiddenFor(m => m.IsKid)
    
    @Html.EditorFor(m => m.Name)

    <div class="editor-label">
        @Html.LabelFor(m => m.Email)
    </div>
    <div class="editor-field">
        @Html.EditorFor(m => m.Email)
    </div>

    <p>
        <input type="submit" value="Save" />
    </p>
}--%>