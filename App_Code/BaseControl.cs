using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for BaseControl
/// </summary>
public class BaseControl : System.Web.UI.UserControl
{
    public BaseControl()
    { SetDefaultShortcuts(); }

    public string Query = "";

    string width = "300px";
    public string Width
    {
        get { return width; }
        set
        {
            width = value;
            if (!width.EndsWith("px") && !width.EndsWith("%"))
                width += "px";
        }
    }
    public int W
    { get { return Convert.ToInt32(width.Replace("px", "").Replace("%", "")); } }

    int rows = 20;
    public int Rows
    {
        get { return rows; }
        set { rows = value; }
    }

    public void SetDefaultShortcuts()
    {
        if (MosheTechnologies.IsWebsite)
        {
            VideoPath = "/media.aspx?type=video&uid=";
            MediaPath = "/media.aspx?type=article&uid=";
            BlogPath = "/media.aspx?type=blog&uid=";
            EventPath = "/event.aspx?uid=";
            ContentPath = "/page.aspx?uid=";
            WebsitePath = "/affiliates.aspx?uid=";
            ProfilePath = "/profiles/?uid=";
        }
        else
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["VideoSubdomain"]))
                VideoPath = MyContent.VideoPath;
            else
                VideoPath = "http://" + ConfigurationManager.AppSettings["VideoSubdomain"] + "." + MosheTechnologies.DefaultDomain + "/";

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["MediaSubdomain"]))
                MediaPath = MyContent.MediaPath;
            else
                MediaPath = "http://" + ConfigurationManager.AppSettings["MediaSubdomain"] + "." + MosheTechnologies.DefaultDomain + "/";

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["BlogSubdomain"]))
                BlogPath = MyContent.BlogPath;
            else
                BlogPath = "http://" + ConfigurationManager.AppSettings["BlogSubdomain"] + "." + MosheTechnologies.DefaultDomain + "/";

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["EventSubdomain"]))
                EventPath = MyContent.EventPath;
            else
                EventPath = "http://" + ConfigurationManager.AppSettings["EventSubdomain"] + "." + MosheTechnologies.DefaultDomain + "/";

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["PageSubdomain"]))
                ContentPath = MyContent.ContentPath;
            else
                ContentPath = "http://" + ConfigurationManager.AppSettings["PageSubdomain"] + "." + MosheTechnologies.DefaultDomain + "/";

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WebsiteSubdomain"]))
                WebsitePath = MyContent.WebsitePath;
            else
                WebsitePath = "http://" + ConfigurationManager.AppSettings["WebsiteSubdomain"] + "." + MosheTechnologies.DefaultDomain + "/";

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ProfileSubdomain"]))
                ProfilePath = MyContent.ProfilePath;
            else
                ProfilePath = "http://" + ConfigurationManager.AppSettings["ProfileSubdomain"] + "." + MosheTechnologies.DefaultDomain + "/";
        }
    }

    string video_path = "";
    public string VideoPath
    {
        get { return video_path; }
        set { video_path = value; }
    }

    string media_path = "";
    public string MediaPath
    {
        get { return media_path; }
        set { media_path = value; }
    }

    string blog_path = MyContent.BlogPath;
    public string BlogPath
    {
        get { return blog_path; }
        set { blog_path = value; }
    }

    string event_path = MyContent.EventPath;
    public string EventPath
    {
        get { return event_path; }
        set { event_path = value; }
    }

    string content_path = MyContent.ContentPath;
    public string ContentPath
    {
        get { return content_path; }
        set { content_path = value; }
    }

    string profile_path = "";
    public string ProfilePath
    {
        get { return profile_path; }
        set { profile_path = value; }
    }

    string website_path = "";
    public string WebsitePath
    {
        get { return website_path; }
        set { website_path = value; }
    }
    public string CampaignPath
    {
        get { return website_path; }
        set { website_path = value; }
    }
}
