<%@ Page Language="C#" MasterPageFile="~/KidSteps.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="MyPage" %>
<%@ MasterType TypeName="BaseMasterWebsite" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Canvas" runat="Server">
    <h2>
        Add family member
    </h2>
    <div>
        <asp:TextBox runat="server" ID="FirstName" Width="220px" Text="first name" ForeColor="#888888" onfocus="ClearIf(this, 'first name');" />
    </div>
    <div>
        <asp:TextBox runat="server" ID="LastName" Width="220px" Text="last name" ForeColor="#888888" onfocus="ClearIf(this, 'last name');" />
    </div>
    <div>
        <asp:TextBox runat="server" ID="Email" Width="220px" Text="email" ForeColor="#888888" onfocus="ClearIf(this, 'email');" />
    </div>
    <div>
        <asp:DropDownList runat="server" ID="Relationships" />
    </div>
    <div>
        <asp:Button runat="server" ID="AddFamilyMemberButton" OnClick="AddFamilyMemberButton_Click" Text="Submit" Width="50" Font-Size="8pt" />
    </div>
</asp:Content>

<script runat="server">
    void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Relationships.DataSource = RelationshipTypeExtensions.GetAllRelationshipTypes();
            Relationships.DataValueField = "Value";
            Relationships.DataTextField = "Text";
            Relationships.DataBind();
        }
    }

    void Page_Load(object sender, EventArgs e)
    {
    }

    void AddFamilyMemberButton_Click(object sender, EventArgs e)
    {
        int new_id = 
            MoshesNetwork.CreateNetwork(
                MoshesNetwork.NetworkType.Websites,
                user.ID, 
                user.StateID,
                FirstName.Text + " " + LastName.Text,
                string.Empty, 
                Email.Text);

        //MoshesNetwork.Join((string)Session["FAMILYID"], new_id.ToString());
        Query = "INSERT INTO userWEBSITES_Affiliates (Parent_Network_ID, Child_Network_ID, Association_ID) VALUES(" + Session["FAMILYID"].ToString() + "," + new_id.ToString() + "," + Relationships.SelectedValue + ");";
        MyDatabase.ExecuteCommand(Query);
        
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