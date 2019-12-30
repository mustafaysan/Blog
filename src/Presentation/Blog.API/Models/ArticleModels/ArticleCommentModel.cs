using System;
using System.ComponentModel;

namespace Blog.API.Models.ArticleModels
{
    /// <summary>
    /// article model
    /// </summary>
    public class ArticleCommentModel
    {
        /// <summary>
        /// Gets or sets the blog articleComment Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the blog articleComment CommentText
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the blog articleComment IsApproved
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the blog articleComment ArticleId
        /// </summary>
        public int ArticleId { get; set; }


    }
}
