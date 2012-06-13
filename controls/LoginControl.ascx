<%@ Control Language="C#" Inherits="BaseControl" ClassName="LoginControl" %>

<script type="text/javascript">
    function Authenticate() {
        __doPostBack('<%=LinkButton1.UniqueID %>', '');
    }
</script>
    
    <asp:Panel runat="server" ID="LoginPanel" DefaultButton="LoginButton">
        <%--<asp:UpdatePanel runat="server" ID="LoginUpdatePanel" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <b>Account ID (email)</b>
                <div><asp:TextBox runat="server" ValidationGroup="LoginPanel" ID="UserName" Width="200" /></div>
                <div style="margin-top:4px"><b>Password</b></div>
                <div><asp:TextBox runat="server" ValidationGroup="LoginPanel" ID="Password" TextMode="Password" Width="120" /></div>
                <div style="margin-top:4px"><asp:Button runat="server" ValidationGroup="LoginPanel" ID="LoginButton" OnClick="LoginButton_Click" Text="Login" Width="50" Font-Size="8pt" /></div>
                <div><asp:CheckBox runat="server" ID="RememberMe" Text="Keep me logged in" Checked="true" style="font-size:8pt; color:#000066;" /></div>
                <div style="margin-top:4px"><a href="javascript:void(0);" style="color:#000066; font-size:8pt; font-style:italic" onclick="ToggleView('password-panel');">Forgot your password?</a></div>
                <div style="margin-top:8px; color:Red;"><asp:Literal runat="server" ID="ErrorMessage" /></div>
                <%--<asp:UpdateProgress runat="server" ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate><img src="/images/animations/loading.gif" style="vertical-align: middle" alt="Processing" /></ProgressTemplate>
                </asp:UpdateProgress>
            </contenttemplate>
        </asp:UpdatePanel>--%>

        <asp:Panel runat="server" ID="FacebookPanel" style="margin-top:15px" Visible="false">
            <fb:login-button onlogin="Authenticate()">login via facebook</fb:login-button>
            <div style="height:25px;"><asp:LinkButton runat="server" ID="LinkButton1" OnClick="FB_Click" style="display:none;" /></div>
        </asp:Panel>
    </asp:Panel>
    <div id="password-panel" style="margin-bottom:30px; display:none;">
        <div class="line-1">Password Retrieval</div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GetPassword" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="processBackgroundFilter"></div>
                        <div class="processContainer"><img src="/images/animations/loading.gif" style="vertical-align: middle" alt="Processing" /> validating account request...</div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                Account ID: 
                <div><asp:TextBox runat="server" ID="Email" Width="220px" Text="email" ForeColor="#333333" onfocus="ClearIf(this, 'email');" /></div>
                <div><asp:Button runat="server" ID="GetPassword" Text="Submit" OnClick="RetrievePassword" Height="24px" Font-Size="8pt" /></div>
                <div style="margin-left:80px"><asp:Literal runat="server" ID="GetPasswordResponse" /></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField runat="server" ID="GoToValue" />
    <asp:HiddenField runat="server" ID="WebsiteIDValue" />
