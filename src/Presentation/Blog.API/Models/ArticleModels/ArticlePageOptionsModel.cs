using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.ArticleModels
{
    /// <summary>
    /// article search model options
    /// </summary>
    public class ArticlePageOptionsModel : PageOptionsModel
    {
        /// <summary>
        /// article title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// article body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Data Count min:1 max:100
        /// </summary>
        [Range(1, 100)]
        public override int Count { get; set; } = 100;
    }
}
