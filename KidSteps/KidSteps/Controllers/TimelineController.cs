using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.ActionFilters;
using KidSteps.Models;
using KidSteps.Utils;

namespace KidSteps.Controllers
{
    public partial class TimelineController : TargetedController<User>
    {
        [UserTarget(Permission.ReadUser)]
        [ChildActionOnly]
        public virtual PartialViewResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            List<TimelineEvent> allEvents = 
                Target.TimelineEvents.
                OrderByDescending(te => te.CreatedTime).
                ToList();
            // sort by create time descending
            allEvents.Sort((te1, te2) => -te1.CreatedTime.CompareTo(te2.CreatedTime));

            Dictionary<TimelineEvent, Stack<TimelineEvent>> conversations =
                new Dictionary<TimelineEvent, Stack<TimelineEvent>>();

            foreach (TimelineEvent timelineEvent in allEvents)
            {
                if (timelineEvent.IsReplyTo == null)
                    conversations[timelineEvent] = new Stack<TimelineEvent>();
            }

            foreach (TimelineEvent timelineEvent in allEvents)
            {
                if (timelineEvent.IsReplyTo != null)
                    conversations[timelineEvent.IsReplyTo].Push(timelineEvent);
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
        [UserTarget(Models.Permission.Comment)]
        public virtual PartialViewResult Index(IndexViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.NewComment))
            {
                Comment newComment = new Comment();
                newComment.Owner = CurrentUser;
                newComment.SubjectUser = Target;
                newComment.Text = model.NewComment;
                if (model.NewCommentParent != -1)
                    // todo: security check - make sure has permissions for parent comment
                    newComment.IsReplyTo = db.TimelineEvents.Find(model.NewCommentParent);
                db.TimelineEvents.Add(newComment);
                db.SaveChanges();
            }

            return Index();

            IndexViewModel model1 = new IndexViewModel();
            List<TimelineEvent> allEvents = Target.TimelineEvents.ToList();
            // sort by create time descending
            allEvents.Sort((te1, te2) => -te1.CreatedTime.CompareTo(te2.CreatedTime));

            Dictionary<TimelineEvent, List<TimelineEvent>> conversations =
                new Dictionary<TimelineEvent, List<TimelineEvent>>();

            foreach (TimelineEvent timelineEvent in allEvents)
            {
                conversations[timelineEvent] = new List<TimelineEvent>();
            }

            foreach (var pair in conversations)
            {
                Conversation conversation = new Conversation();
                conversation.ParentItem = pair.Key;
                conversation.Replies = pair.Value;
                model1.Conversations.Add(conversation);
            }

            return PartialView(model1);
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
            public int NewCommentParent { get; set; }
            public string NewComment { get; set; }
        }

        public class Conversation
        {
            public TimelineEvent ParentItem { get; set; }
            public List<TimelineEvent> Replies { get; set; }
            public string NewReply { get; set; }
        }

        #endregion
    }
}
