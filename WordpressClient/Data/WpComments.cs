using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WordpressClient.Data
{
    public partial class WpComments
    {
        public ulong CommentId { get; set; }
        public ulong CommentPostId { get; set; }
        public string CommentAuthor { get; set; }
        public string CommentAuthorEmail { get; set; }
        public string CommentAuthorUrl { get; set; }
        public string CommentAuthorIp { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime CommentDateGmt { get; set; }
        public string CommentContent { get; set; }
        public int CommentKarma { get; set; }
        public string CommentApproved { get; set; }
        public string CommentAgent { get; set; }
        public string CommentType { get; set; }
        public ulong CommentParent { get; set; }
        public ulong UserId { get; set; }
    }
}
