<%@ Control Language="C#" Inherits="BaseControl" ClassName="ValidationControl" %>

<asp:Panel runat="server" ID="ControlPanel">
    <table>
        <tr runat="server" id="ValidationRow">
            <td rowspan="2" style="vertical-align:top; padding-right:5px;">
                <asp:Repeater runat="server" ID="MosheTechNetwork" OnItemDataBound="UpdateFeaturedMember">
                    <ItemTemplate>
                        <div style="text-align:center">
                            <img style="width:60px; height:60px; cursor:pointer" src="<%#((System.Data.DataRowView)Container.DataItem)["Image"].ToString()%>" alt="<%#((System.Data.DataRowView)Container.DataItem)["Caption"]%>" title="<%#((System.Data.DataRowView)Container.DataItem)["Caption"]%>" onclick="document.getElementById('<%=UserAnswer.ClientID%>').value='<%#((System.Data.DataRowView)Container.DataItem)["Caption"]%>'; enableSubmission();" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel runat="server" ID="DefaultPicPanel" Visible="false" style="padding:1px 1px 2px 1px; border:solid 1px #333333; background-color:#ffffff; text-align:center">
                    <asp:Image runat="server" ID="DefaultPic" style="width:80px; height:80px; cursor:pointer" />
                    <div style="padding:2px 0px"><asp:Label runat="server" ID="DefaultCaption" /></div>
                </asp:Panel>
            </td>
            <td style="vertical-align:top; color:#333333; font-style:italic;">
                <asp:Panel runat="server" ID="ContentPanel"><asp:Literal runat="server" ID="WebPageContent" Text="" /></asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="UserAnswer" runat="server" Width="100px" ForeColor="Green" Font-Bold="true" BorderColor="Transparent" ReadOnly="true" />
                <div><asp:Button runat="server" ID="SubmitButton" Text="submit" OnClick="SubmitButton_Click" OnClientClick="this.style.display='none'; ToggleView('validate_btn1');" /><span id="validate_btn1" style="display:none; font-style:italic;"><img src="/images/animations/loading.gif" alt="sending..." style="vertical-align:middle; height:18px;" />submitting...</span></div>
            </td>
        </tr>
    </table>
    <div><asp:Label runat="server" ID="ErrorMessage" Font-Bold="true" ForeColor="Red" Text="" /></div>
    <asp:HiddenField runat="server" ID="SecurityAnswer" />
    <asp:HiddenField runat="server" ID="WebsiteIDValue" />
    <asp:HiddenField runat="server" ID="PictureIDValue" />
    
    <script type="text/javascript">
        function enableSubmission() {
            $("#<%=SubmitButton.ClientID%>").removeAttr("disabled");
        }
    </script>
</asp:Panel>

