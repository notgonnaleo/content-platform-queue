using Microsoft.AspNetCore.Mvc;
using Newsletter.Articles.Api.Entities;
using Newsletter.Articles.Api.Repositories;

namespace Newsletter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsletterController : ControllerBase
{
    private readonly ArticleRepository _newsletterRepository;
    public NewsletterController(ArticleRepository newsletterRepository)
    {
        _newsletterRepository = newsletterRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> Get()
    {
        var response = await _newsletterRepository.Get();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> Get(int id)
    {
        var response = await _newsletterRepository.Get(id);
        return response != null ? Ok(response) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Article>> Post(Article article)
    {
        var newArticle = await _newsletterRepository.Post(article);
        return Ok(newArticle);
    }

    [HttpPost("{articleId}/like")]
    public async Task<ActionResult<Article>> Like(int articleId)
    {
        var response = await _newsletterRepository.Rate(articleId, true);
        return Ok(response);
    }

    [HttpPost("{articleId}/dislike")]
    public async Task<ActionResult<Article>> Dislike(int articleId)
    {
        var response = await _newsletterRepository.Rate(articleId, false);
        return Ok(response);
    }
}
