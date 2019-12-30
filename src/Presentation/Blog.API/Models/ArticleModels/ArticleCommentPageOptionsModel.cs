using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.ArticleModels
{
    /// <summary>
    /// article comment search model options
    /// </summary>
    public class ArticleCommentPageOptionsModel : PageOptionsModel
    {
        /// <summary>
        /// article comment text
        /// </summary>
        public string CommentText { get; set; }
        /// <summary>
        /// Data Count min:1 max:100
        /// </summary>
        [Range(1, 100)]
        public override int Count { get; set; } = 100;
    }
}
