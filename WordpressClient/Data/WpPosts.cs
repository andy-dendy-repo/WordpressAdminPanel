using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WordpressClient.Data
{
    public partial class WpPosts
    {
        public ulong Id { get; set; }
        public ulong PostAuthor { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
        public DateTime PostDateGmt { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }
        public string PostExcerpt { get; set; }
        public string PostStatus { get; set; }
        public string CommentStatus { get; set; }
        public string PingStatus { get; set; }
        public string PostPassword { get; set; }
        public string PostName { get; set; }
        public string ToPing { get; set; }
        public string Pinged { get; set; }
        public DateTime PostModified { get; set; }
        public DateTime PostModifiedGmt { get; set; }
        public string PostContentFiltered { get; set; }
        public ulong PostParent { get; set; }
        public string Guid { get; set; }
        public int MenuOrder { get; set; }
        public string PostType { get; set; }
        public string PostMimeType { get; set; }
        public long CommentCount { get; set; }
    }
}