<script runat="server">
    string style = "";

    public string WebsiteID
    {
        set { WebsiteIDValue.Value = value; }
    }
    
    public string Style
    {
        get { return LoginPanel.Style.Value; }
        set { LoginPanel.Style.Value = value; }
    }
    public string GoTo
    {
        get { return GoToValue.Value; }
        set { GoToValue.Value = value; }
    }
    public bool FacebookConnect
    {
        get { return FacebookPanel.Visible; }
        set { FacebookPanel.Visible = value; }
    }

    void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (MosheTechnologies.IsWebsite)
                FacebookPanel.Visible = !string.IsNullOrWhiteSpace(MyDatabase.ExecuteScalar("SELECT FB_AppID FROM mosheDOMAINS WHERE domain=" + SiteSecurity.SterilizeValue(MosheTechnologies.ActiveDomain)));
        }
    }
    
    void LoginButton_Click(object sender, EventArgs e)
    {
        if (SiteSecurity.UserLogin(UserName.Text, Password.Text, RememberMe.Checked))
        {            
            if (!string.IsNullOrEmpty(GoToValue.Value))
                Response.Redirect(GoToValue.Value, true);

            if (Session["NextPage"] != null)
                Response.Redirect((string)Session["NextPage"], true);

            if (Request.Path.Contains("login.aspx"))
                Response.Redirect("/", true);

            if (MosheTechnologies.IsWebsite)
                Response.Redirect(Request.Path, true);
            else
                Response.Redirect(Request.RawUrl.Replace("action=logout", "login=accepted"), true);
        }
        else
            ErrorMessage.Text = "Invalid Account or Password";
    }

    void RetrievePassword(object sender, EventArgs e)
    {
        if (MosheTechnologies.IsEmail(Email.Text))
        {
            MosheTechnologies.RecordAction("Password Request (" + Email.Text + ")");
            MyEmail email = new MyEmail(Session["USERID"].ToString());

            email.IsHTML = true;
            email.IncludeUnsubscribeLink = false;
            email.FromEmail = "notifications@moshenet.com";
            email.FromName = MosheTechnologies.SiteDomain + " Support";

            email.Subject = "RE: What is my password?";
            
            Query = "SELECT U.UserName, U.Password, U.FirstName FROM mosheUSERS U INNER JOIN userLOCATIONS UL ON U.ID=UL.User_ID WHERE (U.AccountID=" + SiteSecurity.SterilizeValue(Email.Text) + " OR UL.Email=" + SiteSecurity.SterilizeValue(Email.Text) + ")";
            System.Data.DataRow userCredentials = MyDatabase.LoadRecord(Query);
            if (userCredentials != null)
            {
                string custom_message = userCredentials["FirstName"].ToString() + ",";
                custom_message += "<p>Your network username is: <b>" + userCredentials["UserName"].ToString() + "</b>";
                custom_message += "<br />&amp; your password is: <b>" + SiteSecurity.RetrievePassword(userCredentials["Password"].ToString()) + "</b>";
                custom_message += @"<p><a href='http://" + MosheTechnologies.SiteDomain + "/'>click here to login</a></p>";
                custom_message += @"<p><a href='http://" + MosheTechnologies.SiteDomain + @"/profiles/admin/info.aspx'>to update your profile, click here</a></p>                
                                        <p>If you have any more questions, please feel welcome to contact us.</p>
                                        <p>Thank you,<br />" + MosheTechnologies.SiteDomain + " Support Team</p>";
                email.AddRecipient(Email.Text);
                email.Message = custom_message;
            }
            else
            {
                //email.AddRecipient(Email.Text);
                //email.AddRecipientBC("support@moshenet.com", "Technical Support");
                //email.Message = "<p>An invalid account id (" + Email.Text + ") has requested their network password.</p><p>Timestamp: <b>" + DateTime.Now.ToString() + "</b><br />IP Address: <b>" + MosheTechnologies.UserIP + "</b></p><p>If you cannot remember the email you registered with or this reminder was not triggered by you, please follow up directly with technical support. support@moshenet.com</p><p>Thank you,<br />MosheNET Support</p>";
                GetPasswordResponse.Text = "Unknown email. <a href='http://" + MosheTechnologies.SiteDomain + "/profiles/new.aspx'>click to join the network.</a>";
                return;
            }

            if (email.Send(IntegratedEmail.MessageType.SystemMessage))
            {
                GetPassword.Text = "Success!";

                GetPasswordResponse.Text = "Password Sent! " + DateTime.Now.ToString();
            }
            else
                GetPassword.Text = "Error!";

            GetPassword.Enabled = false;
        }
        else
        {
            GetPasswordResponse.Text = "<b style='color:Red;'>Invalid email address.</b>";
            return;
        }
    }

    void FB_Click(object sender, EventArgs e)
    {
        string app_id = "411518715528778";
        string my_url = "http://moshenet.com/facebook_authentication.aspx";

        if (MosheTechnologies.IsWebsite)
        {
            string query = "SELECT D.FB_AppID FROM mosheDOMAINS D WHERE D.domain=" + SiteSecurity.SterilizeValue(MosheTechnologies.ActiveDomain);
            string fb = MyDatabase.ExecuteScalar(query);
            if (!string.IsNullOrWhiteSpace(fb))
            {
                app_id = fb;
                my_url = "http://" + MosheTechnologies.ActiveDomain + "/facebook_authentication.aspx";
            }
        }

        string code = Request["code"];

        if (string.IsNullOrEmpty(code))
        {
            string state = SiteSecurity.ShtarkEncryption((new Random().Next()).ToString() + DateTime.Now.Ticks.ToString()).Replace(".", ""); //CSRF protection
            Session["state"] = state;

            string dialog_url = "http://www.facebook.com/dialog/oauth?client_id=" + app_id + "&redirect_uri=" + HttpUtility.UrlEncode(my_url) + "&scope=email,user_birthday,user_location,user_religion_politics,read_friendlists,friends_groups,friends_location,read_friendlists,friends_religion_politics,friends_groups,friends_location,friends_birthday&state=" + state;
            Response.Redirect(dialog_url, true);
        }
        else
            Response.Redirect(my_url + "?code=" + code, true);
    }
</script>