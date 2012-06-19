<%@ Page Language="C#" MasterPageFile="~/KidSteps.master" Debug="true" AutoEventWireup="true" Inherits="MyPage" %>
<%@ MasterType TypeName="BaseMasterWebsite" %>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="FullPage" />

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="LeftColumn" />

<asp:Content runat="server" ID="Content0" ContentPlaceHolderID="Canvas">
    <div style="background-image:url('/images/design/login_bg.png'); background-repeat:repeat-x; margin:0px -15px; padding:15px 15px">
        <h2>Promoting Conservatives, Every Hour of Every Day!</h2>
        <h3>Login In</h3>
        <div style="z-index:20"><moshe:LoginPanel runat="server" ID="LoginPanel1" GoTo="/" /></div>
        <div style="z-index:10; margin-top:-20px"><img src="/images/design/UnitedStates.png" alt="" style="width:500px" /></div>
        <div style="padding-top:20px; z-index:15">
            <h4>Running for office and want Conservative grassroots to know about it?</h4>
            <a href="/campaigns/new.aspx">Add your <b>campaign</b> to our list of candidates!</a>
        </div>
    </div>
</asp:Content>

<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="RightColumn">
    <div class="box" style="margin-left:-6px;">
        <h2>Sign Up</h2>
        <moshe:ProfileEditor runat="server" ID="ProfileEditor1" CreateNewProfile="true" NextPage="/groups/get-involved.aspx" ValidationWidth="200" />
    </div>
</asp:Content>

<script runat="server">
    void Page_Init(object sender, EventArgs e)
    {
        ProfileEditor1.ActiveProfile = user;
        if (!IsPostBack)
        {
            if (MosheTechnologies.IsNumeric(Request.QueryString["network"], true))
            {
                ProfileEditor1.WebsiteID = Request.QueryString["network"];
                ProfileEditor1.NextPage += "?id=" + Request.QueryString["network"] + "&goto=help.aspx";
            }
            else
                ProfileEditor1.ShowNetworkOptions = false;

            Page.Title = "Welcome to MosheNET";

            MyContent page = new MyContent(true, "2");

            HtmlMeta metaTag = new HtmlMeta();
            metaTag.Name = "keywords";
            metaTag.Content = page.Keywords;
            Header.Controls.Add(metaTag);

            metaTag = new HtmlMeta();
            metaTag.Name = "description";
            metaTag.Content = page.Description;
            Header.Controls.Add(metaTag);

            metaTag = new HtmlMeta();
            metaTag.Name = "pageID";
            metaTag.Content = page.ID;
            Header.Controls.Add(metaTag);

            metaTag = new HtmlMeta();
            metaTag.Name = "pageInfo";
            metaTag.Content = Master.ID + "~" + ForeignKeyValue + "," + page.WebsiteID + "," + page.Path.TrimStart('/').Replace(".aspx", "").Replace("/", ",");
            Header.Controls.Add(metaTag);
        }
    }

    void Page_Load(object sender, EventArgs e)
    { }
</script>