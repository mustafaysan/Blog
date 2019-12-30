using System;
using System.ComponentModel;

namespace Blog.API.Models.ArticleModels
{
    /// <summary>
    /// article model
    /// </summary>
    public class ArticleModel
    {
        /// <summary>
        /// Gets or sets the blog article Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the blog article title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the blog article body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the blog article overview. If specified, then it's used on the blog page instead of the "Body"
        /// </summary>
        public string BodyOverview { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the blog article comments are allowed 
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Gets or sets the blog tags
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the blog article start date and time
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the blog article end date and time
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }


        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}
