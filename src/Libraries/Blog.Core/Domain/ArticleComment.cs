using System;

namespace Blog.Core.Domain
{
    public class ArticleComment : BaseEntity
    {
        /// <summary>
        /// Gets or sets the comment text
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the comment is approved
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the blog article identifier
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the blog article
        /// </summary>
        public virtual Article Article { get; set; }
    }
}
