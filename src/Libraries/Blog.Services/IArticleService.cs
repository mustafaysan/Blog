using Blog.Core;
using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Services
{
    public interface IArticleService
    {
        Article GetArticleById(int id);
        void AddArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(int id);
        PagingResult<Article> SearchArticles(string title = "", string body = "", string tags = "", int pageIndex = 0, int pageSize = int.MaxValue);
        ArticleComment GetArticleCommentById(int id);
        void AddArticleComment(ArticleComment articleComment);
        void UpdateArticleComment(ArticleComment articleComment);
        void DeleteArticleComment(int id);
        PagingResult<ArticleComment> SearchArticlesComment(string commentText = "", int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
