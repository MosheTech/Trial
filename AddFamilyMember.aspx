<%@ Page Language="C#" MasterPageFile="~/KidSteps.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="MyPage" %>
<%@ MasterType TypeName="BaseMasterWebsite" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Canvas" runat="Server">
    <div>
        <asp:TextBox runat="server" ID="FirstName" Width="220px" Text="first name" ForeColor="#333333" onfocus="ClearIf(this, 'first name');" />
    </div>
</asp:Content>

<script runat="server">
    void Page_Init(object sender, EventArgs e)
    {
    }

    void Page_Load(object sender, EventArgs e)
    {
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