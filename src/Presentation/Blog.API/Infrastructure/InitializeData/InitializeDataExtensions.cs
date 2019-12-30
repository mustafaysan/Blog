using Blog.Core.Domain;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.API.Infrastructure.InitializeData
{
    /// <summary>
    /// Initialize db Data
    /// </summary>
    public static class InitializeDataExtensions
    {
        /// <summary>
        /// Databese deed data
        /// </summary>
        /// <param name="host">IWebHost</param>
        /// <returns>IWebHost</returns>
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<IDbContext>();
                var signInManager = services.GetRequiredService<ISignInManager>();

                dbContext.AddInitializeUserDataAsync(signInManager).Wait();
                dbContext.AddInitializeArticleDataAsync().Wait();
            }
            return host;
        }

        private static async Task AddInitializeUserDataAsync(this IDbContext dbContext, ISignInManager signInManager)
        {
            var usersTable = dbContext.Set<User>();
            if (!(await usersTable.AnyAsync()))
            {
                var user1 = new User
                {
                    FirstName = "fn 1",
                    LastName = "ln 1",
                    UserName = "user1",
                    Email = "user1@user.com",
                    Active = true,
                    CreatedOnUtc = DateTime.UtcNow
                };
                signInManager.CreatePassword(user1, "123");

                var user2 = new User
                {
                    FirstName = "fn 2",
                    LastName = "ln 2",
                    UserName = "user2",
                    Email = "user2@user.com",
                    Active = true,
                    CreatedOnUtc = DateTime.UtcNow
                };
                signInManager.CreatePassword(user2, "123a");

                usersTable.AddRange(user1, user2);
                dbContext.SaveChanges();
            }
        }

        private static async Task AddInitializeArticleDataAsync(this IDbContext dbContext)
        {
            var articlesTable = dbContext.Set<Article>();
            if (!(await articlesTable.AnyAsync()))
            {
                var article1 = new Article
                {
                    Title = "t article1",
                    Body = "b article1",
                    BodyOverview = "bo article1",
                    Tags = "tg article1",
                    MetaTitle = "mt article1",
                    MetaKeywords = "mk article1",
                    MetaDescription = "md article1",
                    StartDateUtc = DateTime.UtcNow.AddHours(2),
                    CreatedOnUtc = DateTime.UtcNow,
                    EndDateUtc = DateTime.UtcNow.AddYears(5),
                    AllowComments = true,
                    Deleted = false
                };
                article1.ArticleComments.Add(new ArticleComment { CommentText = "c article1-1", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });
                article1.ArticleComments.Add(new ArticleComment { CommentText = "c article1-2", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });

                var article2 = new Article
                {
                    Title = "t article2",
                    Body = "b article2",
                    BodyOverview = "bo article2",
                    Tags = "tg article2",
                    MetaTitle = "mt article2",
                    MetaKeywords = "mk article2",
                    MetaDescription = "md article2",
                    StartDateUtc = DateTime.UtcNow.AddHours(2),
                    CreatedOnUtc = DateTime.UtcNow,
                    EndDateUtc = DateTime.UtcNow.AddYears(5),
                    AllowComments = true,
                    Deleted = false
                };
                article2.ArticleComments.Add(new ArticleComment { CommentText = "c article2-1", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });
                article2.ArticleComments.Add(new ArticleComment { CommentText = "c article2-2", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });

                var article3 = new Article
                {
                    Title = "t article3",
                    Body = "b article3",
                    BodyOverview = "bo article3",
                    Tags = "tg article3",
                    MetaTitle = "mt article3",
                    MetaKeywords = "mk article3",
                    MetaDescription = "md article3",
                    StartDateUtc = DateTime.UtcNow.AddHours(2),
                    CreatedOnUtc = DateTime.UtcNow,
                    EndDateUtc = DateTime.UtcNow.AddYears(5),
                    AllowComments = true,
                    Deleted = false
                };
                article3.ArticleComments.Add(new ArticleComment { CommentText = "c article3-1", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });
                article3.ArticleComments.Add(new ArticleComment { CommentText = "c article3-2", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });

                articlesTable.AddRange(article1, article2, article3);
                dbContext.SaveChanges();
            }
        }
    }
}
