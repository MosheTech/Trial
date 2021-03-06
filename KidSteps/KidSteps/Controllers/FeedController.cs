﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.ActionFilters;
using KidSteps.Models;
using KidSteps.Utils;

namespace KidSteps.Controllers
{
    public partial class FeedController : TargetedController<User>
    {
        [UserTarget(Permission.ReadUser)]
        [ChildActionOnly]
        public virtual PartialViewResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            List<FeedItem> allFeedItems = 
                Target.FeedItems.
                OrderByDescending(te => te.CreatedTime).
                ToList();
            // sort by create time descending
            allFeedItems.Sort((te1, te2) => -te1.CreatedTime.CompareTo(te2.CreatedTime));

            Dictionary<FeedItem, Stack<FeedItem>> conversations =
                new Dictionary<FeedItem, Stack<FeedItem>>();

            foreach (FeedItem feedItem in allFeedItems)
            {
                if (feedItem.IsReplyTo == null)
                    conversations[feedItem] = new Stack<FeedItem>();
            }

            foreach (FeedItem feedItem in allFeedItems)
            {
                if (feedItem.IsReplyTo != null)
                    conversations[feedItem.IsReplyTo].Push(feedItem);
            }

            foreach (var pair in conversations)
            {
                Conversation conversation = new Conversation();
                conversation.ParentItem = pair.Key;
                conversation.Replies = pair.Value.ToList();
                model.Conversations.Add(conversation);
            }

            model.CurrentUser = CurrentUser;

            return PartialView(model);
        }

        [HttpPost]
        [UserTarget(Models.Permission.TextPost)]
        public virtual PartialViewResult Index(IndexViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.NewTextPost))
            {
                TextPost newTextPost = new TextPost();
                newTextPost.Owner = CurrentUser;
                newTextPost.SubjectUser = Target;
                newTextPost.Text = model.NewTextPost;
                if (model.NewTextPostParent != -1)
                {
                    newTextPost.IsReplyTo = db.FeedItems.Find(model.NewTextPostParent);
                    if (!CurrentUser.IsAllowedTo(Permission.Reply, newTextPost.IsReplyTo))
                        throw new ArgumentException("Posted to different family");
                }
                db.FeedItems.Add(newTextPost);
                db.SaveChanges();
            }

            return Index();
        }

        #region ViewModels

        public class IndexViewModel
        {
            public IndexViewModel()
            {
                Conversations = new List<Conversation>();
            }

            public User CurrentUser { get; set; }
            public List<Conversation> Conversations { get; set; }
            public int NewTextPostParent { get; set; }
            public string NewTextPost { get; set; }
        }

        public class Conversation
        {
            public FeedItem ParentItem { get; set; }
            public List<FeedItem> Replies { get; set; }
            public string NewReply { get; set; }
        }

        #endregion
    }
}
