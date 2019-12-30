using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Blog.Core;
using Blog.Core.Domain;
using Blog.Data;

namespace Blog.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _repositoryArticle;
        private readonly IRepository<ArticleComment> _repositoryArticleComment;
        public ArticleService(IRepository<Article> repositoryArticle,
            IRepository<ArticleComment> repositoryArticleComment)
        {
            _repositoryArticle = repositoryArticle;
            _repositoryArticleComment = repositoryArticleComment;
        }

        public Article GetArticleById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");

            return _repositoryArticle.GetById(id);
        }

        public void AddArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            _repositoryArticle.Insert(article);
        }

        public void DeleteArticle(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");

            var article = _repositoryArticle.GetById(id);
            if (article == null)
                throw new Exception($"not found article id:{id}");

            article.Deleted = true;
            _repositoryArticle.Update(article);
        }

        public PagingResult<Article> SearchArticles(string title = "", string body = "", string tags = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _repositoryArticle.Table;

            if (!string.IsNullOrEmpty(title))
                query = query.Where(x => x.Title.Contains(title));

            if (!string.IsNullOrEmpty(body))
                query = query.Where(x => x.Body.Contains(body));

            if (!string.IsNullOrEmpty(tags))
                query = query.Where(x => x.Tags.Contains(tags));

            return new PagingResult<Article>(query, pageIndex, pageSize);
        }


        public void UpdateArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            _repositoryArticle.Update(article);
        }

        public ArticleComment GetArticleCommentById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");

            return _repositoryArticleComment.GetById(id);
        }

        public void AddArticleComment(ArticleComment articleComment)
        {
            if (articleComment == null)
                throw new ArgumentNullException("articleComment");

            _repositoryArticleComment.Insert(articleComment);
        }

        public void UpdateArticleComment(ArticleComment articleComment)
        {
            if (articleComment == null)
                throw new ArgumentNullException("arcticleComment");

            _repositoryArticleComment.Update(articleComment);
        }

        public void DeleteArticleComment(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");
            var articleComment = _repositoryArticleComment.GetById(id);
            articleComment.IsApproved = false;

            _repositoryArticleComment.Update(articleComment);
        }

        public PagingResult<ArticleComment> SearchArticlesComment(string commentText = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _repositoryArticleComment.Table;

            if (!string.IsNullOrEmpty(commentText))
                query = query.Where(x => x.CommentText.Contains(commentText));

            return new PagingResult<ArticleComment>(query, pageIndex, pageSize);
        }
    }
}
