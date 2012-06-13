<%@ Control Language="C#" Inherits="BaseControl" ClassName="ProfileEditor" %>
<%@ Register TagPrefix="moshe" TagName="ValidationButton" Src="~/controls/ValidationControl.ascx" %>

<asp:Panel runat="server" ID="ControlPanel" Width="210px">
    <div class="form-field">
        <div class="line-1">First Name:<span style="color:Red">*</span> <asp:RequiredFieldValidator runat="server" ValidationGroup="ProfileEditor" ID="RequiredFieldValidator1" ControlToValidate="FirstName" Text="required" Font-Bold="true" /></div>
        <div><asp:TextBox ID="FirstName" runat="server" ValidationGroup="ProfileEditor" Width="200px" /></div>
    </div>
            
    <div class="form-field">
        <div class="line-1">Last Name:<span style="color:Red">*</span> <asp:RequiredFieldValidator runat="server" ValidationGroup="ProfileEditor" ID="RequiredFieldValidator2" ControlToValidate="LastName" Text="required" Font-Bold="true" /></div>
        <div><asp:TextBox runat="server" ValidationGroup="ProfileEditor" ID="LastName" Width="200px" /></div>
    </div>
    


    <asp:Panel runat="server" ID="AvailableNetworksPanel" style="display:none;">
        <div class="form-field">
            <div class="line-1">Initial Network:<span style="color:Red">*</span> </div>
            <div><asp:DropDownList runat="server" ValidationGroup="ProfileEditor" ID="AvailableNetworks" Width="205px" /></div>
        </div>
        
        <div class="form-field">
            <div class="line-1">Member Title:</div>
            <div><asp:TextBox runat="server" ValidationGroup="ProfileEditor" ID="MemberTitle" Width="200px" Text="Member" /></div>
            <div class="line-3">What is your relationship to this group.</div>
        </div>
    </asp:Panel>
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DuplicateAccountLink" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="form-field">
                <div class="line-1">Account ID (Email):<span style="color:Red">*</span> <asp:RequiredFieldValidator runat="server" ValidationGroup="ProfileEditor" ID="RequiredFieldValidator4" ControlToValidate="AccountID" Text="required" Font-Bold="true" /></div>
                <div><asp:TextBox runat="server" ValidationGroup="ProfileEditor" ID="AccountID" Width="200px" AutoPostBack="true" OnTextChanged="ValidateAccountID" /></div>
                <asp:Literal runat="server" ID="EmailError" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="PasswordPanel">
        <div class="form-field">
            <div class="line-1">Password:<span style="color:Red">*</span> <asp:RequiredFieldValidator runat="server" ValidationGroup="ProfileEditor" ID="RequiredFieldValidator7" ControlToValidate="Password" Text="required" Font-Bold="true" /></div>
            <div><asp:TextBox runat="server" ValidationGroup="ProfileEditor" ID="Password" Width="200px" TextMode="Password" /></div>
        </div>
            
        <asp:Panel runat="server" ID="ConfirmPasswordPanel" CssClass="form-field">
            <div class="line-1">Confirm Password:<span style="color:Red">*</span> <asp:RequiredFieldValidator runat="server" ValidationGroup="ProfileEditor" ID="RequiredFieldValidator9" ControlToValidate="Password2" Text="required" Font-Bold="true" /></div>
            <div><asp:TextBox runat="server" ValidationGroup="ProfileEditor" ID="Password2" Width="200px" TextMode="Password" onkeyup="validatePassword();" /></div>
            <div id="passwordNote"></div>
            <div><asp:CompareValidator runat="server" ID="ComparePasswords" ControlToValidate="Password2" ControlToCompare="Password" ErrorMessage="seriously, they don't match! Try again" Font-Bold="true" EnableClientScript="true" Display="Dynamic" /></div>
        </asp:Panel>
    </asp:Panel>
        
    <script type="text/javascript">
        function validatePassword() {
            var pwd = document.getElementById('<%=Password.ClientID %>').value;
            var pwd2 = document.getElementById('<%=Password2.ClientID %>').value;
            if (pwd2.length >= pwd.length) {
                if (pwd != pwd2)
                    $("#passwordNote").html("<span style='color:Red;'>passwords do not match</span>");
                else
                    $("#passwordNote").html("<span style='color:Green; font-weight:bold'>password match!</span>");
            }
        }

        function validateType(type) {
            if (type == "Liberal") {
                if (confirm('You are not welcome here! Would you like to learn about principles of good government and change your evil ways?'))
                    location.href = 'http://preservingamerica.org/';
                else {
                    if (confirm('Do us all a favor, OK? Get a job, review your paycheck, look at your bills, and ask why you have to pay for people who don\'t work but can!'))
                        location.href = 'http://www.givemeliberty.org/features/taxes/19990709_xcdfr_is_income.htm';
                    else {
                        alert('Want to remain ignorant, eh? Too bad, check out the debt clock!');
                        location.href = 'http://www.usdebtclock.org/';
                    }
                }
            }
        }
    </script>
        
    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="margin-top:6px"><moshe:ValidationButton runat="server" ValidationGroup="ProfileEditor" ID="ValidationButton1" OnSubmit="SubmitButton_Click" /></div>
            <div><asp:Literal runat="server" ID="SubmitResponse" /></div>
            <asp:Panel runat="server" ID="NewAccountConfirmation" Visible="false">
                <asp:ImageButton runat="server" ID="CloseNewAccountConfirmation" ImageUrl="/images/X.png" style="float:right; z-index:3005; margin-top:-20px; margin-right:-20px; cursor:pointer;" OnClick="CloseNewAccountConfirmation_Click" />
                <div class="processContainer" style="top:10%">
                    <h3 class="title">THIS MAY BE A DUPLICATE ACCOUNT!</h3>
                    <div style="margin-top:2px">
                        <asp:Repeater runat="server" ID="DuplicateProfiles">
                            <ItemTemplate>
                                <div style="clear:left; padding-top:12px">
                                    <img src="<%#((System.Data.DataRowView)Container.DataItem)["Image"].ToString()%>" alt="<%#((System.Data.DataRowView)Container.DataItem)["Name"].ToString()%>" style="float:left; margin-right:8px; height:120px; width:120px" />
                                    <div><%#((System.Data.DataRowView)Container.DataItem)["Name"].ToString()%></div>
                                    <div><%#((System.Data.DataRowView)Container.DataItem)["StateName"].ToString()%></div>
                                    <div><%#((System.Data.DataRowView)Container.DataItem)["PostalCode"].ToString()%></div>
                                    <div style="margin-top:6px;">
                                        Good find! Actually<br />this is my account.                            
                                        <div style="margin-top:4px">
                                            <asp:Button runat="server" ID="ConfirmAccountLink" Text="Email me my password" OnCommand="ConfirmThisAccount" CommandName="Confirm" CommandArgument='<%#((System.Data.DataRowView)Container.DataItem)["AccountID"].ToString()%>' />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <hr style="clear:left" />
                        <div style="font-weight:bold;">Nope. This is a new account, I just typed the wrong email address.</div>
                        <div>Please enter a new email address:</div>
                        <div><asp:TextBox runat="server" ID="Email2" /> <asp:Button runat="server" ID="DuplicateAccountLink" Text="Submit" OnCommand="ConfirmThisAccount" CommandName="Create" CommandArgument="0" /></div>
                    </div>
                </div>
                <div class="processBackgroundFilter"></div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
