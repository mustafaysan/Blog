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
    /// articlecommnets api controller
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ArticlesCommentController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticlesCommentController> _logger;

        public ArticlesCommentController(IArticleService articleService,
            ILogger<ArticlesCommentController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        /// <summary>
        /// search articles ArticleCommentPageOptionsModel model parameter
        /// </summary>
        /// <param name="pageOptions">search parameters</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of Article comment for the specified page.", typeof(ArticleCommentPageResultModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public IActionResult Get([FromQuery] ArticleCommentPageOptionsModel pageOptions)
        {
            try
            {
                var result = _articleService.SearchArticlesComment(pageOptions.CommentText, pageIndex: (pageOptions.Page - 1), pageSize: pageOptions.Count);
                if (result.TotalCount == 0)
                    return new NotFoundResult();

                var model = new ArticleCommentPageResultModel
                {
                    TotalCount = result.TotalCount,
                    TotalPages = result.TotalPages,
                    Page = pageOptions.Page,
                    Count = result.Items.Count,
                    Items = result.Items.Select(x => new ArticleCommentModel
                    {
                        Id=x.Id,
                        CommentText = x.CommentText,
                        ArticleId=x.ArticleId,
                        IsApproved=x.IsApproved
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
        /// Get article comment by id
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
                var entity = _articleService.GetArticleCommentById(id);
                if (entity == null)
                    return new NotFoundResult();

                var model = new ArticleCommentModel
                {
                    Id=entity.Id,
                    CommentText = entity.CommentText,
                    ArticleId=entity.ArticleId,
                    IsApproved=entity.IsApproved
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
        /// Add article as ArticleModelComment model
        /// </summary>
        /// <param name="model">ArticleModelComment model</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Insert success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public IActionResult Post([FromBody] ArticleCommentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new ArticleComment
                    {
                        CommentText = model.CommentText,
                        ArticleId=model.ArticleId,
                        IsApproved=model.IsApproved
                    };
                    _articleService.AddArticleComment(entity);

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
        /// Update article comment by id
        /// </summary>
        /// <param name="id">article comment id</param>
        /// <param name="model">ArticleModel model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public IActionResult Put(int id, [FromBody]ArticleCommentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _articleService.GetArticleCommentById(id);
                    if (entity == null)
                        return new NotFoundResult();

                    entity.CommentText = model.CommentText;
                    entity.ArticleId = model.ArticleId;
                    entity.IsApproved = model.IsApproved;

                    _articleService.UpdateArticleComment(entity);

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
        /// Delete article comment by id
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
                var entity = _articleService.GetArticleCommentById(id);
                if (entity == null)
                    return new NotFoundResult();

                entity.IsApproved = false;
                _articleService.UpdateArticleComment(entity);

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
