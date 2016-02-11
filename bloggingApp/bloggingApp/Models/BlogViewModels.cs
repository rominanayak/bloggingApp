using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace bloggingApp.Models
{
    public class Post
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "The {0} must be shown between {2} and {1} characters long.", MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "ShortDescription")]
        [StringLength(250, ErrorMessage = "The {0} must be shown between {2} and {1} characters long.", MinimumLength = 20)]
        public string ShortDescription { get; set; }
        [Required]
        [Display(Name = "Body")]
        [StringLength(5000, ErrorMessage = "The {0} must be shown between {2} and {1} characters long.", MinimumLength = 500)]
        public string Body { get; set; }
        public bool Published { get; set; }
        [DefaultValue(0)]
        public int NetLikeCount { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? Modified { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
        //public ICollection<PostTag> PostTags { get; set; }
        //public ICollection<PostVideo> PostVideos { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        [StringLength(20, ErrorMessage = "The {0} must be shown between {2} and {1} characters long.", MinimumLength = 5)]
        public string Description { get; set; }
        public bool Checked { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; }
    }

    public class PostCategory
    {
        [Key]
        [Column(Order = 0)]
        public string PostId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string CategoryId { get; set; }
        public bool Checked { get; set; }
        public Post Post { get; set; }
        public Category Category { get; set; }
    }

    public class Comment
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be shown between {2} and {1} characters long.", MinimumLength = 25)]
        public string Body { get; set; }
        [DefaultValue(0)]
        public int NetLikeCount { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
        public Post Post { get; set; }

        public ICollection<Reply> Replies { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
    }

    public class Reply
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
        public string ParentReplyId { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be shown between {2} and {1} characters long.", MinimumLength = 25)]
        public string Body { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public ICollection<ReplyLike> ReplyLikes { get; set; }
    }

    public class PostLike
    {
        [Key]
        public string PostId { get; set; }
        public string Username { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
        public Post Post { get; set; }
    }

    public class CommentLike
    {
        [Key]
        public string CommentId { get; set; }
        public string Username { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
        public Comment Comment { get; set; }
    }

    public class ReplyLike
    {
        [Key]
        public string ReplyId { get; set; }
        public string Username { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
        public Reply Reply { get; set; }
    }
}