<asp:HiddenField runat="server" ID="WebsiteIDValue" />
<asp:HiddenField runat="server" ID="ProfileIDValue" />
<asp:HiddenField runat="server" ID="NextPageLoc" />

<script runat="server">    
    MyUser profile;
    public MyUser ActiveProfile
    {
        get
        {
            if (profile == null)
            {
                Control X = this.Parent;
                while (!X.GetType().ToString().EndsWith("aspx"))
                { X = X.Parent; }
                profile = ((MyPage)X).user;
            }

            return profile;
        }
        set { profile = value; }
    }
    
    public string ProfileID
    {
        get { return MosheTechnologies.IsNumeric(ProfileIDValue.Value, true) ? ProfileIDValue.Value : ActiveProfile.ID; }
        set { ProfileIDValue.Value = value; }
    }

    public string CorrectAnswer
    { set { ValidationButton1.CorrectAnswer = value; } }
    public string AnswerKey
    { set { ValidationButton1.AnswerKey = value.Split('|'); } }
    
    public string WebsiteID
    {
        get { return WebsiteIDValue.Value; }
        set { WebsiteIDValue.Value = value; }
    }

    public bool Enabled
    {
        get { return ControlPanel.Enabled; }
        set { ControlPanel.Enabled = value; }
    }

    public string Text
    {
        get { return ValidationButton1.ButtonText; }
        set { ValidationButton1.ButtonText = value; }
    }

    public string NextPage
    {
        get { return string.IsNullOrEmpty(NextPageLoc.Value) ? "/profiles/admin/" : NextPageLoc.Value; }
        set { NextPageLoc.Value = value; }
    }

    bool create_new_profile = false;
    public bool CreateNewProfile
    {
        get { return create_new_profile; }
        set { create_new_profile = value; }
    }

    public int ValidationWidth
    { set { ValidationButton1.Width = value; } }

    public bool ShowNetworkOptions
    {
        get { return AvailableNetworksPanel.Visible; }
        set { AvailableNetworksPanel.Visible = value; }
    }

    string Query = "";
    void Page_Init(object sender, EventArgs e)
    {
    }

    void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            if (ShowNetworkOptions)
            {
                if (MosheTechnologies.IsNumeric(WebsiteID, true))
                {
                    Query = "SELECT W.ID, 'Welcome to ' + W.Name As Name FROM userWEBSITES W WHERE W.ToBeDeleted=0 AND W.IsActive=1 AND W.ID=" + WebsiteID + " ORDER BY W.Name";
                    AvailableNetworks.Enabled = false;
                }
                else
                    Query = "SELECT W.ID, W.Name FROM userWEBSITES W WHERE W.ID=1 OR (W.ToBeDeleted=0 AND W.IsActive=1) ORDER BY W.Name";
                
                AvailableNetworks.DataSource = MyDatabase.LoadRecords(Query);
                AvailableNetworks.DataValueField = "ID";
                AvailableNetworks.DataTextField = "Name";
                AvailableNetworks.DataBind();
            }
            
            if (profile.IsLoggedIn && (!CreateNewProfile || !profile.IsSystemAdmin))
            {
                FirstName.Text = profile.FirstName;
                LastName.Text = profile.LastName;

                AvailableNetworksPanel.Visible = false;

                AccountID.Text = profile.Account;
                AccountID.Enabled = false;

                PasswordPanel.Visible = false;
                
                //Deleted.Checked = (bool)websiteInfo["ToBeDeleted"];

                ValidationButton1.ButtonText = "Update Account";
            }
            else
            {
                AccountID.Text = Request.QueryString["email"];
                ValidationButton1.ButtonText = "Create Account";
            }   
        }
    }

    void SubmitButton_Click(object sender, EventArgs e)
    {
        if (!MosheTechnologies.IsEmail(AccountID.Text))
        {
            EmailError.Text = "<div style='color:Red; font-weight:bold;'>Invalid email address</div>";
            AccountID.Focus();
            return;
        }
        
        string user_id = profile.ID, account = profile.Account; //Current ID and email needs to be captured BEFORE the login test
        bool possible_duplicate = false;
        if (profile.IsLoggedIn && (!CreateNewProfile || !profile.IsSystemAdmin))
        {
            UpdateProfileInfo();

            return;
        }

        AccountID.Text = AccountID.Text.Trim();

        if (FirstName.Text.Equals(LastName.Text) || !MosheTechnologies.IsEmail(AccountID.Text))
        {
            ValidationButton1.Visible = false;

            return;
        }

        Query = "SELECT COUNT(*) FROM mosheUSERS U INNER JOIN userLOCATIONS UL ON U.ID=UL.User_ID WHERE U.AccountID=" + SiteSecurity.SterilizeValue(AccountID.Text) + " OR UL.Email=" + SiteSecurity.SterilizeValue(AccountID.Text, true);
        possible_duplicate = MosheTechnologies.IsNumeric(MyDatabase.ExecuteScalar(Query), true);

        if (possible_duplicate)
        {
            Password.Attributes.Add("value", Password.Text);

            Query = @"SELECT DISTINCT U.ID, U.FirstName+' '+U.LastName As Name, U.AccountID, U.PostalCode, S.Name As StateName, U.Account_Status, U.Image
                    FROM mosheUSERS U INNER JOIN mosheSTATES S ON U.State_ID=S.ID INNER JOIN userLOCATIONS UL ON U.ID=UL.User_ID
                    WHERE U.AccountID=" + SiteSecurity.SterilizeValue(AccountID.Text) + " OR UL.Email=" + SiteSecurity.SterilizeValue(AccountID.Text);
            System.Data.DataTable duplicate_profiles = MyDatabase.LoadRecords(Query);
            DuplicateProfiles.DataSource = duplicate_profiles;
            DuplicateProfiles.DataBind();

//            NewAccountConfirmation.Visible = true;
            
//            string msg = "<ol>";
//            msg += "<li><a href=\"http://" + MosheTechnologies.DefaultDomain + "/admin/users.aspx?id=" + profile.ID + "\">" + FirstName.Text + " " + LastName.Text + " [" + AccountID.Text + "] - NEW!</a></li>";
//            foreach (System.Data.DataRow dupe in duplicate_profiles.Rows)
//            {
//                msg += "<li><a href=\"http://" + MosheTechnologies.DefaultDomain + "/admin/users.aspx?id=" + dupe["ID"].ToString() + "\">" + dupe["Name"].ToString() + " [" + dupe["AccountID"].ToString() + "] " + (dupe["Account_Status"].ToString().Equals("0") ? " - deleted" : "") + "</a></li>";
//            }
//            msg += @"</ol>
//                    The automated duplicate record review system suspects that a duplicate account may have possibly been created.";

//            IntegratedEmail.SendAlert("Possible Duplicate Entry", msg);
        }
        else
            CreateAccount();
    }

    void UpdateProfileInfo()
    {
        Query = @"UPDATE mosheUSERS SET LastUpdated=GETDATE()
                    , FirstName=" + SiteSecurity.SterilizeValue(FirstName.Text) + @"
                    , LastName=" + SiteSecurity.SterilizeValue(LastName.Text) + @"
                WHERE ID=" + profile.ID;
        if (MosheTechnologies.IsNumeric(profile.ID, true))
        {
            MyDatabase.ExecuteCommand(Query);
            SubmitResponse.Text = "Your profile has been updated! " + DateTime.Now.ToString();
        }
        else
        {
            SubmitResponse.Text = "Error! Your profile has not been updated! " + DateTime.Now.ToString();
            MosheTechnologies.RecordError("Update Guest Profile Error", Query);
        }
    }

    void CreateAccount()
    {
        Session["STATUS"] = "2";
        Query = @"INSERT INTO mosheUSERS (State_ID, Location_ID, FirstName, LastName, Display_Name, AccountID, UserName, Password, From_Domain)
            VALUES(0,0," + SiteSecurity.SterilizeValue((FirstName.Text.Trim()));
        Query += @",
                    " + SiteSecurity.SterilizeValue((LastName.Text.Trim())) + @",
                    " + SiteSecurity.SterilizeValue(((FirstName.Text + " " + LastName.Text).Trim())) + @",
                    " + SiteSecurity.SterilizeValue(AccountID.Text.Trim()) + @",     
                    " + SiteSecurity.SterilizeValue(AccountID.Text.Trim(), true) + @",
                    " + SiteSecurity.SterilizeValue(SiteSecurity.ShtarkEncryption(Password.Text.Trim()), true)
                      + "," + SiteSecurity.SterilizeValue(MosheTechnologies.SiteDomain)
                      + ");";
        int new_id = MyDatabase.ExecuteCommand(Query, true);

        if (new_id > 0)
        {
            ProfileID = new_id.ToString();
            
            MyUser.SetupNewUserAccount(ProfileID);


            WebsiteID = MoshesNetwork.CreateNetwork(
                ProfileID, 
                "0",
                MoshesNetwork.NetworkType.Websites,
                FirstName.Text + " " + LastName.Text,
                string.Empty,
                AccountID.Text).ToString();

            MoshesNetwork.Join(WebsiteID, ProfileID);

            if (profile.IsSystemAdmin && CreateNewProfile)
                NextPage = "/admin/users.aspx?uid=" + ProfileID;
            else
            {
                Session["USERID"] = ProfileID;
                Session["FAMILYID"] = WebsiteID;
                
                if (MosheTechnologies.IsWebsite)
                {
                    NextPage = "/websites/signup.aspx?id=" + WebsiteIDValue.Value;
                    Session["NextPage"] = "/profiles/admin/online.aspx?uid=" + ProfileID;
                }
                else
                {
                    string uid = "";
                    //switch (AccountType.SelectedValue)
                    //{
                    //    case "Candidate":
                    //    case "Staffer":
                    //        Query = "SELECT W.ID FROM userWEBSITES W WHERE W.Website LIKE " + SiteSecurity.SterilizeValue("%" + AccountID.Text.Split('@')[1] + "%", true);
                    //        uid = MyDatabase.ExecuteScalar(Query);
                    //        if (MosheTechnologies.IsNumeric(uid, true))
                    //        {
                    //            MoshesNetwork.Join(MoshesNetwork.NetworkType.Websites, uid, ProfileID, true);
                    //            MyWebsite website = new MyWebsite(uid, ProfileID);
                    //            if (website.IsPremiumWebsite)
                    //                NextPage = "/websites/?id=" + uid;
                    //            else
                    //                NextPage = "/groups/?id=" + uid;
                    //        }
                    //        else
                    //            NextPage = "/states/candidates.aspx?id=" + profile.StateID;
                    //        Session["NextPage"] = NextPage;
                    //        break;
                    //    case "Elected":
                    //        Query = "SELECT W.ID FROM userWEBSITES W WHERE W.Website LIKE " + SiteSecurity.SterilizeValue("%" + AccountID.Text.Split('@')[1] + "%", true);
                    //        uid = MyDatabase.ExecuteScalar(Query);
                    //        if (MosheTechnologies.IsNumeric(uid, true))
                    //        {
                    //            MoshesNetwork.Join(MoshesNetwork.NetworkType.Websites, uid, ProfileID, true);
                    //            MyWebsite website = new MyWebsite(uid, ProfileID);
                    //            if (website.IsPremiumWebsite)
                    //                NextPage = "/websites/?id=" + uid;
                    //            else
                    //                NextPage = "/groups/?id=" + uid;
                    //        }
                    //        else
                    //            NextPage = "/states/officials.aspx?id=" + profile.StateID;
                    //        Session["NextPage"] = NextPage;
                    //        break;
                    //    default:
                    //        NextPage = "/states/get-involved.aspx?id=" + profile.StateID;
                    //        Session["NextPage"] = "/profiles/admin/online.aspx?uid=" + ProfileID;
                    //        break;
                    //}
                }   
            }            
            
            MyEmail email = new MyEmail(profile.ID);
            email.IncludeUnsubscribeLink = false;
            string fromName = "Moshe Starkman";
            email.FromEmail = "mstarkman@moshenet.com";
            email.AddRecipient(AccountID.Text, (FirstName.Text + " " + LastName.Text));

            string subject = "Welcome " + FirstName.Text + "!";
            string message;
            
            Query = "SELECT FromName, FromEmail, Subject, Body FROM websiteAUTORESPONSE WHERE Type_ID=4 AND Website_ID=" + WebsiteID;
            System.Data.DataRow msg = MyDatabase.LoadRecord(Query);

            if (msg == null)
            {
                MyContent about = new MyContent(MosheTechnologies.SystemID, "2");

                Query = "SELECT U.ID, U.FirstName, U.LastName, U.AccountID FROM mosheUSERS U INNER JOIN userWEBSITES W ON W.User_ID=U.ID WHERE W.ID=" + WebsiteID;
                System.Data.DataRow admin = MyDatabase.LoadRecord(Query);
                
                message = "Welcome " + FirstName.Text + "!";
                message += @"<p>This email confirms that you have successfully signed up and now have access to MosheNET campaign tools and resources.</p>
                <p><a href=""http://moshenet.com/welcome_to_moshenet"">Click here to get started!</a></p>
                <p>TO update your account, <a href=""http://moshenet.com/profiles/admin/"">clicking here</a>.</p>
                <p><b>For your records, your account information is:</b></p>
                <p>You can view your profile <a href='http://moshenet.com/profiles/?uid=" + ProfileID + @"'>here</a><br />and manage your profile <a href='http://moshenet.com/profiles/admin/default.aspx?uid=" + ProfileID + @"'>here</a>.</p>
                <p><b>Account Information:</b>
                    <br />Account ID: #EMAIL#
                    <br />Username: #USERNAME#
                    <br />Password: #PASSWORDHINT#
                    <br />User ID: #ProfileID#</p>
                <p>When we work together, Conservatives win!<br />Moshe</p>
                <p><a href=""http://twitter.com/MosheNET"">@MosheNET</a><br /><a href=""http://moshenet.com/""><img src=""http://moshenet.com/images/MosheNET_sm.png"" alt=""MosheNET - We Empower People"" title=""MosheNET - We Empower People"" /></a></p>";

                //email.ReplyTo = new System.Net.Mail.MailAddress("mstarkman@moshenet.com", "Moshe Starkman");
            }
            else
            {
                if (string.IsNullOrEmpty(msg["FromName"].ToString())) fromName = msg["FromName"].ToString();
                if (!string.IsNullOrEmpty(msg["Subject"].ToString())) subject = msg["Subject"].ToString();
                if (MosheTechnologies.IsEmail(msg["FromEmail"].ToString()))
                {
                    if (string.IsNullOrEmpty(msg["FromName"].ToString()))
                        email.ReplyTo = new System.Net.Mail.MailAddress(msg["FromEmail"].ToString());
                    else
                        email.ReplyTo = new System.Net.Mail.MailAddress(msg["FromEmail"].ToString(), msg["FromName"].ToString());
                }

                message = msg["Body"].ToString();
            }

            email.FromName = fromName;
            //email.FromEmail = fromEmail;
            email.Subject = subject;
            message = message.Replace("#FIRSTNAME#", FirstName.Text);
            message = message.Replace("#LASTNAME#", LastName.Text);
            message = message.Replace("#EMAIL#", AccountID.Text);
            message = message.Replace("#USERNAME#", AccountID.Text);
            message = message.Replace("#PASSWORD#", Password.Text);
            message = message.Replace("#PASSWORDHINT#", Password.Text.Substring(0, 1).PadRight(Password.Text.Length - 1, '~'));
            message = message.Replace("#DATE#", DateTime.Today.ToShortDateString());
            message = message.Replace("#DATETIME#", DateTime.Now.ToString());
            message = message.Replace("#ProfileID#", ProfileID);
            
            email.Message = message;

            email.Send(IntegratedEmail.MessageType.SystemMessage);

            if (AccountID.Text.EndsWith(".gov"))
            {
                email = new MyEmail("1");
                email.IncludeUnsubscribeLink = false;
                email.FromEmail = "notifications@moshenet.com";
                email.FromName = "MosheNET Alert";
                email.Subject = "ALERT! .gov email address";
                email.AddRecipient("mstarkman@moshetechnologies.com", "MosheTech Support");

                email.Message = "<p>" + FirstName.Text + " " + LastName.Text + "<br />" + AccountID.Text + "</p>";

                email.Send(IntegratedEmail.MessageType.SystemMessage);
            }

            Response.Redirect(NextPage, true);
        }
    }

    void ConfirmThisAccount(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Confirm":
                string account_id = e.CommandArgument.ToString();
                if (MosheTechnologies.IsEmail(account_id))
                {
                    MosheTechnologies.RecordAction("Password Request (" + account_id + ")");
                    MyEmail email = new MyEmail(Session["USERID"].ToString());

                    email.IsHTML = true;
                    email.IncludeUnsubscribeLink = false;
                    email.FromEmail = "notifications@moshenet.com";
                    email.FromName = "MosheNET";

                    email.Subject = MosheTechnologies.ActiveDomain + " - What is my password?";

                    Query = "SELECT U.UserName, U.Password, U.FirstName FROM mosheUSERS U INNER JOIN userLOCATIONS UL ON U.ID=UL.User_ID WHERE (U.AccountID=" + SiteSecurity.SterilizeValue(account_id) + " OR UL.Email=" + SiteSecurity.SterilizeValue(account_id) + ")";
                    System.Data.DataRow userCredentials = MyDatabase.LoadRecord(Query);
                    if (userCredentials != null)
                    {
                        string custom_message = userCredentials["FirstName"].ToString() + ",";
                        custom_message += "<p>Your network username is: <b>" + userCredentials["UserName"].ToString() + "</b>";
                        custom_message += "<br />&amp; your password is: <b>" + SiteSecurity.RetrievePassword(userCredentials["Password"].ToString()) + "</b>";
                        custom_message += @"<p><a href='http://" + MosheTechnologies.ActiveDomain + "/'>click here to login</a></p>";
                        custom_message += @"<p><a href='http://" + MosheTechnologies.ActiveDomain + @"/profiles/admin/info.aspx'>to update your profile, click here</a></p>                
                                        <p>If you have any more questions, please feel welcome to contact us.</p>
                                        <p>Thank you,<br />" + MosheTechnologies.ActiveDomain + " Support Team</p>";
                        email.AddRecipient(account_id);
                        email.Message = custom_message;
                    }

                    Button GetPassword = (Button)sender;
                    
                    if (email.Send(IntegratedEmail.MessageType.SystemMessage))
                        GetPassword.Text = "Password Sent!";

                    GetPassword.Enabled = false;
                }
                break;
            case "Create":
                AccountID.Text = Email2.Text;
                NewAccountConfirmation.Visible = false;
                break;
        }
    }

    void ValidateAccountID(object sender, EventArgs e)
    {
        if (MosheTechnologies.IsEmail(AccountID.Text))
        {
            Query = "SELECT COUNT(*) FROM mosheUSERS WHERE AccountID=" + SiteSecurity.SterilizeValue(AccountID.Text, true);
            if (MyDatabase.ExecuteScalar(Query).Equals("0"))
                EmailError.Text = "<div style='color:Green; font-weight:bold;'>accepted account id</div>";
            else
            {
                EmailError.Text = "<div style='color:Red;'>\"<span style='color:#000000'>" + AccountID.Text + "</span>\" is not available.</div> Try logging in and or requesting your password.</div>";
                AccountID.Text = "";
                AccountID.Focus();
            }
        }
        else
            EmailError.Text = "<div style='color:Red; font-weight:bold;'>Invalid email address</div>";
    }

    void CloseNewAccountConfirmation_Click(object sender, EventArgs e)
    { NewAccountConfirmation.Visible = false; }
</script>