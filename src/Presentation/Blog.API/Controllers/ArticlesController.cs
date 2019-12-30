using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.API.Models.ArticleModels;
using Blog.Core.Domain;
using Blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Blog.API.Controllers
{
    /// <summary>
    /// articles api controller
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IArticleService articleService,
            ILogger<ArticlesController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        /// <summary>
        /// search articles ArticlePageOptionsModel model parameter
        /// </summary>
        /// <param name="pageOptions">search parameters</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of Article for the specified page.", typeof(ArticlePageResultModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public IActionResult Get([FromQuery] ArticlePageOptionsModel pageOptions)
        {
            try
            {
                var result = _articleService.SearchArticles(pageOptions.Title, body: pageOptions.Body, pageIndex: (pageOptions.Page - 1), pageSize: pageOptions.Count);
                if (result.TotalCount == 0)
                    return new NotFoundResult();

                var model = new ArticlePageResultModel
                {
                    TotalCount = result.TotalCount,
                    TotalPages = result.TotalPages,
                    Page = pageOptions.Page,
                    Count = result.Items.Count,
                    Items = result.Items.Select(x => new ArticleModel
                    {
                        Id=x.Id,
                        Title = x.Title,
                        Body = x.Body,
                        AllowComments = x.AllowComments,
                        BodyOverview = x.BodyOverview,
                        CreatedOnUtc = x.CreatedOnUtc,
                        EndDateUtc = x.EndDateUtc,
                        StartDateUtc = x.StartDateUtc,
                        MetaDescription = x.MetaDescription,
                        MetaKeywords = x.MetaKeywords,
                        MetaTitle = x.MetaTitle,
                        Tags = x.Tags
                    }).ToList()
                };

                return new ObjectResult(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get article by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public IActionResult Get(int id)
        {
            try
            {
                var entity = _articleService.GetArticleById(id);
                if (entity == null)
                    return new NotFoundResult();

                var model = new ArticleModel
                {
                    Id=entity.Id,
                    Title = entity.Title,
                    Body = entity.Body,
                    AllowComments = entity.AllowComments,
                    BodyOverview = entity.BodyOverview,
                    CreatedOnUtc = entity.CreatedOnUtc,
                    EndDateUtc = entity.EndDateUtc,
                    StartDateUtc = entity.StartDateUtc,
                    MetaDescription = entity.MetaDescription,
                    MetaKeywords = entity.MetaKeywords,
                    MetaTitle = entity.MetaTitle,
                    Tags = entity.Tags
                };

                return new ObjectResult(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add article as ArticleModel model
        /// </summary>
        /// <param name="model">ArticleModel model</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Insert success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public IActionResult Post([FromBody] ArticleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Article
                    {
                        Title = model.Title,
                        Body = model.Body,
                        AllowComments = model.AllowComments,
                        BodyOverview = model.BodyOverview,
                        EndDateUtc = model.EndDateUtc,
                        StartDateUtc = model.StartDateUtc,
                        MetaDescription = model.MetaDescription,
                        MetaKeywords = model.MetaKeywords,
                        MetaTitle = model.MetaTitle,
                        Tags = model.Tags
                    };
                    _articleService.AddArticle(entity);

                    return Ok();
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update article by id
        /// </summary>
        /// <param name="id">article id</param>
        /// <param name="model">ArticleModel model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public IActionResult Put(int id, [FromBody]ArticleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _articleService.GetArticleById(id);
                    if (entity == null)
                        return new NotFoundResult();

                    entity.Title = model.Title;
                    entity.Body = model.Body;
                    entity.AllowComments = model.AllowComments;
                    entity.BodyOverview = model.BodyOverview;
                    entity.EndDateUtc = model.EndDateUtc;
                    entity.StartDateUtc = model.StartDateUtc;
                    entity.MetaDescription = model.MetaDescription;
                    entity.MetaKeywords = model.MetaKeywords;
                    entity.MetaTitle = model.MetaTitle;
                    entity.Tags = model.Tags;

                    _articleService.UpdateArticle(entity);

                    return Ok();
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete article by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public IActionResult Delete(int id)
        {
            try
            {
                var entity = _articleService.GetArticleById(id);
                if (entity == null)
                    return new NotFoundResult();

                entity.Deleted = true;
                _articleService.UpdateArticle(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
