using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using DataLayer.Models;

namespace DataLayer.ViewModels
{
    public sealed class VmTopic : TblTopic
    {
        public ICollection<TblComment> Comments { get; set; }

        public VmTopic(TblTopic topic)
        {
            TopicId = topic.TopicId;
            Title = topic.Title;
            Body = topic.Body;
            DateCreated = topic.DateCreated;
            ClientId = topic.ClientId;
            VoteCount = topic.VoteCount;
            IsValid = topic.IsValid;
            Client = topic.Client;
            Comments = new List<TblComment>();
            foreach (TblTopicCommentRel i in topic.TblTopicCommentRel)
                Comments.Add(i.Comment);
        }
    }
}