<script runat="server">
    public string WebsiteID
    {
        get { return WebsiteIDValue.Value; }
        set { WebsiteIDValue.Value = value; }
    }

    public string PictureLoc
    {
        get { return PictureIDValue.Value; }
        set { PictureIDValue.Value = value; }
    }

    public string Instructions
    {
        get { return WebPageContent.Text; }
        set { WebPageContent.Text = value; }
    }
    
    public string Error
    {
        get { return ErrorMessage.Text; }
        set { ErrorMessage.Text = value; }
    }
    
    public string Answer
    { get { return UserAnswer.Text; } }
    public string CorrectAnswer
    { 
        get { return SecurityAnswer.Value; }
        set { SecurityAnswer.Value = value; 
        }
    }

    string answer_key = "|";
    public string DefaultAnswer
    {
        set
        {
            if (value.Contains("|"))
                answer_key = value;
        }
    }
    public string[] AnswerKey
    {
        get { return answer_key.Split('|'); }
        set
        {
            if (value.Length.Equals(2))
            {
                MosheTechNetwork.Visible = false;
                DefaultPicPanel.Visible = true;

                PictureIDValue.Value = value[0];
                CorrectAnswer = value[1];

                if (PictureLoc.StartsWith("http"))
                    DefaultPic.ImageUrl = PictureLoc;
                else
                    DefaultPic.ImageUrl = "http://moshenet.com" + PictureLoc;
                
                DefaultPic.Attributes.Add("onclick", "document.getElementById(" + SiteSecurity.SterilizeValue(UserAnswer.ClientID) + ").value=" + SiteSecurity.SterilizeValue(CorrectAnswer) + "; document.getElementById(" + SiteSecurity.SterilizeValue(UserAnswer.ClientID) + ").style.color='#000000'; enableSubmission();");

                DefaultCaption.Text = CorrectAnswer;

                SubmitButton.Attributes.Add("disabled", "disabled");
            }
        }
    }

    public string ValidationGroup
    {
        get { return SubmitButton.ValidationGroup; }
        set { 
            SubmitButton.ValidationGroup = value;
            UserAnswer.ValidationGroup = value;
        }
    }

    public string CssClass
    { set { ControlPanel.CssClass = value; } }
    public string ButtonText
    {
        get { return SubmitButton.Text; }
        set { SubmitButton.Text = value; }
    }

    public string ButtonCss
    { set { SubmitButton.CssClass = value; } }

    public string ButtonStyle
    {
        get { return SubmitButton.Style.Value; }
        set { SubmitButton.Style.Value = value; }
    }

    public bool ShowSubmitButton
    {
        get { return SubmitButton.Visible; }
        set { SubmitButton.Visible = value; }
    }

    bool isValid;
    public bool IsValidated
    {
        get { return isValid; }
        set { isValid = value; }
    }

    public int Width
    {
        set
        {
            ControlPanel.Width = Unit.Pixel(value);
            ContentPanel.Width = Unit.Pixel(value - 90);
        }
    }
    
    public string UserID
    { get { return Session["USERID"].ToString(); } }
    bool isLoggedIn
    { get { return MosheTechnologies.IsNumeric(UserID, true); } }
    
    void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (isLoggedIn)
            {
                ValidationRow.Visible = false;
                UserAnswer.Visible = false;
                SubmitButton.Enabled = true;

                return;
            }

            SubmitButton.Attributes.Add("disabled", "disabled");

            ControlPanel.Width = Unit.Pixel(W);

            if (string.IsNullOrEmpty(WebPageContent.Text))
                WebPageContent.Text = "<div class='line-1'>User verification.</div>Please click the image to the left.";

            LoadDefaultValues();
        }
    }

    public delegate void ClickEventHandler(object sender, EventArgs e);
    public event ClickEventHandler Submit;

    void LoadDefaultValues()
    {
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Image");
        table.Columns.Add("Caption");
        
        string[] row = new string[2];
        row[0] = (string.IsNullOrEmpty(PictureLoc) ? "/images/dont_tread_on_me.png" : PictureLoc);
        row[1] = (string.IsNullOrEmpty(CorrectAnswer) ? "Validated!" : CorrectAnswer);
        table.Rows.Add(row);

        MosheTechNetwork.DataSource = table;
        MosheTechNetwork.DataBind();                    
    }
    
    void SubmitButton_Click(object sender, EventArgs e)
    {
        if (isLoggedIn || (string.IsNullOrEmpty(UserAnswer.Text) && SecurityAnswer.Value.Equals("Validated!")) || UserAnswer.Text.ToLower().Equals(SecurityAnswer.Value.ToLower()))
        {
            ErrorMessage.Text = "";
            isValid = true;

            if (Submit != null) Submit(sender, e);

            return;
        }

        ErrorMessage.Text = "Whoops! You didn't get the security question right. HINT: Click the picture.";

        isValid = false;
    }

    void UpdateFeaturedMember(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        { 
            SecurityAnswer.Value = ((System.Data.DataRowView)e.Item.DataItem)["Caption"].ToString();

            answer_key = ((System.Data.DataRowView)e.Item.DataItem)["Image"].ToString() + "|" + ((System.Data.DataRowView)e.Item.DataItem)["Caption"].ToString();
        }
    }
</script>
