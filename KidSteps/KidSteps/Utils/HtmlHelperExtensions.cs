using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;

namespace KidSteps.Utils
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html, ActionResult action, Image image, int size = 100, string genericPath = null)
        {
            string path;
            string alt;
            if (image != null)
            {
                path = image.Url;
                alt = image.AltText;
            }
            else
            {
                path = genericPath;
                alt = string.Empty;
            }

            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(path));
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("height", size.ToString());
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        public static MvcHtmlString ProfileActionImage(this HtmlHelper html, ActionResult action, Image image, int size = 100)
        {
            return ActionImage(html, action, image, size, Links.Content.Images.profile_placeholder_jpg);
        }
    }
}